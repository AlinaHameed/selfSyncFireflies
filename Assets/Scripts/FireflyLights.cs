using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyLights : MonoBehaviour
{
    public Rigidbody rb;

    public Light lightsource;
    public int sightDistance;

    public float delayMultiplier;

    // Each of the following represents a number of milliseconds

    //The time it takes to reach the top of the threshold. 
    //Represented by THRESHOLD LEVEL (dotted line) in the diagram
     float chargeThreshold = 800;

    //The time between message sending and light flashing. 
    //Represented by timescale distance between MESSAGE and FLASH on diagram
     float sendDelay = 200;

    //The time between message sending and the chargingProcess restarting from zero. 
    //Represented by timescale distance between MESSAGE and the bottom of the chargingProgress on diagram
     float waitDelay = 200;
     float flashTimer = 400;

    public float flashProgress = 0;

    public float chargingProgress = 0;

    public float sendingProgress = 0;
    public float waitProgress = 0;
    // Start is called before the first frame update
    void Start()
    {
        chargeThreshold *= delayMultiplier;
        sendDelay *= delayMultiplier; 
        waitDelay *= delayMultiplier; 
        flashTimer *= delayMultiplier;

        rb = GetComponent<Rigidbody>();
        EventManager.current.OnFireflyFlash += SenseFlashes;
        chargingProgress = Random.Range(0,chargeThreshold);
        StartCoroutine(Charge());
        lightsource.intensity = 0;
    }

    // Charge will increment chargingProgress every 0.001s, stopping when it hits the threshold. 
    // Used to control when the message is sent to flash
    IEnumerator Charge(){
        while (chargingProgress < chargeThreshold){ 
            chargingProgress += Time.deltaTime * 1000;
            yield return new WaitForSeconds(0.001f);
        }
        chargingProgress = 0;
        StartCoroutine(WaitToCharge());
    }

    // Wait will increment Waiting progress every 0.001s, stopping when it hits the threshold,
    // Used to control the timing between when a message is sent to flash and when the firefly begins charging again
    IEnumerator WaitToCharge() {
        while (waitProgress < waitDelay) { 
            waitProgress += Time.deltaTime * 1000;
            yield return new WaitForSeconds(0.001f);
        }
        waitProgress = 0;
        StartCoroutine(SendMessageToFlash());
        StartCoroutine(Charge());
    }

    // SendMessageToFlash will increment sendingProgress every 0.001s, stopping when it hits the threshold.
    // Used to control when a flash is emitted after the message to flash has been sent
    IEnumerator SendMessageToFlash() {
        while (sendingProgress < sendDelay) { 
            // keeps track of how long you've been waiting here
            sendingProgress += Time.deltaTime * 1000;

            yield return new WaitForSeconds(0.001f);
        }

        sendingProgress = 0;
        Flash();
    }

    //SenseFlashes will be run when the firefly sees another flash nearby.
    private void SenseFlashes(Vector3 position){
        float distance = Vector3.Distance(this.transform.position, position);
        
        if(distance > 0 && distance < sightDistance){
            chargingProgress = 0;
            waitProgress = waitDelay;
        }
        
    }

    private void Flash(){
        StartCoroutine(FlashColor());
        EventManager.current.FireflyFlash(this.transform.position);
    }

    IEnumerator FlashColor(){
        while(flashProgress < flashTimer){
            flashProgress += Time.deltaTime * 1000;
            if(flashProgress < flashTimer/2){
                lightsource.intensity += 0.04f * Time.deltaTime * 100 / delayMultiplier;
            }
            else{
                lightsource.intensity -= 0.04f * Time.deltaTime * 100 / delayMultiplier;
            }
            yield return new WaitForSeconds(0.001f);
        }
        flashProgress = 0;
        lightsource.intensity = 0f;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            EventManager.current.FireflyFlash(this.transform.position + new Vector3(1f,1f,1f));
        } 
    }
}

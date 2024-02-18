using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Start is called before the first frame update


    // -------- STUDENTS CAN IGNORE THIS FILE --------

    [Header("Spawn Setup")]
    public FlockUnit firefly;
    public int spawnNumber = 100;
    public int boundarySize = 20;

    
    public FlockUnit[] allFireflies {get; set;}

        [Header("Flashing Setup (Cannot be changed during runtime)")]

    [Range(0, 10)]
    [SerializeField] private int _sightDistance;
    public int sightDistance {get {return _sightDistance;}}

    [Range(0, 5)]
    [SerializeField] private float _delayMultiplier;
    public float delayMultiplier {get {return _delayMultiplier;}}


    [Header("Speed Setup")]
    [Range(0, 10)]
    [SerializeField] private float _minSpeed;
    public float minSpeed {get {return _minSpeed;}}
    [Range(0, 10)]
    [SerializeField] private float _maxSpeed;
    public float maxSpeed {get {return _maxSpeed;}}

   
    [Header("Detection Distances")]
    [Header("--------Flocking Behaviours-----------")]

   
    [Range(0, 10)]
    [SerializeField] private float _cohesionDistance;
    public float cohesionDistance {get {return _cohesionDistance;}}
    [Range(0, 10)]
    [SerializeField] private float _alignmentDistance;
    public float alignmentDistance {get {return _alignmentDistance;}}
    [Range(0, 10)]
    [SerializeField] private float _avoidanceDistance;
    public float avoidanceDistance {get {return _avoidanceDistance;}}

    


    [Range(0, 10)]
    [SerializeField] private float _boundsDistance;
    public float boundsDistance {get {return _boundsDistance;}}

    [Header("Behaviour Weights")]
    [Range(0, 10)]
    [SerializeField] private float _cohesionWeight;
    public float cohesionWeight {get {return _cohesionWeight;}}
    [Range(0, 10)]
    [SerializeField] private float _alignmentWeight;
    public float alignmentWeight {get {return _alignmentWeight;}}
    [Range(0, 10)]
    [SerializeField] private float _avoidanceWeight;
    public float avoidanceWeight {get {return _avoidanceWeight;}}
    [Range(0, 10)]
    [SerializeField] private float _boundsWeight;
    public float boundsWeight {get {return _boundsWeight;}}

    void Start()
    {
        GenerateFireflies();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < allFireflies.Length; i++){
            allFireflies[i].MoveUnit();
        }
    }

    private void GenerateFireflies(){
        allFireflies = new FlockUnit[spawnNumber];
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        for (int i = 0; i < spawnNumber; i++){
            FlockUnit newFirefly = Instantiate(firefly);
            allFireflies[i] = newFirefly;
            
            newFirefly.transform.position = new Vector3(Random.Range(x - boundarySize , x + boundarySize),Random.Range(y - boundarySize, y + boundarySize),Random.Range(z - boundarySize, z + boundarySize));
            newFirefly.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            newFirefly.transform.parent = this.transform;
            FireflyLights lightsScript = newFirefly.GetComponent<FireflyLights>();
            lightsScript.sightDistance = _sightDistance;
            lightsScript.delayMultiplier = _delayMultiplier;
            newFirefly.InititializeSpeed(Random.Range(minSpeed, maxSpeed));
            newFirefly.AssignFlock(this);
        }
    }
}

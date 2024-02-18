using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float speed = 1f;
    public GameObject cam;
    public float rotateY;
    public float sensitivity = 15f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rotateY += Input.GetAxis("Mouse X") * sensitivity;
        transform.localEulerAngles = new Vector3(transform.rotation.x, rotateY, 0);

        float x = Input.GetAxis("Horizontal");
        rb.velocity = transform.forward * x * speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // Start is called before the first frame update

    public float rotateX = 0f;
    public float rotateY = 0f;

    public float sensitivity = 5f;

    // Update is called once per frame
    void Update()
    {

        rotateY = transform.rotation.y;
        
        rotateX += Input.GetAxis("Mouse Y") * sensitivity * -1;
        rotateX = Mathf.Clamp(rotateX, -90, 90);

        transform.localEulerAngles = new Vector3(rotateX, rotateY, 0);
    }
}

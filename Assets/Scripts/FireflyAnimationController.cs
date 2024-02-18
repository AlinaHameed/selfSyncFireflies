using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyAnimationController : MonoBehaviour
{
    [Header("Range of Animation Speed")]
    public float minAnimationSpeed = 3;
    public float maxAnimationSpeed = 7;

    // Start is called before the first frame update
    void Start()
    {
        Animator anim = GetComponent<Animator>();

        float animationSpeed = Random.Range(minAnimationSpeed, maxAnimationSpeed);

        //should not be null, but just in case...
        if (null != anim)
        {
            anim.speed = animationSpeed;
        }
    }
}

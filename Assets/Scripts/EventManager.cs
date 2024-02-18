using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public static EventManager current;

    private void Awake(){
        current = this;
    }
    
    public event Action<Vector3> OnFireflyFlash;
    public void FireflyFlash(Vector3 position)
    {
        if(OnFireflyFlash != null){
            OnFireflyFlash(position);
        }
    }
}

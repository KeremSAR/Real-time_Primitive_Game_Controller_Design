using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickHit : MonoBehaviour
{
    public float offset;
    private BoxCollider _boxCollider;
    void Start()
    {
        _boxCollider = this.gameObject.GetComponent<BoxCollider>();
    }
    
    
    private static float WrapAngle(float angle)
    {
        angle%=360;
        if(angle >180)
            return angle - 360;
 
        return angle;
    }
    // do the operation when hits the object
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "spawnObj")
        {
            float rotZ = this.gameObject.transform.rotation.eulerAngles.z;
            float ObjrotZ = other.gameObject.transform.rotation.z;
            Debug.Log("objrot değeri " +(ObjrotZ));
            Debug.Log("stick rot değeri" +(WrapAngle(rotZ)));
            if ((ObjrotZ-offset<=WrapAngle(rotZ))&&(WrapAngle(rotZ)<=ObjrotZ+offset))
            {
              //  other.gameObject.SetActive(false);
            }
            
        }
      
    }
    
    void Update()
    {
        
    }
}

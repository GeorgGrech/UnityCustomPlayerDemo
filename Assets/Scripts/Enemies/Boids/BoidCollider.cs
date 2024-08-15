using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidCollider : MonoBehaviour
{
    
    public Collider parentCol;

    void Start()
    {
        parentCol = this.GetComponentInParent<Collider>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "boidColliderTrigger")
        {
            parentCol.enabled = enabled;
        }
    }
}

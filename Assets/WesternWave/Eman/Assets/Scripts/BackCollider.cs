using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCollider : MonoBehaviour
{
    FloorPoolSystem pool;

    // Start is called before the first frame update
    void Start()
    {
        pool = GameObject.FindObjectOfType<FloorPoolSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor") 
        {
            pool.PutForward(other.gameObject);
        }
    }
}

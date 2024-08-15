using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
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
        Debug.Log("Collided");
        if (other.gameObject.tag == "Train")
        {
            Debug.Log("Collided");
            //pool.PutForward();
        }
    }
}

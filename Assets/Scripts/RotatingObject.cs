using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool antiClockwise;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotSpeed;

        if (antiClockwise)
            rotSpeed = -speed;
        else
            rotSpeed = speed;

        transform.Rotate(new Vector3(0,rotSpeed*Time.deltaTime,0));
    }
}

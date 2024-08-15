using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformTransform : MonoBehaviour
{
    [SerializeField] Vector3[] waypoints;
    [SerializeField] float speed;
    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        destination = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,destination,speed*Time.deltaTime);

        if(transform.position == destination)
        {
            if(destination == waypoints[0])
            {
                destination = waypoints[1];
            }
            else
            {
                destination = waypoints[0];
            }
        }
    }
}

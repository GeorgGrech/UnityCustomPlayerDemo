using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed;
    private Vector3 destination;

    Rigidbody rb;
    private int currentWaypoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        destination = waypoints[currentWaypoint].position;
        transform.position = destination;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = destination - transform.position;
        direction.Normalize();
        rb.MovePosition(transform.position + (speed * Time.fixedDeltaTime * direction));
        
        if (transform.position == destination)
        {
            currentWaypoint++;

            if(currentWaypoint == waypoints.Length)
            {

                currentWaypoint = 0;
            }
            destination = waypoints[currentWaypoint].position;
        }
    }
}

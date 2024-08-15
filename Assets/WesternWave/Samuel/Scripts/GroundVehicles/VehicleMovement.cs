using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
   
    public Transform playerTransform; // Reference to the player's transform
    public float trackingSpeed = 1.0f; // Adjust the tracking speed to control smoothness



    public void Start()
    {
       trackingSpeed = Random.Range(0.5f,2.0f);
       playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

  void FixedUpdate()
{
    if (playerTransform != null)
    {
        Vector3 currentPosition = transform.position;

        Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);

        if(currentPosition.x != targetPosition.x)
        {
            transform.position = Vector3.Lerp(currentPosition, targetPosition, trackingSpeed * Time.fixedDeltaTime);
        }

    }   
}

}

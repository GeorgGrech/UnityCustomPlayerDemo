using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainWheels : MonoBehaviour
{
     public float rotationSpeed = -500.0f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the propeller around its local X-axis
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}

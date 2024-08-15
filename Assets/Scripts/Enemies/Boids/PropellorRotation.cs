using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellorRotation : MonoBehaviour
{
    public float rotationSpeed = 500.0f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the propeller around its local Z-axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}

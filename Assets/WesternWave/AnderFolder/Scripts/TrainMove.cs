using UnityEngine;

public class TrainMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    public Transform resetPoint; // Set this in the inspector to the original position

    private void Update()
    {
        MoveTrain();
    }

    private void MoveTrain()
    {
        // Move the train in its local right direction (positive Z-axis)
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collider is named "end"
        if (collision.gameObject.CompareTag("end") && !IsInvoking("ResetTrain"))
        {
            // If it is, reset the train position after a delay
            Invoke("ResetTrain", 0.5f);
        }
    }

    private void ResetTrain()
    {
        // Reset the train position to the original position
        transform.position = resetPoint.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public float duration = 0.5f; // Adjust this value to control the speed of the movement


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveForwardCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveForwardCoroutine()
    {
        while (true)
        {
            Vector3 targetPosition = this.transform.position + Vector3.right; // Set the target position to move one unit to the right


            float elapsedTime = 0f;
            Vector3 startPosition = this.transform.position;

            while (elapsedTime < duration)
            {
                this.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Ensure that the object reaches the exact target position
            this.transform.position = targetPosition;
        }
    }
}

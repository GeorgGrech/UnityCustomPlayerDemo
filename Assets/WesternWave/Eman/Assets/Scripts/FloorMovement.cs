using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    public float duration = 0.5f; // Adjust this value to control the speed of the movement
    private Coroutine moveCoroutine;

    void OnEnable()
    {
        moveCoroutine = StartCoroutine(MoveForwardCoroutine());
    }

    void OnDisable()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
    }
    IEnumerator MoveForwardCoroutine()
    {
        while (true)
        {
            Vector3 targetPosition = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);


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

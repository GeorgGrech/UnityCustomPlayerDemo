using UnityEngine;

public class ImageActivator : MonoBehaviour
{
    public GameObject inactiveImage;
    private float timer = 0f;
    private bool isActive = false;

    // Adjust these values according to your requirements
    public float activationInterval = 30f; // Time in seconds before activation
    public float deactivationInterval = 60f; // Time in seconds before deactivation

    void Update()
    {
        timer += Time.deltaTime;

        if (!isActive && timer >= activationInterval)
        {
            // Activate the image
            inactiveImage.SetActive(true);
            isActive = true;

            // Reset the timer for the next deactivation
            timer = 0f;
        }
        else if (isActive && timer >= deactivationInterval)
        {
            // Deactivate the image
            inactiveImage.SetActive(false);
            isActive = false;

            // Reset the timer for the next activation
            timer = 0f;
        }
    }
}

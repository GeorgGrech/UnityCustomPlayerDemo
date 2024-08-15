using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform playerTransform;

    private void Start()
    {
        // Assuming the player is the main camera in a 3D game
        playerTransform = Camera.main.transform;
    }

    void Update()
    {
        // Make the game object look at the player (camera)
        transform.LookAt(playerTransform);
    }
}

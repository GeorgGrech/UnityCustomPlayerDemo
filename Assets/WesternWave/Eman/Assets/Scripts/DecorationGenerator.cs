using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationGenerator : MonoBehaviour
{
    public GameObject[] decorations;
    public int numberOfDecorationsToSpawn = 5; // Number of decorations you want to spawn
    public Vector2 floorDimensions;

    void Start()
    {
        // Use the scale of the floor to determine its dimensions.
        floorDimensions = new Vector2(transform.localScale.x, transform.localScale.z);

        for (int i = 0; i < numberOfDecorationsToSpawn; i++)
        {
            SpawnDecoration();
        }
    }

    void SpawnDecoration()
    {
        // Get a random decoration from the array
        GameObject decoration = decorations[Random.Range(0, decorations.Length)];

        // Calculate random spawn position relative to the floor's position.
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(-floorDimensions.x / 2, floorDimensions.x / 2),
            transform.position.y + 0.5f, // Adjust this if your prefab's base is not at y=0 in local space.
            transform.position.z + Random.Range(-floorDimensions.y / 2, floorDimensions.y / 2)
        );

        // Instantiate the decoration at the chosen position
        Instantiate(decoration, spawnPosition, Quaternion.identity, this.transform);
    }
}

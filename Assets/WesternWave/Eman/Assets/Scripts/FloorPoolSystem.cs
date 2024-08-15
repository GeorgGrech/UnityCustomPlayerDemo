using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FloorPoolSystem : MonoBehaviour
{
    public GameObject Train;
    public List<GameObject> FloorPrefabs;

    public int poolSize = 7;
    public float scaleX;

    public float furthest;

    public Queue<GameObject> floorPool = new Queue<GameObject>();
    public List<GameObject> activeFloors = new List<GameObject>();

    public float floorSpeed = 5f;

    //public List<Biome> biomes;
    //public int biomeCount = 0;

    // Initialize the object pool
    void Start()
    {
        //scaleX = FloorPrefab.transform.localScale.x;

        InitializePool();
        float xOffset = -poolSize / 2; // Initialize xOffset
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newFloor = GetObjectFromPool();
            newFloor.transform.position = new Vector3(Train.transform.position.x + xOffset * scaleX, Train.transform.position.y - 2, Train.transform.position.z);
            xOffset += 1; // Increment xOffset by 1 for the next floor
            furthest = newFloor.transform.position.x;
        }
    }

    void Update()
    {
        MoveFloorsBackward();
    }

    void MoveFloorsBackward()
    {
        foreach (GameObject floor in activeFloors)
        {
            floor.transform.position -= Vector3.right * floorSpeed * Time.deltaTime;
        }
    }

    // Create and enqueue objects in the pool
    void InitializePool()
    {
        int j = 0;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(FloorPrefabs[j]);
            obj.SetActive(false);
            floorPool.Enqueue(obj);
            //floorList.Remove(obj);
            j++;
            if(j == FloorPrefabs.Count) { j = 0; }
        }
    }

    // Retrieve an object from the pool
    public GameObject GetObjectFromPool()
    {
        if (floorPool.Count == 0)
        {
            Debug.LogWarning("Object pool is empty. Consider resizing it.");
            return null;
        }

        GameObject obj = floorPool.Dequeue();
        obj.SetActive(true);
        activeFloors.Add(obj);  // Add the floor to the activeFloors list
        return obj;
    }

    // Return an object to the pool
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = new Vector3(furthest + 1, transform.position.y, transform.position.z);  // Reset the position of the object
        floorPool.Enqueue(obj);
        activeFloors.Remove(obj);  // Remove the floor from the activeFloors list
    }

    public void PutForward(GameObject floor)
    {
        ReturnObjectToPool(floor);

        // You might want to find the position of the furthest active floor rather than relying on a separate 'furthest' variable.
        float newFloorXPosition = FindFurthestFloorXPosition() + scaleX;
        GameObject newFloor = GetObjectFromPool();
        newFloor.transform.position = new Vector3(newFloorXPosition, Train.transform.position.y - 2, 0);
    }

    float FindFurthestFloorXPosition()
    {
        float furthestX = float.NegativeInfinity;
        foreach (GameObject floor in activeFloors)
        {
            if (floor.transform.position.x > furthestX)
            {
                furthestX = floor.transform.position.x;
            }
        }
        return furthestX;
    }



    //public GameObject FindFurthest()
    //{
    //    GameObject furthest = null;
    //    float max = float.MinValue;
    //    foreach (GameObject obj in floorList)
    //    {
    //        if (obj.transform.position.x > max)
    //        {
    //            max = obj.transform.position.x;
    //            furthest = obj;
    //        }
    //    }

    //    return furthest;
    //}

}

[System.Serializable]
public class Biome
{
    public string Name;
    public GameObject[] Sections;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorRayCast : MonoBehaviour
{
      void Update()
    {
        // Check for mouse button click (e.g., left mouse button)
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the cursor position into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit an object
                GameObject hitObject = hit.collider.gameObject;
                
                // You can now interact with the hitObject or perform any desired actions
                Debug.Log("Hit object: " + hitObject.name);
                if(hitObject.tag == "boid")
                {
                    MeshRenderer meshComponent = hitObject.GetComponentInChildren<MeshRenderer>();
                    Collider col = hitObject.GetComponent<Collider>();
                    Destroy(meshComponent);
                    Destroy(col);
                }
                // For example, you could call a method on the hit object:
                // hitObject.SendMessage("YourMethodName");
            }
        }
    }   
}

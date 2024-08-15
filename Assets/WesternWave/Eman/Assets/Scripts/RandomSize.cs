using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSize : MonoBehaviour
{
    public float size;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(Random.Range(0f, size), Random.Range(0f, size), Random.Range(0f, size));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

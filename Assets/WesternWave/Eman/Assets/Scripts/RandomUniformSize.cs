using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUniformSize : MonoBehaviour
{
    public float size;
    // Start is called before the first frame update
    void Start()
    {
        float newSize = Random.Range(0, size);
        this.gameObject.transform.localScale = new Vector3(newSize, newSize, newSize);
    }
}

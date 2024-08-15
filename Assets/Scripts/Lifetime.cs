using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    [SerializeField] private float duration;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifetimeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}

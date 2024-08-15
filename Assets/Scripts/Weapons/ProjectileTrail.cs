using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lifetime of Projectile Trail independent of projectile
/// </summary>
public class ProjectileTrail : MonoBehaviour
{
    [SerializeField] private float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLifetime()
    {
        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}

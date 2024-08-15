using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float range;

    private Vector3 initPos;

    [SerializeField] private GameObject trailPrefab;
    private GameObject trail;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        trail = Instantiate(trailPrefab,initPos,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(initPos, transform.position) > range)
        {
            Debug.Log("Out of range. Destroyed.");
            DestroyProjectile();
        }
        else
        {
            trail.transform.position = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        collision.gameObject.SendMessageUpwards("ChangeHealth", -damage, SendMessageOptions.DontRequireReceiver);
        Debug.Log("Collided with "+collision.gameObject.name);
        DestroyProjectile();
    }

    
    private void DestroyProjectile()
    {
        trail.GetComponent<ProjectileTrail>().StartLifetime();
        Destroy(gameObject);
    }
}

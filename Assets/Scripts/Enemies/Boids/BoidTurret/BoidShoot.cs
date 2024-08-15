using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidShoot : MonoBehaviour
{
    public Rigidbody projectile;
    public float initialSpeed = 20.0f;
    public float reloadTime = 0.5f;
    //public int ammoCount = 20;
    private float lastShot = -10.0f;

    

    private Rigidbody rb;
    private Transform playerTransform;
    private Health boidHealth;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        rb = player.GetComponent<Rigidbody>();
        boidHealth = GetComponentInParent<Health>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            Fire();
        }
    }

    private void Fire()
    {
        // Did the time exceed the reload time?
        if (Time.time > reloadTime + lastShot /*&& ammoCount > 0*/ && !boidHealth.dead)
        {
            // Create a new projectile, use the same position and rotation as the Launcher.
            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation);

            // Time of flight to reach the player
            float t = (playerTransform.position - transform.position).magnitude / initialSpeed;

            // Project the future position
            Vector3 futurePos = playerTransform.position + rb.velocity * t;

            // Aim and fire at that future position
            Vector3 aim = (futurePos - transform.position).normalized;
            instantiatedProjectile.transform.rotation = Quaternion.LookRotation(aim);
            instantiatedProjectile.velocity = aim * initialSpeed;

            // Ignore collisions between the missile and the character controller
            Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

            lastShot = Time.time;
            //ammoCount--;
        }
    }
}



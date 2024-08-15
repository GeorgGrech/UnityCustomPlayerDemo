using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    // Start is called before the first frame update
    Boid boid;

    [SerializeField] Weapon gun;
    Transform target;
    LayerMask obstacleMask;
    BoidManager boidManager;
    [SerializeField] float angle;
    [SerializeField] float distance;
    
    void Start()
    {
        boidManager = GameObject.Find("BoidManager").GetComponent<BoidManager>();
        target = boidManager.boidsTargets[0].transform;
        obstacleMask = boidManager.settings.obstacleMask;
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetInSight())
        {
            gun.AIFire();
        }
    }

    bool TargetInSight()
    {

        if(Vector3.Distance(gun.transform.position, target.position) < distance) //Within distance
        {
            Debug.Log("Within distance");
            Vector3 dirToPlayer = (target.position - gun.transform.position).normalized;
            float angleBetweenPlayer = Vector3.Angle(gun.transform.forward, dirToPlayer);
            if (angleBetweenPlayer < angle / 2f) //Within the viewing angle
            {
                Debug.Log("Within angle");
                if (!Physics.Linecast(gun.transform.position, target.position, obstacleMask))//if view to player is not being obstructed by obstacle (Camera used to dictate by player POV)
                {
                    Debug.Log("Player in sight");
                    return true;
                }
                Debug.Log("blocked");
            }
            return false;
        }
        return false;
    }
}

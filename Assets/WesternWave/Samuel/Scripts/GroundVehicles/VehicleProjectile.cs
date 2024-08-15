using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleProjectile : MonoBehaviour
{
   [SerializeField] private float lifetime;

    public float shotDamage = 3f;

    public GameObject GlobalVolume;
    void Start()
    {
        GlobalVolume = GameObject.Find("Global Volume");
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            collider.SendMessageUpwards("ChangeHealth", -shotDamage, SendMessageOptions.DontRequireReceiver);
            GlobalVolume.SendMessageUpwards("PlayDamageAnimation", SendMessageOptions.DontRequireReceiver);
        }

        StartLifetime();
        
    }

     private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    public void StartLifetime()
    {
        StartCoroutine(Lifetime());
    }

}

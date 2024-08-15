using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutOfBounds : MonoBehaviour
{
    [SerializeField] private Transform playerTeleportPoint;
    [SerializeField] private float damage;

    private GameObject GlobalVolume;

    private void Start()
    {
        GlobalVolume = GameObject.Find("Global Volume");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            GlobalVolume.SendMessageUpwards("PlayDamageAnimation", SendMessageOptions.DontRequireReceiver);
            other.SendMessageUpwards("ChangeHealth", -damage, SendMessageOptions.DontRequireReceiver);
            other.transform.position = playerTeleportPoint.position;
        }
    }
}

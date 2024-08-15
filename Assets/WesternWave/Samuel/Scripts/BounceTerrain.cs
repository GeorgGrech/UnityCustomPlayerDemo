using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTerrain : MonoBehaviour
{
    private AudioManager audioManager;
    // Timer to track collision time
    float _timeColliding;
	// Time before damage is taken, 1 second default
	public float timeThreshold = 1f;

    public float launchForce;

//    void OnCollisionEnter(Collision collider)
//    {
//     if(collider.gameObject.tag == "Player")
//     {
//         collider.gameObject.SendMessageUpwards("RespawnPlayer", SendMessageOptions.DontRequireReceiver);
//     }
//    }

    public GameObject GlobalVolume;
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        launchForce = 30f;
        GlobalVolume = GameObject.Find("Global Volume");
    }

   void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			// _timeColliding = 0f;

			// Debug.Log("Enemy started colliding with player.");

			// collision.gameObject.SendMessageUpwards("ChangeHealth", -20f, SendMessageOptions.DontRequireReceiver);
            // GlobalVolume.SendMessageUpwards("PlayDamageAnimation", SendMessageOptions.DontRequireReceiver);
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // Apply an upward force
                Debug.Log("LaunchPlayer");
                playerRigidbody.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
                audioManager.playSound(7);
                collision.gameObject.SendMessageUpwards("ChangeHealth", -20f, SendMessageOptions.DontRequireReceiver);
                GlobalVolume.SendMessageUpwards("PlayDamageAnimation", SendMessageOptions.DontRequireReceiver);

            }
		}
    }
//    void OnCollisionStay(Collision collision)
//    {
//         if(collision.gameObject.tag == "Player")
//         {
//             if (_timeColliding < timeThreshold) {
//                     _timeColliding += Time.deltaTime;
//                 } else {
//                     // Time is over theshold, player takes damage
//                     collision.gameObject.SendMessageUpwards("ChangeHealth", -20f, SendMessageOptions.DontRequireReceiver);
//                     GlobalVolume.SendMessageUpwards("PlayDamageAnimation", SendMessageOptions.DontRequireReceiver);
//                     // Reset timer
//                     _timeColliding = 0f;
//                 }
//         }
//    }

}

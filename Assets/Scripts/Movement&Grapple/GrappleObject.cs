using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleObject : MonoBehaviour
{
    private AudioManager audioManager;
    public Vector3 grapplePoint;
    public PlayerGrapple playerGrapple;
    public float grappleSpeed;

    public float maxGrappleDistance;
    public Vector3 playerVel; //Used for inheritance

    //public Vector3 surfaceNormal;

    public string grappleableLayer;

    private bool hasConnected = false;

    public Coroutine grappleCoroutine; //Used for both throwing grapple and pulling player

    public GameObject grappleModelPrefab; // Model to spawn and move in accordance to grapple
    public GameObject spawnedGrappleModel;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasConnected)
        {
            playerGrapple.grapplePoint = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision) //Collision with actual surfaces
    {
        if (!hasConnected) //Grapple hasn't connected to anything yet
        {
            if(LayerMask.LayerToName(collision.gameObject.layer) == grappleableLayer) //If collision is grappleable environment
            {
                audioManager.playSound(5);
                //Debug.Log("Connected to Grappleable surface: "+collision.gameObject.name);

                hasConnected = true;

                GetComponent<Rigidbody>().isKinematic = true;
                StopCoroutine(grappleCoroutine); //Stop grapple Lifetime

                spawnedGrappleModel.transform.rotation = Quaternion.FromToRotation(Vector3.forward, collision.GetContact(0).normal);
                spawnedGrappleModel.transform.Rotate(new Vector3(180, 0, 0)); //Flip around
                //transform.rotation = Quaternion.FromToRotation(Vector3.forward, surfaceNormal);

                playerGrapple.pullingPlayer = true;
                //grappleCoroutine = StartCoroutine(playerGrapple.PullPlayer()); //Start pulling player

                collision.gameObject.SendMessageUpwards("GrappleAttached", this, SendMessageOptions.DontRequireReceiver);
                transform.parent = collision.transform;
            }
            else //If collision isn't grappleable environment
            {
                Debug.Log("Hit non-Grappleable surface");
                //Cancel functionality
                CancelGrapple();
            }
        }
    }

    public void ThrowGrapple()
    {
        transform.LookAt(grapplePoint);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * grappleSpeed);
        rb.velocity += playerVel; //Add player Velocity in case of inheritance. If inheritance is off this will be (0,0,0)


        grappleCoroutine = StartCoroutine(Lifetime());
        spawnedGrappleModel = Instantiate(grappleModelPrefab, transform.position, transform.rotation);
        playerGrapple.spawnedGrappleModel = spawnedGrappleModel;
    }

    public void CancelGrapple()
    {
        
        if (grappleCoroutine != null)
        {
            StopCoroutine(grappleCoroutine);
        }

        playerGrapple.lr.enabled = false;
        playerGrapple.grappling = false;
        playerGrapple.EnableGrappleModel(true);
        audioManager.playSound(6);
        Destroy(spawnedGrappleModel);
        Destroy(gameObject);
    }

    private IEnumerator Lifetime()
    {
        Vector3 startPos = transform.position;

        while (Vector3.Distance(startPos, transform.position) <= maxGrappleDistance)
        {
            yield return null;
        }

        Debug.Log("Grapple out of range, destroyed");
        CancelGrapple();
    }
}

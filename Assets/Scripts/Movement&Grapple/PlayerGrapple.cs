using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerGrapple : MonoBehaviour
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private GameObject grapplePrefab;
    [SerializeField] private GameObject grappleViewModel;

    public bool grappling = false;
    public bool pullingPlayer = false;

    [SerializeField] private Transform throwPoint;
    [SerializeField] private float maxGrappleDistance;
    [SerializeField] private float maxPullSpeed;
    [SerializeField] private float pullForce;
    [Space(10)]

    [Header("Instantiated Grapple Properties")]
    [SerializeField] private float grappleSpeed;
    [Space(10)]
    public Vector3 grapplePoint;
    public LayerMask grappleableLayer;
    public string grappleableLayerName; //String used due to issue with LayerMask. TO DO: Check if issues can be resolved to use LayerMask.
    
    private GrappleObject grappleObject;
    [SerializeField] bool inheritance; //Inherit player's velocity

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;


    [Header("Automatic cancel settings")]

    [SerializeField]
    [InspectorName("Cancel Grapple when view obstructed")]
    private bool cancelWhenObstructed;

    [SerializeField]
    [InspectorName("Automatically cancel grapple when in proximity")]
    private bool cancelWhenClose;
    [SerializeField] private float autoCancelDistance;

    private Rigidbody rb;

    public LineRenderer lr;

    public GameObject spawnedGrappleModel; //TO DO: Necessary due to a bug. Check if it can be removed.



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(grappleKey) && !grappling) StartGrapple();

        if(grappleObject)
        {
            if (Input.GetKeyUp(grappleKey) && grappling) grappleObject.CancelGrapple();
        }
        else //Emulate CancelGrapple. TO DO: Refactor to use one CancelGrapple method globally
        {
            lr.enabled = false;
            grappling = false;
            pullingPlayer = false;
            Destroy(spawnedGrappleModel);
        }

        if (grappling) //Auto cancel options
        {
            if (grappleObject)
            {
                lr.SetPosition(0, throwPoint.position);
                lr.SetPosition(1, grappleObject.transform.position);

                grappleObject.spawnedGrappleModel.transform.position = grappleObject.transform.position; //TO DO: Ideally this would be done in GrappleObject
                if ((cancelWhenObstructed
                    && Physics.Linecast(throwPoint.position, grappleObject.transform.position, grappleableLayer)) // Cancel when obstructed
                    
                    || (cancelWhenClose
                    && Vector3.Distance(grapplePoint,transform.position) <= autoCancelDistance)) //Cancel when in proximity of grapple
                {
                    grappleObject.CancelGrapple(); //Break grapple
                }
            }

        }
    }

    private void FixedUpdate() //Moved coroutine to fixedUpdate for more reliable physics
    {
        if (pullingPlayer)
        {
            Vector3 dir = (grapplePoint - transform.position).normalized;
            rb.AddForce(dir * pullForce); //Keep adding force

            Debug.Log("Magnitude: " + rb.velocity.magnitude);
            if (rb.velocity.magnitude > maxPullSpeed)
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxPullSpeed);
        }
    }

    private void StartGrapple()
    {
        grappling = true;
        lr.enabled = true;

      
        grapplePoint = playerCam.position + playerCam.forward * maxGrappleDistance;

        InstantiateGrapple();
        EnableGrappleModel(false);

        //lr.enabled = true;
        //lr.SetPosition(1, grapplePoint);
    }

    private void InstantiateGrapple()
    {
        grappleObject = GameObject.Instantiate(grapplePrefab,throwPoint.position,Quaternion.identity).GetComponent<GrappleObject>();

        //Terrible. Use a constructor or something.
        grappleObject.grappleSpeed = grappleSpeed;
        grappleObject.grapplePoint = grapplePoint;
        grappleObject.playerGrapple = this;

        grappleObject.grappleableLayer = grappleableLayerName;
        grappleObject.maxGrappleDistance= maxGrappleDistance;

        if (inheritance)
            grappleObject.playerVel = rb.velocity;

        grappleObject.ThrowGrapple();
    }

    public void EnableGrappleModel(bool enable)
    {
        grappleViewModel.SetActive(enable);
    }

    public IEnumerator PullPlayer()
    {
        while (true)
        {
            

            yield return null;
        }
    }


}

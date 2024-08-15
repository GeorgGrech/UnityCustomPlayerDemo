using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InertiaController : MonoBehaviour
{
    Rigidbody Rb;

    Transform playerTr;
    Rigidbody playerRb;
    [SerializeField] float lerpSpeed;
    [SerializeField] float rbAmplifier;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();

        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().GetChild(0).transform; //Get CameraAndRotation object
        playerRb = playerTr.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, playerTr.position,lerpSpeed*Time.deltaTime), playerTr.transform.rotation);

        Rb.velocity = playerRb.velocity*rbAmplifier;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    bool isDetected;
    List<GameObject> targets;
    public Transform archerAI;
    Quaternion initialRotation;

    public GameObject arrow;
    public Transform ShootPoint;

    public float arrowSpeed = 10f;
    public float timetoShoot = 1.3f;
    float originalTime;

    public float detectionRadius = 10f;
    public int damageAmount = 20;

    private Animator animator;
    public string animName;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInParent<Animator>();
        originalTime = timetoShoot;
        initialRotation = archerAI.rotation;
        targets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

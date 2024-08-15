using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGroup : MonoBehaviour
{
    Transform player;
    [SerializeField] private float height;
    [SerializeField] private float maxHeight;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(player.position.x,Mathf.Clamp(player.position.y+height,0,maxHeight),player.position.z);
    }
}

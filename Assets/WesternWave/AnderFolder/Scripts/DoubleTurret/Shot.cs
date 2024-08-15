using UnityEngine;

public class Shot : MonoBehaviour
{
    // public GameObject muzzlePrefab;
    public float speed;

    private Rigidbody rb;
    private Vector3 velocity;

    // Timer to track the time the Shot has been alive
    private float timeAlive = 0f;

    void Awake()
    {
        TryGetComponent(out rb);
    }

    void Start()
    {
        // var muzzleEffect = Instantiate(muzzlePrefab, transform.position, transform.rotation);
        // Destroy(muzzleEffect, 5f);
        velocity = transform.forward * speed;
    }

    void FixedUpdate()
    {
        var displacement = velocity * Time.deltaTime;
        rb.MovePosition(rb.position + displacement);

        // Update the time alive
        timeAlive += Time.deltaTime;

        // Check if the time alive has exceeded 10 seconds, and if so, destroy the Shot GameObject
        if (timeAlive >= 10f)
        {
            Destroy(gameObject);
        }
    }
}

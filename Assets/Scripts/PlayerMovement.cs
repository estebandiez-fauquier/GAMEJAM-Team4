
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Player is missing a Rigidbody!");
        }
    }

    void Update()
    {
        float x = 0f;
        float z = 0f;

        // Horizontal movement
        if (Input.GetKey(KeyCode.Q) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftArrow))
            x = -1f;

        if (Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow))
            x = 1f;

        // Vertical movement
        if (Input.GetKey(KeyCode.Z) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.UpArrow))
            z = 1f;

        if (Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.DownArrow))
            z = -1f;

        movement = new Vector3(x, 0f, z).normalized;
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        Vector3 velocity = rb.linearVelocity;

        velocity.x = movement.x * speed;
        velocity.z = movement.z * speed;

        rb.linearVelocity = velocity;
    }
}

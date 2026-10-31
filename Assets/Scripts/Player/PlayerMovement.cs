
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    private Rigidbody rb;
    private SpriteRenderer spriteRenderer;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        float x = 0f;
        float z = 0f;

        // Left
        if (Input.GetKey(KeyCode.Q) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftArrow))
        {
            x = -1f;
        }

        // Right
        if (Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            x = 1f;
        }

        // Up
        if (Input.GetKey(KeyCode.Z) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.UpArrow))
        {
            z = 1f;
        }

        // Down
        if (Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.DownArrow))
        {
            z = -1f;
        }

        movement = new Vector3(x, 0f, z).normalized;

        // Flip the sprite horizontally
        if (spriteRenderer != null)
        {
            if (x < 0f)
                spriteRenderer.flipX = false;

            else if (x > 0f)
                spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        if (rb == null)
            return;

        Vector3 velocity = rb.linearVelocity;

        velocity.x = movement.x * speed;
        velocity.z = movement.z * speed;

        rb.linearVelocity = velocity;
    }
}

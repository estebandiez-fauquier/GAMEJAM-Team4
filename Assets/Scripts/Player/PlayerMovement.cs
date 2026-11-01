
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    private Rigidbody rb;
    private SpriteRenderer spriteRenderer;
    private Vector3 movement;


    [Header("Step")]
    public float stepHeight = 0.3f;
    public float stepCheckDistance = 0.4f;
    public float feetOffset = 0.5f; // distance entre le centre du Player et ses pieds



    void TryStepUp()
    {
        Vector3 feet = transform.position - Vector3.up * feetOffset;
        Vector3 low = feet + Vector3.up * 0.05f;
        Vector3 high = feet + Vector3.up * stepHeight;

        if (Physics.Raycast(low, movement, stepCheckDistance) &&
            !Physics.Raycast(high, movement, stepCheckDistance + 0.1f))
        {
            rb.position += Vector3.up * stepHeight;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
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
                spriteRenderer.flipX = true;

            else if (x > 0f)
                spriteRenderer.flipX = false;
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

        if (rb == null) return;

        if (movement.sqrMagnitude > 0.01f) TryStepUp();
    }
}

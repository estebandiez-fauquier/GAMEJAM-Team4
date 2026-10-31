using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        movement = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.zKey.isPressed)
                movement.y += 1;

            if (Keyboard.current.sKey.isPressed)
                movement.y -= 1;

            if (Keyboard.current.aKey.isPressed || Keyboard.current.qKey.isPressed)
                movement.x -= 1;

            if (Keyboard.current.dKey.isPressed)
                movement.x += 1;

            if (Keyboard.current.upArrowKey.isPressed)
                movement.y += 1;

            if (Keyboard.current.downArrowKey.isPressed)
                movement.y -= 1;

            if (Keyboard.current.leftArrowKey.isPressed)
                movement.x -= 1;

            if (Keyboard.current.rightArrowKey.isPressed)
                movement.x += 1;
        }

        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(movement.x, 0f, movement.y);

        rb.linearVelocity = direction * speed;
    }
}
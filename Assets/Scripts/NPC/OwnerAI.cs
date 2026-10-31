
using UnityEngine;

public class OwnerAI : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] waypoints;
    public float speed = 2.5f;
    public float reachDistance = 0.5f;

    private Rigidbody rb;
    private int currentWaypoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb == null || waypoints == null ||
            waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypoint];

        Vector3 direction = target.position - rb.position;
        direction.y = 0f;

        if (direction.magnitude <= reachDistance)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            return;
        }

        direction.Normalize();

        Vector3 velocity = rb.linearVelocity;
        velocity.x = direction.x * speed;
        velocity.z = direction.z * speed;
        rb.linearVelocity = velocity;

        transform.forward = direction;
    }
}

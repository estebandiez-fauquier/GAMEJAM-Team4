
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
        if (rb == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypoint];

        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction.magnitude <= reachDistance)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            return;
        }

        direction.Normalize();

        rb.linearVelocity = new Vector3(
            direction.x * speed,
            rb.linearVelocity.y,
            direction.z * speed
        );
    }
}


using UnityEngine;

public class OwnerAI : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] waypoints;
    public float speed = 2.5f;
    public float reachDistance = 0.5f;

    private Rigidbody rb;
    private int currentWaypoint;
    [Header("Stun")]
    public float stunDuration = 3f;

    private float stunTimer = 0f;

    public bool IsStunned => stunTimer > 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb == null || waypoints == null || waypoints.Length == 0)
            return;

        if (stunTimer > 0f)
        {
            stunTimer -= Time.fixedDeltaTime;

            Vector3 stunVelocity = rb.linearVelocity;
            stunVelocity.x = 0f;
            stunVelocity.z = 0f;
            rb.linearVelocity = stunVelocity;

            return;
        }

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

    public void Stun(float duration)
    {
        stunTimer = Mathf.Max(stunTimer, duration);

        if (rb != null)
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.x = 0f;
            velocity.z = 0f;
            rb.linearVelocity = velocity;
        }

        Debug.Log("Owner stunned for " + duration + " seconds!");
    }
}

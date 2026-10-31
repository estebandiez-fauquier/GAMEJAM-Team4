
using UnityEngine;

public class GuestAI : MonoBehaviour
{
    [Header("Movement")]
    public float wanderSpeed = 2f;
    public float fleeSpeed = 4f;
    public float wanderRadius = 5f;

    [Header("References")]
    public Transform exitPoint;

    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool frightened;
    private bool leaving;

    public bool IsLeaving => leaving;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ChooseNewDestination();
    }

    void Update()
    {
        if (frightened && exitPoint != null)
        {
            targetPosition = exitPoint.position;
        }
        else if (!frightened &&
                 Vector3.Distance(transform.position, targetPosition) < 1f)
        {
            ChooseNewDestination();
        }
    }

    void FixedUpdate()
    {
        if (rb == null)
            return;

        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        if (direction.magnitude < 0.1f)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        direction.Normalize();

        float speed = frightened ? fleeSpeed : wanderSpeed;

        rb.linearVelocity = new Vector3(
            direction.x * speed,
            rb.linearVelocity.y,
            direction.z * speed
        );
    }

    void ChooseNewDestination()
    {
        Vector3 randomPosition = Random.insideUnitSphere * wanderRadius;
        randomPosition.y = 0f;

        targetPosition = transform.position + randomPosition;
    }

    public void Frighten()
    {
        if (leaving)
            return;

        frightened = true;
        Debug.Log(name + " is frightened!");
    }

    public void ReachExit()
    {
        if (!frightened || leaving)
            return;

        leaving = true;

        if (rb != null)
            rb.linearVelocity = Vector3.zero;

        GuestManager manager = FindFirstObjectByType<GuestManager>();

        if (manager != null)
            manager.GuestLeft(this);

        gameObject.SetActive(false);
    }
}

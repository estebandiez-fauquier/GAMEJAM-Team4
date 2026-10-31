
using UnityEngine;
using UnityEngine.AI;

public class GuestAI : MonoBehaviour
{
    [Header("Movement")]
    public float wanderRadius = 5f;
    public float wanderSpeed = 2f;
    public float fleeSpeed = 4f;

    [Header("Exit")]
    public Transform exitPoint;
    public float exitDistance = 1.5f;

    private NavMeshAgent agent;
    private bool frightened;
    private bool leaving;

    public bool IsLeaving => leaving;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("Guest needs a NavMeshAgent!");
            enabled = false;
            return;
        }

        agent.speed = wanderSpeed;
        ChooseNewDestination();
    }

    void Update()
    {
        if (frightened)
        {
            if (exitPoint == null)
                return;

            if (!leaving)
            {
                leaving = true;
                agent.speed = fleeSpeed;
                agent.SetDestination(exitPoint.position);
            }

            CheckExit();
            return;
        }

        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance + 0.2f)
        {
            ChooseNewDestination();
        }
    }

    void ChooseNewDestination()
    {
        Vector3 randomPosition =
            transform.position +
            Random.insideUnitSphere * wanderRadius;

        randomPosition.y = transform.position.y;

        if (NavMesh.SamplePosition(
            randomPosition,
            out NavMeshHit hit,
            wanderRadius,
            NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void CheckExit()
    {
        if (Vector3.Distance(transform.position, exitPoint.position)
            <= exitDistance)
        {
            ReachExit();
        }
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
        if (!leaving)
            return;

        Debug.Log(name + " has left the mansion!");

        if (agent != null)
            agent.isStopped = true;

        GameObject.FindGameObjectWithTag("Manager").GetComponent<GuestManager>().GuestLeft(this);

        gameObject.SetActive(false);
    }

    
}


//GameObject.FindGameObjectWithTag("Manager").GetComponent<GuestManager>().GuestLeft(this);
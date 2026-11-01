
using UnityEngine;
using UnityEngine.AI;

public class OwnerAI : MonoBehaviour
{
    [Header("Patrouille")]
    public Transform[] waypoints;
    public float reachDistance = 0.5f;

    [Header("Étourdissement")]
    public float stunDuration = 3f;

    private NavMeshAgent agent;
    private int currentWaypoint = 0;
    private float stunTimer = 0f;

    public bool IsStunned => stunTimer > 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("Owner : NavMeshAgent manquant !");
            return;
        }

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("Owner : aucun waypoint assigné !");
            return;
        }

        agent.SetDestination(waypoints[currentWaypoint].position);
    }

    void Update()
    {
        if (agent == null || waypoints == null || waypoints.Length == 0)
            return;

        // Gestion de l'étourdissement
        if (stunTimer > 0f)
        {
            stunTimer -= Time.deltaTime;
            agent.isStopped = true;
            return;
        }

        agent.isStopped = false;

        // Vérifie si le waypoint est atteint
        if (!agent.pathPending &&
            agent.remainingDistance <= reachDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;

            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    public void Stun(float duration)
    {
        stunTimer = Mathf.Max(stunTimer, duration);
        agent.isStopped = true;

        Debug.Log("Owner stunned !");
    }
}

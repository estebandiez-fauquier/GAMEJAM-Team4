
using UnityEngine;
using UnityEngine.AI;

public class DetectionSystem : MonoBehaviour
{
    [Header("Vision")]
    public float visionRange = 8f;

    [Range(0f, 360f)]
    public float visionAngle = 90f;

    public LayerMask ghostLayer;

    [Header("Detection")]
    public float catchCooldown = 2.5f;

    private float nextCatchTime;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Si le propriétaire est étourdi, il ne détecte pas le fantôme
        OwnerAI owner = GetComponent<OwnerAI>();

        if (owner != null && owner.IsStunned)
            return;

        DetectGhost();
    }

    void DetectGhost()
    {
        Collider[] targets = Physics.OverlapSphere(
            transform.position,
            visionRange,
            ghostLayer
        );

        foreach (Collider target in targets)
        {
            Vector3 origin = transform.position + Vector3.up;
            Vector3 destination = target.bounds.center;
            Vector3 direction = destination - origin;

            float distance = direction.magnitude;

            if (distance <= 0f || distance > visionRange)
                continue;

            // Vérification du champ de vision
            float angle = Vector3.Angle(
                transform.forward,
                direction
            );

            if (angle > visionAngle / 2f)
                continue;

            // Vérification des obstacles
            if (Physics.Raycast(
                origin,
                direction.normalized,
                out RaycastHit hit,
                distance,
                Physics.DefaultRaycastLayers,
                QueryTriggerInteraction.Ignore))
            {
                // Le premier objet touché doit être le fantôme
                if (hit.transform.root == target.transform.root)
                {
                    CatchGhost(target);
                    return;
                }
            }
        }
    }

    void CatchGhost(Collider target)
    {
        if (Time.time < nextCatchTime)
            return;

        GhostLives lives = target.GetComponentInParent<GhostLives>();
        GhostRespawn respawn = target.GetComponentInParent<GhostRespawn>();

        if (lives == null || respawn == null)
            return;

        nextCatchTime = Time.time + catchCooldown;

        lives.LoseLife();

        if (lives.CurrentLives > 0)
        {
            respawn.Respawn();
        }
    }

    // Affichage du rayon de vision dans l'éditeur
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Vector3 leftBoundary = Quaternion.Euler(
            0f, -visionAngle / 2f, 0f
        ) * transform.forward;

        Vector3 rightBoundary = Quaternion.Euler(
            0f, visionAngle / 2f, 0f
        ) * transform.forward;

        Gizmos.color = Color.red;

        Gizmos.DrawRay(
            transform.position,
            leftBoundary * visionRange
        );

        Gizmos.DrawRay(
            transform.position,
            rightBoundary * visionRange
        );
    }
}

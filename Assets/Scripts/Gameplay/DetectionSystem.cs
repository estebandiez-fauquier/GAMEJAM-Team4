
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    [Header("Vision")]
    public float visionRange = 8f;
    [Range(0f, 360f)]
    public float visionAngle = 90f;
    public LayerMask ghostLayer;
    public LayerMask obstacleLayer;

    private OwnerAI ownerAI;

    void Start()
    {
        ownerAI = GetComponent<OwnerAI>();
    }

    void Update()
    {
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
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude == 0f)
                continue;

            float angle = Vector3.Angle(transform.forward, direction);

            if (angle > visionAngle / 2f)
                continue;

            if (Physics.Raycast(
                transform.position + Vector3.up,
                direction.normalized,
                out RaycastHit hit,
                visionRange,
                obstacleLayer))
            {
                if (hit.collider.transform.root == target.transform.root)
                {
                    CatchGhost(target);
                }
            }
        }
    }

    void CatchGhost(Collider target)
    {
        GhostLives lives = target.GetComponent<GhostLives>();
        GhostRespawn respawn = target.GetComponent<GhostRespawn>();

        if (lives == null || respawn == null)
            return;

        lives.LoseLife();

        if (lives.CurrentLives > 0)
            respawn.Respawn();
    }
}

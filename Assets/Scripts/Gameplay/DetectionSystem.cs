
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    [Header("Vision")]
    public float visionRange = 8f;
    [Range(0f, 360f)]
    public float visionAngle = 90f;

    public LayerMask ghostLayer;

    private float nextCatchTime;

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
            Vector3 origin = transform.position + Vector3.up;
            Vector3 destination = target.bounds.center;
            Vector3 direction = destination - origin;

            float distance = direction.magnitude;

            if (distance <= 0f || distance > visionRange)
                continue;

            float angle = Vector3.Angle(
                transform.forward,
                direction
            );

            if (angle > visionAngle / 2f)
                continue;

            RaycastHit[] hits = Physics.RaycastAll(
                origin,
                direction.normalized,
                distance,
                Physics.DefaultRaycastLayers,
                QueryTriggerInteraction.Ignore
            );

            float nearestDistance = float.MaxValue;
            Transform firstObject = null;

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.root == transform.root)
                    continue;

                if (hit.distance < nearestDistance)
                {
                    nearestDistance = hit.distance;
                    firstObject = hit.transform.root;
                }
            }

            if (firstObject == target.transform.root)
            {
                CatchGhost(target);
                return;
            }
        }
    }

    void CatchGhost(Collider target)
    {
        if (Time.time < nextCatchTime)
            return;

        GhostLives lives = target.GetComponent<GhostLives>();
        GhostRespawn respawn = target.GetComponent<GhostRespawn>();

        if (lives == null || respawn == null)
            return;

        nextCatchTime = Time.time + 2.5f;

        lives.LoseLife();

        if (lives.CurrentLives > 0)
            respawn.Respawn();
    }
}


using UnityEngine;

public class GhostRespawn : MonoBehaviour
{
    public Transform spawnPoint;

    private Rigidbody rb;
    private GhostLives ghostLives;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ghostLives = GetComponent<GhostLives>();
    }

    public void Respawn()
    {
        if (ghostLives == null)
            return;

        if (ghostLives.CurrentLives <= 0)
            return;

        if (spawnPoint == null)
        {
            Debug.LogWarning("Spawn point is not assigned!");
            return;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = spawnPoint.position;
        }
        else
        {
            transform.position = spawnPoint.position;
        }
    }
}

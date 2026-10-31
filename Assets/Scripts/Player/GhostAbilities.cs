
using UnityEngine;

public class GhostAbilities : MonoBehaviour
{
    [Header("Energy")]
    public float maxEnergy = 100f;
    public float currentEnergy = 100f;
    public float regeneration = 2f;

    [Header("Poltergeist")]
    public float poltergeistCost = 20f;
    public float objectRange = 5f;
    public float throwForce = 8f;
    public LayerMask objectLayer;

    [Header("Flicker")]
    public float flickerCost = 15f;
    public float lightRange = 6f;

    [Header("Apparition")]
    public float apparitionCost = 30f;
    public float apparitionRange = 8f;

    void Update()
    {
        currentEnergy = Mathf.Min(
            maxEnergy,
            currentEnergy + regeneration * Time.deltaTime
        );

        if (Input.GetKeyDown(KeyCode.Space))
            Poltergeist();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            Flicker();

        if (Input.GetKeyDown(KeyCode.E))
            Apparition();
    }

    bool UseEnergy(float amount)
    {
        if (currentEnergy < amount)
        {
            Debug.Log("Not enough energy!");
            return false;
        }

        currentEnergy -= amount;
        return true;
    }

    // ABILITY 1: Throw nearby objects
    void Poltergeist()
    {
        if (!UseEnergy(poltergeistCost))
            return;

        Collider[] objects = Physics.OverlapSphere(
            transform.position,
            objectRange,
            objectLayer
        );

        foreach (Collider obj in objects)
        {
            Rigidbody objectRb = obj.GetComponent<Rigidbody>();

            if (objectRb != null)
            {
                Vector3 direction =
                    (obj.transform.position - transform.position).normalized;

                direction.y = 0.5f;

                objectRb.AddForce(
                    direction * throwForce,
                    ForceMode.Impulse
                );
            }
        }

        FrightenGuests(objectRange);

        Debug.Log("Poltergeist activated!");
    }

    // ABILITY 2: Toggle nearby lights
    void Flicker()
    {
        if (!UseEnergy(flickerCost))
            return;

        Light[] lights = FindObjectsOfType<Light>();

        foreach (Light light in lights)
        {
            float distance = Vector3.Distance(
                transform.position,
                light.transform.position
            );

            if (distance <= lightRange)
            {
                light.enabled = !light.enabled;
            }
        }

        FrightenGuests(lightRange);

        Debug.Log("Flicker activated!");
    }

    // ABILITY 3: Frighten all nearby guests instantly
    void Apparition()
    {
        if (!UseEnergy(apparitionCost))
            return;

        FrightenGuests(apparitionRange);

        Debug.Log("Apparition activated!");
    }

    void FrightenGuests(float range)
    {
        Collider[] targets = Physics.OverlapSphere(
            transform.position,
            range
        );

        foreach (Collider target in targets)
        {
            GuestAI guest = target.GetComponent<GuestAI>();

            if (guest != null)
                guest.Frighten();
        }
    }
}

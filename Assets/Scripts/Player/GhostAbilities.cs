
using UnityEngine;

public class GhostAbilities : MonoBehaviour
{
    [Header("Energy")]
    public float maxEnergy = 100f;
    public float currentEnergy = 100f;
    public float regeneration = 1f;

    [Header("Poltergeist")]
    public float poltergeistCost = 20f;
    public float objectRange = 5f;
    public float throwForce = 8f;
    public float timerPoltergeist = 5f;
    public LayerMask objectLayer;

    public float stunDuration = 3f;
    public float attractionSpeed = 12f;
    public float stunDistance = 1.5f;

    private OwnerAI owner;
    private bool poltergeistInProgress = false;

    [Header("Flicker")]
    public float flickerCost = 15f;
    public float lightRange = 6f;
    public float flickerNumber = 4f;
    public float flickerInterval = 1.5f;
    public float timerFlicker = 8f;

    [Header("Apparition")]
    public float apparitionCost = 30f;
    public float apparitionRange = 8f;
    public float timerApparition = 12f;

    // Cooldowns restants
    private float poltergeistCooldown = 0f;
    private float flickerCooldown = 0f;
    private float apparitionCooldown = 0f;

    // Accès pour l'UI
    public float PoltergeistCooldown => poltergeistCooldown;
    public float FlickerCooldown => flickerCooldown;
    public float ApparitionCooldown => apparitionCooldown;

    public float CurrentEnergy => currentEnergy;
    public float MaxEnergy => maxEnergy;


    private void Start()
    {
        owner = FindFirstObjectByType<OwnerAI>();
    }
    void Update()
    {
        // Régénération d'énergie
        currentEnergy = Mathf.Min(
            maxEnergy,
            currentEnergy + regeneration * Time.deltaTime
        );

        // Mise à jour des timers
        poltergeistCooldown = Mathf.Max(
            0f, poltergeistCooldown - Time.deltaTime
        );

        flickerCooldown = Mathf.Max(
            0f, flickerCooldown - Time.deltaTime
        );

        apparitionCooldown = Mathf.Max(
            0f, apparitionCooldown - Time.deltaTime
        );

        // Inputs
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

    // ABILITY 1: POLTERGEIST

    void Poltergeist()
    {
        if (poltergeistCooldown > 0f || poltergeistInProgress)
            return;

        if (owner == null)
        {
            Debug.LogWarning("Owner not found!");
            return;
        }

        Collider[] objects = Physics.OverlapSphere(
            transform.position,
            objectRange,
            objectLayer
        );

        Rigidbody nearestObject = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider obj in objects)
        {
            Rigidbody rb = obj.attachedRigidbody;

            if (rb == null || rb.isKinematic)
                continue;

            float distance = Vector3.Distance(
                transform.position,
                rb.position
            );

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObject = rb;
            }
        }

        if (nearestObject == null)
        {
            Debug.Log("No throwable object nearby!");
            return;
        }

        if (!UseEnergy(poltergeistCost))
            return;

        poltergeistCooldown = timerPoltergeist;

        StartCoroutine(
            SendObjectToOwner(nearestObject)
        );
    }

    System.Collections.IEnumerator SendObjectToOwner(Rigidbody objectRb)
    {
        poltergeistInProgress = true;

        ThrowableFear fear = objectRb.GetComponent<ThrowableFear>();

        if (fear != null)
            fear.ThrowByPlayer();

        // Temporarily take control of the object
        bool previousKinematic = objectRb.isKinematic;
        objectRb.linearVelocity = Vector3.zero;
        objectRb.isKinematic = true;

        while (objectRb != null && owner != null)
        {
            Vector3 target = owner.transform.position + Vector3.up;

            objectRb.transform.position = Vector3.MoveTowards(
                objectRb.transform.position,
                target,
                attractionSpeed * Time.deltaTime
            );

            float distance = Vector3.Distance(
                objectRb.transform.position,
                target
            );

            if (distance <= stunDistance)
            {
                owner.Stun(stunDuration);
                Debug.Log("Poltergeist stunned the Owner!");
                break;
            }

            yield return null;
        }

        if (objectRb != null)
        {
            objectRb.isKinematic = previousKinematic;
        }

        poltergeistInProgress = false;
    }



    // ABILITY 2: FLICKER
    void Flicker()
    {
        if (flickerCooldown > 0f)
        {
            Debug.Log("Flicker is on cooldown!");
            return;
        }

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
                if (flickerNumber > 0)
                {
                    if (light.enabled)
                    {
                        light.enabled = !light.enabled;
                    }
                    flickerNumber--;
                }
                flickerNumber = 4f;
                               
            }
        }

        FrightenGuests(lightRange);

        flickerCooldown = timerFlicker;
        flickerNumber = 4f;

        Debug.Log("Flicker activated!");
    }

    // ABILITY 3: APPARITION
    void Apparition()
    {
        if (apparitionCooldown > 0f)
        {
            Debug.Log("Apparition is on cooldown!");
            return;
        }

        if (!UseEnergy(apparitionCost))
            return;

        FrightenGuests(apparitionRange);

        apparitionCooldown = timerApparition;

        Debug.Log("Apparition activated!");
    }

    // FRIGHTEN GUESTS
    void FrightenGuests(float range)
    {
        Collider[] targets = Physics.OverlapSphere(
            transform.position,
            range
        );

        foreach (Collider target in targets)
        {
            GuestAI guest = target.GetComponentInParent<GuestAI>();

            if (guest != null)
                guest.Frighten();
        }
    }
}

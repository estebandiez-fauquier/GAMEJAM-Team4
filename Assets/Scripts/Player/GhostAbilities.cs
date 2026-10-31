
using UnityEngine;

public class GhostAbilities : MonoBehaviour
{
    [Header("Energy")]
    public float maxEnergy = 100f;
    public float currentEnergy = 100f;
    public float regeneration = 5f;

    [Header("Ability Costs")]
    public float poltergeistCost = 20f;
    public float flickerCost = 15f;
    public float apparitionCost = 30f;

    [Header("Detection")]
    public float abilityRange = 5f;
    public LayerMask guestLayer;

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
    }

    public bool UseEnergy(float amount)
    {
        if (currentEnergy < amount)
        {
            Debug.Log("Not enough energy!");
            return false;
        }

        currentEnergy -= amount;
        return true;
    }

    void Poltergeist()
    {
        if (!UseEnergy(poltergeistCost))
            return;

        Collider[] targets = Physics.OverlapSphere(
            transform.position,
            abilityRange,
            guestLayer
        );

        foreach (Collider target in targets)
        {
            GuestAI guest = target.GetComponent<GuestAI>();

            if (guest != null)
                guest.Frighten();
        }

        Debug.Log("Poltergeist activated!");
    }

    void Flicker()
    {
        if (!UseEnergy(flickerCost))
            return;

        Collider[] targets = Physics.OverlapSphere(
            transform.position,
            abilityRange,
            guestLayer
        );

        foreach (Collider target in targets)
        {
            GuestAI guest = target.GetComponent<GuestAI>();

            if (guest != null)
                guest.Frighten();
        }

        Debug.Log("Flicker activated!");
    }

    public void Apparition()
    {
        if (!UseEnergy(apparitionCost))
            return;

        Collider[] targets = Physics.OverlapSphere(
            transform.position,
            abilityRange,
            guestLayer
        );

        foreach (Collider target in targets)
        {
            GuestAI guest = target.GetComponent<GuestAI>();

            if (guest != null)
                guest.Frighten();
        }

        Debug.Log("Apparition activated!");
    }
}

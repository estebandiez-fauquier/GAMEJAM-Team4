using UnityEngine;
using System.Collections.Generic;

public class ThrowableFear : MonoBehaviour
{
    private HashSet<GuestAI> frightenedGuests = new HashSet<GuestAI>();

    private bool thrownByPlayer = false;

    public void ThrowByPlayer()
    {
        thrownByPlayer = true;
        frightenedGuests.Clear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!thrownByPlayer)
            return;

        GuestAI guest = collision.collider.GetComponentInParent<GuestAI>();

        if (guest == null)
            return;

        if (frightenedGuests.Contains(guest))
            return;

        Repulse(guest);
    }

    private void Repulse(GuestAI guest)
    {
        frightenedGuests.Add(guest);
        guest.Frighten();

        Debug.Log("A guest was frightened by " + gameObject.name);
    }
}
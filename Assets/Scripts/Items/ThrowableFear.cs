using UnityEngine;
using System.Collections.Generic;

public class ThrowableFear : MonoBehaviour
{

    public bool thrown = false;
    public float timerThrown = 240;
    private HashSet<GuestAI> frightenedGuests = new HashSet<GuestAI>();

    private void Update()
    {
        if (thrown)
        {
            while(timerThrown>0)
            {
                timerThrown--;
            }
            thrown = false;
            timerThrown = 240;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (thrown)
        {
            GuestAI guest = collision.collider.GetComponentInParent<GuestAI>();
            if (guest == null)
                return;
            if (frightenedGuests.Contains(guest))
                return;
            frightenedGuests.Add(guest);
            guest.Frighten();

            Debug.Log("A guest was frightened by " + gameObject.name);
        }
    }
}
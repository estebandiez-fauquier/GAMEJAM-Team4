
using UnityEngine;

public class GuestExit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GuestAI guest = other.GetComponent<GuestAI>();

        if (guest != null)
            guest.ReachExit();
    }
}

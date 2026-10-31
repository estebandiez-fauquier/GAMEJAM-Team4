using UnityEngine;

public class GuestManager : MonoBehaviour
{
    public GuestAI[] guests;

    private int guestsRemaining;

    void Start()
    {
        GameObject[] guestObjects = GameObject.FindGameObjectsWithTag("Guest");

        guests = new GuestAI[guestObjects.Length];

        for (int i = 0; i < guestObjects.Length; i++)
        {
            guests[i] = guestObjects[i].GetComponent<GuestAI>();
        }

        guestsRemaining = guests.Length;

        Debug.Log("Guests found: " + guestsRemaining);
    }

    public void GuestLeft(GuestAI guest)
    {
        guestsRemaining--;

        Debug.Log("Guest left! Remaining: " + guestsRemaining);

        if (guestsRemaining <= 0)
        {
            GameManager.Instance.Victory();
        }
    }

    public int GuestsRemaining
    {
        get { return guestsRemaining; }
    }
}
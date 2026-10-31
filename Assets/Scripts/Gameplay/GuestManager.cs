
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    [Header("Guests")]
    public GuestAI[] guests;

    private int guestsRemaining;

    public int GuestsRemaining => guestsRemaining;

    void Start()
    {
        guestsRemaining = guests.Length;
        Debug.Log("Guests remaining: " + guestsRemaining);
    }

    public void GuestLeft(GuestAI guest)
    {
        guestsRemaining--;

        Debug.Log("Guests remaining: " + guestsRemaining);

        if (guestsRemaining <= 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        Debug.Log("All guests have escaped! VICTORY!");

        if (GameManager.Instance != null)
            GameManager.Instance.Victory();
    }
}

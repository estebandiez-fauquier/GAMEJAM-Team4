
using UnityEngine;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public GuestManager guestManager;
    public TMP_Text objectiveText;

    void Update()
    {
        if (guestManager == null || objectiveText == null)
            return;

        objectiveText.text =
            "Guests remaining: " + guestManager.GuestsRemaining;
    }
}

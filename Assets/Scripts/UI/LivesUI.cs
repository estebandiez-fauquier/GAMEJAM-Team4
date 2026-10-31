
using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public GhostLives ghostLives;
    public TMP_Text livesText;

    void Update()
    {
        if (ghostLives == null || livesText == null)
            return;

        livesText.text = "Lives: " + ghostLives.CurrentLives;
    }
}

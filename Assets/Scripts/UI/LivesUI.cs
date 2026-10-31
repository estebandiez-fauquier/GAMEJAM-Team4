using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public GhostLives ghostLives;
    public TMP_Text livesText;

    public string fullHeart = "♥";
    public string emptyHeart = "♡";

    void Update()
    {
        Debug.Log("Current lives: " + ghostLives.CurrentLives);
        if (ghostLives == null || livesText == null)
            return;

        int currentLives = ghostLives.CurrentLives;
        int maxLives = ghostLives.maxLives;

        string hearts = "";

        for (int i = 0; i < maxLives; i++)
        {
            if (i < currentLives)
                hearts += fullHeart + " ";
            else
                hearts += emptyHeart + " ";
        }

        livesText.text = hearts;
    }
}

using UnityEngine;
using TMPro;

public class GameEndUI : MonoBehaviour
{
    public GameObject gameEndPanel;
    public TMP_Text gameEndTitle;
    public TMP_Text gameEndText;

    private bool displayed;

    void Start()
    {
        gameEndPanel.SetActive(false);
    }

    void Update()
    {
        if (displayed || GameManager.Instance == null)
            return;

        if (GameManager.Instance.victory)
        {
            ShowEndScreen(
                "THE MANSION IS SILENT",
                "All guests have escaped. You win!"
            );
        }
        else if (GameManager.Instance.gameOver)
        {
            ShowEndScreen(
                "THE GHOST HAS BEEN CAUGHT",
                "You have lost all your lives."
            );
        }
    }

    void ShowEndScreen(string title, string message)
    {
        displayed = true;

        gameEndPanel.SetActive(true);
        gameEndTitle.text = title;
        gameEndText.text = message;
    }
}

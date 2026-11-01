
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
            Invoke("backToMenu", 5f);
        }
        else if (GameManager.Instance.gameOver)
        {
            ShowEndScreen(
                "THE GHOST HAS BEEN CAUGHT",
                "You have lost all your lives."
            );
            Invoke("backToMenu", 5f);
        }
    }

    void ShowEndScreen(string title, string message)
    {
        displayed = true;

        gameEndPanel.SetActive(true);
        gameEndTitle.text = title;
        gameEndText.text = message;
    }

    void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

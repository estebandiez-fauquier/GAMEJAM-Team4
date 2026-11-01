using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string gameSceneName = "Game";

    [Header("How to play")]
    public GameObject howToPlayPanel;   // le panneau (Image) à afficher avec More
    public GameObject mainButtons;      // optionnel : le groupe des 4 boutons à masquer

    void Start()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }

    void Update()
    {
        if (howToPlayPanel != null && howToPlayPanel.activeSelf &&
            Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMore();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenMore()
    {
        if (howToPlayPanel.activeSelf)
        {
            if (howToPlayPanel != null) howToPlayPanel.SetActive(false);
            if (mainButtons != null) mainButtons.SetActive(true);
        }
        else
        {
            if (howToPlayPanel != null) howToPlayPanel.SetActive(true);
            if (mainButtons != null) mainButtons.SetActive(false);
        }

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}
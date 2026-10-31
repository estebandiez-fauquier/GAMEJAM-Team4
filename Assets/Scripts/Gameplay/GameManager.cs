
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gameOver = false;
    public bool victory = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        Debug.Log("GAME OVER");
    }

    public void Victory()
    {
        victory = true;
        Debug.Log("VICTORY!");
    }
}

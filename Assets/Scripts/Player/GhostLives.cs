
using UnityEngine;

public class GhostLives : MonoBehaviour
{
    public int maxLives = 3;

    private int currentLives;

    public int CurrentLives => currentLives;

    void Start()
    {
        currentLives = maxLives;
    }

    public void LoseLife()
    {
        currentLives--;


        if (currentLives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Game Over!");
        gameObject.SetActive(false);
    }
}

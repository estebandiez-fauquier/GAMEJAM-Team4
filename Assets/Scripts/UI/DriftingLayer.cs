
using UnityEngine;

public class DriftingLayer : MonoBehaviour
{
    public float speed = 15f;
    public float resetPosition = 1920f;
    public float startPosition = 0f;

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 position = rect.anchoredPosition;

        position.x -= speed * Time.deltaTime;

        if (position.x <= -resetPosition)
            position.x = startPosition;

        rect.anchoredPosition = position;
    }
}

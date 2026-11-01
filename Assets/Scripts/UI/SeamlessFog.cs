
using UnityEngine;

public class SeamlessFog : MonoBehaviour
{
    public float speed = 20f;
    public float imageWidth = 1920f;

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 position = rect.anchoredPosition;
        position.x -= speed * Time.deltaTime;

        // Move this image behind the other one.
        if (position.x <= -imageWidth)
        {
            position.x += imageWidth * 2f;
        }

        rect.anchoredPosition = position;
    }
}

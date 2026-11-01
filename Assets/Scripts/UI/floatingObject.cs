
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float amplitude = 20f;
    public float frequency = 1f;

    private RectTransform rect;
    private Vector2 startPosition;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        startPosition = rect.anchoredPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(
            Time.time * frequency
        ) * amplitude;

        rect.anchoredPosition =
            startPosition + Vector2.up * offset;
    }
}

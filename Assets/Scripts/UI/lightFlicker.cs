
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WindowFlicker : MonoBehaviour
{
    public Image windowImage;

    public float minIntensity = 0.15f;
    public float maxIntensity = 0.8f;

    public float minDelay = 1f;
    public float maxDelay = 4f;

    void Start()
    {
        if (windowImage == null)
            windowImage = GetComponent<Image>();

        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(minDelay, maxDelay)
            );

            Color color = windowImage.color;

            color.a = Random.Range(
                minIntensity,
                maxIntensity
            );

            windowImage.color = color;
        }
    }
}

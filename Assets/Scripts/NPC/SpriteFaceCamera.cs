using UnityEngine;

public class SpriteFaceCamera : MonoBehaviour
{
    private Transform cam;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        cam = Camera.main.transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (cam == null || spriteRenderer == null)
            return;

        transform.forward = cam.forward;
    }
}
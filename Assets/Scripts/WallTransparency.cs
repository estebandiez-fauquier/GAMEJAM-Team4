
using UnityEngine;
using System.Collections.Generic;

public class WallTransparency : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Camera mainCamera;

    [Header("Settings")]
    public LayerMask wallLayer;
    [Range(0.05f, 1f)]
    public float transparentAlpha = 0.25f;

    private Dictionary<Renderer, Material[]> originalMaterials =
        new Dictionary<Renderer, Material[]>();

    private HashSet<Renderer> currentWalls =
        new HashSet<Renderer>();

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (player == null)
            Debug.LogError("WallTransparency: Player is missing!");

        if (mainCamera == null)
            Debug.LogError("WallTransparency: Main Camera is missing!");
    }

    void LateUpdate()
    {
        if (player == null || mainCamera == null)
            return;

        HashSet<Renderer> detectedWalls = new HashSet<Renderer>();

        Vector3 target = player.position + Vector3.up * 1f;

        Vector3 direction = target - mainCamera.transform.position;
        float distance = direction.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(
            mainCamera.transform.position,
            direction.normalized,
            distance,
            wallLayer,
            QueryTriggerInteraction.Ignore
        );

        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();

            if (rend == null)
                rend = hit.collider.GetComponentInChildren<Renderer>();

            if (rend == null)
                rend = hit.collider.GetComponentInParent<Renderer>();

            if (rend != null)
                detectedWalls.Add(rend);
        }

        // Restore walls no longer blocking the camera
        foreach (Renderer rend in currentWalls)
        {
            if (!detectedWalls.Contains(rend))
                RestoreWall(rend);
        }

        // Make newly detected walls transparent
        foreach (Renderer rend in detectedWalls)
        {
            if (!currentWalls.Contains(rend))
                MakeTransparent(rend);
        }

        currentWalls = detectedWalls;
    }

    void MakeTransparent(Renderer rend)
    {
        if (rend == null)
            return;

        // Keep original material instances and alpha values
        if (!originalMaterials.ContainsKey(rend))
        {
            Material[] materials = rend.materials;
            originalMaterials.Add(rend, materials);

            foreach (Material mat in materials)
            {
                if (mat.HasProperty("_Color"))
                {
                    Color color = mat.color;
                    color.a = transparentAlpha;
                    mat.color = color;
                }

                if (mat.HasProperty("_BaseColor"))
                {
                    Color color = mat.GetColor("_BaseColor");
                    color.a = transparentAlpha;
                    mat.SetColor("_BaseColor", color);
                }
            }
        }
    }

    void RestoreWall(Renderer rend)
    {
        if (rend == null || !originalMaterials.ContainsKey(rend))
            return;

        Material[] materials = originalMaterials[rend];

        foreach (Material mat in materials)
        {
            if (mat == null)
                continue;

            if (mat.HasProperty("_Color"))
            {
                Color color = mat.color;
                color.a = 1f;
                mat.color = color;
            }

            if (mat.HasProperty("_BaseColor"))
            {
                Color color = mat.GetColor("_BaseColor");
                color.a = 1f;
                mat.SetColor("_BaseColor", color);
            }
        }

        originalMaterials.Remove(rend);
    }

    void OnDisable()
    {
        foreach (Renderer rend in originalMaterials.Keys)
        {
            RestoreWall(rend);
        }

        currentWalls.Clear();
    }
}

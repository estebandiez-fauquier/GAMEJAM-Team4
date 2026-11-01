
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class OwnerVision : MonoBehaviour
{
    [Header("Vision Settings")]
    public float visionRange = 8f;
    [Range(0f, 360f)]
    public float visionAngle = 90f;

    [Header("Appearance")]
    public Color normalColor = new Color(1f, 0f, 0f, 0.2f);
    public Color alertColor = new Color(1f, 0f, 0f, 0.4f);
    public float heightOffset = 0.05f;
    public int segments = 40;

    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private OwnerAI ownerAI;
    private DetectionSystem detection;

    void Start()
    {
        mesh = new Mesh();
        mesh.name = "Owner Vision Mesh";

        GetComponent<MeshFilter>().mesh = mesh;
        meshRenderer = GetComponent<MeshRenderer>();

        ownerAI = GetComponentInParent<OwnerAI>();
        detection = GetComponentInParent<DetectionSystem>();

        GenerateVisionMesh();
    }

    void Update()
    {
        if (detection != null)
        {
            visionRange = detection.visionRange;
            visionAngle = detection.visionAngle;
        }

        if (meshRenderer != null)
        {
            meshRenderer.material.color =
                ownerAI != null && ownerAI.IsStunned
                ? normalColor
                : alertColor;
        }

        GenerateVisionMesh();
    }

    void GenerateVisionMesh()
    {
        if (mesh == null)
            return;

        mesh.Clear();

        int vertexCount = segments + 2;

        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[segments * 3];

        vertices[0] = new Vector3(0f, heightOffset, 0f);

        for (int i = 0; i <= segments; i++)
        {
            float angle = -visionAngle / 2f +
                          (visionAngle / segments) * i;

            float radians = angle * Mathf.Deg2Rad;

            vertices[i + 1] = new Vector3(
                Mathf.Sin(radians) * visionRange,
                heightOffset,
                Mathf.Cos(radians) * visionRange
            );
        }

        for (int i = 0; i < segments; i++)
        {
            int index = i * 3;

            triangles[index] = 0;
            triangles[index + 1] = i + 1;
            triangles[index + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}

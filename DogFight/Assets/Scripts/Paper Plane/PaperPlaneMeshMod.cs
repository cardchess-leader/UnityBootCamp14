using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PaperPlaneMeshMod : MonoBehaviour
{
    private SkinnedMeshRenderer meshFilter;
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] modifiedVertices;
    private Vector2[] noiseSeeds;

    [Header("Flutter Settings")]
    public float radius = 0.7f;
    public float flutterIntensity = 0.05846636f;
    public float flutterSpeed = 14.23318f;
    public float maxFlutterSpeed = 5f;
    public float maxFlutterIntensity = 0.06f;
    public float inputFlutterMultiplier = 0.005f;

    private float initialFlutterSpeed;
    private float initialFlutterIntensity;

    private PaperPlanePhysics paperPlanePhysics;
    private Rigidbody rb;

    void Start()
    {
        paperPlanePhysics = GetComponentInParent<PaperPlanePhysics>();
        rb = paperPlanePhysics.Rigidbody;

        meshFilter = GetComponent<SkinnedMeshRenderer>();
        if (meshFilter == null)
        {
            enabled = false;
            return;
        }

        mesh = meshFilter.sharedMesh;
        mesh.MarkDynamic();

        originalVertices = mesh.vertices;
        modifiedVertices = new Vector3[originalVertices.Length];
        Array.Copy(originalVertices, modifiedVertices, originalVertices.Length);

        noiseSeeds = new Vector2[originalVertices.Length];
        for (int i = 0; i < noiseSeeds.Length; i++)
        {
            noiseSeeds[i] = new Vector2(Random.Range(0f, 1000f), Random.Range(0f, 1000f));
        }

        initialFlutterSpeed = flutterSpeed;
        initialFlutterIntensity = flutterIntensity;
    }

    void LateUpdate()
    {
        AdjustFlutterIntensity();
        ApplyFlutterEffect();
    }

    void AdjustFlutterIntensity()
    {
        float speedFactor = Mathf.InverseLerp(0, paperPlanePhysics.MaxSpeed, rb.linearVelocity.magnitude);
        float inputFactor = (Mathf.Abs(paperPlanePhysics.YawInput) + Mathf.Abs(paperPlanePhysics.PitchInput)) *
                            inputFlutterMultiplier;

        flutterSpeed = Mathf.Lerp(initialFlutterSpeed, maxFlutterSpeed, speedFactor + inputFactor);
        flutterIntensity = Mathf.Lerp(initialFlutterIntensity, maxFlutterIntensity, speedFactor + inputFactor);
    }

    void ApplyFlutterEffect()
    {
        float timeFactor = Time.time;
        Vector3 velocity = rb.linearVelocity;

        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];

            bool isEdge = Mathf.Abs(vertex.x) > radius || Mathf.Abs(vertex.z) > radius;

            if (isEdge)
            {
                float noise = Mathf.PerlinNoise(noiseSeeds[i].x, noiseSeeds[i].y + timeFactor * flutterSpeed);
                float aerodynamicFactor =
                    Mathf.Abs(Vector3.Dot(velocity.normalized, transform.up));

                vertex.y += Mathf.Sin(Time.time * flutterSpeed + vertex.x + noise) * flutterIntensity *
                            aerodynamicFactor;
                vertex.z += Mathf.Cos(Time.time * flutterSpeed + vertex.y + noise) * flutterIntensity *
                            aerodynamicFactor;
                //
                // vertex.x += Mathf.Sin(Time.time * flutterSpeed + vertex.z + noise) * flutterIntensity *
                //             aerodynamicFactor;
            }

            modifiedVertices[i] = vertex;
        }

        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals();
    }

    private void OnDisable()
    {
        mesh.vertices = originalVertices;
        mesh.RecalculateNormals();
    }
}

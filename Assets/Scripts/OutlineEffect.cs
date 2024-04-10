using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    public Color outlineColor = Color.white; // Outline color
    public float outlineWidth = 0.05f; // Outline width

    private Renderer objectRenderer; // Reference to the Renderer component
    private Material outlineMaterial; // Material for the outline effect

    void Start()
    {
        // Get the Renderer component attached to this object
        objectRenderer = GetComponent<Renderer>();

        // Create a new material for the outline effect
        outlineMaterial = new Material(Shader.Find("Standard"));

        // Set the outline material properties
        outlineMaterial.SetFloat("_OutlineWidth", outlineWidth);
        outlineMaterial.SetColor("_OutlineColor", outlineColor);

        // Assign the outline material to the object
        objectRenderer.material = outlineMaterial;
        Debug.Log("Highlight");
    }

    void OnDestroy()
    {
        // Destroy the outline material when the object is destroyed
        if (outlineMaterial != null)
        {
            Destroy(outlineMaterial);
        }
    }
}
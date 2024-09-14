using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DrunkEffect : MonoBehaviour
{
    public Material drunkEffectMaterial;  // The custom shader material
    public float maxDistortion = 0.5f;    // Maximum distortion value
    public float transitionSpeed = 1.0f;  // Speed of transitioning into drunkenness
    private float currentDistortion = 0.0f;  // Current distortion level
    private bool isDrunk = true;  // Start in a "drunk" state

    // Reference to the camera's post-processing stack (URP)
    private RenderTexture renderTexture;

    void Start()
    {
        // Create a temporary render texture for the effect
        renderTexture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.Default);
        renderTexture.Create();

        // Initialize distortion to maximum value
        currentDistortion = maxDistortion;
    }

    void Update()
    {
        // Keep the distortion at maxDistortion to have the drunk effect constant
        if (isDrunk)
        {
            currentDistortion = Mathf.Lerp(currentDistortion, maxDistortion, Time.deltaTime * transitionSpeed);
        }

        // Update the distortion amount in the material
        drunkEffectMaterial.SetFloat("_DistortionAmount", currentDistortion);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (drunkEffectMaterial != null)
        {
            // Apply the drunk effect shader material on the camera's render texture
            Graphics.Blit(source, destination, drunkEffectMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    void OnDestroy()
    {
        if (renderTexture != null)
        {
            renderTexture.Release();
        }
    }
}

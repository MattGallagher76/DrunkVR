using UnityEngine;

[ExecuteInEditMode]
public class DrunkWaveEffect : MonoBehaviour
{
    public Material drunkEffectMaterial; // Reference to the material using the custom shader
    public float waveX = 10.0f;
    public float waveY = 10.0f;
    public float speed = 1.0f;

    void Start()
    {
        if (drunkEffectMaterial == null)
        {
            Debug.LogError("Please assign a material with the DrunkWaveShader");
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (drunkEffectMaterial != null)
        {
            // Pass the variables to the shader
            drunkEffectMaterial.SetFloat("_WaveX", waveX);
            drunkEffectMaterial.SetFloat("_WaveY", waveY);
            drunkEffectMaterial.SetFloat("_Speed", speed);

            // Apply the material (shader) to the source texture and render it to the destination texture
            Graphics.Blit(source, destination, drunkEffectMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}

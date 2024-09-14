using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrunkEffectCanvasController : MonoBehaviour
{
    public Image distortionImage;  // Reference to the UI image
    public float wobbleSpeed = 1.0f; // Speed of the wobble effect
    public float wobbleAmount = 10.0f; // Magnitude of the wobble effect
    public float maxBlurAmount = 1.5f;  // Optional: max amount of distortion or blur
    private RectTransform rectTransform;

    void Start()
    {
        // Get the RectTransform of the image to manipulate its position/scale
        rectTransform = distortionImage.GetComponent<RectTransform>();
    }

    void Update()
    {
       
    }

    // Apply a wobble effect to simulate drunkenness
    void ApplyWobbleEffect()
    {
        // Calculate the wobble amount using sine and cosine waves for smooth movement
        float wobbleX = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
        float wobbleY = Mathf.Cos(Time.time * wobbleSpeed) * wobbleAmount;

        // Apply the wobble effect to the RectTransform's anchored position
        rectTransform.anchoredPosition = new Vector2(wobbleX, wobbleY);

        // Optional: You can also apply scale distortion for extra drunk effect
        float scale = 1.0f + Mathf.Sin(Time.time * wobbleSpeed * 0.5f) * 0.1f;
        rectTransform.localScale = new Vector3(scale, scale, 1.0f);
    }

    // Optional: If you want to fade out or intensify the effect, you can also adjust opacity
    public void SetEffectOpacity(float opacity)
    {
        Color currentColor = distortionImage.color;
        currentColor.a = Mathf.Clamp01(opacity);
        distortionImage.color = currentColor;
    }
}

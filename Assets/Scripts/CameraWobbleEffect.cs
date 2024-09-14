using UnityEngine;

public class CameraWobbleEffect : MonoBehaviour
{
    public float wobbleSpeed = 1.0f;  // Speed of the wobble effect
    public float wobbleAmount = 0.02f;  // Magnitude of the wobble (for position)
    public float rotationWobbleAmount = 2.0f;  // Magnitude of the wobble (for rotation)

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        // Store the original position and rotation of the camera
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        ApplyWobbleEffect();  // Continuously apply the wobble effect
    }

    // Apply a wobble effect to simulate drunkenness
    void ApplyWobbleEffect()
    {
        // Wobble the position using sine and cosine for smooth movement
        float wobbleX = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
        float wobbleY = Mathf.Cos(Time.time * wobbleSpeed) * wobbleAmount;

        // Apply the wobble to the camera's position
        transform.localPosition = originalPosition + new Vector3(wobbleX, wobbleY, 0);

        // Optionally, apply a small rotational wobble
        float wobbleRotX = Mathf.Sin(Time.time * wobbleSpeed * 0.5f) * rotationWobbleAmount;
        float wobbleRotY = Mathf.Cos(Time.time * wobbleSpeed * 0.5f) * rotationWobbleAmount;

        // Apply rotational wobble (only small rotation for discomfort prevention)
        transform.localRotation = originalRotation * Quaternion.Euler(wobbleRotX, wobbleRotY, 0);
    }
}

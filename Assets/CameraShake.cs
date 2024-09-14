using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float intensity = 1f;  // The strength of the drunk effect
    public float zIntensity = 1f;  // The strength of the drunk effect
    public float xIntensity = 1f;  // The strength of the drunk effect
    public float speed = 1f;      // Speed of the noise effect

    private float xOffset;
    private float zOffset;

    void Start()
    {
        // Initialize offsets with random values to make the effect look more natural
        xOffset = Random.Range(0f, 100f);
        zOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Calculate Perlin noise based rotation for X and Z axes
        // Remap the Perlin noise (0 to 1) to (-1 to 1)
        float xRotation = (Mathf.PerlinNoise(Time.time * speed, xOffset) * 2f - 1f) * zIntensity * intensity;
        float zRotation = (Mathf.PerlinNoise(Time.time * speed, zOffset) * 2f - 1f) * xIntensity * intensity;

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0, zRotation);
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public Image fadeImage;           // Reference to the black image
    public float fadeDuration = 8.0f; // Duration for the fade to complete

    private bool isFading = false;

    // Call this function to start the fade-to-black sequence
    public void StartFadeToBlack()
    {
        if (!isFading)
        {
            StartCoroutine(FadeOut());
        }
    }

    // Coroutine that fades the screen to black
    IEnumerator FadeOut()
    {
        isFading = true;
        Color imageColor = fadeImage.color;
        float elapsedTime = 0f;

        // Ensure the image starts as fully transparent
        imageColor.a = 0f;
        fadeImage.color = imageColor;

        // Gradually increase the alpha value over the fade duration
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(elapsedTime / fadeDuration); // Fade to full opacity (alpha = 1)
            fadeImage.color = imageColor;
            yield return null;
        }

        // Ensure the image is fully opaque at the end of the fade
        imageColor.a = 1f;
        fadeImage.color = imageColor;
    }
}

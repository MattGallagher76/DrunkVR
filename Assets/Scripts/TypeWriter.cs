using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Make sure you import this for UI Text

public class TypewriterEffect : MonoBehaviour
{
    public Text uiText;  // Reference to the UI Text component
    public string fullText;  // The full text to display
    public float delay = 0.05f;  // Delay between each letter

    private string currentText = "";  // Tracks the current displayed text

    void Start()
    {
        // Get the text that was initially set in the Text component
        fullText = uiText.text;
        StartCoroutine(ShowText());  // Start the typewriter effect
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);  // Get text up to the current character
            uiText.text = currentText;  // Update the UI Text component
            yield return new WaitForSeconds(delay);  // Wait before showing the next character
        }
    }
}

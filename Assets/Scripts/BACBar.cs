using UnityEngine;
using UnityEngine.UI;

public class BACBar : MonoBehaviour
{
    public Image bacBarFill;  // Assign the fill image in the Inspector
    public float maxBAC = 0.25f;  // Maximum BAC
    public float currentBAC;  // Current BAC of the player

    void Start()
    {
        //currentBAC = 0f;  // Start with BAC at 0
        UpdateBACBar();
    }

    // Method to update the BAC and refresh the bar
    public void UpdateBAC(float newBAC)
    {
        currentBAC = newBAC;  // Update current BAC
        currentBAC = Mathf.Clamp(currentBAC, 0, maxBAC);  // Ensure BAC stays between 0 and maxBAC
        UpdateBACBar();  // Update the UI
    }

    // Method to update the fill amount based on current BAC
    void UpdateBACBar()
    {
        float fillAmount = currentBAC / maxBAC;  // Calculate the percentage of BAC
        bacBarFill.fillAmount = fillAmount;  // Update the fill amount (range from 0 to 1)
        Debug.Log("Updating BACBar fillAmount: " + fillAmount);
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BACScript : MonoBehaviour
{
    public float bac;

    public float passOutThreshold = 0.25f;

    //Did you eat?
    public bool ate = false;

    public bool isBlackout = false;

    private CameraShake[] cameraShakeScripts;

    private BACBar bacBar;




    // Start is called before the first frame update
    void Start()
    {
        bac = 0.0f;

        bacBar = FindObjectOfType<BACBar>();

        cameraShakeScripts = FindObjectsOfType<CameraShake>();


        if (bacBar != null)
        {
            bacBar.UpdateBAC(bac);  // Initialize the bar to 0
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isBlackout)
        {
            checkBAC();
            UpdateCameraShakeIntensity();
            bacBar.UpdateBAC(bac);
        }
        
    }

    //Getter method for BAC
    public float getBAC()
    {
        return bac;
    }

    //Method to check BAC
    void checkBAC()
    {
        if (bac >= passOutThreshold)
        {
          //  IniciateBlackout();
            
        }
    }

    //Black out sequence
   /* void IniciateBlackout()
    {
        Debug.Log("Blackout");
        FindObjectOfType<FadeToBlack>().StartFadeToBlack();
        isBlackout = true;
    }*/

    public void updateBAC(float points)
    {
        if (!ate)
        {
            points = points * 1.5f;
           
        }

        bac = bac + points;
        
        Debug.Log("BAC is: " + bac);

        if (bacBar != null)
        {
            bacBar.UpdateBAC(bac);
        }



    }

    void UpdateCameraShakeIntensity()
    {
        // Loop through all CameraShake scripts in the scene and update their intensity
        foreach (CameraShake shake in cameraShakeScripts)
        {
            if (shake != null)
            {
                shake.intensity = bac * 20;
            }
        }
    }




}

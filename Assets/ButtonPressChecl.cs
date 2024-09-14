using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressChecl : MonoBehaviour
{
    public GameObject gb;
    public bool c;

    // Update is called once per frame
    void Update()
    {
        // Check if the A button on the right controller is pressed
        if (c || OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            Debug.Log("A button pressed on right controller");
            gb.GetComponent<Renderer>().material.color = GetRandomColor();
        }
    }

    Color GetRandomColor()
    {
        // Generate random values for red, green, and blue
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        // Create and return the new color
        return new Color(r, g, b);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGrabItem : MonoBehaviour
{

    public int id;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Hand"))
        {
            FindObjectOfType<GameStateManager>().itemGrabbed(id);
        }
        else
        {
            Debug.Log("Something else - " + other.tag);
        }
        Debug.Log("Kill me please");
    }
}

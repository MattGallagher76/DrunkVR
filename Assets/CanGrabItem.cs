using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGrabItem : MonoBehaviour
{
    public int id;

    public float dur = 3f;

    float timer = 0f;

    bool canHit = true;

    public void Update()
    {
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                timer = 0f;
                canHit = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Hand"))
        {
            if(canHit)
                FindObjectOfType<GameStateManager>().itemGrabbed(id);
        }
        else
        {
            Debug.Log("Something else - " + other.tag);
        }
        Debug.Log("Kill me please");
    }
}

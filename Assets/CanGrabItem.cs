using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGrabItem : MonoBehaviour
{
    private string[] texts = {
        "Sup bruh, want some?",                        // 0 - Asking about food
        "Your loss bro",                               // 1 - No food
        "Chuu cheesy goodness",                        // 2 - Yes food
        "Yo let's go to Chad's and pregame",           // 3 - Pregame offer
        "Alright, I'll catch you at the party then",   // 4 - No to pregame
        "You chose liquor.",                           // 5
        "Alrighty, one last brewski for you",          // 6 - Beer at pregame
        "Hey man where the drinks at? " +
            "Let's get you a drink man",               // 7 - Party intro
        "(Peer pressure)",                             // 8 - Peer Pressure
        "Alright bro, should we drive or take an uber",// 9 - Drive question
        "Alright man lets check out the F-150",        // 10 - Drive
        "Alright bro, I'll catch you later",           // 11 - uber
        "Oh yea my man!",                              // 12 - Liquour at party
        "Niceeeee",                                    // 13 - Beer at party
        "Oh yea my man!",                              // 14 - Liquour at pregame
        "Alright, more for me then",                   // 15 - No to drink at party
        "Uhh I got a drink just for you...",           // 16 - Creepy dude offer
        "",                                            // 17 - Creepy yes 
        "Oh, ok, not like I care",                     // 18 - Creepy no
        "What - lame. Come on, let's go guys",         // 19 - Peer pressure no
        "Yeaaah let's go my man!",                     // 20 - Peer pressure yes
    };

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

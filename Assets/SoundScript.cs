using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{

    public AudioSource partyMusicSource;   
    public AudioLowPassFilter lowPassFilter; 
    public float maxLowPassCutoff = 5000.0f;  
    public float minLowPassCutoff = 500.0f;  
    public float maxBAC = 1.0f;

    public AudioSource crashSound;
    public AudioSource ambulanceSound;
    public AudioSource morningBirdsSound;
    public AudioSource drinkingSound;
    public AudioSource eatingSound;
    public AudioSource drivingAwaySound;
    public AudioSource combinedCarCrash;
    public AudioSource pizzaOffer;
    public AudioSource yesToPizzaOffer;
    public AudioSource noToPizzaOffer;
    public AudioSource preGameOffer;
    public AudioSource yesToPreGame;
    public AudioSource noToPreGame;
    public AudioSource partyDrinkOffer;
    public AudioSource yesToPartyDrinkOffer;
    public AudioSource noToPartyDrinkOffer;
    public AudioSource strangerOffer;
    public AudioSource yesToStrangerOffer;
    public AudioSource noToStrangerOffer;
    public AudioSource peerPressure;
    public AudioSource yesToPeerPressure;
    public AudioSource noToPeerPressure;
    public AudioSource rideOrDrive;
    public AudioSource yesToRide;
    public AudioSource yesToDrive;



    private BACScript bacScript;

    // Start is called before the first frame update
    void Start()
    {
        bacScript = FindObjectOfType<BACScript>();

        
        ResetSoundEffects();
    }

    // Update is called once per frame
    void Update()
    {
        if (bacScript != null)
        {
            UpdateMusicDistortion(bacScript.getBAC());
        }
    }


    // Method to update sound effects based on player's BAC
    void UpdateMusicDistortion(float currentBAC)
    {
       
        float distortionFactor = Mathf.Clamp01(currentBAC / maxBAC);

        
        float targetCutoff = Mathf.Lerp(maxLowPassCutoff, minLowPassCutoff, distortionFactor);
        lowPassFilter.cutoffFrequency = targetCutoff;

        
        partyMusicSource.pitch = Mathf.Lerp(1.0f, 0.8f, distortionFactor);  
    }

    // Method to reset sound effects to normal (when BAC is low or sober)
    public void ResetSoundEffects()
    {
        lowPassFilter.cutoffFrequency = maxLowPassCutoff;  
        partyMusicSource.pitch = 1.0f;  
    }

    public void PlayCrashSound()
    {
        if (crashSound != null)
        {
            crashSound.Play();
        }
    }

    public void PlayCombinedCrashSound()
    {
        if (crashSound != null)
        {
           combinedCarCrash.Play();
        }
    }

    // Method to play ambulance sound
    public void PlayAmbulanceSound()
    {
        if (ambulanceSound != null)
        {
            ambulanceSound.Play();
        }
    }

    // Method to play morning birds sound
    public void PlayMorningBirdsSound()
    {
        if (morningBirdsSound != null)
        {
            morningBirdsSound.Play();
        }
    }

    // Method to play drinking sound
    public void PlayDrinkingSound()
    {
        if (drinkingSound != null)
        {
            drinkingSound.Play();
        }
    }

    // Method to play eating sound
    public void PlayEatingSound()
    {
        if (eatingSound != null)
        {
            eatingSound.Play();
        }
    }

    // Method to play driving away sound
    public void PlayDrivingAwaySound()
    {
        if (drivingAwaySound != null)
        {
            drivingAwaySound.Play();
        }
    }

    public void PlayPizzaOffer()
    {
        if (pizzaOffer != null)
        {
            pizzaOffer.Play();
        }
    }

    public void PlayYesPizzaOffer()
    {
        if (yesToPizzaOffer != null)
        {
            yesToPizzaOffer.Play();
        }
    }

    public void PlayNoPizzaOffer()
    {
        if (noToPizzaOffer != null)
        {
            noToPizzaOffer.Play();
        }
    }


    public void PlayPreGameOffer()
    {
        if (preGameOffer != null)
        {
            preGameOffer.Play();
        }
    }

    public void PlayYesToPreGame()
    {
        if (yesToPreGame != null)
        {
            yesToPreGame.Play();
        }
    }

    public void PlayNoToPreGame()
    {
        if (noToPreGame != null)
        {
            noToPreGame.Play();
        }
    }

    public void PlayPartyDrinkOffer()
    {
        if (partyDrinkOffer != null)
        {
            partyDrinkOffer.Play();
        }
    }

    public void PlayNoToPartyDrink()
    {
        if (noToPartyDrinkOffer != null)
        {
            noToPartyDrinkOffer.Play();
        }
    }

    public void PlayYesToPartyDrink()
    {
        if (yesToPartyDrinkOffer != null)
        {
            yesToPartyDrinkOffer.Play();
        }
    }

    public void PlayStrangerOffer()
    {
        if (strangerOffer != null)
        {
            strangerOffer.Play();
        }
    }

    public void PlayYesToStrangerOffer()
    {
        if (yesToStrangerOffer != null)
        {
            yesToStrangerOffer.Play();
        }
    }

    public void PlayNoToStrangerOffer()
    {
        if (noToStrangerOffer != null)
        {
            noToStrangerOffer.Play();
        }
    }

    public void PlayPeerPressure()
    {
        if (peerPressure != null)
        {
            peerPressure.Play();
        }
    }

    public void PlayYesToPeerPressure()
    {
        if (yesToPeerPressure != null)
        {
            yesToPeerPressure.Play();
        }
    }

    public void PlayNoToPeerPressure()
    {
        if (yesToPeerPressure != null)
        {
            yesToPeerPressure.Play();
        }
    }

    public void PlayRideOrDrive()
    {
        if (rideOrDrive != null)
        {
            rideOrDrive.Play();
        }
    }

    public void PlayYesToDrive()
    {
        if (yesToDrive != null)
        {
            yesToDrive.Play();
        }
    }

    public void PlayYesToRide()
    {
        if (yesToRide != null)
        {
            yesToRide.Play();
        }
    }



}

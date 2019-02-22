using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelected : MonoBehaviour, ISelectHandler
{
    public AudioSource buttonSelectedAudio;
    public bool defaultSelection = false;


    /* Called when this button is selected. Plays the audio.
     */ 
    public void OnSelect(BaseEventData eventData)
    {
        // Don't play the default selected button's audio on scene start (e.g., Play on menu screen)
        if (!defaultSelection)
        {
            buttonSelectedAudio.Play();
        }
        // Switch the flag so the audio can play next time
        else
        {
            defaultSelection = false;
        }
    }
}

using UnityEngine;
using UnityEngine.UI; 

public class AtmosphereTrigger : MonoBehaviour
{
    [Header("Visuals")]
    public Image backgroundObject; // Drag the background UI element here.
    public Sprite[] backgroundImages; // Put all 5 backgrounds here in order (0 to 4).

    // Start at 1! Since Background 1 (index 0) is already on screen, 
    // the first click needs to load Background 2 (index 1).
    private int currentStage = 1; 

    public void NextAtmosphere() 
    {
        // Only swap if we haven't reached the end of the array
        if (currentStage < backgroundImages.Length) 
        {
            backgroundObject.sprite = backgroundImages[currentStage];
            currentStage++; // Prep for the next click
        }
    }
}

/*
Go to your Project window (down at the bottom where your files live) and click on your first background image. (You can hold Shift and click all 5 to do them at once).

Look over at the Inspector window on the right.

At the very top, find the dropdown menu labeled Texture Type.

Change it from Default to Sprite (2D and UI).

Scroll down to the very bottom of the Inspector and click the Apply button.
*/

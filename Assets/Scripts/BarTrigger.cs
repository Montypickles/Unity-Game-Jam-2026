using UnityEngine;
using UnityEngine.UI; 

public class BarTrigger : MonoBehaviour
{
    [Header("Visuals")]
    public Image backgroundObject; // Drag the background UI element here.
    public Sprite[] backgroundImages; // Put all 5 backgrounds here in order (0 to 4).

    // Start at 1! Since Background 1 (index 0) is already on screen, 
    // the first click needs to load Background 2 (index 1).
    private int currentStage = 1; 

    public void NextBar() 
    {
        // Only swap if we haven't reached the end of the array
        if (currentStage < backgroundImages.Length) 
        {
            backgroundObject.sprite = backgroundImages[currentStage];
            currentStage++; // Prep for the next click
        }
    }
}

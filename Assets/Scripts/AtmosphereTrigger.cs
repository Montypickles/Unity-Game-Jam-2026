using UnityEngine;
using UnityEngine.UI; //needed for UI Images.

public class AtmosphereTrigger : MonoBehaviour
{
    [Header("Visuals")]
    public Image backgroundObject; //Drag the background UI element here.
    public Sprite[] backgroundImages; // To keep track of the 5 PNGs.

    private int currentStage = 0; //Keeps track of how many times hint button was pressed

    public void NextAtmosphere() //Link Hint button directly to this function.
    {
        if (currentStage < backgroundImages.Length) //swaps the background if we still have pictures left.
        {
            backgroundObject.sprite = backgroundImages[currentStage];
        }

        currentStage++; //adds one to the counter so its ready for the next button click.
    }

}
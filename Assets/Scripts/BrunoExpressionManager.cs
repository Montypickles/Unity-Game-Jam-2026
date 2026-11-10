using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BrunoExpressionManager : MonoBehaviour
{
    [Header("UI Referance")]
    public Image brunoDisplay; //This will be the blank image box from the hirearchy.

    [Header("Sprites")]
    public Sprite defaultIdleSprite; //The Main Bruno Sprite
    public Sprite[] talkingSprites; //Drop all the talking sprites in here.

    private Coroutine resetTimer;

    //This method is called in DialogueManager.cs
    public void ShowRandomTalkingExpression()
    {
        if (talkingSprites.Length > 0) //Making sure sprites has loaded to avoid crash.
        {
            int randomIndex = Random.Range(0, talkingSprites.Length); //creating a variable to make a list with the amount of sprites.
            brunoDisplay.sprite = talkingSprites[randomIndex]; //creating a veriable to pick it at random.
            if (resetTimer != null)
            {
                StopCoroutine(resetTimer); //if theres already a timer going. this stops it, therefore spamming textbox wont glitch it out.
            }
            resetTimer = StartCoroutine(ResetToIdle()); //staring timer from fresh.
        }
    }
    private IEnumerator ResetToIdle()
    {
        yield return new WaitForSeconds(1.5f);
        brunoDisplay.sprite = defaultIdleSprite;
    }
}

/*
=======
SCRIPT EXPLANATION
=======
WHAT DOES THE SCRIPT DO:
This script handles brunos physical reactions, when the text updates it grabs a random tallking 
sprite that has been uploaded and displays it for 1.5 seconds then snapps back to the defult "idle" bruno.

VARIABLES INTRODUCED:
- brunoDisplay (Image): The UI component on the Canvas that renders the picture.
- defaultIdleSprite (Sprite): Bruno's neutral, resting face asset.
- talkingSprites (Sprite[]): An array pool of various talking/reaction assets.
- resetTimer (Coroutine): A background tracker that handles the 1.5-second countdown.

HOW TO APPLY IN UNITY:
1. Drag and drop this script onto your GameManager or Canvas object.
2. Inspector: Drag Bruno's UI Image from the Hierarchy into 'Bruno Display'.
3. Inspector: Drag the neutral asset from the Project window into 'Default Idle Sprite'.
4. Inspector: Drag all talking face assets from the Project window into 'Talking Sprites'.
5. Scripting: Inside your Dialogue script, right after updating the text, call:
   brunoExpressionManager.ShowRandomTalkingExpression();
======
*/
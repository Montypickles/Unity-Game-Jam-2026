using System.Collections.Generic; 
using UnityEngine;
using TMPro; // textmeshpro stuff for the hints counter

public class HintManager : MonoBehaviour 
{
    public TargetManager targetmanager; 
    public DialogueManager dialogueManager; // plugs into Bruno's speech bubble
    public TextMeshProUGUI hintsRemainingText; // bottom-left UI counter
    public GuessManager guessManager;
    private List<string> hintsUnused = new List<string>(); 
    private int givenHints = 0; 
    private int maxHints = 5; 

    [Header("Expression Link")]
    public BrunoExpressionManager brunoExpressionManager;

    // Anti-spam timers
    private float cooldownDuration = 1.5f; 
    private float nextAllowedClickTime = 0f;

    private void Start()
    {
        // automatically loads the hints when the round starts
        if (targetmanager == null) targetmanager = FindAnyObjectByType<TargetManager>();
        if (dialogueManager == null) dialogueManager = FindAnyObjectByType<DialogueManager>();
        if(guessManager == null) guessManager = FindAnyObjectByType<GuessManager>();
        hintsSetup();
    }

    public void hintsSetup() 
    {
        hintsUnused.Clear();
        if (targetmanager != null && targetmanager.roundCurrent != null)
        {
             hintsUnused.AddRange(targetmanager.roundCurrent.hints);
        }  
        givenHints = 0; 
        UpdateHintCounter();
    } 
    
    public void randomHint() 
    {
        if (Time.time < nextAllowedClickTime)
        {
            return; // Stops the click if 1.5 seconds haven't passed
        }
        nextAllowedClickTime = Time.time + cooldownDuration;

        // if they used all 5 hints (or the list is empty), Bruno roasts them
        if (givenHints >= maxHints || hintsUnused.Count == 0) 
        {
            dialogueManager.SayOutOfHints();
            if (brunoExpressionManager != null) brunoExpressionManager.ShowRandomTalkingExpression(); 
            if (guessManager != null) guessManager.TriggerGameOver();
            return; 
        }

        // pick a random unused hint and send it to Bruno's speech bubble
        int randomIndex = Random.Range(0, hintsUnused.Count);
        string hintChosen = hintsUnused[randomIndex];
        dialogueManager.SayHint(hintChosen); 

        if (brunoExpressionManager != null)
        {
            brunoExpressionManager.ShowRandomTalkingExpression();
        }

        // remove it so it doesn't repeat and tick the counter up
        hintsUnused.RemoveAt(randomIndex);
        givenHints++;
        UpdateHintCounter();
    } 

    private void UpdateHintCounter()
    {
        // updates the bottom-left text if Pickles plugged it in
        if (hintsRemainingText != null)
        {
            int remaining = maxHints - givenHints;
            hintsRemainingText.text = "Hints Remaining: " + remaining;
        }
    }
}
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

    private void Start()
    {
        // automatically loads the hints when the round starts
        if (targetManager == null) targetmanager = FindObjectOfType<TargetManager>();
        if (dialogueManager == null) dialogueManager = FindObjectOfType<DialogueManager>();
        if(guessManager == null) guessManager = FindObjectOfType<GuessManager>();
        hintsSetup();
    }

    public void hintsSetup() 
    {
        hintsUnused.Clear(); 
        hintsUnused.AddRange(targetmanager.roundCurrent.hints); 
        givenHints = 0; 
        UpdateHintCounter();
    } 
    
    public void randomHint() 
    {
        // if they used all 5 hints (or the list is empty), Bruno roasts them
        if (givenHints >= maxHints || hintsUnused.Count == 0) 
        {
            dialogueManager.SayOutOfHints(); 
            if (guessManager != null) guessManager.TriggerGameOver();
            return; 
        }

        // pick a random unused hint and send it to Bruno's speech bubble
        int randomIndex = Random.Range(0, hintsUnused.Count);
        string hintChosen = hintsUnused[randomIndex];
        dialogueManager.SayHint(hintChosen); 

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
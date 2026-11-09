using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI brunoSpeechBubble;
    [Header("Bruno's Game FLow Dialogue")]
    public string[] greetingDialogue;
    public string[] idleBanter;
    public string[] gameOverInsults;
    public string[] winDialogue;
    
    [Header("Bruno's Guessing Insults")]
    public string[] tooLowInsults;
    public string[] tooHighInsults;
    public string[] blankInputInsults;
    public string[] letterInputInsults;
    public string[] outOfHintsInsults;
    public string[] timeoutInsults;

    [Header("Bruno's Final Strike")]
    public string[] finalStrikeInsults; 

    private void Start()
    {
        if (brunoSpeechBubble == null)
        {
            Debug.LogError("PICKLES: You forgot to add a script cuh");
            return;
        }
        sayGreeting();
    }
    
    //1. GAME START
    public void sayGreeting()
    {
        brunoSpeechBubble.text = GetSafeRandomLine(greetingDialogue);
    }

    // 2. IDLE
    public void SayIdleBanter()
    {
        brunoSpeechBubble.text = GetSafeRandomLine(idleBanter); 
    }

    // 3. TOO LOW
    public void SayTooLow()
    {
        brunoSpeechBubble.text = GetSafeRandomLine(tooLowInsults); 
    }

    // 4. TOO HIGH
    public void SayTooHigh()
    {
        brunoSpeechBubble.text = GetSafeRandomLine(tooHighInsults); 
    }

    // 5. BLANK INPUT
    public void SayBlankInput()
    {
        brunoSpeechBubble.text = GetSafeRandomLine(blankInputInsults); 
    }

    // 6. LETTER INPUT ("bob")
    public void SayLetterInput()
    {
        brunoSpeechBubble.text = GetSafeRandomLine(letterInputInsults); 
    }

    // 7. WIN STATE
    public void SayWin()
    {
        brunoSpeechBubble.text = GetSafeRandomLine(winDialogue); 
    }

    // 8. GAME OVER (Timer ran out)
    public void SayGameOver()
    {
        brunoSpeechBubble.text = GetSafeRandomLine(gameOverInsults); 
    }

    // 9. FINAL STRIKE (Failed 5th attempt right before Application.Quit)
    public void SayFinalStrike()
    {
        brunoSpeechBubble.text = GetSafeRandomLine(finalStrikeInsults); 
    }

    //10. OUT OF HINTS
    public void SayOutOfHints()
    {
        brunoSpeechBubble.text = GetSafeRandomLine(outOfHintsInsults);
    }
    
    // 11. HINT
    public void SayHint(string theHint)
    {
        brunoSpeechBubble.text = theHint; 
    }

    //12. TOO LONG
    public void SayTimeout()
    {
        brunoSpeechBubble.text = GetSafeRandomLine(timeoutInsults);
    } 

    //Setting up saftey
    private string GetSafeRandomLine(string[] dialogueList)
    {
        if (dialogueList == null || dialogueList.Length == 0)
        {
            return "..."; 
        }

        int randomIndex = Random.Range(0, dialogueList.Length);
        return dialogueList[randomIndex];
    }
}
    
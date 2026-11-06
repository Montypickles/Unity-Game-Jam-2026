
using UnityEngine;
using TMPro; //Improrting TextMeshPro so we can control Modern UI text.

public class GuessManager : MonoBehaviour  //Allows script to be attatchable. 
{
    public TargetManager targetManager; //Connects to target manager to check what the lucky number is
    public TMP_InputField playerInput; //Links the UI box to where the player actually type their guess.

    public TextMeshProUGUI feedbackText; //Links the UI text to tell player "Too High" or "Too Low"

    [Header("Brunos Banter List Scripts: Guess Manager")]
    public string[] missingInputBanter; //When the user doesnt type anything.
    public string[] notNumberBanter; //When the user puts any letter into the box.
    public string[] winBanter; //When the user wins.
    public string[] tooLowBanter; //When the user input is too low.
    public string[] tooHighBanter; //When the user input is too high.
    public void CheckGuess()
    {
        //1. Saftey Check: Did they type anything?
        if (string.IsNullOrEmpty(playerInput.text))
        {
            feedbackText.text = missingInputBanter[Random.Range(0, missingInputBanter.Length)];
            return;    
        }

        //2. Saftey Check: Did they type something other than a number?
        int playerGuess; //Creating an empty bucket for the number
        bool isNumber = int.TryParse(playerInput.text, out playerGuess); //Tries to safely turn the text into math.

        if (isNumber == false) //if tthey inputted letters this catches it.
        {
            feedbackText.text = notNumberBanter[Range.Range(0, notNumberBanter.Length)];
            playerInput.text = ""; //resets the Text Box.
            return; //Stops the code to avoid bottleneck.
        }

        //3. THE MATH: Grab the lucky file.
        int correctNumber = targetManager.roundCurrent.luckyNumber;

        //4. THE WIN STATE.
        if (playerGuess == correctNumber)
        {
            feedbackText.text = winBanter[Random.Range(0, winBanter.Length)];
        }

        //5. THE LOSE STATE. (Too Small)
        else if (playerGuess < correctNumber)
        {
            feedbackText.text = tooLowBanter[Random.Range(0, tooLowBanter.Length)];
        }

        //6. THE LOSE STATE (Too High)
        else if (playerGuess > correctNumber)
        {
            feedbackText.text = tooHighBanter[Random,Range(0, tooHighBanter.Length)];
        }

        //7. CLEANUP: Resetting the box for the next guess.
        playerInput.text = "";
    }
}

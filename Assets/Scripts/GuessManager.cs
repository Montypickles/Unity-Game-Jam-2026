using UnityEngine;
using TMPro; // textmeshpro stuff

public class GuessManager : MonoBehaviour 
{
    public TargetManager targetManager; 
    public TMP_InputField playerInput; 
    public TextMeshProUGUI feedbackText; 

    [Header("Brunos Banter List Scripts: Guess Manager")]
    public string[] missingInputBanter; 
    public string[] notNumberBanter; 
    public string[] winBanter; 
    public string[] tooLowBanter; 
    public string[] tooHighBanter; 
    public string[] gameOverBanter; 

    private int failedAttempts = 0; 
    private bool isGameOver = false; // master lock to freeze the game

    public void CheckGuess()
    {
        // if the game ended, ignore all button clicks
        if (isGameOver) return;

        // if they didn't type anything
        if (string.IsNullOrEmpty(playerInput.text))
        {
            feedbackText.text = missingInputBanter[Random.Range(0, missingInputBanter.Length)];
            return;    
        }

        // catch letters and symbols
        int playerGuess; 
        bool isNumber = int.TryParse(playerInput.text, out playerGuess); 

        if (isNumber == false) 
        {
            feedbackText.text = notNumberBanter[Random.Range(0, notNumberBanter.Length)];
            playerInput.text = ""; 
            return; 
        }

        // grab the target number
        int correctNumber = targetManager.roundCurrent.luckyNumber;

        // win state
        if (playerGuess == correctNumber)
        {
            feedbackText.text = winBanter[Random.Range(0, winBanter.Length)];
            isGameOver = true; // flip the master lock
            playerInput.interactable = false; // gray out the text box
            return; 
        }

        // add a strike
        failedAttempts++;

        // 5 strikes and you're out
        if (failedAttempts >= 5)
        {
            feedbackText.text = gameOverBanter[Random.Range(0, gameOverBanter.Length)];
            isGameOver = true; // flip the master lock
            playerInput.interactable = false; // gray out the text box so they can't type
            return; 
        }

        // too small
        if (playerGuess < correctNumber)
        {
            feedbackText.text = tooLowBanter[Random.Range(0, tooLowBanter.Length)];
        }
        // too big
        else if (playerGuess > correctNumber)
        {
            feedbackText.text = tooHighBanter[Random.Range(0, tooHighBanter.Length)];
        }

        // clear the box for next time
        playerInput.text = "";
    }
}
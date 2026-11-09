using UnityEngine;
using TMPro; // textmeshpro stuff

public class GuessManager : MonoBehaviour 
{
    public TargetManager targetManager; 
    public TMP_InputField playerInput; 
    public TextMeshProUGUI feedbackText; 
    public UIManager uiManager;

    [Header("Brunos Banter List Scripts: Guess Manager")]
    public string[] missingInputBanter; 
    public string[] notNumberBanter; 
    public string[] winBanter; 
    public string[] tooLowBanter; 
    public string[] tooHighBanter; 
    public string[] gameOverBanter; 

    private int failedAttempts = 0; 
    public bool isGameOver = false; // master lock to freeze the game

    void Start()
    {
        if (targetManager == null) targetManager = FindAnyObjectByType<TargetManager>();
        if (uiManager == null) uiManager = FindAnyObjectByType<UIManager>();
    }

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

        if (targetManager != null && targetManager.roundCurrent == null)
        {
            targetManager.SelectRandomRound();
        }
        
        if (targetManager == null || targetManager.roundCurrent == null) return;
       
       
        // grab the target number
        int correctNumber = targetManager.roundCurrent.luckyNumber;

        // win state
        if (playerGuess == correctNumber)
        {
            isGameOver = true;
            playerInput.interactable = false;

            uiManager.StopTimer();
            uiManager.ShowWinPanel();

            return;
        }

        // add a strike
        failedAttempts++;

        // 5 strikes and you're out
        if (failedAttempts >= 5)
        {
            feedbackText.text = gameOverBanter[Random.Range(0, gameOverBanter.Length)];
            TriggerGameOver();
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
    public void TriggerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        playerInput.interactable = false;

        if (uiManager != null)
        {
            uiManager.ShowLosePanel();
        }
    }

    /*
    private void Update()
    {
        
        //If they hit enter the input is taken
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckGuess();
            playerInput.ActivateInputField(); //Keeps typing Curser active.
        }
       
    }
    */
}
using UnityEngine;
using TMPro;

public class GuessManager : MonoBehaviour
{
    public TargetManager targetManager;
    public TMP_InputField playerInput;
    public UIManager uiManager;
    public DialogueManager dialogueManager;

    private int failedAttempts = 0;
    public bool isGameOver = false;

    void Start()
    {
        if (targetManager == null)
            targetManager = FindAnyObjectByType<TargetManager>();

        if (uiManager == null)
            uiManager = FindAnyObjectByType<UIManager>();

        if (dialogueManager == null)
            dialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    public void CheckGuess()
    {
        if (isGameOver) return;

        // Empty input
        if (string.IsNullOrWhiteSpace(playerInput.text))
        {
            dialogueManager.SayBlankInput();
            return;
        }

        // Invalid number
        if (!int.TryParse(playerInput.text, out int playerGuess))
        {
            dialogueManager.SayLetterInput();
            playerInput.text = "";
            return;
        }

        // Ensure a round exists
        if (targetManager != null && targetManager.roundCurrent == null)
        {
            targetManager.SelectRandomRound();
        }

        if (targetManager == null || targetManager.roundCurrent == null)
            return;

        int correctNumber = targetManager.roundCurrent.luckyNumber;

        // Correct guess
        if (playerGuess == correctNumber)
        {
            isGameOver = true;
            playerInput.interactable = false;

            dialogueManager.SayWin();

            uiManager.StopTimer();
            uiManager.ShowWinPanel();

            return;
        }

        // Incorrect guess
        failedAttempts++;

        // Fifth incorrect guess
        if (failedAttempts >= 5)
        {
            dialogueManager.SayFinalStrike();
            TriggerGameOver();
            return;
        }

        if (playerGuess < correctNumber)
        {
            dialogueManager.SayTooLow();
        }
        else
        {
            dialogueManager.SayTooHigh();
        }

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
}

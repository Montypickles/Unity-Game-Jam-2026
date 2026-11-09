using UnityEngine;
using HeyHeyThere.PixelUIFree; // Required import for the custom downloaded pixel bar.

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Plug the custom downloaded pixel bar in here.")]
    public timerSlider pixelProgressBar; 

    [Header("Script References")]
    public GuessManager guessManager;
    public DialogueManager dialogueManager;

    [Header("Timer Settings")]
    public float maxTime = 150f; // 2mins 30seconds seconds per game.
    private float currentTime;
    private bool isTimerRunning = false; // Prevents timer from running before game starts.

    private void Start()
    {
        // Auto-grab custom pixel bar if the script is attached to it.
        if (pixelProgressBar == null) 
        {
            pixelProgressBar = GetComponent<timerSlider>();
        }

        StartTimer();
    }

    private void Update()
    {
        // IF: timer has stopped, or the game is over, do nothing.
        if (!isTimerRunning || guessManager.isGameOver) return;
        
        currentTime -= Time.deltaTime; // Drain time on seconds not framerate.

        // Update custom Pixel Bar to match hidden math (percentage: 1.0 down to 0.0).
        if (pixelProgressBar != null) 
        {
            pixelProgressBar.value = currentTime / maxTime; 
        }

        // IF: checking if the player has ran out of time.
        if (currentTime <= 0)
        {
            TriggerTimeout();
        }
    }

    // New class: make timer run when the scene loads.
    public void StartTimer()
    {
        currentTime = maxTime;
        
        // Reset the bar to 100% full.
        if (pixelProgressBar != null) pixelProgressBar.value = 1f; 
        
        isTimerRunning = true;
    }

    // New Class: calling from GuessManager so if they win the clock freezes.
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private void TriggerTimeout()
    {
        StopTimer();
        currentTime = 0; // Locking the timeout at 0.
        
        // Empty the bar.
        if (pixelProgressBar != null) pixelProgressBar.value = 0f; 

        guessManager.TriggerGameOver();
        // Telling GuessManager to lock text box at the end of the game.

        dialogueManager.SayTimeout();
        // Tells Bruno to insult them if theyre going too slow.
    }
}
using UnityEngine;
using UnityEngine.UI; // Required import for the SLider and slider UI.
using HeyHeyThere.PixelUIFree; // Required import for the custom downloaded pixel bar.

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Plug a standard Unity Slider in here.")]
    public Slider timerSlider;
    
    [Tooltip("Plug the custom downloaded pixel bar in here.")]
    public timerSlider customPixelBar; 

    [Header("Script Referances")]
    public GuessManager guessManager;
    public DialogueManager dialogueManager;

    [Header("Timer Settings")]
    public float maxTime = 150f; // 2mins 30seconds seconds per game.
    private float currentTime;
    private bool isTimerRunning = false; // Prevents timer from running before game starts.

    private void Start()
    {
        // Auto-grab standard slider if the script is attached to it.
        if (timerSlider == null) 
        {
            timerSlider = GetComponent<Slider>();
        }
        
        // Auto-grab custom pixel bar if the script is attached to it.
        if (customPixelBar == null) 
        {
            customPixelBar = GetComponent<timerSlider>();
        }

        // Setting timer max value, and fills it up completely to start.
        if (timerSlider != null)
        {
            timerSlider.maxValue = maxTime;
        }

        StartTimer();
    }

    private void Update()
    {
        // IF: timer has stopped, or the game is over, do nothing.
        if (!isTimerRunning || guessManager.isGameOver) return;
        
        currentTime -= Time.deltaTime; // Drain time on seconds not framerate.
        
        // Update standard Unity slider bar to match hidden math (150 down to 0).
        if (timerSlider != null)
        {
            timerSlider.value = currentTime; 
        }

        // Update custom Pixel Bar to match hidden math (percentage: 1.0 down to 0.0).
        if (customPixelBar != null) 
        {
            customPixelBar.value = currentTime / maxTime; 
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
        
        // Reset whichever bars actually exist on the screen.
        if (timerSlider != null) timerSlider.value = currentTime;
        if (customPixelBar != null) customPixelBar.value = 1f; // 1f = 100% full
        
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
        
        // Empty whichever bars actually exist.
        if (timerSlider != null) timerSlider.value = 0;
        if (customPixelBar != null) customPixelBar.value = 0f; // 0f = completely empty

        guessManager.TriggerGameOver();
        // Telling GuessManager to lock text box at the end of the game.

        dialogueManager.SayTimeout();
        // Tells Bruno to insult them if theyre going too slow.
    }
}
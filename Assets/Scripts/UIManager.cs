using UnityEngine;
using UnityEngine.UI; //Required import for the SLider and slider UI.

public class UIManager : MonoBehaviour
{
    [Header ("UI Elements")]
    [Tooltip("Plug a standard Unity Slider in here")]
    public Slider timerSlider;

    [Header ("Script Referances")]
    public GuessManager guessManager;
    public DialogueManager dialogueManager;

    [Header("Timer Settings")]
    public float maxTime = 150f; // 2mins 30seconds seconds per game.
    private float currentTime;
    private bool isTimerRunning = false; // Prevents timer from running before game starts.

    private void Start()
    {
        //Setting timer to 30s, and fills it up completely to start.
        timerSlider = GetComponent<Slider>();
        timerSlider.maxValue = maxTime;
        StartTimer();
    }

    private void Update()
    {
        //IF: timer has stopped, or the game is over, do nothing.
        if (!isTimerRunning || guessManager.isGameOver) return;
        currentTime -= Time.deltaTime; // Drain time on seconds not framerate.
        timerSlider.value = currentTime; //Update slider bar to match hidden math.

        //IF: checking if the player has ran out of time.
        if (currentTime <= 0)
        {
            TriggerTimeout();

        }
    }
    //New class: make timer run when the scene loads.
    public void StartTimer()
    {
        currentTime = maxTime;
        timerSlider.value = currentTime;
        isTimerRunning = true;
    }

    //New Class: calling from GuessManager so if they win the clock freezes.
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private void TriggerTimeout()
    {
        StopTimer();
        currentTime = 0; //Locking the timeout at 0.
        timerSlider.value = 0;

        guessManager.TriggerGameOver();
        //Telling GuessManager to lock text box at the end of the game.

        dialogueManager.SayTimeout();
        //Tells Bruno to insult them if theyre going too slow.
    }
}




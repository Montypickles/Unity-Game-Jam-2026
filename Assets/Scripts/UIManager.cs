using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("UI ELements & Components")]
    public Image pixelProgressBarSlider; // We are using the standard Unity Image now!
    public TextMeshProUGUI timerCountdownText; //This is for the 30 sec demo text box.
    public TMP_InputField playerInput; //This is the textbox the player inputs the text too as we need to clear it.

    [Header("Win Screen")]
    public GameObject winPanel;

    public void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    [Header("Script Referances")]
    public GuessManager guessManager;
    public HintManager hintManager;
    public DialogueManager dialogueManager;

    [Header("Timer Settings")]
    public float maxTime = 150f; //2 mins 30 sec.
    private float currentTime;
    private bool isTimerRunning = false;

    private void Start()
    {
        //Auto-find the referance if left unassigned in the inspector.
        if (guessManager == null) guessManager = FindAnyObjectByType<GuessManager>();
        if (hintManager == null) hintManager = FindAnyObjectByType<HintManager>();
        if (dialogueManager == null) dialogueManager = FindAnyObjectByType<DialogueManager>();
        if (playerInput == null) playerInput = FindAnyObjectByType<TMP_InputField>();
        
        // Note: You MUST manually drag the 'Fill' child object into the Inspector slot for the slider!

        //Show bar at 100% and at 2:30 on setup.
        currentTime = maxTime;
        UpdateTimerDisplay();
        if (pixelProgressBarSlider != null)
        {
            pixelProgressBarSlider.fillAmount = 1f;
        }

        //give a two second delay when loading up the secene before the timer starts counting down for scene transition to stop
        //and player to registered the games loaded.
        StartCoroutine(StartTimerWithDelay(2f));
    }

    private IEnumerator StartTimerWithDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        StartTimer();
    }

    private void Update()
    {
        //Do nothing if the timer has stopped or game is over.
        if (!isTimerRunning || (guessManager != null && guessManager.isGameOver)) return;

        //Count down time.
        currentTime -= Time.deltaTime;

        //1. Update the Pixel Progress Bar using the standard fillAmount.
        if (pixelProgressBarSlider != null)
        {
            pixelProgressBarSlider.fillAmount = currentTime / maxTime; 
        }

        //2. Update the Mins:Sec display on screen without milliseconds.
        UpdateTimerDisplay();

        //3. Check for timeout
        if (currentTime <= 0)
        {
            TriggerTimeout();
        }
    }

    public void StartTimer()
    {
        if (maxTime <= 0f) maxTime = 150f;
        currentTime = maxTime;
        UpdateTimerDisplay(); 
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private void UpdateTimerDisplay()
    {
        if (timerCountdownText == null) return;

        //Setting up the format so theres no .00 in the timer.
        int mins = Mathf.FloorToInt(currentTime / 60f);
        int sec = Mathf.FloorToInt(currentTime % 60f);
        timerCountdownText.text = string.Format("{0:00}:{1:00}", mins, sec);
    }

    private void TriggerTimeout()
    {
        StopTimer();
        currentTime = 0f;

        if (pixelProgressBarSlider != null)
        {
            pixelProgressBarSlider.fillAmount = 0f;
        }

        if (guessManager != null)
        {
            guessManager.TriggerGameOver();
        }

        if (dialogueManager != null)
        {
            dialogueManager.SayTimeout();
        }
    }

    //BUTTON WIRING AND INPUT HELPERS.

    //Hook this method up to your Confirm/Guess button OnClick() in the inspector.
    public void OnSubmitGuessClicked()
    {
        if (guessManager != null)
        {
            guessManager.CheckGuess();
        }

        ClearInput();
    }

    //Hook this method up on the Hint button OnClick() in the inspector
    public void OnRequestHintClicked()
    {
        if (hintManager != null)
        {
            hintManager.randomHint();
        }
    }

    //CLears the input box and keeps cursor active for continuious typing.
    public void ClearInput()
    {
        if (playerInput != null)
        {
            playerInput.text = "";
            playerInput.ActivateInputField();
        }
    }

    // SCENE MANAGEMENT HOOKS.

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
using UnityEngine;
using UnityEngine.UI; 
// Lets UIManager directly shrink the UI bar Image 
using HeyHeyThere.PixelUIFree; // Kept for the HeyHeyThere Pixel UI asset pack 

public class UIManager : MonoBehaviour 
{ 
    [Header("UI Elements")]
    [Tooltip("Drag the Fill child Image (under GreenBar) in here.")] 
    public Image pixelProgressBar; 
    
    [Header("Script References")] 
    public GuessManager guessManager;
    public DialogueManager dialogueManager; 
    
    [Header("Timer Settings")] 
    public float maxTime = 150f; // 2mins 30seconds per game. 
    private float currentTime; 
    private bool isTimerRunning = false; 
    private Vector3 initialScale = Vector3.one; 

    private void Start() 
    { 
        // Auto-find managers so Update() never crashes with a NullReferenceException 
        if (guessManager == null) 
            guessManager = FindObjectOfType<GuessManager>(); 
            
        if (dialogueManager == null) 
            dialogueManager = FindObjectOfType<DialogueManager>(); 
            
        // Auto-grab the Image if this script was placed directly on the bar 
        if (pixelProgressBar == null) 
        { 
            pixelProgressBar = GetComponent<Image>(); 
        } 
        
        if (pixelProgressBar != null) 
        { 
            // Stop the HeyHeyThere timerSlider or Unity Slider on the parent from overriding our bar! 
            timerSlider customSlider = pixelProgressBar.GetComponentInParent<timerSlider>(); 
            if (customSlider != null) 
                customSlider.enabled = false; 
                
            Slider unitySlider = pixelProgressBar.GetComponentInParent<Slider>(); 
            if (unitySlider != null) 
                unitySlider.enabled = false; 
                
            initialScale = pixelProgressBar.rectTransform.localScale; 
            if (initialScale.x == 0f) 
                initialScale = Vector3.one; 
                
            // If it has a sprite, also configure Image.Type.Filled 
            if (pixelProgressBar.sprite != null) 
            { 
                pixelProgressBar.type = Image.Type.Filled;
                pixelProgressBar.fillMethod = Image.FillMethod.Horizontal;
                pixelProgressBar.fillOrigin = (int)Image.OriginHorizontal.Left; 
            } 
        } 
        
        StartTimer();
    } 

    private void Update() 
    { 
        // IF: timer has stopped, or the game is over, do nothing. 
        if (!isTimerRunning || (guessManager != null && guessManager.isGameOver))
            return; 
            
        currentTime -= Time.deltaTime; // Drain time in seconds, not framerate. 
        
        // Shrink the bar image from 1.0 (100%) down to 0.0 (0%) 
        SetBarFill(currentTime / maxTime); 
        
        // IF: checking if the player has run out of time. 
        if (currentTime <= 0) 
        { 
            TriggerTimeout(); 
        } 
    } 

    public void StartTimer() 
    { 
        if (maxTime <= 0f) 
            maxTime = 150f; 
            
        currentTime = maxTime; 
        SetBarFill(1f); // Reset the bar image to 100% full. 
        isTimerRunning = true; 
    } 

    public void StopTimer() 
    { 
        isTimerRunning = false; 
    } 

    private void TriggerTimeout() 
    { 
        StopTimer(); 
        currentTime = 0; 
        SetBarFill(0f); // Empty the bar image completely. 
        
        if (guessManager != null)
            guessManager.TriggerGameOver(); 
            
        if (dialogueManager != null)
            dialogueManager.SayTimeout(); 
    } 

    // Directly shrinks the bar image whether it uses a HeyHeyThere sprite or a solid color block 
    private void SetBarFill(float normalizedValue) 
    { 
        if (pixelProgressBar == null) 
            return; 
            
        float clamped = Mathf.Clamp01(normalizedValue); 
        
        if (pixelProgressBar.sprite != null && pixelProgressBar.type == Image.Type.Filled) 
        { 
            pixelProgressBar.fillAmount = clamped; 
        } 
        
        // Also scale the width directly so the green bar image is guaranteed to shrink visually 
        pixelProgressBar.rectTransform.localScale = new Vector3(
            initialScale.x * clamped, 
            initialScale.y, 
            initialScale.z 
        ); 
    } 
}
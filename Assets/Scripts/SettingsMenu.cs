using UnityEngine;
using UnityEngine.UI; // Required for the Slider and Toggle components

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Controls")]
    public Slider volumeSlider; // The UI slider for volume
    public Toggle muteToggle;   // The UI checkbox for mute

    private void Start()
    {
        // 1. When the settings screen opens, load the saved settings from the player's device.
        // If they have never played before, default to max volume (1f) and unmuted (0).
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);
        bool savedMute = PlayerPrefs.GetInt("GameMuted", 0) == 1;

        // 2. Visually update the UI Slider and Checkbox to match the saved settings
        if (volumeSlider != null) volumeSlider.value = savedVolume;
        if (muteToggle != null) muteToggle.isOn = savedMute;

        // 3. Push these settings to the AudioManager immediately so the game sounds right
        ApplyVolumeAndMute();
    }

    // Pickles will link the Slider's "On Value Changed" event to this function
    public void OnVolumeSliderChanged()
    {
        // Save the new volume level so the game remembers it next time
        PlayerPrefs.SetFloat("GameVolume", volumeSlider.value); 
        ApplyVolumeAndMute();
    }

    // Pickles will link the Mute Checkbox's "On Value Changed" event to this function
    public void OnMuteToggleChanged()
    {
        // PlayerPrefs can't save booleans, so we save 1 for true (muted) and 0 for false (unmuted)
        PlayerPrefs.SetInt("GameMuted", muteToggle.isOn ? 1 : 0); 
        ApplyVolumeAndMute();
    }

    private void ApplyVolumeAndMute()
    {
        // Safely pass the UI values over to our global Audio Manager
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetVolume(volumeSlider.value);
            AudioManager.Instance.ToggleMute(muteToggle.isOn);
        }
    }
}

/*
=============================================================================
SCRIPT EXPLANATION: SettingsMenu
=============================================================================
WHAT THIS SCRIPT DOES:
This script acts as the middleman between the player's UI interactions and the 
global Audio Manager. It reads the slider and checkbox, saves the player's 
preferences using PlayerPrefs (so their settings don't reset every time they 
close the game), and then commands the Audio Manager to update the actual sound output.

VARIABLES IN THE CODE:
- volumeSlider: The physical UI slider graphic the player drags.
- muteToggle: The physical UI checkbox/toggle the player clicks to silence the game.
- savedVolume / savedMute: Temporary variables used to pull the player's history from PlayerPrefs.

HOW TO APPLY IT TO UNITY (For Pickles):
1. Open the Settings scene.
2. Create an empty GameObject (or just select the main Canvas) and name it "SettingsManager".
3. Attach this SettingsMenu.cs script to it.
4. Drag the UI Slider and UI Mute Toggle from the Hierarchy into the empty slots on the script.
5. Select the UI Slider, scroll down to the 'On Value Changed' box, click the '+', drag in the "SettingsManager" object, and select SettingsMenu -> OnVolumeSliderChanged.
6. Select the UI Mute Toggle, scroll down to the 'On Value Changed' box, click the '+', drag in the "SettingsManager" object, and select SettingsMenu -> OnMuteToggleChanged.
=============================================================================
*/
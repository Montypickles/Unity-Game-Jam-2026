using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music Tracks")]
    public AudioClip menuMusic; //Plays across all menus.
    public AudioClip gameplayMusic; //Fades in for GameState.

    [Header("Sound Effects")]
    public AudioClip buttonSFX;
    public AudioClip brunoDialogueSFX;

    [Header("Volume Controls")]
    [Range(0f, 1f)] public float globalVolume = 1f; // The main volume controlled by the slider
    private bool isMuted = false; // Tracks if the player checked the mute box

    [Header("Settings")]
    public float fadeDuration = 1.0f; //How many seconds the fade will take

    private void Awake()
    {
        //Keeps this script alive across all scenes.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load the saved volume/mute settings the exact second the game boots
        globalVolume = PlayerPrefs.GetFloat("GameVolume", 1f);
        isMuted = PlayerPrefs.GetInt("GameMuted", 0) == 1;
        ApplyVolume(); // Applies the saved settings immediately

        if (musicSource != null)
        {
            PlayMenuMusic();
        }
    }

    // --- VOLUME RECEIVERS (Called by SettingsMenu.cs) ---
    public void SetVolume(float newVolume)
    {
        globalVolume = newVolume;
        ApplyVolume();
    }

    public void ToggleMute(bool muteStatus)
    {
        isMuted = muteStatus;
        ApplyVolume();
    }

    private void ApplyVolume()
    {
        // First handle muting
        if (musicSource != null) musicSource.mute = isMuted;
        if (sfxSource != null) sfxSource.mute = isMuted;

        // If not muted, apply the exact slider volume
        if (!isMuted)
        {
            if (musicSource != null) musicSource.volume = globalVolume;
            if (sfxSource != null) sfxSource.volume = globalVolume;
        }
    }

    //Music Control.
    public void PlayMenuMusic()
    {
        if(musicSource.clip != menuMusic)
        {
            musicSource.clip = menuMusic;
            musicSource.Play();
        }
    }

    public void TransitionToGameplayMusic()
    {
        if (musicSource.clip != gameplayMusic)
        {
            StartCoroutine(FadeTrack(gameplayMusic));
        }
    }

    private IEnumerator FadeTrack(AudioClip newClip)
    {
        float currentTime = 0;
        float startVol = musicSource.volume;

        // 1. Fade out current track
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVol, 0, currentTime / fadeDuration);
            yield return null;
        }

        // 2. Swap the track
        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();

        // 3. Fade in new track
        currentTime = 0;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, globalVolume, currentTime / fadeDuration); // Fades up to whatever the slider is set to
            yield return null;
        }
        
        musicSource.volume = globalVolume; // Ensure it snaps exactly to your target volume
    }

    // --- SFX CONTROLS ---
    public void PlayButtonSound()
    {
        if (buttonSFX != null && sfxSource != null) sfxSource.PlayOneShot(buttonSFX);
    }

    public void PlayBrunoSound()
    {
        if (brunoDialogueSFX != null && sfxSource != null) sfxSource.PlayOneShot(brunoDialogueSFX);
    }
}

/*
Right, here is exactly what this script does. It acts as our global audio manager
that doesn't get destroyed between scenes. This means the menu track plays continuously
through the Main Menu, Settings, and Bruno's Intro without breaking, and then smoothly 
fades into the gameplay track when the real game starts. It also listens to the SettingsMenu
script to adjust the volume and mute the game based on the player's saved UI preferences.

Variables:
- Instance: Lets any other script trigger a sound with one simple line of code.
- musicSource & sfxSource: The actual Unity components that physically output the audio.
- menuMusic & gameplayMusic: The looping background tracks.
- buttonSFX & brunoDialogueSFX: The single-shot sound files for clicks and speech.
- globalVolume & isMuted: The master controls that the SettingsMenu slider and checkbox override.
- fadeDuration: Controls the math and timing for the crossfade transition.

How to set it up in Unity:
1. Open the Main Menu scene.
2. Create an empty GameObject and name it "AudioManager".
3. Attach this script to it.
4. Add two standard "Audio Source" components to that exact same object.
5. Drag those two Audio Sources into the 'Music Source' and 'Sfx Source' slots in the script.
6. Drag and drop the actual background tracks and SFX files into their matching slots in the Inspector.
*/
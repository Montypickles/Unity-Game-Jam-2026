using System.Collection;
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
    public AudioCLip buttonSFX;
    public AudioCLip brunoDialogueSFX;

    [Header("Settings")]
    public float fadeDuration = 1.0f; //How many seconds the fade will take
    private float defaultVolume;

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
        if (musicSource != null)
        {
            defaultVolume = musicSource.volume;
            PlayMenuMusic();
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
            musicSource.volume = Mathf.Lerp(0, defaultVolume, currentTime / fadeDuration);
            yield return null;
        }
        
        musicSource.volume = defaultVolume; // Ensure it snaps exactly to your target volume
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
fades into the gameplay track when the real game starts.

Variables:
- Instance: Lets any other script trigger a sound with one simple line of code.
- musicSource & sfxSource: The actual Unity components that physically output the audio.
- menuMusic & gameplayMusic: The looping background tracks.
- buttonSFX & brunoDialogueSFX: The single-shot sound files for clicks and speech.
- fadeDuration & defaultVolume: Controls the math and timing for the crossfade transition.

How to set it up in Unity:
1. Open the Main Menu scene.
2. Create an empty GameObject and name it "AudioManager".
3. Attach this script to it.
4. Add two standard "Audio Source" components to that exact same object.
5. Drag those two Audio Sources into the 'Music Source' and 'Sfx Source' slots in the script.
6. Drag and drop the actual background tracks and SFX files into their matching slots in the Inspector.
*/
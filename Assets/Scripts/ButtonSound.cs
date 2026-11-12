using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void PlayClick()
    {
        AudioManager.Instance?.PlayButtonSound();
    }

    public void PlayCheckBox()
    {
        AudioManager.Instance?.PlayBrunoSound();
    }
}
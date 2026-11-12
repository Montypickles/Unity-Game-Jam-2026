using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    public void PlayAnimationSound()
    {
        AudioManager.Instance?.PlayBrunoSound();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1.2f;

    private bool isTransitioning = false;

    private void Start()
    {
        //Ensure the screen is starting transparent when the scene first loads
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;
        }
    }

    public void PlayGame()
    {
        if (!isTransitioning)
        {
            StartCoroutine(FadeAndLoad());
        }
    }

    private IEnumerator FadeAndLoad()
    {
        isTransitioning = true;

        fadeImage.transform.SetAsLastSibling();

        float elapsedTime = 0f;
        Color color = fadeImage.color;

        //Gradually increases the black overlay's opacity (alpha)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        //making sure the screen is fully black before switching scenes.
        color.a = alpha;
        fadeImage.color = color;

        //Load game scene (MAKE SURE THIS SCENE IS ADDED TO THE BUILD SETTINGS!!!)
        SceneManager.LoadScene("GameScene");
    }
}
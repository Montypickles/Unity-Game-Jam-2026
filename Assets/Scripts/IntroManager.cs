using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject thoughtSpeechBox;
    public GameObject brunoSpeechBox;
    public Button continueButton;
    public TMP_Text brunoSpeechText;
    public TMP_Text continueButtonText;

    [Header("Bruno")]
    public Animator brunoAnimator;

    [Header("Dialogue")]
    [TextArea(2, 4)]
    public string[] brunoDialogue;

    private int dialogueIndex = 0;
    private bool brunoSpeaking = false;
    private bool transitioning = false;

    public void ContinueIntro()
    {
        if (transitioning) return;

        if (!brunoSpeaking)
        {
            StartCoroutine(BrunoEntranceSequence());
        }
        else
        {
            NextDialogue();
        }
    }

    private IEnumerator BrunoEntranceSequence()
    {
        transitioning = true;

        thoughtSpeechBox.SetActive(false);
        continueButton.interactable = false;

        yield return new WaitForSeconds(1f);

        brunoAnimator.Play("BrunoPopup", 0, 0f);

        yield return new WaitForSeconds(0.5f);

        brunoSpeechBox.SetActive(true);

        if (brunoDialogue.Length > 0)
        {
            brunoSpeechText.text = brunoDialogue[0];
            if (brunoDialogue.Length == 1)
            {
                continueButtonText.text = "Ready!";
            }
        }

        brunoSpeaking = true;
        transitioning = false;
        continueButton.interactable = true;
    }

    private void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < brunoDialogue.Length)
        {
            brunoSpeechText.text = brunoDialogue[dialogueIndex];

            if (dialogueIndex == brunoDialogue.Length - 1)
            {
                continueButtonText.text = "Ready!";
            }
        }
        else
        {
            SceneManager.LoadScene("Game State");
        }
    }
}

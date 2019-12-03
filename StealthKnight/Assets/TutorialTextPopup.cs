using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* In-game tutorial popup */
public class TutorialTextPopup : MonoSingleton<TutorialTextPopup>
{
    [SerializeField] private Text TutorialText;
    [SerializeField] private Animator TutorialAnimator;
    private float TimeoutCounter = 0.0f;

    /* Show tutorial popup */
    public void ShowText(string text)
    {
        TutorialText.text = text;
        TutorialAnimator.SetBool("ShowTutorial", true);
        TimeoutCounter = 0.0f;
    }

    /* Hide tutorial popup after time */
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShowText("Some demo tutorial text!");
        }
#endif

        if (TutorialAnimator.GetBool("ShowTutorial"))
        {
            TimeoutCounter += Time.deltaTime;
            if (TimeoutCounter >= 5.0f)
            {
                TutorialAnimator.SetBool("ShowTutorial", false);
            }
        }
    }
}

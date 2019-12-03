using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopupTriggerer : MonoBehaviour
{
    private List<string> TutorialTexts = new List<string>();
    private int CurrentTextIndex = 0;
    private float TutorialTextInterval = 7.0f;
    private float TimeSinceLastPopup = 0.0f;

    /* Set the possible tutorial text */
    private void Start()
    {
        TutorialTexts.Clear();
        TutorialTexts.Add("Break open exhibit cases and steal as many items as you can.");
        TutorialTexts.Add("The alarms will go off when you steal something, and guards will start chasing you.");
        TutorialTexts.Add("If the guards hit you, you'll drop the items you're carrying and lose health.");
        TutorialTexts.Add("You can only sprint when you have stamina.");
        TutorialTexts.Add("Press E to steal, and SPACE to break glass display cases.");
    }

    /* Show the possible tutorial text at intervals */
    void Update()
    {
        if (CurrentTextIndex >= TutorialTexts.Count) return;

        TimeSinceLastPopup += Time.deltaTime;
        if (TimeSinceLastPopup >= TutorialTextInterval)
        {
            TutorialTextPopup.Instance.ShowText(TutorialTexts[CurrentTextIndex]);
            TimeSinceLastPopup = 0.0f;
            CurrentTextIndex++;
        }
    }
}

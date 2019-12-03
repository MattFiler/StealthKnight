using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Portal particle controller */
public class EnableParticlesWhenItemPickedup : MonoBehaviour
{
    [SerializeField] private ParticleSystem Particle1;
    [SerializeField] private ParticleSystem Particle2;
    [SerializeField] private Inventory ScoreManager;
    private float TimeSinceStart = 0.0f;
    private bool DidShowFirstTutorialPopup = false;
    private bool DidTurnOffFirstTime = false;
    private bool DidStartShowingAgain = false;

    /* Show/hide portal */
    void Update()
    {
        //Show portal for 5 secs on start
        if (!DidTurnOffFirstTime)
        {
            TimeSinceStart += Time.deltaTime;
            if (TimeSinceStart >= 1.0f && !DidShowFirstTutorialPopup)
            {
                TutorialTextPopup.Instance.ShowText("Return to this portal with your artefacts to take them home!");
                DidShowFirstTutorialPopup = true;
            }
            if (TimeSinceStart >= 5.0f)
            {
                Particle1.Stop();
                Particle2.Stop();
                DidTurnOffFirstTime = true;
            }
        }

        //Show portal again once we have score
        if (!DidStartShowingAgain && ScoreManager.getScore() > 0)
        {
            Particle1.Play();
            Particle2.Play();
            DidStartShowingAgain = true;
        }
    }
}

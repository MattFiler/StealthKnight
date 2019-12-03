using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* In-game UI controller */
public class SK_UIController : MonoSingleton<SK_UIController>
{
    //more soundtrack stuff
    FMOD.Studio.EventInstance soundtrack;
    private void Start()
    {
        soundtrack = FMODUnity.RuntimeManager.CreateInstance("event:/environment/musicsneak");
        soundtrack.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.gameObject));
        soundtrack.start();
    }

    /* Handle button presses and backout timer for scene changing */
    void Update()
    {
        //shoving soundtrack here because fuck it
        if (IsGameOver)
        {
            soundtrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else
        {
            soundtrack.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.gameObject));
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (GetComponent<Animator>().GetBool("ShowPause")) BackToGame();
            else ShowPause(); 
        }

        if (!IsBackingOut) return;

        TimeSinceBackedOut += Time.deltaTime;
        if (TimeSinceBackedOut >= 1.0f)
        {
            SceneManager.LoadScene(0);
        }
    }

    /* Show the pause menu */
    public void ShowPause()
    {
        GetComponent<Animator>().SetBool("ShowPause", true);
    }

    /* Close the pause menu */
    public void BackToGame()
    {
        GetComponent<Animator>().SetBool("ShowPause", false);
    }

    /* Exit the game */
    private float TimeSinceBackedOut = 0.0f;
    private bool IsBackingOut = false;
    public void BackToMenu()
    {
        GetComponent<Animator>().SetBool("FadeOut", true);
        IsBackingOut = true;
    }

    /* Show the game over screen */
    [SerializeField] private Text WinLossText;
    [SerializeField] private Text WinLossFlavourText;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Inventory ScoreManager;
    FMOD.Studio.EventInstance sfx;
    public bool IsGameOver = false; //lazy
    public void ShowGameOver(bool didWin)
    {
        if (didWin && ScoreManager.getScore() == 0) return; //Ignore the call to this function if we have zero score (hacky fix!)
        IsGameOver = true;

        GetComponent<Animator>().SetBool("PortalOut", didWin); //We can only win if we've used the portal, so play the FX here
        GetComponent<Animator>().SetBool("ShowGameOver", true);
        ScoreText.text = ScoreManager.getScore().ToString();
        if (didWin) WinLossText.text = "VICTORY!";
        if (!didWin) WinLossText.text = "DEFEAT!";
        if (didWin) WinLossFlavourText.text = "YOU RETRIEVED YOUR ARTEFACTS AND ESCAPED THE GUARDS";
        if (!didWin) WinLossFlavourText.text = "THE GUARDS STOPPED YOU RETRIEVING YOUR ARTEFACTS";

        //sounds
        if (!sfx.isValid())
        {
            if (didWin)
            {
                sfx = FMODUnity.RuntimeManager.CreateInstance("event:/environment/musicwin");
            }
            else
            {
                sfx = FMODUnity.RuntimeManager.CreateInstance("event:/environment/musicloss");
            }
            sfx.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.gameObject));
            sfx.setVolume(0.2f);
            sfx.start();
        }
    }

    /* Go to next level */
    public void GoToNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(0);
            return;
        }
        SceneManager.LoadScene(2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* In-game UI controller */
public class SK_UIController : MonoSingleton<SK_UIController>
{
    /* Handle button presses and backout timer for scene changing */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            ShowPause();
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
    public void ShowGameOver(bool didWin)
    {
        GetComponent<Animator>().SetBool("ShowGameOver", true);
        ScoreText.text = ScoreManager.getScore().ToString();
        if (didWin) WinLossText.text = "VICTORY!";
        if (!didWin) WinLossText.text = "DEFEAT!";
        if (didWin) WinLossFlavourText.text = "YOU RETRIEVED YOUR ARTEFACTS AND ESCAPED THE GUARDS";
        if (!didWin) WinLossFlavourText.text = "THE GUARDS STOPPED YOU RETRIEVING YOUR ARTEFACTS";
    }

    /* Go to next level */
    public void GoToNextLevel()
    {
        Debug.Log("THERES NO OTHER LEVEL YET");
        //SceneManager.LoadScene(0); - TODO
    }
}

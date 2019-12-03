using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* In-game UI controller */
public class SK_UIController : MonoBehaviour
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
}

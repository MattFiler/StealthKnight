using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SK_UIController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            GetComponent<Animator>().SetBool("ShowPause", true);
        }

        if (!IsBackingOut) return;

        TimeSinceBackedOut += Time.deltaTime;
        if (TimeSinceBackedOut >= 1.0f)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void BackToGame()
    {
        GetComponent<Animator>().SetBool("ShowPause", false);
    }

    private float TimeSinceBackedOut = 0.0f;
    private bool IsBackingOut = false;
    public void BackToMenu()
    {
        GetComponent<Animator>().SetBool("FadeOut", true);
        IsBackingOut = true;
    }
}

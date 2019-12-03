using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNav : MonoBehaviour
{
    // Start is called before the first frame update
 
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
 
    }

   public void LevelSelected()
    {
        Debug.Log("Clicked");
    }
    public void QuitGame()
    {
       
        Application.Quit(); 
        Debug.Log("GET OUTTA HERE");
    }

    public void GameEnd()
    {
        SceneManager.LoadScene(0);
    }
}
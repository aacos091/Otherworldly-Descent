using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public GameObject pauseMenuUI;

    public void Quit() 
    {
        Application.Quit();
    }

    public void nextScene (string levelName) 
    {
        SceneManager.LoadScene(levelName);
    }

    public void RestartfromPauseMenu() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }
}

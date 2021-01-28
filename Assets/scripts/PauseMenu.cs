using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;

    public void update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("paused");
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumePlay()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}

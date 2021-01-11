using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPause;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPause = true;
    }
    public void Menu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        isPause = false;
    }
    public void Exit()
    {
        Application.Quit();
    }
}

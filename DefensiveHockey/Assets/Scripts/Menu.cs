using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void StartGame()
    {
        animator.SetBool("StartGame",true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Pause()
    {
        Game.gamePause = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Game.gamePause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}


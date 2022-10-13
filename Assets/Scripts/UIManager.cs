using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool isPaused;

    public GameObject pauseMenu;
    private void Start()
    {
        isPaused = false;
        
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
       

    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

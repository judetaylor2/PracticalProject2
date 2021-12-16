using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool isPaused;
    public GameObject pauseMenu, optionsMenu;
    
    void Start()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);    
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isPaused)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && isPaused)
        {
            UnPause();
        }  
    }
    
    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }
}

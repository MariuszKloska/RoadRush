using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseObj;
    public GameObject fpsObj;
    private float tempTimeScale;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                tempTimeScale = Time.timeScale;
            }
            PauseGame();
        }
    }

    private void PauseGame()
    {
        PauseObj.SetActive(!PauseObj.activeInHierarchy);
        fpsObj.SetActive(!fpsObj.activeInHierarchy);
        if(Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = tempTimeScale;
        }
    }

    public void ResumeButton()
    {
        PauseGame();
    }
    public void MenuExit()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;

    }
    public void ExitButton()
    {
        Application.Quit();        
    }
}

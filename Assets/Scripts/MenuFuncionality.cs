using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuFuncionality : MonoBehaviour
{
    public Light redLight;
    public Light blueLight;
    public float lightDelay;
    private float delay;

    public GameObject highscore;
    public GameObject mainmenu;
    public GameObject options;
    

    private void Start()
    {
        delay = lightDelay;
        redLight.enabled = true;
        blueLight.enabled = false;
        Time.timeScale = 1;

        if (PlayerPrefsX.GetIntArray("HighScoresArray", 0, 10)[0] == 0)
        {
            int[] highScoresInitializesionArray = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
            PlayerPrefsX.SetIntArray("HighScoresArray", highScoresInitializesionArray);
        }
    }

    private void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            redLight.enabled = !redLight.enabled;
            blueLight.enabled = !blueLight.enabled;
            delay = lightDelay;
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void StartAI()
    {
        SceneManager.LoadScene(2);
    }
    public void HiScore()
    {
        mainmenu.SetActive(false);
        highscore.SetActive(true);
    }

    public void Back()
    {
        mainmenu.SetActive(true);
        highscore.SetActive(false);
    }
    public void Options()
    {
        mainmenu.SetActive(false);
        options.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}

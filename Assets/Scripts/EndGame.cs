using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI gainedPointsText;
    public TextMeshProUGUI extraLiveBonusText;
    public TextMeshProUGUI noCollisionBonusText;
    public TextMeshProUGUI TotalScoreText;
    [Header("Points Value")]
    public int everyExtraLiveBonus;
    public int noCollisionBonus = 0;

    private GameObject GameMenager;
    private GameObject PlayerCar;

    private int score;
    private int[] highScoresArray = new int[10];

    private void Start()
    {
        highScoresArray = PlayerPrefsX.GetIntArray("HighScoresArray");
        gainedPointsText.text = PointsManager.points.ToString();
        GameMenager = GameObject.Find("Game Manager");
        extraLiveBonusText.text = (GameMenager.GetComponent<CarDurabilityManager>().lives * everyExtraLiveBonus).ToString();
        if((PlayerCar = GameObject.FindWithTag("Player")) != null)
        {
            if (PlayerCar.GetComponent<PlayerCarMovment>().durability == PlayerCar.GetComponent<PlayerCarMovment>().maxDurability && GameMenager.GetComponent<CarDurabilityManager>().lives == GameMenager.GetComponent<CarDurabilityManager>().maxLives)
            {
                noCollisionBonusText.text = noCollisionBonus.ToString();
            }
        }

        TotalScoreText.text = (int.Parse(gainedPointsText.text) + int.Parse(extraLiveBonusText.text) + int.Parse(noCollisionBonusText.text)).ToString();
        score = int.Parse(TotalScoreText.text);
        if (score > highScoresArray[9])
        {
            for (int i = 0; i < 10; i++)
            {
                if(score > highScoresArray[i])
                {
                    for(int j=9; j > i; j--)
                    {
                        highScoresArray[j] = highScoresArray[j-1];
                    }
                    highScoresArray[i]=score;
                    break;
                }
            }
        }

        PlayerPrefsX.SetIntArray("HighScoresArray", highScoresArray);

    }
    public void RetryButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void ExitMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}

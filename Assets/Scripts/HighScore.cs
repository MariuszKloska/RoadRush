using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI highScoresText;
    int[] highScoresArray = new int[10];

    private void Start()
    {
        highScoresArray = PlayerPrefsX.GetIntArray("HighScoresArray");
        if (highScoresArray[0] == 0)
        {
            highScoresText.text = "NO RECORDS!";
        }
        else
        {
            highScoresText.text = "";
            for(int i = 0; highScoresArray[i] !=0; i++)
            {
                highScoresText.text += (i + 1) + ". " + highScoresArray[i] + System.Environment.NewLine;
                if (i == 9)
                {
                    break;
                }
            }
        }
    }
}

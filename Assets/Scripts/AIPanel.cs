using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AIPanel : MonoBehaviour
{
    public TextMeshProUGUI iterationText;
    public TextMeshProUGUI decisinsText;
    public TextMeshProUGUI topDecisinsText;


    private void Update()
    {
        iterationText.text = "Iteration: " + PlayerCarMovment.itteration.ToString();
        decisinsText.text = "Decisions: " + PlayerCarMovment.mapStep.ToString();
        topDecisinsText.text = "Top decisions: " + PlayerCarMovment.decisionsBest.ToString();
    }
}

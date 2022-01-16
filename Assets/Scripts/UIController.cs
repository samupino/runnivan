using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text currentScoreUI;
    public Graphic endPanel;
    public Text endScore;


    public void HideGameOverScreen()
    {
        endPanel.gameObject.SetActive(false);
        currentScoreUI.gameObject.SetActive(true);
    }

    public void ShowGameOverScreen(int finalScore)
    {
        currentScoreUI.gameObject.SetActive(false);
        endScore.text = finalScore.ToString();
        endPanel.gameObject.SetActive(true);
    }

    public void UpdateLiveScore(int score)
    {
        currentScoreUI.text = score.ToString();
    }
}

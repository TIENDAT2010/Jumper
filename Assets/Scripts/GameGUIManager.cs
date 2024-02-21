using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGui;
    public GameObject gameGui;
    public TextMeshProUGUI scoreCountingText;
    public Image powerBarSlider;

    public Dialog achievementDialog;
    public Dialog helpDialog;
    public Dialog gameOverDialog;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void ShowGameGUI(bool isShow)
    {
        if(gameGui)
        {
            gameGui.SetActive(isShow);
        }    

        if(homeGui)
        {
            homeGui.SetActive(!isShow);
        }
    }    

    public void UpdateScoreCounting(int score)
    {
        if(scoreCountingText != null)
        {
            scoreCountingText.text = score.ToString();
        }
    }    

    public void UpdatePowerBar(float curVal, float totalVal)
    {
        if(powerBarSlider != null)
        {
            powerBarSlider.fillAmount = curVal / totalVal;
        }
    }    

    public void ShowAchievementDialog()
    {
        if(achievementDialog != null)
        {
            achievementDialog.Show(true);
        }
    }

    public void ShowHelpDialog()
    {
        if (helpDialog != null)
        {
            helpDialog.Show(true);
        }
    }

    public void ShowGameOverDialog()
    {
        if (gameOverDialog != null)
        {
            gameOverDialog.Show(true);
        }
    }
}
    

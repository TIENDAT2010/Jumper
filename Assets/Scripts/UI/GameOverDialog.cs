using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverDialog : Dialog
{
    public TextMeshProUGUI bestScoreText;
    private bool m_replayBtnClicked;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    public override void Show(bool isShow)
    {
        base.Show(isShow);

        if (bestScoreText != null)
        {
            bestScoreText.text = Prefs.bestScore.ToString();
        }
    }

    public void Replay()
    {
        m_replayBtnClicked = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void BacktoHome()
    {
        GameGUIManager.Ins.ShowGameGUI(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }    

    private void OnSceneLoad(Scene scene,LoadSceneMode mode)
    {
        if(m_replayBtnClicked)
        {
            GameGUIManager.Ins.ShowGameGUI(true);

            GameManager.Ins.PlayGame();
        }    

        SceneManager.sceneLoaded -= OnSceneLoad;
    }    
}

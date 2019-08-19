using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants;

public class MenuManager : MonoBehaviour
{
    public void NewGameBtn()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void RulesBtn()
    {
        SceneManager.LoadScene("RulesScene");
    }

    public void BackBtn()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void MainMenuBtn()
    {
        SceneManager.LoadScene("MainMenuScene");
        GameManager.instance = null;
        endGameScoreMap = new Dictionary<string, int>
        {
            { PLAYER_NAME, 0 },
            { BOT_NAME, 0 }
        };
    }
}

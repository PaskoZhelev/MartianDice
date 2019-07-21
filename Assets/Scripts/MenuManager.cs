using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class EndGameManager : MonoBehaviour
{
    public Image winImage;
    public Image loseImage;
    public Image drawImage;

    public Text playerName;
    public Text playerPoints;
    public Text botName;
    public Text botPoints;

    public Dictionary<string, int> scoreMap;
    // Start is called before the first frame update
    void Start()
    {
        scoreMap = endGameScoreMap;
        DetermineWinner();
    }

    private void DetermineWinner()
    {
        int playerPointsScore = scoreMap[PLAYER_NAME];
        int botPointsScore = scoreMap[BOT_NAME];
        if(playerPointsScore > botPointsScore)
        {
            winImage.enabled = true;
        } else if (playerPointsScore < botPointsScore)
        {
            loseImage.enabled = true;
        } else
        {
            drawImage.enabled = true;
        }
        playerName.text = PLAYER_NAME;
        botName.text = BOT_NAME;
        playerPoints.text = playerPointsScore.ToString();
        botPoints.text = botPointsScore.ToString();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class PlayerManager : MonoBehaviour
{
    public Player normalPlayerPrefab;
    public Player aiPlayerPrefab;

    [HideInInspector]
    public Player[] players { get; set; }
    private int numberPlayers;
    private bool hasAI;
    public bool isAIPlayerTurn;
    private int currentPlayer;

    public void GeneratePlayers()
    {
        currentPlayer = 1;
        hasAI = true;
        isAIPlayerTurn = false;
        numberPlayers = 2;
        players = new Player[numberPlayers];
        for (int i = 0; i < numberPlayers; i++)
        {
            if (i == 1 && hasAI)
            {
                players[i] = Instantiate(aiPlayerPrefab);
                players[i].name = "Bob";
            }
            else
            {
                players[i] = Instantiate(normalPlayerPrefab);
                players[i].name = "Player";
            }
        }
    }

    public void changeCurrentPlayer()
    {
        currentPlayer *= (-1);
        getCurrentPlayer().resetCurrentFields();
        isAIPlayerTurn = currentPlayer == BOT_ID ? true : false;        
    }

    public void addPointsForCurrentPlayer(int diceCount, int diceValue)
    {
       getCurrentPlayer().increaseCurrentPoints(diceCount, diceValue);
    }

    public Player getCurrentPlayer()
    {
        foreach (Player player in players)
        {
            if (player.id == currentPlayer)
            {
                return player;
            }
        }

        return null;
    }

    public Player getInactivePlayer()
    {
        foreach (Player player in players)
        {
            if (player.id != currentPlayer)
            {
                return player;
            }
        }

        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public DiceManager diceManager;
    public UIManager uiManager;
    public PlayerManager playerManager;
    public AIManager aiManager;

    [HideInInspector]
    public bool endGame;
    [HideInInspector]
    public static GameManager instance = null;

    
    // Start is called before the first frame update

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        } else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }          
    }

    void Start()
    {
        StartNewGame();   
    }

    public void StartNewGame()
    {
        endGame = false;
        diceManager.GenerateDice();
        uiManager.GenerateNewGameUI();       
        playerManager.GeneratePlayers();
        aiManager.Generate();
        updatePlayerNames();
        UpdatePlayerStats();
        uiManager.changeCurrentPlayerText(playerManager.getCurrentPlayer().id);
    }

    private void NewRound()
    {
        playerManager.changeCurrentPlayer();
        UpdatePlayerStats();
        uiManager.changeCurrentPlayerText(playerManager.getCurrentPlayer().id);
        diceManager.hideAllDice();
        diceManager.resetAvailableNumOfDice();
        diceManager.resetDicePositions();        
        uiManager.enableRollButton(); 
        if(playerManager.getCurrentPlayer().isAI)
        {
            aiManager.StartAITurn();
        }
    }

    private void UpdatePlayerStats()
    {
        uiManager.updatePlayersStats(playerManager.players);    
    }

    public void RollAllDice()
    {
        diceManager.RollAllDice();       
        uiManager.disableRollButton();
    }

    public void diceTouchEnabledUIChanges(string tag)
    {
        diceManager.makeTransparentDiceWithoutTag(tag);
        uiManager.showSelectBox();
    }

    public void diceTouchDisabledUIChanges()
    {
        diceManager.resetDiceTransparency();
        uiManager.hideSelectBox();
    }

    public void increasePointsForSelectedDice(int diceCount, int diceValue)
    {
        playerManager.addPointsForCurrentPlayer(diceCount, diceValue);     
    }

    public void EndTurn()
    {
        Player currPlayer = playerManager.getCurrentPlayer();
        currPlayer.increasePoints();
        if(currPlayer.points >= MAX_POINTS)
        {
            endGame = true;           
        }

        if (currPlayer.name == BOT_NAME && endGame)
        {
            endGameScoreMap[PLAYER_NAME] = playerManager.getInactivePlayer().points;
            endGameScoreMap[BOT_NAME] = currPlayer.points;
            SceneManager.LoadScene("EndGameScene");
        }
        NewRound();
    }

    private void updatePlayerNames()
    {
        foreach(Player player in playerManager.players)
        {
            if(player.id == PLAYER_ID)
            {
                uiManager.updatePlayerName(player.name);
            } else
            {
                uiManager.updateBotName(player.name);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class AIManager : MonoBehaviour
{
    PlayerManager playerManager;   
    DiceManager diceManager;
    UIManager uiManager;
    [HideInInspector]
    Player aiPlayer;
    public Dictionary<int, int> diceTypesMap;
    private bool endTurn;
    public const int INVALID_VALUE = -1;

    public void Generate()
    {
        diceTypesMap = new Dictionary<int, int>();
        endTurn = true;
        playerManager = GameManager.instance.playerManager;
        diceManager = GameManager.instance.diceManager;
        uiManager = GameManager.instance.uiManager;
        aiPlayer = playerManager.players[1];
    }

    public void StartAITurn()
    {
        endTurn = false;
        NewRound();                     
    }

    private void NewRound()
    {   
        if(endTurn)
        {
            GameManager.instance.EndTurn();
            return;
        }
        uiManager.disableRollButton();
        uiManager.disableEndTurnButton();
        Invoke("RollDice", 0.8f);
    }

    private void RollDice()
    {
        GameManager.instance.RollAllDice();
        Invoke("SelectBestDice", 3);
    }

    private void SelectBestDice()
    {
        constructDiceTypeMap();
        int tankDifference = aiPlayer.currentTanks - aiPlayer.currentDeathRays;
        int availableDice = diceManager.availableDice;

        // current tank difference should be larger than 2 to select death ray
        if (tankDifference > 2 && hasDice(DEATH_RAY_VALUE_3))
        {
            diceManager.SelectDice(DEATH_RAY_VALUE_3);
        } else
        {
            // find best die to select
            int bestDice = bestAvailableDiceValue();           
            if (bestDice != INVALID_VALUE)
            {
                Debug.Log("Best Die: " + diceValueMap[bestDice]);
                diceManager.SelectDice(bestDice);
            } else
            {
                // invalid number means that there are no dice to be selected
                // so turn must end
                endTurn = true;
            }
        }

        if(readyForEndRound())
        {
            endTurn = true;
        }

        NewRound();
    }

    private void constructDiceTypeMap()
    {
        diceTypesMap.Clear();
        foreach (Dice die in diceManager.dice)
        {               
            int value = die.value;
            if (value == INVALID_VALUE) continue;

            if(diceTypesMap.ContainsKey(value))
            {
                diceTypesMap[value] += 1;
            } else
            {
                diceTypesMap.Add(value, 1);
            }         
        }
    }

    private bool readyForEndRound()
    {
        int tankDifference = aiPlayer.currentTanks - aiPlayer.currentDeathRays;
        int availableDice = diceManager.availableDice;
        if (availableDice <= 0 || tankDifference > availableDice || ((availableDice <= 2 && (tankDifference <= 0 && tankDifference >= -1))))
        {
            return true;
        }
        return false;
    }

    private bool hasDice(int value)
    {
        return diceTypesMap.ContainsKey(value);
    }
  
    private int bestAvailableDiceValue()
    {
        int maxNum = 0;
        int currBestDie = INVALID_VALUE;
        foreach(var num in diceTypesMap)
        {
            int dieValue = num.Key;
            int numDice = num.Value;
            if (dieValue == 3) continue;

            if(maxNum < numDice && canSelectDice(dieValue))
            {
                currBestDie = dieValue;
                maxNum = numDice;
            }
        }

        if(currBestDie == INVALID_VALUE)
        {
            currBestDie = hasDice(DEATH_RAY_VALUE_3) ? DEATH_RAY_VALUE_3 : INVALID_VALUE;
        }
        return currBestDie;
    }

    private bool canSelectDice(int value)
    {
        switch (value)
        {
            case HUMAN_VALUE:
                return aiPlayer.currentHumans <= 0;
            case CHICKEN_VALUE:
                return aiPlayer.currentChickens <= 0;
            case COW_VALUE:
                return aiPlayer.currentCows <= 0;
            default:
                return false;
        }
    }
}

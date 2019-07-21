using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Player : ScriptableObject
{
    private static int BONUS_POINTS = 3;

    public new string name;
    public int points;
    public int id;
    public bool isAI;
    public int currentHumans;
    public int currentChickens;
    public int currentCows;
    public int currentDeathRays;
    public int currentTanks;

    public void resetCurrentFields()
    {
        currentHumans = 0;
        currentChickens = 0;
        currentCows = 0;
        currentDeathRays = 0;
        currentTanks = 0;
    }

    public void increasePoints()
    {
        if (currentDeathRays < currentTanks) return;

        points = points + currentHumans + currentChickens + currentCows;
        if(currentHumans != 0 && currentChickens != 0 && currentCows != 0)
        {
            points += BONUS_POINTS;
        }    
    }

    public void increaseCurrentPoints(int diceCount, int diceValue)
    {
        UIManager uiMan = GameManager.instance.uiManager;
        switch(diceValue)
        {           
            case 0:
                currentHumans += diceCount;
                uiMan.updateHumanPoints(currentHumans);
                break;
            case 1:
                currentChickens += diceCount;
                uiMan.updateChickenPoints(currentChickens);
                break;
            case 2:
                currentCows += diceCount;
                uiMan.updateCowPoints(currentCows);
                break;
            case 3:
            case 4:
                currentDeathRays += diceCount;
                uiMan.updateDeathRayPoints(currentDeathRays);
                break;
            default:
                currentTanks += diceCount;
                uiMan.updateTankPoints(currentTanks);
                break;
        }
    }
}

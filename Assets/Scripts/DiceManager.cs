using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class DiceManager : MonoBehaviour
{
    public Dice diePrefab;

    [HideInInspector]
    public List<Dice> dice { get; set; }
    public List<Vector2> dicePositions { get; set; }
    public int availableDice;

    public void GenerateDice()
    {
        availableDice = INITIAL_NUMBER_DICE;
        dice = new List<Dice>();
        dicePositions = new List<Vector2>();    
        dicePositions.Add(new Vector2(STARTING_DIE_X_POS, STARTING_DIE_Y_POS));
        dicePositions.Add(new Vector2(STARTING_DIE_X_POS + DICE_DISTANCE_X, STARTING_DIE_Y_POS));
        dicePositions.Add(new Vector2(STARTING_DIE_X_POS + (2 * DICE_DISTANCE_X), STARTING_DIE_Y_POS));
        dicePositions.Add(new Vector2(STARTING_DIE_X_POS + (3 * DICE_DISTANCE_X), STARTING_DIE_Y_POS));
        dicePositions.Add(new Vector2(STARTING_DIE_X_POS + (4 * DICE_DISTANCE_X), STARTING_DIE_Y_POS));

        dicePositions.Add(new Vector2(STARTING_DIE_X_POS, STARTING_DIE_Y_POS + ((-1.00f) * DICE_DISTANCE_Y)));
        dicePositions.Add(new Vector2(STARTING_DIE_X_POS + DICE_DISTANCE_X, STARTING_DIE_Y_POS + ((-1.00f) * DICE_DISTANCE_Y)));
        dicePositions.Add(new Vector2(STARTING_DIE_X_POS + (2 * DICE_DISTANCE_X), STARTING_DIE_Y_POS + ((-1.00f) * DICE_DISTANCE_Y)));
        dicePositions.Add(new Vector2(STARTING_DIE_X_POS + (3 * DICE_DISTANCE_X), STARTING_DIE_Y_POS + ((-1.00f) * DICE_DISTANCE_Y)));
        dicePositions.Add(new Vector2(STARTING_DIE_X_POS + (4 * DICE_DISTANCE_X), STARTING_DIE_Y_POS + ((-1.00f) * DICE_DISTANCE_Y)));

        dicePositions.Add(new Vector2(STARTING_DIE_X_POS + DICE_DISTANCE_X, STARTING_DIE_Y_POS + ((-1.00f) * (2 * DICE_DISTANCE_Y))));
        dicePositions.Add(new Vector2(STARTING_DIE_X_POS + (2 * DICE_DISTANCE_X), STARTING_DIE_Y_POS + ((-1.00f) * (2 * DICE_DISTANCE_Y))));
        dicePositions.Add(new Vector2(STARTING_DIE_X_POS + (3 * DICE_DISTANCE_X), STARTING_DIE_Y_POS + ((-1.00f) * (2 * DICE_DISTANCE_Y))));

        for (int i = 0; i < availableDice; i++)
        {
            dice.Add(Instantiate(diePrefab, dicePositions[i], Quaternion.identity));
        }

    }

    public void RollAllDice()
    {
        if (availableDice <= 0) return;
        resetDice();

        int[] numbers = generateRandomDiceValues(availableDice);
        Array.Sort(numbers);               
        for (int i = 0; i < availableDice; i++)
        {
            dice[i].RollDieAndSetValue(numbers[i]);          
        }              
    }
    
    private void resetDice()
    {
        makeAllDiceAvailable();
        resetDiceTransparency();
        makeActiveDiceVisible();
    }

    public void makeTransparentDiceWithoutTag(string tag)
    {
        foreach (Dice die in dice)
        {
            if(die.tag != tag)
            {
                die.enableTransparency();
            }
        }
    }

    public void hideAllDice()
    {
        foreach (Dice die in dice)
        {
            die.hideDie();               
        }
    }

    public void makeActiveDiceVisible()
    {
        int count = 1;
        foreach (Dice die in dice)
        {
            if (count <= availableDice)
            {
                die.showDie();
                count++;
            } else
            {
                die.hideDie();
                die.value = -1;
            }
        }
    }

    public void resetDicePositions()
    {
        for (int i = 0; i < INITIAL_NUMBER_DICE; i++)
        {
            dice[i].transform.position = dicePositions[i];
        }
    }

    public void resetDiceTransparency()
    {
        foreach(Dice die in dice) {
            if(die.locked == false && die.available)
            {
                die.resetTransparancy();
            }          
        }
    }
    
    public void SelectDice(int value)
    {
        int count = 0;
        foreach (Dice die in dice)
        {
            if (die.value == value)
            {
                count++;               
            }
        }

        //Increase Points, decrease avail. dice, hide selected dice, unlock roll btn
        GameManager.instance.increasePointsForSelectedDice(count, value);       
        decreaseAvailableDice(count);
        hideAllDice();
        GameManager.instance.uiManager.enableEndTurnButton();

        if (availableDice > 0)
        {
            GameManager.instance.uiManager.enableRollButton();
        }
    }

    public void resetAvailableNumOfDice()
    {
        availableDice = INITIAL_NUMBER_DICE;
    }

    public void decreaseAvailableDice(int num)
    {
        availableDice -= num;
        if(availableDice < 0)
        {
            availableDice = 0;
        }
        GameManager.instance.uiManager.updateDiceNum(availableDice);
    }

    private int[] generateRandomDiceValues(int count)
    {
        int[] numbers = new int[count];
        for (int i = 0; i < count; i++)
        {
            numbers[i] = UnityEngine.Random.Range(0, DICE_SIDES_NUM);
        }
        return numbers;
    }

    public void makeAllDiceAvailable()
    {
        foreach (Dice die in dice)
        {
            die.available = true;
        }
    }

    public void enableAllDice()
    {
        foreach (Dice die in dice)
        {
            die.locked = false;
        }
    }

    public void disableAllDice()
    {
        foreach(Dice die in dice)
        {
            die.locked = true;
        }
    }

   
}


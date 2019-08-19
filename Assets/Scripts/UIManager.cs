using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class UIManager : MonoBehaviour
{
    public Button rollButtonPrefab;
    public Button endTurnButtonPrefab;
    public SelectBox selectBoxPrefab;
    public GameObject humanScorePrefab;
    public GameObject chickenScorePrefab;
    public GameObject cowScorePrefab;
    public GameObject deathRayScorePrefab;
    public GameObject tankScorePrefab;
    public Text playerNamePrefab;
    public Text playerPointsPrefab;
    public Text diceTextPrefab;
    public Text diceNumPrefab;

    private Text playerName;
    private Text playerPoints;
    private Text botName;
    private Text botPoints;
    private Text diceText;
    private Text diceNum;
    private Button rollButton;
    private Button endTurnButton;
    private SelectBox selectBox;
    private GameObject humanScore;
    private GameObject chickenScore;
    private GameObject cowScore;
    private GameObject deathRayScore;
    private GameObject tankScore;

    private Text humanPoints;
    private Text chickenPoints;
    private Text cowPoints;
    private Text deathRayPoints;
    private Text tankPoints;
    public static Canvas canvas;


    public void GenerateNewGameUI()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        rollButton = Instantiate(rollButtonPrefab, new Vector2(ROLL_BTN_X_POS, ROLL_BTN_Y_POS), Quaternion.identity) as Button;
        rollButton.transform.SetParent(canvas.transform, false);
        endTurnButton = Instantiate(endTurnButtonPrefab, new Vector2(END_BTN_X_POS, END_BTN_Y_POS), Quaternion.identity) as Button;
        endTurnButton.transform.SetParent(canvas.transform, false);
        playerName = Instantiate(playerNamePrefab, new Vector2(PLAYER_NAME_X, PLAYER_NAME_Y), Quaternion.identity) as Text;
        playerName.transform.SetParent(canvas.transform, false);
        playerPoints = Instantiate(playerPointsPrefab, new Vector2(PLAYER_POINTS_X, PLAYER_POINTS_Y), Quaternion.identity) as Text;
        playerPoints.transform.SetParent(canvas.transform, false);
        botName = Instantiate(playerNamePrefab, new Vector2(BOT_NAME_X, BOT_NAME_Y), Quaternion.identity) as Text;
        botName.transform.SetParent(canvas.transform, false);
        botPoints = Instantiate(playerPointsPrefab, new Vector2(BOT_POINTS_X, BOT_POINTS_Y), Quaternion.identity) as Text;
        botPoints.transform.SetParent(canvas.transform, false);
        diceText = Instantiate(diceTextPrefab, new Vector2(DICE_TEXT_X_POS, DICE_TEXT_Y_POS), Quaternion.identity) as Text;
        diceText.transform.SetParent(canvas.transform, false);
        diceNum = Instantiate(diceNumPrefab, new Vector2(DICE_NUM_X_POS, DICE_NUM_Y_POS), Quaternion.identity) as Text;
        diceNum.transform.SetParent(canvas.transform, false);

        humanScore = Instantiate(humanScorePrefab, new Vector2(SCORE_COUNTER_UP_X_POS, SCORE_COUNTER_UP_Y_POS), Quaternion.identity) as GameObject;
        humanScore.transform.SetParent(canvas.transform, false);
        chickenScore = Instantiate(chickenScorePrefab, new Vector2(SCORE_COUNTER_UP_X_POS * (-1), SCORE_COUNTER_UP_Y_POS), Quaternion.identity) as GameObject;
        chickenScore.transform.SetParent(canvas.transform, false);
        cowScore = Instantiate(cowScorePrefab, new Vector2(360, SCORE_COUNTER_UP_Y_POS), Quaternion.identity) as GameObject;
        cowScore.transform.SetParent(canvas.transform, false);
        deathRayScore = Instantiate(deathRayScorePrefab, new Vector2(SCORE_COUNTER_DOWN_X_POS - 115, SCORE_COUNTER_DOWN_Y_POS), Quaternion.identity) as GameObject;
        deathRayScore.transform.SetParent(canvas.transform, false);
        tankScore = Instantiate(tankScorePrefab, new Vector2(120, SCORE_COUNTER_DOWN_Y_POS), Quaternion.identity) as GameObject;
        tankScore.transform.SetParent(canvas.transform, false);

        selectBox = Instantiate(selectBoxPrefab, new Vector2(SELECT_BOX_X_POS, SELECT_BOX_Y_POS), Quaternion.identity) as SelectBox;
        hideSelectBox();
        generatePointsTextFields();
        disableEndTurnButton();
    }

    public void enableRollButton()
    {
        rollButton.interactable = true;
    }

    public void disableRollButton()
    {
        rollButton.interactable = false;
    }

    public void enableEndTurnButton()
    {
        endTurnButton.interactable = true;
    }

    public void disableEndTurnButton()
    {
        endTurnButton.interactable = false;
    }

    public void showSelectBox()
    {
        selectBox.showSelectBox();
    }

    public void hideSelectBox()
    {
        selectBox.hideSelectBox();
    }

    private void generatePointsTextFields()
    {
        humanPoints = humanScore.GetComponentInChildren<Text>();
        chickenPoints = chickenScore.GetComponentInChildren<Text>();
        cowPoints = cowScore.GetComponentInChildren<Text>();
        deathRayPoints = deathRayScore.GetComponentInChildren<Text>();
        tankPoints = tankScore.GetComponentInChildren<Text>();
    }

    public void updatePlayersStats(Player[] players)
    {
        foreach(Player player in players)
        {
            if(player.id == PLAYER_ID)
            {
                updatePlayerPoints(player.points);
            } else
            {
                updateBotPoints(player.points);
            }
        }
        resetCurrentPoints();
    }

    private void resetCurrentPoints()
    {
        updateHumanPoints(DEFAULT_ZERO_VALUE);
        updateChickenPoints(DEFAULT_ZERO_VALUE);
        updateCowPoints(DEFAULT_ZERO_VALUE);
        updateDeathRayPoints(DEFAULT_ZERO_VALUE);
        updateTankPoints(DEFAULT_ZERO_VALUE);
        updateDiceNum(INITIAL_NUMBER_DICE);
    }

    public void updateChickenPoints(int value)
    {
        chickenPoints.text = value.ToString();
    }

    public void updateCowPoints(int value)
    {
        cowPoints.text = value.ToString();
    }

    public void updateDeathRayPoints(int value)
    {
        deathRayPoints.text = value.ToString();
    }

    public void updateHumanPoints(int value)
    {
        humanPoints.text = value.ToString();
    }

    public void updateTankPoints(int value)
    {
        tankPoints.text = value.ToString();
    }

    public void updatePlayerName(string name)
    {
        playerName.text = name.ToString();
    }

    public void updatePlayerPoints(int value)
    {
        playerPoints.text = value.ToString();
    }

    public void updateBotName(string name)
    {
        botName.text = name.ToString();
    }

    public void updateBotPoints(int value)
    {
        botPoints.text = value.ToString();
    }

    public void updateDiceNum(int value)
    {
        diceNum.text = value.ToString();
    }

    public void changeCurrentPlayerText(int activePlayerId)
    {
        if(activePlayerId == PLAYER_ID)
        {
            //player active
            playerName.color = DEFAULT_COLOR;
            playerName.fontSize = ACTIVE_PLAYER_FONT_SIZE;
            playerPoints.color = DEFAULT_PLAYER_POINTS_COLOR;

            botName.color = TRANSPARENT_COLOR;
            botName.fontSize = INACTIVE_PLAYER_FONT_SIZE;
            botPoints.color = TRANSPARENT_PLAYER_POINTS_COLOR;
        }
        else
        {
            //bot active
            botName.color = DEFAULT_COLOR;
            botName.fontSize = ACTIVE_PLAYER_FONT_SIZE;
            botPoints.color = DEFAULT_PLAYER_POINTS_COLOR;

            playerName.color = TRANSPARENT_COLOR;
            playerName.fontSize = INACTIVE_PLAYER_FONT_SIZE;
            playerPoints.color = TRANSPARENT_PLAYER_POINTS_COLOR;
        }
    }
}

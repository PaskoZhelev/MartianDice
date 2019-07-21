using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class Constants
{
    public const float STARTING_DIE_X_POS = -2.01f;
    public const float STARTING_DIE_Y_POS = -0.96f;
    public const float DICE_DISTANCE_X = 1.00f;
    public const float DICE_DISTANCE_Y = 1.00f;
    public const int DICE_SIDES_NUM = 6;
    public const float ROLL_BTN_X_POS = 0.00f;
    public const float ROLL_BTN_Y_POS = -662.00f;
    public const float END_BTN_X_POS = 230.00f;
    public const float END_BTN_Y_POS = 412.00f;
    public const float SELECT_BOX_X_POS = 0.00f;
    public const float SELECT_BOX_Y_POS = 0.90f;
    public const float SCORE_COUNTER_UP_X_POS = -120.00f;
    public const float SCORE_COUNTER_UP_Y_POS = 365.00f;
    public const float SCORE_COUNTER_DOWN_X_POS = -07.00f;
    public const float SCORE_COUNTER_DOWN_Y_POS = 265.00f;
    public const float PLAYER_NAME_X = -191.00f;
    public const float PLAYER_NAME_Y = 601.00f;
    public const float PLAYER_POINTS_X = -68.00f;
    public const float PLAYER_POINTS_Y = 594.00f;
    public const float BOT_NAME_X = 121.00f;
    public const float BOT_NAME_Y = 601.00f;
    public const float BOT_POINTS_X = 242.00f;
    public const float BOT_POINTS_Y = 594.00f;
    public const float DICE_TEXT_X_POS = -20.00f;
    public const float DICE_TEXT_Y_POS = -770.00f;
    public const float DICE_NUM_X_POS = 55.00f;
    public const float DICE_NUM_Y_POS = -770.00f;
    public static Color DEFAULT_COLOR = new Color(1f, 1f, 1f, 1f);
    public static Color TRANSPARENT_COLOR = new Color(1f, 1f, 1f, .55f);
    public static Color DEFAULT_PLAYER_POINTS_COLOR = new Color(0.97f, 0.78f, 0.12f, 1f);
    public static Color TRANSPARENT_PLAYER_POINTS_COLOR = new Color(0.97f, 0.78f, 0.12f, 0.5f);
    
    public static Vector2 TANK_DICE_POSITION = new Vector2(0f, 2.3f);
    public const int HUMAN_VALUE = 0;
    public const int CHICKEN_VALUE = 1;
    public const int COW_VALUE = 2;
    public const int DEATH_RAY_VALUE_3 = 3;
    public const int DEATH_RAY_VALUE_4 = 4;
    public const int TANK_VALUE = 5;
    public const int INITIAL_NUMBER_DICE = 13;
    public const int DEFAULT_ZERO_VALUE = 0;
    public const int PLAYER_ID = 1;
    public const int BOT_ID = -1;
    public const int ACTIVE_PLAYER_FONT_SIZE = 55;
    public const int INACTIVE_PLAYER_FONT_SIZE = 45;
    public const int MAX_POINTS = 25;

    public static Canvas canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();

    public const string DICE_DEFAULT_SORTING_LAYER = "DiceDefaultLayer";
    public const string DICE_TOP_SORTING_LAYER = "DiceTopLayer";

    public const string HUMAN = "Human";
    public const string CHICKEN = "Chicken";
    public const string COW = "Cow";
    public const string DEATH_RAY = "DeathRay";
    public const string TANK = "Tank";
    public const string PLAYER_NAME = "Player";
    public const string BOT_NAME = "Bob";

    public static Dictionary<string, int> endGameScoreMap
    = new Dictionary<string, int>
    {
        { PLAYER_NAME, 0 },
        { BOT_NAME, 0 }
    };

    public static readonly Dictionary<int, string> diceValueMap
    = new Dictionary<int, string>
    {
        { 0, HUMAN },
        { 1, CHICKEN },
        { 2, COW },
        { 3, DEATH_RAY },
        { 4, DEATH_RAY },
        { 5, TANK }
    };
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class Dice : MonoBehaviour
{
    public Sprite[] diceSides;

    PlayerManager playerManager;
    DiceManager diceManager;

    private new SpriteRenderer renderer;
    private Vector2 initialPosition;
    private Vector2 cursorPosition;
    private float deltaX;
    private float deltaY;
    public Transform dropPosition;
    public int value { get; set; }
    public bool locked { get; set; }
    public bool available { get; set; }

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = diceSides[0];
        transform.SetParent(canvas.transform);
        value = 0;
        locked = false;
        available = true;
        playerManager = GameManager.instance.playerManager;
        diceManager = GameManager.instance.diceManager;
    }

    private void OnMouseDown()
    {
        if(!locked && available && !playerManager.isAIPlayerTurn)
        {
            initialPosition = transform.position;
            renderer.sortingLayerName = DICE_TOP_SORTING_LAYER;
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            GameManager.instance.diceTouchEnabledUIChanges(tag);
        }
    }

    private void OnMouseDrag()
    {
        if (!locked && available && !playerManager.isAIPlayerTurn)
        {
            cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPosition.x - deltaX, cursorPosition.y - deltaY);
        }
    }

    private void OnMouseUp()
    {
        if (!locked && available && !playerManager.isAIPlayerTurn)
        {
            if(Mathf.Abs(transform.position.x - dropPosition.position.x) <= 1.0 &&
                Mathf.Abs(transform.position.y - dropPosition.position.y) <= 1.0)
            {
                //Dice are in the SelectBox Area
                diceManager.SelectDice(value);            
            } 
                
            //Return dice on the initial position
            transform.position = new Vector2(initialPosition.x, initialPosition.y);           
            renderer.sortingLayerName = DICE_DEFAULT_SORTING_LAYER;
            GameManager.instance.diceTouchDisabledUIChanges();
        }
    }

    public void RollDieAndSetValue(int value)
    {
        StartCoroutine(RollDieCoroutine(value));
    }

    private IEnumerator RollDieCoroutine(int value)
    {
        float countDown = 0.3f;
        float speed = 2500f;
        for (int i = 0; i < 10; i++)
        {
            while (countDown >= 0)
            {
                transform.Rotate(Vector3.back, speed * Time.deltaTime);
                countDown -= Time.deltaTime;
                yield return null;
            }
        }

        setSide(value);
    }

    private void setSide(int side)
    {   
        // 3s and 4s are the same - Death Rays
        value = (side == 4) ? 3 : side;
        renderer.sprite = diceSides[value];
        tag = getDieStringValue();
        transform.rotation = Quaternion.identity;       
        if (value == TANK_VALUE)
        {
            locked = true;
            enableTransparency();        

            //TANK DICE are moved to the upper corner and avail. dice are decreased
            value = -1;
            StartCoroutine(MoveToPosition(transform, TANK_DICE_POSITION, 0.9f));
            
            diceManager.decreaseAvailableDice(1);
            diceManager.disableAllDice();
            return;
        }
        if (value == HUMAN_VALUE)
        {
            checkForExistingDice(playerManager.getCurrentPlayer().currentHumans);
            return;
        }
        if(value == CHICKEN_VALUE)
        {
            checkForExistingDice(playerManager.getCurrentPlayer().currentChickens);
            return;
        }
        if (value == COW_VALUE)
        {
            checkForExistingDice(playerManager.getCurrentPlayer().currentCows);
            return;
        }
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        yield return new WaitForSeconds(0.4f);
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        hideDie();
        GameManager.instance.increasePointsForSelectedDice(1, value);
        GameManager.instance.diceManager.enableAllDice();
    }

    public void enableTransparency()
    {
        renderer.color = TRANSPARENT_COLOR;    
    }

    public void resetTransparancy()
    {
        renderer.color = DEFAULT_COLOR;
    }

    private string getDieStringValue()
    {
        return diceValueMap[value];
    }

    public void showDie()
    {
        gameObject.SetActive(true);
    }

    public void hideDie()
    {
        gameObject.SetActive(false);
    }

    private void checkForExistingDice(int currentPoints)
    {
        if (currentPoints > 0)
        {
            available = false;
            enableTransparency();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public enum GameState { START, PLAYING, SUCCESS, FAIL}
public class SupermarketManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] LayerMask ingredientsLayer;

    [Header("UI")]
    [SerializeField] GameObject cart;
    [SerializeField] GameObject countdownTimer;

    public GameState state;

    public int ingredientsFound;

    public static SupermarketManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        InputTouch.OnFingerDown += CheckForIngredient;
    }

    private void OnDisable()
    {
        InputTouch.OnFingerDown -= CheckForIngredient;
    }

    private void Start()
    {
        state = GameState.START;
        StartCoroutine(StartCountdownTimer(5f));
    }

    
    void CheckForIngredient(Vector3 fingerPosition)
    {

        if (state == GameState.PLAYING)
        {
            GroceryIngredientBlueprint ingredientBlueprint = Physics2D.OverlapPoint(fingerPosition, ingredientsLayer).GetComponent<GroceryIngredientBlueprint>();
            if (ingredientBlueprint != null)
            {
                if (!ingredientBlueprint.ingredientItem.ExtraItem)
                {
                    ingredientBlueprint.touched(cart, speed);
                    ingredientsFound++;
                }
                else
                {
                    SupermarketTimer.OnWrongTouch?.Invoke(fingerPosition);
                }
            }
        }
    }


    IEnumerator StartCountdownTimer(float seconds)
    {
        TextMeshProUGUI timerText = countdownTimer.GetComponent<TextMeshProUGUI>();

        while(seconds != 0)
        {
            yield return new WaitForSeconds(1f);
            timerText.text = seconds.ToString();
            seconds--;
            
        }
        
        yield return new WaitForSeconds(1f);
        timerText.text = "Start";
        yield return new WaitForSeconds(1f);
        countdownTimer.SetActive(false);
        state = GameState.PLAYING;
    }
}

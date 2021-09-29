using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public enum GameState { START, PLAYING, SUCCESS, FAIL}
public class SupermarketManager : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] LayerMask ingredientsLayer;
    [SerializeField] GameObject cart;

    [SerializeField] GameObject success, timeUp;
    [SerializeField] GameObject resultsPanel;
    [SerializeField] GameObject[] stars;
    [SerializeField] TextMeshProUGUI resultText;

    [SerializeField] GameObject countdownTimer;

    public static Action OnFinish;

    public static GameState state;

    private int ingredientsFound;

    public static Action<string[]> setIngredientsName;

    [SerializeField] Text ingredientText;

    private void OnEnable()
    {
        InputTouch.OnFingerDown += CheckForIngredient;
        setIngredientsName += SetIngredients;
        OnFinish += Results;
    }

    private void OnDisable()
    {
        InputTouch.OnFingerDown -= CheckForIngredient;
        setIngredientsName -= SetIngredients;
        OnFinish -= Results;
    }

    private void Start()
    {
        state = GameState.START;
        StartCoroutine(StartCountdownTimer(5f));
    }
    void Results()
    {
        if (state == GameState.SUCCESS)
        {
            success.SetActive(true);
        }
        else if(state == GameState.FAIL)
        {
            timeUp.SetActive(true);
        }
        StartCoroutine(resultsScreen());
    }

    IEnumerator resultsScreen()
    {
        yield return new WaitForSeconds(3f);

        resultsPanel.SetActive(true);

        if (state == GameState.SUCCESS)
            resultText.text = "Perfect!";
        else if (state == GameState.FAIL)
            resultText.text = "Ingredients Found: " + ingredientsFound;

        foreach (GameObject i in stars)
        {
            i.SetActive(true);
            yield return new WaitForSeconds(.66f);
        }
    }
    
    void CheckForIngredient(Vector3 fingerPosition)
    {
        if(state == GameState.PLAYING)
        {
            Collider2D ingredientCollider = Physics2D.OverlapPoint(fingerPosition, ingredientsLayer);
            if (ingredientCollider != null)
            {
                ingredientCollider.GetComponent<IngredientController>().touched(cart, speed);
                ingredientText.text = ingredientText.text.Replace(ingredientCollider.GetComponent<IngredientController>().ingredientName + "       ", "");
                ingredientsFound++;

                if (string.IsNullOrWhiteSpace(ingredientText.text))
                {
                    state = GameState.SUCCESS;
                    OnFinish?.Invoke();
                }
            }
            else
                SupermarketTimer.OnWrongTouch?.Invoke(fingerPosition);
        }
    }

    public void SetIngredients(string[] ingredientsName)
    {
        foreach(string i in ingredientsName)
        {
            ingredientText.text += i;
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

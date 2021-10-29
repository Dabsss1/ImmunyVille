using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SupermarketTimer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int minute;
    [SerializeField] int seconds;
    [SerializeField] int timePenaltyInSeconds;

    public static Action<Vector3> OnWrongTouch;

    [Header("UI")]
    [SerializeField] GameObject timePenaltyGO;
    [SerializeField] Transform canvas;

    private float timer;

    [SerializeField] TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        UpdateTimerText();
    }

    private void OnEnable()
    {
        OnWrongTouch += timePenalty;
    }

    private void OnDisable()
    {
        OnWrongTouch -= timePenalty;
    }

    void timePenalty(Vector3 fingerPosition)
    {
        StartCoroutine(showPenaltyText(fingerPosition));
        if (seconds >= timePenaltyInSeconds)
            seconds -= timePenaltyInSeconds;
        else if(minute == 0 && seconds < timePenaltyInSeconds)
        {

            SupermarketManager.Instance.state = GameState.FAIL;
            IngredientsManager.Instance.ingredientPoolPanel.SetActive(false);
            GroceryResults.Instance.ShowResults();
            seconds = 0;
            UpdateTimerText();
        }
        else
        {
            minute--;
            int temp = timePenaltyInSeconds - seconds;
            seconds = 60 - temp;
        }
    }

    IEnumerator showPenaltyText(Vector3 fingerPosition)
    {
        fingerPosition.z = 0;
        GameObject timePenaltyClone = Instantiate(timePenaltyGO,fingerPosition, Quaternion.identity, canvas);
        yield return new WaitForSeconds(1f);
        Destroy(timePenaltyClone);
    }

    // Update is called once per frame
    void Update()
    {
        if (SupermarketManager.Instance.state == GameState.PLAYING)
        {
            if (minute < 0)
                return;

            if (minute == 0 && seconds == 0)
                return;

            DecreaseTime();

            UpdateTimerText();
            if (minute == 0 && seconds == 0)
            {
                SupermarketManager.Instance.state = GameState.FAIL;
                IngredientsManager.Instance.ingredientPoolPanel.SetActive(false);
                GroceryResults.Instance.ShowResults();
            }
        }
        
    }

    void UpdateTimerText()
    {
        timerText.text = $"{minute:00}:{seconds:00}";
    }

    void DecreaseTime()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (seconds <= 0)
            {
                minute -= 1;
                seconds = 59;
            }
            else
            {
                seconds--;
            }
            timer = 1;
        }
    }
}
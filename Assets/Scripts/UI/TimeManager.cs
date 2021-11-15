using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged, OnHourChanged;
    public static Action OnDayChanged;
    public static Action OnMidnight;

    public int minute;
    public int hour;
    public int day;

    public bool raining;

    public string season = "Dry";

    public static TimeManager Instance {get; private set;}

    [SerializeField] private float realtimeSecondsToIngameMinute = 1f;
    private float timer;

    [SerializeField] Color nightTimeColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayTimeColor;

    [SerializeField] Light2D globalLight;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
            Instance = this;
    }

    private void OnEnable()
    {
        //StreetlightController.TurnOnLights += 
    }

    private void Update()
    {
        if (GameStateManager.Instance.EqualsState(OpenWorldState.EXPLORE) && GameStateManager.Instance.EqualsState(SceneState.OPENWORLD))
            UpdateTime();

        if (!GameStateManager.Instance.EqualsState(SceneState.OPENWORLD))
            globalLight.color = dayTimeColor;
        else if (!SceneInitiator.Instance.outdoor)
            globalLight.color = dayTimeColor;
        else if (raining)
            globalLight.color = nightTimeColor;
        else
        {
            UpdateGlobalLight();

            if (StreetlightController.Instance != null)
            {
                if (hour >= StreetlightController.Instance.timeOn)
                    StreetlightController.Instance.gameObject.SetActive(true);
                else
                    StreetlightController.Instance.gameObject.SetActive(false);
            }
            
        }
            
    }

    void UpdateGlobalLight()
    {
        float hoursInFloat = (float) hour + ((float)minute/60);
        float v = nightTimeCurve.Evaluate(hoursInFloat);
        Color c = Color.Lerp(nightTimeColor, dayTimeColor, v);
        globalLight.color = c;
    }

    void UpdateTime()
    {
        timer += Time.deltaTime;

        if (timer >= realtimeSecondsToIngameMinute)
        {
            minute++;
            OnMinuteChanged?.Invoke();
            if (minute >= 60)
            {
                minute = 0;
                hour++;
                OnHourChanged?.Invoke();


                if (hour == 24)
                {
                    hour = 0;
                    day++;
                    OnDayChanged?.Invoke();

                    if (day > 30)
                    {
                        day = 1;
                        if (season == "Dry")
                            season = "Rainy";
                        else
                            season = "Dry";
                    }
                }
            }
            timer = 0;
        }

        if (hour >= 23)
            OnMidnight?.Invoke();
    }
    public void SetRain()
    {
        if (season == "Dry")
        {
            int rnd = UnityEngine.Random.Range(0,100);
            if (rnd < 10)
                raining = true;
            else
                raining = false;
        }
        else
        {
            int rnd = UnityEngine.Random.Range(0, 100);
            if (rnd < 60)
                raining = true;
            else
                raining = false;
        }
    }

    public void ResetData()
    {
        hour = 6;
        minute = 0;
        day = 1;
        season = "Dry";
    }
}

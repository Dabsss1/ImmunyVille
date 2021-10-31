using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged, OnHourChanged;
    public static Action OnDayChanged;

    public int minute;
    public int hour;
    public int day;

    public string season = "Dry";

    public static TimeManager Instance {get; private set;}

    [SerializeField] private float secondToOneMinuteRT = 1f;
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

    private void Update()
    {
        if (GameStateManager.Instance.EqualsState(OpenWorldState.EXPLORE) && GameStateManager.Instance.EqualsState(SceneState.OPENWORLD))
            UpdateTime();
        if (!GameStateManager.Instance.EqualsState(SceneState.OPENWORLD))
            globalLight.color = dayTimeColor;
        else
            UpdateGlobalLight();
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
        timer -= Time.deltaTime;
        

        if (timer <= 0)
        {
            minute++;
            OnMinuteChanged?.Invoke();
            if (minute >= 60)
            {
                hour++;
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
                OnHourChanged?.Invoke();
                minute = 0;
            }

            timer = secondToOneMinuteRT;
        }
    }
}

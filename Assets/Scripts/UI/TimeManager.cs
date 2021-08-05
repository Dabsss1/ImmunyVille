using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged, OnHourChanged;
    public static Action OnDayChanged;
    public static int minute { get; private set; }
    public static int hour { get; private set; }
    public static int day { get; private set; }

    private static string[] season = { "Dry(Cool)", "Dry2(Hot)", "Rainy1", "Rainy2" };

    public static int seasonCounter;

    private float secondToOneMinuteRT = 2f;
    private float timer;

    private void Start()
    {
        minute = 0;
        hour = 7;
        day = 1;
        seasonCounter = 1;
        timer = secondToOneMinuteRT;
    }

    public static string getSeason (int counter)
    {
        return season[counter];
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        OnMinuteChanged?.Invoke();

        if (timer <= 0) 
        {
            minute++;
            if (minute>=60)
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
                        seasonCounter++;
                    }
                }
                OnHourChanged?.Invoke();
                minute = 0;
            }

            timer = secondToOneMinuteRT;
        }
    }
}

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

    private static string[] season = { "Dry", "Rainy"};

    public static int seasonCounter;

    private float secondToOneMinuteRT = 1f;
    private float timer;

    private void Start()
    {
        //set minute
        if (PlayerData.minute == 0)
            minute = 0;
        else
            minute = PlayerData.minute;
        //set hour
        if (PlayerData.hour == 0)
            hour = 7;
        else
            hour = PlayerData.hour;
        //set day
        if (PlayerData.day == 0)
            day = 1;
        else
            day = PlayerData.day;

        seasonCounter = 0;
        timer = secondToOneMinuteRT;
    }

    private void OnDestroy()
    {
        PlayerData.minute = minute;
        PlayerData.hour = hour;
        PlayerData.day = day;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged, OnHourChanged;

    public static int minute { get; private set; }
    public static int hour { get; private set; }

    private float secondToOneMinuteRT = 2f;
    private float timer;

    private void Start()
    {
        minute = 0;
        hour = 7;
        timer = secondToOneMinuteRT;

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
                OnHourChanged?.Invoke();
                minute = 0;
            }

            timer = secondToOneMinuteRT;
        }
    }
}

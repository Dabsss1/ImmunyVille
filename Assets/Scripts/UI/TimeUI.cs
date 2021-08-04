using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{

    //public TextMeshProUGUI timeText;
    public Text timeText;

    public Text PhoneDate, PhoneTime;


    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        if (PhoneTime.IsActive())
        {
            PhoneTime.text = $"Time:{TimeManager.hour:00}:{TimeManager.minute:00}";
        }
        timeText.text = $"Spr Day 1    {TimeManager.hour:00}:{TimeManager.minute:00}";
    }

}

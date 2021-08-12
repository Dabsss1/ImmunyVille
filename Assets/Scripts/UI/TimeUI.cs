using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dateTime;
    [SerializeField] TextMeshProUGUI phoneDateTime;


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
        if (phoneDateTime.IsActive())
        {
            phoneDateTime.text = $"Day: {TimeManager.day}   Time: {TimeManager.hour:00}:{TimeManager.minute:00}";
        }
        dateTime.text = $"{TimeManager.getSeason(TimeManager.seasonCounter)} Season  Day:{TimeManager.day}   {TimeManager.hour:00}:{TimeManager.minute:00}";
    }

}

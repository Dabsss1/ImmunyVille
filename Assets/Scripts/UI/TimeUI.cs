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

    private void Start()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        if (phoneDateTime.IsActive())
        {
            phoneDateTime.text = $"Day: {TimeManager.Instance.day}   Time: {TimeManager.Instance.hour:00}:{TimeManager.Instance.minute:00}";
        }
        dateTime.text = $"{TimeManager.Instance.season} Season  Day:{TimeManager.Instance.day}   {TimeManager.Instance.hour:00}:{TimeManager.Instance.minute:00}";
    }

}

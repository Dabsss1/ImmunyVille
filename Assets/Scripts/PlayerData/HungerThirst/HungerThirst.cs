using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerThirst : MonoBehaviour
{
    public float hungerStat;
    public float thirstStat;

    public float defaultValue;

    public static HungerThirst Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += DrainStat;
    }
    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= DrainStat;
    }

    void DrainStat()
    {
        if (thirstStat <= 0)
            thirstStat = 0;
        else
            thirstStat -= .83f;

        if (hungerStat <= 0)
            hungerStat = 0;
        else
            hungerStat -= .33f;
    }
    public void IncreaseHunger(float value)
    {
        hungerStat += value;
        if (hungerStat > 100)
            hungerStat = 100;
    }

    public void DecreaseHunger(float value)
    {
        hungerStat -= value;
        if (hungerStat < 0)
            hungerStat = 0;
    }

    public void IncreaseThirst(float value)
    {
        thirstStat += value;
        if (thirstStat > 100)
            thirstStat = 100;
    }

    public void DecreaseThirst(float value)
    {
        thirstStat -= value;
        if (thirstStat < 0)
            thirstStat = 0;
    }

    public void ResetData()
    {
        thirstStat = defaultValue;
        hungerStat = defaultValue;
    }
}
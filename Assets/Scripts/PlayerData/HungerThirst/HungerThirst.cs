using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerThirst : MonoBehaviour
{
    public float hungerStat;
    public float thirstStat;

    public float defaultValue;

    public int thirstCollapseLimit = 240;
    public int hungerCollapseLimit = 240;

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
        {
            thirstStat = 0;
            Stats.Instance.DrainStats();
        }
        else
            thirstStat -= .40f;

        if (hungerStat <= 0)
        {
            hungerStat = 0;
            Stats.Instance.DrainStats();
        }
        else
            hungerStat -= .33f;

        if (Stats.Instance.ZeroStats() && thirstStat <= 0)
        {
            thirstCollapseLimit -= 1;
            if (thirstCollapseLimit <= 0)
            {
                AudioManager.Instance.StopSfx("FootstepsOutdoor");
                AudioManager.Instance.StopSfx("FootstepsIndoor");
                SceneLoaderManager.OnSceneLoad("BadEnding");
            }
        }

        if (Stats.Instance.ZeroStats() && hungerStat <= 0)
        {
            hungerCollapseLimit -= 1;
            if (hungerCollapseLimit <= 0)
            {
                AudioManager.Instance.StopSfx("FootstepsOutdoor");
                AudioManager.Instance.StopSfx("FootstepsIndoor");
                SceneLoaderManager.OnSceneLoad("BadEnding");
            }
        }
    }
    public void IncreaseHunger(float value)
    {
        hungerStat += value;
        if (hungerStat > 100)
            hungerStat = 100;

        hungerCollapseLimit = 240;
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

        thirstCollapseLimit = 240;
    }

    public void DecreaseThirst(float value)
    {
        thirstStat -= value;
        if (thirstStat < 0)
            thirstStat = 0;
    }

    public void HungerThirstDayReset()
    {
        if (thirstStat > 30)
            thirstStat = 30;
        if (hungerStat > 30)
            hungerStat = 30;
    }

    public void ResetData()
    {
        thirstStat = defaultValue;
        hungerStat = defaultValue;

        thirstCollapseLimit = 240;
        hungerCollapseLimit = 240;
    }
}
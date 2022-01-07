using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayResetter : MonoBehaviour
{
    public static Action OnBedSleep;
    private void OnEnable()
    {
        TimeManager.OnMidnight += NextDay;
        OnBedSleep += NextDay;
    }

    private void OnDisable()
    {
        TimeManager.OnMidnight -= NextDay;
        OnBedSleep -= NextDay;
    }

    void NextDay()
    {
        if (GameStateManager.Instance.EqualsState(OpenWorldState.SCENECHANGING))
            return;
        GameStateManager.Instance.ChangeGameState(OpenWorldState.SCENECHANGING);

        if (TimeManager.Instance.hour >= 21)
        {
            Stats.Instance.DeductStats(20,20,20,20);
        }

        TimeManager.Instance.hour = 6;
        TimeManager.Instance.minute = 0;
        TimeManager.Instance.day += 1;

        if(TimeManager.Instance.day >= 31)
        {
            if (TimeManager.Instance.season == "Dry")
                TimeManager.Instance.season = "Rainy";
            else
                TimeManager.Instance.season = "Dry";
        }

        TimeManager.Instance.SetRain();

        PlayerDataManager.Instance.totalDays += 1;

        HungerThirst.Instance.HungerThirstDayReset();

        foreach (PlantedPlantSlot plantSlot in Plants.Instance.plantedSlots)
        {
            plantSlot.daysLeftToGrow -= 1;
        }

        for (int i = 0; i < 7; i++)
        {
            if (Tasks.Instance.taskSlots[i].repeatTimer > 0)
                Tasks.Instance.taskSlots[i].repeatTimer -= 1;

            if (Tasks.Instance.taskSlots[i].repeatTimer <= 0)
            {
                Tasks.Instance.taskSlots[i].done = false;
                Tasks.Instance.taskSlots[i].inProgress = true;
            }
        }

        SpecialCounters.Instance.waterObtained = false;
        SpecialCounters.Instance.walk = 0;
        SpecialCounters.Instance.ResetCounter("DrinkWater");

        SaveSystem.SavePlayer();

        Tasks.Instance.walkdone = false;

        if (PlayerDataManager.Instance.totalDays >= 60)
        {
            if (Stats.Instance.MaxedStats())
                SceneLoaderManager.OnSceneLoad("GoodEnding");
            else
                SceneLoaderManager.OnSceneLoad("BadEnding");
        }

        if (PlayerDataManager.Instance.totalDays % 7 == 0)
        {
            if (Stats.Instance.MaxedStats())
                SceneLoaderManager.OnSceneLoad("GoodEnding");
            else
                SceneLoaderManager.OnSceneLoad("DoctorCheckup");
        }
        else
            SceneLoaderManager.OnSleepLoad("Bedroom");
    }
    
}

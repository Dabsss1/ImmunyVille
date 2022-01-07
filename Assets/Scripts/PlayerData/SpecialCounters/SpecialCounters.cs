using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCounters : MonoBehaviour
{
    public List<PlayerCounter> playerCounters = new List<PlayerCounter>();

    public PlantItem seedRewards;
    public int seedRewardQuantity;

    public bool waterObtained = false;

    public int walk=0;
    public int walkTargetSteps;
    public TaskItem walkTask;

    public static SpecialCounters Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public bool IncreaseCounterAndCheck(string taskName)
    {
        foreach(PlayerCounter counter in playerCounters)
        {
            if (counter.taskName == taskName)
            {
                counter.counter += 1;
                if (counter.counter >= counter.maxCounter)
                    return true;
                else
                    return false;
            }
        }
        return false;
    }

    public void ResetCounter(string taskName)
    {
        foreach (PlayerCounter counter in playerCounters)
        {
            if (counter.taskName == taskName)
            {
                counter.counter = 0;
            }
        }
    }

    public int CurrentCounter(string taskName)
    {
        foreach (PlayerCounter counter in playerCounters)
        {
            if (counter.taskName == taskName)
            {
                return counter.counter;
            }
        }
        return 0;
    }

    public int MaxCounter(string taskName)
    {
        foreach (PlayerCounter counter in playerCounters)
        {
            if (counter.taskName == taskName)
            {
                return counter.maxCounter;
            }
        }
        return 0;
    }

    public void IncreaseWalkCounter()
    {
        if (walk >= walkTargetSteps)
            Tasks.Instance.CompleteTask (walkTask);
        else
            walk+=1;
    }

    public void ObtainFarmAndDog()
    {
        Plants.Instance.ObtainSeed(seedRewards,seedRewardQuantity);
        Badges.Instance.ObtainBadge("Dog",3);
    }

    public void ResetData()
    {
        foreach(PlayerCounter counter in playerCounters)
        {
            counter.counter = 0;
        }

        waterObtained = false;
        walk = 0;
    }
}


[System.Serializable]
public class PlayerCounter
{
    public string taskName;
    public int counter;
    public int maxCounter;
}

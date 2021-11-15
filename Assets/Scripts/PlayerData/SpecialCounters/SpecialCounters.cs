using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCounters : MonoBehaviour
{
    public List<PlayerCounter> playerCounters = new List<PlayerCounter>();

    public PlantItem seedRewards;
    public int seedRewardQuantity;

    public bool waterObtained = false;

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
    
    public void ObtainFarmAndDog()
    {
        Plants.Instance.ObtainSeed(seedRewards,seedRewardQuantity);
        Badges.Instance.ObtainBadge("Dog",3);
    }
}


[System.Serializable]
public class PlayerCounter
{
    public string taskName;
    public int counter;
    public int maxCounter;
}

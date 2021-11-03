using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, Interactable
{
    public void Interact()
    {
        PlayerSceneInformation.Instance.previousScene = "Bedroom";
        NextDay();
        SceneLoaderManager.OnSceneLoad("Bedroom");
    }

    public void NextDay()
    {
        TimeManager.Instance.hour = 6;
        TimeManager.Instance.minute = 0;
        TimeManager.Instance.day++;

        HungerThirst.Instance.hungerStat = 30;
        HungerThirst.Instance.thirstStat = 30;

        foreach (PlantedPlantSlot plantSlot in Plants.Instance.plantedSlots)
        {
            plantSlot.daysLeftToGrow--;
        }

        for(int i=0; i<4; i++)
        {
            if(Tasks.Instance.taskSlots[i].repeatTimer>0)
                Tasks.Instance.taskSlots[i].repeatTimer--;

            if (Tasks.Instance.taskSlots[i].repeatTimer <= 0)
            {
                Tasks.Instance.taskSlots[i].done = false;
                Tasks.Instance.taskSlots[i].inProgress = true;
            }
        }

        SaveSystem.SavePlayer();
    }
}

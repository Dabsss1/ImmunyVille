using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, Interactable
{
    public void Interact()
    {
        Debug.Log("Sleep");
        PlayerSceneInformation.Instance.previousScene = "Bedroom";
        NextDay();
        SceneLoaderManager.OnSceneLoad("Bedroom");
    }

    public void NextDay()
    {
        TimeManager.Instance.hour = 7;
        TimeManager.Instance.minute = 0;
        TimeManager.Instance.day++;

        HungerThirst.Instance.hungerStat = 30;
        HungerThirst.Instance.thirstStat = 30;

        SaveSystem.SavePlayer();
    }
}

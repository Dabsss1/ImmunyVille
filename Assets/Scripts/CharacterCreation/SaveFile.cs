using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
    //player profile
    public string gender;
    public string playerName;

    //quicksave
    public string savedScene = "";
    public float[] position = { 0, 0, 0 };

    //game data
    public int hour, minute, day;
    public string season;

    //player data
    public int[] inventory = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public float health = 0;
    public float confidence = 0;
    public float strength = 0;
    public float body = 0;

    public int[] badge = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public float hunger = 0;
    public float thirst = 0;

    public SaveFile()
    {
        //profile
        gender = PlayerDataManager.Instance.gender;
        playerName = PlayerDataManager.Instance.playerName;

        //game data
        hour = TimeManager.Instance.hour;
        minute = TimeManager.Instance.minute;
        day = TimeManager.Instance.day;
        season = TimeManager.Instance.season;

        //position and location
        savedScene = SceneInitiator.Instance.sceneName;
        position[0] = Player.Instance.transform.position.x;
        position[1] = Player.Instance.transform.position.y;
        position[2] = Player.Instance.transform.position.z;

        //player data
        for (int i = 0; i < inventory.Length; i++)
            inventory[i] = Inventory.Instance.slots[i].count;

        health = Stats.Instance.health;
        confidence = Stats.Instance.confidence;
        strength = Stats.Instance.strength;
        body = Stats.Instance.body;

        for (int i = 0; i < badge.Length; i++)
            inventory[i] = Badges.Instance.badgeSlots[i].tier;

        hunger = HungerThirst.Instance.hungerStat;
        thirst = HungerThirst.Instance.thirstStat;
    }

    public SaveFile(string emptyFile)
    {

    }

    public void DistributeData()
    {
        //profile
        PlayerDataManager.Instance.gender = gender;
        PlayerDataManager.Instance.playerName = playerName;

        //game data
        TimeManager.Instance.hour = hour;
        TimeManager.Instance.minute = minute;
        TimeManager.Instance.day = day;
        TimeManager.Instance.season = season;

        //quicksave
        PlayerDataManager.Instance.savedScene = savedScene;
        PlayerDataManager.Instance.position[0] = position[0];
        PlayerDataManager.Instance.position[1] = position[1];
        PlayerDataManager.Instance.position[2] = position[2];

        //player data
        for (int i = 0; i < inventory.Length; i++)
            Inventory.Instance.slots[i].count = inventory[i];

        Stats.Instance.health = health;
        Stats.Instance.confidence= confidence;
        Stats.Instance.strength = strength;
        Stats.Instance.body = body;

        for (int i = 0; i < badge.Length; i++)
            Badges.Instance.badgeSlots[i].tier = inventory[i];

        HungerThirst.Instance.hungerStat = hunger;
        HungerThirst.Instance.thirstStat = thirst;

        MainMenu.dataLoaded = true;
    }
}

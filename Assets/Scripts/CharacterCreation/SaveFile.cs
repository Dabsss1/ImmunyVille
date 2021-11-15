using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
    //player profile
    public string gender;
    public string playerName;
    public float totalDays;

    //quicksave
    public string savedScene = "";
    public float[] position = { 0, 0, 0 };

    //game data
    public int hour, minute, day;
    public string season;

    //player data

    public int[] inventory = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public float gold = 0;


    public float health = 0;
    public float confidence = 0;
    public float strength = 0;
    public float body = 0;

    public int[] badge = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public float hunger = 0;
    public float thirst = 0;

    public bool[] done = new bool[18];
    public bool[] inProgress = new bool[18];
    public int[] progressCounter = new int[18];
    public int[] repeatTimer = new int[18];

    public int[] seedQuantity = new int[7];
    //plants
    public bool isEmpty = false;
    public string[] plantName = new string[1];
    public float[] plantedSpot = new float[3];
    public int[] daysLeftToGrow = new int[1];

    //special counters
    public int HandgripCounter = 0;
    public bool waterObtained;
    public SaveFile()
    {
        //profile
        gender = PlayerDataManager.Instance.gender;
        playerName = PlayerDataManager.Instance.playerName;
        totalDays = PlayerDataManager.Instance.totalDays;

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
            

        gold = Inventory.Instance.gold;

        health = Stats.Instance.health;
        confidence = Stats.Instance.confidence;
        strength = Stats.Instance.strength;
        body = Stats.Instance.body;

        for (int i = 0; i < badge.Length; i++)
            badge[i] = Badges.Instance.badgeSlots[i].tier;

        hunger = HungerThirst.Instance.hungerStat;
        thirst = HungerThirst.Instance.thirstStat;

        //tasks
        for(int i = 0; i< done.Length; i++)
        {
            done[i] = Tasks.Instance.taskSlots[i].done;
            inProgress[i] = Tasks.Instance.taskSlots[i].inProgress;
            progressCounter[i] = Tasks.Instance.taskSlots[i].progressCounter;
            repeatTimer[i] = Tasks.Instance.taskSlots[i].repeatTimer;
        }

        //seeds
        for (int i = 0; i<seedQuantity.Length; i++)
            seedQuantity[i] = Plants.Instance.seedSlots[i].quantity;

        //Planted plants
        if (Plants.Instance.plantedSlots.Count > 0)
        {
            int totalPlanted = Plants.Instance.plantedSlots.Count;
            string[] plantName = new string[totalPlanted];
            float[] plantedSpot = new float[totalPlanted * 3];
            int[] daysLeftToGrow = new int[totalPlanted];

            int plantedSpotCounter = 0;

            isEmpty = false;
            for (int i = 0; i < totalPlanted; i++)
            {
                plantName[i] = Plants.Instance.plantedSlots[i].slot.plantName;

                plantedSpot[plantedSpotCounter] = Plants.Instance.plantedSlots[i].plantingSpot.x;
                plantedSpotCounter += 1;
                plantedSpot[plantedSpotCounter] = Plants.Instance.plantedSlots[i].plantingSpot.y;
                plantedSpotCounter += 1;
                plantedSpot[plantedSpotCounter] = Plants.Instance.plantedSlots[i].plantingSpot.z;
                plantedSpotCounter += 1;

                daysLeftToGrow[i] = Plants.Instance.plantedSlots[i].daysLeftToGrow;
            }

            this.plantName = plantName;
            this.plantedSpot = plantedSpot;
            this.daysLeftToGrow = daysLeftToGrow;
        }
        else
            isEmpty = true;

        HandgripCounter = SpecialCounters.Instance.playerCounters[0].counter;
        waterObtained = SpecialCounters.Instance.waterObtained;
    }

    public SaveFile(string emptyFile)
    {

    }

    public void DistributeData()
    {
        //profile
        PlayerDataManager.Instance.gender = gender;
        PlayerDataManager.Instance.playerName = playerName;
        PlayerDataManager.Instance.totalDays = totalDays;

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
            

        Inventory.Instance.gold = gold;

        Stats.Instance.health = health;
        Stats.Instance.confidence= confidence;
        Stats.Instance.strength = strength;
        Stats.Instance.body = body;

        for (int i = 0; i < badge.Length; i++)
            Badges.Instance.badgeSlots[i].tier = badge[i];

        HungerThirst.Instance.hungerStat = hunger;
        HungerThirst.Instance.thirstStat = thirst;

        //tasks
        for (int i = 0; i < done.Length; i++)
        {
            Tasks.Instance.taskSlots[i].done = done[i];
            Tasks.Instance.taskSlots[i].inProgress = inProgress[i];
            Tasks.Instance.taskSlots[i].progressCounter = progressCounter[i];
            Tasks.Instance.taskSlots[i].repeatTimer = repeatTimer[i];
        }

        //seeds
        for (int i = 0; i < seedQuantity.Length; i++)
            Plants.Instance.seedSlots[i].quantity = seedQuantity[i];

        //Planted plants
        if (!isEmpty)
        {
            int plantedSpotCounter = 0;
            for (int i=0; i < plantName.Length; i++)
            {
                float posX = plantedSpot[plantedSpotCounter];
                plantedSpotCounter += 1;
                float posY = plantedSpot[plantedSpotCounter];
                plantedSpotCounter += 1;
                float posZ = plantedSpot[plantedSpotCounter];
                plantedSpotCounter += 1;

                Plants.Instance.AddPlant(plantName[i],posX,posY,posZ,daysLeftToGrow[i]);
            }
        }
        SpecialCounters.Instance.playerCounters[0].counter = HandgripCounter;
        SpecialCounters.Instance.waterObtained = waterObtained;

        MainMenu.dataLoaded = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
    public string gender,
        name;

    //game information
    public float[] position;
    public int minute, day, hour;
    public string currentScene;

    public bool isQuickSave;

    public SaveFile()
    {
        gender = PlayerData.gender;
        name = PlayerData.playerName;

        position = PlayerData.position;
        minute = PlayerData.minute;
        hour = PlayerData.hour;
        day = PlayerData.day;

        currentScene = PlayerData.scene;

        isQuickSave = PlayerData.isQuickSave;
    }

    public void setPlayerData()
    {
        PlayerData.gender = gender;
        PlayerData.playerName = name;

        PlayerData.position = position;
        PlayerData.minute = minute;
        PlayerData.hour = hour;
        PlayerData.day = day;

        PlayerData.scene = currentScene;
        PlayerData.isQuickSave = isQuickSave;
    }
}

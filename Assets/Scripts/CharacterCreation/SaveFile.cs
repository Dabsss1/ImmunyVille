using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
    public string gender,
        name;

    public SaveFile()
    {
        gender = PlayerData.gender;
        name = PlayerData.playerName;
    }

    public void setPlayerData()
    {
        PlayerData.gender = gender;
        PlayerData.playerName = name;
    }
}

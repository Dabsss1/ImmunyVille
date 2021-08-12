using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class PlayerData
{
    public static string gender = "", playerName = "";
    public static string scene = "";

    public static bool isQuickSave = false;

    //game data
    public static int hour = 0, minute = 0 , day = 0;
    public static float[] position = { 0,0,0};


}

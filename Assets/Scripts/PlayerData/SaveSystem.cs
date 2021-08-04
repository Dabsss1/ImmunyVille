using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static void SavePlayer (Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.imv";
        FileStream stream = new FileStream(path, FileMode.Create);
        try
        {
            PlayerData data = new PlayerData();

            formatter.Serialize(stream, data);
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
        }
        stream.Close();
    }

    public static void SavePlayer (PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.imv";
        FileStream stream = new FileStream(path, FileMode.Create);
        try
        {
            formatter.Serialize(stream, data);
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
        }

        stream.Close();
    }

    public static PlayerData LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/player.imv";
        if (File.Exists(path))
        {
            PlayerData data = new PlayerData();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                data = formatter.Deserialize(stream) as PlayerData;
            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
            }
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save not found" + path);
            return null;
        }
    }
}

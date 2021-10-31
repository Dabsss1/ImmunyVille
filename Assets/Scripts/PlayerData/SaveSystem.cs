using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static void SavePlayer ()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.imv";
        FileStream stream = new FileStream(path, FileMode.Create);
        try
        {
            SaveFile data = new SaveFile();
            formatter.Serialize(stream, data);
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
        }

        stream.Close();
        Debug.Log("Saved: " + PlayerDataManager.Instance.gender + " " + PlayerDataManager.Instance.playerName);
    }

    public static void LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/player.imv";
        if (File.Exists(path))
        {
            SaveFile data = new SaveFile("empty");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                data = formatter.Deserialize(stream) as SaveFile;
                data.DistributeData();
                Debug.Log("Loaded Data file");
            }
            catch (Exception e)
            {
                Debug.Log("Loading file error");
                Debug.Log(e.StackTrace);
            }
            stream.Close();
            Debug.Log("Loaded: " + PlayerDataManager.Instance.gender + " " + PlayerDataManager.Instance.playerName);
        }
        else
        {
            Debug.LogError("Save not found" + path);
        }
    }

}

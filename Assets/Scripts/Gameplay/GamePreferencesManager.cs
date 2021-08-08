using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GamePreferencesManager : MonoBehaviour
{
    const string previousScene = "prevScene";

    public static Action OnLoadPrefs;

    private void OnEnable()
    {
        OnLoadPrefs += LoadPrefs;
    }

    private void OnDisable()
    {
        OnLoadPrefs -= LoadPrefs;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadPrefs();
    }


    public void SavePrefs(string prevScene)
    {
        PlayerPrefs.SetString(previousScene,prevScene);
        PlayerPrefs.Save();
        Debug.Log("Saved prev scene "+prevScene);
    }

    public void LoadPrefs()
    {
        Spawner.previousScene = PlayerPrefs.GetString(previousScene,"PlayerLot");
    }
}

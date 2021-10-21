using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class GamePreferencesManager : MonoBehaviour
{
    const string musicVol = "musicVol";

    public static Action OnLoadPrefs,OnLoadSettings;

    public static Action<string> OnSavePrefs;

    public static Action<float> OnSaveSettings;
    private void OnEnable()
    {

        OnSaveSettings += SaveSettingsPrefs;
        OnLoadSettings += LoadSettingsPrefs;
    }

    private void OnDisable()
    {

        OnLoadSettings -= LoadSettingsPrefs;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SaveSettingsPrefs(float musicValue)
    {
        PlayerPrefs.SetFloat(musicVol, musicValue);
        PlayerPrefs.Save();
        Debug.Log("Saved Settings\nMusic: "+musicValue);
    }

    public void LoadSettingsPrefs()
    {
        AudioManager.Instance.audioMixer.SetFloat("volume",PlayerPrefs.GetFloat(musicVol, 0));
        Debug.Log("SettingsLoaded");
    }
}

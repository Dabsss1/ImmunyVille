using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] const string MusicVolKey = "MusicVolume";
    [SerializeField] const string SfxVolKey = "SfxVolume";

    public float musicVolume;
    public float sfxVolume;
    
    public static Settings Instance { get; private set; }


    void Awake()
    {
        if (Instance == null)
            Instance = this;

        musicVolume = PlayerPrefs.GetFloat(MusicVolKey);
        sfxVolume = PlayerPrefs.GetFloat(SfxVolKey);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(MusicVolKey,musicVolume);
        PlayerPrefs.SetFloat(SfxVolKey,sfxVolume);
        PlayerPrefs.Save();
    }

    private void Start()
    {
        AudioManager.Instance.SetVolume(musicVolume,sfxVolume);
    }
}

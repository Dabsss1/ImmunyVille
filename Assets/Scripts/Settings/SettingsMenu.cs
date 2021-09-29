using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider musicSlider;

    //public static Action OnVolumeLoad;

    private void Start()
    {
        Debug.Log("Loaded volume: " + PlayerPrefs.GetFloat("musicVol", 0));
        musicSlider.value = PlayerPrefs.GetFloat("musicVol", 0);
    }
    /*
    private void OnEnable()
    {
        OnVolumeLoad += SetSettingsDefault;
    }
    private void OnDisable()
    {
        OnVolumeLoad -= SetSettingsDefault;
    }

    void SetSettingsDefault()
    {
        Debug.Log("Loaded volume: " + PlayerPrefs.GetFloat("musicVol", 0));
        musicSlider.value = PlayerPrefs.GetFloat("musicVol", 0);
    }
    */
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SaveSettings()
    {
        float volume;
        audioMixer.GetFloat("volume", out volume);
        GamePreferencesManager.OnSaveSettings?.Invoke(volume);
    }
}

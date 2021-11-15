using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    //public static Action OnVolumeLoad;

    private void OnEnable()
    {
        musicSlider.value = Settings.Instance.musicVolume;
        sfxSlider.value = Settings.Instance.sfxVolume;
    }

    private void OnDisable()
    {
        Settings.Instance.SaveSettings();
    }

    public void musicSetVolume (float volume)
    {
        AudioManager.Instance.bgmMixer.SetFloat("volume", volume);
        Settings.Instance.musicVolume = volume;
    }

    public void sfxSetVolume(float volume)
    {
        AudioManager.Instance.sfxMixer.SetFloat("volume", volume);
        Settings.Instance.sfxVolume = volume;
    }

    public void ExitSound()
    {
        AudioManager.Instance.PlaySfx("Exit");
    }
}

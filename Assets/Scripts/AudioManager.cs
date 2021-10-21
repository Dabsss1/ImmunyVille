using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public AudioMixer audioMixer;

    public AudioMixerGroup master;
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;;

            s.source.outputAudioMixerGroup = master;
        }
    }

    private void Start()
    {
        GamePreferencesManager.OnLoadSettings?.Invoke();
        //SettingsMenu.OnVolumeLoad?.Invoke();
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound=> sound.name == name);
        if (s == null)
            return;
        if (!s.source.isPlaying)
            s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        if (s.source.isPlaying)
            s.source.Stop();
    }
}

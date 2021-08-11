using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static Action<string> PlaySound,StopSound;

    private void OnEnable()
    {
        PlaySound += Play;
        StopSound += Stop;
    }

    private void OnDisable()
    {
        PlaySound -= Play;
        StopSound += Stop;
    }

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    void Play(string name)
    {
        Sound s = Array.Find(sounds, sound=> sound.name == name);
        if (s == null)
            return;
        if (!s.source.isPlaying)
            s.source.Play();
    }

    void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        if (s.source.isPlaying)
            s.source.Stop();
    }
}

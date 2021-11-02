using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] bgm;
    public Sound[] sfx;

    public AudioMixer bgmMixer;
    public AudioMixerGroup bgmMixerGroup;

    public AudioMixer sfxMixer;
    public AudioMixerGroup sfxMixerGroup;

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

        foreach (Sound s in bgm)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            if(s.volume == 0)
                s.source.volume = 1;
            else
                s.source.volume = s.volume;

            if (s.pitch == 0)
                s.source.pitch = 1;
            else
                s.source.pitch = s.pitch;

            s.source.loop = s.loop;;

            s.source.playOnAwake = false;

            s.source.outputAudioMixerGroup = bgmMixerGroup;
        }

        foreach (Sound s in sfx)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            if (s.volume == 0)
                s.source.volume = 1;
            else
                s.source.volume = s.volume;

            if (s.pitch == 0)
                s.source.pitch = 1;
            else
                s.source.pitch = s.pitch;

            s.source.loop = s.loop; ;

            s.source.playOnAwake = false;

            s.source.outputAudioMixerGroup = sfxMixerGroup;
        }
    }
    public void SetVolume(float bgmVol, float sfxVol)
    {
        bgmMixer.SetFloat("volume", bgmVol);
        sfxMixer.SetFloat("volume", sfxVol);
    }

    public void Play(string name)
    {
        
        Sound s = Array.Find(bgm, sound=> sound.name == name);
        
        if (s == null)
            return;
        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
        
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(bgm, sound => sound.name == name);
        if (s == null)
            return;
        if (s.source.isPlaying)
            s.source.Stop();
    }

    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfx, sound => sound.name == name);
        if (s == null)
            return;
        if (!s.source.isPlaying)
            s.source.Play();
    }

    public void StopSfx(string name)
    {
        Sound s = Array.Find(sfx, sound => sound.name == name);
        if (s == null)
            return;
        if (s.source.isPlaying)
            s.source.Stop();
    }
}

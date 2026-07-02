using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] MusicSounds, SfXSounds;
    public AudioSource MusicSource, SFXSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayMusic(string name)
    {
        if (string.IsNullOrEmpty(name) || MusicSource == null || MusicSounds == null)
        {
            Debug.Log("PlayMusic: invalid input or audio source not assigned.");
            return;
        }

        Sound s = Array.Find(MusicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Music Not Found");
            return;
        }

        MusicSource.clip = s.clip;
        MusicSource.Play();
    }

    public void StopMusic(string name = null)
    {
        if (MusicSource == null || !MusicSource.isPlaying)
            return;

        if (string.IsNullOrEmpty(name) || MusicSource.clip == null)
        {
            MusicSource.Stop();
            return;
        }

        if (MusicSounds == null)
        {
            MusicSource.Stop();
            return;
        }

        Sound s = Array.Find(MusicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Music Not Found");
            return;
        }

        if (MusicSource.clip == s.clip)
        {
            MusicSource.Stop();
        }
    }

    public void PlaySFX(string name)
    {
        if (string.IsNullOrEmpty(name) || SFXSource == null || SfXSounds == null)
        {
            Debug.Log("PlaySFX: invalid input or audio source not assigned.");
            return;
        }

        Sound s = Array.Find(SfXSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("SFX Not Found");
            return;
        }

        SFXSource.clip = s.clip;
        if (!SFXSource.isPlaying)
            SFXSource.Play();
    }

    public void StopSFX(string name = null)
    {
        if (SFXSource == null || !SFXSource.isPlaying)
            return;

        if (string.IsNullOrEmpty(name) || SFXSource.clip == null)
        {
            SFXSource.Stop();
            return;
        }

        if (SfXSounds == null)
        {
            SFXSource.Stop();
            return;
        }

        Sound s = Array.Find(SfXSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("SFX Not Found");
            return;
        }

        if (SFXSource.clip == s.clip)
        {
            SFXSource.Stop();
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (MusicSource != null)
            MusicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSfxVolume(float volume)
    {
        if (SFXSource != null)
            SFXSource.volume = Mathf.Clamp01(volume);
    }

    public void SetVoiceVolume(float volume)
    {
        if (SFXSource != null)
            SFXSource.volume = Mathf.Clamp01(volume);
    }
}

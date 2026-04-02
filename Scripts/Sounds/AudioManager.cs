using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
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
        if (string.IsNullOrEmpty(name) || musicSource == null || musicSounds == null)
        {
            Debug.Log("PlayMusic: invalid input or audio source not assigned.");
            return;
        }

        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Music Not Found");
            return;
        }

        musicSource.clip = s.clip;
        musicSource.Play();
    }

    public void StopMusic(string name = null)
    {
        if (musicSource == null || !musicSource.isPlaying)
            return;

        if (string.IsNullOrEmpty(name) || musicSource.clip == null)
        {
            musicSource.Stop();
            return;
        }

        if (musicSounds == null)
        {
            musicSource.Stop();
            return;
        }

        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Music Not Found");
            return;
        }

        if (musicSource.clip == s.clip)
        {
            musicSource.Stop();
        }
    }

    public void PlaySFX(string name)
    {
        if (string.IsNullOrEmpty(name) || sfxSource == null || sfxSounds == null)
        {
            Debug.Log("PlaySFX: invalid input or audio source not assigned.");
            return;
        }

        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("SFX Not Found");
            return;
        }

        sfxSource.clip = s.clip;
        if (!sfxSource.isPlaying)
            sfxSource.Play();
    }

    public void StopSFX(string name = null)
    {
        if (sfxSource == null || !sfxSource.isPlaying)
            return;

        if (string.IsNullOrEmpty(name) || sfxSource.clip == null)
        {
            sfxSource.Stop();
            return;
        }

        if (sfxSounds == null)
        {
            sfxSource.Stop();
            return;
        }

        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("SFX Not Found");
            return;
        }

        if (sfxSource.clip == s.clip)
        {
            sfxSource.Stop();
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
            musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSfxVolume(float volume)
    {
        if (sfxSource != null)
            sfxSource.volume = Mathf.Clamp01(volume);
    }

    public void SetVoiceVolume(float volume)
    {
        if (sfxSource != null)
            sfxSource.volume = Mathf.Clamp01(volume);
    }
}

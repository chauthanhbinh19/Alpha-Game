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
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Music Not Found");
            return;
        }

        musicSource.clip = s.clip;
        musicSource.Play();
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("SFX Not Found");
            return;
        }

        // Gán clip vào sfxSource để có thể stop sau này
        sfxSource.clip = s.clip;
        if (!sfxSource.isPlaying)
            sfxSource.Play();
    }
    public void StopSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("SFX Not Found");
            return;
        }

        if (sfxSource.isPlaying && sfxSource.clip == s.clip)
        {
            sfxSource.Stop();
        }
    }
}

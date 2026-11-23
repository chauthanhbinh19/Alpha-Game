using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] MusicSounds, SfXSounds;
    public AudioSource MusicSource, SFXSource;

    // Thêm Dictionary để tra cứu O(1) thay vì Array.Find mỗi lần bấm nút
    private Dictionary<string, AudioClip> _musicDict;
    private Dictionary<string, AudioClip> _sfxDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
            InitAudioDictionaries();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitAudioDictionaries()
    {
        _musicDict = new Dictionary<string, AudioClip>();
        if (MusicSounds != null)
        {
            foreach (var s in MusicSounds)
            {
                if (s != null && !string.IsNullOrEmpty(s.name) && s.clip != null)
                    _musicDict[s.name] = s.clip;
            }
        }

        _sfxDict = new Dictionary<string, AudioClip>();
        if (SfXSounds != null)
        {
            foreach (var s in SfXSounds)
            {
                if (s != null && !string.IsNullOrEmpty(s.name) && s.clip != null)
                    _sfxDict[s.name] = s.clip;
            }
        }
    }

    public void PlayMusic(string name)
    {
        if (string.IsNullOrEmpty(name) || MusicSource == null || MusicSounds == null)
        {
            Debug.Log("PlayMusic: invalid input or audio source not assigned.");
            return;
        }

        if (_musicDict == null) InitAudioDictionaries();

        if (!_musicDict.TryGetValue(name, out AudioClip clip))
        {
            Debug.Log("Music Not Found");
            return;
        }

        // Buộc dừng và phát lại từ đầu (0s) bất kể đang phát hay không
        MusicSource.Stop();
        MusicSource.clip = clip;
        MusicSource.time = 0f;
        MusicSource.Play();
    }

    public void StopMusic(string name = null)
    {
        if (MusicSource == null || !MusicSource.isPlaying)
            return;

        if (string.IsNullOrEmpty(name) || MusicSource.clip == null || MusicSounds == null)
        {
            MusicSource.Stop();
            return;
        }

        if (_musicDict == null) InitAudioDictionaries();

        if (!_musicDict.TryGetValue(name, out AudioClip clip))
        {
            Debug.Log("Music Not Found");
            return;
        }

        if (MusicSource.clip == clip)
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

        if (_sfxDict == null) InitAudioDictionaries();

        if (!_sfxDict.TryGetValue(name, out AudioClip clip))
        {
            Debug.Log("SFX Not Found");
            return;
        }

        // Bỏ điều kiện if (!SFXSource.isPlaying)
        // Buộc ngắt đoạn im lặng/âm thanh cũ và phát lại ngay lập tức từ đầu
        SFXSource.Stop();
        SFXSource.clip = clip;
        SFXSource.time = 0f;
        SFXSource.Play();
    }

    public void StopSFX(string name = null)
    {
        if (SFXSource == null || !SFXSource.isPlaying)
            return;

        if (string.IsNullOrEmpty(name) || SFXSource.clip == null || SfXSounds == null)
        {
            SFXSource.Stop();
            return;
        }

        if (_sfxDict == null) InitAudioDictionaries();

        if (!_sfxDict.TryGetValue(name, out AudioClip clip))
        {
            Debug.Log("SFX Not Found");
            return;
        }

        if (SFXSource.clip == clip)
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
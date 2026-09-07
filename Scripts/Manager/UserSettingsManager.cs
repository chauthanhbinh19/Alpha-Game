using System.Collections.Generic;

public class UserSettingsManager
{
    private static UserSettingsManager instance;
    public static UserSettingsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UserSettingsManager();
            }
            return instance;
        }
    }

    private Dictionary<string, UserSettings> settings = new Dictionary<string, UserSettings>();

    public void LoadUserSettings(List<UserSettings> data)
    {
        settings.Clear();
        if (data == null)
        {
            return;
        }

        foreach (var s in data)
        {
            settings[s.SettingKey] = s;
        }

        ApplyRuntimeSettings();
    }

    public void ApplyRuntimeSettings()
    {
        if (settings.TryGetValue(AppConstants.Setting.MUSIC, out UserSettings music)
            && int.TryParse(music.SettingValue, out int musicValue))
        {
            AudioManager.Instance?.SetMusicVolume(musicValue / 100f);
        }

        if (settings.TryGetValue(AppConstants.Setting.SFX, out UserSettings sfx)
            && int.TryParse(sfx.SettingValue, out int sfxValue))
        {
            AudioManager.Instance?.SetSfxVolume(sfxValue / 100f);
        }

        if (settings.TryGetValue(AppConstants.Setting.VOICE, out UserSettings voice)
            && int.TryParse(voice.SettingValue, out int voiceValue))
        {
            AudioManager.Instance?.SetVoiceVolume(voiceValue / 100f);
        }

        if (settings.TryGetValue(AppConstants.Setting.LANGUAGE, out UserSettings language)
            && !string.IsNullOrWhiteSpace(language.SettingValue))
        {
            LocalizationManager.LoadLocalization(language.SettingValue);
        }
    }

    public string GetString(string key)
    {
        return settings.ContainsKey(key) ? settings[key].SettingValue : null;
    }

    public int GetInt(string key)
    {
        return settings.ContainsKey(key) ? int.Parse(settings[key].SettingValue) : 0;
    }

    public float GetFloat(string key)
    {
        return settings.ContainsKey(key) ? float.Parse(settings[key].SettingValue) : 0f;
    }

    public void SetString(string key, string value)
    {
        if (!settings.ContainsKey(key))
        {
            settings[key] = new UserSettings
            {
                SettingKey = key,
                SettingValue = value,
                ValueType = "string"
            };
        }
        else
        {
            settings[key].SettingValue = value;
        }

        if (key == AppConstants.Setting.LANGUAGE && !string.IsNullOrWhiteSpace(value))
        {
            LocalizationManager.LoadLocalization(value);
        }
    }

    public void SetInt(string key, int value)
    {
        if (!settings.ContainsKey(key))
        {
            settings[key] = new UserSettings
            {
                SettingKey = key,
                SettingValue = value.ToString(),
                ValueType = "int"
            };
        }
        else
        {
            settings[key].SettingValue = value.ToString();
        }

        if (key == AppConstants.Setting.MUSIC)
        {
            AudioManager.Instance?.SetMusicVolume(value / 100f);
        }
        else if (key == AppConstants.Setting.SFX)
        {
            AudioManager.Instance?.SetSfxVolume(value / 100f);
        }
        else if (key == AppConstants.Setting.VOICE)
        {
            AudioManager.Instance?.SetVoiceVolume(value / 100f);
        }
    }

    public void SetFloat(string key, float value)
    {
        if (!settings.ContainsKey(key))
        {
            settings[key] = new UserSettings
            {
                SettingKey = key,
                SettingValue = value.ToString(),
                ValueType = "float"
            };
        }
        else
        {
            settings[key].SettingValue = value.ToString();
        }
    }
}

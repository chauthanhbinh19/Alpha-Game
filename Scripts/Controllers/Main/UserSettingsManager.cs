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
        foreach (var s in data)
        {
            settings[s.SettingKey] = s;
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

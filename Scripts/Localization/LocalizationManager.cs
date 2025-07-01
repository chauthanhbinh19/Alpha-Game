using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}

[System.Serializable]
public class LocalizationData
{
    public LocalizationItem[] items;
}

public static class LocalizationManager
{
    private static Dictionary<string, string> localizedText = new Dictionary<string, string>();
    private static bool isLoaded = false;

    public static void LoadLocalization(string languageCode = "en")
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Localization", languageCode + ".json");

        if (!File.Exists(filePath))
        {
            Debug.LogError("Không tìm thấy file: " + filePath);
            return;
        }

        string jsonText = File.ReadAllText(filePath);
        LocalizationItem[] items = JsonHelper.FromJson<LocalizationItem>(jsonText);

        localizedText.Clear();
        foreach (var item in items)
        {
            localizedText[item.key] = item.value;
        }

        isLoaded = true;
        // Debug.Log("Đã load " + localizedText.Count + " dòng từ " + languageCode + ".json");
    }

    public static string Get(string key)
    {
        if (!isLoaded) LoadLocalization();
        if (localizedText.ContainsKey(key))
            return localizedText[key];
        return key;
    }
}

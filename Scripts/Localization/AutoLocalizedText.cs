using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class AutoLocalizedText : MonoBehaviour
{
    public string key;
    void Start()
    {
        TMP_Text text = GetComponent<TMP_Text>();
        // string key = gameObject.name;
        text.text = LocalizationManager.Get(key);
    }
}

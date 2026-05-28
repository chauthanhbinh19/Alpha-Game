using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{
    public static void PopulateDropdown(TMP_Dropdown tmpDropdown, List<string> options, UnityAction<int> onValueChangedCallback = null)
    {
        if (tmpDropdown == null)
        {
            Debug.LogError("TMP_Dropdown is null.");
            return;
        }

        // Clear existing options and add the new ones
        tmpDropdown.ClearOptions();
        tmpDropdown.AddOptions(options);

        // Access the template of the dropdown
        RectTransform template = tmpDropdown.template;
        if (template == null)
        {
            Debug.LogError("Dropdown template not found.");
            return;
        }

        // Ensure the template isn't instantiated again
        template.gameObject.SetActive(true);

        // Force layout to update after modifying the content
        Transform content = template.Find("Viewport/Content");
        if (content != null)
        {
            // Force layout to update after modifying the content
            LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
        }

        // Hide the template again after adjustments
        template.gameObject.SetActive(false);
        // Remove any existing callbacks
        tmpDropdown.onValueChanged.RemoveAllListeners();

        // Add the provided callback
        if (onValueChangedCallback != null)
        {
            tmpDropdown.onValueChanged.AddListener(onValueChangedCallback);
        }
    }

    public static void AddDropdownListener(GameObject dropdownObject, System.Action<int> onSelectCallback)
    {
        if (dropdownObject == null)
        {
            Debug.LogError("Dropdown object is null.");
            return;
        }

        TMP_Dropdown tmpDropdown = dropdownObject.GetComponent<TMP_Dropdown>();
        Dropdown uiDropdown = dropdownObject.GetComponent<Dropdown>();

        if (tmpDropdown != null)
        {
            tmpDropdown.onValueChanged.AddListener(onSelectCallback.Invoke);
        }
        else if (uiDropdown != null)
        {
            uiDropdown.onValueChanged.AddListener(onSelectCallback.Invoke);
        }
        else
        {
            Debug.LogError("Dropdown component not found on the given object.");
        }
    }
}

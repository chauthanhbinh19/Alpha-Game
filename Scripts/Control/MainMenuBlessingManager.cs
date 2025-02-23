using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBlessingManager : MonoBehaviour
{
    private Transform MainPanel;
    private GameObject MainMenuBlessingPanelPrefab;
    private GameObject currentObject;
    // Start is called before the first frame update
    void Start()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MainMenuBlessingPanelPrefab = UIManager.Instance.GetGameObject("MainMenuBlessingPanelPrefab");
    }

     public void CreateMainMenuBlessingManager(object data){
        currentObject = Instantiate(MainMenuBlessingPanelPrefab, MainPanel);
        // TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        // SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        // UpLevelButton = currentObject.transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        // UpMaxLevelButton = currentObject.transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        CloseButton.onClick.AddListener(() => Destroy(currentObject));
    }
    public void Close(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}

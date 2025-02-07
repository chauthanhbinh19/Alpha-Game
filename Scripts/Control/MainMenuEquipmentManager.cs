using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuEquipmentManager : MonoBehaviour
{
    private Transform MainPanel;
    private GameObject MainMenuEquipmentPanelPrefab;
    private GameObject currentObject;
    // Start is called before the first frame update
    void Start()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MainMenuEquipmentPanelPrefab = UIManager.Instance.GetGameObject("MainMenuEquipmentPanelPrefab");
    }

    public void CreateMainMenuEquipmentManager(object data){
        currentObject = Instantiate(MainMenuEquipmentPanelPrefab, MainPanel);
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        CloseButton.onClick.AddListener(() => Destroy(currentObject));
        if (data is CardHeroes card)
        {

        }
        else if (data is Books books)
        {

        }
        else if (data is CardCaptains cardCaptains)
        {

        }
        else if (data is Pets pets)
        {

        }
        else if (data is CardMilitary cardMilitary)
        {

        }
        else if (data is CardSpell cardSpell)
        {

        }
        else if (data is CardMonsters cardMonsters)
        {

        }
        else if (data is CardColonels cardColonels)
        {

        }
        else if (data is CardGenerals cardGenerals)
        {

        }
        else if (data is CardAdmirals cardAdmirals)
        {

        }
    }
    public void Close(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}

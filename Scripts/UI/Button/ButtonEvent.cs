using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour
{
    public static ButtonEvent Instance { get; private set; }
    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    public void Close(Transform content)
    {
        if (content != null)
        {
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
        }
    }
    public void AssignButtonEvent(string buttonName, Transform panel, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = panel.Find(buttonName);
        if (buttonTransform != null)
        {
            Button button = buttonTransform.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(action);
            }
        }
        else
        {
            Debug.LogWarning($"Button {buttonName} not found!");
        }
    }
    public void AddClickListener(EventTrigger trigger, System.Action callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) => { callback(); });
        trigger.triggers.Add(entry);
    }
    public void AddCloseEvent(GameObject obj)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) =>
        {
            Destroy(obj);
        });
        trigger.triggers.Add(entry);
    }
    public void CheckLockedButton(object data, int value, GameObject button)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        RawImage buttonLockedImage = button.transform.Find("Locked")?.GetComponent<RawImage>();
        if (data is CardHeroes cardHeroes)
        {
            // mainId = cardHeroes.id;
            if (cardHeroes.level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is Books books)
        {
            // mainId = books.id;
            if (books.level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardCaptains cardCaptains)
        {
            // mainId = cardCaptains.id;
            if (cardCaptains.level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is Pets pets)
        {
            // mainId = pets.id;
            if (pets.level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardMilitary cardMilitary)
        {
            // mainId = cardMilitary.id;
            if (cardMilitary.level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardSpell cardSpell)
        {
            // mainId = cardSpell.id;
            if (cardSpell.level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardMonsters cardMonsters)
        {
            // mainId = cardMonsters.id;
            if (cardMonsters.level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardColonels cardColonels)
        {
            // mainId = cardColonels.id;
            if (cardColonels.level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardGenerals cardGenerals)
        {
            // mainId = cardGenerals.id;
            if (cardGenerals.level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            // mainId = cardAdmirals.id;
            if (cardAdmirals.level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is Equipments equipments)
        {
            // mainId = cardAdmirals.id;
            if (equipments.level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data.Equals("AnimeStats"))
        {
            if (User.CurrentUserLevel >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#FFFFFF");
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor("#7E7E7E");
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
    }
}
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
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(()=>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    action();
                });
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
        Transform transform = button.transform;
        RawImage buttonImage = transform.GetComponent<RawImage>();
        RawImage buttonLockedImage = transform.Find("Locked")?.GetComponent<RawImage>();
        if (data is CardHeroes cardHero)
        {
            // mainId = cardHeroes.id;
            if (cardHero.Level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.WHITE_COLOR);
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.DARK_GRAY_COLOR);
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is Books book)
        {
            // mainId = books.id;
            if (book.Level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.WHITE_COLOR);
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.DARK_GRAY_COLOR);
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardCaptains cardCaptain)
        {
            // mainId = cardCaptains.id;
            if (cardCaptain.Level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.WHITE_COLOR);
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.DARK_GRAY_COLOR);
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is Pets pet)
        {
            // mainId = pets.id;
            if (pet.Level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.WHITE_COLOR);
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.DARK_GRAY_COLOR);
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardMilitaries cardMilitary)
        {
            // mainId = cardMilitary.id;
            if (cardMilitary.Level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.WHITE_COLOR);
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.DARK_GRAY_COLOR);
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardSpells cardSpell)
        {
            // mainId = cardSpell.id;
            if (cardSpell.Level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.WHITE_COLOR);
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.DARK_GRAY_COLOR);
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardMonsters cardMonster)
        {
            // mainId = cardMonsters.id;
            if (cardMonster.Level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.WHITE_COLOR);
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.DARK_GRAY_COLOR);
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardColonels cardColonel)
        {
            // mainId = cardColonels.id;
            if (cardColonel.Level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.WHITE_COLOR);
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.DARK_GRAY_COLOR);
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardGenerals cardGeneral)
        {
            // mainId = cardGenerals.id;
            if (cardGeneral.Level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.WHITE_COLOR);
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.DARK_GRAY_COLOR);
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            // mainId = cardAdmirals.id;
            if (cardAdmiral.Level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.WHITE_COLOR);
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.DARK_GRAY_COLOR);
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data is Equipments equipment)
        {
            // mainId = cardAdmirals.id;
            if (equipment.Level >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.WHITE_COLOR);
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.DARK_GRAY_COLOR);
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
        else if (data.Equals("AnimeStats"))
        {
            if (User.CurrentUserLevel >= value)
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.WHITE_COLOR);
                buttonLockedImage.gameObject.SetActive(false);
            }
            else
            {
                buttonImage.color = ColorManager.Instance.HexToColor(ColorConstants.DARK_GRAY_COLOR);
                buttonLockedImage.gameObject.SetActive(true);
            }
        }
    }
}
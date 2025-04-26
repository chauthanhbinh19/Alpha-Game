using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuEquipmentManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private Transform SetPanel;
    private GameObject buttonPrefab;
    private GameObject MainMenuEquipmentPanelPrefab;
    private GameObject PopupEquipmentsPanelPrefab;
    private GameObject EquipmentsWearingPrefab;
    private GameObject currentObject;
    private GameObject slotObject;
    private GameObject Slot1Prefab;
    private GameObject Slot4Prefab;
    private GameObject Slot6Prefab;
    private GameObject Slot8Prefab;
    private GameObject Slot10Prefab;
    private GameObject Slot12Prefab;
    private GameObject Slot14Prefab;
    private GameObject Slot16Prefab;
    private GameObject popupEquipmentObject;
    private GameObject TabButton5;
    private GameObject StarPrefab;
    private RawImage mainImage;
    private string mainType;
    private int pageSize;
    private int offset;
    private int currentPage;
    private int totalPage;
    private string statusToggle;
    private string set;
    // Start is called before the first frame update
    void Start()
    {
        pageSize = 100;
        offset = 0;
        currentPage = 1;
        set = "set1";
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MainMenuEquipmentPanelPrefab = UIManager.Instance.GetGameObject("MainMenuEquipmentPanelPrefab");
        PopupEquipmentsPanelPrefab = UIManager.Instance.GetGameObject("PopupEquipmentsPanelPrefab");
        EquipmentsWearingPrefab = UIManager.Instance.GetGameObject("EquipmentsWearingPrefab");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        Slot1Prefab = UIManager.Instance.GetGameObject("Slot1Prefab");
        Slot4Prefab = UIManager.Instance.GetGameObject("Slot4Prefab");
        Slot6Prefab = UIManager.Instance.GetGameObject("Slot6Prefab");
        Slot8Prefab = UIManager.Instance.GetGameObject("Slot8Prefab");
        Slot10Prefab = UIManager.Instance.GetGameObject("Slot10Prefab");
        Slot12Prefab = UIManager.Instance.GetGameObject("Slot12Prefab");
        Slot14Prefab = UIManager.Instance.GetGameObject("Slot14Prefab");
        Slot16Prefab = UIManager.Instance.GetGameObject("Slot16Prefab");
        TabButton5 = UIManager.Instance.GetGameObject("TabButton5");
        StarPrefab = UIManager.Instance.GetGameObject("StarPrefab");
    }

    public void CreateMainMenuEquipmentManager(object data)
    {
        currentObject = Instantiate(MainMenuEquipmentPanelPrefab, MainPanel);
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = "Equipments";
        SetPanel = currentObject.transform.Find("DictionaryCards/SetGroup/Viewport/Content");
        mainImage = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        CloseButton.onClick.AddListener(() => Destroy(currentObject));

        List<string> uniqueTypes = Equipments.GetUniqueEquipmentsTypes();
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                // Tạo một nút mới từ prefab
                string subtype = uniqueTypes[i];
                GameObject button = Instantiate(buttonPrefab, TabButtonPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnButtonClick(button, data, subtype));

                if (i == 0)
                {
                    mainType = subtype;
                    ChangeButtonBackground(button, "Background_V4_166");
                    if (data is CardHeroes cardHeroes)
                    {
                        CreateCardHeroesEquipments(cardHeroes);
                    }
                    else if (data is Books books)
                    {
                        CreateBooksEquipments(books);
                    }
                    else if (data is CardCaptains cardCaptains)
                    {
                        CreateCardCaptainsEquipments(cardCaptains);
                    }
                    else if (data is Pets pets)
                    {
                        CreatePetsEquipments(pets);
                    }
                    else if (data is CardMilitary cardMilitary)
                    {
                        CreateCardMilitaryEquipments(cardMilitary);
                    }
                    else if (data is CardSpell cardSpell)
                    {
                        CreateCardSpellEquipments(cardSpell);
                    }
                    else if (data is CardMonsters cardMonsters)
                    {
                        CreateCardMonstersEquipments(cardMonsters);
                    }
                    else if (data is CardColonels cardColonels)
                    {
                        CreateCardColonelsEquipments(cardColonels);
                    }
                    else if (data is CardGenerals cardGenerals)
                    {
                        CreateCardGeneralsEquipments(cardGenerals);
                    }
                    else if (data is CardAdmirals cardAdmirals)
                    {
                        CreateCardAdmiralsEquipments(cardAdmirals);
                    }
                }
                else
                {
                    ChangeButtonBackground(button, "Background_V4_167");
                }
            }
        }
        CreateSetButton(data);
    }
    void OnButtonClick(GameObject clickedButton, object data, string type)
    {
        foreach (Transform child in TabButtonPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ChangeButtonBackground(button.gameObject, "Background_V4_167"); // Giả sử bạn có texture trắng
            }
        }

        mainType = type;
        ChangeButtonBackground(clickedButton, "Background_V4_166");
        CreateSetButton(data);
        if (data is CardHeroes cardHeroes)
        {
            CreateCardHeroesEquipments(cardHeroes);
        }
        else if (data is Books books)
        {
            CreateBooksEquipments(books);
        }
        else if (data is CardCaptains cardCaptains)
        {
            CreateCardCaptainsEquipments(cardCaptains);
        }
        else if (data is Pets pets)
        {
            CreatePetsEquipments(pets);
        }
        else if (data is CardMilitary cardMilitary)
        {
            CreateCardMilitaryEquipments(cardMilitary);
        }
        else if (data is CardSpell cardSpell)
        {
            CreateCardSpellEquipments(cardSpell);
        }
        else if (data is CardMonsters cardMonsters)
        {
            CreateCardMonstersEquipments(cardMonsters);
        }
        else if (data is CardColonels cardColonels)
        {
            CreateCardColonelsEquipments(cardColonels);
        }
        else if (data is CardGenerals cardGenerals)
        {
            CreateCardGeneralsEquipments(cardGenerals);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            CreateCardAdmiralsEquipments(cardAdmirals);
        }
    }
    private void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = Resources.Load<Texture>($"UI/Background4/{image}");
            if (texture != null)
            {
                buttonImage.texture = texture;
            }
            else
            {
                Debug.LogError($"Texture '{image}' not found in Resources.");
            }
        }
        else
        {
            Debug.LogError("Button does not have a RawImage component.");
        }
    }
    public void Close(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public void CreateSetButton(object data)
    {
        Close(SetPanel);
        List<string> uniqueSet = Equipments.GetEquipmentsSet(mainType);
        if (uniqueSet.Count > 0)
        {
            for (int i = 0; i < uniqueSet.Count; i++)
            {
                string subtype = uniqueSet[i];
                GameObject button = Instantiate(TabButton5, SetPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = subtype.Replace("set", "");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnSetButtonClick(button, data, subtype));
                if (i == 0)
                {
                    set = subtype;
                    ChangeButtonBackground(button, "Background_V4_332");
                }
                else
                {
                    ChangeButtonBackground(button, "Background_V4_259");
                }
            }
        }
    }
    void OnSetButtonClick(GameObject clickedButton, object data, string type)
    {
        foreach (Transform child in SetPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ChangeButtonBackground(button.gameObject, "Background_V4_259"); // Giả sử bạn có texture trắng
            }
        }

        set = type;
        ChangeButtonBackground(clickedButton, "Background_V4_332");
        // CreateSetButton();
        if (data is CardHeroes cardHeroes)
        {
            CreateCardHeroesEquipments(cardHeroes);
        }
        else if (data is Books books)
        {
            CreateBooksEquipments(books);
        }
        else if (data is CardCaptains cardCaptains)
        {
            CreateCardCaptainsEquipments(cardCaptains);
        }
        else if (data is Pets pets)
        {
            CreatePetsEquipments(pets);
        }
        else if (data is CardMilitary cardMilitary)
        {
            CreateCardMilitaryEquipments(cardMilitary);
        }
        else if (data is CardSpell cardSpell)
        {
            CreateCardSpellEquipments(cardSpell);
        }
        else if (data is CardMonsters cardMonsters)
        {
            CreateCardMonstersEquipments(cardMonsters);
        }
        else if (data is CardColonels cardColonels)
        {
            CreateCardColonelsEquipments(cardColonels);
        }
        else if (data is CardGenerals cardGenerals)
        {
            CreateCardGeneralsEquipments(cardGenerals);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            CreateCardAdmiralsEquipments(cardAdmirals);
        }
    }
    public int CheckSlotForEquipments(string type)
    {
        int slot = 0;
        switch (type)
        {
            case "Amnitus_Equipment":
                slot = 4;
                break;
            case "Angelis_Equipment":
                slot = 1;
                break;
            case "Bellion_Equipment":
                slot = 16;
                break;
            case "Benzamin_Equipment":
                slot = 4;
                break;
            case "Celestial_Equipment":
                slot = 4;
                break;
            case "Ceverus_Equipment":
                slot = 10;
                break;
            case "Delius_Equipment":
                slot = 10;
                break;
            case "Domitius_Equipment":
                slot = 8;
                break;
            case "Etherium_Equipment":
                slot = 10;
                break;
            case "Everlyn_Equipment":
                slot = 6;
                break;
            case "EvilFruit_Equipment":
                slot = 10;
                break;
            case "Extra_Equipment":
                slot = 4;
                break;
            case "Faltus_Equipment":
                slot = 16;
                break;
            case "Fealan_Equipment":
                slot = 16;
                break;
            case "Gamma_Equipment":
                slot = 8;
                break;
            case "Gem_Equipment":
                slot = 8;
                break;
            case "Hagoro_Equipment":
                slot = 6;
                break;
            case "Hakalite_Equipment":
                slot = 4;
                break;
            case "Heatherus_Equipment":
                slot = 16;
                break;
            case "Ignis_Equipment":
                slot = 16;
                break;
            case "Ivitus_Equipment":
                slot = 14;
                break;
            case "Karis_Equipment":
                slot = 8;
                break;
            case "Karmus_Equipment":
                slot = 8;
                break;
            case "Lotus_Equipment":
                slot = 16;
                break;
            case "Luminius_Equipment":
                slot = 1;
                break;
            case "Macus_Equipment":
                slot = 14;
                break;
            case "Morganis_Equipment":
                slot = 12;
                break;
            case "Mythical_Object":
                slot = 8;
                break;
            case "Nimigazin_Equipment":
                slot = 14;
                break;
            case "Nova_Equipment":
                slot = 4;
                break;
            case "Omonitus_Equipment":
                slot = 4;
                break;
            case "Pet_Equipment":
                slot = 3;
                break;
            case "Qiyantus_Equipment":
                slot = 1;
                break;
            case "Rainbow_Equipment":
                slot = 4;
                break;
            case "Redvenger_Equipment":
                slot = 6;
                break;
            case "Retanic_Equipment":
                slot = 4;
                break;
            case "Souls_Equipment":
                slot = 10;
                break;
            case "Support_Equipment":
                slot = 1;
                break;
            case "Syncroharon_Equipment":
                slot = 16;
                break;
            case "Uni_Equipment":
                slot = 16;
                break;
            case "Zodiac_Equipment":
                slot = 12;
                break;
            case "Zpower_Equipment":
                slot = 16;
                break;
            default:
                slot = 1;
                break;
        }
        return slot;
    }
    public void CreateCardHeroesEquipments(CardHeroes cardHeroes)
    {
        Close(SlotPanel);
        Equipments equipments = new Equipments();
        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = equipments.GetCardHeroesEquipments(User.CurrentUserId, cardHeroes.id, mainType);
        equipmentList = equipmentList.Where(e => e.set == set).ToList();
        string fileNameWithoutExtension = cardHeroes.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardHeroes, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot15Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot16Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public void CreateCardCaptainsEquipments(CardCaptains cardCaptains)
    {
        Close(SlotPanel);
        Equipments equipments = new Equipments();
        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = equipments.GetCardCaptainsEquipments(User.CurrentUserId, cardCaptains.id, mainType);
        equipmentList = equipmentList.Where(e => e.set == set).ToList();
        string fileNameWithoutExtension = cardCaptains.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardCaptains, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot15Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot16Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public void CreateCardColonelsEquipments(CardColonels cardColonels)
    {
        Close(SlotPanel);
        Equipments equipments = new Equipments();
        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = equipments.GetCardColonelsEquipments(User.CurrentUserId, cardColonels.id, mainType);
        equipmentList = equipmentList.Where(e => e.set == set).ToList();
        string fileNameWithoutExtension = cardColonels.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardColonels, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot15Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot16Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public void CreateCardGeneralsEquipments(CardGenerals cardGenerals)
    {
        Close(SlotPanel);
        Equipments equipments = new Equipments();
        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = equipments.GetCardGeneralsEquipments(User.CurrentUserId, cardGenerals.id, mainType);
        equipmentList = equipmentList.Where(e => e.set == set).ToList();
        string fileNameWithoutExtension = cardGenerals.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardGenerals, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot15Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot16Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public void CreateCardAdmiralsEquipments(CardAdmirals cardAdmirals)
    {
        Close(SlotPanel);
        Equipments equipments = new Equipments();
        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = equipments.GetCardAdmiralsEquipments(User.CurrentUserId, cardAdmirals.id, mainType);
        equipmentList = equipmentList.Where(e => e.set == set).ToList();
        string fileNameWithoutExtension = cardAdmirals.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardAdmirals, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot15Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot16Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public void CreateCardMonstersEquipments(CardMonsters cardMonsters)
    {
        Close(SlotPanel);
        Equipments equipments = new Equipments();
        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = equipments.GetCardMonstersEquipments(User.CurrentUserId, cardMonsters.id, mainType);
        equipmentList = equipmentList.Where(e => e.set == set).ToList();
        string fileNameWithoutExtension = cardMonsters.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardMonsters, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot15Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot16Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public void CreateCardMilitaryEquipments(CardMilitary cardMilitary)
    {
        Close(SlotPanel);
        Equipments equipments = new Equipments();
        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = equipments.GetCardMilitaryEquipments(User.CurrentUserId, cardMilitary.id, mainType);
        equipmentList = equipmentList.Where(e => e.set == set).ToList();
        string fileNameWithoutExtension = cardMilitary.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardMilitary, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot15Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot16Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public void CreateCardSpellEquipments(CardSpell cardSpell)
    {
        Close(SlotPanel);
        Equipments equipments = new Equipments();
        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = equipments.GetCardSpellEquipments(User.CurrentUserId, cardSpell.id, mainType);
        equipmentList = equipmentList.Where(e => e.set == set).ToList();
        string fileNameWithoutExtension = cardSpell.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardSpell, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot15Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot16Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public void CreateBooksEquipments(Books books)
    {
        Close(SlotPanel);
        Equipments equipments = new Equipments();
        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = equipments.GetBooksEquipments(User.CurrentUserId, books.id, mainType);
        equipmentList = equipmentList.Where(e => e.set == set).ToList();
        string fileNameWithoutExtension = books.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(books, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot15Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot16Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public void CreatePetsEquipments(Pets pets)
    {
        Close(SlotPanel);
        Equipments equipments = new Equipments();
        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = equipments.GetPetsEquipments(User.CurrentUserId, pets.id, mainType);
        equipmentList = equipmentList.Where(e => e.set == set).ToList();
        string fileNameWithoutExtension = pets.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(pets, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>()
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot15Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot16Button").GetComponent<Button>(),
            };
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public void ApplyEquipmentImage(object data, Button button, int position, List<Equipments> equipmentList)
    {
        bool foundEquipment = false;
        Equipments foundEquip = null;
        foreach (Equipments equipment in equipmentList)
        {
            if (equipment.position == position)
            {
                string fileNameWithoutExtension = equipment.image.Replace(".png", "").Replace(".jpg", "");
                Texture equipmentTexture = Resources.Load<Texture>(fileNameWithoutExtension);

                if (equipmentTexture != null)
                {
                    RawImage rawImage = button.GetComponent<RawImage>();
                    rawImage.texture = equipmentTexture;

                    TextMeshProUGUI LevelText = button.transform.Find("Level").GetComponent<TextMeshProUGUI>();
                    if (LevelText != null)
                    {
                        if (equipment.level != 0)
                        {
                            LevelText.text = equipment.level.ToString();
                        }
                    }
                    else
                    {
                        Debug.LogError("Không tìm thấy TextMeshProUGUI trong button: " + button.name);
                    }

                    Transform currentStar = button.transform.Find("Star");
                    CreateStarUI(equipment.star, currentStar);
                }

                foundEquipment = true; // Đánh dấu là đã tìm thấy thiết bị
                foundEquip = equipment; // Lưu lại equipment
                break;
            }
        }
        // Nếu không tìm thấy thiết bị nào, thêm sự kiện onClick
        if (!foundEquipment)
        {
            // button.onClick.RemoveAllListeners(); // Xóa các sự kiện trước đó (nếu có)
            button.onClick.AddListener(() =>
            {
                CreatePopupEquipments(data, position);
            });
        }else{
            // Đã tìm thấy equipment
            Equipments tempEquip = foundEquip;
            button.onClick.AddListener(() =>
            {
                FindObjectOfType<MainMenuDetailsManager>().PopupDetails(tempEquip, MainPanel);
            });
        }
    }
    public int CalculateTotalPages(int totalRecords, int pageSize)
    {
        if (pageSize <= 0) return 0; // Đảm bảo pageSize không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / pageSize);
    }
    public void CreatePopupEquipments(object data, int position, string statusToggle = "NOT EQUIP")
    {
        popupEquipmentObject = Instantiate(PopupEquipmentsPanelPrefab, MainPanel);
        Transform contentPanel = popupEquipmentObject.transform.Find("Scroll View/Viewport/Content");
        Text PageText = popupEquipmentObject.transform.Find("Pagination/Page").GetComponent<Text>();
        Toggle toggle = popupEquipmentObject.transform.Find("Toggle").GetComponent<Toggle>();
        toggle.isOn = (statusToggle == "ALL");
        toggle.onValueChanged.AddListener((bool isOn) =>
        {
            string newStatusToggle = isOn ? "ALL" : "NOT EQUIP";
            Destroy(popupEquipmentObject);
            CreatePopupEquipments(data, position, newStatusToggle); // Gọi lại nhưng giữ statusToggle mới
        });
        Button NextButton = popupEquipmentObject.transform.Find("Pagination/Next").GetComponent<Button>();
        Button PreviousButton = popupEquipmentObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        Button CloseButton = popupEquipmentObject.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => Destroy(popupEquipmentObject));
        Equipments equipments = new Equipments();
        List<Equipments> equipmentsList = new List<Equipments>();
        if (data is CardHeroes cardHeroes)
        {
            equipmentsList = equipments.GetAllCardHeroesEquipments(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardCaptains cardCaptains)
        {
            equipmentsList = equipments.GetAllCardCaptainsEquipments(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardColonels cardColonels)
        {
            equipmentsList = equipments.GetAllCardColonelsEquipments(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardGenerals cardGenerals)
        {
            equipmentsList = equipments.GetAllCardGeneralsEquipments(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            equipmentsList = equipments.GetAllCardAdmiralsEquipments(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardMonsters cardMonsters)
        {
            equipmentsList = equipments.GetAllCardMonstersEquipments(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardMilitary cardMilitary)
        {
            equipmentsList = equipments.GetAllCardMilitaryEquipments(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardSpell cardSpell)
        {
            equipmentsList = equipments.GetAllCardSpellEquipments(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is Books books)
        {
            equipmentsList = equipments.GetAllBooksEquipments(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is Pets pets)
        {
            equipmentsList = equipments.GetAllPetsEquipments(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        equipmentsList = equipmentsList.Where(e => e.set == set).ToList();
        int totalRecord = equipments.GetUserEquipmentsCount(User.CurrentUserId, mainType);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        CreatePopupEquipmentsUI(data, equipmentsList, contentPanel, position);
        NextButton.onClick.AddListener(() => { ChangeNextPage(data, PageText, contentPanel, mainType, position); });
        PreviousButton.onClick.AddListener(() => { ChangePreviousPage(data, PageText, contentPanel, mainType, position); });
    }
    public void CreatePopupEquipmentsUI(object data, List<Equipments> equipmentsList, Transform content, int position)
    {
        foreach (var equipment in equipmentsList)
        {
            GameObject equipmentObject = Instantiate(EquipmentsWearingPrefab, content);

            TextMeshProUGUI Title = equipmentObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = equipment.name.Replace("_", " ");

            TextMeshProUGUI Power = equipmentObject.transform.Find("PowerText").GetComponent<TextMeshProUGUI>();
            Power.text = equipment.power.ToString();

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = equipment.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.rare}");
            rareImage.texture = rareTexture;

            Button EquipButton = equipmentObject.transform.Find("EquipButton").GetComponent<Button>();
            EquipButton.onClick.AddListener(() =>
            {
                Destroy(popupEquipmentObject);
                if (data is CardHeroes cardHeroes)
                {
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    equipment.InsertCardHeroesEquipments(cardHeroes.id, equipment, position);
                    CreateCardHeroesEquipments(cardHeroes);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardCaptains cardCaptains)
                {
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    equipment.InsertCardCaptainsEquipments(cardCaptains.id, equipment, position);
                    CreateCardCaptainsEquipments(cardCaptains);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardColonels cardColonels)
                {
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    equipment.InsertCardColonelsEquipments(cardColonels.id, equipment, position);
                    CreateCardColonelsEquipments(cardColonels);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardGenerals cardGenerals)
                {
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    equipment.InsertCardGeneralsEquipments(cardGenerals.id, equipment, position);
                    CreateCardGeneralsEquipments(cardGenerals);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardAdmirals cardAdmirals)
                {
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    equipment.InsertCardAdmiralsEquipments(cardAdmirals.id, equipment, position);
                    CreateCardAdmiralsEquipments(cardAdmirals);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardMonsters cardMonsters)
                {
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    equipment.InsertCardMonstersEquipments(cardMonsters.id, equipment, position);
                    CreateCardMonstersEquipments(cardMonsters);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardMilitary cardMilitary)
                {
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    equipment.InsertCardMilitaryEquipments(cardMilitary.id, equipment, position);
                    CreateCardMilitaryEquipments(cardMilitary);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardSpell cardSpell)
                {
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    equipment.InsertCardSpellEquipments(cardSpell.id, equipment, position);
                    CreateCardSpellEquipments(cardSpell);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is Books books)
                {
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    equipment.InsertBooksEquipments(books.id, equipment, position);
                    CreateBooksEquipments(books);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is Pets pets)
                {
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    equipment.InsertPetsEquipments(pets.id, equipment, position);
                    CreatePetsEquipments(pets);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                }

                Destroy(popupEquipmentObject);
            });
        }
        GridLayoutGroup gridLayout = content.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(340, 130);
        }
    }
    public void ChangeNextPage(object data, Text PageText, Transform content, string subType, int position)
    {
        if (currentPage < totalPage)
        {
            Close(content);
            int totalRecord = 0;

            if (data is CardHeroes cardHeroes)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardHeroesEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardCaptains cardCaptains)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardCaptainsEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardColonels cardColonels)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardColonelsEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardGenerals cardGenerals)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardGeneralsEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardAdmirals cardAdmirals)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardAdmiralsEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMonsters cardMonsters)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardMonstersEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMilitary cardMilitary)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardMilitaryEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardSpell cardSpell)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardSpellEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Books books)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllBooksEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Pets pets)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllPetsEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ChangePreviousPage(object data, Text PageText, Transform content, string subType, int position)
    {
        if (currentPage > 1)
        {
            Close(content);
            int totalRecord = 0;

            if (data is CardHeroes cardHeroes)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardHeroesEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardCaptains cardCaptains)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardCaptainsEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardColonels cardColonels)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardColonelsEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardGenerals cardGenerals)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardGeneralsEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardAdmirals cardAdmirals)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardAdmiralsEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMonsters cardMonsters)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardMonstersEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMilitary cardMilitary)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardMilitaryEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardSpell cardSpell)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardSpellEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Books books)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetAllBooksEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Pets pets)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(User.CurrentUserId, subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetAllPetsEquipments(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }


            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void CreateStarUI(int star, Transform currentStar)
    {
        int imageIndex = (star == 0) ? 0 : ((star - 1) % 10) + 1;
        int starIndex = (star == 0) ? 1 : (star - 1) / 10;
        for (int i = 0; i < imageIndex; i++)
        {
            GameObject starObject = Instantiate(StarPrefab, currentStar);

            RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
            GetStarImage(starImage, starIndex);
        }
        GridLayoutGroup GridLayout = currentStar.GetComponent<GridLayoutGroup>();
        if (GridLayout != null)
        {
            GridLayout.cellSize = new Vector2(20, 20);
        }
    }
    public void GetStarImage(RawImage starImage, int starIndex)
    {
        Texture starTexture = Resources.Load<Texture>($"UI/UI/Star1");
        switch (starIndex)
        {
            case 0:
                starTexture = Resources.Load<Texture>($"UI/UI/Star1");
                starImage.texture = starTexture;
                break;
            case 1:
                starTexture = Resources.Load<Texture>($"UI/UI/Star2");
                starImage.texture = starTexture;
                break;
            case 2:
                starTexture = Resources.Load<Texture>($"UI/UI/Star3");
                starImage.texture = starTexture;
                break;
            case 3:
                starTexture = Resources.Load<Texture>($"UI/UI/Star4");
                starImage.texture = starTexture;
                break;
            case 4:
                starTexture = Resources.Load<Texture>($"UI/UI/Star5");
                starImage.texture = starTexture;
                break;
            case 5:
                starTexture = Resources.Load<Texture>($"UI/UI/Star6");
                starImage.texture = starTexture;
                break;
            case 6:
                starTexture = Resources.Load<Texture>($"UI/UI/Star7");
                starImage.texture = starTexture;
                break;
            case 7:
                starTexture = Resources.Load<Texture>($"UI/UI/Star8");
                starImage.texture = starTexture;
                break;
            case 8:
                starTexture = Resources.Load<Texture>($"UI/UI/Star9");
                starImage.texture = starTexture;
                break;
            case 9:
                starTexture = Resources.Load<Texture>($"UI/UI/Star10");
                starImage.texture = starTexture;
                break;
            default:
                starTexture = Resources.Load<Texture>($"UI/UI/Star1");
                starImage.texture = starTexture;
                break;
        }
    }
}

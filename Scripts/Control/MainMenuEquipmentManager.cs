using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuEquipmentManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
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
    private RawImage mainImage;
    private int mainId;
    private string mainType;
    private int pageSize;
    private int offset;
    private int currentPage;
    private int totalPage;
    // Start is called before the first frame update
    void Start()
    {
        pageSize = 100;
        offset = 0;
        currentPage = 1;
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
    }

    public void CreateMainMenuEquipmentManager(object data)
    {
        currentObject = Instantiate(MainMenuEquipmentPanelPrefab, MainPanel);
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
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
                        mainId = cardHeroes.id;
                        CreateCardHeroesEquipments(cardHeroes);
                    }
                    else if (data is Books books)
                    {
                        mainId = books.id;
                        CreateBooksEquipments(books);
                    }
                    else if (data is CardCaptains cardCaptains)
                    {
                        mainId = cardCaptains.id;
                        CreateCardCaptainsEquipments(cardCaptains);
                    }
                    else if (data is Pets pets)
                    {
                        mainId = pets.id;
                        CreatePetsEquipments(pets);
                    }
                    else if (data is CardMilitary cardMilitary)
                    {
                        mainId = cardMilitary.id;
                        CreateCardMilitaryEquipments(cardMilitary);
                    }
                    else if (data is CardSpell cardSpell)
                    {
                        mainId = cardSpell.id;
                        CreateCardSpellEquipments(cardSpell);
                    }
                    else if (data is CardMonsters cardMonsters)
                    {
                        mainId = cardMonsters.id;
                        CreateCardMonstersEquipments(cardMonsters);
                    }
                    else if (data is CardColonels cardColonels)
                    {
                        mainId = cardColonels.id;
                        CreateCardColonelsEquipments(cardColonels);
                    }
                    else if (data is CardGenerals cardGenerals)
                    {
                        mainId = cardGenerals.id;
                        CreateCardGeneralsEquipments(cardGenerals);
                    }
                    else if (data is CardAdmirals cardAdmirals)
                    {
                        mainId = cardAdmirals.id;
                        CreateCardAdmiralsEquipments(cardAdmirals);
                    }
                }
                else
                {
                    ChangeButtonBackground(button, "Background_V4_167");
                }
            }
        }
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

        if (data is CardHeroes cardHeroes)
        {
            mainId = cardHeroes.id;
            CreateCardHeroesEquipments(cardHeroes);
        }
        else if (data is Books books)
        {
            mainId = books.id;
            CreateBooksEquipments(books);
        }
        else if (data is CardCaptains cardCaptains)
        {
            mainId = cardCaptains.id;
            CreateCardCaptainsEquipments(cardCaptains);
        }
        else if (data is Pets pets)
        {
            mainId = pets.id;
            CreatePetsEquipments(pets);
        }
        else if (data is CardMilitary cardMilitary)
        {
            mainId = cardMilitary.id;
            CreateCardMilitaryEquipments(cardMilitary);
        }
        else if (data is CardSpell cardSpell)
        {
            mainId = cardSpell.id;
            CreateCardSpellEquipments(cardSpell);
        }
        else if (data is CardMonsters cardMonsters)
        {
            mainId = cardMonsters.id;
            CreateCardMonstersEquipments(cardMonsters);
        }
        else if (data is CardColonels cardColonels)
        {
            mainId = cardColonels.id;
            CreateCardColonelsEquipments(cardColonels);
        }
        else if (data is CardGenerals cardGenerals)
        {
            mainId = cardGenerals.id;
            CreateCardGeneralsEquipments(cardGenerals);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            mainId = cardAdmirals.id;
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
        equipmentList = equipments.GetCardHeroesEquipments(cardHeroes.id, mainType);
        string fileNameWithoutExtension = cardHeroes.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardHeroes, EquipmentSlot1Button, 1, equipmentList);
            mainImage.gameObject.SetActive(false);
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
        equipmentList = equipments.GetCardCaptainsEquipments(cardCaptains.id, mainType);
        string fileNameWithoutExtension = cardCaptains.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardCaptains, EquipmentSlot1Button, 1, equipmentList);
            mainImage.gameObject.SetActive(false);
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
        equipmentList = equipments.GetCardColonelsEquipments(cardColonels.id, mainType);
        string fileNameWithoutExtension = cardColonels.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardColonels, EquipmentSlot1Button, 1, equipmentList);
            mainImage.gameObject.SetActive(false);
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
        equipmentList = equipments.GetCardGeneralsEquipments(cardGenerals.id, mainType);
        string fileNameWithoutExtension = cardGenerals.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardGenerals, EquipmentSlot1Button, 1, equipmentList);
            mainImage.gameObject.SetActive(false);
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
        equipmentList = equipments.GetCardAdmiralsEquipments(cardAdmirals.id, mainType);
        string fileNameWithoutExtension = cardAdmirals.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardAdmirals, EquipmentSlot1Button, 1, equipmentList);
            mainImage.gameObject.SetActive(false);
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
        equipmentList = equipments.GetCardMonstersEquipments(cardMonsters.id, mainType);
        string fileNameWithoutExtension = cardMonsters.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardMonsters, EquipmentSlot1Button, 1, equipmentList);
            mainImage.gameObject.SetActive(false);
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
        equipmentList = equipments.GetCardMilitaryEquipments(cardMilitary.id, mainType);
        string fileNameWithoutExtension = cardMilitary.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardMilitary, EquipmentSlot1Button, 1, equipmentList);
            mainImage.gameObject.SetActive(false);
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
        equipmentList = equipments.GetCardSpellEquipments(cardSpell.id, mainType);
        string fileNameWithoutExtension = cardSpell.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardSpell, EquipmentSlot1Button, 1, equipmentList);
            mainImage.gameObject.SetActive(false);
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
        equipmentList = equipments.GetBooksEquipments(books.id, mainType);
        string fileNameWithoutExtension = books.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(books, EquipmentSlot1Button, 1, equipmentList);
            mainImage.gameObject.SetActive(false);
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
        equipmentList = equipments.GetPetsEquipments(pets.id, mainType);
        string fileNameWithoutExtension = pets.image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(pets, EquipmentSlot1Button, 1, equipmentList);
            mainImage.gameObject.SetActive(false);
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
                }

                foundEquipment = true; // Đánh dấu là đã tìm thấy thiết bị
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
        }
    }
    public int CalculateTotalPages(int totalRecords, int pageSize)
    {
        if (pageSize <= 0) return 0; // Đảm bảo pageSize không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / pageSize);
    }
    public void CreatePopupEquipments(object data, int position)
    {
        popupEquipmentObject = Instantiate(PopupEquipmentsPanelPrefab, MainPanel);
        Transform contentPanel = popupEquipmentObject.transform.Find("Scroll View/Viewport/Content");
        Text PageText = popupEquipmentObject.transform.Find("Pagination/Page").GetComponent<Text>();
        Button NextButton = popupEquipmentObject.transform.Find("Pagination/Next").GetComponent<Button>();
        Button PreviousButton = popupEquipmentObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        Button CloseButton = popupEquipmentObject.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => Destroy(popupEquipmentObject));
        Equipments equipments = new Equipments();
        List<Equipments> equipmentsList = new List<Equipments>();
        if (data is CardHeroes cardHeroes)
        {
            equipmentsList = equipments.GetAllCardHeroesEquipments(mainType, pageSize, offset);
        }
        else if (data is CardCaptains cardCaptains)
        {
            equipmentsList = equipments.GetAllCardCaptainsEquipments(mainType, pageSize, offset);
        }
        else if (data is CardColonels cardColonels)
        {
            equipmentsList = equipments.GetAllCardColonelsEquipments(mainType, pageSize, offset);
        }
        else if (data is CardGenerals cardGenerals)
        {
            equipmentsList = equipments.GetAllCardGeneralsEquipments(mainType, pageSize, offset);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            equipmentsList = equipments.GetAllCardAdmiralsEquipments(mainType, pageSize, offset);
        }
        else if (data is CardMonsters cardMonsters)
        {
            equipmentsList = equipments.GetAllCardMonstersEquipments(mainType, pageSize, offset);
        }
        else if (data is CardMilitary cardMilitary)
        {
            equipmentsList = equipments.GetAllCardMilitaryEquipments(mainType, pageSize, offset);
        }
        else if (data is CardSpell cardSpell)
        {
            equipmentsList = equipments.GetAllCardSpellEquipments(mainType, pageSize, offset);
        }
        else if (data is Books books)
        {
            equipmentsList = equipments.GetAllBooksEquipments(mainType, pageSize, offset);
        }
        else if (data is Pets pets)
        {
            equipmentsList = equipments.GetAllPetsEquipments(mainType, pageSize, offset);
        }
        int totalRecord = equipments.GetUserEquipmentsCount(mainType);
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
                    PowerManager powerManager = new PowerManager();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower();
                    equipment.InsertCardHeroesEquipments(mainId, equipment, position);
                    CreateCardHeroesEquipments(cardHeroes);
                    double newPower = teams.GetTeamsPower();
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower-currentPower, 1);
                }
                else if (data is CardCaptains cardCaptains)
                {
                    PowerManager powerManager = new PowerManager();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower();
                    equipment.InsertCardCaptainsEquipments(mainId, equipment, position);
                    CreateCardCaptainsEquipments(cardCaptains);
                    double newPower = teams.GetTeamsPower();
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower-currentPower, 1);
                }
                else if (data is CardColonels cardColonels)
                {
                    PowerManager powerManager = new PowerManager();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower();
                    equipment.InsertCardColonelsEquipments(mainId, equipment, position);
                    CreateCardColonelsEquipments(cardColonels);
                    double newPower = teams.GetTeamsPower();
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower-currentPower, 1);
                }
                else if (data is CardGenerals cardGenerals)
                {
                    PowerManager powerManager = new PowerManager();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower();
                    equipment.InsertCardGeneralsEquipments(mainId, equipment, position);
                    CreateCardGeneralsEquipments(cardGenerals);
                    double newPower = teams.GetTeamsPower();
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower-currentPower, 1);
                }
                else if (data is CardAdmirals cardAdmirals)
                {
                    PowerManager powerManager = new PowerManager();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower();
                    equipment.InsertCardAdmiralsEquipments(mainId, equipment, position);
                    CreateCardAdmiralsEquipments(cardAdmirals);
                    double newPower = teams.GetTeamsPower();
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower-currentPower, 1);
                }
                else if (data is CardMonsters cardMonsters)
                {
                    PowerManager powerManager = new PowerManager();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower();
                    equipment.InsertCardMonstersEquipments(mainId, equipment, position);
                    CreateCardMonstersEquipments(cardMonsters);
                    double newPower = teams.GetTeamsPower();
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower-currentPower, 1);
                }
                else if (data is CardMilitary cardMilitary)
                {
                    PowerManager powerManager = new PowerManager();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower();
                    equipment.InsertCardMilitaryEquipments(mainId, equipment, position);
                    CreateCardMilitaryEquipments(cardMilitary);
                    double newPower = teams.GetTeamsPower();
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower-currentPower, 1);
                }
                else if (data is CardSpell cardSpell)
                {
                    PowerManager powerManager = new PowerManager();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower();
                    equipment.InsertCardSpellEquipments(mainId, equipment, position);
                    CreateCardSpellEquipments(cardSpell);
                    double newPower = teams.GetTeamsPower();
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower-currentPower, 1);
                }
                else if (data is Books books)
                {
                    PowerManager powerManager = new PowerManager();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower();
                    equipment.InsertBooksEquipments(mainId, equipment, position);
                    CreateBooksEquipments(books);
                    double newPower = teams.GetTeamsPower();
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower-currentPower, 1);
                }
                else if (data is Pets pets)
                {
                    PowerManager powerManager = new PowerManager();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower();
                    equipment.InsertPetsEquipments(mainId, equipment, position);
                    CreatePetsEquipments(pets);
                    double newPower = teams.GetTeamsPower();
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower-currentPower, 1);
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
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardCaptainsEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardCaptains cardCaptains)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardCaptainsEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardColonels cardColonels)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardColonelsEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardGenerals cardGenerals)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardGeneralsEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardAdmirals cardAdmirals)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardAdmiralsEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMonsters cardMonsters)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardMonstersEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMilitary cardMilitary)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardMilitaryEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardSpell cardSpell)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardSpellEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Books books)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllBooksEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Pets pets)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllPetsEquipments(subType, pageSize, offset);
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
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardHeroesEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardCaptains cardCaptains)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardCaptainsEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardColonels cardColonels)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardColonelsEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardGenerals cardGenerals)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardGeneralsEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardAdmirals cardAdmirals)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardAdmiralsEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMonsters cardMonsters)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardMonstersEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMilitary cardMilitary)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardMilitaryEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardSpell cardSpell)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllCardSpellEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Books books)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllBooksEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Pets pets)
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetUserEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetAllPetsEquipments(subType, pageSize, offset);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }


            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
}

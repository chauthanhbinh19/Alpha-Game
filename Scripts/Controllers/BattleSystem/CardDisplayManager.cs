using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class CardDisplayManager : MonoBehaviour, ICardDisplayManager
{
    [SerializeField] public Transform PlayerCardsField;
    [SerializeField] public Transform EnemyCardsField;
    [SerializeField] public CardSlot[] AllySlots = new CardSlot[10]; // 5 vị trí cho phe mình
    [SerializeField] public CardSlot[] EnemySlots = new CardSlot[10]; // 5 vị trí cho phe địch
    [SerializeField] public GameObject CardContractPrefab;
    [SerializeField] public GameObject CardPenaltyPrefab;
    [SerializeField] public Transform CardPanel;
    [SerializeField] public GameObject CardModelPrefab;
    void Start()
    {

    }
    // ==========================================================
    // CƠ CHẾ TỰ ĐỘNG ĐIỀN CHỈ TRONG EDITOR
    // ==========================================================

    // Hàm này chạy trong Editor (khi bạn thay đổi script, thay đổi giá trị...)
    // Hoặc khi bạn click vào nút "Reset" trong Inspector
    private void OnValidate()
    {
        // Tự động điền mảng khi script được cập nhật trong Editor
        AutoFillSlots();
    }

    private void AutoFillSlots()
    {
        if (PlayerCardsField != null)
        {
            AllySlots = GetSlotsFromParent(PlayerCardsField);
        }

        if (EnemyCardsField != null)
        {
            EnemySlots = GetSlotsFromParent(EnemyCardsField);
        }
    }

    private CardSlot[] GetSlotsFromParent(Transform parent)
    {
        // Lấy tất cả các con (CardPositionX)
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < parent.childCount; i++)
        {
            children.Add(parent.GetChild(i));
        }

        // Tạo mảng CardSlotReference
        CardSlot[] slots = new CardSlot[children.Count];

        for (int i = 0; i < children.Count; i++)
        {
            slots[i] = new CardSlot
            {
                // Gán GameObject con vào tham chiếu
                positionObject = children[i].gameObject,
                // Gán Slot Index theo thứ tự (bắt đầu từ 1)
                slotIndex = i + 1
            };
        }
        return slots;
    }
    public void AssignCardsToAllySlots(List<CardBase> cardsToPlace)
    {
        if(cardsToPlace.Count == 0)
        {
            return;
        }
        foreach (var card in cardsToPlace)
        {
            // Lấy vị trí MainPosition từ CardBase (đã được ánh xạ từ Entity Position "x-y")
            // *Lưu ý: Nếu MainPosition trong CardBase là 1-based (1, 2, 3...), 
            //         thì slotIndex của CardSlot cũng phải là 1-based (như hình bạn cung cấp)
            int cardMainPosition = card.MainPosition;

            // Tìm Slot tương ứng với MainPosition
            CardSlot targetSlot = AllySlots
                .FirstOrDefault(slot => slot.slotIndex == cardMainPosition);

            if (targetSlot != null && targetSlot.positionObject != null)
            {
                // A. INSTANTIATE PREFAB MODEL CARD
                GameObject cardInstance = Instantiate(CardModelPrefab, targetSlot.positionObject.transform);

                // Đặt vị trí chính xác trong Slot (ví dụ: ở gốc (0,0,0) của Slot)
                cardInstance.transform.localPosition = new Vector3(-5f, 10f, -10f);
                cardInstance.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                cardInstance.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

                Transform mirrorTransform = cardInstance.transform.Find("Image");

                if (mirrorTransform != null)
                {
                    // 2. Lấy thành phần Renderer từ GameObject "Mirror"
                    Renderer mirrorRenderer = mirrorTransform.GetComponent<Renderer>();

                    if (mirrorRenderer != null)
                    {
                        // 3. Tải Texture từ đường dẫn (được lưu trong card.Image)
                        Texture newTexture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(card.Image)); // *LƯU Ý: Đảm bảo đường dẫn (path) trong card.Image là chính xác*

                        if (newTexture != null)
                        {
                            // 4. Gán Texture vào Material (sử dụng mainTexture hoặc một thuộc tính shader cụ thể)
                            // Lấy Material đầu tiên (hoặc Material chính)
                            Material targetMaterial = mirrorRenderer.material;

                            // Gán Texture vào thuộc tính chính của Shader (thường là "_MainTex")
                            targetMaterial.mainTexture = newTexture;

                            // HOẶC nếu bạn cần gán cho một thuộc tính shader khác (ví dụ: Texture ánh sáng):
                            // targetMaterial.SetTexture("_EmissionMap", newTexture);
                        }
                        else
                        {
                            Debug.LogError($"Không thể tải Texture từ đường dẫn: {card.Image}");
                        }
                    }
                    else
                    {
                        Debug.LogError("GameObject 'Mirror' không có component Renderer.");
                    }
                }
                else
                {
                    Debug.LogError("Không tìm thấy GameObject con tên là 'Mirror' trong CardModelPrefab.");
                }

                // B. GẮN ĐỐI TƯỢNG LOGIC VÀO GAMEOBJECT VỪA TẠO
                // *Giả sử Prefab CardModelPrefab có script CardVisual/CardController*

                // Lấy script quản lý model từ GameObject
                // CardVisualController visualController = cardInstance.GetComponent<CardVisualController>();
                // if (visualController != null)
                // {
                //     // Gán dữ liệu CardBase logic vào Model
                //     visualController.Initialize(card); 
                // }

                // C. Cập nhật trạng thái Slot (Nếu bạn có component CardSlot trên positionObject)
                // targetSlot.positionObject.GetComponent<CardSlotComponent>().PlaceCard(card);
            }
            else
            {
                Debug.LogWarning($"Không tìm thấy Slot với index {cardMainPosition} hoặc Slot Object bị null.");
            }
        }
    }
    public void AssignCardsToEnemySlots(List<CardBase> cardsToPlace)
    {
        if(cardsToPlace.Count == 0)
        {
            return;
        }
        foreach (var card in cardsToPlace)
        {
            // Lấy vị trí MainPosition từ CardBase (đã được ánh xạ từ Entity Position "x-y")
            // *Lưu ý: Nếu MainPosition trong CardBase là 1-based (1, 2, 3...), 
            //         thì slotIndex của CardSlot cũng phải là 1-based (như hình bạn cung cấp)
            int cardMainPosition = card.MainPosition;

            // Tìm Slot tương ứng với MainPosition
            CardSlot targetSlot = EnemySlots
                .FirstOrDefault(slot => slot.slotIndex == cardMainPosition);

            if (targetSlot != null && targetSlot.positionObject != null)
            {
                // A. INSTANTIATE PREFAB MODEL CARD
                GameObject cardInstance = Instantiate(CardModelPrefab, targetSlot.positionObject.transform);

                // Đặt vị trí chính xác trong Slot (ví dụ: ở gốc (0,0,0) của Slot)
                cardInstance.transform.localPosition = new Vector3(-5f, 10f, -10f);
                cardInstance.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                cardInstance.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

                Transform mirrorTransform = cardInstance.transform.Find("Image");

                if (mirrorTransform != null)
                {
                    // 2. Lấy thành phần Renderer từ GameObject "Mirror"
                    Renderer mirrorRenderer = mirrorTransform.GetComponent<Renderer>();

                    if (mirrorRenderer != null)
                    {
                        // 3. Tải Texture từ đường dẫn (được lưu trong card.Image)
                        Texture newTexture = Resources.Load<Texture>(ImageExtensionHandler.RemoveImageExtension(card.Image)); // *LƯU Ý: Đảm bảo đường dẫn (path) trong card.Image là chính xác*

                        if (newTexture != null)
                        {
                            // 4. Gán Texture vào Material (sử dụng mainTexture hoặc một thuộc tính shader cụ thể)
                            // Lấy Material đầu tiên (hoặc Material chính)
                            Material targetMaterial = mirrorRenderer.material;

                            // Gán Texture vào thuộc tính chính của Shader (thường là "_MainTex")
                            targetMaterial.mainTexture = newTexture;

                            // HOẶC nếu bạn cần gán cho một thuộc tính shader khác (ví dụ: Texture ánh sáng):
                            // targetMaterial.SetTexture("_EmissionMap", newTexture);
                        }
                        else
                        {
                            Debug.LogError($"Không thể tải Texture từ đường dẫn: {card.Image}");
                        }
                    }
                    else
                    {
                        Debug.LogError("GameObject 'Mirror' không có component Renderer.");
                    }
                }
                else
                {
                    Debug.LogError("Không tìm thấy GameObject con tên là 'Mirror' trong CardModelPrefab.");
                }

                // B. GẮN ĐỐI TƯỢNG LOGIC VÀO GAMEOBJECT VỪA TẠO
                // *Giả sử Prefab CardModelPrefab có script CardVisual/CardController*

                // Lấy script quản lý model từ GameObject
                // CardVisualController visualController = cardInstance.GetComponent<CardVisualController>();
                // if (visualController != null)
                // {
                //     // Gán dữ liệu CardBase logic vào Model
                //     visualController.Initialize(card); 
                // }

                // C. Cập nhật trạng thái Slot (Nếu bạn có component CardSlot trên positionObject)
                // targetSlot.positionObject.GetComponent<CardSlotComponent>().PlaceCard(card);
            }
            else
            {
                Debug.LogWarning($"Không tìm thấy Slot với index {cardMainPosition} hoặc Slot Object bị null.");
            }
        }
    }
    public void DisplayCardContract(CardContract cardContract)
    {
        if (CardContractPrefab == null)
        {
            Debug.LogError("Prefab chưa được load/gán.");
            return;
        }
        if (CardPanel == null)
        {
            Debug.LogError("MainPanel chưa được load/gán.");
            return;
        }

        GameObject contractDisplayObject = Instantiate(CardContractPrefab, CardPanel);
        TextMeshProUGUI titleText = contractDisplayObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        titleText.name = cardContract.Name;

        // AudioManager.Instance.PlaySFX(AudioConstants.SFX.APPEAR);

        contractDisplayObject.transform.localPosition = Vector3.zero;
        contractDisplayObject.transform.localScale = Vector3.one;

        Transform positionContent = contractDisplayObject.transform.Find("PositionContent");

        if (positionContent == null)
        {
            Debug.LogError("Không tìm thấy PositionContent trong CardContractPrefab.");
            return;
        }

        foreach (var position in cardContract.Positions.OrderBy(p => p.Index))
        {
            string positionName = $"Position{position.Index}";

            // 4. Tìm RawImage tương ứng
            Transform positionImageTransform = positionContent.Find(positionName);

            if (positionImageTransform != null)
            {
                RawImage rawImage = positionImageTransform.GetComponent<RawImage>();

                if (rawImage != null)
                {
                    // 5. Lấy màu dựa trên CardType
                    string requiredImage = CardImageMapper.GetImage(position.Type);

                    // 6. Set màu cho RawImage
                    rawImage.texture = Resources.Load<Texture>(requiredImage);
                    // Debug.Log($"Set màu cho {positionName} ({position.Type}) thành {requiredImage}");
                }
                else
                {
                    Debug.LogWarning($"Object {positionName} không có RawImage component.");
                }
            }
            else
            {
                Debug.LogWarning($"Không tìm thấy object {positionName} trong PositionContent.");
            }
        }
    }
    public void DisplayCardPenalty(CardPenalty cardPenalty)
    {
        if (CardPenaltyPrefab == null)
        {
            Debug.LogError("Prefab chưa được load/gán.");
            return;
        }
        if (CardPanel == null)
        {
            Debug.LogError("MainPanel chưa được load/gán.");
            return;
        }

        GameObject penaltyDisplayObject = Instantiate(CardPenaltyPrefab, CardPanel);
        TextMeshProUGUI titleText = penaltyDisplayObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        titleText.name = cardPenalty.Name;

        // AudioManager.Instance.PlaySFX(AudioConstants.SFX.APPEAR);

        penaltyDisplayObject.transform.localPosition = Vector3.zero;
        penaltyDisplayObject.transform.localScale = Vector3.one;

        Transform orderContent = penaltyDisplayObject.transform.Find("OrderContent");

        if (orderContent == null)
        {
            Debug.LogError("Không tìm thấy OrderContent trong CardPenaltyPrefab.");
            return;
        }

        List<CardPenaltyItem> orderedPenalties = cardPenalty.Penalties
            .OrderBy(item => item.PenaltyValue)
            .Take(7) // Chỉ lấy 7 mục đầu tiên
            .ToList();

        for (int i = 0; i < orderedPenalties.Count; i++)
        {
            CardPenaltyItem penaltyItem = orderedPenalties[i];
            
            // Tên đối tượng UI sẽ là Order1, Order2, ..., Order7
            string orderName = $"Order{i + 1}"; 

            // 5. Tìm component TextMeshProUGUI tương ứng
            // Giả định TextMeshProUGUI là component chính của Order1, Order2...
            // HOẶC là con của Order1
            Transform orderTransform = orderContent.Find(orderName);
            
            if (orderTransform != null)
            {
                // Tìm TextMeshProUGUI
                TextMeshProUGUI tmpText = orderTransform.GetComponent<TextMeshProUGUI>();
                
                // Nếu OrderX là một GameObject chứa Text (con của nó)
                if (tmpText == null) 
                {
                    tmpText = orderTransform.GetComponentInChildren<TextMeshProUGUI>();
                }


                if (tmpText != null)
                {
                    // 6. Lấy chuỗi hiển thị tên thẻ dựa trên CardType
                    string cardTypeName = LocalizationManager.Get(GetCardTypeName(penaltyItem.Type)).Replace("Card ","");

                    // 7. Format và Set text
                    tmpText.text = $"{cardTypeName}: {penaltyItem.PenaltyValue}";
                    
                    // Debug.Log($"Hiển thị Penalty cho {orderName}: {tmpText.text}");
                }
                else
                {
                    Debug.LogWarning($"Object {orderName} không có component TextMeshProUGUI.");
                }
            }
            else
            {
                Debug.LogWarning($"Không tìm thấy object {orderName} trong OrderContent.");
            }
        }
    }
    private string GetCardTypeName(CardType type)
    {
        // Bạn cần tự điền logic ánh xạ này dựa trên enum CardType của mình
        switch (type)
        {
            case CardType.CardHero:
                return AppDisplayConstants.MainType.CARD_HERO;
            case CardType.CardCaptain:
                return AppDisplayConstants.MainType.CARD_CAPTAIN;
            case CardType.CardColonel:
                return AppDisplayConstants.MainType.CARD_COLONEL;
            case CardType.CardGeneral:
                return AppDisplayConstants.MainType.CARD_GENERAL;
            case CardType.CardAdmiral:
                return AppDisplayConstants.MainType.CARD_ADMIRAL;
            case CardType.CardMonster:
                return AppDisplayConstants.MainType.CARD_MONSTER;
            case CardType.CardMilitary:
                return AppDisplayConstants.MainType.CARD_MILITARY;
            // ... Thêm các case khác ...
            default:
                return type.ToString(); // Trả về tên enum nếu không tìm thấy
        }
    }
}
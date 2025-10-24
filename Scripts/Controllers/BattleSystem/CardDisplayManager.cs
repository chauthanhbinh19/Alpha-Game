using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class CardDisplayManager : MonoBehaviour, ICardDisplayManager
{
    [SerializeField] public GameObject CardContractPrefab;
    [SerializeField] public GameObject CardPenaltyPrefab;
    [SerializeField] public Transform CardPanel;
    void Start()
    {
        
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

        AudioManager.Instance.PlaySFX(AudioConstants.SFX.APPEAR);

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

        AudioManager.Instance.PlaySFX(AudioConstants.SFX.APPEAR);

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
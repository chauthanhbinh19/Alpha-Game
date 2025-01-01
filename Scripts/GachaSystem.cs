using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GachaSystem : MonoBehaviour
{
    private List<Cards> cards; // Danh sách thẻ bài
    private GameObject cardPrefab; // Prefab đại diện cho một thẻ bài
    private Transform summonArea; // Khu vực hiển thị thẻ bài
    private Texture backImage; // Mặt sau (image1.png)
    private bool isSummonAreaActive = false;

    public void Summon(string type, Transform area, int quantity)
    {
        if (area == null)
        {
            Debug.LogError("Summon area is null!");
            return;
        }

        Cards cardsManager = new Cards();
        cards = cardsManager.GetAllCards(type);

        if (cards == null || cards.Count == 0)
        {
            Debug.LogError("No cards found for type: " + type);
            return;
        }
        backImage = Resources.Load<Texture>("UI/Frame_5");
        if (backImage == null)
        {
            Debug.LogError("Back image not found in Resources!");
            return;
        }

        summonArea = area;
        summonArea.gameObject.SetActive(true);
        isSummonAreaActive = true;
        AddCloseEvent();

        cardPrefab = UIManager.Instance.GetGameObject("CardsPrefab");

        if (cardPrefab == null)
        {
            Debug.LogError("Card prefab is null! Check if the prefab is correctly loaded.");
            return;
        }

        // Lấy 10 thẻ ngẫu nhiên
        List<Cards> selectedCards = cards.OrderBy(x => Random.value).Take(quantity).ToList();
        foreach (var card in selectedCards){
            cardsManager.InsertUserCards(card);
            cardsManager.UpdateCardsGallery(card.id);
        }
        // Hiển thị các thẻ bài mặt sau
        StartCoroutine(DisplayCards(selectedCards));
    }

    private IEnumerator DisplayCards(List<Cards> selectedCards)
    {
        float delay = 0.2f; // Thời gian giữa mỗi lần hiển thị thẻ
        List<RawImage> cardImages = new List<RawImage>(); // Danh sách lưu trữ các RawImage của thẻ bài

        // Hiển thị tất cả các thẻ bài với mặt sau
        foreach (Cards card in selectedCards)
        {
            // Tạo một thẻ bài từ prefab
            GameObject cardObject = Instantiate(cardPrefab, summonArea);

            // Tìm RawImage trong cardObject
            RawImage image = cardObject.transform.Find("Image")?.GetComponent<RawImage>();
            Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
            Title.gameObject.SetActive(false);
            RawImage CardTitleImage = cardObject.transform.Find("CardTitleImage").GetComponent<RawImage>();
            CardTitleImage.gameObject.SetActive(false);

            RawImage rareBackground = cardObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareBackground.gameObject.SetActive(false);
            RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
            rareImage.texture = rareTexture;
            rareImage.gameObject.SetActive(false);
            if (image != null)
            {
                // Thiết lập mặt sau của thẻ bài
                image.texture = backImage;

                // Thêm vào danh sách cardImages để dùng khi lật thẻ
                cardImages.Add(image);

                // Hiển thị thẻ và đợi delay
                yield return new WaitForSeconds(delay);
            }
            else
            {
                Debug.LogError("Không tìm thấy RawImage trong cardObject");
            }
        }

        // Đợi tất cả thẻ hiển thị xong
        yield return new WaitForSeconds(delay);

        // Lật từng thẻ bài
        foreach (var image in cardImages)
        {
            // Tìm thẻ bài tương ứng
            Cards card = selectedCards[cardImages.IndexOf(image)];

            // Gọi Coroutine để lật thẻ
            yield return StartCoroutine(FlipCard(image, card.image));
        }
    }

    private IEnumerator FlipCard(RawImage cardImage, string frontImagePath)
    {
        float flipDuration = 0.2f; // Thời gian lật thẻ (độ delay giống hiển thị)
        float elapsedTime = 0f;

        // Lật từ mặt sau
        while (elapsedTime < flipDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float scaleX = Mathf.Lerp(1f, 0f, elapsedTime / (flipDuration / 2));
            cardImage.rectTransform.localScale = new Vector3(scaleX, 1f, 1f);
            yield return null;
        }

        // Thay đổi từ mặt sau sang mặt trước
        string fileNameWithoutExtension = frontImagePath.Replace(".png", "");
        Texture frontTexture = Resources.Load<Texture>(fileNameWithoutExtension);

        if (frontTexture != null)
        {
            cardImage.texture = frontTexture;
        }
        else
        {
            Debug.LogError("Không tìm thấy texture: " + fileNameWithoutExtension);
        }

        // Hiển thị rareBackground và rareImage
        Transform parent = cardImage.transform.parent;
        RawImage rareBackground = parent.Find("RareBackground")?.GetComponent<RawImage>();
        RawImage rareImage = parent.Find("Rare")?.GetComponent<RawImage>();

        if (rareBackground != null)
        {
            rareBackground.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Không tìm thấy RareBackground trong cardObject");
        }

        if (rareImage != null)
        {
            rareImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Không tìm thấy Rare trong cardObject");
        }
        // Lật sang mặt trước
        elapsedTime = 0f;
        while (elapsedTime < flipDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float scaleX = Mathf.Lerp(0f, 1f, elapsedTime / (flipDuration / 2));
            cardImage.rectTransform.localScale = new Vector3(scaleX, 1f, 1f);
            yield return null;
        }
    }
    private void AddCloseEvent()
    {
        EventTrigger trigger = summonArea.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = summonArea.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) =>
        {
            summonArea.gameObject.SetActive(false);
            isSummonAreaActive = false;
            foreach (Transform child in summonArea)
            {
                Destroy(child.gameObject);
            }
        });
        trigger.triggers.Add(entry);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }


}

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

    public void Summon(string name, string type, Transform area, int quantity)
    {
        if (area == null)
        {
            Debug.LogError("Summon area is null!");
            return;
        }

        object manager = null;
        IList items = null;

        // Xác định class dựa trên type
        switch (name.ToLower())
        {
            case "cards":
                manager = new Cards();
                items = ((Cards)manager).GetAllCards(type);
                break;
            case "books":
                manager = new Books();
                items = ((Books)manager).GetAllBooks(type);
                break;
            case "captains":
                manager = new Captains();
                items = ((Captains)manager).GetAllCaptains(type);
                break;
            case "monsters":
                manager = new Monsters();
                items = ((Monsters)manager).GetAllMonsters();
                break;
            case "military":
                manager = new Military();
                items = ((Military)manager).GetAllMilitary(type);
                break;
            case "spell":
                manager = new Spell();
                items = ((Spell)manager).GetAllSpell(type);
                break;
            default:
                Debug.LogError("Invalid type: " + type);
                return;
        }

        if (items == null || items.Count == 0)
        {
            Debug.LogError("No items found for type: " + type);
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
        var randomItems = items.Cast<object>().OrderBy(x => Random.value).Take(quantity).ToList();

        foreach (var item in randomItems)
        {
            // Debug.Log("Summoned item: " + item.ToString());
            // Thực hiện logic riêng tùy thuộc vào loại đối tượng
            if (manager is Cards)
            {
                Cards cardItem = item as Cards;
                if(cardItem != null){
                    ((Cards)manager).InsertUserCards(cardItem);
                }
            }
            else if (manager is Books)
            {
                Books bookItem = item as Books;
                if (bookItem != null)
                {
                    ((Books)manager).InsertUserBooks(bookItem);
                }
            }
            else if (manager is Captains)
            {
                Captains captainItem = item as Captains;
                if (captainItem != null){
                    ((Captains)manager).InsertUserCaptains(captainItem);
                }
            }
            else if (manager is Monsters)
            {
                Monsters monsterItem = item as Monsters;
                if (monsterItem != null){
                    ((Monsters)manager).InsertUserMonsters(monsterItem);
                }
            }
            else if (manager is Military)
            {
                Military militaryItem = item as Military;
                if(militaryItem != null){
                    ((Military)manager).InsertUserMilitary(militaryItem);
                }
            }
            else if (manager is Spell)
            {
                Spell spellItem = item as Spell;
                if (spellItem != null){
                    ((Spell)manager).InsertUserSpell(spellItem);
                }
            }
            // Thêm các xử lý tương tự cho các loại khác
        }
        // Hiển thị các thẻ bài mặt sau
        StartCoroutine(DisplayCards(randomItems));
    }

    private IEnumerator DisplayCards(List<object> randomItems)
    {
        float delay = 0.2f; // Thời gian giữa mỗi lần hiển thị thẻ
        List<RawImage> cardImages = new List<RawImage>(); // Danh sách lưu trữ các RawImage của thẻ bài

        // Hiển thị tất cả các thẻ bài với mặt sau
        foreach (var item in randomItems)
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
            if (item is Cards card)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is Books book)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{book.rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is Captains captains)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{captains.rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is Monsters monsters)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{monsters.rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is Military military)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{military.rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is Spell spell)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spell.rare}");
                rareImage.texture = rareTexture;
            }
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
            // Lấy thẻ bài tương ứng từ danh sách
            int index = cardImages.IndexOf(image);
            if (randomItems[index] is Cards card)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, card.image));
            }
            else if (randomItems[index] is Books book)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, book.image));
            }
            else if (randomItems[index] is Captains captain)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, captain.image));
            }
            else if (randomItems[index] is Monsters monster)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, monster.image));
            }
            else if (randomItems[index] is Military military)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, military.image));
            }
            else if (randomItems[index] is Spell spell)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, spell.image));
            }
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

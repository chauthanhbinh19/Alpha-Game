using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using System;
using System.Threading.Tasks;

public class GachaSystem : MonoBehaviour
{
    private GameObject cardPrefab; // Prefab đại diện cho một thẻ bài
    private Transform summonArea; // Khu vực hiển thị thẻ bài
    private Texture backImage; // Mặt sau (image1.png)
    // private bool isSummonAreaActive = false;

    public void Summon(string name, string type, GameObject summonObject, int quantity, List<Items> items, Action<bool> onFinished)
    {
        StartCoroutine(PlaySummonVideoAndSummon(name, type, summonObject, quantity, items, onFinished));

    }
    IEnumerator PlaySummonVideoAndSummon(string name, string type, GameObject summonObject, int quantity, List<Items> items, Action<bool> onFinished)
    {
        RawImage summonEffectImage = summonObject.transform.Find("SummonEffectImage").GetComponent<RawImage>();
        VideoPlayer videoPlayer = summonObject.transform.Find("SummonEffect").GetComponent<VideoPlayer>();

        if (videoPlayer == null || videoPlayer.clip == null)
        {
            Debug.LogWarning("VideoPlayer or video clip is missing!");
            onFinished?.Invoke(false); // Thất bại
            yield break;
        }

        // Gán texture cho RawImage nếu có
        if (videoPlayer.targetTexture != null)
        {
            summonEffectImage.texture = videoPlayer.targetTexture;
        }

        bool videoFinished = false;

        // Gán sự kiện khi video kết thúc
        videoPlayer.loopPointReached += (VideoPlayer vp) => { videoFinished = true; };

        summonEffectImage.gameObject.SetActive(true);
        videoPlayer.Play();

        // Đợi cho video bắt đầu phát (tránh skip)
        while (!videoPlayer.isPlaying && videoPlayer.frame == 0)
        {
            yield return null;
        }

        // Đợi video phát xong
        while (!videoFinished)
        {
            yield return null;
        }

        // Chờ thêm 0.5 giây sau khi video kết thúc
        yield return new WaitForSeconds(0.5f);
        // Tắt RawImage sau khi video kết thúc
        summonEffectImage.gameObject.SetActive(false);
        
        // 👉 GỌI async method và đợi kết quả
        Task<bool> summonTask =
            SummonEventAsync(name, type, summonObject, quantity, items);

        yield return CoroutineAsyncBridge.WaitTask(
            summonTask,
            result => onFinished?.Invoke(result)
        );
    }
    private async Task<bool> SummonEventAsync(string name, string type, GameObject summonObject, int quantity, List<Items> items)
    {
        Transform area = summonObject.transform.Find("SummonArea");

        if (area == null)
        {
            Debug.LogError("Summon area is null!");
            return false;
        }

        IList cards = null;

        foreach (Items item in items)
        {
            if (item.Quantity >= quantity)
            {
                item.Quantity = item.Quantity - quantity;
                await UserItemsService.Create().UpdateUserItemQuantityAsync(item);
            }
            else
            {
                return false;
            }
        }

        // Xác định class dựa trên type
        switch (name)
        {
            case AppConstants.MainType.SUMMON_CARD_HEROES:
                cards = await CardHeroesService.Create().GetAllCardHeroesAsync(type);
                break;
            case AppConstants.MainType.SUMMON_BOOKS:
                cards = await BooksService.Create().GetAllBooksAsync(type);
                break;
            case AppConstants.MainType.SUMMON_CARD_CAPTAINS:
                cards = await CardCaptainsService.Create().GetAllCardCaptainsAsync(type);
                break;
            case AppConstants.MainType.SUMMON_CARD_MONSTERS:
                cards = await CardMonstersService.Create().GetAllCardMonstersAsync(type);
                break;
            case AppConstants.MainType.SUMMON_CARD_MILITARY:
                cards = await CardMilitariesService.Create().GetAllCardMilitariesAsync(type);
                break;
            case AppConstants.MainType.SUMMON_CARD_SPELLS:
                cards = await CardSpellsService.Create().GetAllCardSpellsAsync(type);
                break;
            case AppConstants.MainType.SUMMON_CARD_COLONELS:
                cards = await CardColonelsService.Create().GetAllCardColonelsAsync(type);
                break;
            case AppConstants.MainType.SUMMON_CARD_GENERALS:
                cards = await CardGeneralsService.Create().GetAllCardGeneralsAsync(type);
                break;
            case AppConstants.MainType.SUMMON_CARD_ADMIRALS:
                cards = await CardAdmiralsService.Create().GetAllCardAdmiralsAsync(type);
                break;
            default:
                Debug.LogError("Invalid type: " + type);
                return false;
        }

        if (items == null || items.Count == 0)
        {
            Debug.LogError("No items found for type: " + type);
            return false;
        }
        backImage = Resources.Load<Texture>("UI/Frame_5");
        if (backImage == null)
        {
            Debug.LogError(MessageHelper.ImageConstants.ImageIsNull);
            return false;
        }

        summonArea = area;
        summonArea.gameObject.SetActive(true);
        // isSummonAreaActive = true;
        AddCloseEvent();

        cardPrefab = UIManager.Instance.Get("CardsPrefab");

        if (cardPrefab == null)
        {
            Debug.LogError(MessageHelper.PrefabConstants.PrefabIsNull);
            return false;
        }

        // Lấy 10 thẻ ngẫu nhiên
        var randomItems = cards.Cast<object>().OrderBy(x => UnityEngine.Random.value).Take(quantity).ToList();

        foreach (var card in randomItems)
        {
            // Debug.Log("Summoned item: " + item.ToString());
            // Thực hiện logic riêng tùy thuộc vào loại đối tượng
            if (name.Equals(AppConstants.MainType.SUMMON_CARD_HEROES))
            {
                CardHeroes cardItem = card as CardHeroes;
                if (cardItem != null)
                {
                    cardItem.Quantity = cardItem.Quantity + 1;
                    await UserCardHeroesService.Create().InsertUserCardHeroAsync(cardItem);
                    await CardHeroesGalleryService.Create().InsertCardHeroGalleryAsync(cardItem.Id);
                }
            }
            else if (name.Equals(AppConstants.MainType.SUMMON_BOOKS))
            {
                Books bookItem = card as Books;
                if (bookItem != null)
                {
                    bookItem.Quantity = bookItem.Quantity + 1;
                    await UserBooksService.Create().InsertUserBookAsync(bookItem);
                    await BooksGalleryService.Create().InsertBookGalleryAsync(bookItem.Id);
                }
            }
            else if (name.Equals(AppConstants.MainType.SUMMON_CARD_CAPTAINS))
            {
                CardCaptains captainItem = card as CardCaptains;
                if (captainItem != null)
                {
                    captainItem.Quantity = captainItem.Quantity + 1;
                    await UserCardCaptainsService.Create().InsertUserCardCaptainAsync(captainItem);
                    await CardCaptainsGalleryService.Create().InsertCardCaptainGalleryAsync(captainItem.Id);
                }
            }
            else if (name.Equals(AppConstants.MainType.SUMMON_CARD_MONSTERS))
            {
                CardMonsters monsterItem = card as CardMonsters;
                if (monsterItem != null)
                {
                    monsterItem.Quantity = monsterItem.Quantity + 1;
                    await UserCardMonstersService.Create().InsertUserCardMonsterAsync(monsterItem);
                    await CardMonstersGalleryService.Create().InsertCardMonsterGalleryAsync(monsterItem.Id);
                }
            }
            else if (name.Equals(AppConstants.MainType.SUMMON_CARD_MILITARY))
            {
                CardMilitaries militaryItem = card as CardMilitaries;
                if (militaryItem != null)
                {
                    militaryItem.Quantity = militaryItem.Quantity + 1;
                    await UserCardMilitariesService.Create().InsertUserCardMilitaryAsync(militaryItem);
                    await CardMilitariesGalleryService.Create().InsertCardMilitaryGalleryAsync(militaryItem.Id);
                }
            }
            else if (name.Equals(AppConstants.MainType.SUMMON_CARD_SPELLS))
            {
                CardSpells spellItem = card as CardSpells;
                if (spellItem != null)
                {
                    spellItem.Quantity = spellItem.Quantity + 1;
                    await UserCardSpellsService.Create().InsertUserCardSpellAsync(spellItem);
                    await CardSpellsGalleryService.Create().InsertCardSpellGalleryAsync(spellItem.Id);
                }
            }
            else if (name.Equals(AppConstants.MainType.SUMMON_CARD_COLONELS))
            {
                CardColonels colonelItem = card as CardColonels;
                if (colonelItem != null)
                {
                    colonelItem.Quantity = colonelItem.Quantity + 1;
                    await UserCardColonelsService.Create().InsertUserCardColonelAsync(colonelItem);
                    await CardColonelsGalleryService.Create().InsertCardColonelGalleryAsync(colonelItem.Id);
                }
            }
            else if (name.Equals(AppConstants.MainType.SUMMON_CARD_GENERALS))
            {
                CardGenerals generalItem = card as CardGenerals;
                if (generalItem != null)
                {
                    generalItem.Quantity = generalItem.Quantity + 1;
                    await UserCardGeneralsService.Create().InsertUserCardGeneralAsync(generalItem);
                    await CardGeneralsGalleryService.Create().InsertCardGeneralGalleryAsync(generalItem.Id);
                }
            }
            else if (name.Equals(AppConstants.MainType.SUMMON_CARD_ADMIRALS))
            {
                CardAdmirals admiralItem = card as CardAdmirals;
                if (admiralItem != null)
                {
                    admiralItem.Quantity = admiralItem.Quantity + 1;
                    await UserCardAdmiralsService.Create().InsertUserCardAdmiralAsync(admiralItem);
                    await CardAdmiralsGalleryService.Create().InsertCardAdmiralGalleryAsync(admiralItem.Id);
                }
            }
            // Thêm các xử lý tương tự cho các loại khác
        }
        // Hiển thị các thẻ bài mặt sau
        StartCoroutine(DisplayCards(randomItems));
        return true;
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
            if (item is CardHeroes card)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.Rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is Books book)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{book.Rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is CardCaptains captains)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{captains.Rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is CardMonsters monsters)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{monsters.Rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is CardMilitaries military)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{military.Rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is CardSpells spell)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spell.Rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is CardColonels colonels)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{colonels.Rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is CardGenerals generals)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{generals.Rare}");
                rareImage.texture = rareTexture;
            }
            else if (item is CardAdmirals admirals)
            {
                Texture rareTexture = Resources.Load<Texture>($"UI/UI/{admirals.Rare}");
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
            if (randomItems[index] is CardHeroes card)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, card.Image));
            }
            else if (randomItems[index] is Books book)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, book.Image));
            }
            else if (randomItems[index] is CardCaptains captain)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, captain.Image));
            }
            else if (randomItems[index] is CardMonsters monster)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, monster.Image));
            }
            else if (randomItems[index] is CardMilitaries military)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, military.Image));
            }
            else if (randomItems[index] is CardSpells spell)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, spell.Image));
            }
            else if (randomItems[index] is CardColonels colonels)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, colonels.Image));
            }
            else if (randomItems[index] is CardGenerals generals)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, generals.Image));
            }
            else if (randomItems[index] is CardAdmirals admirals)
            {
                // Gọi Coroutine để lật thẻ
                yield return StartCoroutine(FlipCard(image, admirals.Image));
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
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(frontImagePath);
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
            // isSummonAreaActive = false;
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

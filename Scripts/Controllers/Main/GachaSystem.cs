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
            onFinished?.Invoke(false);
            yield break;
        }

        if (videoPlayer.targetTexture != null)
        {
            summonEffectImage.texture = videoPlayer.targetTexture;
        }

        bool videoFinished = false;
        void OnVideoFinished(VideoPlayer vp)
        {
            videoFinished = true;
            vp.loopPointReached -= OnVideoFinished;
        }

        videoPlayer.loopPointReached += OnVideoFinished;
        summonEffectImage.gameObject.SetActive(true);
        videoPlayer.Play();

        while (!videoPlayer.isPlaying && videoPlayer.frame == 0)
        {
            yield return null;
        }

        while (!videoFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        summonEffectImage.gameObject.SetActive(false);

        Task<bool> summonTask = SummonEventAsync(name, type, summonObject, quantity, items);
        yield return CoroutineAsyncBridge.WaitTask(summonTask, result => onFinished?.Invoke(result));
    }
    
    private async Task<bool> SummonEventAsync(string name, string type, GameObject summonObject, int quantity, List<Items> items)
    {
        if (items == null || items.Count == 0)
        {
            Debug.LogError("No items provided for summon.");
            return false;
        }

        Transform area = summonObject.transform.Find("SummonArea");
        if (area == null)
        {
            Debug.LogError("Summon area is null!");
            return false;
        }

        if (!await DeductSummonItemsAsync(quantity, items))
            return false;

        List<object> cards = await LoadSummonPoolAsync(name, type);
        if (cards == null || cards.Count == 0)
        {
            Debug.LogError("No cards found for summon type: " + name);
            return false;
        }

        backImage ??= TextureHelper.LoadTextureCached("UI/Frame_5");
        if (backImage == null)
        {
            Debug.LogError(MessageConstants.IMAGE_IS_NULL);
            return false;
        }

        summonArea = area;
        summonArea.gameObject.SetActive(true);
        AddCloseEvent();

        cardPrefab = UIManager.Instance.Get("CardsPrefab");
        if (cardPrefab == null)
        {
            Debug.LogError(MessageConstants.PREFAB_IS_NULL);
            return false;
        }

        var randomItems = SelectRandomItems(cards, quantity);
        if (randomItems.Count == 0)
        {
            Debug.LogError("Not enough cards available to summon.");
            return false;
        }

        foreach (var card in randomItems)
        {
            await InsertSummonedCardAsync(name, card);
        }

        StartCoroutine(DisplayCards(randomItems));
        return true;
    }
    
    private IEnumerator DisplayCards(List<object> randomItems)
    {
        float delay = 0.2f;
        var cardData = new List<(RawImage Image, string FrontImagePath)>(randomItems.Count);

        for (int i = 0; i < randomItems.Count; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab, summonArea);
            RawImage image = cardObject.transform.Find("Image")?.GetComponent<RawImage>();
            Text title = cardObject.transform.Find("Title").GetComponent<Text>();
            title.gameObject.SetActive(false);
            RawImage cardTitleImage = cardObject.transform.Find("CardTitleImage").GetComponent<RawImage>();
            cardTitleImage.gameObject.SetActive(false);

            RawImage rareBackground = cardObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareBackground.gameObject.SetActive(false);
            RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();

            string rarePath = GetRareImagePath(randomItems[i]);
            if (!string.IsNullOrEmpty(rarePath) && rareImage != null)
            {
                rareImage.texture = TextureHelper.LoadTextureCached(rarePath);
            }

            if (image == null)
            {
                Debug.LogError("Không tìm thấy RawImage trong cardObject");
                continue;
            }

            image.texture = backImage;
            cardData.Add((image, GetFrontImagePath(randomItems[i])));
            rareImage?.gameObject.SetActive(false);

            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(delay);

        for (int i = 0; i < cardData.Count; i++)
        {
            yield return StartCoroutine(FlipCard(cardData[i].Image, cardData[i].FrontImagePath));
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
        Texture frontTexture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);

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
    
    private async Task<bool> DeductSummonItemsAsync(int quantity, List<Items> items)
    {
        foreach (Items item in items)
        {
            if (item.Quantity < quantity)
            {
                Debug.LogWarning($"Not enough quantity for item {item.Id}");
                return false;
            }

            item.Quantity -= quantity;
            await UserItemsService.Create().UpdateUserItemQuantityAsync(item);
        }

        return true;
    }

    private async Task<List<object>> LoadSummonPoolAsync(string name, string type)
    {
        return name switch
        {
            AppConstants.MainType.SUMMON_CARD_HERO => (await CardHeroesService.Create().GetAllCardHeroesAsync(type)).Cast<object>().ToList(),
            AppConstants.MainType.SUMMON_BOOK => (await BooksService.Create().GetAllBooksAsync(type)).Cast<object>().ToList(),
            AppConstants.MainType.SUMMON_CARD_CAPTAIN => (await CardCaptainsService.Create().GetAllCardCaptainsAsync(type)).Cast<object>().ToList(),
            AppConstants.MainType.SUMMON_CARD_MONSTER => (await CardMonstersService.Create().GetAllCardMonstersAsync(type)).Cast<object>().ToList(),
            AppConstants.MainType.SUMMON_CARD_MILITARY => (await CardMilitariesService.Create().GetAllCardMilitariesAsync(type)).Cast<object>().ToList(),
            AppConstants.MainType.SUMMON_CARD_SPELL => (await CardSpellsService.Create().GetAllCardSpellsAsync(type)).Cast<object>().ToList(),
            AppConstants.MainType.SUMMON_CARD_COLONEL => (await CardColonelsService.Create().GetAllCardColonelsAsync(type)).Cast<object>().ToList(),
            AppConstants.MainType.SUMMON_CARD_GENERAL => (await CardGeneralsService.Create().GetAllCardGeneralsAsync(type)).Cast<object>().ToList(),
            AppConstants.MainType.SUMMON_CARD_ADMIRAL => (await CardAdmiralsService.Create().GetAllCardAdmiralsAsync(type)).Cast<object>().ToList(),
            _ => null,
        };
    }

    private List<object> SelectRandomItems(List<object> cards, int quantity)
    {
        if (cards.Count <= quantity)
            return cards.ToList();

        return cards.OrderBy(_ => UnityEngine.Random.value).Take(quantity).ToList();
    }

    private async Task InsertSummonedCardAsync(string name, object card)
    {
        switch (name)
        {
            case AppConstants.MainType.SUMMON_CARD_HERO:
                if (card is CardHeroes hero)
                {
                    hero.Quantity += 1;
                    await UserCardHeroesService.Create().InsertUserCardHeroAsync(hero);
                    await CardHeroesGalleryService.Create().InsertCardHeroGalleryAsync(hero.Id);
                }
                break;
            case AppConstants.MainType.SUMMON_BOOK:
                if (card is Books book)
                {
                    book.Quantity += 1;
                    await UserBooksService.Create().InsertUserBookAsync(book);
                    await BooksGalleryService.Create().InsertBookGalleryAsync(book.Id);
                }
                break;
            case AppConstants.MainType.SUMMON_CARD_CAPTAIN:
                if (card is CardCaptains captain)
                {
                    captain.Quantity += 1;
                    await UserCardCaptainsService.Create().InsertUserCardCaptainAsync(captain);
                    await CardCaptainsGalleryService.Create().InsertCardCaptainGalleryAsync(captain.Id);
                }
                break;
            case AppConstants.MainType.SUMMON_CARD_MONSTER:
                if (card is CardMonsters monster)
                {
                    monster.Quantity += 1;
                    await UserCardMonstersService.Create().InsertUserCardMonsterAsync(monster);
                    await CardMonstersGalleryService.Create().InsertCardMonsterGalleryAsync(monster.Id);
                }
                break;
            case AppConstants.MainType.SUMMON_CARD_MILITARY:
                if (card is CardMilitaries military)
                {
                    military.Quantity += 1;
                    await UserCardMilitariesService.Create().InsertUserCardMilitaryAsync(military);
                    await CardMilitariesGalleryService.Create().InsertCardMilitaryGalleryAsync(military.Id);
                }
                break;
            case AppConstants.MainType.SUMMON_CARD_SPELL:
                if (card is CardSpells spell)
                {
                    spell.Quantity += 1;
                    await UserCardSpellsService.Create().InsertUserCardSpellAsync(spell);
                    await CardSpellsGalleryService.Create().InsertCardSpellGalleryAsync(spell.Id);
                }
                break;
            case AppConstants.MainType.SUMMON_CARD_COLONEL:
                if (card is CardColonels colonel)
                {
                    colonel.Quantity += 1;
                    await UserCardColonelsService.Create().InsertUserCardColonelAsync(colonel);
                    await CardColonelsGalleryService.Create().InsertCardColonelGalleryAsync(colonel.Id);
                }
                break;
            case AppConstants.MainType.SUMMON_CARD_GENERAL:
                if (card is CardGenerals general)
                {
                    general.Quantity += 1;
                    await UserCardGeneralsService.Create().InsertUserCardGeneralAsync(general);
                    await CardGeneralsGalleryService.Create().InsertCardGeneralGalleryAsync(general.Id);
                }
                break;
            case AppConstants.MainType.SUMMON_CARD_ADMIRAL:
                if (card is CardAdmirals admiral)
                {
                    admiral.Quantity += 1;
                    await UserCardAdmiralsService.Create().InsertUserCardAdmiralAsync(admiral);
                    await CardAdmiralsGalleryService.Create().InsertCardAdmiralGalleryAsync(admiral.Id);
                }
                break;
        }
    }

    private string GetRareImagePath(object item)
    {
        return item switch
        {
            CardHeroes c => $"UI/UI/{c.Rare}",
            Books b => $"UI/UI/{b.Rare}",
            CardCaptains c => $"UI/UI/{c.Rare}",
            CardMonsters c => $"UI/UI/{c.Rare}",
            CardMilitaries c => $"UI/UI/{c.Rare}",
            CardSpells c => $"UI/UI/{c.Rare}",
            CardColonels c => $"UI/UI/{c.Rare}",
            CardGenerals c => $"UI/UI/{c.Rare}",
            CardAdmirals c => $"UI/UI/{c.Rare}",
            _ => string.Empty,
        };
    }

    private string GetFrontImagePath(object item)
    {
        return item switch
        {
            CardHeroes c => c.Image,
            Books c => c.Image,
            CardCaptains c => c.Image,
            CardMonsters c => c.Image,
            CardMilitaries c => c.Image,
            CardSpells c => c.Image,
            CardColonels c => c.Image,
            CardGenerals c => c.Image,
            CardAdmirals c => c.Image,
            _ => string.Empty,
        };
    }
    
    private void AddCloseEvent()
    {
        EventTrigger trigger = summonArea.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = summonArea.gameObject.AddComponent<EventTrigger>();
        }
        else
        {
            trigger.triggers.Clear();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) =>
        {
            summonArea.gameObject.SetActive(false);
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

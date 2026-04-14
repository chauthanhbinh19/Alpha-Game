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
    private bool isSkipRequested = false;
    private bool isRevealFinished = false;
    // private bool isSummonAreaActive = false;

    public void Summon(string name, string type, GameObject summonObject, int quantity, List<Items> items, Action<bool> onFinished)
    {
        isSkipRequested = false;
        isRevealFinished = false;
        RegisterSkip(summonObject);
        StartCoroutine(PlaySummonVideoAndSummon(name, type, summonObject, quantity, items, onFinished));
    }

    #region SKIP EVENT
    private void RegisterSkip(GameObject summonObject)
    {
        EventTrigger trigger = summonObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = summonObject.AddComponent<EventTrigger>();
        else
            trigger.triggers.Clear();

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };

        entry.callback.AddListener((data) =>
        {
            // 👉 Nếu chưa hiện xong → SKIP
            if (!isRevealFinished)
            {
                isSkipRequested = true;
            }
            else
            {
                // 👉 Nếu đã hiện xong → ĐÓNG
                CloseSummonArea();
            }
        });

        trigger.triggers.Add(entry);
    }

    private void CloseSummonArea()
    {
        if (summonArea == null) return;

        summonArea.gameObject.SetActive(false);

        foreach (Transform child in summonArea)
        {
            Destroy(child.gameObject);
        }
    }
    #endregion

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
            if (isSkipRequested)
            {
                videoPlayer.Stop();
                break;
            }
            yield return null;
        }

        summonEffectImage.gameObject.SetActive(false);

        Task<bool> summonTask = SummonEventAsync(name, type, summonObject, quantity, items);
        yield return CoroutineAsyncBridge.WaitTask(summonTask, result => onFinished?.Invoke(result));
    }

    private async Task<bool> SummonEventAsync(string name, string type, GameObject summonObject, int quantity, List<Items> items)
    {
        if (items == null || items.Count == 0) return false;

        Transform area = summonObject.transform.Find("SummonArea");
        if (area == null) return false;

        if (!await DeductSummonItemsAsync(quantity, items))
            return false;

        List<object> cards = await LoadSummonPoolAsync(name, type);
        if (cards == null || cards.Count == 0) return false;

        backImage ??= TextureHelper.LoadTextureCached("UI/Frame_5");

        summonArea = area;
        summonArea.gameObject.SetActive(true);

        cardPrefab = UIManager.Instance.Get("CardsPrefab");

        var randomItems = SelectRandomItems(cards, quantity);

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
        var cardData = new List<(RawImage Image, string FrontPath)>();

        // 🔹 Spawn đủ card (KHÔNG break)
        for (int i = 0; i < randomItems.Count; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab, summonArea);

            RawImage image = cardObject.transform.Find("Image")?.GetComponent<RawImage>();
            RawImage rareBg = cardObject.transform.Find("RareBackground").GetComponent<RawImage>();
            RawImage rare = cardObject.transform.Find("Rare").GetComponent<RawImage>();

            rareBg.gameObject.SetActive(true);
            rare.gameObject.SetActive(true);

            image.texture = backImage;

            string rarePath = GetRareImagePath(randomItems[i]);

            if (!string.IsNullOrEmpty(rarePath))
            {
                Texture rareTexture = TextureHelper.LoadTextureCached(rarePath);

                if (rareTexture != null)
                {
                    rare.texture = rareTexture;
                }
            }

            cardData.Add((image, GetFrontImagePath(randomItems[i])));

            // ✅ chỉ delay khi KHÔNG skip
            if (!isSkipRequested)
            {
                yield return new WaitForSeconds(delay);
            }
        }

        // 👉 SKIP → hiện full luôn (sau khi đã spawn đủ)
        if (isSkipRequested)
        {
            foreach (var data in cardData)
            {
                ShowFullCard(data.Image, data.FrontPath);
            }

            isRevealFinished = true;
            yield break;
        }

        yield return new WaitForSeconds(delay);

        // 🔹 Flip từng card
        for (int i = 0; i < cardData.Count; i++)
        {
            if (isSkipRequested)
            {
                for (int j = i; j < cardData.Count; j++)
                {
                    ShowFullCard(cardData[j].Image, cardData[j].FrontPath);
                }

                isRevealFinished = true;
                yield break;
            }

            yield return StartCoroutine(FlipCard(cardData[i].Image, cardData[i].FrontPath));
        }

        isRevealFinished = true;
    }

    private IEnumerator FlipCard(RawImage cardImage, string frontImagePath)
    {
        float duration = 0.2f;
        float time = 0f;

        while (time < duration / 2)
        {
            if (isSkipRequested)
            {
                ShowFullCard(cardImage, frontImagePath);
                yield break;
            }

            time += Time.deltaTime;
            float scaleX = Mathf.Lerp(1f, 0f, time / (duration / 2));
            cardImage.rectTransform.localScale = new Vector3(scaleX, 1f, 1f);
            yield return null;
        }

        ShowFullCard(cardImage, frontImagePath);

        time = 0f;
        while (time < duration / 2)
        {
            if (isSkipRequested) yield break;

            time += Time.deltaTime;
            float scaleX = Mathf.Lerp(0f, 1f, time / (duration / 2));
            cardImage.rectTransform.localScale = new Vector3(scaleX, 1f, 1f);
            yield return null;
        }
    }

    private void ShowFullCard(RawImage cardImage, string frontPath)
    {
        string file = ImageExtensionHandler.RemoveImageExtension(frontPath);
        Texture tex = TextureHelper.LoadTextureCached(file);

        if (tex != null)
            cardImage.texture = tex;

        Transform parent = cardImage.transform.parent;

        parent.Find("RareBackground")?.gameObject.SetActive(true);
        parent.Find("Rare")?.gameObject.SetActive(true);

        cardImage.rectTransform.localScale = Vector3.one;
    }

    private async Task<bool> DeductSummonItemsAsync(int quantity, List<Items> items)
    {
        foreach (var item in items)
        {
            if (item.Quantity < quantity) return false;

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

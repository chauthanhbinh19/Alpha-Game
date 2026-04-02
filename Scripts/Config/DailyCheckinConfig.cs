using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class DailyCheckinConfig : MonoBehaviour
{
    private static readonly Dictionary<int, DailyCheckinRule> DailyCheckinRules = new()
    {
        { 1,  new(AppConstants.MainType.CARD_HERO,             () => CardHeroesService.Create().GetUniqueCardHeroesIdAsync()) },
        { 2,  new(AppConstants.MainType.ALCHEMY,               () => AlchemiesService.Create().GetUniqueAlchemiesIdAsync()) },
        { 3,  new(AppConstants.MainType.AVATAR,                () => AvatarsService.Create().GetUniqueAvatarsIdAsync()) },
        { 4,  new(AppConstants.MainType.BORDER,                () => BordersService.Create().GetUniqueBordersIdAsync()) },
        { 5,  new(AppConstants.MainType.BOOK,                  () => BooksService.Create().GetUniqueBooksIdAsync()) },
        { 6,  new(AppConstants.MainType.CARD_ADMIRAL,          () => CardAdmiralsService.Create().GetUniqueCardAdmiralsIdAsync()) },
        { 7,  new(AppConstants.MainType.CARD_CAPTAIN,          () => CardCaptainsService.Create().GetUniqueCardCaptainsIdAsync()) },
        { 8,  new(AppConstants.MainType.CARD_COLONEL,          () => CardColonelsService.Create().GetUniqueCardColonelsIdAsync()) },
        { 9,  new(AppConstants.MainType.CARD_GENERAL,          () => CardGeneralsService.Create().GetUniqueCardGeneralsIdAsync()) },
        { 10, new(AppConstants.MainType.CARD_LIFE,             () => CardLivesService.Create().GetUniqueCardLivesIdAsync()) },
        { 11, new(AppConstants.MainType.CARD_MILITARY,         () => CardMilitariesService.Create().GetUniqueCardMilitariesIdAsync()) },
        { 12, new(AppConstants.MainType.CARD_MONSTER,          () => CardMonstersService.Create().GetUniqueCardMonstersIdAsync()) },
        { 13, new(AppConstants.MainType.CARD_SPELL,            () => CardSpellsService.Create().GetUniqueCardSpellsIdAsync()) },
        { 14, new(AppConstants.MainType.COLLABORATION_EQUIPMENT,() => CollaborationEquipmentsService.Create().GetUniqueCollaborationEquipmentsIdAsync()) },
        { 15, new(AppConstants.MainType.COLLABORATION,         () => CollaborationsService.Create().GetUniqueCollaborationsIdAsync()) },
        { 16, new(AppConstants.MainType.EQUIPMENT,             () => EquipmentsService.Create().GetUniqueEquipmentsIdAsync()) },
        { 17, new(AppConstants.MainType.FORGE,                 () => ForgesService.Create().GetUniqueForgesIdAsync()) },
        { 18, new(AppConstants.MainType.MAGIC_FORMATION_CIRCLE,() => MagicFormationCirclesService.Create().GetUniqueMagicFormationCirclesIdAsync()) },
        { 19, new(AppConstants.MainType.MEDAL,                 () => MedalsService.Create().GetUniqueMedalsIdAsync()) },
        { 20, new(AppConstants.MainType.PET,                   () => PetsService.Create().GetUniquePetsIdAsync()) },
        { 21, new(AppConstants.MainType.PUPPET,                () => PuppetsService.Create().GetUniquePuppetsIdAsync()) },
        { 22, new(AppConstants.MainType.RELIC,                 () => RelicsService.Create().GetUniqueRelicsIdAsync()) },
        { 23, new(AppConstants.MainType.SKILL,                 () => SkillsService.Create().GetUniqueSkillsIdAsync()) },
        { 24, new(AppConstants.MainType.SYMBOL,                () => SymbolsService.Create().GetUniqueSymbolsIdAsync()) },
        { 25, new(AppConstants.MainType.TALISMAN,              () => TalismansService.Create().GetUniqueTalismansIdAsync()) },
        { 26, new(AppConstants.MainType.TITLE,                 () => TitlesService.Create().GetUniqueTitlesIdAsync()) },
        { 27, new(AppConstants.MainType.ITEM,                  () => ItemsService.Create().GetUniqueItemsIdAsync()) },
        { 28, new(AppConstants.MainType.ITEM,                  () => ItemsService.Create().GetUniqueItemsIdAsync()) },
        { 29, new(AppConstants.MainType.ITEM,                  () => ItemsService.Create().GetUniqueItemsIdAsync()) },
        { 30, new(AppConstants.MainType.ITEM,                  () => ItemsService.Create().GetUniqueItemsIdAsync()) },
        { 31, new(AppConstants.MainType.ITEM,                  () => ItemsService.Create().GetUniqueItemsIdAsync()) },
    };

    void Start()
    {
        _ = CreateDailyCheckinAsync();
    }

    public async Task CreateDailyCheckinAsync()
    {
        DateTime now = DateTime.Now;
        int year = now.Year;
        int month = now.Month;
        int daysInMonth = DateTime.DaysInMonth(year, month);

        for (int day = 1; day <= daysInMonth; day++)
        {
            DateTime currentDate = new DateTime(year, month, day);
            await DailyCheckinService.Create().DeleteDailyCheckinAsync(day.ToString());
            await CreateAndInsertDailyCheckinAsync(day, currentDate);
        }
    }

    public async Task CreateAndInsertDailyCheckinAsync(int day, DateTime currentDate)
    {
        if (!DailyCheckinRules.TryGetValue(day, out DailyCheckinRule rule))
            return;

        List<string> ids = await rule.GetUniqueIds();
        if (ids == null || ids.Count == 0)
            return;

        string randomItem = ids[UnityEngine.Random.Range(0, ids.Count)];
        DailyCheckin dailyCheckin = new DailyCheckin
        {
            Id = day.ToString(),
            Date = currentDate,
            Month = currentDate.Month,
            Year = currentDate.Year,
            Type = rule.MainType,
            ObjectId = randomItem,
            Quantity = rule.Quantity
        };

        await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
    }

    private class DailyCheckinRule
    {
        public DailyCheckinRule(string mainType, Func<Task<List<string>>> getUniqueIds, int quantity = 5000)
        {
            MainType = mainType;
            GetUniqueIds = getUniqueIds;
            Quantity = quantity;
        }

        public string MainType { get; }
        public Func<Task<List<string>>> GetUniqueIds { get; }
        public int Quantity { get; }
    }
}

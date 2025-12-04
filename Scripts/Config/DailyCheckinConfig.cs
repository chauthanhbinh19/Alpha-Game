using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class DailyCheckinConfig : MonoBehaviour
{
    void Start()
    {
        _=CreateDailyCheckinAsync();
    }
    public async Task CreateDailyCheckinAsync()
    {
        // Lấy tháng và năm hiện tại
        DateTime now = DateTime.Now;
        if (true)
        {
            // UserDailyCheckinService.Create().DeleteUserDailyCheckin(User.CurrentUserId);

            int year = now.Year;
            int month = now.Month;

            // Lấy số ngày trong tháng hiện tại
            int daysInMonth = DateTime.DaysInMonth(year, month);

            // In từng ngày từ 01 đến ngày cuối tháng
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(year, month, day);
                await DailyCheckinService.Create().DeleteDailyCheckinAsync(day.ToString());
                await CreateAndInsertDailyCheckinAsync(day, currentDate);
                // UserDailyCheckinService.Create().InsertUserDailyCheckin(User.CurrentUserId, day.ToString());
            }
        }
    }
    public async Task CreateAndInsertDailyCheckinAsync(int day, DateTime currentDate)
    {
        List<string> list = new List<string>();
        switch (day)
        {
            case 1:
                list = await CardHeroesService.Create().GetUniqueCardHeroesIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.CARD_HERO,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 2:
                list = await AlchemiesService.Create().GetUniqueAlchemiesIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.ALCHEMY,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 3:
                list = await AvatarsService.Create().GetUniqueAvatarsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.AVATAR,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 4:
                list = await BordersService.Create().GetUniqueBordersIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.BORDER,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 5:
                list = await BooksService.Create().GetUniqueBooksIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.BOOK,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 6:
                list = await CardAdmiralsService.Create().GetUniqueCardAdmiralsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.CARD_ADMIRAL,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 7:
                list = await CardCaptainsService.Create().GetUniqueCardCaptainsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.CARD_CAPTAIN,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 8:
                list = await CardColonelsService.Create().GetUniqueCardColonelsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.CARD_COLONEL,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 9:
                list = await CardGeneralsService.Create().GetUniqueCardGeneralsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.CARD_GENERAL,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 10:
                list = await CardLivesService.Create().GetUniqueCardLivesIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.CARD_LIFE,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 11:
                list = await CardMilitariesService.Create().GetUniqueCardMilitariesIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.CARD_MILITARY,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 12:
                list = await CardMonstersService.Create().GetUniqueCardMonstersIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.CARD_MONSTER,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 13:
                list = await CardSpellsService.Create().GetUniqueCardSpellsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.CARD_SPELL,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 14:
                list = await CollaborationEquipmentsService.Create().GetUniqueCollaborationEquipmentsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.COLLABORATION_EQUIPMENT,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 15:
                list = await CollaborationsService.Create().GetUniqueCollaborationsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.COLLABORATION,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 16:
                list = await EquipmentsService.Create().GetUniqueEquipmentsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.EQUIPMENT,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 17:
                list = await ForgesService.Create().GetUniqueForgesIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.FORGE,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 18:
                list = await MagicFormationCirclesService.Create().GetUniqueMagicFormationCirclesIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.MAGIC_FORMATION_CIRCLE,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 19:
                list = await MedalsService.Create().GetUniqueMedalsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.MEDAL,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 20:
                list = await PetsService.Create().GetUniquePetsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.PET,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 21:
                list = await PuppetsService.Create().GetUniquePuppetsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.PUPPET,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 22:
                list = await RelicsService.Create().GetUniqueRelicsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.RELIC,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 23:
                list = await SkillsService.Create().GetUniqueSkillsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.SKILL,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 24:
                list = await SymbolsService.Create().GetUniqueSymbolsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.SYMBOL,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 25:
                list = await TalismansService.Create().GetUniqueTalismansIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.TALISMAN,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 26:
                list = await TitlesService.Create().GetUniqueTitlesIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.TITLE,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 27:
                list = await ItemsService.Create().GetUniqueItemsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.ITEM,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 28:
                list = await ItemsService.Create().GetUniqueItemsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.ITEM,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 29:
                list = await ItemsService.Create().GetUniqueItemsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.ITEM,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 30:
                list = await ItemsService.Create().GetUniqueItemsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.ITEM,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            case 31:
                list = await ItemsService.Create().GetUniqueItemsIdAsync();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = day.ToString(),
                        Date = currentDate,
                        Month = currentDate.Month,
                        Year = currentDate.Year,
                        Type = AppConstants.MainType.ITEM,
                        ObjectId = randomItem,
                        Quantity = 5000
                    };

                    await DailyCheckinService.Create().InsertDailyCheckinAsync(dailyCheckin);
                }
                break;
            default:
                break;
        }
    }
}
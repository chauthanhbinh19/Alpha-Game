using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DailyCheckinConfig : MonoBehaviour
{
    void Start()
    {
        CreateDailyCheckin();
    }
    public void CreateDailyCheckin()
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
                DailyCheckinService.Create().DeleteDailyCheckin(day.ToString());
                CreateAndInsertDailyCheckin(day, currentDate);
                // UserDailyCheckinService.Create().InsertUserDailyCheckin(User.CurrentUserId, day.ToString());
            }
        }
    }
    public void CreateAndInsertDailyCheckin(int day, DateTime currentDate)
    {
        List<string> list = new List<string>();
        switch (day)
        {
            case 1:
                list = CardHeroesService.Create().GetUniqueCardHeroId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "card_heroes",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 2:
                list = AlchemyService.Create().GetUniqueAlchemyId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "alchemy",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 3:
                list = AvatarsService.Create().GetUniqueAvatarsId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "avatars",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 4:
                list = BordersService.Create().GetUniqueBordersId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "borders",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 5:
                list = BooksService.Create().GetUniqueBookId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "books",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 6:
                list = CardAdmiralsService.Create().GetUniqueCardAdmiralsId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "card_admirals",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 7:
                list = CardCaptainsService.Create().GetUniqueCardCaptainsId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "card_captains",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 8:
                list = CardColonelsService.Create().GetUniqueCardColonelsId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "card_colonels",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 9:
                list = CardGeneralsService.Create().GetUniqueCardGeneralsId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "card_generals",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 10:
                list = CardLifeService.Create().GetUniqueCardLifeId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "card_life",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 11:
                list = CardMilitaryService.Create().GetUniqueCardMilitaryId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "card_military",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 12:
                list = CardMonstersService.Create().GetUniqueCardMonstersId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "card_monsters",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 13:
                list = CardSpellService.Create().GetUniqueCardSpellId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "card_spell",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 14:
                list = CollaborationEquipmentService.Create().GetUniqueCollaborationEquipmentId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "collaboration_equipment",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 15:
                list = CollaborationService.Create().GetUniqueCollaborationId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "collaboration",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 16:
                list = EquipmentsService.Create().GetUniqueEquipmentsId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "equipments",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 17:
                list = ForgeService.Create().GetUniqueForgeId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "forge",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 18:
                list = MagicFormationCircleService.Create().GetUniqueMagicFormationCircleId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "magic_formation_circle",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 19:
                list = MedalsService.Create().GetUniqueMedalId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "medals",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 20:
                list = PetsService.Create().GetUniquePetsId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "pet",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 21:
                list = PuppetService.Create().GetUniquePuppetId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "puppet",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 22:
                list = RelicsService.Create().GetUniqueRelicsId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "relic",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 23:
                list = SkillsService.Create().GetUniqueSkillsId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "skill",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 24:
                list = SymbolsService.Create().GetUniqueSymbolsId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "symbol",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 25:
                list = TalismanService.Create().GetUniqueTalismanId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "talisman",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 26:
                list = TitlesService.Create().GetUniqueTitleId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "title",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 27:
                list = ItemsService.Create().GetUniqueItemId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "item",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 28:
                list = ItemsService.Create().GetUniqueItemId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "item",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 29:
                list = ItemsService.Create().GetUniqueItemId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "item",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 30:
                list = ItemsService.Create().GetUniqueItemId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "item",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            case 31:
                list = ItemsService.Create().GetUniqueItemId();
                if (list != null && list.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, list.Count); // [0, list.Count)
                    string randomItem = list[randomIndex];

                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = day.ToString(),
                        date = currentDate,
                        month = currentDate.Month,
                        year = currentDate.Year,
                        type = "item",
                        object_id = randomItem,
                        quantity = 5000
                    };

                    DailyCheckinService.Create().InsertDailyCheckin(dailyCheckin);
                }
                break;
            default:
                break;
        }
    }
}
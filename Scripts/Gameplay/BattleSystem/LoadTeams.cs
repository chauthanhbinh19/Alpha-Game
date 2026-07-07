using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

// Lớp bọc kết quả trả về sau khi phân loại dữ liệu dựa trên quy ước của bạn
public class TeamDeploymentResult
{
    public List<CardBase> OnFieldCards = new List<CardBase>(); // Đang trên sân (MainPosition từ 1-10)
    public List<CardBase> BenchCards = new List<CardBase>();   // Đang ở hàng chờ (Chỉ lưu làm dữ liệu tạm)
}

public class LoadTeams
{
    public async Task<TeamDeploymentResult> LoadAndSortTeamAsync(string userId, string teamId)
    {
        // Gọi các hàm Async chạy song song từ Service của bạn
        var heroTask = UserCardHeroesService.Create().GetUserCardHeroesTeamWithoutPositionAsync(userId, teamId);
        var captainTask = UserCardCaptainsService.Create().GetUserCardCaptainsTeamWithoutPositionAsync(userId, teamId);
        var colonelTask = UserCardColonelsService.Create().GetUserCardColonelsTeamWithoutPositionAsync(userId, teamId);
        var generalTask = UserCardGeneralsService.Create().GetUserCardGeneralsTeamWithoutPositionAsync(userId, teamId);
        var admiralTask = UserCardAdmiralsService.Create().GetUserCardAdmiralsTeamWithoutPositionAsync(userId, teamId);
        var monsterTask = UserCardMonstersService.Create().GetUserCardMonstersTeamWithoutPositionAsync(userId, teamId);
        var militaryTask = UserCardMilitariesService.Create().GetUserCardMilitariesTeamWithoutPositionAsync(userId, teamId);
        var soldierTask = UserCardSoldiersService.Create().GetUserCardSoldiersTeamWithoutPositionAsync(userId, teamId);
        var spellTask = UserCardSpellsService.Create().GetUserCardSpellsTeamWithoutPositionAsync(userId, teamId);

        // 2. CHIA THEO NHÓM (BATCHING) - Mỗi lần chỉ cho 3 kết nối chạy đồng thời
        // Nhóm 1: Các tướng chính
        await Task.WhenAll(heroTask, captainTask, colonelTask);

        // Nhóm 2: Các cấp chỉ huy và quái vật
        await Task.WhenAll(generalTask, admiralTask, monsterTask);

        // Nhóm 3: Quân lính và phép bổ trợ
        await Task.WhenAll(militaryTask, soldierTask, spellTask);

        TeamDeploymentResult result = new TeamDeploymentResult();

        AddCardsToResult(heroTask.Result, () => new CardHero(), result);
        AddCardsToResult(captainTask.Result, () => new CardCaptain(), result);
        AddCardsToResult(colonelTask.Result, () => new CardColonel(), result);
        AddCardsToResult(generalTask.Result, () => new CardGeneral(), result);
        AddCardsToResult(admiralTask.Result, () => new CardAdmiral(), result);
        AddCardsToResult(militaryTask.Result, () => new CardMilitary(), result);
        AddCardsToResult(monsterTask.Result, () => new CardMonster(), result);
        AddCardsToResult(soldierTask.Result, () => new CardSoldier(), result);
        AddCardsToResult(spellTask.Result, () => new CardSpell(), result);

        return result;
    }
    private void AddCardsToResult<TSource>(IEnumerable<TSource> entities, System.Func<CardBase> cardFactory, TeamDeploymentResult result)
    {
        if (entities == null)
        {
            return;
        }

        foreach (var entity in entities)
        {
            try
            {
                CardBase card = cardFactory();
                card.Initialize(entity);

                if (card.MainPosition >= 1 && card.MainPosition <= 10)
                {
                    result.OnFieldCards.Add(card);
                }
                else
                {
                    result.BenchCards.Add(card);
                    if (!string.IsNullOrWhiteSpace(card.Position))
                    {
                        Debug.LogWarning($"[LoadTeams] Card '{card.Name}' position '{card.Position}' could not be mapped to a valid field slot.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[LoadTeams] Failed to initialize card of type {typeof(TSource).Name}: {ex.Message}");
            }
        }
    }

    // Hàm hỗ trợ: Chuyển đổi CardPenalty List sang Dictionary để tra cứu nhanh O(1)

}

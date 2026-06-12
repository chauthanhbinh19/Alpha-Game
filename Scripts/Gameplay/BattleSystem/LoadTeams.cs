using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

// Lớp bọc kết quả trả về sau khi phân loại dữ liệu dựa trên quy ước của bạn
public class TeamDeploymentResult
{
    public List<CardHero> OnFieldCards = new List<CardHero>(); // Đang trên sân (MainPosition từ 1-10)
    public List<CardBase> BenchCards = new List<CardBase>();   // Đang ở hàng chờ (Chỉ lưu làm dữ liệu tạm)
}

public class LoadTeams
{
    public async Task<TeamDeploymentResult> LoadPlayerTeamCardAsync(string userId, string teamId)
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

        await Task.WhenAll(heroTask, captainTask, colonelTask, generalTask, admiralTask, monsterTask, militaryTask, soldierTask, spellTask);

        TeamDeploymentResult result = new TeamDeploymentResult();

        // PHÂN LOẠI CARD HERO
        if (heroTask.Result != null)
        {
            foreach (var entity in heroTask.Result)
            {
                CardHero card = new CardHero();
                card.Initialize(entity); // Hàm nạp từ thực thể DB của bạn

                // Kiểm tra trực tiếp nếu MainPosition là số nguyên từ 1 đến 10
                if (card.MainPosition >= 1 && card.MainPosition <= 10)
                {
                    result.OnFieldCards.Add(card);
                }
                else
                {
                    result.BenchCards.Add(card);
                }
            }
        }

        // CÁC LOẠI CARD KHÁC: Tạm thời gom hết vào hàng chờ nền theo yêu cầu
        if (captainTask.Result != null) foreach (var e in captainTask.Result) { var c = new CardCaptain(); c.Initialize(e); result.BenchCards.Add(c); }
        if (colonelTask.Result != null) foreach (var e in colonelTask.Result) { var c = new CardColonel(); c.Initialize(e); result.BenchCards.Add(c); }
        if (generalTask.Result != null) foreach (var e in generalTask.Result) { var c = new CardGeneral(); c.Initialize(e); result.BenchCards.Add(c); }
        if (admiralTask.Result != null) foreach (var e in admiralTask.Result) { var c = new CardAdmiral(); c.Initialize(e); result.BenchCards.Add(c); }
        if (militaryTask.Result != null) foreach (var e in militaryTask.Result) { var c = new CardMilitary(); c.Initialize(e); result.BenchCards.Add(c); }
        if (monsterTask.Result != null) foreach (var e in monsterTask.Result) { var c = new CardMonster(); c.Initialize(e); result.BenchCards.Add(c); }
        if (soldierTask.Result != null) foreach (var e in soldierTask.Result) { var c = new CardSoldier(); c.Initialize(e); result.BenchCards.Add(c); }
        if (spellTask.Result != null) foreach (var e in spellTask.Result) { var c = new CardSpell(); c.Initialize(e); result.BenchCards.Add(c); }

        return result;
    }
    // Hàm hỗ trợ: Chuyển đổi CardPenalty List sang Dictionary để tra cứu nhanh O(1)

}

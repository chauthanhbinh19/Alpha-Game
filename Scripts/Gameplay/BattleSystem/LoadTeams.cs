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
        UserStatsContextDTO sharedContext = await UserStatsService.Create().GetUserStatsContextAsync(userId);
        // Gọi các hàm Async chạy song song từ Service của bạn
        var heroTask = UserCardHeroesService.Create().GetUserCardHeroesTeamWithoutPositionAsync(userId, teamId, sharedContext);
        var captainTask = UserCardCaptainsService.Create().GetUserCardCaptainsTeamWithoutPositionAsync(userId, teamId, sharedContext);
        var colonelTask = UserCardColonelsService.Create().GetUserCardColonelsTeamWithoutPositionAsync(userId, teamId, sharedContext);
        var generalTask = UserCardGeneralsService.Create().GetUserCardGeneralsTeamWithoutPositionAsync(userId, teamId, sharedContext);
        var admiralTask = UserCardAdmiralsService.Create().GetUserCardAdmiralsTeamWithoutPositionAsync(userId, teamId, sharedContext);
        var monsterTask = UserCardMonstersService.Create().GetUserCardMonstersTeamWithoutPositionAsync(userId, teamId, sharedContext);
        var militaryTask = UserCardMilitariesService.Create().GetUserCardMilitariesTeamWithoutPositionAsync(userId, teamId, sharedContext);
        var soldierTask = UserCardSoldiersService.Create().GetUserCardSoldiersTeamWithoutPositionAsync(userId, teamId, sharedContext);
        var spellTask = UserCardSpellsService.Create().GetUserCardSpellsTeamWithoutPositionAsync(userId, teamId, sharedContext);

        // 2. CHIA THEO NHÓM (BATCHING) - Mỗi lần chỉ cho 3 kết nối chạy đồng thời
        // Nhóm 1: Các tướng chính
        await Task.WhenAll(heroTask
        , captainTask, colonelTask
        );

        // Nhóm 2: Các cấp chỉ huy và quái vật
        await Task.WhenAll(generalTask, admiralTask, monsterTask);

        // Nhóm 3: Quân lính và phép bổ trợ
        await Task.WhenAll(militaryTask, soldierTask, spellTask);

        List<string> cardHeroIds = heroTask.Result.Select(hero => hero.Id).ToList();
        List<string> cardCaptainIds = captainTask.Result.Select(hero => hero.Id).ToList();
        List<string> cardColonelIds = colonelTask.Result.Select(hero => hero.Id).ToList();
        List<string> cardGeneralIds = generalTask.Result.Select(hero => hero.Id).ToList();
        List<string> cardAdmiralIds = admiralTask.Result.Select(hero => hero.Id).ToList();
        List<string> cardMonsterIds = monsterTask.Result.Select(hero => hero.Id).ToList();
        List<string> cardMilitaryIds = militaryTask.Result.Select(hero => hero.Id).ToList();
        List<string> cardSoldierIds = soldierTask.Result.Select(hero => hero.Id).ToList();
        List<string> cardSpellIds = spellTask.Result.Select(hero => hero.Id).ToList();

        var skillTask = UserSkillsService.Create().GetUserSkillsWithCardsAsync(
            userId,
            cardHeroIds,
            cardCaptainIds,
            cardColonelIds,
            cardGeneralIds,
            cardAdmiralIds,
            cardMonsterIds,
            cardMilitaryIds,
            cardSoldierIds,
            cardSpellIds);

        await Task.WhenAll(skillTask);

        var allSkills = skillTask.Result ?? new List<Skills>();

        // 1. Phân tích và nhóm Skills theo từng ID của từng loại Card (bây giờ selector nhận về List<CardSkillRelation>)
        var heroSkillsMap = BuildCardSkillsMap(allSkills, s => s.cardHeroIds);
        var captainSkillsMap = BuildCardSkillsMap(allSkills, s => s.cardCaptainIds);
        var colonelSkillsMap = BuildCardSkillsMap(allSkills, s => s.cardColonelIds);
        var generalSkillsMap = BuildCardSkillsMap(allSkills, s => s.cardGeneralIds);
        var admiralSkillsMap = BuildCardSkillsMap(allSkills, s => s.cardAdmiralIds);
        var monsterSkillsMap = BuildCardSkillsMap(allSkills, s => s.cardMonsterIds);
        var militarySkillsMap = BuildCardSkillsMap(allSkills, s => s.cardMilitaryIds);
        var soldierSkillsMap = BuildCardSkillsMap(allSkills, s => s.cardSoldierIds);
        var spellSkillsMap = BuildCardSkillsMap(allSkills, s => s.cardSpellIds);

        // 2. Tiến hành gán an toàn và tường minh cho từng danh sách Card (Đã được tự động sắp xếp theo Position)
        AssignSkillsToCards(heroTask.Result, heroSkillsMap, c => c.Id, (c, s) => c.Skills = s);
        AssignSkillsToCards(captainTask.Result, captainSkillsMap, c => c.Id, (c, s) => c.Skills = s);
        AssignSkillsToCards(colonelTask.Result, colonelSkillsMap, c => c.Id, (c, s) => c.Skills = s);
        AssignSkillsToCards(generalTask.Result, generalSkillsMap, c => c.Id, (c, s) => c.Skills = s);
        AssignSkillsToCards(admiralTask.Result, admiralSkillsMap, c => c.Id, (c, s) => c.Skills = s);
        AssignSkillsToCards(monsterTask.Result, monsterSkillsMap, c => c.Id, (c, s) => c.Skills = s);
        AssignSkillsToCards(militaryTask.Result, militarySkillsMap, c => c.Id, (c, s) => c.Skills = s);
        AssignSkillsToCards(soldierTask.Result, soldierSkillsMap, c => c.Id, (c, s) => c.Skills = s);
        AssignSkillsToCards(spellTask.Result, spellSkillsMap, c => c.Id, (c, s) => c.Skills = s);

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
    private Dictionary<string, List<Skills>> BuildCardSkillsMap(IEnumerable<Skills> allSkills, Func<Skills, IEnumerable<CardSkillRelation>> relationSelector)
    {
        var map = new Dictionary<string, List<Skills>>();
        if (allSkills == null) return map;

        foreach (var skill in allSkills)
        {
            // Lấy danh sách quan hệ chứa cả { id, pos }
            var relations = relationSelector(skill);
            if (relations == null) continue;

            foreach (var rel in relations)
            {
                if (rel == null || string.IsNullOrEmpty(rel.id)) continue;

                // Tạo một bản sao độc lập của Skill cho Card này
                var skillClone = skill.Clone();

                // Gán pos từ DB vào thuộc tính Position của bản sao Skill
                skillClone.Position = rel.pos;

                if (!map.TryGetValue(rel.id, out var skillList))
                {
                    skillList = new List<Skills>();
                    map[rel.id] = skillList;
                }
                skillList.Add(skillClone);
            }
        }
        return map;
    }
    private void AssignSkillsToCards<TCard>(IEnumerable<TCard> cards, Dictionary<string, List<Skills>> skillsMap, Func<TCard, string> idSelector, Action<TCard, List<Skills>> skillsAssigner) where TCard : class
    {
        if (cards == null) return;

        foreach (var card in cards)
        {
            if (card == null) continue;

            string id = idSelector(card);

            if (skillsMap.TryGetValue(id, out var assignedSkills))
            {
                // Sắp xếp các kỹ năng tăng dần theo Position trước khi gán
                var sortedSkills = assignedSkills.OrderBy(s => s.Position).ToList();
                skillsAssigner(card, sortedSkills);
            }
            else
            {
                skillsAssigner(card, new List<Skills>());
            }
        }
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

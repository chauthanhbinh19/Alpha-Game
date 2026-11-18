using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

public class TeamsService : ITeamsService
{
    private readonly ITeamsRepository _teamsRepository;

    public TeamsService(ITeamsRepository teamsRepository)
    {
        _teamsRepository = teamsRepository;
    }

    public static ITeamsService Create() => ServiceContainer.GetService<ITeamsService>();

    public async Task<List<Teams>> GetUserTeamsAsync(string userId)
        => await _teamsRepository.GetUserTeamsAsync(userId);

    public async Task<bool> InsertUserTeamsAsync(string userId, int team_number)
        => await _teamsRepository.InsertUserTeamsAsync(userId, team_number);

    // public int GetMaxTeamId(MySqlConnection connection)
    //     => _teamsRepository.GetMaxTeamId(connection);
    /// <summary>
    /// Chỉ gọi lúc vào game hoặc khi user thay đổi thành viên trong đội hình (Thêm/Xóa Card)
    /// </summary>
    public async Task InitializeCardAsync(string userId)
    {
        // 1. Khởi tạo song song 9 Task lấy dữ liệu từ DB
        var heroesTask = UserCardHeroesService.Create().GetUserCardHeroesInTeamAsync(userId);
        var captainsTask = UserCardCaptainsService.Create().GetUserCardCaptainsInTeamAsync(userId);
        var colonelsTask = UserCardColonelsService.Create().GetUserCardColonelsInTeamAsync(userId);
        var generalsTask = UserCardGeneralsService.Create().GetUserCardGeneralsInTeamAsync(userId);
        var admiralsTask = UserCardAdmiralsService.Create().GetUserCardAdmiralsInTeamAsync(userId);
        var monstersTask = UserCardMonstersService.Create().GetUserCardMonstersInTeamAsync(userId);
        var militariesTask = UserCardMilitariesService.Create().GetUserCardMilitariesInTeamAsync(userId);
        var soldiersTask = UserCardSoldiersService.Create().GetUserCardSoldiersInTeamAsync(userId);
        var spellsTask = UserCardSpellsService.Create().GetUserCardSpellsInTeamAsync(userId);

        await Task.WhenAll(
            heroesTask, captainsTask, colonelsTask,
            generalsTask, admiralsTask, monstersTask,
            militariesTask, soldiersTask, spellsTask
        );

        // 2. Gán dữ liệu thô vào User
        User.CardHeroes = heroesTask.Result;
        User.CardCaptains = captainsTask.Result;
        User.CardColonels = colonelsTask.Result;
        User.CardGenerals = generalsTask.Result;
        User.CardAdmirals = admiralsTask.Result;
        User.CardMonsters = monstersTask.Result;
        User.CardMilitaries = militariesTask.Result;
        User.CardSoldiers = soldiersTask.Result;
        User.CardSpells = spellsTask.Result;

        // 3. Chạy tính toán lực chiến thẻ bài ngay lần đầu
        RecalculateTeamStats();
    }

    /// <summary>
    /// Chạy hoàn toàn trên RAM mỗi khi User.UserPower thay đổi (Không gọi DB)
    /// </summary>
    public void RecalculateTeamStats()
    {
        TotalBuffs totalBuffs = new TotalBuffs();
        totalBuffs.AddBuff(User.UserPower);

        User.CardHeroes = EvaluateAndApplyBuffs(User.CardHeroes, c => { c.ApplyTotalBuffs(totalBuffs); c.RecalculatePower(); });
        User.CardCaptains = EvaluateAndApplyBuffs(User.CardCaptains, c => { c.ApplyTotalBuffs(totalBuffs); c.RecalculatePower(); });
        User.CardColonels = EvaluateAndApplyBuffs(User.CardColonels, c => { c.ApplyTotalBuffs(totalBuffs); c.RecalculatePower(); });
        User.CardGenerals = EvaluateAndApplyBuffs(User.CardGenerals, c => { c.ApplyTotalBuffs(totalBuffs); c.RecalculatePower(); });
        User.CardAdmirals = EvaluateAndApplyBuffs(User.CardAdmirals, c => { c.ApplyTotalBuffs(totalBuffs); c.RecalculatePower(); });
        User.CardMonsters = EvaluateAndApplyBuffs(User.CardMonsters, c => { c.ApplyTotalBuffs(totalBuffs); c.RecalculatePower(); });
        User.CardMilitaries = EvaluateAndApplyBuffs(User.CardMilitaries, c => { c.ApplyTotalBuffs(totalBuffs); c.RecalculatePower(); });
        User.CardSoldiers = EvaluateAndApplyBuffs(User.CardSoldiers, c => { c.ApplyTotalBuffs(totalBuffs); c.RecalculatePower(); });
        User.CardSpells = EvaluateAndApplyBuffs(User.CardSpells, c => { c.ApplyTotalBuffs(totalBuffs); c.RecalculatePower(); });
    }

    private List<T> EvaluateAndApplyBuffs<T>(List<T> cards, Action<T> applyBuffAndRecalculate) where T : IStats
    {
        if (cards == null || cards.Count == 0) return cards;

        cards = QualityEvaluatorHelper.GetQualityPower(cards);
        cards = LevelEvaluatorHelper.GetLevelPower(cards);
        cards = StarEvaluatorHelper.GetStarPower(cards);
        cards = ModuleEvaluatorHelper.GetModulePower(cards);
        cards = UpgradeEvaluatorHelper.GetUpgradePower(cards);

        foreach (var card in cards)
        {
            if (card == null) continue;
            applyBuffAndRecalculate(card);
        }

        return cards;
    }

    public async Task<double> GetTeamsPowerAsync(string userId)
    {
        double totalPower = 0;

        totalPower += User.CardAdmirals?.Sum(c => c.Power) ?? 0;
        totalPower += User.CardCaptains?.Sum(c => c.Power) ?? 0;
        totalPower += User.CardColonels?.Sum(c => c.Power) ?? 0;
        totalPower += User.CardGenerals?.Sum(c => c.Power) ?? 0;
        totalPower += User.CardHeroes?.Sum(c => c.Power) ?? 0;
        totalPower += User.CardMilitaries?.Sum(c => c.Power) ?? 0;
        totalPower += User.CardMonsters?.Sum(c => c.Power) ?? 0;
        totalPower += User.CardSoldiers?.Sum(c => c.Power) ?? 0;
        totalPower += User.CardSpells?.Sum(c => c.Power) ?? 0;

        // Chỉ ghi DB khi tổng lực chiến thực sự thay đổi
        if (Math.Abs(User.CurrentUserPower - totalPower) > 0.0001)
        {
            User.CurrentUserPower = totalPower;
            await UserService.Create().UpdateUserPowerAsync(userId, totalPower);
        }

        return totalPower;
    }

    public Task<List<TeamEmblems>> GetUserTeamEmblemsAsync(string userId, string teamId, int position, string cardType)
    {
        return _teamsRepository.GetUserTeamEmblemsAsync(userId, teamId, position, cardType);
    }

    public async Task<List<Objects>> GetUserObjectsInTeamAsync(string userId, UserStatsContextDTO context = null)
    {
        var result = await _teamsRepository.GetUserObjectsInTeamAsync(userId);

        TotalBuffs totalBuffs = new TotalBuffs();
        totalBuffs.AddBuff(context.UserPowersData);
        totalBuffs.AddBuff(context.PowerManagerData);
        // totalBuffs.AddBuff(context.ScienceFictionData);
        // totalBuffs.AddBuff(context.ResearchData);
        // totalBuffs.AddBuff(context.ArchiveData);
        // totalBuffs.AddBuff(context.UniverseData);
        // totalBuffs.AddBuff(context.HiinData);
        // totalBuffs.AddBuff(context.SswnData);
        // totalBuffs.AddBuff(context.HitnData);
        // totalBuffs.AddBuff(context.HihnData);
        // totalBuffs.AddBuff(context.HienData);
        // totalBuffs.AddBuff(context.HicaData);
        // totalBuffs.AddBuff(context.HirnData);
        // totalBuffs.AddBuff(context.HidcData);
        // totalBuffs.AddBuff(context.HicbData);
        // totalBuffs.AddBuff(context.HisnData);
        // totalBuffs.AddBuff(context.AnimeStatsData);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        // result = StatsHelper.GetAllEquipmentPower(result, equipments);

        foreach (var card in result)
        {
            if (card == null) continue; // Phòng hờ phần tử trong result bị null

            // Áp dụng tổng buff (Flat + % Base stats)
            card.ApplyTotalBuffs(totalBuffs);

            // Gán Skills an toàn, tránh tạo List thừa
            // card.Skills = skillsLookup.Contains(card.Id)
            //     ? skillsLookup[card.Id].ToList()
            //     : new List<Skills>();

            // Tính toán lại tổng lực chiến (Sau khi đã có đầy đủ chỉ số và Skills)
            card.RecalculatePower();
        }

        return result;
    }

    public Task<bool> InsertUserTeamEmblemsAsync(string userId, string teamId, int position, EmblemDTO emblemDTO)
    {
        return _teamsRepository.InsertUserTeamEmblemsAsync(userId, teamId, position, emblemDTO);
    }

    public Task<bool> DeleteUserTeamEmblemsAsync(string userId, string teamId, int position, string cardType)
    {
        return _teamsRepository.DeleteUserTeamEmblemsAsync(userId, teamId, position, cardType);
    }

    public Task<bool> UpdateUserCardHeroesTeamPositionsAsync(string userId)
    {
        return _teamsRepository.UpdateUserCardHeroesTeamPositionsAsync(userId);
    }

    public Task<bool> UpdateUserCardCaptainsTeamPositionsAsync(string userId)
    {
        return _teamsRepository.UpdateUserCardCaptainsTeamPositionsAsync(userId);
    }

    public Task<bool> UpdateUserCardColonelsTeamPositionsAsync(string userId)
    {
        return _teamsRepository.UpdateUserCardColonelsTeamPositionsAsync(userId);
    }

    public Task<bool> UpdateUserCardGeneralsTeamPositionsAsync(string userId)
    {
        return _teamsRepository.UpdateUserCardGeneralsTeamPositionsAsync(userId);
    }

    public Task<bool> UpdateUserCardAdmiralsTeamPositionsAsync(string userId)
    {
        return _teamsRepository.UpdateUserCardAdmiralsTeamPositionsAsync(userId);
    }

    public Task<bool> UpdateUserCardMonstersTeamPositionsAsync(string userId)
    {
        return _teamsRepository.UpdateUserCardMonstersTeamPositionsAsync(userId);
    }

    public Task<bool> UpdateUserCardMilitariesTeamPositionsAsync(string userId)
    {
        return _teamsRepository.UpdateUserCardMilitariesTeamPositionsAsync(userId);
    }

    public Task<bool> UpdateUserCardSoldiersTeamPositionsAsync(string userId)
    {
        return _teamsRepository.UpdateUserCardSoldiersTeamPositionsAsync(userId);
    }

    public Task<bool> UpdateUserCardSpellsTeamPositionsAsync(string userId)
    {
        return _teamsRepository.UpdateUserCardSpellsTeamPositionsAsync(userId);
    }
}

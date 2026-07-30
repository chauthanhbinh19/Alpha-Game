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

    public async Task<List<Teams>> GetUserTeamsAsync(string user_id)
        => await _teamsRepository.GetUserTeamsAsync(user_id);

    public async Task<bool> InsertUserTeamsAsync(string user_id, int team_number)
        => await _teamsRepository.InsertUserTeamsAsync(user_id, team_number);

    // public int GetMaxTeamId(MySqlConnection connection)
    //     => _teamsRepository.GetMaxTeamId(connection);

    public async Task<double> GetTeamsPowerAsync(string userId)
    {
        UserStatsContextDTO sharedContext = await UserStatsService.Create().GetUserStatsContextAsync(userId);
        BaseStats cardHeroesStats = await UserCardHeroesService.Create().GetTeamTotalStatsAsync(userId, sharedContext);
        BaseStats cardCaptainsStats = await UserCardCaptainsService.Create().GetTeamTotalStatsAsync(userId, sharedContext);
        BaseStats cardColonelsStats = await UserCardColonelsService.Create().GetTeamTotalStatsAsync(userId, sharedContext);
        BaseStats cardGeneralsStats = await UserCardGeneralsService.Create().GetTeamTotalStatsAsync(userId, sharedContext);
        BaseStats cardAdmiralsStats = await UserCardAdmiralsService.Create().GetTeamTotalStatsAsync(userId, sharedContext);
        BaseStats cardMonstersStats = await UserCardMonstersService.Create().GetTeamTotalStatsAsync(userId, sharedContext);
        BaseStats cardMilitariesStats = await UserCardMilitariesService.Create().GetTeamTotalStatsAsync(userId, sharedContext);
        BaseStats cardSpellsStats = await UserCardSpellsService.Create().GetTeamTotalStatsAsync(userId, sharedContext);
        BaseStats booksStats = await UserBooksService.Create().GetTeamTotalStatsAsync(userId, sharedContext);
        BaseStats petsStats = await UserPetsService.Create().GetTeamTotalStatsAsync(userId, sharedContext);

        double totalPower = 0;

        // Sử dụng Sum() của LINQ
        totalPower += cardHeroesStats.Power;
        totalPower += cardCaptainsStats.Power;
        totalPower += cardColonelsStats.Power;
        totalPower += cardGeneralsStats.Power;
        totalPower += cardAdmiralsStats.Power;
        totalPower += cardMonstersStats.Power;
        totalPower += cardMilitariesStats.Power;
        totalPower += cardSpellsStats.Power;
        totalPower += booksStats.Power;
        totalPower += petsStats.Power;

        await UserService.Create().UpdateUserPowerAsync(userId, totalPower);

        return totalPower;
    }

    public Task<List<TeamEmblems>> GetUserTeamEmblemsAsync(string user_id, string team_id, int position, string cardType)
    {
        return _teamsRepository.GetUserTeamEmblemsAsync(user_id, team_id, position, cardType);
    }

    public Task<bool> InsertUserTeamEmblemsAsync(string user_id, string teamId, int position, EmblemDTO emblemDTO)
    {
        return _teamsRepository.InsertUserTeamEmblemsAsync(user_id, teamId, position, emblemDTO);
    }

    public Task<bool> DeleteUserTeamEmblemsAsync(string user_id, string teamId, int position, string cardType)
    {
        return _teamsRepository.DeleteUserTeamEmblemsAsync(user_id, teamId, position, cardType);
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

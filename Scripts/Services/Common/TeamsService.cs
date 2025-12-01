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

    public static TeamsService Create()
    {
        return new TeamsService(new TeamsRepository());
    }

    public async Task<List<Teams>> GetUserTeamsAsync(string user_id)
        => await _teamsRepository.GetUserTeamsAsync(user_id);

    public async Task<bool> InsertUserTeamsAsync(string user_id, int team_number)
        => await _teamsRepository.InsertUserTeamsAsync(user_id, team_number);

    // public int GetMaxTeamId(MySqlConnection connection)
    //     => _teamsRepository.GetMaxTeamId(connection);

    public async Task<double> GetTeamsPowerAsync(string user_id)
    {
        List<CardHeroes> cardHeroes = UserCardHeroesService.Create().GetAllUserCardHeroesInTeam(user_id);

        double totalPower = 0;

        // Sử dụng Sum() của LINQ
        totalPower += cardHeroes.Sum(c => c.Power);
        totalPower += UserCardCaptainsService.Create().GetAllUserCardCaptainsInTeam(user_id).Sum(c => c.Power);
        totalPower += UserCardColonelsService.Create().GetAllUserCardColonelsInTeam(user_id).Sum(c => c.Power);
        totalPower += UserCardGeneralsService.Create().GetAllUserCardGeneralsInTeam(user_id).Sum(c => c.Power);
        totalPower += UserCardAdmiralsService.Create().GetAllUserCardAdmiralsInTeam(user_id).Sum(c => c.Power);
        totalPower += UserCardMonstersService.Create().GetAllUserCardMonstersInTeam(user_id).Sum(c => c.Power);
        totalPower += UserCardMilitaryService.Create().GetAllUserCardMilitaryInTeam(user_id).Sum(c => c.Power);
        totalPower += UserCardSpellService.Create().GetAllUserCardSpellInTeam(user_id).Sum(c => c.Power);
        totalPower += UserBooksService.Create().GetAllUserBooksInTeam(user_id).Sum(c => c.Power);
        totalPower += UserPetsService.Create().GetAllUserPetsInTeam(user_id).Sum(c => c.Power);

        await UserService.Create().UpdateUserPowerAsync(user_id, totalPower);

        return totalPower;
    }
}

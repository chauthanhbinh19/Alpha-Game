using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

public class TeamsService : ITeamsService
{
    private static TeamsService _instance;
    private readonly ITeamsRepository _teamsRepository;

    public TeamsService(ITeamsRepository teamsRepository)
    {
        _teamsRepository = teamsRepository;
    }

    public static TeamsService Create()
    {
        if (_instance == null)
        {
            _instance = new TeamsService(new TeamsRepository());
        }
        return _instance;
    }

    public async Task<List<Teams>> GetUserTeamsAsync(string user_id)
        => await _teamsRepository.GetUserTeamsAsync(user_id);

    public async Task<bool> InsertUserTeamsAsync(string user_id, int team_number)
        => await _teamsRepository.InsertUserTeamsAsync(user_id, team_number);

    // public int GetMaxTeamId(MySqlConnection connection)
    //     => _teamsRepository.GetMaxTeamId(connection);

    public async Task<double> GetTeamsPowerAsync(string user_id)
    {
        List<CardHeroes> cardHeroes = await UserCardHeroesService.Create().GetAllUserCardHeroesInTeamAsync(user_id);

        double totalPower = 0;

        // Sử dụng Sum() của LINQ
        totalPower += cardHeroes.Sum(c => c.Power);
        totalPower += (await UserCardCaptainsService.Create().GetAllUserCardCaptainsInTeamAsync(user_id))
            .Sum(c => c.Power);

        totalPower += (await UserCardColonelsService.Create().GetAllUserCardColonelsInTeamAsync(user_id))
            .Sum(c => c.Power);

        totalPower += (await UserCardGeneralsService.Create().GetAllUserCardGeneralsInTeamAsync(user_id))
            .Sum(c => c.Power);

        totalPower += (await UserCardAdmiralsService.Create().GetAllUserCardAdmiralsInTeamAsync(user_id))
            .Sum(c => c.Power);

        totalPower += (await UserCardMonstersService.Create().GetAllUserCardMonstersInTeamAsync(user_id))
            .Sum(c => c.Power);

        totalPower += (await UserCardMilitariesService.Create().GetAllUserCardMilitariesInTeamAsync(user_id))
            .Sum(c => c.Power);

        totalPower += (await UserCardSpellsService.Create().GetAllUserCardSpellsInTeamAsync(user_id))
            .Sum(c => c.Power);

        totalPower += (await UserBooksService.Create().GetAllUserBooksInTeamAsync(user_id))
            .Sum(c => c.Power);

        totalPower += (await UserPetsService.Create().GetAllUserPetsInTeamAsync(user_id))
            .Sum(c => c.Power);

        await UserService.Create().UpdateUserPowerAsync(user_id, totalPower);

        return totalPower;
    }
}

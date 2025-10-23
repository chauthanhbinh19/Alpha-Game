using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

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

    public List<Teams> GetUserTeams(string user_id)
        => _teamsRepository.GetUserTeams(user_id);

    public bool InsertUserTeams(string user_id, int team_number)
        => _teamsRepository.InsertUserTeams(user_id, team_number);

    public int GetMaxTeamId(MySqlConnection connection)
        => _teamsRepository.GetMaxTeamId(connection);

    public double GetTeamsPower(string user_id)
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

        return totalPower;
    }
}

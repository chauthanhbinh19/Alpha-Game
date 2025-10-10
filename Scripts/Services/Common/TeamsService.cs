using System.Collections.Generic;
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
        double totalPower = 0;
        List<CardHeroes> cardHeroes = UserCardHeroesService.Create().GetAllUserCardHeroesInTeam(user_id);
        List<CardCaptains> cardCaptains = UserCardCaptainsService.Create().GetAllUserCardCaptainsInTeam(user_id);
        List<CardColonels> cardColonels = UserCardColonelsService.Create().GetAllUserCardColonelsInTeam(user_id);
        List<CardGenerals> cardGenerals = UserCardGeneralsService.Create().GetAllUserCardGeneralsInTeam(user_id);
        List<CardAdmirals> cardAdmirals = UserCardAdmiralsService.Create().GetAllUserCardAdmiralsInTeam(user_id);
        List<CardMonsters> cardMonsters = UserCardMonstersService.Create().GetAllUserCardMonstersInTeam(user_id);
        List<CardMilitary> cardMilitaries = UserCardMilitaryService.Create().GetAllUserCardMilitaryInTeam(user_id);
        List<CardSpell> cardSpells = UserCardSpellService.Create().GetAllUserCardSpellInTeam(user_id);
        List<Books> books = UserBooksService.Create().GetAllUserBooksInTeam(user_id);
        List<Pets> pets = UserPetsService.Create().GetAllUserPetsInTeam(user_id);

        foreach (CardHeroes c in cardHeroes)
        {
            totalPower = totalPower + c.power;
        }
        foreach (CardCaptains c in cardCaptains)
        {
            totalPower = totalPower + c.power;
        }
        foreach (CardColonels c in cardColonels)
        {
            totalPower = totalPower + c.power;
        }
        foreach (CardGenerals c in cardGenerals)
        {
            totalPower = totalPower + c.power;
        }
        foreach (CardAdmirals c in cardAdmirals)
        {
            totalPower = totalPower + c.power;
        }
        foreach (CardMonsters c in cardMonsters)
        {
            totalPower = totalPower + c.power;
        }
        foreach (CardMilitary c in cardMilitaries)
        {
            totalPower = totalPower + c.power;
        }
        foreach (CardSpell c in cardSpells)
        {
            totalPower = totalPower + c.power;
        }
        foreach (Books c in books)
        {
            totalPower = totalPower + c.power;
        }
        foreach (Pets c in pets)
        {
            totalPower = totalPower + c.power;
        }
        return totalPower;
    }
}

using System.Collections.Generic;
using System.Linq;

public class TeamSetupService
{
    private LoadTeams _loadTeamService;
    public TeamSetupService()
    {
        _loadTeamService = new LoadTeams();
    }
    private CardType GetCardBaseType(CardBase card)
    {
        if (card is CardHero) return CardType.CardHero;
        if (card is CardCaptain) return CardType.CardCaptain;
        if (card is CardColonel) return CardType.CardColonel;
        if (card is CardGeneral) return CardType.CardGeneral;
        if (card is CardAdmiral) return CardType.CardAdmiral;
        if (card is CardMonster) return CardType.CardMonster;
        if (card is CardMilitary) return CardType.CardMilitary;
        if (card is CardSpell) return CardType.CardSpell;

        throw new System.InvalidOperationException("Unknown card type.");
    }

    public static CardContract CreateRandomContract(string contractName)
    {
        return ContractRandomizer.RandomizePositions(contractName);
    }

    public static CardPenalty CreateRandomPenalty(string penaltyName)
    {
        return PenaltyRandomizer.GenerateRandomPenalties(penaltyName);
    }
    
    public void SetupPlayerTeam(string userId, string teamId)
    {
        List<CardBase> allLoadedCards = _loadTeamService.LoadPlayerTeamCard(userId, teamId);
    }
    
    // private List<CardBase> GetFinalTeamCards(List<CardBase> availableCards, CardContract contract, Dictionary<CardType, int> penaltyLookup)
    // {

    // }
}
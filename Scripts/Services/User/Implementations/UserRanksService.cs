using System.Collections.Generic;
using System.Threading.Tasks;
public class UserRanksService : IUserRanksService
{
    private static UserRanksService _instance;
    private readonly IUserRanksRepository _userRanksRepository;

    public UserRanksService(IUserRanksRepository userRanksRepository)
    {
        _userRanksRepository = userRanksRepository;
    }

    public static UserRanksService Create()
    {
        if (_instance == null)
        {
            _instance = new UserRanksService(new UserRanksRepository());
        }
        return _instance;
    }

    public async Task<UserRanks> GetUserRanksAsync(string id)
    {
        return await _userRanksRepository.GetUserRanksAsync(id);
    }

    public async Task<UserRanks> GetSumUserRanksAsync(string user_id)
    {
        return await _userRanksRepository.GetSumUserRanksAsync(user_id);
    }

    public async Task InsertOrUpdateUserRanksAsync(string userId, UserRanks Ranks, string id, IStats stat)
    {
        if(stat is CardHeroes cardHero)
        {
            await UserCardHeroesRankService.Create().InsertOrUpdateCardHeroRankAsync(userId, Ranks, cardHero.Id);
        }
        else if (stat is CardCaptains cardCaptain)
        {
            await UserCardCaptainsRankService.Create().InsertOrUpdateCardCaptainRankAsync(userId, Ranks, cardCaptain.Id);
        }
        else if (stat is CardColonels cardColonel)
        {
            await UserCardColonelsRankService.Create().InsertOrUpdateCardColonelRankAsync(userId, Ranks, cardColonel.Id);
        }
        else if (stat is CardGenerals cardGeneral)
        {
            await UserCardGeneralsRankService.Create().InsertOrUpdateCardGeneralRankAsync(userId, Ranks, cardGeneral.Id);
        }
        else if (stat is CardAdmirals cardAdmiral)
        {
            await UserCardAdmiralsRankService.Create().InsertOrUpdateCardAdmiralRankAsync(userId, Ranks, cardAdmiral.Id);
        }
        else if (stat is CardMilitaries cardMilitary)
        {
            await UserCardMilitariesRankService.Create().InsertOrUpdateCardMilitaryRankAsync(userId, Ranks, cardMilitary.Id);
        }
        else if (stat is CardMonsters cardMonster)
        {
            await UserCardMonstersRankService.Create().InsertOrUpdateCardMonsterRankAsync(userId, Ranks, cardMonster.Id);
        }
        else if (stat is CardSpells cardSpell)
        {
            await UserCardSpellsRankService.Create().InsertOrUpdateCardSpellRankAsync(userId, Ranks, cardSpell.Id);
        }
        else if (stat is CardSoldiers cardSoldier)
        {
            await UserCardSoldiersRankService.Create().InsertOrUpdateCardSoldierRankAsync(userId, Ranks, cardSoldier.Id);
        }
        else if (stat is Books book)
        {
            await UserBooksRankService.Create().InsertOrUpdateBookRankAsync(userId, Ranks, book.Id);
        }
        else if (stat is Pets pet)
        {
            await UserPetsRankService.Create().InsertOrUpdatePetRankAsync(userId, Ranks, pet.Id);
        }
        await _userRanksRepository.InsertOrUpdateUserRanksAsync(userId, Ranks, id);
    }

}
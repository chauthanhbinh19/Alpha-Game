using System.Collections.Generic;
using System.Threading.Tasks;
public class UserRanksService : IUserRanksService
{
    private readonly IUserRanksRepository _userRanksRepository;

    public UserRanksService(IUserRanksRepository userRanksRepository)
    {
        _userRanksRepository = userRanksRepository;
    }

    public static IUserRanksService Create() => ServiceContainer.GetService<IUserRanksService>();

    public async Task<UserRanks> GetUserRanksAsync(string userId, string id)
    {
        return await _userRanksRepository.GetUserRanksAsync(userId, id);
    }

    public async Task<UserRanks> GetSumUserRanksAsync(string userId)
    {
        return await _userRanksRepository.GetSumUserRanksAsync(userId);
    }

    public async Task InsertOrUpdateUserRanksAsync(string userId, UserRanks Ranks, string id, IStats stat)
    {
        if(stat is CardHeroes cardHero)
        {
            await UserCardHeroesRankService.Create().InsertOrUpdateUserCardHeroRankAsync(userId, Ranks, cardHero.Id);
        }
        else if (stat is CardCaptains cardCaptain)
        {
            await UserCardCaptainsRankService.Create().InsertOrUpdateUserCardCaptainRankAsync(userId, Ranks, cardCaptain.Id);
        }
        else if (stat is CardColonels cardColonel)
        {
            await UserCardColonelsRankService.Create().InsertOrUpdateUserCardColonelRankAsync(userId, Ranks, cardColonel.Id);
        }
        else if (stat is CardGenerals cardGeneral)
        {
            await UserCardGeneralsRankService.Create().InsertOrUpdateUserCardGeneralRankAsync(userId, Ranks, cardGeneral.Id);
        }
        else if (stat is CardAdmirals cardAdmiral)
        {
            await UserCardAdmiralsRankService.Create().InsertOrUpdateUserCardAdmiralRankAsync(userId, Ranks, cardAdmiral.Id);
        }
        else if (stat is CardMilitaries cardMilitary)
        {
            await UserCardMilitariesRankService.Create().InsertOrUpdateUserCardMilitaryRankAsync(userId, Ranks, cardMilitary.Id);
        }
        else if (stat is CardMonsters cardMonster)
        {
            await UserCardMonstersRankService.Create().InsertOrUpdateUserCardMonsterRankAsync(userId, Ranks, cardMonster.Id);
        }
        else if (stat is CardSpells cardSpell)
        {
            await UserCardSpellsRankService.Create().InsertOrUpdateUserCardSpellRankAsync(userId, Ranks, cardSpell.Id);
        }
        else if (stat is CardSoldiers cardSoldier)
        {
            await UserCardSoldiersRankService.Create().InsertOrUpdateUserCardSoldierRankAsync(userId, Ranks, cardSoldier.Id);
        }
        else if (stat is Books book)
        {
            await UserBooksRankService.Create().InsertOrUpdateUserBookRankAsync(userId, Ranks, book.Id);
        }
        else if (stat is Pets pet)
        {
            await UserPetsRankService.Create().InsertOrUpdateUserPetRankAsync(userId, Ranks, pet.Id);
        }
        await _userRanksRepository.InsertOrUpdateUserRanksAsync(userId, Ranks, id);
    }

}
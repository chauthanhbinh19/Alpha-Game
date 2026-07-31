using System.Collections.Generic;
using System.Threading.Tasks;
public class UserMastersService : IUserMastersService
{
    private readonly IUserMastersRepository _userMastersRepository;

    public UserMastersService(IUserMastersRepository userMastersRepository)
    {
        _userMastersRepository = userMastersRepository;
    }

    public static IUserMastersService Create() => ServiceContainer.GetService<IUserMastersService>();

    public async Task<UserMasters> GetUserMastersAsync(string userId, string id)
    {
        return await _userMastersRepository.GetUserMastersAsync(userId, id);
    }

    public async Task<UserMasters> GetSumUserMastersAsync(string userId)
    {
        return await _userMastersRepository.GetSumUserMastersAsync(userId);
    }

    public async Task InsertOrUpdateUserMastersAsync(string userId, UserMasters Masters, string id, IStats stat)
    {
        if(stat is CardHeroes cardHero)
        {
            await UserCardHeroesMasterService.Create().InsertOrUpdateUserCardHeroMasterAsync(userId, Masters, cardHero.Id);
        }
        else if (stat is CardCaptains cardCaptain)
        {
            await UserCardCaptainsMasterService.Create().InsertOrUpdateUserCardCaptainMasterAsync(userId, Masters, cardCaptain.Id);
        }
        else if (stat is CardColonels cardColonel)
        {
            await UserCardColonelsMasterService.Create().InsertOrUpdateUserCardColonelMasterAsync(userId, Masters, cardColonel.Id);
        }
        else if (stat is CardGenerals cardGeneral)
        {
            await UserCardGeneralsMasterService.Create().InsertOrUpdateUserCardGeneralMasterAsync(userId, Masters, cardGeneral.Id);
        }
        else if (stat is CardAdmirals cardAdmiral)
        {
            await UserCardAdmiralsMasterService.Create().InsertOrUpdateUserCardAdmiralMasterAsync(userId, Masters, cardAdmiral.Id);
        }
        else if (stat is CardMilitaries cardMilitary)
        {
            await UserCardMilitariesMasterService.Create().InsertOrUpdateUserCardMilitaryMasterAsync(userId, Masters, cardMilitary.Id);
        }
        else if (stat is CardMonsters cardMonster)
        {
            await UserCardMonstersMasterService.Create().InsertOrUpdateUserCardMonsterMasterAsync(userId, Masters, cardMonster.Id);
        }
        else if (stat is CardSpells cardSpell)
        {
            await UserCardSpellsMasterService.Create().InsertOrUpdateUserCardSpellMasterAsync(userId, Masters, cardSpell.Id);
        }
        else if (stat is CardSoldiers cardSoldier)
        {
            await UserCardSoldiersMasterService.Create().InsertOrUpdateUserCardSoldierMasterAsync(userId, Masters, cardSoldier.Id);
        }
        else if (stat is Books book)
        {
            await UserBooksMasterService.Create().InsertOrUpdateUserBookMasterAsync(userId, Masters, book.Id);
        }
        else if (stat is Pets pet)
        {
            await UserPetsMasterService.Create().InsertOrUpdateUserPetMasterAsync(userId, Masters, pet.Id);
        }
        await _userMastersRepository.InsertOrUpdateUserMastersAsync(userId, Masters, id);
    }

}
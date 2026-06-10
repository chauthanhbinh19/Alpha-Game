using System.Collections.Generic;
using System.Threading.Tasks;
public class UserMastersService : IUserMastersService
{
    private static UserMastersService _instance;
    private readonly IUserMastersRepository _userMastersRepository;

    public UserMastersService(IUserMastersRepository userMastersRepository)
    {
        _userMastersRepository = userMastersRepository;
    }

    public static UserMastersService Create()
    {
        if (_instance == null)
        {
            _instance = new UserMastersService(new UserMastersRepository());
        }
        return _instance;
    }

    public async Task<UserMasters> GetUserMastersAsync(string id)
    {
        return await _userMastersRepository.GetUserMastersAsync(id);
    }

    public async Task<UserMasters> GetSumUserMastersAsync(string user_id)
    {
        return await _userMastersRepository.GetSumUserMastersAsync(user_id);
    }

    public async Task InsertOrUpdateUserMastersAsync(string userId, UserMasters Masters, string id, IStats stat)
    {
        if(stat is CardHeroes cardHero)
        {
            await UserCardHeroesMasterService.Create().InsertOrUpdateCardHeroMasterAsync(userId, Masters, cardHero.Id);
        }
        else if (stat is CardCaptains cardCaptain)
        {
            await UserCardCaptainsMasterService.Create().InsertOrUpdateCardCaptainMasterAsync(userId, Masters, cardCaptain.Id);
        }
        else if (stat is CardColonels cardColonel)
        {
            await UserCardColonelsMasterService.Create().InsertOrUpdateCardColonelMasterAsync(userId, Masters, cardColonel.Id);
        }
        else if (stat is CardGenerals cardGeneral)
        {
            await UserCardGeneralsMasterService.Create().InsertOrUpdateCardGeneralMasterAsync(userId, Masters, cardGeneral.Id);
        }
        else if (stat is CardAdmirals cardAdmiral)
        {
            await UserCardAdmiralsMasterService.Create().InsertOrUpdateCardAdmiralMasterAsync(userId, Masters, cardAdmiral.Id);
        }
        else if (stat is CardMilitaries cardMilitary)
        {
            await UserCardMilitariesMasterService.Create().InsertOrUpdateCardMilitaryMasterAsync(userId, Masters, cardMilitary.Id);
        }
        else if (stat is CardMonsters cardMonster)
        {
            await UserCardMonstersMasterService.Create().InsertOrUpdateCardMonsterMasterAsync(userId, Masters, cardMonster.Id);
        }
        else if (stat is CardSpells cardSpell)
        {
            await UserCardSpellsMasterService.Create().InsertOrUpdateCardSpellMasterAsync(userId, Masters, cardSpell.Id);
        }
        else if (stat is CardSoldiers cardSoldier)
        {
            await UserCardSoldiersMasterService.Create().InsertOrUpdateCardSoldierMasterAsync(userId, Masters, cardSoldier.Id);
        }
        else if (stat is Books book)
        {
            await UserBooksMasterService.Create().InsertOrUpdateBookMasterAsync(userId, Masters, book.Id);
        }
        else if (stat is Pets pet)
        {
            await UserPetsMasterService.Create().InsertOrUpdatePetMasterAsync(userId, Masters, pet.Id);
        }
        await _userMastersRepository.InsertOrUpdateUserMastersAsync(userId, Masters, id);
    }

}
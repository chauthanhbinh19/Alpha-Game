using System.Collections.Generic;
using System.Threading.Tasks;

public class MasterService : IMasterService
{
    
    public async Task UpLevelAsync(object data, Master master, string type)
    {
        if (data is Equipments cardHero)
        {
            await UserCardHeroesMasterService.Create().InsertOrUpdateCardHeroMasterAsync(master, cardHero.Id);
        }
        else if (data is Books book)
        {
            await UserBooksMasterService.Create().InsertOrUpdateBookMasterAsync(master, book.Id);
        }
        else if (data is CardCaptains cardCaptain)
        {
            await UserCardCaptainsMasterService.Create().InsertOrUpdateCardCaptainMasterAsync(master, cardCaptain.Id);
        }
        else if (data is Pets pet)
        {
            await UserPetsMasterService.Create().InsertOrUpdatePetMasterAsync(master, pet.Id);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            await UserCardMilitariesMasterService.Create().InsertOrUpdateCardMilitaryMasterAsync(master, cardMilitary.Id);
        }
        else if (data is CardSpells cardSpell)
        {
            await UserCardSpellsMasterService.Create().InsertOrUpdateCardSpellMasterAsync(master, cardSpell.Id);
        }
        else if (data is CardMonsters cardMonster)
        {
            await UserCardMonstersMasterService.Create().InsertOrUpdateCardMonsterMasterAsync(master, cardMonster.Id);
        }
        else if (data is CardColonels cardColonel)
        {
            await UserCardColonelsMasterService.Create().InsertOrUpdateCardColonelMasterAsync(master, cardColonel.Id);
        }
        else if (data is CardGenerals cardGeneral)
        {
            await UserCardGeneralsMasterService.Create().InsertOrUpdateCardGeneralMasterAsync(master, cardGeneral.Id);
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            await UserCardAdmiralsMasterService.Create().InsertOrUpdateCardAdmiralMasterAsync(master, cardAdmiral.Id);
        }
        else if (data is CardSoldiers cardSoldier)
        {
            await UserCardSoldiersMasterService.Create().InsertOrUpdateCardSoldierMasterAsync(master, cardSoldier.Id);
        }
    }
}
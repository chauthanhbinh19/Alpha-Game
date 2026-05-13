using System.Collections.Generic;
using System.Threading.Tasks;

public class RankService : IRankService
{
    public async Task UpLevelAsync(object data, Rank rank, string type)
    {
        if (data is Equipments cardHero)
        {
            await UserCardHeroesRankService.Create().InsertOrUpdateCardHeroRankAsync(rank, cardHero.Id);
        }
        else if (data is Books book)
        {
            await UserBooksRankService.Create().InsertOrUpdateBookRankAsync(rank, book.Id);
        }
        else if (data is CardCaptains cardCaptain)
        {
            await UserCardCaptainsRankService.Create().InsertOrUpdateCardCaptainRankAsync(rank, cardCaptain.Id);
        }
        else if (data is Pets pet)
        {
            await UserPetsRankService.Create().InsertOrUpdatePetRankAsync(rank, pet.Id);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            await UserCardMilitariesRankService.Create().InsertOrUpdateCardMilitaryRankAsync(rank, cardMilitary.Id);
        }
        else if (data is CardSpells cardSpell)
        {
            await UserCardSpellsRankService.Create().InsertOrUpdateCardSpellRankAsync(rank, cardSpell.Id);
        }
        else if (data is CardMonsters cardMonster)
        {
            await UserCardMonstersRankService.Create().InsertOrUpdateCardMonsterRankAsync(rank, cardMonster.Id);
        }
        else if (data is CardColonels cardColonel)
        {
            await UserCardColonelsRankService.Create().InsertOrUpdateCardColonelRankAsync(rank, cardColonel.Id);
        }
        else if (data is CardGenerals cardGeneral)
        {
            await UserCardGeneralsRankService.Create().InsertOrUpdateCardGeneralRankAsync(rank, cardGeneral.Id);
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            await UserCardAdmiralsRankService.Create().InsertOrUpdateCardAdmiralRankAsync(rank, cardAdmiral.Id);
        }
        else if (data is CardSoldiers cardSoldier)
        {
            await UserCardSoldiersRankService.Create().InsertOrUpdateCardSoldierRankAsync(rank, cardSoldier.Id);
        }
    }
}
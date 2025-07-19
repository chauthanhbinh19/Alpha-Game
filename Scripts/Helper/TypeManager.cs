using System;
using System.Collections.Generic;

public static class TypeManager
{
    private static readonly Dictionary<string, Func<List<string>>> typeHandlers = new Dictionary<string, Func<List<string>>>(StringComparer.OrdinalIgnoreCase)
    {
        { AppConstants.CardHero, () => new CardHeroesService(new CardHeroesRepository()).GetUniqueCardHeroTypes() },
        { AppConstants.Book, () => new BooksService(new BooksRepository()).GetUniqueBookTypes() },
        { AppConstants.CardCaptain, () => new CardCaptainsService(new CardCaptainsRepository()).GetUniqueCardCaptainsTypes() },
        { AppConstants.CollaborationEquipment, () => new CollaborationEquipmentService(new CollaborationEquipmentRepository()).GetUniqueCollaborationEquipmentTypes() },
        { AppConstants.Equipment, () => new EquipmentsService(new EquipmentsRepository()).GetUniqueEquipmentsTypes() },
        { AppConstants.Pet, () => new PetsService(new PetsRepository()).GetUniquePetsTypes() },
        { AppConstants.Skill, () => new SkillsService(new SkillsRepository()).GetUniqueSkillsTypes() },
        { AppConstants.Symbol, () => new SymbolsService(new SymbolsRepository()).GetUniqueSymbolsTypes() },
        { AppConstants.CardMilitary, () => new CardMilitaryService(new CardMilitaryRepository()).GetUniqueCardMilitaryTypes() },
        { AppConstants.CardSpell, () => new CardSpellService(new CardSpellRepository()).GetUniqueCardSpellTypes() },
        { AppConstants.MagicFormationCircle, () => new MagicFormationCircleService(new MagicFormationCircleRepository()).GetUniqueMagicFormationCircleTypes() },
        { AppConstants.Relic, () => new RelicsService(new RelicsRepository()).GetUniqueRelicsTypes() },
        { AppConstants.CardMonster, () => new CardMonstersService(new CardMonstersRepository()).GetUniqueCardMonstersTypes() },
        { AppConstants.CardColonel, () => new CardColonelsService(new CardColonelsRepository()).GetUniqueCardColonelsTypes() },
        { AppConstants.CardGeneral, () => new CardGeneralsService(new CardGeneralsRepository()).GetUniqueCardGeneralsTypes() },
        { AppConstants.CardAdmiral, () => new CardAdmiralsService(new CardAdmiralsRepository()).GetUniqueCardAdmiralsTypes() },
        { AppConstants.Talisman, () => new TalismanService(new TalismanRepository()).GetUniqueTalismanTypes() },
        { AppConstants.Puppet, () => new PuppetService(new PuppetRepository()).GetUniquePuppetTypes() },
        { AppConstants.Alchemy, () => new AlchemyService(new AlchemyRepository()).GetUniqueAlchemyTypes() },
        { AppConstants.Forge, () => new ForgeService(new ForgeRepository()).GetUniqueForgeTypes() },
        { AppConstants.CardLife, () => new CardLifeService(new CardLifeRepository()).GetUniqueCardLifeTypes() },
        { AppConstants.Artwork, () => new ArtworkService(new ArtworkRepository()).GetUniqueArtworkTypes() },
        { AppConstants.SummonCardHeroes, () => new CardHeroesService(new CardHeroesRepository()).GetUniqueCardHeroTypes() },
        { AppConstants.SummonBooks, () => new BooksService(new BooksRepository()).GetUniqueBookTypes() },
        { AppConstants.SummonCardCaptains, () => new CardCaptainsService(new CardCaptainsRepository()).GetUniqueCardCaptainsTypes() },
        { AppConstants.SummonCardMilitaries, () => new CardMilitaryService(new CardMilitaryRepository()).GetUniqueCardMilitaryTypes() },
        { AppConstants.SummonCardSpells, () => new CardSpellService(new CardSpellRepository()).GetUniqueCardSpellTypes() },
        { AppConstants.SummonCardMonsters, () => new CardMonstersService(new CardMonstersRepository()).GetUniqueCardMonstersTypes() },
        { AppConstants.SummonCardColonels, () => new CardColonelsService(new CardColonelsRepository()).GetUniqueCardColonelsTypes() },
        { AppConstants.SummonCardGenerals, () => new CardGeneralsService(new CardGeneralsRepository()).GetUniqueCardGeneralsTypes() },
        { AppConstants.SummonCardAdmirals, () => new CardAdmiralsService(new CardAdmiralsRepository()).GetUniqueCardAdmiralsTypes() },
    };
    public static List<string> GetUniqueTypes(string type)
    {
        if (typeHandlers.TryGetValue(type, out var handler))
        {
            return handler();
        }
        return new List<string>();
    }
}
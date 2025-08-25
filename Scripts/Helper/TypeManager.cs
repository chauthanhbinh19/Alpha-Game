using System;
using System.Collections.Generic;

public static class TypeManager
{
    private static readonly Dictionary<string, Func<List<string>>> typeHandlers = new Dictionary<string, Func<List<string>>>(StringComparer.OrdinalIgnoreCase)
    {
        { AppConstants.MainType.CardHero, () => new CardHeroesService(new CardHeroesRepository()).GetUniqueCardHeroTypes() },
        { AppConstants.MainType.Book, () => new BooksService(new BooksRepository()).GetUniqueBookTypes() },
        { AppConstants.MainType.CardCaptain, () => new CardCaptainsService(new CardCaptainsRepository()).GetUniqueCardCaptainsTypes() },
        { AppConstants.MainType.CollaborationEquipment, () => new CollaborationEquipmentService(new CollaborationEquipmentRepository()).GetUniqueCollaborationEquipmentTypes() },
        { AppConstants.MainType.Equipment, () => new EquipmentsService(new EquipmentsRepository()).GetUniqueEquipmentsTypes() },
        { AppConstants.MainType.Pet, () => new PetsService(new PetsRepository()).GetUniquePetsTypes() },
        { AppConstants.MainType.Skill, () => new SkillsService(new SkillsRepository()).GetUniqueSkillsTypes() },
        { AppConstants.MainType.Symbol, () => new SymbolsService(new SymbolsRepository()).GetUniqueSymbolsTypes() },
        { AppConstants.MainType.CardMilitary, () => new CardMilitaryService(new CardMilitaryRepository()).GetUniqueCardMilitaryTypes() },
        { AppConstants.MainType.CardSpell, () => new CardSpellService(new CardSpellRepository()).GetUniqueCardSpellTypes() },
        { AppConstants.MainType.MagicFormationCircle, () => new MagicFormationCircleService(new MagicFormationCircleRepository()).GetUniqueMagicFormationCircleTypes() },
        { AppConstants.MainType.Relic, () => new RelicsService(new RelicsRepository()).GetUniqueRelicsTypes() },
        { AppConstants.MainType.CardMonster, () => new CardMonstersService(new CardMonstersRepository()).GetUniqueCardMonstersTypes() },
        { AppConstants.MainType.CardColonel, () => new CardColonelsService(new CardColonelsRepository()).GetUniqueCardColonelsTypes() },
        { AppConstants.MainType.CardGeneral, () => new CardGeneralsService(new CardGeneralsRepository()).GetUniqueCardGeneralsTypes() },
        { AppConstants.MainType.CardAdmiral, () => new CardAdmiralsService(new CardAdmiralsRepository()).GetUniqueCardAdmiralsTypes() },
        { AppConstants.MainType.Talisman, () => new TalismanService(new TalismanRepository()).GetUniqueTalismanTypes() },
        { AppConstants.MainType.Puppet, () => new PuppetService(new PuppetRepository()).GetUniquePuppetTypes() },
        { AppConstants.MainType.Alchemy, () => new AlchemyService(new AlchemyRepository()).GetUniqueAlchemyTypes() },
        { AppConstants.MainType.Forge, () => new ForgeService(new ForgeRepository()).GetUniqueForgeTypes() },
        { AppConstants.MainType.CardLife, () => new CardLifeService(new CardLifeRepository()).GetUniqueCardLifeTypes() },
        { AppConstants.MainType.Artwork, () => new ArtworkService(new ArtworkRepository()).GetUniqueArtworkTypes() },
        { AppConstants.MainType.SummonCardHeroes, () => new CardHeroesService(new CardHeroesRepository()).GetUniqueCardHeroTypes() },
        { AppConstants.MainType.SummonBooks, () => new BooksService(new BooksRepository()).GetUniqueBookTypes() },
        { AppConstants.MainType.SummonCardCaptains, () => new CardCaptainsService(new CardCaptainsRepository()).GetUniqueCardCaptainsTypes() },
        { AppConstants.MainType.SummonCardMilitaries, () => new CardMilitaryService(new CardMilitaryRepository()).GetUniqueCardMilitaryTypes() },
        { AppConstants.MainType.SummonCardSpells, () => new CardSpellService(new CardSpellRepository()).GetUniqueCardSpellTypes() },
        { AppConstants.MainType.SummonCardMonsters, () => new CardMonstersService(new CardMonstersRepository()).GetUniqueCardMonstersTypes() },
        { AppConstants.MainType.SummonCardColonels, () => new CardColonelsService(new CardColonelsRepository()).GetUniqueCardColonelsTypes() },
        { AppConstants.MainType.SummonCardGenerals, () => new CardGeneralsService(new CardGeneralsRepository()).GetUniqueCardGeneralsTypes() },
        { AppConstants.MainType.SummonCardAdmirals, () => new CardAdmiralsService(new CardAdmiralsRepository()).GetUniqueCardAdmiralsTypes() },
        { AppConstants.MainType.Item, () => new ItemsService(new ItemsRepository()).GetUniqueItemTypes() },
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
using System;
using System.Collections.Generic;

public static class TypeManager
{
    private static readonly Dictionary<string, Func<List<string>>> typeHandlers = new Dictionary<string, Func<List<string>>>(StringComparer.OrdinalIgnoreCase)
    {
        { AppConstants.MainType.CARD_HERO, () => new CardHeroesService(new CardHeroesRepository()).GetUniqueCardHeroTypes() },
        { AppConstants.MainType.BOOK, () => new BooksService(new BooksRepository()).GetUniqueBookTypes() },
        { AppConstants.MainType.CARD_CAPTAIN, () => new CardCaptainsService(new CardCaptainsRepository()).GetUniqueCardCaptainsTypes() },
        { AppConstants.MainType.COLLABORATION_EQUIPMENT, () => new CollaborationEquipmentService(new CollaborationEquipmentRepository()).GetUniqueCollaborationEquipmentTypes() },
        { AppConstants.MainType.EQUIPMENT, () => new EquipmentsService(new EquipmentsRepository()).GetUniqueEquipmentsTypes() },
        { AppConstants.MainType.PET, () => new PetsService(new PetsRepository()).GetUniquePetsTypes() },
        { AppConstants.MainType.SKILL, () => new SkillsService(new SkillsRepository()).GetUniqueSkillsTypes() },
        { AppConstants.MainType.SYMBOL, () => new SymbolsService(new SymbolsRepository()).GetUniqueSymbolsTypes() },
        { AppConstants.MainType.CARD_MILITARY, () => new CardMilitaryService(new CardMilitaryRepository()).GetUniqueCardMilitaryTypes() },
        { AppConstants.MainType.CARD_SPELL, () => new CardSpellService(new CardSpellRepository()).GetUniqueCardSpellTypes() },
        { AppConstants.MainType.MAGIC_FORMATION_CIRCLE, () => new MagicFormationCircleService(new MagicFormationCircleRepository()).GetUniqueMagicFormationCircleTypes() },
        { AppConstants.MainType.RELIC, () => new RelicsService(new RelicsRepository()).GetUniqueRelicsTypes() },
        { AppConstants.MainType.CARD_MONSTER, () => new CardMonstersService(new CardMonstersRepository()).GetUniqueCardMonstersTypes() },
        { AppConstants.MainType.CARD_COLONEL, () => new CardColonelsService(new CardColonelsRepository()).GetUniqueCardColonelsTypes() },
        { AppConstants.MainType.CARD_GENERAL, () => new CardGeneralsService(new CardGeneralsRepository()).GetUniqueCardGeneralsTypes() },
        { AppConstants.MainType.CARD_ADMIRAL, () => new CardAdmiralsService(new CardAdmiralsRepository()).GetUniqueCardAdmiralsTypes() },
        { AppConstants.MainType.TALISMAN, () => new TalismanService(new TalismanRepository()).GetUniqueTalismanTypes() },
        { AppConstants.MainType.PUPPET, () => new PuppetService(new PuppetRepository()).GetUniquePuppetTypes() },
        { AppConstants.MainType.ALCHEMY, () => new AlchemyService(new AlchemyRepository()).GetUniqueAlchemyTypes() },
        { AppConstants.MainType.FORGE, () => new ForgeService(new ForgeRepository()).GetUniqueForgeTypes() },
        { AppConstants.MainType.CARD_LIFE, () => new CardLifeService(new CardLifeRepository()).GetUniqueCardLifeTypes() },
        { AppConstants.MainType.ARTWORK, () => new ArtworkService(new ArtworkRepository()).GetUniqueArtworkTypes() },
        { AppConstants.MainType.SUMMON_CARD_HEROES, () => new CardHeroesService(new CardHeroesRepository()).GetUniqueCardHeroTypes() },
        { AppConstants.MainType.SUMMON_BOOKS, () => new BooksService(new BooksRepository()).GetUniqueBookTypes() },
        { AppConstants.MainType.SUMMON_CARD_CAPTAINS, () => new CardCaptainsService(new CardCaptainsRepository()).GetUniqueCardCaptainsTypes() },
        { AppConstants.MainType.SUMMON_CARD_MILITARY, () => new CardMilitaryService(new CardMilitaryRepository()).GetUniqueCardMilitaryTypes() },
        { AppConstants.MainType.SUMMON_CARD_SPELLS, () => new CardSpellService(new CardSpellRepository()).GetUniqueCardSpellTypes() },
        { AppConstants.MainType.SUMMON_CARD_MONSTERS, () => new CardMonstersService(new CardMonstersRepository()).GetUniqueCardMonstersTypes() },
        { AppConstants.MainType.SUMMON_CARD_COLONELS, () => new CardColonelsService(new CardColonelsRepository()).GetUniqueCardColonelsTypes() },
        { AppConstants.MainType.SUMMON_CARD_GENERALS, () => new CardGeneralsService(new CardGeneralsRepository()).GetUniqueCardGeneralsTypes() },
        { AppConstants.MainType.SUMMON_CARD_ADMIRALS, () => new CardAdmiralsService(new CardAdmiralsRepository()).GetUniqueCardAdmiralsTypes() },
        { AppConstants.MainType.ITEM, () => new ItemsService(new ItemsRepository()).GetUniqueItemTypes() },
        { AppConstants.MainType.SPIRIT_CARD, () => new SpiritCardService(new SpiritCardRepository()).GetUniqueSpiritCardTypes() },
        { AppConstants.MainType.CARDS, () => new CardsService(new CardsRepository()).GetUniqueCardId() },
        { AppConstants.MainType.ARCHITECTURE, () => new ArchitecturesService(new ArchitecturesRepository()).GetUniqueArchitectureId() },
        { AppConstants.MainType.TECHNOLOGY, () => new TechnologiesService(new TechnologiesRepository()).GetUniqueTechnologyId() },
        { AppConstants.MainType.VEHICLE, () => new VehiclesService(new VehiclesRepository()).GetUniqueVehicleTypes() },
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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class TypeManager
{
    private static readonly Dictionary<string, Func<Task<List<string>>>> typeHandlers =
    new Dictionary<string, Func<Task<List<string>>>>(StringComparer.OrdinalIgnoreCase)
{
    { AppConstants.MainType.CARD_HERO, async () => await CardHeroesService.Create().GetUniqueCardHeroesTypesAsync() },
    { AppConstants.MainType.BOOK, async () => await BooksService.Create().GetUniqueBooksIdAsync() },
    { AppConstants.MainType.CARD_CAPTAIN, async () => await CardCaptainsService.Create().GetUniqueCardCaptainsTypesAsync() },
    { AppConstants.MainType.COLLABORATION_EQUIPMENT, async () => await CollaborationEquipmentsService.Create().GetUniqueCollaborationEquipmentsTypesAsync() },
    { AppConstants.MainType.EQUIPMENT, async () => await EquipmentsService.Create().GetUniqueEquipmentsTypesAsync() },
    { AppConstants.MainType.PET, async () => await PetsService.Create().GetUniquePetsTypesAsync() },
    { AppConstants.MainType.SKILL, async () => await SkillsService.Create().GetUniqueSkillsTypesAsync() },
    { AppConstants.MainType.SYMBOL, async () => await SymbolsService.Create().GetUniqueSymbolsTypesAsync() },
    { AppConstants.MainType.CARD_MILITARY, async () => await CardMilitariesService.Create().GetUniqueCardMilitariesTypesAsync() },
    { AppConstants.MainType.CARD_SPELL, async () => await CardSpellsService.Create().GetUniqueCardSpellsTypesAsync() },
    { AppConstants.MainType.MAGIC_FORMATION_CIRCLE, async () => await MagicFormationCirclesService.Create().GetUniqueMagicFormationCirclesTypesAsync() },
    { AppConstants.MainType.RELIC, async () => await RelicsService.Create().GetUniqueRelicsTypesAsync() },
    { AppConstants.MainType.CARD_MONSTER, async () => await CardMonstersService.Create().GetUniqueCardMonstersTypesAsync() },
    { AppConstants.MainType.CARD_COLONEL, async () => await CardColonelsService.Create().GetUniqueCardColonelsTypesAsync() },
    { AppConstants.MainType.CARD_GENERAL, async () => await CardGeneralsService.Create().GetUniqueCardGeneralsTypesAsync() },
    { AppConstants.MainType.CARD_ADMIRAL, async () => await CardAdmiralsService.Create().GetUniqueCardAdmiralsTypesAsync() },
    { AppConstants.MainType.TALISMAN, async () => await TalismansService.Create().GetUniqueTalismansTypesAsync() },
    { AppConstants.MainType.PUPPET, async () => await PuppetsService.Create().GetUniquePuppetsTypesAsync() },
    { AppConstants.MainType.ALCHEMY, async () => await AlchemiesService.Create().GetUniqueAlchemiesTypesAsync() },
    { AppConstants.MainType.FORGE, async () => await ForgesService.Create().GetUniqueForgesTypesAsync() },
    { AppConstants.MainType.CARD_LIFE, async () => await CardLivesService.Create().GetUniqueCardLivesTypesAsync() },
    { AppConstants.MainType.ARTWORK, async () => await ArtworksService.Create().GetUniqueArtworksTypesAsync() },

    // SUMMON
    { AppConstants.MainType.SUMMON_CARD_HEROES, async () => await CardHeroesService.Create().GetUniqueCardHeroesTypesAsync() },
    { AppConstants.MainType.SUMMON_BOOKS, async () => await BooksService.Create().GetUniqueBooksIdAsync() },
    { AppConstants.MainType.SUMMON_CARD_CAPTAINS, async () => await CardCaptainsService.Create().GetUniqueCardCaptainsTypesAsync() },
    { AppConstants.MainType.SUMMON_CARD_MILITARY, async () => await CardMilitariesService.Create().GetUniqueCardMilitariesTypesAsync() },
    { AppConstants.MainType.SUMMON_CARD_SPELLS, async () => await CardSpellsService.Create().GetUniqueCardSpellsTypesAsync() },
    { AppConstants.MainType.SUMMON_CARD_MONSTERS, async () => await CardMonstersService.Create().GetUniqueCardMonstersTypesAsync() },
    { AppConstants.MainType.SUMMON_CARD_COLONELS, async () => await CardColonelsService.Create().GetUniqueCardColonelsTypesAsync() },
    { AppConstants.MainType.SUMMON_CARD_GENERALS, async () => await CardGeneralsService.Create().GetUniqueCardGeneralsTypesAsync() },
    { AppConstants.MainType.SUMMON_CARD_ADMIRALS, async () => await CardAdmiralsService.Create().GetUniqueCardAdmiralsTypesAsync() },

    { AppConstants.MainType.ITEM, async () => await ItemsService.Create().GetUniqueItemsTypesAsync() },
    { AppConstants.MainType.SPIRIT_CARD, async () => await SpiritCardsService.Create().GetUniqueSpiritCardsTypesAsync() },

    // FIXED lỗi VEHICLE
    { AppConstants.MainType.VEHICLE, async () => await VehiclesService.Create().GetUniqueVehiclesTypesAsync() },
};
    public static async Task<List<string>> GetUniqueTypesAsync(string type)
    {
        if (typeHandlers.TryGetValue(type, out var handler))
        {
            return await handler();
        }
        return new List<string>();
    }
}
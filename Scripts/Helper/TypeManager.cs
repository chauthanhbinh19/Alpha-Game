using System;
using System.Collections.Generic;

public static class TypeManager
{
    private static readonly Dictionary<string, Func<List<string>>> typeHandlers = new Dictionary<string, Func<List<string>>>(StringComparer.OrdinalIgnoreCase)
    {
        { "CardHeroes", () => new CardHeroesService(new CardHeroesRepository()).GetUniqueCardHeroTypes() },
        { "Books", () => new BooksService(new BooksRepository()).GetUniqueBookTypes() },
        { "CardCaptains", () => new CardCaptainsService(new CardCaptainsRepository()).GetUniqueCardCaptainsTypes() },
        { "CollaborationEquipments", () => new CollaborationEquipmentService(new CollaborationEquipmentRepository()).GetUniqueCollaborationEquipmentTypes() },
        { "Equipments", () => new EquipmentsService(new EquipmentsRepository()).GetUniqueEquipmentsTypes() },
        { "Pets", () => new PetsService(new PetsRepository()).GetUniquePetsTypes() },
        { "Skills", () => new SkillsService(new SkillsRepository()).GetUniqueSkillsTypes() },
        { "Symbols", () => new SymbolsService(new SymbolsRepository()).GetUniqueSymbolsTypes() },
        { "CardMilitary", () => new CardMilitaryService(new CardMilitaryRepository()).GetUniqueCardMilitaryTypes() },
        { "CardSpell", () => new CardSpellService(new CardSpellRepository()).GetUniqueCardSpellTypes() },
        { "MagicFormationCircle", () => new MagicFormationCircleService(new MagicFormationCircleRepository()).GetUniqueMagicFormationCircleTypes() },
        { "Relics", () => new RelicsService(new RelicsRepository()).GetUniqueRelicsTypes() },
        { "CardMonsters", () => new CardMonstersService(new CardMonstersRepository()).GetUniqueCardMonstersTypes() },
        { "CardColonels", () => new CardColonelsService(new CardColonelsRepository()).GetUniqueCardColonelsTypes() },
        { "CardGenerals", () => new CardGeneralsService(new CardGeneralsRepository()).GetUniqueCardGeneralsTypes() },
        { "CardAdmirals", () => new CardAdmiralsService(new CardAdmiralsRepository()).GetUniqueCardAdmiralsTypes() },
        { "Talisman", () => new TalismanService(new TalismanRepository()).GetUniqueTalismanTypes() },
        { "Puppet", () => new PuppetService(new PuppetRepository()).GetUniquePuppetTypes() },
        { "Alchemy", () => new AlchemyService(new AlchemyRepository()).GetUniqueAlchemyTypes() },
        { "Forge", () => new ForgeService(new ForgeRepository()).GetUniqueForgeTypes() },
        { "CardLife", () => new CardLifeService(new CardLifeRepository()).GetUniqueCardLifeTypes() },
        { "SummonCardHeroes", () => new CardHeroesService(new CardHeroesRepository()).GetUniqueCardHeroTypes() },
        { "SummonBooks", () => new BooksService(new BooksRepository()).GetUniqueBookTypes() },
        { "SummonCardCaptains", () => new CardCaptainsService(new CardCaptainsRepository()).GetUniqueCardCaptainsTypes() },
        { "SummonCardMilitary", () => new CardMilitaryService(new CardMilitaryRepository()).GetUniqueCardMilitaryTypes() },
        { "SummonCardSpell", () => new CardSpellService(new CardSpellRepository()).GetUniqueCardSpellTypes() },
        { "SummonCardMonsters", () => new CardMonstersService(new CardMonstersRepository()).GetUniqueCardMonstersTypes() },
        { "SummonCardColonels", () => new CardColonelsService(new CardColonelsRepository()).GetUniqueCardColonelsTypes() },
        { "SummonCardGenerals", () => new CardGeneralsService(new CardGeneralsRepository()).GetUniqueCardGeneralsTypes() },
        { "SummonCardAdmirals", () => new CardAdmiralsService(new CardAdmiralsRepository()).GetUniqueCardAdmiralsTypes() },
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
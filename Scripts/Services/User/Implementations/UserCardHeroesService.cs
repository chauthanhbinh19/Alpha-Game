using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

public class UserCardHeroesService : IUserCardHeroesService
{
    private readonly IUserCardHeroesRepository _userCardHeroesRepository;
    private readonly ICardHeroesGalleryService _cardHeroesGalleryService;
    private readonly IUserSkillsRepository _userSkillsRepository;
    private readonly IPatternsService _patternsService;
    private readonly IUserStatsService _userStatsService;

    public UserCardHeroesService(
        IUserCardHeroesRepository userCardHeroesRepository,
        ICardHeroesGalleryService cardHeroesGalleryService,
        IUserSkillsRepository userSkillsRepository,
        IPatternsService patternsService,
        IUserStatsService userStatsService)
    {
        _userCardHeroesRepository = userCardHeroesRepository;
        _cardHeroesGalleryService = cardHeroesGalleryService;
        _userSkillsRepository = userSkillsRepository;
        _patternsService = patternsService;
        _userStatsService = userStatsService;
    }

    public static IUserCardHeroesService Create() => ServiceContainer.GetService<IUserCardHeroesService>();

    public async Task<List<CardHeroes>> GetAllEquipmentPowerAsync(string userId, List<CardHeroes> CardHeroesList)
    {
        foreach (var c in CardHeroesList)
        {
            Equipments equipments = await UserEquipmentsService.Create().GetAllUserEquipmentsByCardHeorIdAsync(userId, c.Id);
            c.Health = c.Health + equipments.Health + equipments.SpecialHealth;
            c.PhysicalAttack = c.PhysicalAttack + equipments.PhysicalAttack + equipments.SpecialPhysicalAttack;
            c.PhysicalDefense = c.PhysicalDefense + equipments.PhysicalDefense + equipments.SpecialPhysicalDefense;
            c.MagicalAttack = c.MagicalAttack + equipments.MagicalAttack + equipments.SpecialMagicalAttack;
            c.MagicalDefense = c.MagicalDefense + equipments.MagicalDefense + equipments.SpecialMagicalDefense;
            c.ChemicalAttack = c.ChemicalAttack + equipments.ChemicalAttack + equipments.SpecialChemicalAttack;
            c.ChemicalDefense = c.ChemicalDefense + equipments.ChemicalDefense + equipments.SpecialChemicalDefense;
            c.AtomicAttack = c.AtomicAttack + equipments.AtomicAttack + equipments.SpecialAtomicAttack;
            c.AtomicDefense = c.AtomicDefense + equipments.AtomicDefense + equipments.SpecialAtomicDefense;
            c.MentalAttack = c.MentalAttack + equipments.MentalAttack + equipments.SpecialMentalAttack;
            c.MentalDefense = c.MentalDefense + equipments.MentalDefense + equipments.SpecialMentalDefense;
            c.Speed = c.Speed + equipments.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + equipments.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + equipments.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + equipments.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + equipments.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + equipments.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + equipments.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + equipments.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + equipments.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + equipments.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + equipments.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + equipments.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + equipments.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + equipments.AccuracyRate;
            c.LifestealRate = c.LifestealRate + equipments.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + equipments.ShieldStrength;
            c.Tenacity = c.Tenacity + equipments.Tenacity;
            c.ResistanceRate = c.ResistanceRate + equipments.ResistanceRate;
            c.ComboRate = c.ComboRate + equipments.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + equipments.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + equipments.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + equipments.ComboResistanceRate;
            c.StunRate = c.StunRate + equipments.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + equipments.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + equipments.ReflectionRate;
            c.IgnoreReflectionRate = c.IgnoreReflectionRate + equipments.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + equipments.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + equipments.ReflectionResistanceRate;
            c.Mana = c.Mana + equipments.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + equipments.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + equipments.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + equipments.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + equipments.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + equipments.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + equipments.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + equipments.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + equipments.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + equipments.SkillResistanceRate;

            c.Power = PowerHelper.CalculatePower(
            c.Health,
            c.PhysicalAttack, c.PhysicalDefense,
            c.MagicalAttack, c.MagicalDefense,
            c.ChemicalAttack, c.ChemicalDefense,
            c.AtomicAttack, c.AtomicDefense,
            c.MentalAttack, c.MentalDefense,
            c.Speed,
            c.CriticalDamageRate, c.CriticalRate, c.CriticalResistanceRate, c.IgnoreCriticalRate,
            c.PenetrationRate, c.PenetrationResistanceRate, c.EvasionRate,
            c.DamageAbsorptionRate, c.IgnoreDamageAbsorptionRate, c.AbsorbedDamageRate,
            c.VitalityRegenerationRate, c.VitalityRegenerationResistanceRate,
            c.AccuracyRate, c.LifestealRate,
            c.ShieldStrength, c.Tenacity, c.ResistanceRate,
            c.ComboRate, c.IgnoreComboRate, c.ComboDamageRate, c.ComboResistanceRate,
            c.StunRate, c.IgnoreStunRate,
            c.ReflectionRate, c.IgnoreReflectionRate, c.ReflectionDamageRate, c.ReflectionResistanceRate,
            c.Mana, c.ManaRegenerationRate,
            c.DamageToDifferentFactionRate, c.ResistanceToDifferentFactionRate,
            c.DamageToSameFactionRate, c.ResistanceToSameFactionRate,
            c.NormalDamageRate, c.NormalResistanceRate,
            c.SkillDamageRate, c.SkillResistanceRate
        );
        }
        return CardHeroesList;
    }
    public async Task<List<CardHeroes>> GetAllRankPowerAsync(string userId, List<CardHeroes> CardHeroesList)
    {
        foreach (var c in CardHeroesList)
        {
            Rank rank = await UserCardHeroesRankService.Create().GetSumUserCardHeroesRankAsync(userId, c.Id);
            c.Health = c.Health + rank.Health + c.BaseStats.Health * rank.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + rank.PhysicalAttack + c.BaseStats.PhysicalAttack * rank.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + rank.PhysicalDefense + c.BaseStats.PhysicalDefense * rank.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + rank.MagicalAttack + c.BaseStats.MagicalAttack * rank.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + rank.MagicalDefense + c.BaseStats.MagicalDefense * rank.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + rank.ChemicalAttack + c.BaseStats.ChemicalAttack * rank.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + rank.ChemicalDefense + c.BaseStats.ChemicalDefense * rank.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + rank.AtomicAttack + c.BaseStats.AtomicAttack * rank.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + rank.AtomicDefense + c.BaseStats.AtomicDefense * rank.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + rank.MentalAttack + c.BaseStats.MentalAttack * rank.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + rank.MentalDefense + c.BaseStats.MentalDefense * rank.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + rank.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + rank.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + rank.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + rank.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + rank.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + rank.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + rank.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + rank.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + rank.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + rank.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + rank.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + rank.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + rank.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + rank.AccuracyRate;
            c.LifestealRate = c.LifestealRate + rank.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + rank.ShieldStrength;
            c.Tenacity = c.Tenacity + rank.Tenacity;
            c.ResistanceRate = c.ResistanceRate + rank.ResistanceRate;
            c.ComboRate = c.ComboRate + rank.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + rank.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + rank.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + rank.ComboResistanceRate;
            c.StunRate = c.StunRate + rank.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + rank.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + rank.ReflectionRate;
            c.IgnoreReflectionRate = c.IgnoreReflectionRate + rank.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + rank.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + rank.ReflectionResistanceRate;
            c.Mana = c.Mana + rank.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + rank.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + rank.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + rank.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + rank.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + rank.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + rank.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + rank.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + rank.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + rank.SkillResistanceRate;

            c.Power = PowerHelper.CalculatePower(
            c.Health,
            c.PhysicalAttack, c.PhysicalDefense,
            c.MagicalAttack, c.MagicalDefense,
            c.ChemicalAttack, c.ChemicalDefense,
            c.AtomicAttack, c.AtomicDefense,
            c.MentalAttack, c.MentalDefense,
            c.Speed,
            c.CriticalDamageRate, c.CriticalRate, c.CriticalResistanceRate, c.IgnoreCriticalRate,
            c.PenetrationRate, c.PenetrationResistanceRate, c.EvasionRate,
            c.DamageAbsorptionRate, c.IgnoreDamageAbsorptionRate, c.AbsorbedDamageRate,
            c.VitalityRegenerationRate, c.VitalityRegenerationResistanceRate,
            c.AccuracyRate, c.LifestealRate,
            c.ShieldStrength, c.Tenacity, c.ResistanceRate,
            c.ComboRate, c.IgnoreComboRate, c.ComboDamageRate, c.ComboResistanceRate,
            c.StunRate, c.IgnoreStunRate,
            c.ReflectionRate, c.IgnoreReflectionRate, c.ReflectionDamageRate, c.ReflectionResistanceRate,
            c.Mana, c.ManaRegenerationRate,
            c.DamageToDifferentFactionRate, c.ResistanceToDifferentFactionRate,
            c.DamageToSameFactionRate, c.ResistanceToSameFactionRate,
            c.NormalDamageRate, c.NormalResistanceRate,
            c.SkillDamageRate, c.SkillResistanceRate
        );
        }
        return CardHeroesList;
    }
    public async Task<List<CardHeroes>> GetAllMasterPowerAsync(string userId, List<CardHeroes> CardHeroesList)
    {
        foreach (var c in CardHeroesList)
        {
            Master master = await UserCardHeroesMasterService.Create().GetSumUserCardHeroesMasterAsync(userId, c.Id);
            c.Health = c.Health + master.Health + c.BaseStats.Health * master.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + master.PhysicalAttack + c.BaseStats.PhysicalAttack * master.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + master.PhysicalDefense + c.BaseStats.PhysicalDefense * master.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + master.MagicalAttack + c.BaseStats.MagicalAttack * master.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + master.MagicalDefense + c.BaseStats.MagicalDefense * master.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + master.ChemicalAttack + c.BaseStats.ChemicalAttack * master.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + master.ChemicalDefense + c.BaseStats.ChemicalDefense * master.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + master.AtomicAttack + c.BaseStats.AtomicAttack * master.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + master.AtomicDefense + c.BaseStats.AtomicDefense * master.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + master.MentalAttack + c.BaseStats.MentalAttack * master.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + master.MentalDefense + c.BaseStats.MentalDefense * master.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + master.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + master.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + master.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + master.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + master.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + master.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + master.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + master.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + master.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + master.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + master.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + master.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + master.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + master.AccuracyRate;
            c.LifestealRate = c.LifestealRate + master.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + master.ShieldStrength;
            c.Tenacity = c.Tenacity + master.Tenacity;
            c.ResistanceRate = c.ResistanceRate + master.ResistanceRate;
            c.ComboRate = c.ComboRate + master.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + master.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + master.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + master.ComboResistanceRate;
            c.StunRate = c.StunRate + master.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + master.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + master.ReflectionRate;
            c.IgnoreReflectionRate = c.IgnoreReflectionRate + master.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + master.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + master.ReflectionResistanceRate;
            c.Mana = c.Mana + master.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + master.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + master.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + master.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + master.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + master.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + master.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + master.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + master.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + master.SkillResistanceRate;

            c.Power = PowerHelper.CalculatePower(
            c.Health,
            c.PhysicalAttack, c.PhysicalDefense,
            c.MagicalAttack, c.MagicalDefense,
            c.ChemicalAttack, c.ChemicalDefense,
            c.AtomicAttack, c.AtomicDefense,
            c.MentalAttack, c.MentalDefense,
            c.Speed,
            c.CriticalDamageRate, c.CriticalRate, c.CriticalResistanceRate, c.IgnoreCriticalRate,
            c.PenetrationRate, c.PenetrationResistanceRate, c.EvasionRate,
            c.DamageAbsorptionRate, c.IgnoreDamageAbsorptionRate, c.AbsorbedDamageRate,
            c.VitalityRegenerationRate, c.VitalityRegenerationResistanceRate,
            c.AccuracyRate, c.LifestealRate,
            c.ShieldStrength, c.Tenacity, c.ResistanceRate,
            c.ComboRate, c.IgnoreComboRate, c.ComboDamageRate, c.ComboResistanceRate,
            c.StunRate, c.IgnoreStunRate,
            c.ReflectionRate, c.IgnoreReflectionRate, c.ReflectionDamageRate, c.ReflectionResistanceRate,
            c.Mana, c.ManaRegenerationRate,
            c.DamageToDifferentFactionRate, c.ResistanceToDifferentFactionRate,
            c.DamageToSameFactionRate, c.ResistanceToSameFactionRate,
            c.NormalDamageRate, c.NormalResistanceRate,
            c.SkillDamageRate, c.SkillResistanceRate
        );
        }
        return CardHeroesList;
    }
    public async Task<List<CardHeroes>> GetAllSpiritBeastPowerAsync(string userId, List<CardHeroes> cardHeroes)
    {
        foreach (var c in cardHeroes)
        {
            SpiritBeasts spiritBeast = await UserSpiritBeastsService.Create().GetUserCardHeroSpiritBeastAsync(userId, c);
            if (spiritBeast != null)
            {
                c.Health = c.Health + spiritBeast.Health + c.BaseStats.Health * spiritBeast.PercentAllHealth / 100;
                c.PhysicalAttack = c.PhysicalAttack + spiritBeast.PhysicalAttack + c.BaseStats.PhysicalAttack * spiritBeast.PercentAllPhysicalAttack / 100;
                c.PhysicalDefense = c.PhysicalDefense + spiritBeast.PhysicalDefense + c.BaseStats.PhysicalDefense * spiritBeast.PercentAllPhysicalDefense / 100;
                c.MagicalAttack = c.MagicalAttack + spiritBeast.MagicalAttack + c.BaseStats.MagicalAttack * spiritBeast.PercentAllMagicalAttack / 100;
                c.MagicalDefense = c.MagicalDefense + spiritBeast.MagicalDefense + c.BaseStats.MagicalDefense * spiritBeast.PercentAllMagicalDefense / 100;
                c.ChemicalAttack = c.ChemicalAttack + spiritBeast.ChemicalAttack + c.BaseStats.ChemicalAttack * spiritBeast.PercentAllChemicalAttack / 100;
                c.ChemicalDefense = c.ChemicalDefense + spiritBeast.ChemicalDefense + c.BaseStats.ChemicalDefense * spiritBeast.PercentAllChemicalDefense / 100;
                c.AtomicAttack = c.AtomicAttack + spiritBeast.AtomicAttack + c.BaseStats.AtomicAttack * spiritBeast.PercentAllAtomicAttack / 100;
                c.AtomicDefense = c.AtomicDefense + spiritBeast.AtomicDefense + c.BaseStats.AtomicDefense * spiritBeast.PercentAllAtomicDefense / 100;
                c.MentalAttack = c.MentalAttack + spiritBeast.MentalAttack + c.BaseStats.MentalAttack * spiritBeast.PercentAllMentalAttack / 100;
                c.MentalDefense = c.MentalDefense + spiritBeast.MentalDefense + c.BaseStats.MentalDefense * spiritBeast.PercentAllMentalDefense / 100;
                c.Speed = c.Speed + spiritBeast.Speed;
                c.CriticalDamageRate = c.CriticalDamageRate + spiritBeast.CriticalDamageRate;
                c.CriticalRate = c.CriticalRate + spiritBeast.CriticalRate;
                c.CriticalResistanceRate = c.CriticalResistanceRate + spiritBeast.CriticalResistanceRate;
                c.IgnoreCriticalRate = c.IgnoreCriticalRate + spiritBeast.IgnoreCriticalRate;
                c.PenetrationRate = c.PenetrationRate + spiritBeast.PenetrationRate;
                c.PenetrationResistanceRate = c.PenetrationResistanceRate + spiritBeast.PenetrationResistanceRate;
                c.EvasionRate = c.EvasionRate + spiritBeast.EvasionRate;
                c.DamageAbsorptionRate = c.DamageAbsorptionRate + spiritBeast.DamageAbsorptionRate;
                c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + spiritBeast.IgnoreDamageAbsorptionRate;
                c.AbsorbedDamageRate = c.AbsorbedDamageRate + spiritBeast.AbsorbedDamageRate;
                c.VitalityRegenerationRate = c.VitalityRegenerationRate + spiritBeast.VitalityRegenerationRate;
                c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + spiritBeast.VitalityRegenerationResistanceRate;
                c.AccuracyRate = c.AccuracyRate + spiritBeast.AccuracyRate;
                c.LifestealRate = c.LifestealRate + spiritBeast.LifestealRate;
                c.ShieldStrength = c.ShieldStrength + spiritBeast.ShieldStrength;
                c.Tenacity = c.Tenacity + spiritBeast.Tenacity;
                c.ResistanceRate = c.ResistanceRate + spiritBeast.ResistanceRate;
                c.ComboRate = c.ComboRate + spiritBeast.ComboRate;
                c.IgnoreComboRate = c.IgnoreComboRate + spiritBeast.IgnoreComboRate;
                c.ComboDamageRate = c.ComboDamageRate + spiritBeast.ComboDamageRate;
                c.ComboResistanceRate = c.ComboResistanceRate + spiritBeast.ComboResistanceRate;
                c.StunRate = c.StunRate + spiritBeast.StunRate;
                c.IgnoreStunRate = c.IgnoreStunRate + spiritBeast.IgnoreStunRate;
                c.ReflectionRate = c.ReflectionRate + spiritBeast.ReflectionRate;
                c.IgnoreReflectionRate = c.IgnoreReflectionRate + spiritBeast.IgnoreReflectionRate;
                c.ReflectionDamageRate = c.ReflectionDamageRate + spiritBeast.ReflectionDamageRate;
                c.ReflectionResistanceRate = c.ReflectionResistanceRate + spiritBeast.ReflectionResistanceRate;
                c.Mana = c.Mana + spiritBeast.Mana;
                c.ManaRegenerationRate = c.ManaRegenerationRate + spiritBeast.ManaRegenerationRate;
                c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + spiritBeast.DamageToDifferentFactionRate;
                c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + spiritBeast.ResistanceToDifferentFactionRate;
                c.DamageToSameFactionRate = c.DamageToSameFactionRate + spiritBeast.DamageToSameFactionRate;
                c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + spiritBeast.ResistanceToSameFactionRate;
                c.NormalDamageRate = c.NormalDamageRate + spiritBeast.NormalDamageRate;
                c.NormalResistanceRate = c.NormalResistanceRate + spiritBeast.NormalResistanceRate;
                c.SkillDamageRate = c.SkillDamageRate + spiritBeast.SkillDamageRate;
                c.SkillResistanceRate = c.SkillResistanceRate + spiritBeast.SkillResistanceRate;
            }

            c.Power = PowerHelper.CalculatePower(
            c.Health,
            c.PhysicalAttack, c.PhysicalDefense,
            c.MagicalAttack, c.MagicalDefense,
            c.ChemicalAttack, c.ChemicalDefense,
            c.AtomicAttack, c.AtomicDefense,
            c.MentalAttack, c.MentalDefense,
            c.Speed,
            c.CriticalDamageRate, c.CriticalRate, c.CriticalResistanceRate, c.IgnoreCriticalRate,
            c.PenetrationRate, c.PenetrationResistanceRate, c.EvasionRate,
            c.DamageAbsorptionRate, c.IgnoreDamageAbsorptionRate, c.AbsorbedDamageRate,
            c.VitalityRegenerationRate, c.VitalityRegenerationResistanceRate,
            c.AccuracyRate, c.LifestealRate,
            c.ShieldStrength, c.Tenacity, c.ResistanceRate,
            c.ComboRate, c.IgnoreComboRate, c.ComboDamageRate, c.ComboResistanceRate,
            c.StunRate, c.IgnoreStunRate,
            c.ReflectionRate, c.IgnoreReflectionRate, c.ReflectionDamageRate, c.ReflectionResistanceRate,
            c.Mana, c.ManaRegenerationRate,
            c.DamageToDifferentFactionRate, c.ResistanceToDifferentFactionRate,
            c.DamageToSameFactionRate, c.ResistanceToSameFactionRate,
            c.NormalDamageRate, c.NormalResistanceRate,
            c.SkillDamageRate, c.SkillResistanceRate
        );
        }
        return cardHeroes;
    }

    public async Task<List<CardHeroes>> GetUserCardHeroesAsync(string userId, string search, string type, int pageSize, int offset, string rare, UserStatsContextDTO sharedContext = null)
    {
        List<CardHeroes> list = await _userCardHeroesRepository.GetUserCardHeroesAsync(userId, search, type, pageSize, offset, rare);

        List<string> cardHeroIds = list.Select(hero => hero.Id).ToList();

        // var skillsTask = _userSkillsRepository.GetUserCardHeroesSkillsAsync(userId, cardHeroIds);

        // var skillData = await skillsTask;
        // foreach (var skill in skillData)
        // {
        //     if (skill.Pattern != null && !string.IsNullOrEmpty(skill.Pattern.Id))
        //     {
        //         skill.Pattern = _patternsService.GetPatternFromCache(skill.Pattern.Id);
        //     }
        // }

        UserStatsContextDTO context = sharedContext;
        if (context == null)
        {
            context = await _userStatsService.GetUserStatsContextAsync(userId);
        }

        // var skillsLookup = skillData.ToLookup(s => s.CardId);

        TotalBuffs totalBuffs = new TotalBuffs();
        totalBuffs.AddBuff(context.PowerManagerData);
        totalBuffs.AddBuff(context.ScienceFictionData);
        totalBuffs.AddBuff(context.ResearchData);
        totalBuffs.AddBuff(context.ArchiveData);
        totalBuffs.AddBuff(context.UniverseData);
        totalBuffs.AddBuff(context.HiinData);
        totalBuffs.AddBuff(context.SswnData);
        totalBuffs.AddBuff(context.HitnData);
        totalBuffs.AddBuff(context.HihnData);
        totalBuffs.AddBuff(context.HienData);
        totalBuffs.AddBuff(context.HicaData);
        totalBuffs.AddBuff(context.HirnData);
        totalBuffs.AddBuff(context.HidcData);
        totalBuffs.AddBuff(context.HicbData);
        totalBuffs.AddBuff(context.HisnData);
        totalBuffs.AddBuff(context.AnimeStatsData);

        list = QualityEvaluatorHelper.GetQualityPower(list);

        foreach (var card in list)
        {
            if (card == null) continue; // Phòng hờ phần tử trong list bị null

            // Áp dụng tổng buff (Flat + % Base stats)
            card.ApplyTotalBuffs(totalBuffs);

            // Gán Skills an toàn, tránh tạo List thừa
            // card.Skills = skillsLookup.Contains(card.Id)
            //     ? skillsLookup[card.Id].ToList()
            //     : new List<Skills>();

            // Tính toán lại tổng lực chiến (Sau khi đã có đầy đủ chỉ số và Skills)
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<CardHeroes>> GetUserCardHeroesTeamAsync(string userId, string teamId, string position, UserStatsContextDTO sharedContext = null)
    {
        List<CardHeroes> list = await _userCardHeroesRepository.GetUserCardHeroesTeamAsync(userId, teamId, position);

        List<string> cardHeroIds = list.Select(hero => hero.Id).ToList();

        // var skillsTask = _userSkillsRepository.GetUserCardHeroesSkillsAsync(userId, cardHeroIds);

        // var skillData = await skillsTask;
        // foreach (var skill in skillData)
        // {
        //     if (skill.Pattern != null && !string.IsNullOrEmpty(skill.Pattern.Id))
        //     {
        //         skill.Pattern = _patternsService.GetPatternFromCache(skill.Pattern.Id);
        //     }
        // }

        UserStatsContextDTO context = sharedContext;
        if (context == null)
        {
            context = await _userStatsService.GetUserStatsContextAsync(userId);
        }

        // var skillsLookup = skillData.ToLookup(s => s.CardId);

        TotalBuffs totalBuffs = new TotalBuffs();
        totalBuffs.AddBuff(context.PowerManagerData);
        totalBuffs.AddBuff(context.ScienceFictionData);
        totalBuffs.AddBuff(context.ResearchData);
        totalBuffs.AddBuff(context.ArchiveData);
        totalBuffs.AddBuff(context.UniverseData);
        totalBuffs.AddBuff(context.HiinData);
        totalBuffs.AddBuff(context.SswnData);
        totalBuffs.AddBuff(context.HitnData);
        totalBuffs.AddBuff(context.HihnData);
        totalBuffs.AddBuff(context.HienData);
        totalBuffs.AddBuff(context.HicaData);
        totalBuffs.AddBuff(context.HirnData);
        totalBuffs.AddBuff(context.HidcData);
        totalBuffs.AddBuff(context.HicbData);
        totalBuffs.AddBuff(context.HisnData);
        totalBuffs.AddBuff(context.AnimeStatsData);

        list = QualityEvaluatorHelper.GetQualityPower(list);

        foreach (var card in list)
        {
            if (card == null) continue; // Phòng hờ phần tử trong list bị null

            // Áp dụng tổng buff (Flat + % Base stats)
            card.ApplyTotalBuffs(totalBuffs);

            // Gán Skills an toàn, tránh tạo List thừa
            // card.Skills = skillsLookup.Contains(card.Id)
            //     ? skillsLookup[card.Id].ToList()
            //     : new List<Skills>();

            // Tính toán lại tổng lực chiến (Sau khi đã có đầy đủ chỉ số và Skills)
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<CardHeroes>> GetUserCardHeroesTeamWithoutPositionAsync(string userId, string teamId, UserStatsContextDTO sharedContext = null)
    {
        List<CardHeroes> list = await _userCardHeroesRepository.GetUserCardHeroesTeamWithoutPositionAsync(userId, teamId);

        List<string> cardHeroIds = list.Select(hero => hero.Id).ToList();

        // var skillsTask = _userSkillsRepository.GetUserCardHeroesSkillsAsync(userId, cardHeroIds);

        // var skillData = await skillsTask;
        // foreach (var skill in skillData)
        // {
        //     if (skill.Pattern != null && !string.IsNullOrEmpty(skill.Pattern.Id))
        //     {
        //         skill.Pattern = _patternsService.GetPatternFromCache(skill.Pattern.Id);
        //     }
        // }

        UserStatsContextDTO context = sharedContext;
        if (context == null)
        {
            context = await _userStatsService.GetUserStatsContextAsync(userId);
        }

        // var skillsLookup = skillData.ToLookup(s => s.CardId);

        TotalBuffs totalBuffs = new TotalBuffs();
        totalBuffs.AddBuff(context.PowerManagerData);
        totalBuffs.AddBuff(context.ScienceFictionData);
        totalBuffs.AddBuff(context.ResearchData);
        totalBuffs.AddBuff(context.ArchiveData);
        totalBuffs.AddBuff(context.UniverseData);
        totalBuffs.AddBuff(context.HiinData);
        totalBuffs.AddBuff(context.SswnData);
        totalBuffs.AddBuff(context.HitnData);
        totalBuffs.AddBuff(context.HihnData);
        totalBuffs.AddBuff(context.HienData);
        totalBuffs.AddBuff(context.HicaData);
        totalBuffs.AddBuff(context.HirnData);
        totalBuffs.AddBuff(context.HidcData);
        totalBuffs.AddBuff(context.HicbData);
        totalBuffs.AddBuff(context.HisnData);
        totalBuffs.AddBuff(context.AnimeStatsData);

        list = QualityEvaluatorHelper.GetQualityPower(list);

        foreach (var card in list)
        {
            if (card == null) continue; // Phòng hờ phần tử trong list bị null

            // Áp dụng tổng buff (Flat + % Base stats)
            card.ApplyTotalBuffs(totalBuffs);

            // Gán Skills an toàn, tránh tạo List thừa
            // card.Skills = skillsLookup.Contains(card.Id)
            //     ? skillsLookup[card.Id].ToList()
            //     : new List<Skills>();

            // Tính toán lại tổng lực chiến (Sau khi đã có đầy đủ chỉ số và Skills)
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    // public async Task<List<CardHeroes>> GetUserCardHeroesTeamWithoutPositionAsync(string userId, string teamId, UserStatsContextDTO sharedContext = null)
    // {
    //     // 1. Đo thời gian lấy danh sách Hero gốc từ DB
    //     var swRepo = Stopwatch.StartNew();
    //     List<CardHeroes> list = await _userCardHeroesRepository.GetUserCardHeroesTeamWithoutPositionAsync(userId, teamId);
    //     swRepo.Stop();
    //     UnityEngine.Debug.Log($"[Timer] _userCardHeroesRepository: {swRepo.ElapsedMilliseconds} ms");

    //     List<string> cardHeroIds = list.Select(hero => hero.Id).ToList();

    //     // Khởi tạo các Stopwatch riêng biệt cho từng Task bất đồng bộ
    //     var swSkills = new Stopwatch();

    //     // Hàm local hỗ trợ đo đạc và tự động bắn log lên Unity Console
    //     async Task<T> MeasureTask<T>(Func<Task<T>> taskFunc, Stopwatch sw, string taskName)
    //     {
    //         sw.Start();
    //         try
    //         {
    //             return await taskFunc();
    //         }
    //         finally
    //         {
    //             sw.Stop();
    //             UnityEngine.Debug.Log($"[Timer] {taskName}: {sw.ElapsedMilliseconds} ms");
    //         }
    //     }

    //     // Lấy danh sách kỹ năng dựa theo Id Hero
    //     var skillsTask = MeasureTask(() => _userSkillsRepository.GetUserCardHeroesSkillsAsync(userId, cardHeroIds), swSkills, "SkillsFetch");

    //     // Đợi tất cả hoàn thành cùng nhau
    //     var swTotalParallel = Stopwatch.StartNew();
    //     await Task.WhenAll(skillsTask);
    //     swTotalParallel.Stop();
    //     UnityEngine.Debug.LogWarning($"--- [Timer] Tổng thời gian chờ xử lý song song (WhenAll): {swTotalParallel.ElapsedMilliseconds} ms ---");

    //     // Lấy kết quả trả về từ các Task
    //     var skillData = await skillsTask;

    //     // 2. Đo thời gian xử lý Cache dữ liệu Patterns
    //     var swCache = Stopwatch.StartNew();
    //     foreach (var skill in skillData)
    //     {
    //         if (skill.Pattern != null && !string.IsNullOrEmpty(skill.Pattern.Id))
    //         {
    //             skill.Pattern = _patternsService.GetPatternFromCache(skill.Pattern.Id);
    //         }
    //     }

    //     UserStatsContextDTO context = sharedContext;
    //     if (context == null)
    //     {
    //         context = await _userStatsService.GetUserStatsContextAsync(userId);
    //     }

    //     swCache.Stop();
    //     UnityEngine.Debug.Log($"[Timer] Pattern Cache Mapping: {swCache.ElapsedMilliseconds} ms");

    //     // 3. Đo thời gian xử lý logic tính toán Chỉ số sức mạnh & Sắp xếp
    //     var swCalculations = Stopwatch.StartNew();
    //     list = QualityEvaluatorHelper.GetQualityPower(list);

    //     foreach (var card in list)
    //     {
    //         card.ApplyPowerStats(context.PowerManagerData);
    //         card.ApplyScienceFictionStats(context.ScienceFictionData);
    //         card.ApplyResearchStats(context.ResearchData);
    //         card.ApplyArchiveStats(context.ArchiveData);
    //         card.ApplyUniverseStats(context.UniverseData);
    //         card.ApplyHIINStats(context.HiinData);
    //         card.ApplySSWNStats(context.SswnData);
    //         card.ApplyHITNStats(context.HitnData);
    //         card.ApplyHIHNStats(context.HihnData);
    //         card.ApplyHIENStats(context.HienData);
    //         card.ApplyHICAStats(context.HicaData);
    //         card.ApplyHIRNStats(context.HirnData);
    //         card.ApplyHIDCStats(context.HidcData);
    //         card.ApplyHICBStats(context.HicbData);
    //         card.ApplyHISNStats(context.HisnData);
    //         card.ApplyAllUserAnimes(context.AnimeStatsData);
    //         card.Skills = skillData.Where(s => s.CardId == card.Id).ToList();
    //         card.RecalculatePower();
    //     }
    //     ListSortHelper.SortByPower(list);
    //     swCalculations.Stop();

    //     UnityEngine.Debug.Log($"[Timer] Calculate Power & Sort (CPU): {swCalculations.ElapsedMilliseconds} ms");

    //     return list;
    // }

    public async Task<Dictionary<string, int>> GetUniqueUserCardHeroesTypesTeamAsync(string userId, string teamId)
    {
        return await _userCardHeroesRepository.GetUniqueUserCardHeroesTypesTeamAsync(userId, teamId);
    }

    public async Task<int> GetUserCardHeroesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userCardHeroesRepository.GetUserCardHeroesCountAsync(userId, search, type, rare);
    }

    public async Task<int> GetUserCardHeroesTeamsPositionCountAsync(string userId, string teamId, string position)
    {
        return await _userCardHeroesRepository.GetUserCardHeroesTeamsPositionCountAsync(userId, teamId, position);
    }

    public async Task<int> GetUserCardHeroesTeamsCountAsync(string userId, string teamId)
    {
        return await _userCardHeroesRepository.GetUserCardHeroesTeamsCountAsync(userId, teamId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardHeroAsync(string userId, CardHeroes cardHero)
    {
        var insertOrUpdateResult = await _userCardHeroesRepository.InsertOrUpdateUserCardHeroAsync(userId, cardHero);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _cardHeroesGalleryService.InsertCardHeroGalleryAsync(userId, cardHero.Id);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardHeroesBatchAsync(string userId, List<CardHeroes> cardHeroes)
    {
        var repositoryResult = await _userCardHeroesRepository.InsertOrUpdateUserCardHeroesBatchAsync(userId, cardHeroes);

        // 1. Kiểm tra Null hoặc nếu Repository trả về không thành công
        if (repositoryResult?.Data == null || !repositoryResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        // 2. Gộp logic xử lý Gallery nếu có thẻ mới được Insert (dùng cho cả Inserted và Mixed)
        var newlyInsertedCards = repositoryResult.Data.InsertedItems;
        if (newlyInsertedCards != null && newlyInsertedCards.Count > 0)
        {
            await _cardHeroesGalleryService.InsertBatchCardHeroesGalleryAsync(userId, newlyInsertedCards);
        }

        // 3. Mapping kết quả OperationType trả về gọn gàng
        return repositoryResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserCardHeroLevelAsync(string userId, CardHeroes cardHero)
    {
        var updateResult = await _userCardHeroesRepository.UpdateUserCardHeroLevelAsync(userId, cardHero);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserCardHeroStarAsync(string userId, CardHeroes cardHero)
    {
        var updateResult = await _userCardHeroesRepository.UpdateUserCardHeroStarAsync(userId, cardHero);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _cardHeroesGalleryService.UpdateTempStarCardHeroGalleryAsync(userId, cardHero.Id, cardHero.Star);

        return true;
    }

    public async Task<bool> UpdateTeamUserCardHeroAsync(string userId, string teamId, string position, string cardId)
    {
        return await _userCardHeroesRepository.UpdateTeamUserCardHeroAsync(userId, teamId, position, cardId);
    }

    public async Task<CardHeroes> GetUserCardHeroByIdAsync(string userId, string Id, UserStatsContextDTO sharedContext = null)
    {
        CardHeroes cardHero = await _userCardHeroesRepository.GetUserCardHeroByIdAsync(userId, Id);
        if (cardHero == null) return null;

        // Bọc vào list để tái sử dụng logic
        List<CardHeroes> list = new List<CardHeroes> { cardHero };

        List<string> cardHeroIds = list.Select(hero => hero.Id).ToList();

        var skillsTask = _userSkillsRepository.GetUserCardHeroesSkillsAsync(userId, cardHeroIds);

        var skillData = await skillsTask;
        foreach (var skill in skillData)
        {
            if (skill.Pattern != null && !string.IsNullOrEmpty(skill.Pattern.Id))
            {
                skill.Pattern = _patternsService.GetPatternFromCache(skill.Pattern.Id);
            }
        }

        UserStatsContextDTO context = sharedContext;
        if (context == null)
        {
            context = await _userStatsService.GetUserStatsContextAsync(userId);
        }

        var skillsLookup = skillData.ToLookup(s => s.CardId);

        TotalBuffs totalBuffs = new TotalBuffs();
        totalBuffs.AddBuff(context.PowerManagerData);
        totalBuffs.AddBuff(context.ScienceFictionData);
        totalBuffs.AddBuff(context.ResearchData);
        totalBuffs.AddBuff(context.ArchiveData);
        totalBuffs.AddBuff(context.UniverseData);
        totalBuffs.AddBuff(context.HiinData);
        totalBuffs.AddBuff(context.SswnData);
        totalBuffs.AddBuff(context.HitnData);
        totalBuffs.AddBuff(context.HihnData);
        totalBuffs.AddBuff(context.HienData);
        totalBuffs.AddBuff(context.HicaData);
        totalBuffs.AddBuff(context.HirnData);
        totalBuffs.AddBuff(context.HidcData);
        totalBuffs.AddBuff(context.HicbData);
        totalBuffs.AddBuff(context.HisnData);
        totalBuffs.AddBuff(context.AnimeStatsData);

        list = QualityEvaluatorHelper.GetQualityPower(list);

        foreach (var card in list)
        {
            if (card == null) continue; // Phòng hờ phần tử trong list bị null

            // Áp dụng tổng buff (Flat + % Base stats)
            card.ApplyTotalBuffs(totalBuffs);

            // Gán Skills an toàn, tránh tạo List thừa
            card.Skills = skillsLookup.Contains(card.Id)
                ? skillsLookup[card.Id].ToList()
                : new List<Skills>();

            // Tính toán lại tổng lực chiến (Sau khi đã có đầy đủ chỉ số và Skills)
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list.FirstOrDefault();
    }

    public async Task<BaseStats> GetTeamTotalStatsAsync(string userId, UserStatsContextDTO sharedContext = null)
    {
        var totalStats = await _userCardHeroesRepository.GetTeamTotalStatsAsync(userId);
        var baseStats = await _userCardHeroesRepository.GetTeamTotalStatsWithoutQualityAsync(userId);

        UserStatsContextDTO context = sharedContext;
        if (context == null)
        {
            context = await _userStatsService.GetUserStatsContextAsync(userId);
        }

        TotalBuffs totalBuffs = new TotalBuffs();
        totalBuffs.AddBuff(context.PowerManagerData);
        totalBuffs.AddBuff(context.ScienceFictionData);
        totalBuffs.AddBuff(context.ResearchData);
        totalBuffs.AddBuff(context.ArchiveData);
        totalBuffs.AddBuff(context.UniverseData);
        totalBuffs.AddBuff(context.HiinData);
        totalBuffs.AddBuff(context.SswnData);
        totalBuffs.AddBuff(context.HitnData);
        totalBuffs.AddBuff(context.HihnData);
        totalBuffs.AddBuff(context.HienData);
        totalBuffs.AddBuff(context.HicaData);
        totalBuffs.AddBuff(context.HirnData);
        totalBuffs.AddBuff(context.HidcData);
        totalBuffs.AddBuff(context.HicbData);
        totalBuffs.AddBuff(context.HisnData);
        totalBuffs.AddBuff(context.AnimeStatsData);

        totalStats.ApplyTotalBuffs(baseStats, totalBuffs);
        totalStats.RecalculatePower();

        return totalStats;
    }

}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserCardHeroesService : IUserCardHeroesService
{
     private static UserCardHeroesService _instance;
    private readonly IUserCardHeroesRepository _userCardHeroesRepository;

    public UserCardHeroesService(IUserCardHeroesRepository userCardHeroesRepository)
    {
        _userCardHeroesRepository = userCardHeroesRepository;
    }

    public static UserCardHeroesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardHeroesService(new UserCardHeroesRepository());
        }
        return _instance;
    }

    public async Task<List<CardHeroes>> GetFinalPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        PowerManager powerManager = await PowerManagerService.Create().GetUserStatsAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + powerManager.Health + c.BaseStats.Health * powerManager.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + powerManager.PhysicalAttack + c.BaseStats.PhysicalAttack * powerManager.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + powerManager.PhysicalDefense + c.BaseStats.PhysicalDefense * powerManager.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + powerManager.MagicalAttack + c.BaseStats.MagicalAttack * powerManager.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + powerManager.MagicalDefense + c.BaseStats.MagicalDefense * powerManager.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + powerManager.ChemicalAttack + c.BaseStats.ChemicalAttack * powerManager.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + powerManager.ChemicalDefense + c.BaseStats.ChemicalDefense * powerManager.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + powerManager.AtomicAttack + c.BaseStats.AtomicAttack * powerManager.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + powerManager.AtomicDefense + c.BaseStats.AtomicDefense * powerManager.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + powerManager.MentalAttack + c.BaseStats.MentalAttack * powerManager.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + powerManager.MentalDefense + c.BaseStats.MentalDefense * powerManager.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + powerManager.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + powerManager.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + powerManager.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + powerManager.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + powerManager.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + powerManager.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + powerManager.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + powerManager.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + powerManager.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + powerManager.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + powerManager.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + powerManager.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + powerManager.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + powerManager.AccuracyRate;
            c.LifestealRate = c.LifestealRate + powerManager.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + powerManager.ShieldStrength;
            c.Tenacity = c.Tenacity + powerManager.Tenacity;
            c.ResistanceRate = c.ResistanceRate + powerManager.ResistanceRate;
            c.ComboRate = c.ComboRate + powerManager.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + powerManager.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + powerManager.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + powerManager.ComboResistanceRate;
            c.StunRate = c.StunRate + powerManager.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + powerManager.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + powerManager.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + powerManager.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + powerManager.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + powerManager.ReflectionResistanceRate;
            c.Mana = c.Mana + powerManager.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + powerManager.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + powerManager.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + powerManager.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + powerManager.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + powerManager.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + powerManager.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + powerManager.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + powerManager.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + powerManager.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetScienceFictionPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        ScienceFiction scienceFiction = await ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + scienceFiction.Health + c.BaseStats.Health * scienceFiction.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + scienceFiction.PhysicalAttack + c.BaseStats.PhysicalAttack * scienceFiction.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + scienceFiction.PhysicalDefense + c.BaseStats.PhysicalDefense * scienceFiction.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + scienceFiction.MagicalAttack + c.BaseStats.MagicalAttack * scienceFiction.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + scienceFiction.MagicalDefense + c.BaseStats.MagicalDefense * scienceFiction.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + scienceFiction.ChemicalAttack + c.BaseStats.ChemicalAttack * scienceFiction.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + scienceFiction.ChemicalDefense + c.BaseStats.ChemicalDefense * scienceFiction.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + scienceFiction.AtomicAttack + c.BaseStats.AtomicAttack * scienceFiction.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + scienceFiction.AtomicDefense + c.BaseStats.AtomicDefense * scienceFiction.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + scienceFiction.MentalAttack + c.BaseStats.MentalAttack * scienceFiction.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + scienceFiction.MentalDefense + c.BaseStats.MentalDefense * scienceFiction.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + scienceFiction.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + scienceFiction.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + scienceFiction.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + scienceFiction.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + scienceFiction.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + scienceFiction.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + scienceFiction.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + scienceFiction.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + scienceFiction.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + scienceFiction.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + scienceFiction.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + scienceFiction.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + scienceFiction.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + scienceFiction.AccuracyRate;
            c.LifestealRate = c.LifestealRate + scienceFiction.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + scienceFiction.ShieldStrength;
            c.Tenacity = c.Tenacity + scienceFiction.Tenacity;
            c.ResistanceRate = c.ResistanceRate + scienceFiction.ResistanceRate;
            c.ComboRate = c.ComboRate + scienceFiction.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + scienceFiction.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + scienceFiction.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + scienceFiction.ComboResistanceRate;
            c.StunRate = c.StunRate + scienceFiction.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + scienceFiction.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + scienceFiction.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + scienceFiction.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + scienceFiction.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + scienceFiction.ReflectionResistanceRate;
            c.Mana = c.Mana + scienceFiction.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + scienceFiction.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + scienceFiction.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + scienceFiction.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + scienceFiction.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + scienceFiction.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + scienceFiction.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + scienceFiction.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + scienceFiction.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + scienceFiction.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetResearchPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        Researchs research = await ResearchsService.Create().GetSumResearchsAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + research.Health + c.BaseStats.Health * research.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + research.PhysicalAttack + c.BaseStats.PhysicalAttack * research.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + research.PhysicalDefense + c.BaseStats.PhysicalDefense * research.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + research.MagicalAttack + c.BaseStats.MagicalAttack * research.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + research.MagicalDefense + c.BaseStats.MagicalDefense * research.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + research.ChemicalAttack + c.BaseStats.ChemicalAttack * research.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + research.ChemicalDefense + c.BaseStats.ChemicalDefense * research.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + research.AtomicAttack + c.BaseStats.AtomicAttack * research.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + research.AtomicDefense + c.BaseStats.AtomicDefense * research.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + research.MentalAttack + c.BaseStats.MentalAttack * research.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + research.MentalDefense + c.BaseStats.MentalDefense * research.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + research.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + research.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + research.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + research.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + research.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + research.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + research.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + research.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + research.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + research.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + research.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + research.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + research.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + research.AccuracyRate;
            c.LifestealRate = c.LifestealRate + research.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + research.ShieldStrength;
            c.Tenacity = c.Tenacity + research.Tenacity;
            c.ResistanceRate = c.ResistanceRate + research.ResistanceRate;
            c.ComboRate = c.ComboRate + research.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + research.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + research.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + research.ComboResistanceRate;
            c.StunRate = c.StunRate + research.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + research.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + research.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + research.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + research.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + research.ReflectionResistanceRate;
            c.Mana = c.Mana + research.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + research.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + research.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + research.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + research.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + research.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + research.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + research.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + research.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + research.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetArchivePowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        Archives archive = await ArchivesService.Create().GetSumArchivesAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + archive.Health + c.BaseStats.Health * archive.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + archive.PhysicalAttack + c.BaseStats.PhysicalAttack * archive.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + archive.PhysicalDefense + c.BaseStats.PhysicalDefense * archive.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + archive.MagicalAttack + c.BaseStats.MagicalAttack * archive.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + archive.MagicalDefense + c.BaseStats.MagicalDefense * archive.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + archive.ChemicalAttack + c.BaseStats.ChemicalAttack * archive.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + archive.ChemicalDefense + c.BaseStats.ChemicalDefense * archive.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + archive.AtomicAttack + c.BaseStats.AtomicAttack * archive.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + archive.AtomicDefense + c.BaseStats.AtomicDefense * archive.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + archive.MentalAttack + c.BaseStats.MentalAttack * archive.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + archive.MentalDefense + c.BaseStats.MentalDefense * archive.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + archive.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + archive.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + archive.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + archive.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + archive.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + archive.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + archive.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + archive.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + archive.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + archive.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + archive.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + archive.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + archive.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + archive.AccuracyRate;
            c.LifestealRate = c.LifestealRate + archive.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + archive.ShieldStrength;
            c.Tenacity = c.Tenacity + archive.Tenacity;
            c.ResistanceRate = c.ResistanceRate + archive.ResistanceRate;
            c.ComboRate = c.ComboRate + archive.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + archive.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + archive.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + archive.ComboResistanceRate;
            c.StunRate = c.StunRate + archive.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + archive.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + archive.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + archive.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + archive.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + archive.ReflectionResistanceRate;
            c.Mana = c.Mana + archive.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + archive.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + archive.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + archive.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + archive.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + archive.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + archive.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + archive.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + archive.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + archive.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetUniversePowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        Universes universe = await UniversesService.Create().GetSumUniversesAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + universe.Health + c.BaseStats.Health * universe.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + universe.PhysicalAttack + c.BaseStats.PhysicalAttack * universe.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + universe.PhysicalDefense + c.BaseStats.PhysicalDefense * universe.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + universe.MagicalAttack + c.BaseStats.MagicalAttack * universe.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + universe.MagicalDefense + c.BaseStats.MagicalDefense * universe.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + universe.ChemicalAttack + c.BaseStats.ChemicalAttack * universe.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + universe.ChemicalDefense + c.BaseStats.ChemicalDefense * universe.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + universe.AtomicAttack + c.BaseStats.AtomicAttack * universe.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + universe.AtomicDefense + c.BaseStats.AtomicDefense * universe.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + universe.MentalAttack + c.BaseStats.MentalAttack * universe.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + universe.MentalDefense + c.BaseStats.MentalDefense * universe.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + universe.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + universe.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + universe.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + universe.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + universe.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + universe.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + universe.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + universe.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + universe.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + universe.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + universe.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + universe.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + universe.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + universe.AccuracyRate;
            c.LifestealRate = c.LifestealRate + universe.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + universe.ShieldStrength;
            c.Tenacity = c.Tenacity + universe.Tenacity;
            c.ResistanceRate = c.ResistanceRate + universe.ResistanceRate;
            c.ComboRate = c.ComboRate + universe.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + universe.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + universe.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + universe.ComboResistanceRate;
            c.StunRate = c.StunRate + universe.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + universe.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + universe.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + universe.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + universe.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + universe.ReflectionResistanceRate;
            c.Mana = c.Mana + universe.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + universe.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + universe.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + universe.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + universe.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + universe.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + universe.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + universe.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + universe.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + universe.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetHIINPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        HIINs hiin = await HIINsService.Create().GetSumHIINsAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + hiin.Health + c.BaseStats.Health * hiin.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + hiin.PhysicalAttack + c.BaseStats.PhysicalAttack * hiin.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + hiin.PhysicalDefense + c.BaseStats.PhysicalDefense * hiin.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + hiin.MagicalAttack + c.BaseStats.MagicalAttack * hiin.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + hiin.MagicalDefense + c.BaseStats.MagicalDefense * hiin.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + hiin.ChemicalAttack + c.BaseStats.ChemicalAttack * hiin.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + hiin.ChemicalDefense + c.BaseStats.ChemicalDefense * hiin.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + hiin.AtomicAttack + c.BaseStats.AtomicAttack * hiin.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + hiin.AtomicDefense + c.BaseStats.AtomicDefense * hiin.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + hiin.MentalAttack + c.BaseStats.MentalAttack * hiin.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + hiin.MentalDefense + c.BaseStats.MentalDefense * hiin.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + hiin.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + hiin.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + hiin.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + hiin.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + hiin.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + hiin.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + hiin.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + hiin.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + hiin.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + hiin.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + hiin.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + hiin.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + hiin.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + hiin.AccuracyRate;
            c.LifestealRate = c.LifestealRate + hiin.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + hiin.ShieldStrength;
            c.Tenacity = c.Tenacity + hiin.Tenacity;
            c.ResistanceRate = c.ResistanceRate + hiin.ResistanceRate;
            c.ComboRate = c.ComboRate + hiin.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + hiin.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + hiin.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + hiin.ComboResistanceRate;
            c.StunRate = c.StunRate + hiin.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + hiin.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + hiin.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + hiin.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + hiin.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + hiin.ReflectionResistanceRate;
            c.Mana = c.Mana + hiin.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + hiin.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + hiin.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + hiin.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + hiin.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + hiin.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + hiin.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + hiin.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + hiin.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + hiin.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetSSWNPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        SSWNs sswn = await SSWNsService.Create().GetSumSSWNsAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + sswn.Health + c.BaseStats.Health * sswn.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + sswn.PhysicalAttack + c.BaseStats.PhysicalAttack * sswn.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + sswn.PhysicalDefense + c.BaseStats.PhysicalDefense * sswn.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + sswn.MagicalAttack + c.BaseStats.MagicalAttack * sswn.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + sswn.MagicalDefense + c.BaseStats.MagicalDefense * sswn.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + sswn.ChemicalAttack + c.BaseStats.ChemicalAttack * sswn.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + sswn.ChemicalDefense + c.BaseStats.ChemicalDefense * sswn.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + sswn.AtomicAttack + c.BaseStats.AtomicAttack * sswn.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + sswn.AtomicDefense + c.BaseStats.AtomicDefense * sswn.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + sswn.MentalAttack + c.BaseStats.MentalAttack * sswn.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + sswn.MentalDefense + c.BaseStats.MentalDefense * sswn.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + sswn.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + sswn.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + sswn.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + sswn.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + sswn.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + sswn.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + sswn.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + sswn.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + sswn.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + sswn.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + sswn.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + sswn.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + sswn.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + sswn.AccuracyRate;
            c.LifestealRate = c.LifestealRate + sswn.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + sswn.ShieldStrength;
            c.Tenacity = c.Tenacity + sswn.Tenacity;
            c.ResistanceRate = c.ResistanceRate + sswn.ResistanceRate;
            c.ComboRate = c.ComboRate + sswn.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + sswn.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + sswn.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + sswn.ComboResistanceRate;
            c.StunRate = c.StunRate + sswn.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + sswn.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + sswn.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + sswn.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + sswn.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + sswn.ReflectionResistanceRate;
            c.Mana = c.Mana + sswn.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + sswn.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + sswn.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + sswn.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + sswn.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + sswn.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + sswn.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + sswn.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + sswn.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + sswn.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetHITNPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        HITNs hitn = await HITNsService.Create().GetSumHITNsAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + hitn.Health + c.BaseStats.Health * hitn.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + hitn.PhysicalAttack + c.BaseStats.PhysicalAttack * hitn.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + hitn.PhysicalDefense + c.BaseStats.PhysicalDefense * hitn.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + hitn.MagicalAttack + c.BaseStats.MagicalAttack * hitn.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + hitn.MagicalDefense + c.BaseStats.MagicalDefense * hitn.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + hitn.ChemicalAttack + c.BaseStats.ChemicalAttack * hitn.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + hitn.ChemicalDefense + c.BaseStats.ChemicalDefense * hitn.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + hitn.AtomicAttack + c.BaseStats.AtomicAttack * hitn.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + hitn.AtomicDefense + c.BaseStats.AtomicDefense * hitn.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + hitn.MentalAttack + c.BaseStats.MentalAttack * hitn.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + hitn.MentalDefense + c.BaseStats.MentalDefense * hitn.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + hitn.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + hitn.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + hitn.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + hitn.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + hitn.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + hitn.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + hitn.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + hitn.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + hitn.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + hitn.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + hitn.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + hitn.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + hitn.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + hitn.AccuracyRate;
            c.LifestealRate = c.LifestealRate + hitn.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + hitn.ShieldStrength;
            c.Tenacity = c.Tenacity + hitn.Tenacity;
            c.ResistanceRate = c.ResistanceRate + hitn.ResistanceRate;
            c.ComboRate = c.ComboRate + hitn.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + hitn.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + hitn.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + hitn.ComboResistanceRate;
            c.StunRate = c.StunRate + hitn.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + hitn.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + hitn.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + hitn.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + hitn.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + hitn.ReflectionResistanceRate;
            c.Mana = c.Mana + hitn.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + hitn.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + hitn.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + hitn.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + hitn.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + hitn.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + hitn.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + hitn.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + hitn.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + hitn.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetHIHNPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        HIHNs hihn = await HIHNsService.Create().GetSumHIHNsAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + hihn.Health + c.BaseStats.Health * hihn.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + hihn.PhysicalAttack + c.BaseStats.PhysicalAttack * hihn.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + hihn.PhysicalDefense + c.BaseStats.PhysicalDefense * hihn.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + hihn.MagicalAttack + c.BaseStats.MagicalAttack * hihn.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + hihn.MagicalDefense + c.BaseStats.MagicalDefense * hihn.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + hihn.ChemicalAttack + c.BaseStats.ChemicalAttack * hihn.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + hihn.ChemicalDefense + c.BaseStats.ChemicalDefense * hihn.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + hihn.AtomicAttack + c.BaseStats.AtomicAttack * hihn.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + hihn.AtomicDefense + c.BaseStats.AtomicDefense * hihn.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + hihn.MentalAttack + c.BaseStats.MentalAttack * hihn.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + hihn.MentalDefense + c.BaseStats.MentalDefense * hihn.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + hihn.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + hihn.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + hihn.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + hihn.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + hihn.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + hihn.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + hihn.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + hihn.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + hihn.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + hihn.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + hihn.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + hihn.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + hihn.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + hihn.AccuracyRate;
            c.LifestealRate = c.LifestealRate + hihn.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + hihn.ShieldStrength;
            c.Tenacity = c.Tenacity + hihn.Tenacity;
            c.ResistanceRate = c.ResistanceRate + hihn.ResistanceRate;
            c.ComboRate = c.ComboRate + hihn.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + hihn.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + hihn.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + hihn.ComboResistanceRate;
            c.StunRate = c.StunRate + hihn.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + hihn.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + hihn.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + hihn.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + hihn.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + hihn.ReflectionResistanceRate;
            c.Mana = c.Mana + hihn.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + hihn.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + hihn.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + hihn.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + hihn.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + hihn.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + hihn.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + hihn.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + hihn.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + hihn.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetHIENPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        HIENs hien = await HIENsService.Create().GetSumHIENsAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + hien.Health + c.BaseStats.Health * hien.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + hien.PhysicalAttack + c.BaseStats.PhysicalAttack * hien.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + hien.PhysicalDefense + c.BaseStats.PhysicalDefense * hien.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + hien.MagicalAttack + c.BaseStats.MagicalAttack * hien.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + hien.MagicalDefense + c.BaseStats.MagicalDefense * hien.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + hien.ChemicalAttack + c.BaseStats.ChemicalAttack * hien.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + hien.ChemicalDefense + c.BaseStats.ChemicalDefense * hien.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + hien.AtomicAttack + c.BaseStats.AtomicAttack * hien.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + hien.AtomicDefense + c.BaseStats.AtomicDefense * hien.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + hien.MentalAttack + c.BaseStats.MentalAttack * hien.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + hien.MentalDefense + c.BaseStats.MentalDefense * hien.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + hien.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + hien.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + hien.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + hien.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + hien.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + hien.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + hien.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + hien.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + hien.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + hien.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + hien.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + hien.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + hien.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + hien.AccuracyRate;
            c.LifestealRate = c.LifestealRate + hien.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + hien.ShieldStrength;
            c.Tenacity = c.Tenacity + hien.Tenacity;
            c.ResistanceRate = c.ResistanceRate + hien.ResistanceRate;
            c.ComboRate = c.ComboRate + hien.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + hien.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + hien.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + hien.ComboResistanceRate;
            c.StunRate = c.StunRate + hien.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + hien.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + hien.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + hien.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + hien.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + hien.ReflectionResistanceRate;
            c.Mana = c.Mana + hien.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + hien.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + hien.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + hien.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + hien.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + hien.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + hien.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + hien.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + hien.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + hien.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetHICAPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        HICAs hica = await HICAsService.Create().GetSumHICAsAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + hica.Health + c.BaseStats.Health * hica.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + hica.PhysicalAttack + c.BaseStats.PhysicalAttack * hica.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + hica.PhysicalDefense + c.BaseStats.PhysicalDefense * hica.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + hica.MagicalAttack + c.BaseStats.MagicalAttack * hica.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + hica.MagicalDefense + c.BaseStats.MagicalDefense * hica.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + hica.ChemicalAttack + c.BaseStats.ChemicalAttack * hica.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + hica.ChemicalDefense + c.BaseStats.ChemicalDefense * hica.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + hica.AtomicAttack + c.BaseStats.AtomicAttack * hica.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + hica.AtomicDefense + c.BaseStats.AtomicDefense * hica.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + hica.MentalAttack + c.BaseStats.MentalAttack * hica.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + hica.MentalDefense + c.BaseStats.MentalDefense * hica.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + hica.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + hica.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + hica.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + hica.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + hica.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + hica.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + hica.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + hica.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + hica.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + hica.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + hica.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + hica.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + hica.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + hica.AccuracyRate;
            c.LifestealRate = c.LifestealRate + hica.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + hica.ShieldStrength;
            c.Tenacity = c.Tenacity + hica.Tenacity;
            c.ResistanceRate = c.ResistanceRate + hica.ResistanceRate;
            c.ComboRate = c.ComboRate + hica.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + hica.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + hica.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + hica.ComboResistanceRate;
            c.StunRate = c.StunRate + hica.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + hica.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + hica.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + hica.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + hica.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + hica.ReflectionResistanceRate;
            c.Mana = c.Mana + hica.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + hica.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + hica.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + hica.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + hica.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + hica.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + hica.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + hica.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + hica.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + hica.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetHIRNPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        HIRNs hirn = await HIRNsService.Create().GetSumHIRNsAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + hirn.Health + c.BaseStats.Health * hirn.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + hirn.PhysicalAttack + c.BaseStats.PhysicalAttack * hirn.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + hirn.PhysicalDefense + c.BaseStats.PhysicalDefense * hirn.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + hirn.MagicalAttack + c.BaseStats.MagicalAttack * hirn.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + hirn.MagicalDefense + c.BaseStats.MagicalDefense * hirn.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + hirn.ChemicalAttack + c.BaseStats.ChemicalAttack * hirn.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + hirn.ChemicalDefense + c.BaseStats.ChemicalDefense * hirn.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + hirn.AtomicAttack + c.BaseStats.AtomicAttack * hirn.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + hirn.AtomicDefense + c.BaseStats.AtomicDefense * hirn.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + hirn.MentalAttack + c.BaseStats.MentalAttack * hirn.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + hirn.MentalDefense + c.BaseStats.MentalDefense * hirn.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + hirn.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + hirn.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + hirn.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + hirn.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + hirn.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + hirn.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + hirn.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + hirn.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + hirn.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + hirn.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + hirn.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + hirn.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + hirn.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + hirn.AccuracyRate;
            c.LifestealRate = c.LifestealRate + hirn.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + hirn.ShieldStrength;
            c.Tenacity = c.Tenacity + hirn.Tenacity;
            c.ResistanceRate = c.ResistanceRate + hirn.ResistanceRate;
            c.ComboRate = c.ComboRate + hirn.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + hirn.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + hirn.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + hirn.ComboResistanceRate;
            c.StunRate = c.StunRate + hirn.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + hirn.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + hirn.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + hirn.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + hirn.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + hirn.ReflectionResistanceRate;
            c.Mana = c.Mana + hirn.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + hirn.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + hirn.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + hirn.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + hirn.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + hirn.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + hirn.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + hirn.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + hirn.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + hirn.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetHIDCPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        HIDCs hidc = await HIDCsService.Create().GetSumHIDCsAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + hidc.Health + c.BaseStats.Health * hidc.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + hidc.PhysicalAttack + c.BaseStats.PhysicalAttack * hidc.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + hidc.PhysicalDefense + c.BaseStats.PhysicalDefense * hidc.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + hidc.MagicalAttack + c.BaseStats.MagicalAttack * hidc.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + hidc.MagicalDefense + c.BaseStats.MagicalDefense * hidc.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + hidc.ChemicalAttack + c.BaseStats.ChemicalAttack * hidc.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + hidc.ChemicalDefense + c.BaseStats.ChemicalDefense * hidc.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + hidc.AtomicAttack + c.BaseStats.AtomicAttack * hidc.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + hidc.AtomicDefense + c.BaseStats.AtomicDefense * hidc.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + hidc.MentalAttack + c.BaseStats.MentalAttack * hidc.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + hidc.MentalDefense + c.BaseStats.MentalDefense * hidc.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + hidc.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + hidc.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + hidc.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + hidc.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + hidc.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + hidc.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + hidc.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + hidc.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + hidc.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + hidc.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + hidc.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + hidc.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + hidc.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + hidc.AccuracyRate;
            c.LifestealRate = c.LifestealRate + hidc.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + hidc.ShieldStrength;
            c.Tenacity = c.Tenacity + hidc.Tenacity;
            c.ResistanceRate = c.ResistanceRate + hidc.ResistanceRate;
            c.ComboRate = c.ComboRate + hidc.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + hidc.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + hidc.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + hidc.ComboResistanceRate;
            c.StunRate = c.StunRate + hidc.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + hidc.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + hidc.ReflectionRate;
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + hidc.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + hidc.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + hidc.ReflectionResistanceRate;
            c.Mana = c.Mana + hidc.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + hidc.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + hidc.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + hidc.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + hidc.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + hidc.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + hidc.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + hidc.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + hidc.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + hidc.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetAllEquipmentPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        foreach (var c in CardHeroesList)
        {
            Equipments equipments = await UserEquipmentsService.Create().GetAllEquipmentsByCardHeorIdAsync(user_id, c.Id);
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

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetAllRankPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        foreach (var c in CardHeroesList)
        {
            Rank rank = await UserCardHeroesRankService.Create().GetSumCardHeroesRankAsync(user_id, c.Id);
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
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + rank.IgnoreReflectionRate;
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

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetAllMasterPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        foreach (var c in CardHeroesList)
        {
            Master master = await UserCardHeroesMasterService.Create().GetSumCardHeroesMasterAsync(user_id, c.Id);
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
            c.IgnoreReflectionRate  = c.IgnoreReflectionRate + master.IgnoreReflectionRate;
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

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetAllAnimeStatsPowerAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        AnimeStats animeStats = await AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);
        foreach (var c in CardHeroesList)
        {
            c.Health = c.Health + animeStats.Health + c.BaseStats.Health * animeStats.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + animeStats.PhysicalAttack + c.BaseStats.PhysicalAttack * animeStats.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + animeStats.PhysicalDefense + c.BaseStats.PhysicalDefense * animeStats.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + animeStats.MagicalAttack + c.BaseStats.MagicalAttack * animeStats.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + animeStats.MagicalDefense + c.BaseStats.MagicalDefense * animeStats.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + animeStats.ChemicalAttack + c.BaseStats.ChemicalAttack * animeStats.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + animeStats.ChemicalDefense + c.BaseStats.ChemicalDefense * animeStats.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + animeStats.AtomicAttack + c.BaseStats.AtomicAttack * animeStats.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + animeStats.AtomicDefense + c.BaseStats.AtomicDefense * animeStats.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + animeStats.MentalAttack + c.BaseStats.MentalAttack * animeStats.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + animeStats.MentalDefense + c.BaseStats.MentalDefense * animeStats.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + animeStats.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + animeStats.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + animeStats.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + animeStats.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + animeStats.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + animeStats.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + animeStats.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + animeStats.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + animeStats.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + animeStats.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + animeStats.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + animeStats.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + animeStats.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + animeStats.AccuracyRate;
            c.LifestealRate = c.LifestealRate + animeStats.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + animeStats.ShieldStrength;
            c.Tenacity = c.Tenacity + animeStats.Tenacity;
            c.ResistanceRate = c.ResistanceRate + animeStats.ResistanceRate;
            c.ComboRate = c.ComboRate + animeStats.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + animeStats.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + animeStats.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + animeStats.ComboResistanceRate;
            c.StunRate = c.StunRate + animeStats.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + animeStats.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + animeStats.ReflectionRate;
            c.IgnoreReflectionRate = c.IgnoreReflectionRate + animeStats.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + animeStats.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + animeStats.ReflectionResistanceRate;
            c.Mana = c.Mana + animeStats.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + animeStats.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + animeStats.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + animeStats.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + animeStats.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + animeStats.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + animeStats.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + animeStats.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + animeStats.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + animeStats.SkillResistanceRate;

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<List<CardHeroes>> GetAllSpiritBeastPowerAsync(string user_id, List<CardHeroes> cardHeroes)
    {
        foreach (var c in cardHeroes)
        {
            SpiritBeasts spiritBeast = await UserSpiritBeastsService.Create().GetUserCardHeroSpiritBeastAsync(user_id, c);
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

            c.Power = EvaluatePower.CalculatePower(
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
    public async Task<CardHeroes> GetNewLevelPowerAsync(CardHeroes c, double coefficient)
    {
        ICardHeroesRepository _repository = new CardHeroesRepository();
        CardHeroesService _service = new CardHeroesService(_repository);
        CardHeroes orginCard = await _service.GetCardHeroByIdAsync(c.Id);
        CardHeroes cardHero = new CardHeroes
        {
            Id = c.Id,
            Health = c.Health + orginCard.Health * coefficient,
            PhysicalAttack = c.PhysicalAttack + orginCard.PhysicalAttack * coefficient,
            PhysicalDefense = c.PhysicalDefense + orginCard.PhysicalDefense * coefficient,
            MagicalAttack = c.MagicalAttack + orginCard.MagicalAttack * coefficient,
            MagicalDefense = c.MagicalDefense + orginCard.MagicalDefense * coefficient,
            ChemicalAttack = c.ChemicalAttack + orginCard.ChemicalAttack * coefficient,
            ChemicalDefense = c.ChemicalDefense + orginCard.ChemicalDefense * coefficient,
            AtomicAttack = c.AtomicAttack + orginCard.AtomicAttack * coefficient,
            AtomicDefense = c.AtomicDefense + orginCard.AtomicDefense * coefficient,
            MentalAttack = c.MentalAttack + orginCard.MentalAttack * coefficient,
            MentalDefense = c.MentalDefense + orginCard.MentalDefense * coefficient,
            Speed = c.Speed + orginCard.Speed * coefficient,
            CriticalDamageRate = c.CriticalDamageRate + orginCard.CriticalDamageRate * coefficient,
            CriticalRate = c.CriticalRate + orginCard.CriticalRate * coefficient,
            CriticalResistanceRate = c.CriticalResistanceRate + orginCard.CriticalResistanceRate * coefficient,
            IgnoreCriticalRate = c.IgnoreCriticalRate + orginCard.IgnoreCriticalRate * coefficient,
            PenetrationRate = c.PenetrationRate + orginCard.PenetrationRate * coefficient,
            PenetrationResistanceRate = c.PenetrationResistanceRate + orginCard.PenetrationResistanceRate * coefficient,
            EvasionRate = c.EvasionRate + orginCard.EvasionRate * coefficient,
            DamageAbsorptionRate = c.DamageAbsorptionRate + orginCard.DamageAbsorptionRate * coefficient,
            IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + orginCard.IgnoreDamageAbsorptionRate * coefficient,
            AbsorbedDamageRate = c.AbsorbedDamageRate + orginCard.AbsorbedDamageRate * coefficient,
            VitalityRegenerationRate = c.VitalityRegenerationRate + orginCard.VitalityRegenerationRate * coefficient,
            VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + orginCard.VitalityRegenerationResistanceRate * coefficient,
            AccuracyRate = c.AccuracyRate + orginCard.AccuracyRate * coefficient,
            LifestealRate = c.LifestealRate + orginCard.LifestealRate * coefficient,
            ShieldStrength = c.ShieldStrength + orginCard.ShieldStrength * coefficient,
            Tenacity = c.Tenacity + orginCard.Tenacity * coefficient,
            ResistanceRate = c.ResistanceRate + orginCard.ResistanceRate * coefficient,
            ComboRate = c.ComboRate + orginCard.ComboRate * coefficient,
            IgnoreComboRate = c.IgnoreComboRate + orginCard.IgnoreComboRate * coefficient,
            ComboDamageRate = c.ComboDamageRate + orginCard.ComboDamageRate * coefficient,
            ComboResistanceRate = c.ComboResistanceRate + orginCard.ComboResistanceRate * coefficient,
            StunRate = c.StunRate + orginCard.StunRate * coefficient,
            IgnoreStunRate = c.IgnoreStunRate + orginCard.IgnoreStunRate * coefficient,
            ReflectionRate = c.ReflectionRate + orginCard.ReflectionRate * coefficient,
            IgnoreReflectionRate = c.IgnoreReflectionRate + orginCard.IgnoreReflectionRate * coefficient,
            ReflectionDamageRate = c.ReflectionDamageRate + orginCard.ReflectionDamageRate * coefficient,
            ReflectionResistanceRate = c.ReflectionResistanceRate + orginCard.ReflectionResistanceRate * coefficient,
            Mana = c.Mana + orginCard.Mana * (float)coefficient,
            ManaRegenerationRate = c.ManaRegenerationRate + orginCard.ManaRegenerationRate * coefficient,
            DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + orginCard.DamageToDifferentFactionRate * coefficient,
            ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + orginCard.ResistanceToDifferentFactionRate * coefficient,
            DamageToSameFactionRate = c.DamageToSameFactionRate + orginCard.DamageToSameFactionRate * coefficient,
            ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + orginCard.ResistanceToSameFactionRate * coefficient,
            NormalDamageRate = c.NormalDamageRate + orginCard.NormalDamageRate * coefficient,
            NormalResistanceRate = c.NormalResistanceRate + orginCard.NormalResistanceRate * coefficient,
            SkillDamageRate = c.SkillDamageRate + orginCard.SkillDamageRate * coefficient,
            SkillResistanceRate = c.SkillResistanceRate + orginCard.SkillResistanceRate * coefficient
        };
        cardHero.Power = EvaluatePower.CalculatePower(
            cardHero.Health,
            cardHero.PhysicalAttack, cardHero.PhysicalDefense,
            cardHero.MagicalAttack, cardHero.MagicalDefense,
            cardHero.ChemicalAttack, cardHero.ChemicalDefense,
            cardHero.AtomicAttack, cardHero.AtomicDefense,
            cardHero.MentalAttack, cardHero.MentalDefense,
            cardHero.Speed,
            cardHero.CriticalDamageRate, cardHero.CriticalRate, cardHero.CriticalResistanceRate, cardHero.IgnoreCriticalRate,
            cardHero.PenetrationRate, cardHero.PenetrationResistanceRate, cardHero.EvasionRate,
            cardHero.DamageAbsorptionRate, cardHero.IgnoreDamageAbsorptionRate, cardHero.AbsorbedDamageRate,
            cardHero.VitalityRegenerationRate, cardHero.VitalityRegenerationResistanceRate,
            cardHero.AccuracyRate, cardHero.LifestealRate,
            cardHero.ShieldStrength, cardHero.Tenacity, cardHero.ResistanceRate,
            cardHero.ComboRate, cardHero.IgnoreComboRate, cardHero.ComboDamageRate, cardHero.ComboResistanceRate,
            cardHero.StunRate, cardHero.IgnoreStunRate,
            cardHero.ReflectionRate, cardHero.IgnoreReflectionRate, cardHero.ReflectionDamageRate, cardHero.ReflectionResistanceRate,
            cardHero.Mana, cardHero.ManaRegenerationRate,
            cardHero.DamageToDifferentFactionRate, cardHero.ResistanceToDifferentFactionRate,
            cardHero.DamageToSameFactionRate, cardHero.ResistanceToSameFactionRate,
            cardHero.NormalDamageRate, cardHero.NormalResistanceRate,
            cardHero.SkillDamageRate, cardHero.SkillResistanceRate
        );
        return cardHero;
    }
    public async Task<CardHeroes> GetNewBreakthroughPowerAsync(CardHeroes c, double coefficient)
    {
        ICardHeroesRepository _repository = new CardHeroesRepository();
        CardHeroesService _service = new CardHeroesService(_repository);
        CardHeroes orginCard = await _service.GetCardHeroByIdAsync(c.Id);
        CardHeroes cardHero = new CardHeroes
        {
            Id = c.Id,
            Health = c.Health + orginCard.Health * coefficient,
            PhysicalAttack = c.PhysicalAttack + orginCard.PhysicalAttack * coefficient,
            PhysicalDefense = c.PhysicalDefense + orginCard.PhysicalDefense * coefficient,
            MagicalAttack = c.MagicalAttack + orginCard.MagicalAttack * coefficient,
            MagicalDefense = c.MagicalDefense + orginCard.MagicalDefense * coefficient,
            ChemicalAttack = c.ChemicalAttack + orginCard.ChemicalAttack * coefficient,
            ChemicalDefense = c.ChemicalDefense + orginCard.ChemicalDefense * coefficient,
            AtomicAttack = c.AtomicAttack + orginCard.AtomicAttack * coefficient,
            AtomicDefense = c.AtomicDefense + orginCard.AtomicDefense * coefficient,
            MentalAttack = c.MentalAttack + orginCard.MentalAttack * coefficient,
            MentalDefense = c.MentalDefense + orginCard.MentalDefense * coefficient,
            Speed = c.Speed + orginCard.Speed * coefficient,
            CriticalDamageRate = c.CriticalDamageRate + orginCard.CriticalDamageRate * coefficient,
            CriticalRate = c.CriticalRate + orginCard.CriticalRate * coefficient,
            CriticalResistanceRate = c.CriticalResistanceRate + orginCard.CriticalResistanceRate * coefficient,
            IgnoreCriticalRate = c.IgnoreCriticalRate + orginCard.IgnoreCriticalRate * coefficient,
            PenetrationRate = c.PenetrationRate + orginCard.PenetrationRate * coefficient,
            PenetrationResistanceRate = c.PenetrationResistanceRate + orginCard.PenetrationResistanceRate * coefficient,
            EvasionRate = c.EvasionRate + orginCard.EvasionRate * coefficient,
            DamageAbsorptionRate = c.DamageAbsorptionRate + orginCard.DamageAbsorptionRate * coefficient,
            IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + orginCard.IgnoreDamageAbsorptionRate * coefficient,
            AbsorbedDamageRate = c.AbsorbedDamageRate + orginCard.AbsorbedDamageRate * coefficient,
            VitalityRegenerationRate = c.VitalityRegenerationRate + orginCard.VitalityRegenerationRate * coefficient,
            VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + orginCard.VitalityRegenerationResistanceRate * coefficient,
            AccuracyRate = c.AccuracyRate + orginCard.AccuracyRate * coefficient,
            LifestealRate = c.LifestealRate + orginCard.LifestealRate * coefficient,
            ShieldStrength = c.ShieldStrength + orginCard.ShieldStrength * coefficient,
            Tenacity = c.Tenacity + orginCard.Tenacity * coefficient,
            ResistanceRate = c.ResistanceRate + orginCard.ResistanceRate * coefficient,
            ComboRate = c.ComboRate + orginCard.ComboRate * coefficient,
            IgnoreComboRate = c.IgnoreComboRate + orginCard.IgnoreComboRate * coefficient,
            ComboDamageRate = c.ComboDamageRate + orginCard.ComboDamageRate * coefficient,
            ComboResistanceRate = c.ComboResistanceRate + orginCard.ComboResistanceRate * coefficient,
            StunRate = c.StunRate + orginCard.StunRate * coefficient,
            IgnoreStunRate = c.IgnoreStunRate + orginCard.IgnoreStunRate * coefficient,
            ReflectionRate = c.ReflectionRate + orginCard.ReflectionRate * coefficient,
            IgnoreReflectionRate  = c.IgnoreReflectionRate + orginCard.IgnoreReflectionRate * coefficient,
            ReflectionDamageRate = c.ReflectionDamageRate + orginCard.ReflectionDamageRate * coefficient,
            ReflectionResistanceRate = c.ReflectionResistanceRate + orginCard.ReflectionResistanceRate * coefficient,
            Mana = c.Mana + orginCard.Mana * (float)coefficient,
            ManaRegenerationRate = c.ManaRegenerationRate + orginCard.ManaRegenerationRate * coefficient,
            DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + orginCard.DamageToDifferentFactionRate * coefficient,
            ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + orginCard.ResistanceToDifferentFactionRate * coefficient,
            DamageToSameFactionRate = c.DamageToSameFactionRate + orginCard.DamageToSameFactionRate * coefficient,
            ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + orginCard.ResistanceToSameFactionRate * coefficient,
            NormalDamageRate = c.NormalDamageRate + orginCard.NormalDamageRate * coefficient,
            NormalResistanceRate = c.NormalResistanceRate + orginCard.NormalResistanceRate * coefficient,
            SkillDamageRate = c.SkillDamageRate + orginCard.SkillDamageRate * coefficient,
            SkillResistanceRate = c.SkillResistanceRate + orginCard.SkillResistanceRate * coefficient
        };
        cardHero.Power = EvaluatePower.CalculatePower(
            cardHero.Health,
            cardHero.PhysicalAttack, cardHero.PhysicalDefense,
            cardHero.MagicalAttack, cardHero.MagicalDefense,
            cardHero.ChemicalAttack, cardHero.ChemicalDefense,
            cardHero.AtomicAttack, cardHero.AtomicDefense,
            cardHero.MentalAttack, cardHero.MentalDefense,
            cardHero.Speed,
            cardHero.CriticalDamageRate, cardHero.CriticalRate, cardHero.CriticalResistanceRate, cardHero.IgnoreCriticalRate,
            cardHero.PenetrationRate, cardHero.PenetrationResistanceRate, cardHero.EvasionRate,
            cardHero.DamageAbsorptionRate, cardHero.IgnoreDamageAbsorptionRate, cardHero.AbsorbedDamageRate,
            cardHero.VitalityRegenerationRate, cardHero.VitalityRegenerationResistanceRate,
            cardHero.AccuracyRate, cardHero.LifestealRate,
            cardHero.ShieldStrength, cardHero.Tenacity, cardHero.ResistanceRate,
            cardHero.ComboRate, cardHero.IgnoreComboRate, cardHero.ComboDamageRate, cardHero.ComboResistanceRate,
            cardHero.StunRate, cardHero.IgnoreStunRate,
            cardHero.ReflectionRate, cardHero.IgnoreReflectionRate, cardHero.ReflectionDamageRate, cardHero.ReflectionResistanceRate,
            cardHero.Mana, cardHero.ManaRegenerationRate,
            cardHero.DamageToDifferentFactionRate, cardHero.ResistanceToDifferentFactionRate,
            cardHero.DamageToSameFactionRate, cardHero.ResistanceToSameFactionRate,
            cardHero.NormalDamageRate, cardHero.NormalResistanceRate,
            cardHero.SkillDamageRate, cardHero.SkillResistanceRate
        );
        return cardHero;
    }
    public async Task<List<CardHeroes>> GetSkillsAsync(string user_id, List<CardHeroes> CardHeroesList)
    {
        foreach(CardHeroes cardHeroes in CardHeroesList)
        {
            var skills = await UserSkillsService.Create().GetUserCardHeroesSkillsAsync(user_id, cardHeroes.Id);
            skills = skills.Where(x => x.Position != 0).ToList();
            cardHeroes.Skills = skills;
        }
        return CardHeroesList;
    }
    public async Task<List<CardHeroes>> GetUserCardHeroesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardHeroes> list = await _userCardHeroesRepository.GetUserCardHeroesAsync(user_id, search, type, pageSize, offset, rare);
        list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = await GetFinalPowerAsync(user_id, list);
        list = await GetAllEquipmentPowerAsync(user_id, list);
        list = await GetAllRankPowerAsync(user_id, list);
        list = await GetAllMasterPowerAsync(user_id, list);
        list = await GetAllAnimeStatsPowerAsync(user_id, list);
        list = await GetScienceFictionPowerAsync(user_id, list);
        list = await GetResearchPowerAsync(user_id, list);
        list = await GetArchivePowerAsync(user_id, list);
        list = await GetUniversePowerAsync(user_id, list);
        list = await GetHIINPowerAsync(user_id, list);
        list = await GetSSWNPowerAsync(user_id, list);
        list = await GetHITNPowerAsync(user_id, list);
        list = await GetHIHNPowerAsync(user_id, list);
        list = await GetHIENPowerAsync(user_id, list);
        list = await GetHICAPowerAsync(user_id, list);
        list = await GetHIRNPowerAsync(user_id, list);
        list = await GetHIDCPowerAsync(user_id, list);
        list = await GetSkillsAsync(user_id, list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<CardHeroes>> GetUserCardHeroesTeamAsync(string user_id, string teamId, string position)
    {
        List<CardHeroes> list = await _userCardHeroesRepository.GetUserCardHeroesTeamAsync(user_id, teamId, position);
        list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = await GetFinalPowerAsync(user_id, list);
        list = await GetAllEquipmentPowerAsync(user_id, list);
        list = await GetAllRankPowerAsync(user_id, list);
        list = await GetAllMasterPowerAsync(user_id, list);
        list = await GetAllAnimeStatsPowerAsync(user_id, list);
        list = await GetScienceFictionPowerAsync(user_id, list);
        list = await GetResearchPowerAsync(user_id, list);
        list = await GetArchivePowerAsync(user_id, list);
        list = await GetUniversePowerAsync(user_id, list);
        list = await GetHIINPowerAsync(user_id, list);
        list = await GetSSWNPowerAsync(user_id, list);
        list = await GetHITNPowerAsync(user_id, list);
        list = await GetHIHNPowerAsync(user_id, list);
        list = await GetHIENPowerAsync(user_id, list);
        list = await GetHICAPowerAsync(user_id, list);
        list = await GetHIRNPowerAsync(user_id, list);
        list = await GetHIDCPowerAsync(user_id, list);
        list = await GetSkillsAsync(user_id, list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<CardHeroes>> GetUserCardHeroesTeamWithoutPositionAsync(string user_id, string teamId)
    {
        List<CardHeroes> list = await _userCardHeroesRepository.GetUserCardHeroesTeamWithoutPositionAsync(user_id, teamId);
        list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = await GetFinalPowerAsync(user_id, list);
        list = await GetAllEquipmentPowerAsync(user_id, list);
        list = await GetAllRankPowerAsync(user_id, list);
        list = await GetAllMasterPowerAsync(user_id, list);
        list = await GetAllAnimeStatsPowerAsync(user_id, list);
        list = await GetScienceFictionPowerAsync(user_id, list);
        list = await GetResearchPowerAsync(user_id, list);
        list = await GetArchivePowerAsync(user_id, list);
        list = await GetUniversePowerAsync(user_id, list);
        list = await GetHIINPowerAsync(user_id, list);
        list = await GetSSWNPowerAsync(user_id, list);
        list = await GetHITNPowerAsync(user_id, list);
        list = await GetHIHNPowerAsync(user_id, list);
        list = await GetHIENPowerAsync(user_id, list);
        list = await GetHICAPowerAsync(user_id, list);
        list = await GetHIRNPowerAsync(user_id, list);
        list = await GetHIDCPowerAsync(user_id, list);
        list = await GetSkillsAsync(user_id, list);
        return list;
    }

    public async Task<Dictionary<string, int>> GetUniqueCardHeroesTypesTeamAsync(string teamId)
    {
        return await _userCardHeroesRepository.GetUniqueCardHeroesTypesTeamAsync(teamId);
    }

    public async Task<int> GetUserCardHeroesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userCardHeroesRepository.GetUserCardHeroesCountAsync(user_id, search, type, rare);
    }

    public async Task<int> GetUserCardHeroesTeamsPositionCountAsync(string user_id, string team_id, string position)
    {
        return await _userCardHeroesRepository.GetUserCardHeroesTeamsPositionCountAsync(user_id, team_id, position);
    }

    public async Task<int> GetUserCardHeroesTeamsCountAsync(string user_id, string team_id)
    {
        return await _userCardHeroesRepository.GetUserCardHeroesTeamsCountAsync(user_id, team_id);
    }

    public async Task<bool> InsertUserCardHeroAsync(CardHeroes CardHeroes)
    {
        return await _userCardHeroesRepository.InsertUserCardHeroAsync(CardHeroes);
    }

    public async Task<bool> UpdateCardHeroLevelAsync(CardHeroes cardHeroes, int cardLevel)
    {
        return await _userCardHeroesRepository.UpdateCardHeroLevelAsync(cardHeroes, cardLevel);
    }

    public async Task<bool> UpdateCardHeroBreakthroughAsync(CardHeroes cardHeroes, int star, double quantity)
    {
        return await _userCardHeroesRepository.UpdateCardHeroBreakthroughAsync(cardHeroes, star, quantity);
    }

    public async Task<bool> UpdateTeamCardHeroAsync(string team_id, string position, string card_id)
    {
        return await _userCardHeroesRepository.UpdateTeamCardHeroAsync(team_id, position, card_id);
    }

    public async Task<CardHeroes> GetUserCardHeroByIdAsync(string user_id, string Id)
    {
        CardHeroes cardHero = await _userCardHeroesRepository.GetUserCardHeroByIdAsync(user_id, Id);
        if (cardHero == null) return null;

        // Bọc vào list để tái sử dụng logic
        List<CardHeroes> list = new List<CardHeroes> { cardHero };

        list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = await GetFinalPowerAsync(user_id, list);
        list = await GetAllEquipmentPowerAsync(user_id, list);
        list = await GetAllRankPowerAsync(user_id, list);
        list = await GetAllMasterPowerAsync(user_id, list);
        list = await GetAllAnimeStatsPowerAsync(user_id, list);
        list = await GetScienceFictionPowerAsync(user_id, list);
        list = await GetResearchPowerAsync(user_id, list);
        list = await GetArchivePowerAsync(user_id, list);
        list = await GetUniversePowerAsync(user_id, list);
        list = await GetHIINPowerAsync(user_id, list);
        list = await GetSSWNPowerAsync(user_id, list);
        list = await GetHITNPowerAsync(user_id, list);
        list = await GetHIHNPowerAsync(user_id, list);
        list = await GetHIENPowerAsync(user_id, list);
        list = await GetHICAPowerAsync(user_id, list);
        list = await GetHIRNPowerAsync(user_id, list);
        list = await GetHIDCPowerAsync(user_id, list);
        list = await GetSkillsAsync(user_id, list);
        return list.FirstOrDefault();
    }

    public async Task<List<CardHeroes>> GetAllUserCardHeroesInTeamAsync(string user_id)
    {
        List<CardHeroes> list = await _userCardHeroesRepository.GetAllUserCardHeroesInTeamAsync(user_id);
        list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = await GetFinalPowerAsync(user_id, list);
        list = await GetAllEquipmentPowerAsync(user_id, list);
        list = await GetAllRankPowerAsync(user_id, list);
        list = await GetAllMasterPowerAsync(user_id, list);
        list = await GetAllAnimeStatsPowerAsync(user_id, list);
        list = await GetScienceFictionPowerAsync(user_id, list);
        list = await GetResearchPowerAsync(user_id, list);
        list = await GetArchivePowerAsync(user_id, list);
        list = await GetUniversePowerAsync(user_id, list);
        list = await GetHIINPowerAsync(user_id, list);
        list = await GetSSWNPowerAsync(user_id, list);
        list = await GetHITNPowerAsync(user_id, list);
        list = await GetHIHNPowerAsync(user_id, list);
        list = await GetHIENPowerAsync(user_id, list);
        list = await GetHICAPowerAsync(user_id, list);
        list = await GetHIRNPowerAsync(user_id, list);
        list = await GetHIDCPowerAsync(user_id, list);
        list = await GetSkillsAsync(user_id, list);
        ListSortHelper.SortByPower(list);
        return list;
    }
}

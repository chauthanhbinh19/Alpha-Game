using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserCardCaptainsService : IUserCardCaptainsService
{
     private static UserCardCaptainsService _instance;
    private IUserCardCaptainsRepository _userCardCaptainsRepository;

    public UserCardCaptainsService(IUserCardCaptainsRepository userCardCaptainsRepository)
    {
        _userCardCaptainsRepository = userCardCaptainsRepository;
    }

    public static UserCardCaptainsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardCaptainsService(new UserCardCaptainsRepository());
        }
        return _instance;
    }

    public async Task<List<CardCaptains>> GetFinalPowerAsync(string user_id, List<CardCaptains> CardCaptainsList)
    {
        PowerManager powerManager = await PowerManagerService.Create().GetUserStatsAsync(user_id);
        foreach (var c in CardCaptainsList)
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
        return CardCaptainsList;
    }
    public async Task<List<CardCaptains>> GetScienceFictionPowerAsync(string user_id, List<CardCaptains> CardCaptainsList)
    {
        ScienceFiction scienceFiction = await ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        foreach (var c in CardCaptainsList)
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
        return CardCaptainsList;
    }
    public async Task<List<CardCaptains>> GetResearchPowerAsync(string user_id, List<CardCaptains> CardCaptainsList)
    {
        Researchs research = await ResearchsService.Create().GetSumResearchsAsync(user_id);
        foreach (var c in CardCaptainsList)
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
        return CardCaptainsList;
    }
    public async Task<List<CardCaptains>> GetArchivePowerAsync(string user_id, List<CardCaptains> CardCaptainsList)
    {
        Archives archive = await ArchivesService.Create().GetSumArchivesAsync(user_id);
        foreach (var c in CardCaptainsList)
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
        return CardCaptainsList;
    }
    public async Task<List<CardCaptains>> GetUniversePowerAsync(string user_id, List<CardCaptains> CardCaptainsList)
    {
        Universes universe = await UniversesService.Create().GetSumUniversesAsync(user_id);
        foreach (var c in CardCaptainsList)
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
        return CardCaptainsList;
    }
    public async Task<List<CardCaptains>> GetHIINPowerAsync(string user_id, List<CardCaptains> CardCaptainsList)
    {
        HIINs hiin = await HIINsService.Create().GetSumHIINsAsync(user_id);
        foreach (var c in CardCaptainsList)
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
        return CardCaptainsList;
    }
    public async Task<List<CardCaptains>> GetAllEquipmentPowerAsync(string user_id, List<CardCaptains> CardCaptainsList)
    {
        foreach (var c in CardCaptainsList)
        {
            Equipments equipments = await UserEquipmentsService.Create().GetAllEquipmentsByCardCaptainIdAsync(user_id, c.Id);
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
        return CardCaptainsList;
    }
    public async Task<List<CardCaptains>> GetAllRankPowerAsync(string user_id, List<CardCaptains> CardCaptainsList)
    {
        foreach (var c in CardCaptainsList)
        {
            Rank rank = await UserCardCaptainsRankService.Create().GetSumCardCaptainsRankAsync(user_id, c.Id);
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
        return CardCaptainsList;
    }
    public async Task<List<CardCaptains>> GetAllMasterPowerAsync(string user_id, List<CardCaptains> CardCaptainsList)
    {
        foreach (var c in CardCaptainsList)
        {
            Master master = await UserCardCaptainsMasterService.Create().GetSumCardCaptainsMasterAsync(user_id, c.Id);
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
        return CardCaptainsList;
    }
    public async Task<List<CardCaptains>> GetAllAnimeStatsPowerAsync(string user_id, List<CardCaptains> CardCaptainsList)
    {
        AnimeStats animeStats = await AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);
        foreach (var c in CardCaptainsList)
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
        return CardCaptainsList;
    }
    public async Task<List<CardCaptains>> GetAllSpiritBeastPowerAsync(string user_id, List<CardCaptains> cardCaptains)
    {
        foreach (var c in cardCaptains)
        {
            SpiritBeasts spiritBeast = await UserSpiritBeastsService.Create().GetUserCardCaptainSpiritBeastAsync(user_id, c);
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
        return cardCaptains;
    }
    public async Task<CardCaptains> GetNewLevelPowerAsync(CardCaptains c, double coefficient)
    {
        ICardCaptainsRepository _repository = new CardCaptainsRepository();
        CardCaptainsService _service = new CardCaptainsService(_repository);
        CardCaptains orginCard = await _service.GetCardCaptainByIdAsync(c.Id);
        CardCaptains cardCaptains = new CardCaptains
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
        cardCaptains.Power = EvaluatePower.CalculatePower(
            cardCaptains.Health,
            cardCaptains.PhysicalAttack, cardCaptains.PhysicalDefense,
            cardCaptains.MagicalAttack, cardCaptains.MagicalDefense,
            cardCaptains.ChemicalAttack, cardCaptains.ChemicalDefense,
            cardCaptains.AtomicAttack, cardCaptains.AtomicDefense,
            cardCaptains.MentalAttack, cardCaptains.MentalDefense,
            cardCaptains.Speed,
            cardCaptains.CriticalDamageRate, cardCaptains.CriticalRate, cardCaptains.CriticalResistanceRate, cardCaptains.IgnoreCriticalRate,
            cardCaptains.PenetrationRate, cardCaptains.PenetrationResistanceRate, cardCaptains.EvasionRate,
            cardCaptains.DamageAbsorptionRate, cardCaptains.IgnoreDamageAbsorptionRate, cardCaptains.AbsorbedDamageRate,
            cardCaptains.VitalityRegenerationRate, cardCaptains.VitalityRegenerationResistanceRate,
            cardCaptains.AccuracyRate, cardCaptains.LifestealRate,
            cardCaptains.ShieldStrength, cardCaptains.Tenacity, cardCaptains.ResistanceRate,
            cardCaptains.ComboRate, cardCaptains.IgnoreComboRate, cardCaptains.ComboDamageRate, cardCaptains.ComboResistanceRate,
            cardCaptains.StunRate, cardCaptains.IgnoreStunRate,
            cardCaptains.ReflectionRate, cardCaptains.IgnoreReflectionRate, cardCaptains.ReflectionDamageRate, cardCaptains.ReflectionResistanceRate,
            cardCaptains.Mana, cardCaptains.ManaRegenerationRate,
            cardCaptains.DamageToDifferentFactionRate, cardCaptains.ResistanceToDifferentFactionRate,
            cardCaptains.DamageToSameFactionRate, cardCaptains.ResistanceToSameFactionRate,
            cardCaptains.NormalDamageRate, cardCaptains.NormalResistanceRate,
            cardCaptains.SkillDamageRate, cardCaptains.SkillResistanceRate
        );
        return cardCaptains;
    }
    public async Task<CardCaptains> GetNewBreakthroughPowerAsync(CardCaptains c, double coefficient)
    {
        ICardCaptainsRepository _repository = new CardCaptainsRepository();
        CardCaptainsService _service = new CardCaptainsService(_repository);
        CardCaptains orginCard = await _service.GetCardCaptainByIdAsync(c.Id);
        CardCaptains cardCaptains = new CardCaptains
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
        cardCaptains.Power = EvaluatePower.CalculatePower(
            cardCaptains.Health,
            cardCaptains.PhysicalAttack, cardCaptains.PhysicalDefense,
            cardCaptains.MagicalAttack, cardCaptains.MagicalDefense,
            cardCaptains.ChemicalAttack, cardCaptains.ChemicalDefense,
            cardCaptains.AtomicAttack, cardCaptains.AtomicDefense,
            cardCaptains.MentalAttack, cardCaptains.MentalDefense,
            cardCaptains.Speed,
            cardCaptains.CriticalDamageRate, cardCaptains.CriticalRate, cardCaptains.CriticalResistanceRate, cardCaptains.IgnoreCriticalRate,
            cardCaptains.PenetrationRate, cardCaptains.PenetrationResistanceRate, cardCaptains.EvasionRate,
            cardCaptains.DamageAbsorptionRate, cardCaptains.IgnoreDamageAbsorptionRate, cardCaptains.AbsorbedDamageRate,
            cardCaptains.VitalityRegenerationRate, cardCaptains.VitalityRegenerationResistanceRate,
            cardCaptains.AccuracyRate, cardCaptains.LifestealRate,
            cardCaptains.ShieldStrength, cardCaptains.Tenacity, cardCaptains.ResistanceRate,
            cardCaptains.ComboRate, cardCaptains.IgnoreComboRate, cardCaptains.ComboDamageRate, cardCaptains.ComboResistanceRate,
            cardCaptains.StunRate, cardCaptains.IgnoreStunRate,
            cardCaptains.ReflectionRate, cardCaptains.IgnoreReflectionRate, cardCaptains.ReflectionDamageRate, cardCaptains.ReflectionResistanceRate,
            cardCaptains.Mana, cardCaptains.ManaRegenerationRate,
            cardCaptains.DamageToDifferentFactionRate, cardCaptains.ResistanceToDifferentFactionRate,
            cardCaptains.DamageToSameFactionRate, cardCaptains.ResistanceToSameFactionRate,
            cardCaptains.NormalDamageRate, cardCaptains.NormalResistanceRate,
            cardCaptains.SkillDamageRate, cardCaptains.SkillResistanceRate
        );
        return cardCaptains;
    }
    public async Task<List<CardCaptains>> GetSkillsAsync(string user_id, List<CardCaptains> CardCaptainsList)
    {
        foreach(CardCaptains cardCaptain in CardCaptainsList)
        {
            var skills = await UserSkillsService.Create().GetUserCardCaptainsSkillsAsync(user_id, cardCaptain.Id);
            skills = skills.Where(x => x.Position != 0).ToList();
            cardCaptain.Skills = skills;
        }
        return CardCaptainsList;
    }
    public async Task<List<CardCaptains>> GetUserCardCaptainsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardCaptains> list = await _userCardCaptainsRepository.GetUserCardCaptainsAsync(user_id, search, type, pageSize, offset, rare);
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
        list = await GetSkillsAsync(user_id, list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<CardCaptains>> GetUserCardCaptainsTeamAsync(string user_id, string teamId, string position)
    {
        List<CardCaptains> list = await _userCardCaptainsRepository.GetUserCardCaptainsTeamAsync(user_id, teamId, position);
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
        list = await GetSkillsAsync(user_id, list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<CardCaptains>> GetUserCardCaptainsTeamWithoutPositionAsync(string user_id, string teamId)
    {
        List<CardCaptains> list = await _userCardCaptainsRepository.GetUserCardCaptainsTeamWithoutPositionAsync(user_id, teamId);
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
        list = await GetSkillsAsync(user_id, list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<Dictionary<string, int>> GetUniqueCardCaptainsTypesTeamAsync(string teamId)
    {
        return await _userCardCaptainsRepository.GetUniqueCardCaptainsTypesTeamAsync(teamId);
    }

    public async Task<bool> UpdateTeamCardCaptainAsync(string team_id, string position, string card_id)
    {
        return await _userCardCaptainsRepository.UpdateTeamCardCaptainAsync(team_id, position, card_id);
    }

    public async Task<int> GetUserCardCaptainsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userCardCaptainsRepository.GetUserCardCaptainsCountAsync(user_id, search, type, rare);
    }

    public async Task<int> GetUserCardCaptainsTeamsPositionCountAsync(string user_id, string team_id, string position)
    {
        return await _userCardCaptainsRepository.GetUserCardCaptainsTeamsPositionCountAsync(user_id, team_id, position);
    }

    public async Task<int> GetUserCardCaptainsTeamsCountAsync(string user_id, string team_id)
    {
        return await _userCardCaptainsRepository.GetUserCardCaptainsTeamsCountAsync(user_id, team_id);
    }

    public async Task<bool> InsertUserCardCaptainAsync(CardCaptains CardCaptains)
    {
        return await _userCardCaptainsRepository.InsertUserCardCaptainAsync(CardCaptains);
    }

    public async Task<bool> UpdateCardCaptainLevelAsync(CardCaptains cardCaptains, int cardLevel)
    {
        return await _userCardCaptainsRepository.UpdateCardCaptainLevelAsync(cardCaptains, cardLevel);
    }

    public async Task<bool> UpdateCardCaptainBreakthroughAsync(CardCaptains cardCaptains, int star, double quantity)
    {
        return await _userCardCaptainsRepository.UpdateCardCaptainBreakthroughAsync(cardCaptains, star, quantity);
    }

    public async Task<CardCaptains> GetUserCardCaptainByIdAsync(string user_id, string Id)
    {
        CardCaptains cardCaptain = await _userCardCaptainsRepository.GetUserCardCaptainByIdAsync(user_id, Id);
        if (cardCaptain == null) return null;

        // Bọc vào list để tái sử dụng logic
        List<CardCaptains> list = new List<CardCaptains> { cardCaptain };

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
        list = await GetSkillsAsync(user_id, list);
        return list.FirstOrDefault();
    }

    public async Task<List<CardCaptains>> GetAllUserCardCaptainsInTeamAsync(string user_id)
    {
        List<CardCaptains> list = await _userCardCaptainsRepository.GetAllUserCardCaptainsInTeamAsync(user_id);
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
        list = await GetSkillsAsync(user_id, list);
        ListSortHelper.SortByPower(list);
        return list;
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserCardMonstersService : IUserCardMonstersService
{
    private readonly IUserCardMonstersRepository _userCardMonstersRepository;

    public UserCardMonstersService(IUserCardMonstersRepository userCardMonstersRepository)
    {
        _userCardMonstersRepository = userCardMonstersRepository;
    }

    public static UserCardMonstersService Create()
    {
        return new UserCardMonstersService(new UserCardMonstersRepository());
    }

    public async Task<List<CardMonsters>> GetFinalPowerAsync(string user_id, List<CardMonsters> CardMonstersList)
    {
        PowerManager powerManager = await PowerManagerService.Create().GetUserStatsAsync(user_id);
        foreach (var c in CardMonstersList)
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
        return CardMonstersList;
    }
    public async Task<List<CardMonsters>> GetScienceFictionPowerAsync(string user_id, List<CardMonsters> CardMonstersList)
    {
        ScienceFiction scienceFiction = await ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        foreach (var c in CardMonstersList)
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
        return CardMonstersList;
    }
    public async Task<List<CardMonsters>> GetAllEquipmentPowerAsync(string user_id, List<CardMonsters> CardMonstersList)
    {
        foreach (var c in CardMonstersList)
        {
            Equipments equipments = await UserEquipmentsService.Create().GetAllEquipmentsByCardMonsterIdAsync(user_id, c.Id);
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
        return CardMonstersList;
    }
    public async Task<List<CardMonsters>> GetAllRankPowerAsync(string user_id, List<CardMonsters> CardMonstersList)
    {
        foreach (var c in CardMonstersList)
        {
            Rank rank = await UserCardMonstersRankService.Create().GetSumCardMonstersRankAsync(user_id, c.Id);
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
        return CardMonstersList;
    }
    public async Task<List<CardMonsters>> GetAllMasterPowerAsync(string user_id, List<CardMonsters> CardMonstersList)
    {
        foreach (var c in CardMonstersList)
        {
            Master master = await UserCardMonstersMasterService.Create().GetSumCardMonstersMasterAsync(user_id, c.Id);
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
        return CardMonstersList;
    }
    public async Task<List<CardMonsters>> GetAllAnimeStatsPowerAsync(string user_id, List<CardMonsters> CardMonstersList)
    {
        AnimeStats animeStats = await AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);
        foreach (var c in CardMonstersList)
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
        return CardMonstersList;
    }
    public async Task<List<CardMonsters>> GetAllSpiritBeastPowerAsync(string user_id, List<CardMonsters> cardMonsters)
    {
        foreach (var c in cardMonsters)
        {
            SpiritBeasts spiritBeast = await UserSpiritBeastsService.Create().GetUserCardMonsterSpiritBeastAsync(user_id, c);
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
        return cardMonsters;
    }
    public async Task<CardMonsters> GetNewLevelPowerAsync(CardMonsters c, double coefficient)
    {
        ICardMonstersRepository _repository = new CardMonstersRepository();
        CardMonstersService _service = new CardMonstersService(_repository);
        CardMonsters orginCard = await _service.GetCardMonsterByIdAsync(c.Id);
        CardMonsters cardMonsters = new CardMonsters
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
        cardMonsters.Power = EvaluatePower.CalculatePower(
            cardMonsters.Health,
            cardMonsters.PhysicalAttack, cardMonsters.PhysicalDefense,
            cardMonsters.MagicalAttack, cardMonsters.MagicalDefense,
            cardMonsters.ChemicalAttack, cardMonsters.ChemicalDefense,
            cardMonsters.AtomicAttack, cardMonsters.AtomicDefense,
            cardMonsters.MentalAttack, cardMonsters.MentalDefense,
            cardMonsters.Speed,
            cardMonsters.CriticalDamageRate, cardMonsters.CriticalRate, cardMonsters.CriticalResistanceRate, cardMonsters.IgnoreCriticalRate,
            cardMonsters.PenetrationRate, cardMonsters.PenetrationResistanceRate, cardMonsters.EvasionRate,
            cardMonsters.DamageAbsorptionRate, cardMonsters.IgnoreDamageAbsorptionRate, cardMonsters.AbsorbedDamageRate,
            cardMonsters.VitalityRegenerationRate, cardMonsters.VitalityRegenerationResistanceRate,
            cardMonsters.AccuracyRate, cardMonsters.LifestealRate,
            cardMonsters.ShieldStrength, cardMonsters.Tenacity, cardMonsters.ResistanceRate,
            cardMonsters.ComboRate, cardMonsters.IgnoreComboRate, cardMonsters.ComboDamageRate, cardMonsters.ComboResistanceRate,
            cardMonsters.StunRate, cardMonsters.IgnoreStunRate,
            cardMonsters.ReflectionRate, cardMonsters.IgnoreReflectionRate, cardMonsters.ReflectionDamageRate, cardMonsters.ReflectionResistanceRate,
            cardMonsters.Mana, cardMonsters.ManaRegenerationRate,
            cardMonsters.DamageToDifferentFactionRate, cardMonsters.ResistanceToDifferentFactionRate,
            cardMonsters.DamageToSameFactionRate, cardMonsters.ResistanceToSameFactionRate,
            cardMonsters.NormalDamageRate, cardMonsters.NormalResistanceRate,
            cardMonsters.SkillDamageRate, cardMonsters.SkillResistanceRate
        );
        return cardMonsters;
    }
    public async Task<CardMonsters> GetNewBreakthroughPowerAsync(CardMonsters c, double coefficient)
    {
        ICardMonstersRepository _repository = new CardMonstersRepository();
        CardMonstersService _service = new CardMonstersService(_repository);
        CardMonsters orginCard = await _service.GetCardMonsterByIdAsync(c.Id);
        CardMonsters cardMonsters = new CardMonsters
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
        cardMonsters.Power = EvaluatePower.CalculatePower(
            cardMonsters.Health,
            cardMonsters.PhysicalAttack, cardMonsters.PhysicalDefense,
            cardMonsters.MagicalAttack, cardMonsters.MagicalDefense,
            cardMonsters.ChemicalAttack, cardMonsters.ChemicalDefense,
            cardMonsters.AtomicAttack, cardMonsters.AtomicDefense,
            cardMonsters.MentalAttack, cardMonsters.MentalDefense,
            cardMonsters.Speed,
            cardMonsters.CriticalDamageRate, cardMonsters.CriticalRate, cardMonsters.CriticalResistanceRate, cardMonsters.IgnoreCriticalRate,
            cardMonsters.PenetrationRate, cardMonsters.PenetrationResistanceRate, cardMonsters.EvasionRate,
            cardMonsters.DamageAbsorptionRate, cardMonsters.IgnoreDamageAbsorptionRate, cardMonsters.AbsorbedDamageRate,
            cardMonsters.VitalityRegenerationRate, cardMonsters.VitalityRegenerationResistanceRate,
            cardMonsters.AccuracyRate, cardMonsters.LifestealRate,
            cardMonsters.ShieldStrength, cardMonsters.Tenacity, cardMonsters.ResistanceRate,
            cardMonsters.ComboRate, cardMonsters.IgnoreComboRate, cardMonsters.ComboDamageRate, cardMonsters.ComboResistanceRate,
            cardMonsters.StunRate, cardMonsters.IgnoreStunRate,
            cardMonsters.ReflectionRate, cardMonsters.IgnoreReflectionRate, cardMonsters.ReflectionDamageRate, cardMonsters.ReflectionResistanceRate,
            cardMonsters.Mana, cardMonsters.ManaRegenerationRate,
            cardMonsters.DamageToDifferentFactionRate, cardMonsters.ResistanceToDifferentFactionRate,
            cardMonsters.DamageToSameFactionRate, cardMonsters.ResistanceToSameFactionRate,
            cardMonsters.NormalDamageRate, cardMonsters.NormalResistanceRate,
            cardMonsters.SkillDamageRate, cardMonsters.SkillResistanceRate
        );
        return cardMonsters;
    }
    public async Task<List<CardMonsters>> GetSkillsAsync(string user_id, List<CardMonsters> CardMonstersList)
    {
        foreach(CardMonsters cardMonster in CardMonstersList)
        {
            var skills = await UserSkillsService.Create().GetUserCardMonstersSkillsAsync(user_id, cardMonster.Id);
            skills = skills.Where(x => x.Position != 0).ToList();
            cardMonster.Skills = skills;
        }
        return CardMonstersList;
    }
    public async Task<List<CardMonsters>> GetUserCardMonstersAsync(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<CardMonsters> list = await _userCardMonstersRepository.GetUserCardMonstersAsync(user_id, type, pageSize, offset, rare);
        list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = await GetFinalPowerAsync(user_id, list);
        list = await GetAllEquipmentPowerAsync(user_id, list);
        list = await GetAllRankPowerAsync(user_id, list);
        list = await GetAllMasterPowerAsync(user_id, list);
        list = await GetAllAnimeStatsPowerAsync(user_id, list);
        list = await GetScienceFictionPowerAsync(user_id, list);
        list = await GetSkillsAsync(user_id, list);
        return list;
    }

    public async Task<List<CardMonsters>> GetUserCardMonstersTeamAsync(string user_id, string teamId, string position)
    {
        List<CardMonsters> list = await _userCardMonstersRepository.GetUserCardMonstersTeamAsync(user_id, teamId, position);
        list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = await GetFinalPowerAsync(user_id, list);
        list = await GetAllEquipmentPowerAsync(user_id, list);
        list = await GetAllRankPowerAsync(user_id, list);
        list = await GetAllMasterPowerAsync(user_id, list);
        list = await GetAllAnimeStatsPowerAsync(user_id, list);
        list = await GetScienceFictionPowerAsync(user_id, list);
        list = await GetSkillsAsync(user_id, list);
        return list;
    }

    public async Task<List<CardMonsters>> GetUserCardMonstersTeamWithoutPositionAsync(string user_id, string teamId)
    {
        List<CardMonsters> list = await _userCardMonstersRepository.GetUserCardMonstersTeamWithoutPositionAsync(user_id, teamId);
        list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = await GetFinalPowerAsync(user_id, list);
        list = await GetAllEquipmentPowerAsync(user_id, list);
        list = await GetAllRankPowerAsync(user_id, list);
        list = await GetAllMasterPowerAsync(user_id, list);
        list = await GetAllAnimeStatsPowerAsync(user_id, list);
        list = await GetScienceFictionPowerAsync(user_id, list);
        list = await GetSkillsAsync(user_id, list);
        return list;
    }

    public async Task<Dictionary<string, int>> GetUniqueCardMonstersTypesTeamAsync(string teamId)
    {
        return await _userCardMonstersRepository.GetUniqueCardMonstersTypesTeamAsync(teamId);
    }

    public async Task<int> GetUserCardMonstersCountAsync(string user_id, string type, string rare)
    {
        return await _userCardMonstersRepository.GetUserCardMonstersCountAsync(user_id, type, rare);
    }

    public async Task<int> GetUserCardMonstersTeamsPositionCountAsync(string user_id, string team_id, string position)
    {
        return await _userCardMonstersRepository.GetUserCardMonstersTeamsPositionCountAsync(user_id, team_id, position);
    }

    public async Task<int> GetUserCardMonstersTeamsCountAsync(string user_id, string team_id)
    {
        return await _userCardMonstersRepository.GetUserCardMonstersTeamsCountAsync(user_id, team_id);
    }

    public async Task<bool> InsertUserCardMonsterAsync(CardMonsters CardMonsters)
    {
        return await _userCardMonstersRepository.InsertUserCardMonsterAsync(CardMonsters);
    }

    public async Task<bool> UpdateCardMonsterLevelAsync(CardMonsters cardMonsters, int cardLevel)
    {
        return await _userCardMonstersRepository.UpdateCardMonsterLevelAsync(cardMonsters, cardLevel);
    }

    public async Task<bool> UpdateCardMonsterBreakthroughAsync(CardMonsters cardMonsters, int star, double quantity)
    {
        return await _userCardMonstersRepository.UpdateCardMonsterBreakthroughAsync(cardMonsters, star, quantity);
    }

    public async Task<bool> UpdateTeamCardMonsterAsync(string team_id, string position, string card_id)
    {
        return await _userCardMonstersRepository.UpdateTeamCardMonsterAsync(team_id, position, card_id);
    }

    public async Task<CardMonsters> GetUserCardMonsterByIdAsync(string user_id, string Id)
    {
        CardMonsters cardMonster = await _userCardMonstersRepository.GetUserCardMonsterByIdAsync(user_id, Id);
        if (cardMonster == null) return null;

        // Bọc vào list để tái sử dụng logic
        List<CardMonsters> list = new List<CardMonsters> { cardMonster };

        list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = await GetFinalPowerAsync(user_id, list);
        list = await GetAllEquipmentPowerAsync(user_id, list);
        list = await GetAllRankPowerAsync(user_id, list);
        list = await GetAllMasterPowerAsync(user_id, list);
        list = await GetAllAnimeStatsPowerAsync(user_id, list);
        list = await GetScienceFictionPowerAsync(user_id, list);
        list = await GetSkillsAsync(user_id, list);
        return list.FirstOrDefault();
    }

    public async Task<List<CardMonsters>> GetAllUserCardMonstersInTeamAsync(string user_id)
    {
        List<CardMonsters> list = await _userCardMonstersRepository.GetAllUserCardMonstersInTeamAsync(user_id);
        list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = await GetFinalPowerAsync(user_id, list);
        list = await GetAllEquipmentPowerAsync(user_id, list);
        list = await GetAllRankPowerAsync(user_id, list);
        list = await GetAllMasterPowerAsync(user_id, list);
        list = await GetAllAnimeStatsPowerAsync(user_id, list);
        list = await GetScienceFictionPowerAsync(user_id, list);
        list = await GetSkillsAsync(user_id, list);
        return list;
    }
}

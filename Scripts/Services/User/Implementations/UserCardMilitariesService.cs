using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserCardMilitariesService : IUserCardMilitariesService
{
     private static UserCardMilitariesService _instance;
    private readonly IUserCardMilitariesRepository _userCardMilitariesRepository;

    public UserCardMilitariesService(IUserCardMilitariesRepository userCardMilitariesRepository)
    {
        _userCardMilitariesRepository = userCardMilitariesRepository;
    }

    public static UserCardMilitariesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardMilitariesService(new UserCardMilitariesRepository());
        }
        return _instance;
    }

    public async Task<List<CardMilitaries>> GetAllEquipmentPowerAsync(string user_id, List<CardMilitaries> CardMilitaryList)
    {
        foreach (var c in CardMilitaryList)
        {
            Equipments equipments = await UserEquipmentsService.Create().GetAllEquipmentsByCardMilitaryIdAsync(user_id, c.Id);
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
        return CardMilitaryList;
    }
    public async Task<List<CardMilitaries>> GetAllRankPowerAsync(string user_id, List<CardMilitaries> CardMilitaryList)
    {
        foreach (var c in CardMilitaryList)
        {
            Rank rank = await UserCardMilitariesRankService.Create().GetSumCardMilitariesRankAsync(user_id, c.Id);
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
        return CardMilitaryList;
    }
    public async Task<List<CardMilitaries>> GetAllMasterPowerAsync(string user_id, List<CardMilitaries> CardMilitaryList)
    {
        foreach (var c in CardMilitaryList)
        {
            Master master = await UserCardMilitariesMasterService.Create().GetSumCardMilitariesMasterAsync(user_id, c.Id);
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
        return CardMilitaryList;
    }
    public async Task<List<CardMilitaries>> GetAllSpiritBeastPowerAsync(string user_id, List<CardMilitaries> cardMilitaries)
    {
        foreach (var c in cardMilitaries)
        {
            SpiritBeasts spiritBeast = await UserSpiritBeastsService.Create().GetUserCardMilitarySpiritBeastAsync(user_id, c);
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
        return cardMilitaries;
    }
    
    
    public async Task<List<CardMilitaries>> GetSkillsAsync(string user_id, List<CardMilitaries> CardMilitaryList)
    {
        foreach(CardMilitaries cardMilitary in CardMilitaryList)
        {
            var skills = await UserSkillsService.Create().GetUserCardMilitariesSkillsAsync(user_id, cardMilitary.Id);
            skills = skills.Where(x => x.Position != 0).ToList();
            cardMilitary.Skills = skills;
        }
        return CardMilitaryList;
    }
    public async Task<List<CardMilitaries>> GetUserCardMilitariesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardMilitaries> list = await _userCardMilitariesRepository.GetUserCardMilitariesAsync(user_id, search, type, pageSize, offset, rare);
        
        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(user_id);
        var scienceFictionTask = ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        var researchTask = ResearchsService.Create().GetSumResearchsAsync(user_id);
        var archiveTask = ArchivesService.Create().GetSumArchivesAsync(user_id);
        var universeTask = UniversesService.Create().GetSumUniversesAsync(user_id);
        var hiinTask = HIINsService.Create().GetSumHIINsAsync(user_id);
        var sswnTask = SSWNsService.Create().GetSumSSWNsAsync(user_id);
        var hitnTask = HITNsService.Create().GetSumHITNsAsync(user_id);
        var hihnTask = HIHNsService.Create().GetSumHIHNsAsync(user_id);
        var hienTask = HIENsService.Create().GetSumHIENsAsync(user_id);
        var hicaTask = HICAsService.Create().GetSumHICAsAsync(user_id);
        var hirnTask = HIRNsService.Create().GetSumHIRNsAsync(user_id);
        var hidcTask = HIDCsService.Create().GetSumHIDCsAsync(user_id);
        var hicbTask = HICBsService.Create().GetSumHICBsAsync(user_id);
        var hisnTask = HISNsService.Create().GetSumHISNsAsync(user_id);
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

        await Task.WhenAll(powerManagerTask, scienceFictionTask, researchTask, archiveTask,
        universeTask, hiinTask, sswnTask, hitnTask, hihnTask, hienTask, hicaTask, hirnTask,
        hidcTask, hicbTask, hisnTask, animeStatsTask);

        var powerManagerData = await powerManagerTask;
        var scienceFictionData = await scienceFictionTask;
        var researchData = await researchTask;
        var archiveData = await archiveTask;
        var universeData = await universeTask;
        var hiinData = await hiinTask;
        var sswnData = await sswnTask;
        var hitnData = await hitnTask;
        var hihnData = await hihnTask;
        var hienData = await hienTask;
        var hicaData = await hicaTask;
        var hirnData = await hirnTask;
        var hidcData = await hidcTask;
        var hicbData = await hicbTask;
        var hisnData = await hisnTask;
        var animeStatsData = await animeStatsTask;

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        // list = await GetSkillsAsync(user_id, list);
        foreach(var card in list)
        {
            card.ApplyPowerStats(powerManagerData);
            card.ApplyScienceFictionStats(scienceFictionData);
            card.ApplyResearchStats(researchData);
            card.ApplyArchiveStats(archiveData);
            card.ApplyUniverseStats(universeData);
            card.ApplyHIINStats(hiinData);
            card.ApplySSWNStats(sswnData);
            card.ApplyHITNStats(hitnData);
            card.ApplyHIHNStats(hihnData);
            card.ApplyHIENStats(hienData);
            card.ApplyHICAStats(hicaData);
            card.ApplyHIRNStats(hirnData);
            card.ApplyHIDCStats(hidcData);
            card.ApplyHICBStats(hicbData);
            card.ApplyHISNStats(hisnData);
            card.ApplyAllAnimeStats(animeStatsData);
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<CardMilitaries>> GetUserCardMilitariesTeamAsync(string user_id, string teamId, string position)
    {
        List<CardMilitaries> list = await _userCardMilitariesRepository.GetUserCardMilitariesTeamAsync(user_id, teamId, position);
        
        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(user_id);
        var scienceFictionTask = ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        var researchTask = ResearchsService.Create().GetSumResearchsAsync(user_id);
        var archiveTask = ArchivesService.Create().GetSumArchivesAsync(user_id);
        var universeTask = UniversesService.Create().GetSumUniversesAsync(user_id);
        var hiinTask = HIINsService.Create().GetSumHIINsAsync(user_id);
        var sswnTask = SSWNsService.Create().GetSumSSWNsAsync(user_id);
        var hitnTask = HITNsService.Create().GetSumHITNsAsync(user_id);
        var hihnTask = HIHNsService.Create().GetSumHIHNsAsync(user_id);
        var hienTask = HIENsService.Create().GetSumHIENsAsync(user_id);
        var hicaTask = HICAsService.Create().GetSumHICAsAsync(user_id);
        var hirnTask = HIRNsService.Create().GetSumHIRNsAsync(user_id);
        var hidcTask = HIDCsService.Create().GetSumHIDCsAsync(user_id);
        var hicbTask = HICBsService.Create().GetSumHICBsAsync(user_id);
        var hisnTask = HISNsService.Create().GetSumHISNsAsync(user_id);
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

        await Task.WhenAll(powerManagerTask, scienceFictionTask, researchTask, archiveTask,
        universeTask, hiinTask, sswnTask, hitnTask, hihnTask, hienTask, hicaTask, hirnTask,
        hidcTask, hicbTask, hisnTask, animeStatsTask);

        var powerManagerData = await powerManagerTask;
        var scienceFictionData = await scienceFictionTask;
        var researchData = await researchTask;
        var archiveData = await archiveTask;
        var universeData = await universeTask;
        var hiinData = await hiinTask;
        var sswnData = await sswnTask;
        var hitnData = await hitnTask;
        var hihnData = await hihnTask;
        var hienData = await hienTask;
        var hicaData = await hicaTask;
        var hirnData = await hirnTask;
        var hidcData = await hidcTask;
        var hicbData = await hicbTask;
        var hisnData = await hisnTask;
        var animeStatsData = await animeStatsTask;

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        // list = await GetSkillsAsync(user_id, list);
        foreach(var card in list)
        {
            card.ApplyPowerStats(powerManagerData);
            card.ApplyScienceFictionStats(scienceFictionData);
            card.ApplyResearchStats(researchData);
            card.ApplyArchiveStats(archiveData);
            card.ApplyUniverseStats(universeData);
            card.ApplyHIINStats(hiinData);
            card.ApplySSWNStats(sswnData);
            card.ApplyHITNStats(hitnData);
            card.ApplyHIHNStats(hihnData);
            card.ApplyHIENStats(hienData);
            card.ApplyHICAStats(hicaData);
            card.ApplyHIRNStats(hirnData);
            card.ApplyHIDCStats(hidcData);
            card.ApplyHICBStats(hicbData);
            card.ApplyHISNStats(hisnData);
            card.ApplyAllAnimeStats(animeStatsData);
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<CardMilitaries>> GetUserCardMilitariesTeamWithoutPositionAsync(string user_id, string teamId)
    {
        List<CardMilitaries> list = await _userCardMilitariesRepository.GetUserCardMilitariesTeamWithoutPositionAsync(user_id, teamId);
        
        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(user_id);
        var scienceFictionTask = ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        var researchTask = ResearchsService.Create().GetSumResearchsAsync(user_id);
        var archiveTask = ArchivesService.Create().GetSumArchivesAsync(user_id);
        var universeTask = UniversesService.Create().GetSumUniversesAsync(user_id);
        var hiinTask = HIINsService.Create().GetSumHIINsAsync(user_id);
        var sswnTask = SSWNsService.Create().GetSumSSWNsAsync(user_id);
        var hitnTask = HITNsService.Create().GetSumHITNsAsync(user_id);
        var hihnTask = HIHNsService.Create().GetSumHIHNsAsync(user_id);
        var hienTask = HIENsService.Create().GetSumHIENsAsync(user_id);
        var hicaTask = HICAsService.Create().GetSumHICAsAsync(user_id);
        var hirnTask = HIRNsService.Create().GetSumHIRNsAsync(user_id);
        var hidcTask = HIDCsService.Create().GetSumHIDCsAsync(user_id);
        var hicbTask = HICBsService.Create().GetSumHICBsAsync(user_id);
        var hisnTask = HISNsService.Create().GetSumHISNsAsync(user_id);
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

        await Task.WhenAll(powerManagerTask, scienceFictionTask, researchTask, archiveTask,
        universeTask, hiinTask, sswnTask, hitnTask, hihnTask, hienTask, hicaTask, hirnTask,
        hidcTask, hicbTask, hisnTask, animeStatsTask);

        var powerManagerData = await powerManagerTask;
        var scienceFictionData = await scienceFictionTask;
        var researchData = await researchTask;
        var archiveData = await archiveTask;
        var universeData = await universeTask;
        var hiinData = await hiinTask;
        var sswnData = await sswnTask;
        var hitnData = await hitnTask;
        var hihnData = await hihnTask;
        var hienData = await hienTask;
        var hicaData = await hicaTask;
        var hirnData = await hirnTask;
        var hidcData = await hidcTask;
        var hicbData = await hicbTask;
        var hisnData = await hisnTask;
        var animeStatsData = await animeStatsTask;

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        // list = await GetSkillsAsync(user_id, list);
        foreach(var card in list)
        {
            card.ApplyPowerStats(powerManagerData);
            card.ApplyScienceFictionStats(scienceFictionData);
            card.ApplyResearchStats(researchData);
            card.ApplyArchiveStats(archiveData);
            card.ApplyUniverseStats(universeData);
            card.ApplyHIINStats(hiinData);
            card.ApplySSWNStats(sswnData);
            card.ApplyHITNStats(hitnData);
            card.ApplyHIHNStats(hihnData);
            card.ApplyHIENStats(hienData);
            card.ApplyHICAStats(hicaData);
            card.ApplyHIRNStats(hirnData);
            card.ApplyHIDCStats(hidcData);
            card.ApplyHICBStats(hicbData);
            card.ApplyHISNStats(hisnData);
            card.ApplyAllAnimeStats(animeStatsData);
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<Dictionary<string, int>> GetUniqueCardMilitariesTypesTeamAsync(string teamId)
    {
        return await _userCardMilitariesRepository.GetUniqueCardMilitariesTypesTeamAsync(teamId);
    }

    public async Task<bool> UpdateTeamCardMilitaryAsync(string team_id, string position, string card_id)
    {
        return await _userCardMilitariesRepository.UpdateTeamCardMilitaryAsync(team_id, position, card_id);
    }

    public async Task<int> GetUserCardMilitariesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userCardMilitariesRepository.GetUserCardMilitariesCountAsync(user_id, search, type, rare);
    }

    public async Task<int> GetUserCardMilitariesTeamsPositionCountAsync(string user_id, string team_id, string position)
    {
        return await _userCardMilitariesRepository.GetUserCardMilitariesTeamsPositionCountAsync(user_id, team_id, position);
    }

    public async Task<int> GetUserCardMilitariesTeamsCountAsync(string user_id, string team_id)
    {
        return await _userCardMilitariesRepository.GetUserCardMilitariesTeamsCountAsync(user_id, team_id);
    }

    public async Task<bool> InsertUserCardMilitaryAsync(CardMilitaries cardMilitary)
    {
        return await _userCardMilitariesRepository.InsertUserCardMilitaryAsync(cardMilitary);
    }

    public async Task<bool> UpdateCardMilitaryLevelAsync(CardMilitaries cardMilitary, int level)
    {
        return await _userCardMilitariesRepository.UpdateCardMilitaryLevelAsync(cardMilitary, level);
    }

    public async Task<bool> UpdateCardMilitaryBreakthroughAsync(CardMilitaries cardMilitary, int star, double quantity)
    {
        return await _userCardMilitariesRepository.UpdateCardMilitaryBreakthroughAsync(cardMilitary, star, quantity);
    }

    public async Task<CardMilitaries> GetUserCardMilitaryByIdAsync(string user_id, string Id)
    {
        CardMilitaries cardMilitary = await _userCardMilitariesRepository.GetUserCardMilitaryByIdAsync(user_id, Id);
        if (cardMilitary == null) return null;

        // Bọc vào list để tái sử dụng logic
        List<CardMilitaries> list = new List<CardMilitaries> { cardMilitary };

        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(user_id);
        var scienceFictionTask = ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        var researchTask = ResearchsService.Create().GetSumResearchsAsync(user_id);
        var archiveTask = ArchivesService.Create().GetSumArchivesAsync(user_id);
        var universeTask = UniversesService.Create().GetSumUniversesAsync(user_id);
        var hiinTask = HIINsService.Create().GetSumHIINsAsync(user_id);
        var sswnTask = SSWNsService.Create().GetSumSSWNsAsync(user_id);
        var hitnTask = HITNsService.Create().GetSumHITNsAsync(user_id);
        var hihnTask = HIHNsService.Create().GetSumHIHNsAsync(user_id);
        var hienTask = HIENsService.Create().GetSumHIENsAsync(user_id);
        var hicaTask = HICAsService.Create().GetSumHICAsAsync(user_id);
        var hirnTask = HIRNsService.Create().GetSumHIRNsAsync(user_id);
        var hidcTask = HIDCsService.Create().GetSumHIDCsAsync(user_id);
        var hicbTask = HICBsService.Create().GetSumHICBsAsync(user_id);
        var hisnTask = HISNsService.Create().GetSumHISNsAsync(user_id);
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

        await Task.WhenAll(powerManagerTask, scienceFictionTask, researchTask, archiveTask,
        universeTask, hiinTask, sswnTask, hitnTask, hihnTask, hienTask, hicaTask, hirnTask,
        hidcTask, hicbTask, hisnTask, animeStatsTask);

        var powerManagerData = await powerManagerTask;
        var scienceFictionData = await scienceFictionTask;
        var researchData = await researchTask;
        var archiveData = await archiveTask;
        var universeData = await universeTask;
        var hiinData = await hiinTask;
        var sswnData = await sswnTask;
        var hitnData = await hitnTask;
        var hihnData = await hihnTask;
        var hienData = await hienTask;
        var hicaData = await hicaTask;
        var hirnData = await hirnTask;
        var hidcData = await hidcTask;
        var hicbData = await hicbTask;
        var hisnData = await hisnTask;
        var animeStatsData = await animeStatsTask;

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        // list = await GetSkillsAsync(user_id, list);
        foreach(var card in list)
        {
            card.ApplyPowerStats(powerManagerData);
            card.ApplyScienceFictionStats(scienceFictionData);
            card.ApplyResearchStats(researchData);
            card.ApplyArchiveStats(archiveData);
            card.ApplyUniverseStats(universeData);
            card.ApplyHIINStats(hiinData);
            card.ApplySSWNStats(sswnData);
            card.ApplyHITNStats(hitnData);
            card.ApplyHIHNStats(hihnData);
            card.ApplyHIENStats(hienData);
            card.ApplyHICAStats(hicaData);
            card.ApplyHIRNStats(hirnData);
            card.ApplyHIDCStats(hidcData);
            card.ApplyHICBStats(hicbData);
            card.ApplyHISNStats(hisnData);
            card.ApplyAllAnimeStats(animeStatsData);
            card.RecalculatePower();
        }
        return list.FirstOrDefault();
    }

    public async Task<List<CardMilitaries>> GetAllUserCardMilitariesInTeamAsync(string user_id)
    {
        List<CardMilitaries> list = await _userCardMilitariesRepository.GetAllUserCardMilitariesInTeamAsync(user_id);
        
        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(user_id);
        var scienceFictionTask = ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        var researchTask = ResearchsService.Create().GetSumResearchsAsync(user_id);
        var archiveTask = ArchivesService.Create().GetSumArchivesAsync(user_id);
        var universeTask = UniversesService.Create().GetSumUniversesAsync(user_id);
        var hiinTask = HIINsService.Create().GetSumHIINsAsync(user_id);
        var sswnTask = SSWNsService.Create().GetSumSSWNsAsync(user_id);
        var hitnTask = HITNsService.Create().GetSumHITNsAsync(user_id);
        var hihnTask = HIHNsService.Create().GetSumHIHNsAsync(user_id);
        var hienTask = HIENsService.Create().GetSumHIENsAsync(user_id);
        var hicaTask = HICAsService.Create().GetSumHICAsAsync(user_id);
        var hirnTask = HIRNsService.Create().GetSumHIRNsAsync(user_id);
        var hidcTask = HIDCsService.Create().GetSumHIDCsAsync(user_id);
        var hicbTask = HICBsService.Create().GetSumHICBsAsync(user_id);
        var hisnTask = HISNsService.Create().GetSumHISNsAsync(user_id);
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

        await Task.WhenAll(powerManagerTask, scienceFictionTask, researchTask, archiveTask,
        universeTask, hiinTask, sswnTask, hitnTask, hihnTask, hienTask, hicaTask, hirnTask,
        hidcTask, hicbTask, hisnTask, animeStatsTask);

        var powerManagerData = await powerManagerTask;
        var scienceFictionData = await scienceFictionTask;
        var researchData = await researchTask;
        var archiveData = await archiveTask;
        var universeData = await universeTask;
        var hiinData = await hiinTask;
        var sswnData = await sswnTask;
        var hitnData = await hitnTask;
        var hihnData = await hihnTask;
        var hienData = await hienTask;
        var hicaData = await hicaTask;
        var hirnData = await hirnTask;
        var hidcData = await hidcTask;
        var hicbData = await hicbTask;
        var hisnData = await hisnTask;
        var animeStatsData = await animeStatsTask;

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        // list = await GetSkillsAsync(user_id, list);
        foreach(var card in list)
        {
            card.ApplyPowerStats(powerManagerData);
            card.ApplyScienceFictionStats(scienceFictionData);
            card.ApplyResearchStats(researchData);
            card.ApplyArchiveStats(archiveData);
            card.ApplyUniverseStats(universeData);
            card.ApplyHIINStats(hiinData);
            card.ApplySSWNStats(sswnData);
            card.ApplyHITNStats(hitnData);
            card.ApplyHIHNStats(hihnData);
            card.ApplyHIENStats(hienData);
            card.ApplyHICAStats(hicaData);
            card.ApplyHIRNStats(hirnData);
            card.ApplyHIDCStats(hidcData);
            card.ApplyHICBStats(hicbData);
            card.ApplyHISNStats(hisnData);
            card.ApplyAllAnimeStats(animeStatsData);
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<bool> InsertOrUpdateUserCardMilitariesBatchAsync(List<CardMilitaries> cardMilitaries)
    {
        return await _userCardMilitariesRepository.InsertOrUpdateUserCardMilitariesBatchAsync(cardMilitaries);
    }
}

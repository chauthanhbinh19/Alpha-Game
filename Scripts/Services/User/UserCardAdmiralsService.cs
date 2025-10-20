using System.Collections.Generic;
using System.Linq;

public class UserCardAdmiralsService : IUserCardAdmiralsService
{
    private IUserCardAdmiralsRepository _userCardAdmiralsRepository;

    public UserCardAdmiralsService(IUserCardAdmiralsRepository userCardAdmiralsRepository)
    {
        _userCardAdmiralsRepository = userCardAdmiralsRepository;
    }

    public static UserCardAdmiralsService Create()
    {
        return new UserCardAdmiralsService(new UserCardAdmiralsRepository());
    }

    public List<CardAdmirals> GetFinalPower(string user_id, List<CardAdmirals> CardAdmiralsList)
    {
        IPowerManagerRepository powerManagerRepository = new PowerManagerRepository();
        PowerManagerService powerManagerService = new PowerManagerService(powerManagerRepository);
        PowerManager powerManager = powerManagerService.GetUserStats(user_id);
        foreach (var c in CardAdmiralsList)
        {
            CardAdmirals card = _userCardAdmiralsRepository.GetUserCardAdmiralsById(user_id, c.Id);
            c.Health = c.Health + powerManager.Health + card.Health * powerManager.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + powerManager.PhysicalAttack + card.PhysicalAttack * powerManager.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + powerManager.PhysicalDefense + card.PhysicalDefense * powerManager.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + powerManager.MagicalAttack + card.MagicalAttack * powerManager.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + powerManager.MagicalDefense + card.MagicalDefense * powerManager.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + powerManager.ChemicalAttack + card.ChemicalAttack * powerManager.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + powerManager.ChemicalDefense + card.ChemicalDefense * powerManager.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + powerManager.AtomicAttack + card.AtomicAttack * powerManager.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + powerManager.AtomicDefense + card.AtomicDefense * powerManager.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + powerManager.MentalAttack + card.MentalAttack * powerManager.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + powerManager.MentalDefense + card.MentalDefense * powerManager.PercentAllMentalDefense / 100;
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
            c.IgnoreReflectionRate = c.IgnoreReflectionRate + powerManager.IgnoreReflectionRate;
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
        return CardAdmiralsList;
    }
    public List<CardAdmirals> GetScienceFictionPower(string user_id, List<CardAdmirals> CardAdmiralsList)
    {
        ScienceFiction scienceFiction = ScienceFictionService.Create().GetSumScienceFiction(user_id);
        foreach (var c in CardAdmiralsList)
        {
            CardAdmirals card = _userCardAdmiralsRepository.GetUserCardAdmiralsById(user_id, c.Id);
            c.Health = c.Health + scienceFiction.Health + card.Health * scienceFiction.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + scienceFiction.PhysicalAttack + card.PhysicalAttack * scienceFiction.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + scienceFiction.PhysicalDefense + card.PhysicalDefense * scienceFiction.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + scienceFiction.MagicalAttack + card.MagicalAttack * scienceFiction.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + scienceFiction.MagicalDefense + card.MagicalDefense * scienceFiction.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + scienceFiction.ChemicalAttack + card.ChemicalAttack * scienceFiction.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + scienceFiction.ChemicalDefense + card.ChemicalDefense * scienceFiction.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + scienceFiction.AtomicAttack + card.AtomicAttack * scienceFiction.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + scienceFiction.AtomicDefense + card.AtomicDefense * scienceFiction.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + scienceFiction.MentalAttack + card.MentalAttack * scienceFiction.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + scienceFiction.MentalDefense + card.MentalDefense * scienceFiction.PercentAllMentalDefense / 100;
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
            c.IgnoreReflectionRate = c.IgnoreReflectionRate + scienceFiction.IgnoreReflectionRate;
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
        return CardAdmiralsList;
    }
    public List<CardAdmirals> GetAllEquipmentPower(string user_id, List<CardAdmirals> CardAdmiralsList)
    {
        IUserEquipmentsRepository userEquipmentsRepository = new UserEquipmentsRepository();
        UserEquipmentsService userEquipmentsService = new UserEquipmentsService(userEquipmentsRepository);
        foreach (var c in CardAdmiralsList)
        {
            Equipments equipments = userEquipmentsService.GetAllEquipmentsByCardAdmiralsId(user_id, c.Id);
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
        return CardAdmiralsList;
    }
    public List<CardAdmirals> GetAllRankPower(string user_id, List<CardAdmirals> CardAdmiralsList)
    {
        IUserCardAdmiralsRankRepository userCardAdmiralsRankRepository = new UserCardAdmiralsRankRepository();
        UserCardAdmiralsRankService userCardAdmiralsRankService = new UserCardAdmiralsRankService(userCardAdmiralsRankRepository);
        foreach (var c in CardAdmiralsList)
        {
            CardAdmirals card = _userCardAdmiralsRepository.GetUserCardAdmiralsById(user_id, c.Id);
            Rank rank = userCardAdmiralsRankService.GetSumCardAdmiralsRank(user_id, c.Id);
            c.Health = c.Health + rank.Health + card.Health * rank.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + rank.PhysicalAttack + card.PhysicalAttack * rank.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + rank.PhysicalDefense + card.PhysicalDefense * rank.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + rank.MagicalAttack + card.MagicalAttack * rank.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + rank.MagicalDefense + card.MagicalDefense * rank.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + rank.ChemicalAttack + card.ChemicalAttack * rank.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + rank.ChemicalDefense + card.ChemicalDefense * rank.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + rank.AtomicAttack + card.AtomicAttack * rank.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + rank.AtomicDefense + card.AtomicDefense * rank.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + rank.MentalAttack + card.MentalAttack * rank.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + rank.MentalDefense + card.MentalDefense * rank.PercentAllMentalDefense / 100;
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
        return CardAdmiralsList;
    }
    public List<CardAdmirals> GetAllMasterPower(string user_id, List<CardAdmirals> CardAdmiralsList)
    {
        IUserCardAdmiralsMasterRepository userCardAdmiralsMasterRepository = new UserCardAdmiralsMasterRepository();
        UserCardAdmiralsMasterService userCardAdmiralsMasterService = new UserCardAdmiralsMasterService(userCardAdmiralsMasterRepository);
        foreach (var c in CardAdmiralsList)
        {
            CardAdmirals card = _userCardAdmiralsRepository.GetUserCardAdmiralsById(user_id, c.Id);
            Master master = userCardAdmiralsMasterService.GetSumCardAdmiralsMaster(user_id, c.Id);
            c.Health = c.Health + master.Health + card.Health * master.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + master.PhysicalAttack + card.PhysicalAttack * master.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + master.PhysicalDefense + card.PhysicalDefense * master.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + master.MagicalAttack + card.MagicalAttack * master.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + master.MagicalDefense + card.MagicalDefense * master.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + master.ChemicalAttack + card.ChemicalAttack * master.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + master.ChemicalDefense + card.ChemicalDefense * master.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + master.AtomicAttack + card.AtomicAttack * master.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + master.AtomicDefense + card.AtomicDefense * master.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + master.MentalAttack + card.MentalAttack * master.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + master.MentalDefense + card.MentalDefense * master.PercentAllMentalDefense / 100;
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
        return CardAdmiralsList;
    }
    public List<CardAdmirals> GetAllAnimeStatsPower(string user_id, List<CardAdmirals> cardAdmirals)
    {
        IAnimeStatsRepository animeStatsRepository = new AnimeStatsRepository();
        AnimeStatsService animeStatsService = new AnimeStatsService(animeStatsRepository);
        foreach (var c in cardAdmirals)
        {
            CardAdmirals card = _userCardAdmiralsRepository.GetUserCardAdmiralsById(user_id, c.Id);
            AnimeStats animeStats = animeStatsService.GetSumAnimeStats(user_id);
            c.Health = c.Health + animeStats.Health + card.Health * animeStats.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + animeStats.PhysicalAttack + card.PhysicalAttack * animeStats.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + animeStats.PhysicalDefense + card.PhysicalDefense * animeStats.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + animeStats.MagicalAttack + card.MagicalAttack * animeStats.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + animeStats.MagicalDefense + card.MagicalDefense * animeStats.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + animeStats.ChemicalAttack + card.ChemicalAttack * animeStats.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + animeStats.ChemicalDefense + card.ChemicalDefense * animeStats.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + animeStats.AtomicAttack + card.AtomicAttack * animeStats.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + animeStats.AtomicDefense + card.AtomicDefense * animeStats.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + animeStats.MentalAttack + card.MentalAttack * animeStats.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + animeStats.MentalDefense + card.MentalDefense * animeStats.PercentAllMentalDefense / 100;
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
        return cardAdmirals;
    }
    public List<CardAdmirals> GetAllSpiritBeastPower(string user_id, List<CardAdmirals> cardAdmirals)
    {
        IUserSpiritBeastRepository userSpiritBeastRepository = new UserSpiritBeastRepository();
        UserSpiritBeastService userSpiritBeastService = new UserSpiritBeastService(userSpiritBeastRepository);
        foreach (var c in cardAdmirals)
        {
            CardAdmirals card = _userCardAdmiralsRepository.GetUserCardAdmiralsById(user_id, c.Id);
            SpiritBeasts spiritBeast = userSpiritBeastService.GetUserCardAdmiralsSpiritBeast(user_id, c);
            if (spiritBeast != null)
            {
                c.Health = c.Health + spiritBeast.Health + card.Health * spiritBeast.PercentAllHealth / 100;
                c.PhysicalAttack = c.PhysicalAttack + spiritBeast.PhysicalAttack + card.PhysicalAttack * spiritBeast.PercentAllPhysicalAttack / 100;
                c.PhysicalDefense = c.PhysicalDefense + spiritBeast.PhysicalDefense + card.PhysicalDefense * spiritBeast.PercentAllPhysicalDefense / 100;
                c.MagicalAttack = c.MagicalAttack + spiritBeast.MagicalAttack + card.MagicalAttack * spiritBeast.PercentAllMagicalAttack / 100;
                c.MagicalDefense = c.MagicalDefense + spiritBeast.MagicalDefense + card.MagicalDefense * spiritBeast.PercentAllMagicalDefense / 100;
                c.ChemicalAttack = c.ChemicalAttack + spiritBeast.ChemicalAttack + card.ChemicalAttack * spiritBeast.PercentAllChemicalAttack / 100;
                c.ChemicalDefense = c.ChemicalDefense + spiritBeast.ChemicalDefense + card.ChemicalDefense * spiritBeast.PercentAllChemicalDefense / 100;
                c.AtomicAttack = c.AtomicAttack + spiritBeast.AtomicAttack + card.AtomicAttack * spiritBeast.PercentAllAtomicAttack / 100;
                c.AtomicDefense = c.AtomicDefense + spiritBeast.AtomicDefense + card.AtomicDefense * spiritBeast.PercentAllAtomicDefense / 100;
                c.MentalAttack = c.MentalAttack + spiritBeast.MentalAttack + card.MentalAttack * spiritBeast.PercentAllMentalAttack / 100;
                c.MentalDefense = c.MentalDefense + spiritBeast.MentalDefense + card.MentalDefense * spiritBeast.PercentAllMentalDefense / 100;
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
        return cardAdmirals;
    }
    // public List<CardAdmirals> GetMasterBoardPower(string user_id, List<CardAdmirals> CardAdmiralsList)
    // {
    //     IUserMasterBoardRepository userMasterBoardRepository = new UserMasterBoardRepository();
    //     UserMasterBoardService userMasterBoardService = new UserMasterBoardService(userMasterBoardRepository);
    //     MasterBoard masterBoard = userMasterBoardService.GetUserMasterBoard(user_id);
    //     foreach (var c in CardAdmiralsList)
    //     {
    //         CardAdmirals card = _userCardAdmiralsRepository.GetUserCardAdmiralsById(user_id, c.id);
    //         c.health = c.health + masterBoard.health + card.health * masterBoard.percent_all_health / 100;
    //         c.physical_attack = c.physical_attack + masterBoard.physical_attack + card.physical_attack * masterBoard.percent_all_physical_attack / 100;
    //         c.physical_defense = c.physical_defense + masterBoard.physical_defense + card.physical_defense * masterBoard.percent_all_physical_defense / 100;
    //         c.magical_attack = c.magical_attack + masterBoard.magical_attack + card.magical_attack * masterBoard.percent_all_magical_attack / 100;
    //         c.magical_defense = c.magical_defense + masterBoard.magical_defense + card.magical_defense * masterBoard.percent_all_magical_defense / 100;
    //         c.chemical_attack = c.chemical_attack + masterBoard.chemical_attack + card.chemical_attack * masterBoard.percent_all_chemical_attack / 100;
    //         c.chemical_defense = c.chemical_defense + masterBoard.chemical_defense + card.chemical_defense * masterBoard.percent_all_chemical_defense / 100;
    //         c.atomic_attack = c.atomic_attack + masterBoard.atomic_attack + card.atomic_attack * masterBoard.percent_all_atomic_attack / 100;
    //         c.atomic_defense = c.atomic_defense + masterBoard.atomic_defense + card.atomic_defense * masterBoard.percent_all_atomic_defense / 100;
    //         c.mental_attack = c.mental_attack + masterBoard.mental_attack + card.mental_attack * masterBoard.percent_all_mental_attack / 100;
    //         c.mental_defense = c.mental_defense + masterBoard.mental_defense + card.mental_defense * masterBoard.percent_all_mental_defense / 100;
    //         c.speed = c.speed + masterBoard.speed;
    //         c.critical_damage_rate = c.critical_damage_rate + masterBoard.critical_damage_rate;
    //         c.critical_rate = c.critical_rate + masterBoard.critical_rate;
    //         c.critical_resistance_rate = c.critical_resistance_rate + masterBoard.critical_resistance_rate;
    //         c.ignore_critical_rate = c.ignore_critical_rate + masterBoard.ignore_critical_rate;
    //         c.penetration_rate = c.penetration_rate + masterBoard.penetration_rate;
    //         c.penetration_resistance_rate = c.penetration_resistance_rate + masterBoard.penetration_resistance_rate;
    //         c.evasion_rate = c.evasion_rate + masterBoard.evasion_rate;
    //         c.damage_absorption_rate = c.damage_absorption_rate + masterBoard.damage_absorption_rate;
    //         c.ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + masterBoard.ignore_damage_absorption_rate;
    //         c.absorbed_damage_rate = c.absorbed_damage_rate + masterBoard.absorbed_damage_rate;
    //         c.vitality_regeneration_rate = c.vitality_regeneration_rate + masterBoard.vitality_regeneration_rate;
    //         c.vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + masterBoard.vitality_regeneration_resistance_rate;
    //         c.accuracy_rate = c.accuracy_rate + masterBoard.accuracy_rate;
    //         c.lifesteal_rate = c.lifesteal_rate + masterBoard.lifesteal_rate;
    //         c.shield_strength = c.shield_strength + masterBoard.shield_strength;
    //         c.tenacity = c.tenacity + masterBoard.tenacity;
    //         c.resistance_rate = c.resistance_rate + masterBoard.resistance_rate;
    //         c.combo_rate = c.combo_rate + masterBoard.combo_rate;
    //         c.ignore_combo_rate = c.ignore_combo_rate + masterBoard.ignore_combo_rate;
    //         c.combo_damage_rate = c.combo_damage_rate + masterBoard.combo_damage_rate;
    //         c.combo_resistance_rate = c.combo_resistance_rate + masterBoard.combo_resistance_rate;
    //         c.stun_rate = c.stun_rate + masterBoard.stun_rate;
    //         c.ignore_stun_rate = c.ignore_stun_rate + masterBoard.ignore_stun_rate;
    //         c.reflection_rate = c.reflection_rate + masterBoard.reflection_rate;
    //         c.ignore_reflection_rate = c.ignore_reflection_rate + masterBoard.ignore_reflection_rate;
    //         c.reflection_damage_rate = c.reflection_damage_rate + masterBoard.reflection_damage_rate;
    //         c.reflection_resistance_rate = c.reflection_resistance_rate + masterBoard.reflection_resistance_rate;
    //         c.mana = c.mana + masterBoard.mana;
    //         c.mana_regeneration_rate = c.mana_regeneration_rate + masterBoard.mana_regeneration_rate;
    //         c.damage_to_different_faction_rate = c.damage_to_different_faction_rate + masterBoard.damage_to_different_faction_rate;
    //         c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + masterBoard.resistance_to_different_faction_rate;
    //         c.damage_to_same_faction_rate = c.damage_to_same_faction_rate + masterBoard.damage_to_same_faction_rate;
    //         c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + masterBoard.resistance_to_same_faction_rate;
    //         c.normal_damage_rate = c.normal_damage_rate + masterBoard.normal_damage_rate;
    //         c.normal_resistance_rate = c.normal_resistance_rate + masterBoard.normal_resistance_rate;
    //         c.skill_damage_rate = c.skill_damage_rate + masterBoard.skill_damage_rate;
    //         c.skill_resistance_rate = c.skill_resistance_rate + masterBoard.skill_resistance_rate;

    //         c.power = EvaluatePower.CalculatePower(
    //         c.health,
    //         c.physical_attack, c.physical_defense,
    //         c.magical_attack, c.magical_defense,
    //         c.chemical_attack, c.chemical_defense,
    //         c.atomic_attack, c.atomic_defense,
    //         c.mental_attack, c.mental_defense,
    //         c.speed,
    //         c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
    //         c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
    //         c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
    //         c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
    //         c.accuracy_rate, c.lifesteal_rate,
    //         c.shield_strength, c.tenacity, c.resistance_rate,
    //         c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
    //         c.stun_rate, c.ignore_stun_rate,
    //         c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
    //         c.mana, c.mana_regeneration_rate,
    //         c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
    //         c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
    //         c.normal_damage_rate, c.normal_resistance_rate,
    //         c.skill_damage_rate, c.skill_resistance_rate
    //     );
    //     }
    //     return CardAdmiralsList;
    // }
    public CardAdmirals GetNewLevelPower(CardAdmirals c, double coefficient)
    {
        ICardAdmiralsRepository _repository = new CardAdmiralsRepository();
        CardAdmiralsService _service = new CardAdmiralsService(_repository);
        CardAdmirals orginCard = _service.GetCardAdmiralsById(c.Id);
        CardAdmirals cardAdmirals = new CardAdmirals
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
        cardAdmirals.Power = EvaluatePower.CalculatePower(
            cardAdmirals.Health,
            cardAdmirals.PhysicalAttack, cardAdmirals.PhysicalDefense,
            cardAdmirals.MagicalAttack, cardAdmirals.MagicalDefense,
            cardAdmirals.ChemicalAttack, cardAdmirals.ChemicalDefense,
            cardAdmirals.AtomicAttack, cardAdmirals.AtomicDefense,
            cardAdmirals.MentalAttack, cardAdmirals.MentalDefense,
            cardAdmirals.Speed,
            cardAdmirals.CriticalDamageRate, cardAdmirals.CriticalRate, cardAdmirals.CriticalResistanceRate, cardAdmirals.IgnoreCriticalRate,
            cardAdmirals.PenetrationRate, cardAdmirals.PenetrationResistanceRate, cardAdmirals.EvasionRate,
            cardAdmirals.DamageAbsorptionRate, cardAdmirals.IgnoreDamageAbsorptionRate, cardAdmirals.AbsorbedDamageRate,
            cardAdmirals.VitalityRegenerationRate, cardAdmirals.VitalityRegenerationResistanceRate,
            cardAdmirals.AccuracyRate, cardAdmirals.LifestealRate,
            cardAdmirals.ShieldStrength, cardAdmirals.Tenacity, cardAdmirals.ResistanceRate,
            cardAdmirals.ComboRate, cardAdmirals.IgnoreComboRate, cardAdmirals.ComboDamageRate, cardAdmirals.ComboResistanceRate,
            cardAdmirals.StunRate, cardAdmirals.IgnoreStunRate,
            cardAdmirals.ReflectionRate, cardAdmirals.IgnoreReflectionRate, cardAdmirals.ReflectionDamageRate, cardAdmirals.ReflectionResistanceRate,
            cardAdmirals.Mana, cardAdmirals.ManaRegenerationRate,
            cardAdmirals.DamageToDifferentFactionRate, cardAdmirals.ResistanceToDifferentFactionRate,
            cardAdmirals.DamageToSameFactionRate, cardAdmirals.ResistanceToSameFactionRate,
            cardAdmirals.NormalDamageRate, cardAdmirals.NormalResistanceRate,
            cardAdmirals.SkillDamageRate, cardAdmirals.SkillResistanceRate
        );
        return cardAdmirals;
    }
    public CardAdmirals GetNewBreakthroughPower(CardAdmirals c, double coefficient)
    {
        ICardAdmiralsRepository _repository = new CardAdmiralsRepository();
        CardAdmiralsService _service = new CardAdmiralsService(_repository);
        CardAdmirals orginCard = _service.GetCardAdmiralsById(c.Id);
        CardAdmirals cardAdmirals = new CardAdmirals
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
        cardAdmirals.Power = EvaluatePower.CalculatePower(
            cardAdmirals.Health,
            cardAdmirals.PhysicalAttack, cardAdmirals.PhysicalDefense,
            cardAdmirals.MagicalAttack, cardAdmirals.MagicalDefense,
            cardAdmirals.ChemicalAttack, cardAdmirals.ChemicalDefense,
            cardAdmirals.AtomicAttack, cardAdmirals.AtomicDefense,
            cardAdmirals.MentalAttack, cardAdmirals.MentalDefense,
            cardAdmirals.Speed,
            cardAdmirals.CriticalDamageRate, cardAdmirals.CriticalRate, cardAdmirals.CriticalResistanceRate, cardAdmirals.IgnoreCriticalRate,
            cardAdmirals.PenetrationRate, cardAdmirals.PenetrationResistanceRate, cardAdmirals.EvasionRate,
            cardAdmirals.DamageAbsorptionRate, cardAdmirals.IgnoreDamageAbsorptionRate, cardAdmirals.AbsorbedDamageRate,
            cardAdmirals.VitalityRegenerationRate, cardAdmirals.VitalityRegenerationResistanceRate,
            cardAdmirals.AccuracyRate, cardAdmirals.LifestealRate,
            cardAdmirals.ShieldStrength, cardAdmirals.Tenacity, cardAdmirals.ResistanceRate,
            cardAdmirals.ComboRate, cardAdmirals.IgnoreComboRate, cardAdmirals.ComboDamageRate, cardAdmirals.ComboResistanceRate,
            cardAdmirals.StunRate, cardAdmirals.IgnoreStunRate,
            cardAdmirals.ReflectionRate, cardAdmirals.IgnoreReflectionRate, cardAdmirals.ReflectionDamageRate, cardAdmirals.ReflectionResistanceRate,
            cardAdmirals.Mana, cardAdmirals.ManaRegenerationRate,
            cardAdmirals.DamageToDifferentFactionRate, cardAdmirals.ResistanceToDifferentFactionRate,
            cardAdmirals.DamageToSameFactionRate, cardAdmirals.ResistanceToSameFactionRate,
            cardAdmirals.NormalDamageRate, cardAdmirals.NormalResistanceRate,
            cardAdmirals.SkillDamageRate, cardAdmirals.SkillResistanceRate
        );
        return cardAdmirals;
    }

    public List<CardAdmirals> GetUserCardAdmirals(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<CardAdmirals> list = _userCardAdmiralsRepository.GetUserCardAdmirals(user_id, type, pageSize, offset, rare);
        list = GetAllSpiritBeastPower(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllMasterPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        list = GetScienceFictionPower(user_id, list);
        return list;
    }

    public List<CardAdmirals> GetUserCardAdmiralsTeam(string user_id, string teamId, string position)
    {
        List<CardAdmirals> list = _userCardAdmiralsRepository.GetUserCardAdmiralsTeam(user_id, teamId, position);
        list = GetAllSpiritBeastPower(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllMasterPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        list = GetScienceFictionPower(user_id, list);
        return list;
    }

    public Dictionary<string, int> GetUniqueCardAdmiralTypesTeam(string teamId)
    {
        return _userCardAdmiralsRepository.GetUniqueCardAdmiralTypesTeam(teamId);
    }

    public bool UpdateTeamCardAdmirals(string team_id, string position, string card_id)
    {
        return _userCardAdmiralsRepository.UpdateTeamCardAdmirals(team_id, position, card_id);
    }

    public int GetUserCardAdmiralsCount(string user_id, string type, string rare)
    {
        return _userCardAdmiralsRepository.GetUserCardAdmiralsCount(user_id, type, rare);
    }

    public int GetUserCardAdmiralsTeamsPositionCount(string user_id, string team_id, string position)
    {
        return _userCardAdmiralsRepository.GetUserCardAdmiralsTeamsPositionCount(user_id, team_id, position);
    }

    public int GetUserCardAdmiralsTeamsCount(string user_id, string team_id)
    {
        return _userCardAdmiralsRepository.GetUserCardAdmiralsTeamsCount(user_id, team_id);
    }

    public bool InsertUserCardAdmirals(CardAdmirals cardAdmirals)
    {
        return _userCardAdmiralsRepository.InsertUserCardAdmirals(cardAdmirals);
    }

    public bool UpdateCardAdmiralsLevel(CardAdmirals cardAdmirals, int cardLevel)
    {
        return _userCardAdmiralsRepository.UpdateCardAdmiralsLevel(cardAdmirals, cardLevel);
    }

    public bool UpdateCardAdmiralsBreakthrough(CardAdmirals cardAdmirals, int star, int quantity)
    {
        return _userCardAdmiralsRepository.UpdateCardAdmiralsBreakthrough(cardAdmirals, star, quantity);
    }

    public CardAdmirals GetUserCardAdmiralsById(string user_id, string Id)
    {
        CardAdmirals cardAdmiral = _userCardAdmiralsRepository.GetUserCardAdmiralsById(user_id, Id);
        if (cardAdmiral == null) return null;

        // Bọc vào list để tái sử dụng logic
        List<CardAdmirals> list = new List<CardAdmirals> { cardAdmiral };

        list = GetAllSpiritBeastPower(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllMasterPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        list = GetScienceFictionPower(user_id, list);

        return list.FirstOrDefault();
    }

    public List<CardAdmirals> GetAllUserCardAdmiralsInTeam(string user_id)
    {
        List<CardAdmirals> list = _userCardAdmiralsRepository.GetAllUserCardAdmiralsInTeam(user_id);
        list = QualityEvaluator.GetQualityPower(list);
        list = GetFinalPower(user_id, list);
        list = GetAllEquipmentPower(user_id, list);
        list = GetAllRankPower(user_id, list);
        list = GetAllMasterPower(user_id, list);
        list = GetAllAnimeStatsPower(user_id, list);
        list = GetScienceFictionPower(user_id, list);
        return list;
    }
}

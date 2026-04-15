using System.Collections.Generic;
using System.Threading.Tasks;

public class UserPetsService : IUserPetsService
{
     private static UserPetsService _instance;
    private readonly IUserPetsRepository _userPetsRepository;

    public UserPetsService(IUserPetsRepository userPetsRepository)
    {
        _userPetsRepository = userPetsRepository;
    }

    public static UserPetsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserPetsService(new UserPetsRepository());
        }
        return _instance;
    }

    public async Task<List<Pets>> GetAllEquipmentPowerAsync(string user_id, List<Pets> PetsList)
    {
        foreach (var c in PetsList)
        {
            Equipments equipments = await UserEquipmentsService.Create().GetAllEquipmentsByPetIdAsync(user_id, c.Id);
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
        return PetsList;
    }
    public async Task<List<Pets>> GetAllRankPowerAsync(string user_id, List<Pets> PetsList)
    {
        foreach (var c in PetsList)
        {
            Rank rank = await UserPetsRankService.Create().GetSumPetsRankAsync(user_id, c.Id);
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
        return PetsList;
    }
    public async Task<List<Pets>> GetAllMasterPowerAsync(string user_id, List<Pets> PetsList)
    {
        foreach (var c in PetsList)
        {
            Master master = await UserPetsMasterService.Create().GetSumPetsMasterAsync(user_id, c.Id);
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
        return PetsList;
    }
    public async Task<Pets> GetNewLevelPowerAsync(Pets c, double coefficient)
    {
        IPetsRepository _repository = new PetsRepository();
        PetsService _service = new PetsService(_repository);
        Pets orginCard = await _service.GetPetByIdAsync(c.Id);
        Pets pet = new Pets
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
        pet.Power = EvaluatePower.CalculatePower(
            pet.Health,
            pet.PhysicalAttack, pet.PhysicalDefense,
            pet.MagicalAttack, pet.MagicalDefense,
            pet.ChemicalAttack, pet.ChemicalDefense,
            pet.AtomicAttack, pet.AtomicDefense,
            pet.MentalAttack, pet.MentalDefense,
            pet.Speed,
            pet.CriticalDamageRate, pet.CriticalRate, pet.CriticalResistanceRate, pet.IgnoreCriticalRate,
            pet.PenetrationRate, pet.PenetrationResistanceRate, pet.EvasionRate,
            pet.DamageAbsorptionRate, pet.IgnoreDamageAbsorptionRate, pet.AbsorbedDamageRate,
            pet.VitalityRegenerationRate, pet.VitalityRegenerationResistanceRate,
            pet.AccuracyRate, pet.LifestealRate,
            pet.ShieldStrength, pet.Tenacity, pet.ResistanceRate,
            pet.ComboRate, pet.IgnoreComboRate, pet.ComboDamageRate, pet.ComboResistanceRate,
            pet.StunRate, pet.IgnoreStunRate,
            pet.ReflectionRate, pet.IgnoreReflectionRate, pet.ReflectionDamageRate, pet.ReflectionResistanceRate,
            pet.Mana, pet.ManaRegenerationRate,
            pet.DamageToDifferentFactionRate, pet.ResistanceToDifferentFactionRate,
            pet.DamageToSameFactionRate, pet.ResistanceToSameFactionRate,
            pet.NormalDamageRate, pet.NormalResistanceRate,
            pet.SkillDamageRate, pet.SkillResistanceRate
        );
        return pet;
    }
    public async Task<Pets> GetNewBreakthroughPowerAsync(Pets c, double coefficient)
    {
        IPetsRepository _repository = new PetsRepository();
        PetsService _service = new PetsService(_repository);
        Pets orginCard = await _service.GetPetByIdAsync(c.Id);
        Pets pet = new Pets
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
        pet.Power = EvaluatePower.CalculatePower(
            pet.Health,
            pet.PhysicalAttack, pet.PhysicalDefense,
            pet.MagicalAttack, pet.MagicalDefense,
            pet.ChemicalAttack, pet.ChemicalDefense,
            pet.AtomicAttack, pet.AtomicDefense,
            pet.MentalAttack, pet.MentalDefense,
            pet.Speed,
            pet.CriticalDamageRate, pet.CriticalRate, pet.CriticalResistanceRate, pet.IgnoreCriticalRate,
            pet.PenetrationRate, pet.PenetrationResistanceRate, pet.EvasionRate,
            pet.DamageAbsorptionRate, pet.IgnoreDamageAbsorptionRate, pet.AbsorbedDamageRate,
            pet.VitalityRegenerationRate, pet.VitalityRegenerationResistanceRate,
            pet.AccuracyRate, pet.LifestealRate,
            pet.ShieldStrength, pet.Tenacity, pet.ResistanceRate,
            pet.ComboRate, pet.IgnoreComboRate, pet.ComboDamageRate, pet.ComboResistanceRate,
            pet.StunRate, pet.IgnoreStunRate,
            pet.ReflectionRate, pet.IgnoreReflectionRate, pet.ReflectionDamageRate, pet.ReflectionResistanceRate,
            pet.Mana, pet.ManaRegenerationRate,
            pet.DamageToDifferentFactionRate, pet.ResistanceToDifferentFactionRate,
            pet.DamageToSameFactionRate, pet.ResistanceToSameFactionRate,
            pet.NormalDamageRate, pet.NormalResistanceRate,
            pet.SkillDamageRate, pet.SkillResistanceRate
        );
        return pet;
    }

    public async Task<List<Pets>> GetUserPetsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Pets> list = await _userPetsRepository.GetUserPetsAsync(user_id, search, type, pageSize, offset, rare);
        
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
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

        await Task.WhenAll(powerManagerTask, scienceFictionTask, researchTask, archiveTask,
        universeTask, hiinTask, sswnTask, hitnTask, hihnTask, hienTask, hicaTask, hirnTask,
        hidcTask, animeStatsTask);

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
        var animeStatsData = await animeStatsTask;

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        foreach(var book in list)
        {
            book.ApplyPowerStats(powerManagerData);
            book.ApplyScienceFictionStats(scienceFictionData);
            book.ApplyResearchStats(researchData);
            book.ApplyArchiveStats(archiveData);
            book.ApplyUniverseStats(universeData);
            book.ApplyHIINStats(hiinData);
            book.ApplySSWNStats(sswnData);
            book.ApplyHITNStats(hitnData);
            book.ApplyHIHNStats(hihnData);
            book.ApplyHIENStats(hienData);
            book.ApplyHICAStats(hicaData);
            book.ApplyHIRNStats(hirnData);
            book.ApplyHIDCStats(hidcData);
            book.ApplyAllAnimeStats(animeStatsData);
            book.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<Pets>> GetUserPetsTeamAsync(string user_id, string teamId)
    {
        List<Pets> list = await _userPetsRepository.GetUserPetsTeamAsync(user_id, teamId);
        
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
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

        await Task.WhenAll(powerManagerTask, scienceFictionTask, researchTask, archiveTask,
        universeTask, hiinTask, sswnTask, hitnTask, hihnTask, hienTask, hicaTask, hirnTask,
        hidcTask, animeStatsTask);

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
        var animeStatsData = await animeStatsTask;

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        foreach(var book in list)
        {
            book.ApplyPowerStats(powerManagerData);
            book.ApplyScienceFictionStats(scienceFictionData);
            book.ApplyResearchStats(researchData);
            book.ApplyArchiveStats(archiveData);
            book.ApplyUniverseStats(universeData);
            book.ApplyHIINStats(hiinData);
            book.ApplySSWNStats(sswnData);
            book.ApplyHITNStats(hitnData);
            book.ApplyHIHNStats(hihnData);
            book.ApplyHIENStats(hienData);
            book.ApplyHICAStats(hicaData);
            book.ApplyHIRNStats(hirnData);
            book.ApplyHIDCStats(hidcData);
            book.ApplyAllAnimeStats(animeStatsData);
            book.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<Dictionary<string, int>> GetUniquePetsTypesTeamAsync(string teamId)
    {
        return await _userPetsRepository.GetUniquePetsTypesTeamAsync(teamId);
    }

    public async Task<int> GetUserPetsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userPetsRepository.GetUserPetsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserPetAsync(Pets pets, string userId)
    {
        return await _userPetsRepository.InsertUserPetAsync(pets, userId);
    }

    public async Task<bool> UpdatePetLevelAsync(Pets pets, int cardLevel)
    {
        return await _userPetsRepository.UpdatePetLevelAsync(pets, cardLevel);
    }

    public async Task<bool> UpdatePetBreakthroughAsync(Pets pets, int star, double quantity)
    {
        return await _userPetsRepository.UpdatePetBreakthroughAsync(pets, star, quantity);
    }

    public async Task<bool> UpdateTeamPetAsync(string team_id, string card_id)
    {
        return await _userPetsRepository.UpdateTeamPetAsync(team_id, card_id);
    }

    public async Task<Pets> GetUserPetByIdAsync(string user_id, string Id)
    {
        return await _userPetsRepository.GetUserPetByIdAsync(user_id, Id);
    }

    public async Task<List<Pets>> GetAllUserPetsInTeamAsync(string user_id)
    {
        List<Pets> list = await _userPetsRepository.GetAllUserPetsInTeamAsync(user_id);
        
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
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

        await Task.WhenAll(powerManagerTask, scienceFictionTask, researchTask, archiveTask,
        universeTask, hiinTask, sswnTask, hitnTask, hihnTask, hienTask, hicaTask, hirnTask,
        hidcTask, animeStatsTask);

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
        var animeStatsData = await animeStatsTask;

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluator.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        foreach(var book in list)
        {
            book.ApplyPowerStats(powerManagerData);
            book.ApplyScienceFictionStats(scienceFictionData);
            book.ApplyResearchStats(researchData);
            book.ApplyArchiveStats(archiveData);
            book.ApplyUniverseStats(universeData);
            book.ApplyHIINStats(hiinData);
            book.ApplySSWNStats(sswnData);
            book.ApplyHITNStats(hitnData);
            book.ApplyHIHNStats(hihnData);
            book.ApplyHIENStats(hienData);
            book.ApplyHICAStats(hicaData);
            book.ApplyHIRNStats(hirnData);
            book.ApplyHIDCStats(hidcData);
            book.ApplyAllAnimeStats(animeStatsData);
            book.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }
}

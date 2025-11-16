using System.Collections.Generic;

public class UserVehicleService : IUserVehicleService
{
    private readonly IUserVehicleRepository _userVehicleRepository;

    public UserVehicleService(IUserVehicleRepository userVehicleRepository)
    {
        _userVehicleRepository = userVehicleRepository;
    }

    public static UserVehicleService Create()
    {
        return new UserVehicleService(new UserVehicleRepository());
    }

    public Vehicles GetNewLevelPower(Vehicles c, double coefficient)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        Vehicles orginCard = _service.GetVehicleById(c.Id);
        Vehicles Vehicle = new Vehicles
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
        Vehicle.Power = EvaluatePower.CalculatePower(
            Vehicle.Health,
            Vehicle.PhysicalAttack, Vehicle.PhysicalDefense,
            Vehicle.MagicalAttack, Vehicle.MagicalDefense,
            Vehicle.ChemicalAttack, Vehicle.ChemicalDefense,
            Vehicle.AtomicAttack, Vehicle.AtomicDefense,
            Vehicle.MentalAttack, Vehicle.MentalDefense,
            Vehicle.Speed,
            Vehicle.CriticalDamageRate, Vehicle.CriticalRate, Vehicle.CriticalResistanceRate, Vehicle.IgnoreCriticalRate,
            Vehicle.PenetrationRate, Vehicle.PenetrationResistanceRate, Vehicle.EvasionRate,
            Vehicle.DamageAbsorptionRate, Vehicle.IgnoreDamageAbsorptionRate, Vehicle.AbsorbedDamageRate,
            Vehicle.VitalityRegenerationRate, Vehicle.VitalityRegenerationResistanceRate,
            Vehicle.AccuracyRate, Vehicle.LifestealRate,
            Vehicle.ShieldStrength, Vehicle.Tenacity, Vehicle.ResistanceRate,
            Vehicle.ComboRate, Vehicle.IgnoreComboRate, Vehicle.ComboDamageRate, Vehicle.ComboResistanceRate,
            Vehicle.StunRate, Vehicle.IgnoreStunRate,
            Vehicle.ReflectionRate, Vehicle.IgnoreReflectionRate, Vehicle.ReflectionDamageRate, Vehicle.ReflectionResistanceRate,
            Vehicle.Mana, Vehicle.ManaRegenerationRate,
            Vehicle.DamageToDifferentFactionRate, Vehicle.ResistanceToDifferentFactionRate,
            Vehicle.DamageToSameFactionRate, Vehicle.ResistanceToSameFactionRate,
            Vehicle.NormalDamageRate, Vehicle.NormalResistanceRate,
            Vehicle.SkillDamageRate, Vehicle.SkillResistanceRate
        );
        return Vehicle;
    }
    public Vehicles GetNewBreakthroughPower(Vehicles c, double coefficient)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        Vehicles orginCard = _service.GetVehicleById(c.Id);
        Vehicles Vehicle = new Vehicles
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
        Vehicle.Power = EvaluatePower.CalculatePower(
            Vehicle.Health,
            Vehicle.PhysicalAttack, Vehicle.PhysicalDefense,
            Vehicle.MagicalAttack, Vehicle.MagicalDefense,
            Vehicle.ChemicalAttack, Vehicle.ChemicalDefense,
            Vehicle.AtomicAttack, Vehicle.AtomicDefense,
            Vehicle.MentalAttack, Vehicle.MentalDefense,
            Vehicle.Speed,
            Vehicle.CriticalDamageRate, Vehicle.CriticalRate, Vehicle.CriticalResistanceRate, Vehicle.IgnoreCriticalRate,
            Vehicle.PenetrationRate, Vehicle.PenetrationResistanceRate, Vehicle.EvasionRate,
            Vehicle.DamageAbsorptionRate, Vehicle.IgnoreDamageAbsorptionRate, Vehicle.AbsorbedDamageRate,
            Vehicle.VitalityRegenerationRate, Vehicle.VitalityRegenerationResistanceRate,
            Vehicle.AccuracyRate, Vehicle.LifestealRate,
            Vehicle.ShieldStrength, Vehicle.Tenacity, Vehicle.ResistanceRate,
            Vehicle.ComboRate, Vehicle.IgnoreComboRate, Vehicle.ComboDamageRate, Vehicle.ComboResistanceRate,
            Vehicle.StunRate, Vehicle.IgnoreStunRate,
            Vehicle.ReflectionRate, Vehicle.IgnoreReflectionRate, Vehicle.ReflectionDamageRate, Vehicle.ReflectionResistanceRate,
            Vehicle.Mana, Vehicle.ManaRegenerationRate,
            Vehicle.DamageToDifferentFactionRate, Vehicle.ResistanceToDifferentFactionRate,
            Vehicle.DamageToSameFactionRate, Vehicle.ResistanceToSameFactionRate,
            Vehicle.NormalDamageRate, Vehicle.NormalResistanceRate,
            Vehicle.SkillDamageRate, Vehicle.SkillResistanceRate
        );
        return Vehicle;
    }

    public List<Vehicles> GetUserVehicle(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> list = _userVehicleRepository.GetUserVehicle(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserVehicleCount(string user_id, string type, string rare)
    {
        return _userVehicleRepository.GetUserVehicleCount(user_id, type, rare);
    }

    public bool InsertUserVehicle(Vehicles Vehicle)
    {
        return _userVehicleRepository.InsertUserVehicle(Vehicle);
    }

    public bool UpdateVehicleLevel(Vehicles Vehicle, int cardLevel)
    {
        return _userVehicleRepository.UpdateVehicleLevel(Vehicle, cardLevel);
    }

    public bool UpdateVehicleBreakthrough(Vehicles Vehicle, int star, double quantity)
    {
        return _userVehicleRepository.UpdateVehicleBreakthrough(Vehicle, star, quantity);
    }

    public Vehicles GetUserVehicleById(string user_id, string Id)
    {
        return _userVehicleRepository.GetUserVehicleById(user_id, Id);
    }

    public Vehicles SumPowerUserVehicle()
    {
        return _userVehicleRepository.SumPowerUserVehicle();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

public class UserVehiclesService : IUserVehiclesService
{
    private readonly IUserVehiclesRepository _userVehicleRepository;

    public UserVehiclesService(IUserVehiclesRepository userVehicleRepository)
    {
        _userVehicleRepository = userVehicleRepository;
    }

    public static UserVehiclesService Create()
    {
        return new UserVehiclesService(new UserVehiclesRepository());
    }

    public async Task<Vehicles> GetNewLevelPowerAsync(Vehicles c, double coefficient)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        Vehicles orginCard = await _service.GetVehicleByIdAsync(c.Id);
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
    public async Task<Vehicles> GetNewBreakthroughPowerAsync(Vehicles c, double coefficient)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        Vehicles orginCard = await _service.GetVehicleByIdAsync(c.Id);
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

    public async Task<List<Vehicles>> GetUserVehiclesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> list = await _userVehicleRepository.GetUserVehiclesAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserVehiclesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userVehicleRepository.GetUserVehiclesCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserVehicleAsync(Vehicles Vehicle, string userId)
    {
        return await _userVehicleRepository.InsertUserVehicleAsync(Vehicle, userId);
    }

    public async Task<bool> UpdateVehicleLevelAsync(Vehicles Vehicle, int cardLevel)
    {
        return await _userVehicleRepository.UpdateVehicleLevelAsync(Vehicle, cardLevel);
    }

    public async Task<bool> UpdateVehicleBreakthroughAsync(Vehicles Vehicle, int star, double quantity)
    {
        return await _userVehicleRepository.UpdateVehicleBreakthroughAsync(Vehicle, star, quantity);
    }

    public async Task<Vehicles> GetUserVehicleByIdAsync(string user_id, string Id)
    {
        return await _userVehicleRepository.GetUserVehicleByIdAsync(user_id, Id);
    }

    public async Task<Vehicles> SumPowerUserVehiclesAsync()
    {
        return await _userVehicleRepository.SumPowerUserVehiclesAsync();
    }
}

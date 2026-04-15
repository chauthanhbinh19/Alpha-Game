using System.Collections.Generic;
using System.Threading.Tasks;

public class UserVehiclesService : IUserVehiclesService
{
     private static UserVehiclesService _instance;
    private readonly IUserVehiclesRepository _userVehiclesRepository;

    public UserVehiclesService(IUserVehiclesRepository userVehiclesRepository)
    {
        _userVehiclesRepository = userVehiclesRepository;
    }

    public static UserVehiclesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserVehiclesService(new UserVehiclesRepository());
        }
        return _instance;
    }

    public async Task<Vehicles> GetNewLevelPowerAsync(Vehicles c, double coefficient)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        Vehicles orginCard = await _service.GetVehicleByIdAsync(c.Id);
        Vehicles vehicle = new Vehicles
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
        vehicle.Power = PowerHelper.CalculatePower(
            vehicle.Health,
            vehicle.PhysicalAttack, vehicle.PhysicalDefense,
            vehicle.MagicalAttack, vehicle.MagicalDefense,
            vehicle.ChemicalAttack, vehicle.ChemicalDefense,
            vehicle.AtomicAttack, vehicle.AtomicDefense,
            vehicle.MentalAttack, vehicle.MentalDefense,
            vehicle.Speed,
            vehicle.CriticalDamageRate, vehicle.CriticalRate, vehicle.CriticalResistanceRate, vehicle.IgnoreCriticalRate,
            vehicle.PenetrationRate, vehicle.PenetrationResistanceRate, vehicle.EvasionRate,
            vehicle.DamageAbsorptionRate, vehicle.IgnoreDamageAbsorptionRate, vehicle.AbsorbedDamageRate,
            vehicle.VitalityRegenerationRate, vehicle.VitalityRegenerationResistanceRate,
            vehicle.AccuracyRate, vehicle.LifestealRate,
            vehicle.ShieldStrength, vehicle.Tenacity, vehicle.ResistanceRate,
            vehicle.ComboRate, vehicle.IgnoreComboRate, vehicle.ComboDamageRate, vehicle.ComboResistanceRate,
            vehicle.StunRate, vehicle.IgnoreStunRate,
            vehicle.ReflectionRate, vehicle.IgnoreReflectionRate, vehicle.ReflectionDamageRate, vehicle.ReflectionResistanceRate,
            vehicle.Mana, vehicle.ManaRegenerationRate,
            vehicle.DamageToDifferentFactionRate, vehicle.ResistanceToDifferentFactionRate,
            vehicle.DamageToSameFactionRate, vehicle.ResistanceToSameFactionRate,
            vehicle.NormalDamageRate, vehicle.NormalResistanceRate,
            vehicle.SkillDamageRate, vehicle.SkillResistanceRate
        );
        return vehicle;
    }
    public async Task<Vehicles> GetNewBreakthroughPowerAsync(Vehicles c, double coefficient)
    {
        IVehiclesRepository _repository = new VehiclesRepository();
        VehiclesService _service = new VehiclesService(_repository);
        Vehicles orginCard = await _service.GetVehicleByIdAsync(c.Id);
        Vehicles vehicle = new Vehicles
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
        vehicle.Power = PowerHelper.CalculatePower(
            vehicle.Health,
            vehicle.PhysicalAttack, vehicle.PhysicalDefense,
            vehicle.MagicalAttack, vehicle.MagicalDefense,
            vehicle.ChemicalAttack, vehicle.ChemicalDefense,
            vehicle.AtomicAttack, vehicle.AtomicDefense,
            vehicle.MentalAttack, vehicle.MentalDefense,
            vehicle.Speed,
            vehicle.CriticalDamageRate, vehicle.CriticalRate, vehicle.CriticalResistanceRate, vehicle.IgnoreCriticalRate,
            vehicle.PenetrationRate, vehicle.PenetrationResistanceRate, vehicle.EvasionRate,
            vehicle.DamageAbsorptionRate, vehicle.IgnoreDamageAbsorptionRate, vehicle.AbsorbedDamageRate,
            vehicle.VitalityRegenerationRate, vehicle.VitalityRegenerationResistanceRate,
            vehicle.AccuracyRate, vehicle.LifestealRate,
            vehicle.ShieldStrength, vehicle.Tenacity, vehicle.ResistanceRate,
            vehicle.ComboRate, vehicle.IgnoreComboRate, vehicle.ComboDamageRate, vehicle.ComboResistanceRate,
            vehicle.StunRate, vehicle.IgnoreStunRate,
            vehicle.ReflectionRate, vehicle.IgnoreReflectionRate, vehicle.ReflectionDamageRate, vehicle.ReflectionResistanceRate,
            vehicle.Mana, vehicle.ManaRegenerationRate,
            vehicle.DamageToDifferentFactionRate, vehicle.ResistanceToDifferentFactionRate,
            vehicle.DamageToSameFactionRate, vehicle.ResistanceToSameFactionRate,
            vehicle.NormalDamageRate, vehicle.NormalResistanceRate,
            vehicle.SkillDamageRate, vehicle.SkillResistanceRate
        );
        return vehicle;
    }

    public async Task<List<Vehicles>> GetUserVehiclesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> list = await _userVehiclesRepository.GetUserVehiclesAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserVehiclesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userVehiclesRepository.GetUserVehiclesCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserVehicleAsync(Vehicles Vehicle, string userId)
    {
        return await _userVehiclesRepository.InsertUserVehicleAsync(Vehicle, userId);
    }

    public async Task<bool> UpdateVehicleLevelAsync(Vehicles Vehicle, int cardLevel)
    {
        return await _userVehiclesRepository.UpdateVehicleLevelAsync(Vehicle, cardLevel);
    }

    public async Task<bool> UpdateVehicleBreakthroughAsync(Vehicles Vehicle, int star, double quantity)
    {
        return await _userVehiclesRepository.UpdateVehicleBreakthroughAsync(Vehicle, star, quantity);
    }

    public async Task<Vehicles> GetUserVehicleByIdAsync(string user_id, string Id)
    {
        return await _userVehiclesRepository.GetUserVehicleByIdAsync(user_id, Id);
    }

    public async Task<Vehicles> SumPowerUserVehiclesAsync()
    {
        return await _userVehiclesRepository.SumPowerUserVehiclesAsync();
    }
}

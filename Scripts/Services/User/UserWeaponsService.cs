using System.Collections.Generic;
using System.Threading.Tasks;

public class UserWeaponsService : IUserWeaponsService
{
     private static UserWeaponsService _instance;
    private readonly IUserWeaponsRepository _userWeaponsRepository;

    public UserWeaponsService(IUserWeaponsRepository userWeaponsRepository)
    {
        _userWeaponsRepository = userWeaponsRepository;
    }

    public static UserWeaponsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserWeaponsService(new UserWeaponsRepository());
        }
        return _instance;
    }

    public async Task<Weapons> GetNewLevelPowerAsync(Weapons c, double coefficient)
    {
        IWeaponsRepository _repository = new WeaponsRepository();
        WeaponsService _service = new WeaponsService(_repository);
        Weapons orginCard = await _service.GetWeaponByIdAsync(c.Id);
        Weapons weapon = new Weapons
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
        weapon.Power = PowerHelper.CalculatePower(
            weapon.Health,
            weapon.PhysicalAttack, weapon.PhysicalDefense,
            weapon.MagicalAttack, weapon.MagicalDefense,
            weapon.ChemicalAttack, weapon.ChemicalDefense,
            weapon.AtomicAttack, weapon.AtomicDefense,
            weapon.MentalAttack, weapon.MentalDefense,
            weapon.Speed,
            weapon.CriticalDamageRate, weapon.CriticalRate, weapon.CriticalResistanceRate, weapon.IgnoreCriticalRate,
            weapon.PenetrationRate, weapon.PenetrationResistanceRate, weapon.EvasionRate,
            weapon.DamageAbsorptionRate, weapon.IgnoreDamageAbsorptionRate, weapon.AbsorbedDamageRate,
            weapon.VitalityRegenerationRate, weapon.VitalityRegenerationResistanceRate,
            weapon.AccuracyRate, weapon.LifestealRate,
            weapon.ShieldStrength, weapon.Tenacity, weapon.ResistanceRate,
            weapon.ComboRate, weapon.IgnoreComboRate, weapon.ComboDamageRate, weapon.ComboResistanceRate,
            weapon.StunRate, weapon.IgnoreStunRate,
            weapon.ReflectionRate, weapon.IgnoreReflectionRate, weapon.ReflectionDamageRate, weapon.ReflectionResistanceRate,
            weapon.Mana, weapon.ManaRegenerationRate,
            weapon.DamageToDifferentFactionRate, weapon.ResistanceToDifferentFactionRate,
            weapon.DamageToSameFactionRate, weapon.ResistanceToSameFactionRate,
            weapon.NormalDamageRate, weapon.NormalResistanceRate,
            weapon.SkillDamageRate, weapon.SkillResistanceRate
        );
        return weapon;
    }
    public async Task<Weapons> GetNewBreakthroughPowerAsync(Weapons c, double coefficient)
    {
        IWeaponsRepository _repository = new WeaponsRepository();
        WeaponsService _service = new WeaponsService(_repository);
        Weapons orginCard = await _service.GetWeaponByIdAsync(c.Id);
        Weapons weapon = new Weapons
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
        weapon.Power = PowerHelper.CalculatePower(
            weapon.Health,
            weapon.PhysicalAttack, weapon.PhysicalDefense,
            weapon.MagicalAttack, weapon.MagicalDefense,
            weapon.ChemicalAttack, weapon.ChemicalDefense,
            weapon.AtomicAttack, weapon.AtomicDefense,
            weapon.MentalAttack, weapon.MentalDefense,
            weapon.Speed,
            weapon.CriticalDamageRate, weapon.CriticalRate, weapon.CriticalResistanceRate, weapon.IgnoreCriticalRate,
            weapon.PenetrationRate, weapon.PenetrationResistanceRate, weapon.EvasionRate,
            weapon.DamageAbsorptionRate, weapon.IgnoreDamageAbsorptionRate, weapon.AbsorbedDamageRate,
            weapon.VitalityRegenerationRate, weapon.VitalityRegenerationResistanceRate,
            weapon.AccuracyRate, weapon.LifestealRate,
            weapon.ShieldStrength, weapon.Tenacity, weapon.ResistanceRate,
            weapon.ComboRate, weapon.IgnoreComboRate, weapon.ComboDamageRate, weapon.ComboResistanceRate,
            weapon.StunRate, weapon.IgnoreStunRate,
            weapon.ReflectionRate, weapon.IgnoreReflectionRate, weapon.ReflectionDamageRate, weapon.ReflectionResistanceRate,
            weapon.Mana, weapon.ManaRegenerationRate,
            weapon.DamageToDifferentFactionRate, weapon.ResistanceToDifferentFactionRate,
            weapon.DamageToSameFactionRate, weapon.ResistanceToSameFactionRate,
            weapon.NormalDamageRate, weapon.NormalResistanceRate,
            weapon.SkillDamageRate, weapon.SkillResistanceRate
        );
        return weapon;
    }

    public async Task<List<Weapons>> GetUserWeaponsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Weapons> list = await _userWeaponsRepository.GetUserWeaponsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserWeaponsCountAsync(string user_id, string search, string rare)
    {
        return await _userWeaponsRepository.GetUserWeaponsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserWeaponAsync(Weapons Weapons, string userId)
    {
        return await _userWeaponsRepository.InsertUserWeaponAsync(Weapons, userId);
    }

    public async Task<bool> UpdateWeaponLevelAsync(Weapons Weapons, int cardLevel)
    {
        return await _userWeaponsRepository.UpdateWeaponLevelAsync(Weapons, cardLevel);
    }

    public async Task<bool> UpdateWeaponBreakthroughAsync(Weapons Weapons, int star, double quantity)
    {
        return await _userWeaponsRepository.UpdateWeaponBreakthroughAsync(Weapons, star, quantity);
    }

    public async Task<Weapons> GetUserWeaponByIdAsync(string user_id, string Id)
    {
        return await _userWeaponsRepository.GetUserWeaponByIdAsync(user_id, Id);
    }

    public async Task<Weapons> SumPowerUserWeaponsAsync()
    {
        return await _userWeaponsRepository.SumPowerUserWeaponsAsync();
    }
}

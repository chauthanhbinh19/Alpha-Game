using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMechaBeastsService : IUserMechaBeastsService
{
     private static UserMechaBeastsService _instance;
    private readonly IUserMechaBeastsRepository _userMechaBeastsRepository;

    public UserMechaBeastsService(IUserMechaBeastsRepository userMechaBeastsRepository)
    {
        _userMechaBeastsRepository = userMechaBeastsRepository;
    }

    public static UserMechaBeastsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserMechaBeastsService(new UserMechaBeastsRepository());
        }
        return _instance;
    }

    public async Task<MechaBeasts> GetNewLevelPowerAsync(MechaBeasts c, double coefficient)
    {
        IMechaBeastsRepository _repository = new MechaBeastsRepository();
        MechaBeastsService _service = new MechaBeastsService(_repository);
        MechaBeasts orginCard = await _service.GetMechaBeastByIdAsync(c.Id);
        MechaBeasts mechaBeast = new MechaBeasts
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
        mechaBeast.Power = PowerHelper.CalculatePower(
            mechaBeast.Health,
            mechaBeast.PhysicalAttack, mechaBeast.PhysicalDefense,
            mechaBeast.MagicalAttack, mechaBeast.MagicalDefense,
            mechaBeast.ChemicalAttack, mechaBeast.ChemicalDefense,
            mechaBeast.AtomicAttack, mechaBeast.AtomicDefense,
            mechaBeast.MentalAttack, mechaBeast.MentalDefense,
            mechaBeast.Speed,
            mechaBeast.CriticalDamageRate, mechaBeast.CriticalRate, mechaBeast.CriticalResistanceRate, mechaBeast.IgnoreCriticalRate,
            mechaBeast.PenetrationRate, mechaBeast.PenetrationResistanceRate, mechaBeast.EvasionRate,
            mechaBeast.DamageAbsorptionRate, mechaBeast.IgnoreDamageAbsorptionRate, mechaBeast.AbsorbedDamageRate,
            mechaBeast.VitalityRegenerationRate, mechaBeast.VitalityRegenerationResistanceRate,
            mechaBeast.AccuracyRate, mechaBeast.LifestealRate,
            mechaBeast.ShieldStrength, mechaBeast.Tenacity, mechaBeast.ResistanceRate,
            mechaBeast.ComboRate, mechaBeast.IgnoreComboRate, mechaBeast.ComboDamageRate, mechaBeast.ComboResistanceRate,
            mechaBeast.StunRate, mechaBeast.IgnoreStunRate,
            mechaBeast.ReflectionRate, mechaBeast.IgnoreReflectionRate, mechaBeast.ReflectionDamageRate, mechaBeast.ReflectionResistanceRate,
            mechaBeast.Mana, mechaBeast.ManaRegenerationRate,
            mechaBeast.DamageToDifferentFactionRate, mechaBeast.ResistanceToDifferentFactionRate,
            mechaBeast.DamageToSameFactionRate, mechaBeast.ResistanceToSameFactionRate,
            mechaBeast.NormalDamageRate, mechaBeast.NormalResistanceRate,
            mechaBeast.SkillDamageRate, mechaBeast.SkillResistanceRate
        );
        return mechaBeast;
    }
    public async Task<MechaBeasts> GetNewBreakthroughPowerAsync(MechaBeasts c, double coefficient)
    {
        IMechaBeastsRepository _repository = new MechaBeastsRepository();
        MechaBeastsService _service = new MechaBeastsService(_repository);
        MechaBeasts orginCard = await _service.GetMechaBeastByIdAsync(c.Id);
        MechaBeasts mechaBeast = new MechaBeasts
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
        mechaBeast.Power = PowerHelper.CalculatePower(
            mechaBeast.Health,
            mechaBeast.PhysicalAttack, mechaBeast.PhysicalDefense,
            mechaBeast.MagicalAttack, mechaBeast.MagicalDefense,
            mechaBeast.ChemicalAttack, mechaBeast.ChemicalDefense,
            mechaBeast.AtomicAttack, mechaBeast.AtomicDefense,
            mechaBeast.MentalAttack, mechaBeast.MentalDefense,
            mechaBeast.Speed,
            mechaBeast.CriticalDamageRate, mechaBeast.CriticalRate, mechaBeast.CriticalResistanceRate, mechaBeast.IgnoreCriticalRate,
            mechaBeast.PenetrationRate, mechaBeast.PenetrationResistanceRate, mechaBeast.EvasionRate,
            mechaBeast.DamageAbsorptionRate, mechaBeast.IgnoreDamageAbsorptionRate, mechaBeast.AbsorbedDamageRate,
            mechaBeast.VitalityRegenerationRate, mechaBeast.VitalityRegenerationResistanceRate,
            mechaBeast.AccuracyRate, mechaBeast.LifestealRate,
            mechaBeast.ShieldStrength, mechaBeast.Tenacity, mechaBeast.ResistanceRate,
            mechaBeast.ComboRate, mechaBeast.IgnoreComboRate, mechaBeast.ComboDamageRate, mechaBeast.ComboResistanceRate,
            mechaBeast.StunRate, mechaBeast.IgnoreStunRate,
            mechaBeast.ReflectionRate, mechaBeast.IgnoreReflectionRate, mechaBeast.ReflectionDamageRate, mechaBeast.ReflectionResistanceRate,
            mechaBeast.Mana, mechaBeast.ManaRegenerationRate,
            mechaBeast.DamageToDifferentFactionRate, mechaBeast.ResistanceToDifferentFactionRate,
            mechaBeast.DamageToSameFactionRate, mechaBeast.ResistanceToSameFactionRate,
            mechaBeast.NormalDamageRate, mechaBeast.NormalResistanceRate,
            mechaBeast.SkillDamageRate, mechaBeast.SkillResistanceRate
        );
        return mechaBeast;
    }

    public async Task<List<MechaBeasts>> GetUserMechaBeastsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = await _userMechaBeastsRepository.GetUserMechaBeastsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMechaBeastsCountAsync(string user_id, string search, string rare)
    {
        return await _userMechaBeastsRepository.GetUserMechaBeastsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserMechaBeastAsync(MechaBeasts MechaBeasts, string userId)
    {
        return await _userMechaBeastsRepository.InsertUserMechaBeastAsync(MechaBeasts, userId);
    }

    public async Task<bool> UpdateMechaBeastLevelAsync(MechaBeasts MechaBeasts, int cardLevel)
    {
        return await _userMechaBeastsRepository.UpdateMechaBeastLevelAsync(MechaBeasts, cardLevel);
    }

    public async Task<bool> UpdateMechaBeastBreakthroughAsync(MechaBeasts MechaBeasts, int star, double quantity)
    {
        return await _userMechaBeastsRepository.UpdateMechaBeastBreakthroughAsync(MechaBeasts, star, quantity);
    }

    public async Task<MechaBeasts> GetUserMechaBeastByIdAsync(string user_id, string Id)
    {
        return await _userMechaBeastsRepository.GetUserMechaBeastByIdAsync(user_id, Id);
    }

    public async Task<MechaBeasts> SumPowerUserMechaBeastsAsync()
    {
        return await _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync();
    }
}

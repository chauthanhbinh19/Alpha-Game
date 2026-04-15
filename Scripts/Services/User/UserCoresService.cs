using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCoresService : IUserCoresService
{
     private static UserCoresService _instance;
    private readonly IUserCoresRepository _userCoresRepository;

    public UserCoresService(IUserCoresRepository userCoresRepository)
    {
        _userCoresRepository = userCoresRepository;
    }

    public static UserCoresService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCoresService(new UserCoresRepository());
        }
        return _instance;
    }

    public async Task<Cores> GetNewLevelPowerAsync(Cores c, double coefficient)
    {
        ICoresRepository _repository = new CoresRepository();
        CoresService _service = new CoresService(_repository);
        Cores orginCard = await _service.GetCoreByIdAsync(c.Id);
        Cores core = new Cores
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
        core.Power = PowerHelper.CalculatePower(
            core.Health,
            core.PhysicalAttack, core.PhysicalDefense,
            core.MagicalAttack, core.MagicalDefense,
            core.ChemicalAttack, core.ChemicalDefense,
            core.AtomicAttack, core.AtomicDefense,
            core.MentalAttack, core.MentalDefense,
            core.Speed,
            core.CriticalDamageRate, core.CriticalRate, core.CriticalResistanceRate, core.IgnoreCriticalRate,
            core.PenetrationRate, core.PenetrationResistanceRate, core.EvasionRate,
            core.DamageAbsorptionRate, core.IgnoreDamageAbsorptionRate, core.AbsorbedDamageRate,
            core.VitalityRegenerationRate, core.VitalityRegenerationResistanceRate,
            core.AccuracyRate, core.LifestealRate,
            core.ShieldStrength, core.Tenacity, core.ResistanceRate,
            core.ComboRate, core.IgnoreComboRate, core.ComboDamageRate, core.ComboResistanceRate,
            core.StunRate, core.IgnoreStunRate,
            core.ReflectionRate, core.IgnoreReflectionRate, core.ReflectionDamageRate, core.ReflectionResistanceRate,
            core.Mana, core.ManaRegenerationRate,
            core.DamageToDifferentFactionRate, core.ResistanceToDifferentFactionRate,
            core.DamageToSameFactionRate, core.ResistanceToSameFactionRate,
            core.NormalDamageRate, core.NormalResistanceRate,
            core.SkillDamageRate, core.SkillResistanceRate
        );
        return core;
    }
    public async Task<Cores> GetNewBreakthroughPowerAsync(Cores c, double coefficient)
    {
        ICoresRepository _repository = new CoresRepository();
        CoresService _service = new CoresService(_repository);
        Cores orginCard = await _service.GetCoreByIdAsync(c.Id);
        Cores core = new Cores
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
        core.Power = PowerHelper.CalculatePower(
            core.Health,
            core.PhysicalAttack, core.PhysicalDefense,
            core.MagicalAttack, core.MagicalDefense,
            core.ChemicalAttack, core.ChemicalDefense,
            core.AtomicAttack, core.AtomicDefense,
            core.MentalAttack, core.MentalDefense,
            core.Speed,
            core.CriticalDamageRate, core.CriticalRate, core.CriticalResistanceRate, core.IgnoreCriticalRate,
            core.PenetrationRate, core.PenetrationResistanceRate, core.EvasionRate,
            core.DamageAbsorptionRate, core.IgnoreDamageAbsorptionRate, core.AbsorbedDamageRate,
            core.VitalityRegenerationRate, core.VitalityRegenerationResistanceRate,
            core.AccuracyRate, core.LifestealRate,
            core.ShieldStrength, core.Tenacity, core.ResistanceRate,
            core.ComboRate, core.IgnoreComboRate, core.ComboDamageRate, core.ComboResistanceRate,
            core.StunRate, core.IgnoreStunRate,
            core.ReflectionRate, core.IgnoreReflectionRate, core.ReflectionDamageRate, core.ReflectionResistanceRate,
            core.Mana, core.ManaRegenerationRate,
            core.DamageToDifferentFactionRate, core.ResistanceToDifferentFactionRate,
            core.DamageToSameFactionRate, core.ResistanceToSameFactionRate,
            core.NormalDamageRate, core.NormalResistanceRate,
            core.SkillDamageRate, core.SkillResistanceRate
        );
        return core;
    }

    public async Task<List<Cores>> GetUserCoresAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Cores> list = await _userCoresRepository.GetUserCoresAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCoresCountAsync(string user_id, string search, string rare)
    {
        return await _userCoresRepository.GetUserCoresCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserCoreAsync(Cores Cores, string userId)
    {
        return await _userCoresRepository.InsertUserCoreAsync(Cores, userId);
    }

    public async Task<bool> UpdateCoreLevelAsync(Cores Cores, int cardLevel)
    {
        return await _userCoresRepository.UpdateCoreLevelAsync(Cores, cardLevel);
    }

    public async Task<bool> UpdateCoreBreakthroughAsync(Cores Cores, int star, double quantity)
    {
        return await _userCoresRepository.UpdateCoreBreakthroughAsync(Cores, star, quantity);
    }

    public async Task<Cores> GetUserCoreByIdAsync(string user_id, string Id)
    {
        return await _userCoresRepository.GetUserCoreByIdAsync(user_id, Id);
    }

    public async Task<Cores> SumPowerUserCoresAsync()
    {
        return await _userCoresRepository.SumPowerUserCoresAsync();
    }
}

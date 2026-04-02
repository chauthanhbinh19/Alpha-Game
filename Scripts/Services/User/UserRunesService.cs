using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRunesService : IUserRunesService
{
     private static UserRunesService _instance;
    private readonly IUserRunesRepository _userRunesRepository;

    public UserRunesService(IUserRunesRepository userRunesRepository)
    {
        _userRunesRepository = userRunesRepository;
    }

    public static UserRunesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserRunesService(new UserRunesRepository());
        }
        return _instance;
    }

    public async Task<Runes> GetNewLevelPowerAsync(Runes c, double coefficient)
    {
        IRunesRepository _repository = new RunesRepository();
        RunesService _service = new RunesService(_repository);
        Runes orginCard = await _service.GetRuneByIdAsync(c.Id);
        Runes rune = new Runes
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
        rune.Power = EvaluatePower.CalculatePower(
            rune.Health,
            rune.PhysicalAttack, rune.PhysicalDefense,
            rune.MagicalAttack, rune.MagicalDefense,
            rune.ChemicalAttack, rune.ChemicalDefense,
            rune.AtomicAttack, rune.AtomicDefense,
            rune.MentalAttack, rune.MentalDefense,
            rune.Speed,
            rune.CriticalDamageRate, rune.CriticalRate, rune.CriticalResistanceRate, rune.IgnoreCriticalRate,
            rune.PenetrationRate, rune.PenetrationResistanceRate, rune.EvasionRate,
            rune.DamageAbsorptionRate, rune.IgnoreDamageAbsorptionRate, rune.AbsorbedDamageRate,
            rune.VitalityRegenerationRate, rune.VitalityRegenerationResistanceRate,
            rune.AccuracyRate, rune.LifestealRate,
            rune.ShieldStrength, rune.Tenacity, rune.ResistanceRate,
            rune.ComboRate, rune.IgnoreComboRate, rune.ComboDamageRate, rune.ComboResistanceRate,
            rune.StunRate, rune.IgnoreStunRate,
            rune.ReflectionRate, rune.IgnoreReflectionRate, rune.ReflectionDamageRate, rune.ReflectionResistanceRate,
            rune.Mana, rune.ManaRegenerationRate,
            rune.DamageToDifferentFactionRate, rune.ResistanceToDifferentFactionRate,
            rune.DamageToSameFactionRate, rune.ResistanceToSameFactionRate,
            rune.NormalDamageRate, rune.NormalResistanceRate,
            rune.SkillDamageRate, rune.SkillResistanceRate
        );
        return rune;
    }
    public async Task<Runes> GetNewBreakthroughPowerAsync(Runes c, double coefficient)
    {
        IRunesRepository _repository = new RunesRepository();
        RunesService _service = new RunesService(_repository);
        Runes orginCard = await _service.GetRuneByIdAsync(c.Id);
        Runes rune = new Runes
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
        rune.Power = EvaluatePower.CalculatePower(
            rune.Health,
            rune.PhysicalAttack, rune.PhysicalDefense,
            rune.MagicalAttack, rune.MagicalDefense,
            rune.ChemicalAttack, rune.ChemicalDefense,
            rune.AtomicAttack, rune.AtomicDefense,
            rune.MentalAttack, rune.MentalDefense,
            rune.Speed,
            rune.CriticalDamageRate, rune.CriticalRate, rune.CriticalResistanceRate, rune.IgnoreCriticalRate,
            rune.PenetrationRate, rune.PenetrationResistanceRate, rune.EvasionRate,
            rune.DamageAbsorptionRate, rune.IgnoreDamageAbsorptionRate, rune.AbsorbedDamageRate,
            rune.VitalityRegenerationRate, rune.VitalityRegenerationResistanceRate,
            rune.AccuracyRate, rune.LifestealRate,
            rune.ShieldStrength, rune.Tenacity, rune.ResistanceRate,
            rune.ComboRate, rune.IgnoreComboRate, rune.ComboDamageRate, rune.ComboResistanceRate,
            rune.StunRate, rune.IgnoreStunRate,
            rune.ReflectionRate, rune.IgnoreReflectionRate, rune.ReflectionDamageRate, rune.ReflectionResistanceRate,
            rune.Mana, rune.ManaRegenerationRate,
            rune.DamageToDifferentFactionRate, rune.ResistanceToDifferentFactionRate,
            rune.DamageToSameFactionRate, rune.ResistanceToSameFactionRate,
            rune.NormalDamageRate, rune.NormalResistanceRate,
            rune.SkillDamageRate, rune.SkillResistanceRate
        );
        return rune;
    }

    public async Task<List<Runes>> GetUserRunesAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Runes> list = await _userRunesRepository.GetUserRunesAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserRunesCountAsync(string user_id, string search, string rare)
    {
        return await _userRunesRepository.GetUserRunesCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserRuneAsync(Runes Runes, string userId)
    {
        return await _userRunesRepository.InsertUserRuneAsync(Runes, userId);
    }

    public async Task<bool> UpdateRuneLevelAsync(Runes Runes, int cardLevel)
    {
        return await _userRunesRepository.UpdateRuneLevelAsync(Runes, cardLevel);
    }

    public async Task<bool> UpdateRuneBreakthroughAsync(Runes Runes, int star, double quantity)
    {
        return await _userRunesRepository.UpdateRuneBreakthroughAsync(Runes, star, quantity);
    }

    public async Task<Runes> GetUserRuneByIdAsync(string user_id, string Id)
    {
        return await _userRunesRepository.GetUserRuneByIdAsync(user_id, Id);
    }

    public async Task<Runes> SumPowerUserRunesAsync()
    {
        return await _userRunesRepository.SumPowerUserRunesAsync();
    }
}

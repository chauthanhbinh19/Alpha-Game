using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRelicsService : IUserRelicsService
{
    private readonly IUserRelicsRepository _userRelicsRepository;

    public UserRelicsService(IUserRelicsRepository userRelicsRepository)
    {
        _userRelicsRepository = userRelicsRepository;
    }

    public static UserRelicsService Create()
    {
        return new UserRelicsService(new UserRelicsRepository());
    }

    public async Task<Relics> GetNewLevelPowerAsync(Relics c, double coefficient)
    {
        IRelicsRepository _repository = new RelicsRepository();
        RelicsService _service = new RelicsService(_repository);
        Relics orginCard = await _service.GetRelicByIdAsync(c.Id);
        Relics relics = new Relics
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
        relics.Power = EvaluatePower.CalculatePower(
            relics.Health,
            relics.PhysicalAttack, relics.PhysicalDefense,
            relics.MagicalAttack, relics.MagicalDefense,
            relics.ChemicalAttack, relics.ChemicalDefense,
            relics.AtomicAttack, relics.AtomicDefense,
            relics.MentalAttack, relics.MentalDefense,
            relics.Speed,
            relics.CriticalDamageRate, relics.CriticalRate, relics.CriticalResistanceRate, relics.IgnoreCriticalRate,
            relics.PenetrationRate, relics.PenetrationResistanceRate, relics.EvasionRate,
            relics.DamageAbsorptionRate, relics.IgnoreDamageAbsorptionRate, relics.AbsorbedDamageRate,
            relics.VitalityRegenerationRate, relics.VitalityRegenerationResistanceRate,
            relics.AccuracyRate, relics.LifestealRate,
            relics.ShieldStrength, relics.Tenacity, relics.ResistanceRate,
            relics.ComboRate, relics.IgnoreComboRate, relics.ComboDamageRate, relics.ComboResistanceRate,
            relics.StunRate, relics.IgnoreStunRate,
            relics.ReflectionRate, relics.IgnoreReflectionRate, relics.ReflectionDamageRate, relics.ReflectionResistanceRate,
            relics.Mana, relics.ManaRegenerationRate,
            relics.DamageToDifferentFactionRate, relics.ResistanceToDifferentFactionRate,
            relics.DamageToSameFactionRate, relics.ResistanceToSameFactionRate,
            relics.NormalDamageRate, relics.NormalResistanceRate,
            relics.SkillDamageRate, relics.SkillResistanceRate
        );
        return relics;
    }
    public async Task<Relics> GetNewBreakthroughPowerAsync(Relics c, double coefficient)
    {
        IRelicsRepository _repository = new RelicsRepository();
        RelicsService _service = new RelicsService(_repository);
        Relics orginCard = await _service.GetRelicByIdAsync(c.Id);
        Relics relics = new Relics
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
        relics.Power = EvaluatePower.CalculatePower(
            relics.Health,
            relics.PhysicalAttack, relics.PhysicalDefense,
            relics.MagicalAttack, relics.MagicalDefense,
            relics.ChemicalAttack, relics.ChemicalDefense,
            relics.AtomicAttack, relics.AtomicDefense,
            relics.MentalAttack, relics.MentalDefense,
            relics.Speed,
            relics.CriticalDamageRate, relics.CriticalRate, relics.CriticalResistanceRate, relics.IgnoreCriticalRate,
            relics.PenetrationRate, relics.PenetrationResistanceRate, relics.EvasionRate,
            relics.DamageAbsorptionRate, relics.IgnoreDamageAbsorptionRate, relics.AbsorbedDamageRate,
            relics.VitalityRegenerationRate, relics.VitalityRegenerationResistanceRate,
            relics.AccuracyRate, relics.LifestealRate,
            relics.ShieldStrength, relics.Tenacity, relics.ResistanceRate,
            relics.ComboRate, relics.IgnoreComboRate, relics.ComboDamageRate, relics.ComboResistanceRate,
            relics.StunRate, relics.IgnoreStunRate,
            relics.ReflectionRate, relics.IgnoreReflectionRate, relics.ReflectionDamageRate, relics.ReflectionResistanceRate,
            relics.Mana, relics.ManaRegenerationRate,
            relics.DamageToDifferentFactionRate, relics.ResistanceToDifferentFactionRate,
            relics.DamageToSameFactionRate, relics.ResistanceToSameFactionRate,
            relics.NormalDamageRate, relics.NormalResistanceRate,
            relics.SkillDamageRate, relics.SkillResistanceRate
        );
        return relics;
    }

    public async Task<List<Relics>> GetUserRelicsAsync(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Relics> list = await _userRelicsRepository.GetUserRelicsAsync(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserRelicsCountAsync(string user_id, string type, string rare)
    {
        return await _userRelicsRepository.GetUserRelicsCountAsync(user_id, type, rare);
    }

    public async Task<bool> InsertUserRelicAsync(Relics relics, string userId)
    {
        return await _userRelicsRepository.InsertUserRelicAsync(relics, userId);
    }

    public async Task<bool> UpdateRelicLevelAsync(Relics relics, int cardLevel)
    {
        return await _userRelicsRepository.UpdateRelicLevelAsync(relics, cardLevel);
    }

    public async Task<bool> UpdateRelicBreakthroughAsync(Relics relics, int star, double quantity)
    {
        return await _userRelicsRepository.UpdateRelicBreakthroughAsync(relics, star, quantity);
    }

    public async Task<Relics> GetUserRelicByIdAsync(string user_id, string Id)
    {
        return await _userRelicsRepository.GetUserRelicByIdAsync(user_id, Id);
    }

    public async Task<Relics> SumPowerUserRelicsAsync()
    {
        return await _userRelicsRepository.SumPowerUserRelicsAsync();
    }
}

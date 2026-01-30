using System.Collections.Generic;
using System.Threading.Tasks;

public class UserForgesService : IUserForgesService
{
    private IUserForgesRepository _userForgeRepository;

    public UserForgesService(IUserForgesRepository userForgeRepository)
    {
        _userForgeRepository = userForgeRepository;
    }

    public static UserForgesService Create()
    {
        return new UserForgesService(new UserForgesRepository());
    }

    public async Task<Forges> GetNewLevelPowerAsync(Forges c, double coefficient)
    {
        IForgesRepository _repository = new ForgesRepository();
        ForgesService _service = new ForgesService(_repository);
        Forges orginCard = await _service.GetForgeByIdAsync(c.Id);
        Forges Forge = new Forges
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
        Forge.Power = EvaluatePower.CalculatePower(
            Forge.Health,
            Forge.PhysicalAttack, Forge.PhysicalDefense,
            Forge.MagicalAttack, Forge.MagicalDefense,
            Forge.ChemicalAttack, Forge.ChemicalDefense,
            Forge.AtomicAttack, Forge.AtomicDefense,
            Forge.MentalAttack, Forge.MentalDefense,
            Forge.Speed,
            Forge.CriticalDamageRate, Forge.CriticalRate, Forge.CriticalResistanceRate, Forge.IgnoreCriticalRate,
            Forge.PenetrationRate, Forge.PenetrationResistanceRate, Forge.EvasionRate,
            Forge.DamageAbsorptionRate, Forge.IgnoreDamageAbsorptionRate, Forge.AbsorbedDamageRate,
            Forge.VitalityRegenerationRate, Forge.VitalityRegenerationResistanceRate,
            Forge.AccuracyRate, Forge.LifestealRate,
            Forge.ShieldStrength, Forge.Tenacity, Forge.ResistanceRate,
            Forge.ComboRate, Forge.IgnoreComboRate, Forge.ComboDamageRate, Forge.ComboResistanceRate,
            Forge.StunRate, Forge.IgnoreStunRate,
            Forge.ReflectionRate, Forge.IgnoreReflectionRate, Forge.ReflectionDamageRate, Forge.ReflectionResistanceRate,
            Forge.Mana, Forge.ManaRegenerationRate,
            Forge.DamageToDifferentFactionRate, Forge.ResistanceToDifferentFactionRate,
            Forge.DamageToSameFactionRate, Forge.ResistanceToSameFactionRate,
            Forge.NormalDamageRate, Forge.NormalResistanceRate,
            Forge.SkillDamageRate, Forge.SkillResistanceRate
        );
        return Forge;
    }
    public async Task<Forges> GetNewBreakthroughPowerAsync(Forges c, double coefficient)
    {
        IForgesRepository _repository = new ForgesRepository();
        ForgesService _service = new ForgesService(_repository);
        Forges orginCard = await _service.GetForgeByIdAsync(c.Id);
        Forges Forge = new Forges
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
        Forge.Power = EvaluatePower.CalculatePower(
            Forge.Health,
            Forge.PhysicalAttack, Forge.PhysicalDefense,
            Forge.MagicalAttack, Forge.MagicalDefense,
            Forge.ChemicalAttack, Forge.ChemicalDefense,
            Forge.AtomicAttack, Forge.AtomicDefense,
            Forge.MentalAttack, Forge.MentalDefense,
            Forge.Speed,
            Forge.CriticalDamageRate, Forge.CriticalRate, Forge.CriticalResistanceRate, Forge.IgnoreCriticalRate,
            Forge.PenetrationRate, Forge.PenetrationResistanceRate, Forge.EvasionRate,
            Forge.DamageAbsorptionRate, Forge.IgnoreDamageAbsorptionRate, Forge.AbsorbedDamageRate,
            Forge.VitalityRegenerationRate, Forge.VitalityRegenerationResistanceRate,
            Forge.AccuracyRate, Forge.LifestealRate,
            Forge.ShieldStrength, Forge.Tenacity, Forge.ResistanceRate,
            Forge.ComboRate, Forge.IgnoreComboRate, Forge.ComboDamageRate, Forge.ComboResistanceRate,
            Forge.StunRate, Forge.IgnoreStunRate,
            Forge.ReflectionRate, Forge.IgnoreReflectionRate, Forge.ReflectionDamageRate, Forge.ReflectionResistanceRate,
            Forge.Mana, Forge.ManaRegenerationRate,
            Forge.DamageToDifferentFactionRate, Forge.ResistanceToDifferentFactionRate,
            Forge.DamageToSameFactionRate, Forge.ResistanceToSameFactionRate,
            Forge.NormalDamageRate, Forge.NormalResistanceRate,
            Forge.SkillDamageRate, Forge.SkillResistanceRate
        );
        return Forge;
    }

    public async Task<List<Forges>> GetUserForgesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Forges> list = await _userForgeRepository.GetUserForgesAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserForgesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userForgeRepository.GetUserForgesCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserForgeAsync(Forges forge, string userId)
    {
        return await _userForgeRepository.InsertUserForgeAsync(forge, userId);
    }

    public async Task<bool> UpdateForgeLevelAsync(Forges forge, int cardLevel)
    {
        return await _userForgeRepository.UpdateForgeLevelAsync(forge, cardLevel);
    }

    public async Task<bool> UpdateForgeBreakthroughAsync(Forges forge, int star, double quantity)
    {
        return await _userForgeRepository.UpdateForgeBreakthroughAsync(forge, star, quantity);
    }

    public async Task<Forges> GetUserForgeByIdAsync(string user_id, string Id)
    {
        return await _userForgeRepository.GetUserForgeByIdAsync(user_id, Id);
    }

    public async Task<Forges> SumPowerUserForgesAsync()
    {
        return await _userForgeRepository.SumPowerUserForgesAsync();
    }
}

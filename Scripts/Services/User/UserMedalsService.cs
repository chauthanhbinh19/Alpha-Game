using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMedalsService : IUserMedalsService
{
    private IUserMedalsRepository _userMedalsRepository;

    public UserMedalsService(IUserMedalsRepository userMedalsRepository)
    {
        _userMedalsRepository = userMedalsRepository;
    }

    public static UserMedalsService Create()
    {
        return new UserMedalsService(new UserMedalsRepository());
    }

    public async Task<Medals> GetNewLevelPowerAsync(Medals c, double coefficient)
    {
        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        Medals orginCard = await _service.GetMedalByIdAsync(c.Id);
        Medals medals = new Medals
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
        medals.Power = EvaluatePower.CalculatePower(
            medals.Health,
            medals.PhysicalAttack, medals.PhysicalDefense,
            medals.MagicalAttack, medals.MagicalDefense,
            medals.ChemicalAttack, medals.ChemicalDefense,
            medals.AtomicAttack, medals.AtomicDefense,
            medals.MentalAttack, medals.MentalDefense,
            medals.Speed,
            medals.CriticalDamageRate, medals.CriticalRate, medals.CriticalResistanceRate, medals.IgnoreCriticalRate,
            medals.PenetrationRate, medals.PenetrationResistanceRate, medals.EvasionRate,
            medals.DamageAbsorptionRate, medals.IgnoreDamageAbsorptionRate, medals.AbsorbedDamageRate,
            medals.VitalityRegenerationRate, medals.VitalityRegenerationResistanceRate,
            medals.AccuracyRate, medals.LifestealRate,
            medals.ShieldStrength, medals.Tenacity, medals.ResistanceRate,
            medals.ComboRate, medals.IgnoreComboRate, medals.ComboDamageRate, medals.ComboResistanceRate,
            medals.StunRate, medals.IgnoreStunRate,
            medals.ReflectionRate, medals.IgnoreReflectionRate, medals.ReflectionDamageRate, medals.ReflectionResistanceRate,
            medals.Mana, medals.ManaRegenerationRate,
            medals.DamageToDifferentFactionRate, medals.ResistanceToDifferentFactionRate,
            medals.DamageToSameFactionRate, medals.ResistanceToSameFactionRate,
            medals.NormalDamageRate, medals.NormalResistanceRate,
            medals.SkillDamageRate, medals.SkillResistanceRate
        );
        return medals;
    }
    public async Task<Medals> GetNewBreakthroughPowerAsync(Medals c, double coefficient)
    {
        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        Medals orginCard = await _service.GetMedalByIdAsync(c.Id);
        Medals medals = new Medals
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
        medals.Power = EvaluatePower.CalculatePower(
            medals.Health,
            medals.PhysicalAttack, medals.PhysicalDefense,
            medals.MagicalAttack, medals.MagicalDefense,
            medals.ChemicalAttack, medals.ChemicalDefense,
            medals.AtomicAttack, medals.AtomicDefense,
            medals.MentalAttack, medals.MentalDefense,
            medals.Speed,
            medals.CriticalDamageRate, medals.CriticalRate, medals.CriticalResistanceRate, medals.IgnoreCriticalRate,
            medals.PenetrationRate, medals.PenetrationResistanceRate, medals.EvasionRate,
            medals.DamageAbsorptionRate, medals.IgnoreDamageAbsorptionRate, medals.AbsorbedDamageRate,
            medals.VitalityRegenerationRate, medals.VitalityRegenerationResistanceRate,
            medals.AccuracyRate, medals.LifestealRate,
            medals.ShieldStrength, medals.Tenacity, medals.ResistanceRate,
            medals.ComboRate, medals.IgnoreComboRate, medals.ComboDamageRate, medals.ComboResistanceRate,
            medals.StunRate, medals.IgnoreStunRate,
            medals.ReflectionRate, medals.IgnoreReflectionRate, medals.ReflectionDamageRate, medals.ReflectionResistanceRate,
            medals.Mana, medals.ManaRegenerationRate,
            medals.DamageToDifferentFactionRate, medals.ResistanceToDifferentFactionRate,
            medals.DamageToSameFactionRate, medals.ResistanceToSameFactionRate,
            medals.NormalDamageRate, medals.NormalResistanceRate,
            medals.SkillDamageRate, medals.SkillResistanceRate
        );
        return medals;
    }

    public async Task<List<Medals>> GetUserMedalsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Medals> list = await _userMedalsRepository.GetUserMedalsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserMedalsCountAsync(string user_id, string search, string rare)
    {
        return await _userMedalsRepository.GetUserMedalsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserMedalAsync(Medals medals, string userId)
    {
        return await _userMedalsRepository.InsertUserMedalAsync(medals, userId);
    }

    public async Task<bool> UpdateMedalLevelAsync(Medals medals, int cardLevel)
    {
        return await _userMedalsRepository.UpdateMedalLevelAsync(medals, cardLevel);
    }

    public async Task<bool> UpdateMedalBreakthroughAsync(Medals medals, int star, double quantity)
    {
        return await _userMedalsRepository.UpdateMedalBreakthroughAsync(medals, star, quantity);
    }

    public async Task<Medals> GetUserMedalByIdAsync(string user_id, string Id)
    {
        return await _userMedalsRepository.GetUserMedalByIdAsync(user_id, Id);
    }

    public async Task<Medals> SumPowerUserMedalsAsync()
    {
        return await _userMedalsRepository.SumPowerUserMedalsAsync();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMedalsService : IUserMedalsService
{
     private static UserMedalsService _instance;
    private IUserMedalsRepository _userMedalsRepository;

    public UserMedalsService(IUserMedalsRepository userMedalsRepository)
    {
        _userMedalsRepository = userMedalsRepository;
    }

    public static UserMedalsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserMedalsService(new UserMedalsRepository());
        }
        return _instance;
    }

    public async Task<Medals> GetNewLevelPowerAsync(Medals c, double coefficient)
    {
        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        Medals orginCard = await _service.GetMedalByIdAsync(c.Id);
        Medals medal = new Medals
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
        medal.Power = EvaluatePower.CalculatePower(
            medal.Health,
            medal.PhysicalAttack, medal.PhysicalDefense,
            medal.MagicalAttack, medal.MagicalDefense,
            medal.ChemicalAttack, medal.ChemicalDefense,
            medal.AtomicAttack, medal.AtomicDefense,
            medal.MentalAttack, medal.MentalDefense,
            medal.Speed,
            medal.CriticalDamageRate, medal.CriticalRate, medal.CriticalResistanceRate, medal.IgnoreCriticalRate,
            medal.PenetrationRate, medal.PenetrationResistanceRate, medal.EvasionRate,
            medal.DamageAbsorptionRate, medal.IgnoreDamageAbsorptionRate, medal.AbsorbedDamageRate,
            medal.VitalityRegenerationRate, medal.VitalityRegenerationResistanceRate,
            medal.AccuracyRate, medal.LifestealRate,
            medal.ShieldStrength, medal.Tenacity, medal.ResistanceRate,
            medal.ComboRate, medal.IgnoreComboRate, medal.ComboDamageRate, medal.ComboResistanceRate,
            medal.StunRate, medal.IgnoreStunRate,
            medal.ReflectionRate, medal.IgnoreReflectionRate, medal.ReflectionDamageRate, medal.ReflectionResistanceRate,
            medal.Mana, medal.ManaRegenerationRate,
            medal.DamageToDifferentFactionRate, medal.ResistanceToDifferentFactionRate,
            medal.DamageToSameFactionRate, medal.ResistanceToSameFactionRate,
            medal.NormalDamageRate, medal.NormalResistanceRate,
            medal.SkillDamageRate, medal.SkillResistanceRate
        );
        return medal;
    }
    public async Task<Medals> GetNewBreakthroughPowerAsync(Medals c, double coefficient)
    {
        IMedalsRepository _repository = new MedalsRepository();
        MedalsService _service = new MedalsService(_repository);
        Medals orginCard = await _service.GetMedalByIdAsync(c.Id);
        Medals medal = new Medals
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
        medal.Power = EvaluatePower.CalculatePower(
            medal.Health,
            medal.PhysicalAttack, medal.PhysicalDefense,
            medal.MagicalAttack, medal.MagicalDefense,
            medal.ChemicalAttack, medal.ChemicalDefense,
            medal.AtomicAttack, medal.AtomicDefense,
            medal.MentalAttack, medal.MentalDefense,
            medal.Speed,
            medal.CriticalDamageRate, medal.CriticalRate, medal.CriticalResistanceRate, medal.IgnoreCriticalRate,
            medal.PenetrationRate, medal.PenetrationResistanceRate, medal.EvasionRate,
            medal.DamageAbsorptionRate, medal.IgnoreDamageAbsorptionRate, medal.AbsorbedDamageRate,
            medal.VitalityRegenerationRate, medal.VitalityRegenerationResistanceRate,
            medal.AccuracyRate, medal.LifestealRate,
            medal.ShieldStrength, medal.Tenacity, medal.ResistanceRate,
            medal.ComboRate, medal.IgnoreComboRate, medal.ComboDamageRate, medal.ComboResistanceRate,
            medal.StunRate, medal.IgnoreStunRate,
            medal.ReflectionRate, medal.IgnoreReflectionRate, medal.ReflectionDamageRate, medal.ReflectionResistanceRate,
            medal.Mana, medal.ManaRegenerationRate,
            medal.DamageToDifferentFactionRate, medal.ResistanceToDifferentFactionRate,
            medal.DamageToSameFactionRate, medal.ResistanceToSameFactionRate,
            medal.NormalDamageRate, medal.NormalResistanceRate,
            medal.SkillDamageRate, medal.SkillResistanceRate
        );
        return medal;
    }

    public async Task<List<Medals>> GetUserMedalsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Medals> list = await _userMedalsRepository.GetUserMedalsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
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

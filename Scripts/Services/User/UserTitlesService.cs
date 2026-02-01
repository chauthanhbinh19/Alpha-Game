using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTitlesService : IUserTitlesService
{
     private static UserTitlesService _instance;
    private readonly IUserTitlesRepository _userTitlesRepository;

    public UserTitlesService(IUserTitlesRepository userTitlesRepository)
    {
        _userTitlesRepository = userTitlesRepository;
    }

    public static UserTitlesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserTitlesService(new UserTitlesRepository());
        }
        return _instance;
    }

    public async Task<Titles> GetNewLevelPowerAsync(Titles c, double coefficient)
    {
        ITitlesRepository _repository = new TitlesRepository();
        TitlesService _service = new TitlesService(_repository);
        Titles orginCard = await _service.GetTitleByIdAsync(c.Id);
        Titles titles = new Titles
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
        titles.Power = EvaluatePower.CalculatePower(
            titles.Health,
            titles.PhysicalAttack, titles.PhysicalDefense,
            titles.MagicalAttack, titles.MagicalDefense,
            titles.ChemicalAttack, titles.ChemicalDefense,
            titles.AtomicAttack, titles.AtomicDefense,
            titles.MentalAttack, titles.MentalDefense,
            titles.Speed,
            titles.CriticalDamageRate, titles.CriticalRate, titles.CriticalResistanceRate, titles.IgnoreCriticalRate,
            titles.PenetrationRate, titles.PenetrationResistanceRate, titles.EvasionRate,
            titles.DamageAbsorptionRate, titles.IgnoreDamageAbsorptionRate, titles.AbsorbedDamageRate,
            titles.VitalityRegenerationRate, titles.VitalityRegenerationResistanceRate,
            titles.AccuracyRate, titles.LifestealRate,
            titles.ShieldStrength, titles.Tenacity, titles.ResistanceRate,
            titles.ComboRate, titles.IgnoreComboRate, titles.ComboDamageRate, titles.ComboResistanceRate,
            titles.StunRate, titles.IgnoreStunRate,
            titles.ReflectionRate, titles.IgnoreReflectionRate, titles.ReflectionDamageRate, titles.ReflectionResistanceRate,
            titles.Mana, titles.ManaRegenerationRate,
            titles.DamageToDifferentFactionRate, titles.ResistanceToDifferentFactionRate,
            titles.DamageToSameFactionRate, titles.ResistanceToSameFactionRate,
            titles.NormalDamageRate, titles.NormalResistanceRate,
            titles.SkillDamageRate, titles.SkillResistanceRate
        );
        return titles;
    }
    public async Task<Titles> GetNewBreakthroughPowerAsync(Titles c, double coefficient)
    {
        ITitlesRepository _repository = new TitlesRepository();
        TitlesService _service = new TitlesService(_repository);
        Titles orginCard = await _service.GetTitleByIdAsync(c.Id);
        Titles titles = new Titles
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
        titles.Power = EvaluatePower.CalculatePower(
            titles.Health,
            titles.PhysicalAttack, titles.PhysicalDefense,
            titles.MagicalAttack, titles.MagicalDefense,
            titles.ChemicalAttack, titles.ChemicalDefense,
            titles.AtomicAttack, titles.AtomicDefense,
            titles.MentalAttack, titles.MentalDefense,
            titles.Speed,
            titles.CriticalDamageRate, titles.CriticalRate, titles.CriticalResistanceRate, titles.IgnoreCriticalRate,
            titles.PenetrationRate, titles.PenetrationResistanceRate, titles.EvasionRate,
            titles.DamageAbsorptionRate, titles.IgnoreDamageAbsorptionRate, titles.AbsorbedDamageRate,
            titles.VitalityRegenerationRate, titles.VitalityRegenerationResistanceRate,
            titles.AccuracyRate, titles.LifestealRate,
            titles.ShieldStrength, titles.Tenacity, titles.ResistanceRate,
            titles.ComboRate, titles.IgnoreComboRate, titles.ComboDamageRate, titles.ComboResistanceRate,
            titles.StunRate, titles.IgnoreStunRate,
            titles.ReflectionRate, titles.IgnoreReflectionRate, titles.ReflectionDamageRate, titles.ReflectionResistanceRate,
            titles.Mana, titles.ManaRegenerationRate,
            titles.DamageToDifferentFactionRate, titles.ResistanceToDifferentFactionRate,
            titles.DamageToSameFactionRate, titles.ResistanceToSameFactionRate,
            titles.NormalDamageRate, titles.NormalResistanceRate,
            titles.SkillDamageRate, titles.SkillResistanceRate
        );
        return titles;
    }

    public async Task<List<Titles>> GetUserTitlesAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Titles> list = await _userTitlesRepository.GetUserTitlesAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserTitlesCountAsync(string user_id, string search, string rare)
    {
        return await _userTitlesRepository.GetUserTitlesCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserTitleAsync(Titles titles, string userId)
    {
        return await _userTitlesRepository.InsertUserTitleAsync(titles, userId);
    }

    public async Task<bool> UpdateTitleLevelAsync(Titles titles, int cardLevel)
    {
        return await _userTitlesRepository.UpdateTitleLevelAsync(titles, cardLevel);
    }

    public async Task<bool> UpdateTitleBreakthroughAsync(Titles titles, int star, double quantity)
    {
        return await _userTitlesRepository.UpdateTitleBreakthroughAsync(titles, star, quantity);
    }

    public async Task<Titles> GetUserTitleByIdAsync(string user_id, string Id)
    {
        return await _userTitlesRepository.GetUserTitleByIdAsync(user_id, Id);
    }

    public async Task<Titles> SumPowerUserTitlesAsync()
    {
        return await _userTitlesRepository.SumPowerUserTitlesAsync();
    }
}

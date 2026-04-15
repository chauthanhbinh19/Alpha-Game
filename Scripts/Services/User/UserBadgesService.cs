using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBadgesService : IUserBadgesService
{
     private static UserBadgesService _instance;
    private readonly IUserBadgesRepository _userBadgesRepository;

    public UserBadgesService(IUserBadgesRepository userBadgesRepository)
    {
        _userBadgesRepository = userBadgesRepository;
    }

    public static UserBadgesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserBadgesService(new UserBadgesRepository());
        }
        return _instance;
    }

    public async Task<Badges> GetNewLevelPowerAsync(Badges c, double coefficient)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        Badges orginCard = await _service.GetBadgeByIdAsync(c.Id);
        Badges badge = new Badges
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
        badge.Power = PowerHelper.CalculatePower(
            badge.Health,
            badge.PhysicalAttack, badge.PhysicalDefense,
            badge.MagicalAttack, badge.MagicalDefense,
            badge.ChemicalAttack, badge.ChemicalDefense,
            badge.AtomicAttack, badge.AtomicDefense,
            badge.MentalAttack, badge.MentalDefense,
            badge.Speed,
            badge.CriticalDamageRate, badge.CriticalRate, badge.CriticalResistanceRate, badge.IgnoreCriticalRate,
            badge.PenetrationRate, badge.PenetrationResistanceRate, badge.EvasionRate,
            badge.DamageAbsorptionRate, badge.IgnoreDamageAbsorptionRate, badge.AbsorbedDamageRate,
            badge.VitalityRegenerationRate, badge.VitalityRegenerationResistanceRate,
            badge.AccuracyRate, badge.LifestealRate,
            badge.ShieldStrength, badge.Tenacity, badge.ResistanceRate,
            badge.ComboRate, badge.IgnoreComboRate, badge.ComboDamageRate, badge.ComboResistanceRate,
            badge.StunRate, badge.IgnoreStunRate,
            badge.ReflectionRate, badge.IgnoreReflectionRate, badge.ReflectionDamageRate, badge.ReflectionResistanceRate,
            badge.Mana, badge.ManaRegenerationRate,
            badge.DamageToDifferentFactionRate, badge.ResistanceToDifferentFactionRate,
            badge.DamageToSameFactionRate, badge.ResistanceToSameFactionRate,
            badge.NormalDamageRate, badge.NormalResistanceRate,
            badge.SkillDamageRate, badge.SkillResistanceRate
        );
        return badge;
    }
    public async Task<Badges> GetNewBreakthroughPowerAsync(Badges c, double coefficient)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        Badges orginCard = await _service.GetBadgeByIdAsync(c.Id);
        Badges badge = new Badges
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
        badge.Power = PowerHelper.CalculatePower(
            badge.Health,
            badge.PhysicalAttack, badge.PhysicalDefense,
            badge.MagicalAttack, badge.MagicalDefense,
            badge.ChemicalAttack, badge.ChemicalDefense,
            badge.AtomicAttack, badge.AtomicDefense,
            badge.MentalAttack, badge.MentalDefense,
            badge.Speed,
            badge.CriticalDamageRate, badge.CriticalRate, badge.CriticalResistanceRate, badge.IgnoreCriticalRate,
            badge.PenetrationRate, badge.PenetrationResistanceRate, badge.EvasionRate,
            badge.DamageAbsorptionRate, badge.IgnoreDamageAbsorptionRate, badge.AbsorbedDamageRate,
            badge.VitalityRegenerationRate, badge.VitalityRegenerationResistanceRate,
            badge.AccuracyRate, badge.LifestealRate,
            badge.ShieldStrength, badge.Tenacity, badge.ResistanceRate,
            badge.ComboRate, badge.IgnoreComboRate, badge.ComboDamageRate, badge.ComboResistanceRate,
            badge.StunRate, badge.IgnoreStunRate,
            badge.ReflectionRate, badge.IgnoreReflectionRate, badge.ReflectionDamageRate, badge.ReflectionResistanceRate,
            badge.Mana, badge.ManaRegenerationRate,
            badge.DamageToDifferentFactionRate, badge.ResistanceToDifferentFactionRate,
            badge.DamageToSameFactionRate, badge.ResistanceToSameFactionRate,
            badge.NormalDamageRate, badge.NormalResistanceRate,
            badge.SkillDamageRate, badge.SkillResistanceRate
        );
        return badge;
    }

    public async Task<List<Badges>> GetUserBadgesAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Badges> list = await _userBadgesRepository.GetUserBadgesAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBadgesCountAsync(string user_id, string search, string rare)
    {
        return await _userBadgesRepository.GetUserBadgesCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserBadgeAsync(Badges Badges, string userId)
    {
        return await _userBadgesRepository.InsertUserBadgeAsync(Badges, userId);
    }

    public async Task<bool> UpdateBadgeLevelAsync(Badges Badges, int cardLevel)
    {
        return await _userBadgesRepository.UpdateBadgeLevelAsync(Badges, cardLevel);
    }

    public async Task<bool> UpdateBadgeBreakthroughAsync(Badges Badges, int star, double quantity)
    {
        return await _userBadgesRepository.UpdateBadgeBreakthroughAsync(Badges, star, quantity);
    }

    public async Task<Badges> GetUserBadgeByIdAsync(string user_id, string Id)
    {
        return await _userBadgesRepository.GetUserBadgeByIdAsync(user_id, Id);
    }

    public async Task<Badges> SumPowerUserBadgesAsync()
    {
        return await _userBadgesRepository.SumPowerUserBadgesAsync();
    }
}

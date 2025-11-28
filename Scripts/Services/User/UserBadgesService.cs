using System.Collections.Generic;

public class UserBadgesService : IUserBadgesService
{
    private readonly IUserBadgesRepository _userBadgesRepository;

    public UserBadgesService(IUserBadgesRepository userBadgesRepository)
    {
        _userBadgesRepository = userBadgesRepository;
    }

    public static UserBadgesService Create()
    {
        return new UserBadgesService(new UserBadgesRepository());
    }

    public Badges GetNewLevelPower(Badges c, double coefficient)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        Badges orginCard = _service.GetBadgesById(c.Id);
        Badges Badges = new Badges
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
        Badges.Power = EvaluatePower.CalculatePower(
            Badges.Health,
            Badges.PhysicalAttack, Badges.PhysicalDefense,
            Badges.MagicalAttack, Badges.MagicalDefense,
            Badges.ChemicalAttack, Badges.ChemicalDefense,
            Badges.AtomicAttack, Badges.AtomicDefense,
            Badges.MentalAttack, Badges.MentalDefense,
            Badges.Speed,
            Badges.CriticalDamageRate, Badges.CriticalRate, Badges.CriticalResistanceRate, Badges.IgnoreCriticalRate,
            Badges.PenetrationRate, Badges.PenetrationResistanceRate, Badges.EvasionRate,
            Badges.DamageAbsorptionRate, Badges.IgnoreDamageAbsorptionRate, Badges.AbsorbedDamageRate,
            Badges.VitalityRegenerationRate, Badges.VitalityRegenerationResistanceRate,
            Badges.AccuracyRate, Badges.LifestealRate,
            Badges.ShieldStrength, Badges.Tenacity, Badges.ResistanceRate,
            Badges.ComboRate, Badges.IgnoreComboRate, Badges.ComboDamageRate, Badges.ComboResistanceRate,
            Badges.StunRate, Badges.IgnoreStunRate,
            Badges.ReflectionRate, Badges.IgnoreReflectionRate, Badges.ReflectionDamageRate, Badges.ReflectionResistanceRate,
            Badges.Mana, Badges.ManaRegenerationRate,
            Badges.DamageToDifferentFactionRate, Badges.ResistanceToDifferentFactionRate,
            Badges.DamageToSameFactionRate, Badges.ResistanceToSameFactionRate,
            Badges.NormalDamageRate, Badges.NormalResistanceRate,
            Badges.SkillDamageRate, Badges.SkillResistanceRate
        );
        return Badges;
    }
    public Badges GetNewBreakthroughPower(Badges c, double coefficient)
    {
        IBadgesRepository _repository = new BadgesRepository();
        BadgesService _service = new BadgesService(_repository);
        Badges orginCard = _service.GetBadgesById(c.Id);
        Badges Badges = new Badges
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
        Badges.Power = EvaluatePower.CalculatePower(
            Badges.Health,
            Badges.PhysicalAttack, Badges.PhysicalDefense,
            Badges.MagicalAttack, Badges.MagicalDefense,
            Badges.ChemicalAttack, Badges.ChemicalDefense,
            Badges.AtomicAttack, Badges.AtomicDefense,
            Badges.MentalAttack, Badges.MentalDefense,
            Badges.Speed,
            Badges.CriticalDamageRate, Badges.CriticalRate, Badges.CriticalResistanceRate, Badges.IgnoreCriticalRate,
            Badges.PenetrationRate, Badges.PenetrationResistanceRate, Badges.EvasionRate,
            Badges.DamageAbsorptionRate, Badges.IgnoreDamageAbsorptionRate, Badges.AbsorbedDamageRate,
            Badges.VitalityRegenerationRate, Badges.VitalityRegenerationResistanceRate,
            Badges.AccuracyRate, Badges.LifestealRate,
            Badges.ShieldStrength, Badges.Tenacity, Badges.ResistanceRate,
            Badges.ComboRate, Badges.IgnoreComboRate, Badges.ComboDamageRate, Badges.ComboResistanceRate,
            Badges.StunRate, Badges.IgnoreStunRate,
            Badges.ReflectionRate, Badges.IgnoreReflectionRate, Badges.ReflectionDamageRate, Badges.ReflectionResistanceRate,
            Badges.Mana, Badges.ManaRegenerationRate,
            Badges.DamageToDifferentFactionRate, Badges.ResistanceToDifferentFactionRate,
            Badges.DamageToSameFactionRate, Badges.ResistanceToSameFactionRate,
            Badges.NormalDamageRate, Badges.NormalResistanceRate,
            Badges.SkillDamageRate, Badges.SkillResistanceRate
        );
        return Badges;
    }

    public List<Badges> GetUserBadges(string user_id, int pageSize, int offset, string rare)
    {
        List<Badges> list = _userBadgesRepository.GetUserBadges(user_id, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserBadgesCount(string user_id, string rare)
    {
        return _userBadgesRepository.GetUserBadgesCount(user_id, rare);
    }

    public bool InsertUserBadges(Badges Badges, string userId)
    {
        return _userBadgesRepository.InsertUserBadges(Badges, userId);
    }

    public bool UpdateBadgesLevel(Badges Badges, int cardLevel)
    {
        return _userBadgesRepository.UpdateBadgesLevel(Badges, cardLevel);
    }

    public bool UpdateBadgesBreakthrough(Badges Badges, int star, double quantity)
    {
        return _userBadgesRepository.UpdateBadgesBreakthrough(Badges, star, quantity);
    }

    public Badges GetUserBadgesById(string user_id, string Id)
    {
        return _userBadgesRepository.GetUserBadgesById(user_id, Id);
    }

    public Badges SumPowerUserBadges()
    {
        return _userBadgesRepository.SumPowerUserBadges();
    }
}

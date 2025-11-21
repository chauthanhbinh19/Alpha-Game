using System.Collections.Generic;

public class UserRobotsService : IUserRobotsService
{
    private readonly IUserRobotsRepository _userRobotsRepository;

    public UserRobotsService(IUserRobotsRepository userRobotsRepository)
    {
        _userRobotsRepository = userRobotsRepository;
    }

    public static UserRobotsService Create()
    {
        return new UserRobotsService(new UserRobotsRepository());
    }

    public Robots GetNewLevelPower(Robots c, double coefficient)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        Robots orginCard = _service.GetRobotsById(c.Id);
        Robots Robots = new Robots
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
        Robots.Power = EvaluatePower.CalculatePower(
            Robots.Health,
            Robots.PhysicalAttack, Robots.PhysicalDefense,
            Robots.MagicalAttack, Robots.MagicalDefense,
            Robots.ChemicalAttack, Robots.ChemicalDefense,
            Robots.AtomicAttack, Robots.AtomicDefense,
            Robots.MentalAttack, Robots.MentalDefense,
            Robots.Speed,
            Robots.CriticalDamageRate, Robots.CriticalRate, Robots.CriticalResistanceRate, Robots.IgnoreCriticalRate,
            Robots.PenetrationRate, Robots.PenetrationResistanceRate, Robots.EvasionRate,
            Robots.DamageAbsorptionRate, Robots.IgnoreDamageAbsorptionRate, Robots.AbsorbedDamageRate,
            Robots.VitalityRegenerationRate, Robots.VitalityRegenerationResistanceRate,
            Robots.AccuracyRate, Robots.LifestealRate,
            Robots.ShieldStrength, Robots.Tenacity, Robots.ResistanceRate,
            Robots.ComboRate, Robots.IgnoreComboRate, Robots.ComboDamageRate, Robots.ComboResistanceRate,
            Robots.StunRate, Robots.IgnoreStunRate,
            Robots.ReflectionRate, Robots.IgnoreReflectionRate, Robots.ReflectionDamageRate, Robots.ReflectionResistanceRate,
            Robots.Mana, Robots.ManaRegenerationRate,
            Robots.DamageToDifferentFactionRate, Robots.ResistanceToDifferentFactionRate,
            Robots.DamageToSameFactionRate, Robots.ResistanceToSameFactionRate,
            Robots.NormalDamageRate, Robots.NormalResistanceRate,
            Robots.SkillDamageRate, Robots.SkillResistanceRate
        );
        return Robots;
    }
    public Robots GetNewBreakthroughPower(Robots c, double coefficient)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        Robots orginCard = _service.GetRobotsById(c.Id);
        Robots Robots = new Robots
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
        Robots.Power = EvaluatePower.CalculatePower(
            Robots.Health,
            Robots.PhysicalAttack, Robots.PhysicalDefense,
            Robots.MagicalAttack, Robots.MagicalDefense,
            Robots.ChemicalAttack, Robots.ChemicalDefense,
            Robots.AtomicAttack, Robots.AtomicDefense,
            Robots.MentalAttack, Robots.MentalDefense,
            Robots.Speed,
            Robots.CriticalDamageRate, Robots.CriticalRate, Robots.CriticalResistanceRate, Robots.IgnoreCriticalRate,
            Robots.PenetrationRate, Robots.PenetrationResistanceRate, Robots.EvasionRate,
            Robots.DamageAbsorptionRate, Robots.IgnoreDamageAbsorptionRate, Robots.AbsorbedDamageRate,
            Robots.VitalityRegenerationRate, Robots.VitalityRegenerationResistanceRate,
            Robots.AccuracyRate, Robots.LifestealRate,
            Robots.ShieldStrength, Robots.Tenacity, Robots.ResistanceRate,
            Robots.ComboRate, Robots.IgnoreComboRate, Robots.ComboDamageRate, Robots.ComboResistanceRate,
            Robots.StunRate, Robots.IgnoreStunRate,
            Robots.ReflectionRate, Robots.IgnoreReflectionRate, Robots.ReflectionDamageRate, Robots.ReflectionResistanceRate,
            Robots.Mana, Robots.ManaRegenerationRate,
            Robots.DamageToDifferentFactionRate, Robots.ResistanceToDifferentFactionRate,
            Robots.DamageToSameFactionRate, Robots.ResistanceToSameFactionRate,
            Robots.NormalDamageRate, Robots.NormalResistanceRate,
            Robots.SkillDamageRate, Robots.SkillResistanceRate
        );
        return Robots;
    }

    public List<Robots> GetUserRobots(string user_id, int pageSize, int offset, string rare)
    {
        List<Robots> list = _userRobotsRepository.GetUserRobots(user_id, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserRobotsCount(string user_id, string rare)
    {
        return _userRobotsRepository.GetUserRobotsCount(user_id, rare);
    }

    public bool InsertUserRobots(Robots Robots)
    {
        return _userRobotsRepository.InsertUserRobots(Robots);
    }

    public bool UpdateRobotsLevel(Robots Robots, int cardLevel)
    {
        return _userRobotsRepository.UpdateRobotsLevel(Robots, cardLevel);
    }

    public bool UpdateRobotsBreakthrough(Robots Robots, int star, double quantity)
    {
        return _userRobotsRepository.UpdateRobotsBreakthrough(Robots, star, quantity);
    }

    public Robots GetUserRobotsById(string user_id, string Id)
    {
        return _userRobotsRepository.GetUserRobotsById(user_id, Id);
    }

    public Robots SumPowerUserRobots()
    {
        return _userRobotsRepository.SumPowerUserRobots();
    }
}

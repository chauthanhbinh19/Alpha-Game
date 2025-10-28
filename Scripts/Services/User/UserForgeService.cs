using System.Collections.Generic;

public class UserForgeService : IUserForgeService
{
    private IUserForgeRepository _userForgeRepository;

    public UserForgeService(IUserForgeRepository userForgeRepository)
    {
        _userForgeRepository = userForgeRepository;
    }

    public static UserForgeService Create()
    {
        return new UserForgeService(new UserForgeRepository());
    }

    public Forges GetNewLevelPower(Forges c, double coefficient)
    {
        IForgeRepository _repository = new ForgeRepository();
        ForgeService _service = new ForgeService(_repository);
        Forges orginCard = _service.GetForgeById(c.Id);
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
    public Forges GetNewBreakthroughPower(Forges c, double coefficient)
    {
        IForgeRepository _repository = new ForgeRepository();
        ForgeService _service = new ForgeService(_repository);
        Forges orginCard = _service.GetForgeById(c.Id);
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

    public List<Forges> GetUserForge(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Forges> list = _userForgeRepository.GetUserForge(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserForgeCount(string user_id, string type, string rare)
    {
        return _userForgeRepository.GetUserForgeCount(user_id, type, rare);
    }

    public bool InsertUserForge(Forges forge)
    {
        return _userForgeRepository.InsertUserForge(forge);
    }

    public bool UpdateForgeLevel(Forges forge, int cardLevel)
    {
        return _userForgeRepository.UpdateForgeLevel(forge, cardLevel);
    }

    public bool UpdateForgeBreakthrough(Forges forge, int star, double quantity)
    {
        return _userForgeRepository.UpdateForgeBreakthrough(forge, star, quantity);
    }

    public Forges GetUserForgeById(string user_id, string Id)
    {
        return _userForgeRepository.GetUserForgeById(user_id, Id);
    }

    public Forges SumPowerUserForge()
    {
        return _userForgeRepository.SumPowerUserForge();
    }
}

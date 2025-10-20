using System.Collections.Generic;

public class UserTalismanService : IUserTalismanService
{
    private readonly IUserTalismanRepository _userTalismanRepository;

    public UserTalismanService(IUserTalismanRepository userTalismanRepository)
    {
        _userTalismanRepository = userTalismanRepository;
    }

    public static UserTalismanService Create()
    {
        return new UserTalismanService(new UserTalismanRepository());
    }

    public Talismans GetNewLevelPower(Talismans c, double coefficient)
    {
        ITalismanRepository _repository = new TalismanRepository();
        TalismanService _service = new TalismanService(_repository);
        Talismans orginCard = _service.GetTalismanById(c.Id);
        Talismans talisman = new Talismans
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
        talisman.Power = EvaluatePower.CalculatePower(
            talisman.Health,
            talisman.PhysicalAttack, talisman.PhysicalDefense,
            talisman.MagicalAttack, talisman.MagicalDefense,
            talisman.ChemicalAttack, talisman.ChemicalDefense,
            talisman.AtomicAttack, talisman.AtomicDefense,
            talisman.MentalAttack, talisman.MentalDefense,
            talisman.Speed,
            talisman.CriticalDamageRate, talisman.CriticalRate, talisman.CriticalResistanceRate, talisman.IgnoreCriticalRate,
            talisman.PenetrationRate, talisman.PenetrationResistanceRate, talisman.EvasionRate,
            talisman.DamageAbsorptionRate, talisman.IgnoreDamageAbsorptionRate, talisman.AbsorbedDamageRate,
            talisman.VitalityRegenerationRate, talisman.VitalityRegenerationResistanceRate,
            talisman.AccuracyRate, talisman.LifestealRate,
            talisman.ShieldStrength, talisman.Tenacity, talisman.ResistanceRate,
            talisman.ComboRate, talisman.IgnoreComboRate, talisman.ComboDamageRate, talisman.ComboResistanceRate,
            talisman.StunRate, talisman.IgnoreStunRate,
            talisman.ReflectionRate, talisman.IgnoreReflectionRate, talisman.ReflectionDamageRate, talisman.ReflectionResistanceRate,
            talisman.Mana, talisman.ManaRegenerationRate,
            talisman.DamageToDifferentFactionRate, talisman.ResistanceToDifferentFactionRate,
            talisman.DamageToSameFactionRate, talisman.ResistanceToSameFactionRate,
            talisman.NormalDamageRate, talisman.NormalResistanceRate,
            talisman.SkillDamageRate, talisman.SkillResistanceRate
        );
        return talisman;
    }
    public Talismans GetNewBreakthroughPower(Talismans c, double coefficient)
    {
        ITalismanRepository _repository = new TalismanRepository();
        TalismanService _service = new TalismanService(_repository);
        Talismans orginCard = _service.GetTalismanById(c.Id);
        Talismans talisman = new Talismans
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
        talisman.Power = EvaluatePower.CalculatePower(
            talisman.Health,
            talisman.PhysicalAttack, talisman.PhysicalDefense,
            talisman.MagicalAttack, talisman.MagicalDefense,
            talisman.ChemicalAttack, talisman.ChemicalDefense,
            talisman.AtomicAttack, talisman.AtomicDefense,
            talisman.MentalAttack, talisman.MentalDefense,
            talisman.Speed,
            talisman.CriticalDamageRate, talisman.CriticalRate, talisman.CriticalResistanceRate, talisman.IgnoreCriticalRate,
            talisman.PenetrationRate, talisman.PenetrationResistanceRate, talisman.EvasionRate,
            talisman.DamageAbsorptionRate, talisman.IgnoreDamageAbsorptionRate, talisman.AbsorbedDamageRate,
            talisman.VitalityRegenerationRate, talisman.VitalityRegenerationResistanceRate,
            talisman.AccuracyRate, talisman.LifestealRate,
            talisman.ShieldStrength, talisman.Tenacity, talisman.ResistanceRate,
            talisman.ComboRate, talisman.IgnoreComboRate, talisman.ComboDamageRate, talisman.ComboResistanceRate,
            talisman.StunRate, talisman.IgnoreStunRate,
            talisman.ReflectionRate, talisman.IgnoreReflectionRate, talisman.ReflectionDamageRate, talisman.ReflectionResistanceRate,
            talisman.Mana, talisman.ManaRegenerationRate,
            talisman.DamageToDifferentFactionRate, talisman.ResistanceToDifferentFactionRate,
            talisman.DamageToSameFactionRate, talisman.ResistanceToSameFactionRate,
            talisman.NormalDamageRate, talisman.NormalResistanceRate,
            talisman.SkillDamageRate, talisman.SkillResistanceRate
        );
        return talisman;
    }

    public List<Talismans> GetUserTalisman(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Talismans> list = _userTalismanRepository.GetUserTalisman(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserTalismanCount(string user_id, string type, string rare)
    {
        return _userTalismanRepository.GetUserTalismanCount(user_id, type, rare);
    }

    public bool InsertUserTalisman(Talismans talisman)
    {
        return _userTalismanRepository.InsertUserTalisman(talisman);
    }

    public bool UpdateTalismanLevel(Talismans talisman, int level)
    {
        return _userTalismanRepository.UpdateTalismanLevel(talisman, level);
    }

    public bool UpdateTalismanBreakthrough(Talismans talisman, int star, int quantity)
    {
        return _userTalismanRepository.UpdateTalismanBreakthrough(talisman, star, quantity);
    }

    public Talismans GetUserTalismanById(string user_id, string Id)
    {
        return _userTalismanRepository.GetUserTalismanById(user_id, Id);
    }

    public Talismans SumPowerUserTalisman()
    {
        return _userTalismanRepository.SumPowerUserTalisman();
    }
}

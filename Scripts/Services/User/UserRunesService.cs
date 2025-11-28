using System.Collections.Generic;

public class UserRunesService : IUserRunesService
{
    private readonly IUserRunesRepository _userRunesRepository;

    public UserRunesService(IUserRunesRepository userRunesRepository)
    {
        _userRunesRepository = userRunesRepository;
    }

    public static UserRunesService Create()
    {
        return new UserRunesService(new UserRunesRepository());
    }

    public Runes GetNewLevelPower(Runes c, double coefficient)
    {
        IRunesRepository _repository = new RunesRepository();
        RunesService _service = new RunesService(_repository);
        Runes orginCard = _service.GetRunesById(c.Id);
        Runes Runes = new Runes
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
        Runes.Power = EvaluatePower.CalculatePower(
            Runes.Health,
            Runes.PhysicalAttack, Runes.PhysicalDefense,
            Runes.MagicalAttack, Runes.MagicalDefense,
            Runes.ChemicalAttack, Runes.ChemicalDefense,
            Runes.AtomicAttack, Runes.AtomicDefense,
            Runes.MentalAttack, Runes.MentalDefense,
            Runes.Speed,
            Runes.CriticalDamageRate, Runes.CriticalRate, Runes.CriticalResistanceRate, Runes.IgnoreCriticalRate,
            Runes.PenetrationRate, Runes.PenetrationResistanceRate, Runes.EvasionRate,
            Runes.DamageAbsorptionRate, Runes.IgnoreDamageAbsorptionRate, Runes.AbsorbedDamageRate,
            Runes.VitalityRegenerationRate, Runes.VitalityRegenerationResistanceRate,
            Runes.AccuracyRate, Runes.LifestealRate,
            Runes.ShieldStrength, Runes.Tenacity, Runes.ResistanceRate,
            Runes.ComboRate, Runes.IgnoreComboRate, Runes.ComboDamageRate, Runes.ComboResistanceRate,
            Runes.StunRate, Runes.IgnoreStunRate,
            Runes.ReflectionRate, Runes.IgnoreReflectionRate, Runes.ReflectionDamageRate, Runes.ReflectionResistanceRate,
            Runes.Mana, Runes.ManaRegenerationRate,
            Runes.DamageToDifferentFactionRate, Runes.ResistanceToDifferentFactionRate,
            Runes.DamageToSameFactionRate, Runes.ResistanceToSameFactionRate,
            Runes.NormalDamageRate, Runes.NormalResistanceRate,
            Runes.SkillDamageRate, Runes.SkillResistanceRate
        );
        return Runes;
    }
    public Runes GetNewBreakthroughPower(Runes c, double coefficient)
    {
        IRunesRepository _repository = new RunesRepository();
        RunesService _service = new RunesService(_repository);
        Runes orginCard = _service.GetRunesById(c.Id);
        Runes Runes = new Runes
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
        Runes.Power = EvaluatePower.CalculatePower(
            Runes.Health,
            Runes.PhysicalAttack, Runes.PhysicalDefense,
            Runes.MagicalAttack, Runes.MagicalDefense,
            Runes.ChemicalAttack, Runes.ChemicalDefense,
            Runes.AtomicAttack, Runes.AtomicDefense,
            Runes.MentalAttack, Runes.MentalDefense,
            Runes.Speed,
            Runes.CriticalDamageRate, Runes.CriticalRate, Runes.CriticalResistanceRate, Runes.IgnoreCriticalRate,
            Runes.PenetrationRate, Runes.PenetrationResistanceRate, Runes.EvasionRate,
            Runes.DamageAbsorptionRate, Runes.IgnoreDamageAbsorptionRate, Runes.AbsorbedDamageRate,
            Runes.VitalityRegenerationRate, Runes.VitalityRegenerationResistanceRate,
            Runes.AccuracyRate, Runes.LifestealRate,
            Runes.ShieldStrength, Runes.Tenacity, Runes.ResistanceRate,
            Runes.ComboRate, Runes.IgnoreComboRate, Runes.ComboDamageRate, Runes.ComboResistanceRate,
            Runes.StunRate, Runes.IgnoreStunRate,
            Runes.ReflectionRate, Runes.IgnoreReflectionRate, Runes.ReflectionDamageRate, Runes.ReflectionResistanceRate,
            Runes.Mana, Runes.ManaRegenerationRate,
            Runes.DamageToDifferentFactionRate, Runes.ResistanceToDifferentFactionRate,
            Runes.DamageToSameFactionRate, Runes.ResistanceToSameFactionRate,
            Runes.NormalDamageRate, Runes.NormalResistanceRate,
            Runes.SkillDamageRate, Runes.SkillResistanceRate
        );
        return Runes;
    }

    public List<Runes> GetUserRunes(string user_id, int pageSize, int offset, string rare)
    {
        List<Runes> list = _userRunesRepository.GetUserRunes(user_id, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserRunesCount(string user_id, string rare)
    {
        return _userRunesRepository.GetUserRunesCount(user_id, rare);
    }

    public bool InsertUserRunes(Runes Runes, string userId)
    {
        return _userRunesRepository.InsertUserRunes(Runes, userId);
    }

    public bool UpdateRunesLevel(Runes Runes, int cardLevel)
    {
        return _userRunesRepository.UpdateRunesLevel(Runes, cardLevel);
    }

    public bool UpdateRunesBreakthrough(Runes Runes, int star, double quantity)
    {
        return _userRunesRepository.UpdateRunesBreakthrough(Runes, star, quantity);
    }

    public Runes GetUserRunesById(string user_id, string Id)
    {
        return _userRunesRepository.GetUserRunesById(user_id, Id);
    }

    public Runes SumPowerUserRunes()
    {
        return _userRunesRepository.SumPowerUserRunes();
    }
}

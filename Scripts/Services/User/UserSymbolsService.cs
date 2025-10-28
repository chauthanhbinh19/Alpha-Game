using System.Collections.Generic;

public class UserSymbolsService : IUserSymbolsService
{
    private readonly IUserSymbolsRepository _userSymbolsRepository;

    public UserSymbolsService(IUserSymbolsRepository userSymbolsRepository)
    {
        _userSymbolsRepository = userSymbolsRepository;
    }

    public static UserSymbolsService Create()
    {
        return new UserSymbolsService(new UserSymbolsRepository());
    }

    public Symbols GetNewLevelPower(Symbols c, double coefficient)
    {
        ISymbolsRepository _repository = new SymbolsRepository();
        SymbolsService _service = new SymbolsService(_repository);
        Symbols orginCard = _service.GetSymbolsById(c.Id);
        Symbols symbols = new Symbols
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
        symbols.Power = EvaluatePower.CalculatePower(
            symbols.Health,
            symbols.PhysicalAttack, symbols.PhysicalDefense,
            symbols.MagicalAttack, symbols.MagicalDefense,
            symbols.ChemicalAttack, symbols.ChemicalDefense,
            symbols.AtomicAttack, symbols.AtomicDefense,
            symbols.MentalAttack, symbols.MentalDefense,
            symbols.Speed,
            symbols.CriticalDamageRate, symbols.CriticalRate, symbols.CriticalResistanceRate, symbols.IgnoreCriticalRate,
            symbols.PenetrationRate, symbols.PenetrationResistanceRate, symbols.EvasionRate,
            symbols.DamageAbsorptionRate, symbols.IgnoreDamageAbsorptionRate, symbols.AbsorbedDamageRate,
            symbols.VitalityRegenerationRate, symbols.VitalityRegenerationResistanceRate,
            symbols.AccuracyRate, symbols.LifestealRate,
            symbols.ShieldStrength, symbols.Tenacity, symbols.ResistanceRate,
            symbols.ComboRate, symbols.IgnoreComboRate, symbols.ComboDamageRate, symbols.ComboResistanceRate,
            symbols.StunRate, symbols.IgnoreStunRate,
            symbols.ReflectionRate, symbols.IgnoreReflectionRate, symbols.ReflectionDamageRate, symbols.ReflectionResistanceRate,
            symbols.Mana, symbols.ManaRegenerationRate,
            symbols.DamageToDifferentFactionRate, symbols.ResistanceToDifferentFactionRate,
            symbols.DamageToSameFactionRate, symbols.ResistanceToSameFactionRate,
            symbols.NormalDamageRate, symbols.NormalResistanceRate,
            symbols.SkillDamageRate, symbols.SkillResistanceRate
        );
        return symbols;
    }
    public Symbols GetNewBreakthroughPower(Symbols c, double coefficient)
    {
        ISymbolsRepository _repository = new SymbolsRepository();
        SymbolsService _service = new SymbolsService(_repository);
        Symbols orginCard = _service.GetSymbolsById(c.Id);
        Symbols symbols = new Symbols
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
        symbols.Power = EvaluatePower.CalculatePower(
            symbols.Health,
            symbols.PhysicalAttack, symbols.PhysicalDefense,
            symbols.MagicalAttack, symbols.MagicalDefense,
            symbols.ChemicalAttack, symbols.ChemicalDefense,
            symbols.AtomicAttack, symbols.AtomicDefense,
            symbols.MentalAttack, symbols.MentalDefense,
            symbols.Speed,
            symbols.CriticalDamageRate, symbols.CriticalRate, symbols.CriticalResistanceRate, symbols.IgnoreCriticalRate,
            symbols.PenetrationRate, symbols.PenetrationResistanceRate, symbols.EvasionRate,
            symbols.DamageAbsorptionRate, symbols.IgnoreDamageAbsorptionRate, symbols.AbsorbedDamageRate,
            symbols.VitalityRegenerationRate, symbols.VitalityRegenerationResistanceRate,
            symbols.AccuracyRate, symbols.LifestealRate,
            symbols.ShieldStrength, symbols.Tenacity, symbols.ResistanceRate,
            symbols.ComboRate, symbols.IgnoreComboRate, symbols.ComboDamageRate, symbols.ComboResistanceRate,
            symbols.StunRate, symbols.IgnoreStunRate,
            symbols.ReflectionRate, symbols.IgnoreReflectionRate, symbols.ReflectionDamageRate, symbols.ReflectionResistanceRate,
            symbols.Mana, symbols.ManaRegenerationRate,
            symbols.DamageToDifferentFactionRate, symbols.ResistanceToDifferentFactionRate,
            symbols.DamageToSameFactionRate, symbols.ResistanceToSameFactionRate,
            symbols.NormalDamageRate, symbols.NormalResistanceRate,
            symbols.SkillDamageRate, symbols.SkillResistanceRate
        );
        return symbols;
    }

    public List<Symbols> GetUserSymbols(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Symbols> list = _userSymbolsRepository.GetUserSymbols(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserSymbolsCount(string user_id, string type, string rare)
    {
        return _userSymbolsRepository.GetUserSymbolsCount(user_id, type, rare);
    }

    public bool InsertUserSymbols(Symbols symbols)
    {
        return _userSymbolsRepository.InsertUserSymbols(symbols);
    }

    public bool UpdateSymbolsLevel(Symbols symbols, int cardLevel)
    {
        return _userSymbolsRepository.UpdateSymbolsLevel(symbols, cardLevel);
    }

    public bool UpdateSymbolsBreakthrough(Symbols symbols, int star, double quantity)
    {
        return _userSymbolsRepository.UpdateSymbolsBreakthrough(symbols, star, quantity);
    }

    public Symbols GetUserSymbolsById(string user_id, string Id)
    {
        return _userSymbolsRepository.GetUserSymbolsById(user_id, Id);
    }

    public Symbols SumPowerUserSymbols()
    {
        return _userSymbolsRepository.SumPowerUserSymbols();
    }
}

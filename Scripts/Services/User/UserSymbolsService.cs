using System.Collections.Generic;
using System.Threading.Tasks;

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

    public async Task<Symbols> GetNewLevelPowerAsync(Symbols c, double coefficient)
    {
        ISymbolsRepository _repository = new SymbolsRepository();
        SymbolsService _service = new SymbolsService(_repository);
        Symbols orginCard = await _service.GetSymbolByIdAsync(c.Id);
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
    public async Task<Symbols> GetNewBreakthroughPowerAsync(Symbols c, double coefficient)
    {
        ISymbolsRepository _repository = new SymbolsRepository();
        SymbolsService _service = new SymbolsService(_repository);
        Symbols orginCard = await _service.GetSymbolByIdAsync(c.Id);
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

    public async Task<List<Symbols>> GetUserSymbolsAsync(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Symbols> list = await _userSymbolsRepository.GetUserSymbolsAsync(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserSymbolsCountAsync(string user_id, string type, string rare)
    {
        return await _userSymbolsRepository.GetUserSymbolsCountAsync(user_id, type, rare);
    }

    public async Task<bool> InsertUserSymbolAsync(Symbols symbols, string userId)
    {
        return await _userSymbolsRepository.InsertUserSymbolAsync(symbols, userId);
    }

    public async Task<bool> UpdateSymbolLevelAsync(Symbols symbols, int cardLevel)
    {
        return await _userSymbolsRepository.UpdateSymbolLevelAsync(symbols, cardLevel);
    }

    public async Task<bool> UpdateSymbolBreakthroughAsync(Symbols symbols, int star, double quantity)
    {
        return await _userSymbolsRepository.UpdateSymbolBreakthroughAsync(symbols, star, quantity);
    }

    public async Task<Symbols> GetUserSymbolByIdAsync(string user_id, string Id)
    {
        return await _userSymbolsRepository.GetUserSymbolByIdAsync(user_id, Id);
    }

    public async Task<Symbols> SumPowerUserSymbolsAsync()
    {
        return await _userSymbolsRepository.SumPowerUserSymbolsAsync();
    }
}

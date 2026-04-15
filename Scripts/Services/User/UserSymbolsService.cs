using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSymbolsService : IUserSymbolsService
{
     private static UserSymbolsService _instance;
    private readonly IUserSymbolsRepository _userSymbolsRepository;

    public UserSymbolsService(IUserSymbolsRepository userSymbolsRepository)
    {
        _userSymbolsRepository = userSymbolsRepository;
    }

    public static UserSymbolsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserSymbolsService(new UserSymbolsRepository());
        }
        return _instance;
    }

    public async Task<Symbols> GetNewLevelPowerAsync(Symbols c, double coefficient)
    {
        ISymbolsRepository _repository = new SymbolsRepository();
        SymbolsService _service = new SymbolsService(_repository);
        Symbols orginCard = await _service.GetSymbolByIdAsync(c.Id);
        Symbols symbol = new Symbols
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
        symbol.Power = PowerHelper.CalculatePower(
            symbol.Health,
            symbol.PhysicalAttack, symbol.PhysicalDefense,
            symbol.MagicalAttack, symbol.MagicalDefense,
            symbol.ChemicalAttack, symbol.ChemicalDefense,
            symbol.AtomicAttack, symbol.AtomicDefense,
            symbol.MentalAttack, symbol.MentalDefense,
            symbol.Speed,
            symbol.CriticalDamageRate, symbol.CriticalRate, symbol.CriticalResistanceRate, symbol.IgnoreCriticalRate,
            symbol.PenetrationRate, symbol.PenetrationResistanceRate, symbol.EvasionRate,
            symbol.DamageAbsorptionRate, symbol.IgnoreDamageAbsorptionRate, symbol.AbsorbedDamageRate,
            symbol.VitalityRegenerationRate, symbol.VitalityRegenerationResistanceRate,
            symbol.AccuracyRate, symbol.LifestealRate,
            symbol.ShieldStrength, symbol.Tenacity, symbol.ResistanceRate,
            symbol.ComboRate, symbol.IgnoreComboRate, symbol.ComboDamageRate, symbol.ComboResistanceRate,
            symbol.StunRate, symbol.IgnoreStunRate,
            symbol.ReflectionRate, symbol.IgnoreReflectionRate, symbol.ReflectionDamageRate, symbol.ReflectionResistanceRate,
            symbol.Mana, symbol.ManaRegenerationRate,
            symbol.DamageToDifferentFactionRate, symbol.ResistanceToDifferentFactionRate,
            symbol.DamageToSameFactionRate, symbol.ResistanceToSameFactionRate,
            symbol.NormalDamageRate, symbol.NormalResistanceRate,
            symbol.SkillDamageRate, symbol.SkillResistanceRate
        );
        return symbol;
    }
    public async Task<Symbols> GetNewBreakthroughPowerAsync(Symbols c, double coefficient)
    {
        ISymbolsRepository _repository = new SymbolsRepository();
        SymbolsService _service = new SymbolsService(_repository);
        Symbols orginCard = await _service.GetSymbolByIdAsync(c.Id);
        Symbols symbol = new Symbols
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
        symbol.Power = PowerHelper.CalculatePower(
            symbol.Health,
            symbol.PhysicalAttack, symbol.PhysicalDefense,
            symbol.MagicalAttack, symbol.MagicalDefense,
            symbol.ChemicalAttack, symbol.ChemicalDefense,
            symbol.AtomicAttack, symbol.AtomicDefense,
            symbol.MentalAttack, symbol.MentalDefense,
            symbol.Speed,
            symbol.CriticalDamageRate, symbol.CriticalRate, symbol.CriticalResistanceRate, symbol.IgnoreCriticalRate,
            symbol.PenetrationRate, symbol.PenetrationResistanceRate, symbol.EvasionRate,
            symbol.DamageAbsorptionRate, symbol.IgnoreDamageAbsorptionRate, symbol.AbsorbedDamageRate,
            symbol.VitalityRegenerationRate, symbol.VitalityRegenerationResistanceRate,
            symbol.AccuracyRate, symbol.LifestealRate,
            symbol.ShieldStrength, symbol.Tenacity, symbol.ResistanceRate,
            symbol.ComboRate, symbol.IgnoreComboRate, symbol.ComboDamageRate, symbol.ComboResistanceRate,
            symbol.StunRate, symbol.IgnoreStunRate,
            symbol.ReflectionRate, symbol.IgnoreReflectionRate, symbol.ReflectionDamageRate, symbol.ReflectionResistanceRate,
            symbol.Mana, symbol.ManaRegenerationRate,
            symbol.DamageToDifferentFactionRate, symbol.ResistanceToDifferentFactionRate,
            symbol.DamageToSameFactionRate, symbol.ResistanceToSameFactionRate,
            symbol.NormalDamageRate, symbol.NormalResistanceRate,
            symbol.SkillDamageRate, symbol.SkillResistanceRate
        );
        return symbol;
    }

    public async Task<List<Symbols>> GetUserSymbolsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Symbols> list = await _userSymbolsRepository.GetUserSymbolsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSymbolsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userSymbolsRepository.GetUserSymbolsCountAsync(user_id, search, type, rare);
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

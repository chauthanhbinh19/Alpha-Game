using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArchitecturesService : IUserArchitecturesService
{
     private static UserArchitecturesService _instance;
    private readonly IUserArchitecturesRepository _userArchitecturesRepository;

    public UserArchitecturesService(IUserArchitecturesRepository userArchitecturesRepository)
    {
        _userArchitecturesRepository = userArchitecturesRepository;
    }

    public static UserArchitecturesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserArchitecturesService(new UserArchitecturesRepository());
        }
        return _instance;
    }

    public async Task<Architectures> GetNewLevelPowerAsync(Architectures c, double coefficient)
    {
        IArchitecturesRepository _repository = new ArchitecturesRepository();
        ArchitecturesService _service = new ArchitecturesService(_repository);
        Architectures orginCard = await _service.GetArchitectureByIdAsync(c.Id);
        Architectures architecture = new Architectures
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
        architecture.Power = EvaluatePower.CalculatePower(
            architecture.Health,
            architecture.PhysicalAttack, architecture.PhysicalDefense,
            architecture.MagicalAttack, architecture.MagicalDefense,
            architecture.ChemicalAttack, architecture.ChemicalDefense,
            architecture.AtomicAttack, architecture.AtomicDefense,
            architecture.MentalAttack, architecture.MentalDefense,
            architecture.Speed,
            architecture.CriticalDamageRate, architecture.CriticalRate, architecture.CriticalResistanceRate, architecture.IgnoreCriticalRate,
            architecture.PenetrationRate, architecture.PenetrationResistanceRate, architecture.EvasionRate,
            architecture.DamageAbsorptionRate, architecture.IgnoreDamageAbsorptionRate, architecture.AbsorbedDamageRate,
            architecture.VitalityRegenerationRate, architecture.VitalityRegenerationResistanceRate,
            architecture.AccuracyRate, architecture.LifestealRate,
            architecture.ShieldStrength, architecture.Tenacity, architecture.ResistanceRate,
            architecture.ComboRate, architecture.IgnoreComboRate, architecture.ComboDamageRate, architecture.ComboResistanceRate,
            architecture.StunRate, architecture.IgnoreStunRate,
            architecture.ReflectionRate, architecture.IgnoreReflectionRate, architecture.ReflectionDamageRate, architecture.ReflectionResistanceRate,
            architecture.Mana, architecture.ManaRegenerationRate,
            architecture.DamageToDifferentFactionRate, architecture.ResistanceToDifferentFactionRate,
            architecture.DamageToSameFactionRate, architecture.ResistanceToSameFactionRate,
            architecture.NormalDamageRate, architecture.NormalResistanceRate,
            architecture.SkillDamageRate, architecture.SkillResistanceRate
        );
        return architecture;
    }
    public async Task<Architectures> GetNewBreakthroughPowerAsync(Architectures c, double coefficient)
    {
        IArchitecturesRepository _repository = new ArchitecturesRepository();
        ArchitecturesService _service = new ArchitecturesService(_repository);
        Architectures orginCard = await _service.GetArchitectureByIdAsync(c.Id);
        Architectures architecture = new Architectures
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
        architecture.Power = EvaluatePower.CalculatePower(
            architecture.Health,
            architecture.PhysicalAttack, architecture.PhysicalDefense,
            architecture.MagicalAttack, architecture.MagicalDefense,
            architecture.ChemicalAttack, architecture.ChemicalDefense,
            architecture.AtomicAttack, architecture.AtomicDefense,
            architecture.MentalAttack, architecture.MentalDefense,
            architecture.Speed,
            architecture.CriticalDamageRate, architecture.CriticalRate, architecture.CriticalResistanceRate, architecture.IgnoreCriticalRate,
            architecture.PenetrationRate, architecture.PenetrationResistanceRate, architecture.EvasionRate,
            architecture.DamageAbsorptionRate, architecture.IgnoreDamageAbsorptionRate, architecture.AbsorbedDamageRate,
            architecture.VitalityRegenerationRate, architecture.VitalityRegenerationResistanceRate,
            architecture.AccuracyRate, architecture.LifestealRate,
            architecture.ShieldStrength, architecture.Tenacity, architecture.ResistanceRate,
            architecture.ComboRate, architecture.IgnoreComboRate, architecture.ComboDamageRate, architecture.ComboResistanceRate,
            architecture.StunRate, architecture.IgnoreStunRate,
            architecture.ReflectionRate, architecture.IgnoreReflectionRate, architecture.ReflectionDamageRate, architecture.ReflectionResistanceRate,
            architecture.Mana, architecture.ManaRegenerationRate,
            architecture.DamageToDifferentFactionRate, architecture.ResistanceToDifferentFactionRate,
            architecture.DamageToSameFactionRate, architecture.ResistanceToSameFactionRate,
            architecture.NormalDamageRate, architecture.NormalResistanceRate,
            architecture.SkillDamageRate, architecture.SkillResistanceRate
        );
        return architecture;
    }

    public async Task<List<Architectures>> GetUserArchitecturesAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Architectures> list = await _userArchitecturesRepository.GetUserArchitecturesAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserArchitecturesCountAsync(string user_id, string search, string rare)
    {
        return await _userArchitecturesRepository.GetUserArchitecturesCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserArchitectureAsync(Architectures Architectures, string userId)
    {
        return await _userArchitecturesRepository.InsertUserArchitectureAsync(Architectures, userId);
    }

    public async Task<bool> UpdateArchitectureLevelAsync(Architectures Architectures, int cardLevel)
    {
        return await _userArchitecturesRepository.UpdateArchitectureLevelAsync(Architectures, cardLevel);
    }

    public async Task<bool> UpdateArchitectureBreakthroughAsync(Architectures Architectures, int star, double quantity)
    {
        return await _userArchitecturesRepository.UpdateArchitectureBreakthroughAsync(Architectures, star, quantity);
    }

    public async Task<Architectures> GetUserArchitectureByIdAsync(string user_id, string Id)
    {
        return await _userArchitecturesRepository.GetUserArchitectureByIdAsync(user_id, Id);
    }

    public async Task<Architectures> SumPowerUserArchitecturesAsync()
    {
        return await _userArchitecturesRepository.SumPowerUserArchitecturesAsync();
    }
}

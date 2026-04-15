using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArtifactsService : IUserArtifactsService
{
     private static UserArtifactsService _instance;
    private readonly IUserArtifactsRepository _userArtifactsRepository;

    public UserArtifactsService(IUserArtifactsRepository userArtifactsRepository)
    {
        _userArtifactsRepository = userArtifactsRepository;
    }

    public static UserArtifactsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserArtifactsService(new UserArtifactsRepository());
        }
        return _instance;
    }

    public async Task<Artifacts> GetNewLevelPowerAsync(Artifacts c, double coefficient)
    {
        IArtifactsRepository _repository = new ArtifactsRepository();
        ArtifactsService _service = new ArtifactsService(_repository);
        Artifacts orginArtifact = await _service.GetArtifactByIdAsync(c.Id);
        Artifacts artifact = new Artifacts
        {
            Id = c.Id,
            Health = c.Health + orginArtifact.Health * coefficient,
            PhysicalAttack = c.PhysicalAttack + orginArtifact.PhysicalAttack * coefficient,
            PhysicalDefense = c.PhysicalDefense + orginArtifact.PhysicalDefense * coefficient,
            MagicalAttack = c.MagicalAttack + orginArtifact.MagicalAttack * coefficient,
            MagicalDefense = c.MagicalDefense + orginArtifact.MagicalDefense * coefficient,
            ChemicalAttack = c.ChemicalAttack + orginArtifact.ChemicalAttack * coefficient,
            ChemicalDefense = c.ChemicalDefense + orginArtifact.ChemicalDefense * coefficient,
            AtomicAttack = c.AtomicAttack + orginArtifact.AtomicAttack * coefficient,
            AtomicDefense = c.AtomicDefense + orginArtifact.AtomicDefense * coefficient,
            MentalAttack = c.MentalAttack + orginArtifact.MentalAttack * coefficient,
            MentalDefense = c.MentalDefense + orginArtifact.MentalDefense * coefficient,
            Speed = c.Speed + orginArtifact.Speed * coefficient,
            CriticalDamageRate = c.CriticalDamageRate + orginArtifact.CriticalDamageRate * coefficient,
            CriticalRate = c.CriticalRate + orginArtifact.CriticalRate * coefficient,
            CriticalResistanceRate = c.CriticalResistanceRate + orginArtifact.CriticalResistanceRate * coefficient,
            IgnoreCriticalRate = c.IgnoreCriticalRate + orginArtifact.IgnoreCriticalRate * coefficient,
            PenetrationRate = c.PenetrationRate + orginArtifact.PenetrationRate * coefficient,
            PenetrationResistanceRate = c.PenetrationResistanceRate + orginArtifact.PenetrationResistanceRate * coefficient,
            EvasionRate = c.EvasionRate + orginArtifact.EvasionRate * coefficient,
            DamageAbsorptionRate = c.DamageAbsorptionRate + orginArtifact.DamageAbsorptionRate * coefficient,
            IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + orginArtifact.IgnoreDamageAbsorptionRate * coefficient,
            AbsorbedDamageRate = c.AbsorbedDamageRate + orginArtifact.AbsorbedDamageRate * coefficient,
            VitalityRegenerationRate = c.VitalityRegenerationRate + orginArtifact.VitalityRegenerationRate * coefficient,
            VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + orginArtifact.VitalityRegenerationResistanceRate * coefficient,
            AccuracyRate = c.AccuracyRate + orginArtifact.AccuracyRate * coefficient,
            LifestealRate = c.LifestealRate + orginArtifact.LifestealRate * coefficient,
            ShieldStrength = c.ShieldStrength + orginArtifact.ShieldStrength * coefficient,
            Tenacity = c.Tenacity + orginArtifact.Tenacity * coefficient,
            ResistanceRate = c.ResistanceRate + orginArtifact.ResistanceRate * coefficient,
            ComboRate = c.ComboRate + orginArtifact.ComboRate * coefficient,
            IgnoreComboRate = c.IgnoreComboRate + orginArtifact.IgnoreComboRate * coefficient,
            ComboDamageRate = c.ComboDamageRate + orginArtifact.ComboDamageRate * coefficient,
            ComboResistanceRate = c.ComboResistanceRate + orginArtifact.ComboResistanceRate * coefficient,
            StunRate = c.StunRate + orginArtifact.StunRate * coefficient,
            IgnoreStunRate = c.IgnoreStunRate + orginArtifact.IgnoreStunRate * coefficient,
            ReflectionRate = c.ReflectionRate + orginArtifact.ReflectionRate * coefficient,
            IgnoreReflectionRate  = c.IgnoreReflectionRate + orginArtifact.IgnoreReflectionRate * coefficient,
            ReflectionDamageRate = c.ReflectionDamageRate + orginArtifact.ReflectionDamageRate * coefficient,
            ReflectionResistanceRate = c.ReflectionResistanceRate + orginArtifact.ReflectionResistanceRate * coefficient,
            Mana = c.Mana + orginArtifact.Mana * (float)coefficient,
            ManaRegenerationRate = c.ManaRegenerationRate + orginArtifact.ManaRegenerationRate * coefficient,
            DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + orginArtifact.DamageToDifferentFactionRate * coefficient,
            ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + orginArtifact.ResistanceToDifferentFactionRate * coefficient,
            DamageToSameFactionRate = c.DamageToSameFactionRate + orginArtifact.DamageToSameFactionRate * coefficient,
            ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + orginArtifact.ResistanceToSameFactionRate * coefficient,
            NormalDamageRate = c.NormalDamageRate + orginArtifact.NormalDamageRate * coefficient,
            NormalResistanceRate = c.NormalResistanceRate + orginArtifact.NormalResistanceRate * coefficient,
            SkillDamageRate = c.SkillDamageRate + orginArtifact.SkillDamageRate * coefficient,
            SkillResistanceRate = c.SkillResistanceRate + orginArtifact.SkillResistanceRate * coefficient
        };
        artifact.Power = PowerHelper.CalculatePower(
            artifact.Health,
            artifact.PhysicalAttack, artifact.PhysicalDefense,
            artifact.MagicalAttack, artifact.MagicalDefense,
            artifact.ChemicalAttack, artifact.ChemicalDefense,
            artifact.AtomicAttack, artifact.AtomicDefense,
            artifact.MentalAttack, artifact.MentalDefense,
            artifact.Speed,
            artifact.CriticalDamageRate, artifact.CriticalRate, artifact.CriticalResistanceRate, artifact.IgnoreCriticalRate,
            artifact.PenetrationRate, artifact.PenetrationResistanceRate, artifact.EvasionRate,
            artifact.DamageAbsorptionRate, artifact.IgnoreDamageAbsorptionRate, artifact.AbsorbedDamageRate,
            artifact.VitalityRegenerationRate, artifact.VitalityRegenerationResistanceRate,
            artifact.AccuracyRate, artifact.LifestealRate,
            artifact.ShieldStrength, artifact.Tenacity, artifact.ResistanceRate,
            artifact.ComboRate, artifact.IgnoreComboRate, artifact.ComboDamageRate, artifact.ComboResistanceRate,
            artifact.StunRate, artifact.IgnoreStunRate,
            artifact.ReflectionRate, artifact.IgnoreReflectionRate, artifact.ReflectionDamageRate, artifact.ReflectionResistanceRate,
            artifact.Mana, artifact.ManaRegenerationRate,
            artifact.DamageToDifferentFactionRate, artifact.ResistanceToDifferentFactionRate,
            artifact.DamageToSameFactionRate, artifact.ResistanceToSameFactionRate,
            artifact.NormalDamageRate, artifact.NormalResistanceRate,
            artifact.SkillDamageRate, artifact.SkillResistanceRate
        );
        return artifact;
    }
    public async Task<Artifacts> GetNewBreakthroughPowerAsync(Artifacts c, double coefficient)
    {
        IArtifactsRepository _repository = new ArtifactsRepository();
        ArtifactsService _service = new ArtifactsService(_repository);
        Artifacts orginArtifact = await _service.GetArtifactByIdAsync(c.Id);
        Artifacts artifact = new Artifacts
        {
            Id = c.Id,
            Health = c.Health + orginArtifact.Health * coefficient,
            PhysicalAttack = c.PhysicalAttack + orginArtifact.PhysicalAttack * coefficient,
            PhysicalDefense = c.PhysicalDefense + orginArtifact.PhysicalDefense * coefficient,
            MagicalAttack = c.MagicalAttack + orginArtifact.MagicalAttack * coefficient,
            MagicalDefense = c.MagicalDefense + orginArtifact.MagicalDefense * coefficient,
            ChemicalAttack = c.ChemicalAttack + orginArtifact.ChemicalAttack * coefficient,
            ChemicalDefense = c.ChemicalDefense + orginArtifact.ChemicalDefense * coefficient,
            AtomicAttack = c.AtomicAttack + orginArtifact.AtomicAttack * coefficient,
            AtomicDefense = c.AtomicDefense + orginArtifact.AtomicDefense * coefficient,
            MentalAttack = c.MentalAttack + orginArtifact.MentalAttack * coefficient,
            MentalDefense = c.MentalDefense + orginArtifact.MentalDefense * coefficient,
            Speed = c.Speed + orginArtifact.Speed * coefficient,
            CriticalDamageRate = c.CriticalDamageRate + orginArtifact.CriticalDamageRate * coefficient,
            CriticalRate = c.CriticalRate + orginArtifact.CriticalRate * coefficient,
            CriticalResistanceRate = c.CriticalResistanceRate + orginArtifact.CriticalResistanceRate * coefficient,
            IgnoreCriticalRate = c.IgnoreCriticalRate + orginArtifact.IgnoreCriticalRate * coefficient,
            PenetrationRate = c.PenetrationRate + orginArtifact.PenetrationRate * coefficient,
            PenetrationResistanceRate = c.PenetrationResistanceRate + orginArtifact.PenetrationResistanceRate * coefficient,
            EvasionRate = c.EvasionRate + orginArtifact.EvasionRate * coefficient,
            DamageAbsorptionRate = c.DamageAbsorptionRate + orginArtifact.DamageAbsorptionRate * coefficient,
            IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + orginArtifact.IgnoreDamageAbsorptionRate * coefficient,
            AbsorbedDamageRate = c.AbsorbedDamageRate + orginArtifact.AbsorbedDamageRate * coefficient,
            VitalityRegenerationRate = c.VitalityRegenerationRate + orginArtifact.VitalityRegenerationRate * coefficient,
            VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + orginArtifact.VitalityRegenerationResistanceRate * coefficient,
            AccuracyRate = c.AccuracyRate + orginArtifact.AccuracyRate * coefficient,
            LifestealRate = c.LifestealRate + orginArtifact.LifestealRate * coefficient,
            ShieldStrength = c.ShieldStrength + orginArtifact.ShieldStrength * coefficient,
            Tenacity = c.Tenacity + orginArtifact.Tenacity * coefficient,
            ResistanceRate = c.ResistanceRate + orginArtifact.ResistanceRate * coefficient,
            ComboRate = c.ComboRate + orginArtifact.ComboRate * coefficient,
            IgnoreComboRate = c.IgnoreComboRate + orginArtifact.IgnoreComboRate * coefficient,
            ComboDamageRate = c.ComboDamageRate + orginArtifact.ComboDamageRate * coefficient,
            ComboResistanceRate = c.ComboResistanceRate + orginArtifact.ComboResistanceRate * coefficient,
            StunRate = c.StunRate + orginArtifact.StunRate * coefficient,
            IgnoreStunRate = c.IgnoreStunRate + orginArtifact.IgnoreStunRate * coefficient,
            ReflectionRate = c.ReflectionRate + orginArtifact.ReflectionRate * coefficient,
            IgnoreReflectionRate  = c.IgnoreReflectionRate + orginArtifact.IgnoreReflectionRate * coefficient,
            ReflectionDamageRate = c.ReflectionDamageRate + orginArtifact.ReflectionDamageRate * coefficient,
            ReflectionResistanceRate = c.ReflectionResistanceRate + orginArtifact.ReflectionResistanceRate * coefficient,
            Mana = c.Mana + orginArtifact.Mana * (float)coefficient,
            ManaRegenerationRate = c.ManaRegenerationRate + orginArtifact.ManaRegenerationRate * coefficient,
            DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + orginArtifact.DamageToDifferentFactionRate * coefficient,
            ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + orginArtifact.ResistanceToDifferentFactionRate * coefficient,
            DamageToSameFactionRate = c.DamageToSameFactionRate + orginArtifact.DamageToSameFactionRate * coefficient,
            ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + orginArtifact.ResistanceToSameFactionRate * coefficient,
            NormalDamageRate = c.NormalDamageRate + orginArtifact.NormalDamageRate * coefficient,
            NormalResistanceRate = c.NormalResistanceRate + orginArtifact.NormalResistanceRate * coefficient,
            SkillDamageRate = c.SkillDamageRate + orginArtifact.SkillDamageRate * coefficient,
            SkillResistanceRate = c.SkillResistanceRate + orginArtifact.SkillResistanceRate * coefficient
        };
        artifact.Power = PowerHelper.CalculatePower(
            artifact.Health,
            artifact.PhysicalAttack, artifact.PhysicalDefense,
            artifact.MagicalAttack, artifact.MagicalDefense,
            artifact.ChemicalAttack, artifact.ChemicalDefense,
            artifact.AtomicAttack, artifact.AtomicDefense,
            artifact.MentalAttack, artifact.MentalDefense,
            artifact.Speed,
            artifact.CriticalDamageRate, artifact.CriticalRate, artifact.CriticalResistanceRate, artifact.IgnoreCriticalRate,
            artifact.PenetrationRate, artifact.PenetrationResistanceRate, artifact.EvasionRate,
            artifact.DamageAbsorptionRate, artifact.IgnoreDamageAbsorptionRate, artifact.AbsorbedDamageRate,
            artifact.VitalityRegenerationRate, artifact.VitalityRegenerationResistanceRate,
            artifact.AccuracyRate, artifact.LifestealRate,
            artifact.ShieldStrength, artifact.Tenacity, artifact.ResistanceRate,
            artifact.ComboRate, artifact.IgnoreComboRate, artifact.ComboDamageRate, artifact.ComboResistanceRate,
            artifact.StunRate, artifact.IgnoreStunRate,
            artifact.ReflectionRate, artifact.IgnoreReflectionRate, artifact.ReflectionDamageRate, artifact.ReflectionResistanceRate,
            artifact.Mana, artifact.ManaRegenerationRate,
            artifact.DamageToDifferentFactionRate, artifact.ResistanceToDifferentFactionRate,
            artifact.DamageToSameFactionRate, artifact.ResistanceToSameFactionRate,
            artifact.NormalDamageRate, artifact.NormalResistanceRate,
            artifact.SkillDamageRate, artifact.SkillResistanceRate
        );
        return artifact;
    }

    public async Task<List<Artifacts>> GetUserArtifactsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Artifacts> list = await _userArtifactsRepository.GetUserArtifactsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserArtifactsCountAsync(string user_id, string search, string rare)
    {
        return await _userArtifactsRepository.GetUserArtifactsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserArtifactAsync(Artifacts Artifacts, string userId)
    {
        return await _userArtifactsRepository.InsertUserArtifactAsync(Artifacts, userId);
    }

    public async Task<bool> UpdateArtifactLevelAsync(Artifacts Artifacts, int cardLevel)
    {
        return await _userArtifactsRepository.UpdateArtifactLevelAsync(Artifacts, cardLevel);
    }

    public async Task<bool> UpdateArtifactBreakthroughAsync(Artifacts Artifacts, int star, double quantity)
    {
        return await _userArtifactsRepository.UpdateArtifactBreakthroughAsync(Artifacts, star, quantity);
    }

    public async Task<Artifacts> GetUserArtifactByIdAsync(string user_id, string Id)
    {
        return await _userArtifactsRepository.GetUserArtifactByIdAsync(user_id, Id);
    }

    public async Task<Artifacts> SumPowerUserArtifactsAsync()
    {
        return await _userArtifactsRepository.SumPowerUserArtifactsAsync();
    }
}

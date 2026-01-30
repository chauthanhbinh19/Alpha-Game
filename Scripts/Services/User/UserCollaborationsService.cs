using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCollaborationsService : IUserCollaborationsService
{
    private readonly IUserCollaborationsRepository _userCollaborationRepository;

    public UserCollaborationsService(IUserCollaborationsRepository userCollaborationRepository)
    {
        _userCollaborationRepository = userCollaborationRepository;
    }

    public static UserCollaborationsService Create()
    {
        return new UserCollaborationsService(new UserCollaborationsRepository());
    }

    public async Task<Collaborations> GetNewLevelPowerAsync(Collaborations c, double coefficient)
    {
        ICollaborationsRepository _repository = new CollaborationsRepository();
        CollaborationsService _service = new CollaborationsService(_repository);
        Collaborations orginCard = await _service.GetCollaborationByIdAsync(c.Id);
        Collaborations collaboration = new Collaborations
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
        collaboration.Power = EvaluatePower.CalculatePower(
            collaboration.Health,
            collaboration.PhysicalAttack, collaboration.PhysicalDefense,
            collaboration.MagicalAttack, collaboration.MagicalDefense,
            collaboration.ChemicalAttack, collaboration.ChemicalDefense,
            collaboration.AtomicAttack, collaboration.AtomicDefense,
            collaboration.MentalAttack, collaboration.MentalDefense,
            collaboration.Speed,
            collaboration.CriticalDamageRate, collaboration.CriticalRate, collaboration.CriticalResistanceRate, collaboration.IgnoreCriticalRate,
            collaboration.PenetrationRate, collaboration.PenetrationResistanceRate, collaboration.EvasionRate,
            collaboration.DamageAbsorptionRate, collaboration.IgnoreDamageAbsorptionRate, collaboration.AbsorbedDamageRate,
            collaboration.VitalityRegenerationRate, collaboration.VitalityRegenerationResistanceRate,
            collaboration.AccuracyRate, collaboration.LifestealRate,
            collaboration.ShieldStrength, collaboration.Tenacity, collaboration.ResistanceRate,
            collaboration.ComboRate, collaboration.IgnoreComboRate, collaboration.ComboDamageRate, collaboration.ComboResistanceRate,
            collaboration.StunRate, collaboration.IgnoreStunRate,
            collaboration.ReflectionRate, collaboration.IgnoreReflectionRate, collaboration.ReflectionDamageRate, collaboration.ReflectionResistanceRate,
            collaboration.Mana, collaboration.ManaRegenerationRate,
            collaboration.DamageToDifferentFactionRate, collaboration.ResistanceToDifferentFactionRate,
            collaboration.DamageToSameFactionRate, collaboration.ResistanceToSameFactionRate,
            collaboration.NormalDamageRate, collaboration.NormalResistanceRate,
            collaboration.SkillDamageRate, collaboration.SkillResistanceRate
        );
        return collaboration;
    }
    public async Task<Collaborations> GetNewBreakthroughPowerAsync(Collaborations c, double coefficient)
    {
        ICollaborationsRepository _repository = new CollaborationsRepository();
        CollaborationsService _service = new CollaborationsService(_repository);
        Collaborations orginCard = await _service.GetCollaborationByIdAsync(c.Id);
        Collaborations collaboration = new Collaborations
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
        collaboration.Power = EvaluatePower.CalculatePower(
            collaboration.Health,
            collaboration.PhysicalAttack, collaboration.PhysicalDefense,
            collaboration.MagicalAttack, collaboration.MagicalDefense,
            collaboration.ChemicalAttack, collaboration.ChemicalDefense,
            collaboration.AtomicAttack, collaboration.AtomicDefense,
            collaboration.MentalAttack, collaboration.MentalDefense,
            collaboration.Speed,
            collaboration.CriticalDamageRate, collaboration.CriticalRate, collaboration.CriticalResistanceRate, collaboration.IgnoreCriticalRate,
            collaboration.PenetrationRate, collaboration.PenetrationResistanceRate, collaboration.EvasionRate,
            collaboration.DamageAbsorptionRate, collaboration.IgnoreDamageAbsorptionRate, collaboration.AbsorbedDamageRate,
            collaboration.VitalityRegenerationRate, collaboration.VitalityRegenerationResistanceRate,
            collaboration.AccuracyRate, collaboration.LifestealRate,
            collaboration.ShieldStrength, collaboration.Tenacity, collaboration.ResistanceRate,
            collaboration.ComboRate, collaboration.IgnoreComboRate, collaboration.ComboDamageRate, collaboration.ComboResistanceRate,
            collaboration.StunRate, collaboration.IgnoreStunRate,
            collaboration.ReflectionRate, collaboration.IgnoreReflectionRate, collaboration.ReflectionDamageRate, collaboration.ReflectionResistanceRate,
            collaboration.Mana, collaboration.ManaRegenerationRate,
            collaboration.DamageToDifferentFactionRate, collaboration.ResistanceToDifferentFactionRate,
            collaboration.DamageToSameFactionRate, collaboration.ResistanceToSameFactionRate,
            collaboration.NormalDamageRate, collaboration.NormalResistanceRate,
            collaboration.SkillDamageRate, collaboration.SkillResistanceRate
        );
        return collaboration;
    }

    public async Task<List<Collaborations>> GetUserCollaborationsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Collaborations> list = await _userCollaborationRepository.GetUserCollaborationsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserCollaborationsCountAsync(string user_id, string search, string rare)
    {
        return await _userCollaborationRepository.GetUserCollaborationsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserCollaborationAsync(Collaborations collaboration, string userId)
    {
        return await _userCollaborationRepository.InsertUserCollaborationAsync(collaboration, userId);
    }

    public async Task<bool> UpdateCollaborationLevelAsync(Collaborations collaboration, int cardLevel)
    {
        return await _userCollaborationRepository.UpdateCollaborationLevelAsync(collaboration, cardLevel);
    }

    public async Task<bool> UpdateCollaborationBreakthroughAsync(Collaborations collaboration, int star, double quantity)
    {
        return await _userCollaborationRepository.UpdateCollaborationBreakthroughAsync(collaboration, star, quantity);
    }

    public async Task<Collaborations> GetUserCollaborationByIdAsync(string user_id, string Id)
    {
        return await _userCollaborationRepository.GetUserCollaborationByIdAsync(user_id, Id);
    }

    public async Task<Collaborations> SumPowerUserCollaborationsAsync()
    {
        return await _userCollaborationRepository.SumPowerUserCollaborationsAsync();
    }
}

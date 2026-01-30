using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCollaborationEquipmentsService : IUserCollaborationEquipmentsService
{
    private readonly IUserCollaborationEquipmentsRepository _userCollabEquipmentsRepo;

    public UserCollaborationEquipmentsService(IUserCollaborationEquipmentsRepository userCollabEquipmentsRepo)
    {
        _userCollabEquipmentsRepo = userCollabEquipmentsRepo;
    }

    public static UserCollaborationEquipmentsService Create()
    {
        return new UserCollaborationEquipmentsService(new UserCollaborationEquipmentsRepository());
    }

    public async Task<CollaborationEquipments> GetNewLevelPowerAsync(CollaborationEquipments c, double coefficient)
    {
        ICollaborationEquipmentsRepository _repository = new CollaborationEquipmentsRepository();
        CollaborationEquipmentsService _service = new CollaborationEquipmentsService(_repository);
        CollaborationEquipments orginCard = await _service.GetCollaborationEquipmentByIdAsync(c.Id);
        CollaborationEquipments collaborationEquipment = new CollaborationEquipments
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
        collaborationEquipment.Power = EvaluatePower.CalculatePower(
            collaborationEquipment.Health,
            collaborationEquipment.PhysicalAttack, collaborationEquipment.PhysicalDefense,
            collaborationEquipment.MagicalAttack, collaborationEquipment.MagicalDefense,
            collaborationEquipment.ChemicalAttack, collaborationEquipment.ChemicalDefense,
            collaborationEquipment.AtomicAttack, collaborationEquipment.AtomicDefense,
            collaborationEquipment.MentalAttack, collaborationEquipment.MentalDefense,
            collaborationEquipment.Speed,
            collaborationEquipment.CriticalDamageRate, collaborationEquipment.CriticalRate, collaborationEquipment.CriticalResistanceRate, collaborationEquipment.IgnoreCriticalRate,
            collaborationEquipment.PenetrationRate, collaborationEquipment.PenetrationResistanceRate, collaborationEquipment.EvasionRate,
            collaborationEquipment.DamageAbsorptionRate, collaborationEquipment.IgnoreDamageAbsorptionRate, collaborationEquipment.AbsorbedDamageRate,
            collaborationEquipment.VitalityRegenerationRate, collaborationEquipment.VitalityRegenerationResistanceRate,
            collaborationEquipment.AccuracyRate, collaborationEquipment.LifestealRate,
            collaborationEquipment.ShieldStrength, collaborationEquipment.Tenacity, collaborationEquipment.ResistanceRate,
            collaborationEquipment.ComboRate, collaborationEquipment.IgnoreComboRate, collaborationEquipment.ComboDamageRate, collaborationEquipment.ComboResistanceRate,
            collaborationEquipment.StunRate, collaborationEquipment.IgnoreStunRate,
            collaborationEquipment.ReflectionRate, collaborationEquipment.IgnoreReflectionRate, collaborationEquipment.ReflectionDamageRate, collaborationEquipment.ReflectionResistanceRate,
            collaborationEquipment.Mana, collaborationEquipment.ManaRegenerationRate,
            collaborationEquipment.DamageToDifferentFactionRate, collaborationEquipment.ResistanceToDifferentFactionRate,
            collaborationEquipment.DamageToSameFactionRate, collaborationEquipment.ResistanceToSameFactionRate,
            collaborationEquipment.NormalDamageRate, collaborationEquipment.NormalResistanceRate,
            collaborationEquipment.SkillDamageRate, collaborationEquipment.SkillResistanceRate
        );
        return collaborationEquipment;
    }
    public async Task<CollaborationEquipments> GetNewBreakthroughPowerAsync(CollaborationEquipments c, double coefficient)
    {
        ICollaborationEquipmentsRepository _repository = new CollaborationEquipmentsRepository();
        CollaborationEquipmentsService _service = new CollaborationEquipmentsService(_repository);
        CollaborationEquipments orginCard = await _service.GetCollaborationEquipmentByIdAsync(c.Id);
        CollaborationEquipments collaborationEquipment = new CollaborationEquipments
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
        collaborationEquipment.Power = EvaluatePower.CalculatePower(
            collaborationEquipment.Health,
            collaborationEquipment.PhysicalAttack, collaborationEquipment.PhysicalDefense,
            collaborationEquipment.MagicalAttack, collaborationEquipment.MagicalDefense,
            collaborationEquipment.ChemicalAttack, collaborationEquipment.ChemicalDefense,
            collaborationEquipment.AtomicAttack, collaborationEquipment.AtomicDefense,
            collaborationEquipment.MentalAttack, collaborationEquipment.MentalDefense,
            collaborationEquipment.Speed,
            collaborationEquipment.CriticalDamageRate, collaborationEquipment.CriticalRate, collaborationEquipment.CriticalResistanceRate, collaborationEquipment.IgnoreCriticalRate,
            collaborationEquipment.PenetrationRate, collaborationEquipment.PenetrationResistanceRate, collaborationEquipment.EvasionRate,
            collaborationEquipment.DamageAbsorptionRate, collaborationEquipment.IgnoreDamageAbsorptionRate, collaborationEquipment.AbsorbedDamageRate,
            collaborationEquipment.VitalityRegenerationRate, collaborationEquipment.VitalityRegenerationResistanceRate,
            collaborationEquipment.AccuracyRate, collaborationEquipment.LifestealRate,
            collaborationEquipment.ShieldStrength, collaborationEquipment.Tenacity, collaborationEquipment.ResistanceRate,
            collaborationEquipment.ComboRate, collaborationEquipment.IgnoreComboRate, collaborationEquipment.ComboDamageRate, collaborationEquipment.ComboResistanceRate,
            collaborationEquipment.StunRate, collaborationEquipment.IgnoreStunRate,
            collaborationEquipment.ReflectionRate, collaborationEquipment.IgnoreReflectionRate, collaborationEquipment.ReflectionDamageRate, collaborationEquipment.ReflectionResistanceRate,
            collaborationEquipment.Mana, collaborationEquipment.ManaRegenerationRate,
            collaborationEquipment.DamageToDifferentFactionRate, collaborationEquipment.ResistanceToDifferentFactionRate,
            collaborationEquipment.DamageToSameFactionRate, collaborationEquipment.ResistanceToSameFactionRate,
            collaborationEquipment.NormalDamageRate, collaborationEquipment.NormalResistanceRate,
            collaborationEquipment.SkillDamageRate, collaborationEquipment.SkillResistanceRate
        );
        return collaborationEquipment;
    }

    public async Task<List<CollaborationEquipments>> GetUserCollaborationEquipmentsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> list = await _userCollabEquipmentsRepo.GetUserCollaborationEquipmentsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserCollaborationEquipmentsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userCollabEquipmentsRepo.GetUserCollaborationEquipmentsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserCollaborationEquipmentAsync(CollaborationEquipments collaborationEquipment, string userId)
    {
        return await _userCollabEquipmentsRepo.InsertUserCollaborationEquipmentAsync(collaborationEquipment, userId);
    }

    public async Task<bool> UpdateCollaborationEquipmentLevelAsync(CollaborationEquipments collaborationEquipment, int cardLevel)
    {
        return await _userCollabEquipmentsRepo.UpdateCollaborationEquipmentLevelAsync(collaborationEquipment, cardLevel);
    }

    public async Task<bool> UpdateCollaborationEquipmentBreakthroughAsync(CollaborationEquipments collaborationEquipment, int star, double quantity)
    {
        return await _userCollabEquipmentsRepo.UpdateCollaborationEquipmentBreakthroughAsync(collaborationEquipment, star, quantity);
    }

    public async Task<CollaborationEquipments> GetUserCollaborationEquipmentByIdAsync(string user_id, string Id)
    {
        return await _userCollabEquipmentsRepo.GetUserCollaborationEquipmentByIdAsync(user_id, Id);
    }

    public async Task<CollaborationEquipments> SumPowerUserCollaborationEquipmentsAsync()
    {
        return await _userCollabEquipmentsRepo.SumPowerUserCollaborationEquipmentsAsync();
    }
}

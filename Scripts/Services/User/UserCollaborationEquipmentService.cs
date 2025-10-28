using System.Collections.Generic;

public class UserCollaborationEquipmentService : IUserCollaborationEquipmentService
{
    private readonly IUserCollaborationEquipmentRepository _userCollabEquipmentsRepo;

    public UserCollaborationEquipmentService(IUserCollaborationEquipmentRepository userCollabEquipmentsRepo)
    {
        _userCollabEquipmentsRepo = userCollabEquipmentsRepo;
    }

    public static UserCollaborationEquipmentService Create()
    {
        return new UserCollaborationEquipmentService(new UserCollaborationEquipmentRepository());
    }

    public CollaborationEquipments GetNewLevelPower(CollaborationEquipments c, double coefficient)
    {
        ICollaborationEquipmentRepository _repository = new CollaborationEquipmentRepository();
        CollaborationEquipmentService _service = new CollaborationEquipmentService(_repository);
        CollaborationEquipments orginCard = _service.GetCollaborationEquipmentsById(c.Id);
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
    public CollaborationEquipments GetNewBreakthroughPower(CollaborationEquipments c, double coefficient)
    {
        ICollaborationEquipmentRepository _repository = new CollaborationEquipmentRepository();
        CollaborationEquipmentService _service = new CollaborationEquipmentService(_repository);
        CollaborationEquipments orginCard = _service.GetCollaborationEquipmentsById(c.Id);
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

    public List<CollaborationEquipments> GetUserCollaborationEquipments(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> list = _userCollabEquipmentsRepo.GetUserCollaborationEquipments(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserCollaborationEquipmentCount(string user_id, string type, string rare)
    {
        return _userCollabEquipmentsRepo.GetUserCollaborationEquipmentCount(user_id, type, rare);
    }

    public bool InsertUserCollaborationEquipments(CollaborationEquipments collaborationEquipment)
    {
        return _userCollabEquipmentsRepo.InsertUserCollaborationEquipments(collaborationEquipment);
    }

    public bool UpdateCollaborationEquipmentsLevel(CollaborationEquipments collaborationEquipment, int cardLevel)
    {
        return _userCollabEquipmentsRepo.UpdateCollaborationEquipmentsLevel(collaborationEquipment, cardLevel);
    }

    public bool UpdateCollaborationEquipmentsBreakthrough(CollaborationEquipments collaborationEquipment, int star, double quantity)
    {
        return _userCollabEquipmentsRepo.UpdateCollaborationEquipmentsBreakthrough(collaborationEquipment, star, quantity);
    }

    public CollaborationEquipments GetUserCollaborationEquipmentsById(string user_id, string Id)
    {
        return _userCollabEquipmentsRepo.GetUserCollaborationEquipmentsById(user_id, Id);
    }

    public CollaborationEquipments SumPowerUserCollaborationEquipments()
    {
        return _userCollabEquipmentsRepo.SumPowerUserCollaborationEquipments();
    }
}

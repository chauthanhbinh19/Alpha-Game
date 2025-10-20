using System.Collections.Generic;

public class UserPuppetService : IUserPuppetService
{
    private readonly IUserPuppetRepository _userPuppetRepository;

    public UserPuppetService(IUserPuppetRepository userPuppetRepository)
    {
        _userPuppetRepository = userPuppetRepository;
    }

    public static UserPuppetService Create()
    {
        return new UserPuppetService(new UserPuppetRepository());
    }

    public Puppets GetNewLevelPower(Puppets c, double coefficient)
    {
        IPuppetRepository _repository = new PuppetRepository();
        PuppetService _service = new PuppetService(_repository);
        Puppets orginCard = _service.GetPuppetById(c.Id);
        Puppets Puppet = new Puppets
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
        Puppet.Power = EvaluatePower.CalculatePower(
            Puppet.Health,
            Puppet.PhysicalAttack, Puppet.PhysicalDefense,
            Puppet.MagicalAttack, Puppet.MagicalDefense,
            Puppet.ChemicalAttack, Puppet.ChemicalDefense,
            Puppet.AtomicAttack, Puppet.AtomicDefense,
            Puppet.MentalAttack, Puppet.MentalDefense,
            Puppet.Speed,
            Puppet.CriticalDamageRate, Puppet.CriticalRate, Puppet.CriticalResistanceRate, Puppet.IgnoreCriticalRate,
            Puppet.PenetrationRate, Puppet.PenetrationResistanceRate, Puppet.EvasionRate,
            Puppet.DamageAbsorptionRate, Puppet.IgnoreDamageAbsorptionRate, Puppet.AbsorbedDamageRate,
            Puppet.VitalityRegenerationRate, Puppet.VitalityRegenerationResistanceRate,
            Puppet.AccuracyRate, Puppet.LifestealRate,
            Puppet.ShieldStrength, Puppet.Tenacity, Puppet.ResistanceRate,
            Puppet.ComboRate, Puppet.IgnoreComboRate, Puppet.ComboDamageRate, Puppet.ComboResistanceRate,
            Puppet.StunRate, Puppet.IgnoreStunRate,
            Puppet.ReflectionRate, Puppet.IgnoreReflectionRate, Puppet.ReflectionDamageRate, Puppet.ReflectionResistanceRate,
            Puppet.Mana, Puppet.ManaRegenerationRate,
            Puppet.DamageToDifferentFactionRate, Puppet.ResistanceToDifferentFactionRate,
            Puppet.DamageToSameFactionRate, Puppet.ResistanceToSameFactionRate,
            Puppet.NormalDamageRate, Puppet.NormalResistanceRate,
            Puppet.SkillDamageRate, Puppet.SkillResistanceRate
        );
        return Puppet;
    }
    public Puppets GetNewBreakthroughPower(Puppets c, double coefficient)
    {
        IPuppetRepository _repository = new PuppetRepository();
        PuppetService _service = new PuppetService(_repository);
        Puppets orginCard = _service.GetPuppetById(c.Id);
        Puppets Puppet = new Puppets
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
        Puppet.Power = EvaluatePower.CalculatePower(
            Puppet.Health,
            Puppet.PhysicalAttack, Puppet.PhysicalDefense,
            Puppet.MagicalAttack, Puppet.MagicalDefense,
            Puppet.ChemicalAttack, Puppet.ChemicalDefense,
            Puppet.AtomicAttack, Puppet.AtomicDefense,
            Puppet.MentalAttack, Puppet.MentalDefense,
            Puppet.Speed,
            Puppet.CriticalDamageRate, Puppet.CriticalRate, Puppet.CriticalResistanceRate, Puppet.IgnoreCriticalRate,
            Puppet.PenetrationRate, Puppet.PenetrationResistanceRate, Puppet.EvasionRate,
            Puppet.DamageAbsorptionRate, Puppet.IgnoreDamageAbsorptionRate, Puppet.AbsorbedDamageRate,
            Puppet.VitalityRegenerationRate, Puppet.VitalityRegenerationResistanceRate,
            Puppet.AccuracyRate, Puppet.LifestealRate,
            Puppet.ShieldStrength, Puppet.Tenacity, Puppet.ResistanceRate,
            Puppet.ComboRate, Puppet.IgnoreComboRate, Puppet.ComboDamageRate, Puppet.ComboResistanceRate,
            Puppet.StunRate, Puppet.IgnoreStunRate,
            Puppet.ReflectionRate, Puppet.IgnoreReflectionRate, Puppet.ReflectionDamageRate, Puppet.ReflectionResistanceRate,
            Puppet.Mana, Puppet.ManaRegenerationRate,
            Puppet.DamageToDifferentFactionRate, Puppet.ResistanceToDifferentFactionRate,
            Puppet.DamageToSameFactionRate, Puppet.ResistanceToSameFactionRate,
            Puppet.NormalDamageRate, Puppet.NormalResistanceRate,
            Puppet.SkillDamageRate, Puppet.SkillResistanceRate
        );
        return Puppet;
    }

    public List<Puppets> GetUserPuppet(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = _userPuppetRepository.GetUserPuppet(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserPuppetCount(string user_id, string type, string rare)
    {
        return _userPuppetRepository.GetUserPuppetCount(user_id, type, rare);
    }

    public bool InsertUserPuppet(Puppets puppet)
    {
        return _userPuppetRepository.InsertUserPuppet(puppet);
    }

    public bool UpdatePuppetLevel(Puppets puppet, int cardLevel)
    {
        return _userPuppetRepository.UpdatePuppetLevel(puppet, cardLevel);
    }

    public bool UpdatePuppetBreakthrough(Puppets puppet, int star, int quantity)
    {
        return _userPuppetRepository.UpdatePuppetBreakthrough(puppet, star, quantity);
    }

    public Puppets GetUserPuppetById(string user_id, string Id)
    {
        return _userPuppetRepository.GetUserPuppetById(user_id, Id);
    }

    public Puppets SumPowerUserPuppet()
    {
        return _userPuppetRepository.SumPowerUserPuppet();
    }
}

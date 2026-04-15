using System.Collections.Generic;
using System.Threading.Tasks;

public class UserPuppetsService : IUserPuppetsService
{
     private static UserPuppetsService _instance;
    private readonly IUserPuppetsRepository _userPuppetsRepository;

    public UserPuppetsService(IUserPuppetsRepository userPuppetsRepository)
    {
        _userPuppetsRepository = userPuppetsRepository;
    }

    public static UserPuppetsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserPuppetsService(new UserPuppetsRepository());
        }
        return _instance;
    }

    public async Task<Puppets> GetNewLevelPowerAsync(Puppets c, double coefficient)
    {
        IPuppetsRepository _repository = new PuppetsRepository();
        PuppetsService _service = new PuppetsService(_repository);
        Puppets orginCard = await _service.GetPuppetByIdAsync(c.Id);
        Puppets puppet = new Puppets
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
        puppet.Power = PowerHelper.CalculatePower(
            puppet.Health,
            puppet.PhysicalAttack, puppet.PhysicalDefense,
            puppet.MagicalAttack, puppet.MagicalDefense,
            puppet.ChemicalAttack, puppet.ChemicalDefense,
            puppet.AtomicAttack, puppet.AtomicDefense,
            puppet.MentalAttack, puppet.MentalDefense,
            puppet.Speed,
            puppet.CriticalDamageRate, puppet.CriticalRate, puppet.CriticalResistanceRate, puppet.IgnoreCriticalRate,
            puppet.PenetrationRate, puppet.PenetrationResistanceRate, puppet.EvasionRate,
            puppet.DamageAbsorptionRate, puppet.IgnoreDamageAbsorptionRate, puppet.AbsorbedDamageRate,
            puppet.VitalityRegenerationRate, puppet.VitalityRegenerationResistanceRate,
            puppet.AccuracyRate, puppet.LifestealRate,
            puppet.ShieldStrength, puppet.Tenacity, puppet.ResistanceRate,
            puppet.ComboRate, puppet.IgnoreComboRate, puppet.ComboDamageRate, puppet.ComboResistanceRate,
            puppet.StunRate, puppet.IgnoreStunRate,
            puppet.ReflectionRate, puppet.IgnoreReflectionRate, puppet.ReflectionDamageRate, puppet.ReflectionResistanceRate,
            puppet.Mana, puppet.ManaRegenerationRate,
            puppet.DamageToDifferentFactionRate, puppet.ResistanceToDifferentFactionRate,
            puppet.DamageToSameFactionRate, puppet.ResistanceToSameFactionRate,
            puppet.NormalDamageRate, puppet.NormalResistanceRate,
            puppet.SkillDamageRate, puppet.SkillResistanceRate
        );
        return puppet;
    }
    public async Task<Puppets> GetNewBreakthroughPowerAsync(Puppets c, double coefficient)
    {
        IPuppetsRepository _repository = new PuppetsRepository();
        PuppetsService _service = new PuppetsService(_repository);
        Puppets orginCard = await _service.GetPuppetByIdAsync(c.Id);
        Puppets puppet = new Puppets
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
        puppet.Power = PowerHelper.CalculatePower(
            puppet.Health,
            puppet.PhysicalAttack, puppet.PhysicalDefense,
            puppet.MagicalAttack, puppet.MagicalDefense,
            puppet.ChemicalAttack, puppet.ChemicalDefense,
            puppet.AtomicAttack, puppet.AtomicDefense,
            puppet.MentalAttack, puppet.MentalDefense,
            puppet.Speed,
            puppet.CriticalDamageRate, puppet.CriticalRate, puppet.CriticalResistanceRate, puppet.IgnoreCriticalRate,
            puppet.PenetrationRate, puppet.PenetrationResistanceRate, puppet.EvasionRate,
            puppet.DamageAbsorptionRate, puppet.IgnoreDamageAbsorptionRate, puppet.AbsorbedDamageRate,
            puppet.VitalityRegenerationRate, puppet.VitalityRegenerationResistanceRate,
            puppet.AccuracyRate, puppet.LifestealRate,
            puppet.ShieldStrength, puppet.Tenacity, puppet.ResistanceRate,
            puppet.ComboRate, puppet.IgnoreComboRate, puppet.ComboDamageRate, puppet.ComboResistanceRate,
            puppet.StunRate, puppet.IgnoreStunRate,
            puppet.ReflectionRate, puppet.IgnoreReflectionRate, puppet.ReflectionDamageRate, puppet.ReflectionResistanceRate,
            puppet.Mana, puppet.ManaRegenerationRate,
            puppet.DamageToDifferentFactionRate, puppet.ResistanceToDifferentFactionRate,
            puppet.DamageToSameFactionRate, puppet.ResistanceToSameFactionRate,
            puppet.NormalDamageRate, puppet.NormalResistanceRate,
            puppet.SkillDamageRate, puppet.SkillResistanceRate
        );
        return puppet;
    }

    public async Task<List<Puppets>> GetUserPuppetsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = await _userPuppetsRepository.GetUserPuppetsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserPuppetsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userPuppetsRepository.GetUserPuppetsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserPuppetAsync(Puppets puppet, string userId)
    {
        return await _userPuppetsRepository.InsertUserPuppetAsync(puppet, userId);
    }

    public async Task<bool> UpdatePuppetLevelAsync(Puppets puppet, int cardLevel)
    {
        return await _userPuppetsRepository.UpdatePuppetLevelAsync(puppet, cardLevel);
    }

    public async Task<bool> UpdatePuppetBreakthroughAsync(Puppets puppet, int star, double quantity)
    {
        return await _userPuppetsRepository.UpdatePuppetBreakthroughAsync(puppet, star, quantity);
    }

    public async Task<Puppets> GetUserPuppetByIdAsync(string user_id, string Id)
    {
        return await _userPuppetsRepository.GetUserPuppetByIdAsync(user_id, Id);
    }

    public async Task<Puppets> SumPowerUserPuppetsAsync()
    {
        return await _userPuppetsRepository.SumPowerUserPuppetsAsync();
    }
}

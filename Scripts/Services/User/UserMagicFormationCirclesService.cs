using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMagicFormationCirclesService : IUserMagicFormationCirclesService
{
     private static UserMagicFormationCirclesService _instance;
    private IUserMagicFormationCirclesRepository _userMagicFormationCirclesRepository;

    public UserMagicFormationCirclesService(IUserMagicFormationCirclesRepository userMagicFormationCirclesRepository)
    {
        _userMagicFormationCirclesRepository = userMagicFormationCirclesRepository;
    }

    public static UserMagicFormationCirclesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserMagicFormationCirclesService(new UserMagicFormationCirclesRepository());
        }
        return _instance;
    }

    public async Task<MagicFormationCircles> GetNewLevelPowerAsync(MagicFormationCircles c, double coefficient)
    {
        IMagicFormationCirclesRepository _repository = new MagicFormationCirclesRepository();
        MagicFormationCirclesService _service = new MagicFormationCirclesService(_repository);
        MagicFormationCircles orginCard = await _service.GetMagicFormationCircleByIdAsync(c.Id);
        MagicFormationCircles magicFormationCircle = new MagicFormationCircles
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
        magicFormationCircle.Power = PowerHelper.CalculatePower(
            magicFormationCircle.Health,
            magicFormationCircle.PhysicalAttack, magicFormationCircle.PhysicalDefense,
            magicFormationCircle.MagicalAttack, magicFormationCircle.MagicalDefense,
            magicFormationCircle.ChemicalAttack, magicFormationCircle.ChemicalDefense,
            magicFormationCircle.AtomicAttack, magicFormationCircle.AtomicDefense,
            magicFormationCircle.MentalAttack, magicFormationCircle.MentalDefense,
            magicFormationCircle.Speed,
            magicFormationCircle.CriticalDamageRate, magicFormationCircle.CriticalRate, magicFormationCircle.CriticalResistanceRate, magicFormationCircle.IgnoreCriticalRate,
            magicFormationCircle.PenetrationRate, magicFormationCircle.PenetrationResistanceRate, magicFormationCircle.EvasionRate,
            magicFormationCircle.DamageAbsorptionRate, magicFormationCircle.IgnoreDamageAbsorptionRate, magicFormationCircle.AbsorbedDamageRate,
            magicFormationCircle.VitalityRegenerationRate, magicFormationCircle.VitalityRegenerationResistanceRate,
            magicFormationCircle.AccuracyRate, magicFormationCircle.LifestealRate,
            magicFormationCircle.ShieldStrength, magicFormationCircle.Tenacity, magicFormationCircle.ResistanceRate,
            magicFormationCircle.ComboRate, magicFormationCircle.IgnoreComboRate, magicFormationCircle.ComboDamageRate, magicFormationCircle.ComboResistanceRate,
            magicFormationCircle.StunRate, magicFormationCircle.IgnoreStunRate,
            magicFormationCircle.ReflectionRate, magicFormationCircle.IgnoreReflectionRate, magicFormationCircle.ReflectionDamageRate, magicFormationCircle.ReflectionResistanceRate,
            magicFormationCircle.Mana, magicFormationCircle.ManaRegenerationRate,
            magicFormationCircle.DamageToDifferentFactionRate, magicFormationCircle.ResistanceToDifferentFactionRate,
            magicFormationCircle.DamageToSameFactionRate, magicFormationCircle.ResistanceToSameFactionRate,
            magicFormationCircle.NormalDamageRate, magicFormationCircle.NormalResistanceRate,
            magicFormationCircle.SkillDamageRate, magicFormationCircle.SkillResistanceRate
        );
        return magicFormationCircle;
    }
    public async Task<MagicFormationCircles> GetNewBreakthroughPowerAsync(MagicFormationCircles c, double coefficient)
    {
        IMagicFormationCirclesRepository _repository = new MagicFormationCirclesRepository();
        MagicFormationCirclesService _service = new MagicFormationCirclesService(_repository);
        MagicFormationCircles orginCard = await _service.GetMagicFormationCircleByIdAsync(c.Id);
        MagicFormationCircles magicFormationCircle = new MagicFormationCircles
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
        magicFormationCircle.Power = PowerHelper.CalculatePower(
            magicFormationCircle.Health,
            magicFormationCircle.PhysicalAttack, magicFormationCircle.PhysicalDefense,
            magicFormationCircle.MagicalAttack, magicFormationCircle.MagicalDefense,
            magicFormationCircle.ChemicalAttack, magicFormationCircle.ChemicalDefense,
            magicFormationCircle.AtomicAttack, magicFormationCircle.AtomicDefense,
            magicFormationCircle.MentalAttack, magicFormationCircle.MentalDefense,
            magicFormationCircle.Speed,
            magicFormationCircle.CriticalDamageRate, magicFormationCircle.CriticalRate, magicFormationCircle.CriticalResistanceRate, magicFormationCircle.IgnoreCriticalRate,
            magicFormationCircle.PenetrationRate, magicFormationCircle.PenetrationResistanceRate, magicFormationCircle.EvasionRate,
            magicFormationCircle.DamageAbsorptionRate, magicFormationCircle.IgnoreDamageAbsorptionRate, magicFormationCircle.AbsorbedDamageRate,
            magicFormationCircle.VitalityRegenerationRate, magicFormationCircle.VitalityRegenerationResistanceRate,
            magicFormationCircle.AccuracyRate, magicFormationCircle.LifestealRate,
            magicFormationCircle.ShieldStrength, magicFormationCircle.Tenacity, magicFormationCircle.ResistanceRate,
            magicFormationCircle.ComboRate, magicFormationCircle.IgnoreComboRate, magicFormationCircle.ComboDamageRate, magicFormationCircle.ComboResistanceRate,
            magicFormationCircle.StunRate, magicFormationCircle.IgnoreStunRate,
            magicFormationCircle.ReflectionRate, magicFormationCircle.IgnoreReflectionRate, magicFormationCircle.ReflectionDamageRate, magicFormationCircle.ReflectionResistanceRate,
            magicFormationCircle.Mana, magicFormationCircle.ManaRegenerationRate,
            magicFormationCircle.DamageToDifferentFactionRate, magicFormationCircle.ResistanceToDifferentFactionRate,
            magicFormationCircle.DamageToSameFactionRate, magicFormationCircle.ResistanceToSameFactionRate,
            magicFormationCircle.NormalDamageRate, magicFormationCircle.NormalResistanceRate,
            magicFormationCircle.SkillDamageRate, magicFormationCircle.SkillResistanceRate
        );
        return magicFormationCircle;
    }

    public async Task<List<MagicFormationCircles>> GetUserMagicFormationCirclesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> list = await _userMagicFormationCirclesRepository.GetUserMagicFormationCirclesAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMagicFormationCirclesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userMagicFormationCirclesRepository.GetUserMagicFormationCirclesCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserMagicFormationCircleAsync(MagicFormationCircles magicFormationCircle, string userId)
    {
        return await _userMagicFormationCirclesRepository.InsertUserMagicFormationCircleAsync(magicFormationCircle, userId);
    }

    public async Task<bool> UpdateMagicFormationCircleLevelAsync(MagicFormationCircles magicFormationCircle, int cardLevel)
    {
        return await _userMagicFormationCirclesRepository.UpdateMagicFormationCircleLevelAsync(magicFormationCircle, cardLevel);
    }

    public async Task<bool> UpdateMagicFormationCircleBreakthroughAsync(MagicFormationCircles magicFormationCircle, int star, double quantity)
    {
        return await _userMagicFormationCirclesRepository.UpdateMagicFormationCircleBreakthroughAsync(magicFormationCircle, star, quantity);
    }

    public async Task<MagicFormationCircles> GetUserMagicFormationCircleByIdAsync(string user_id, string Id)
    {
        return await _userMagicFormationCirclesRepository.GetUserMagicFormationCircleByIdAsync(user_id, Id);
    }

    public async Task<MagicFormationCircles> SumPowerUserMagicFormationCirclesAsync()
    {
        return await _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync();
    }
}

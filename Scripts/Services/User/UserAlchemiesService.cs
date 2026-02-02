
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserAlchemiesService : IUserAlchemiesService
{
     private static UserAlchemiesService _instance;
    private IUserAlchemiesRepository _userAlchemiesRepository;

    public UserAlchemiesService(IUserAlchemiesRepository userAlchemiesRepository)
    {
        _userAlchemiesRepository = userAlchemiesRepository;
    }

    public static UserAlchemiesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserAlchemiesService(new UserAlchemiesRepository());
        }
        return _instance;
    }

    public async Task<Alchemies> GetNewLevelPowerAsync(Alchemies c, double coefficient)
    {
        IAlchemiesRepository _repository = new AlchemiesRepository();
        AlchemiesService _service = new AlchemiesService(_repository);
        Alchemies orginCard = await _service.GetAlchemyByIdAsync(c.Id);
        Alchemies Alchemy = new Alchemies
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
        Alchemy.Power = EvaluatePower.CalculatePower(
            Alchemy.Health,
            Alchemy.PhysicalAttack, Alchemy.PhysicalDefense,
            Alchemy.MagicalAttack, Alchemy.MagicalDefense,
            Alchemy.ChemicalAttack, Alchemy.ChemicalDefense,
            Alchemy.AtomicAttack, Alchemy.AtomicDefense,
            Alchemy.MentalAttack, Alchemy.MentalDefense,
            Alchemy.Speed,
            Alchemy.CriticalDamageRate, Alchemy.CriticalRate, Alchemy.CriticalResistanceRate, Alchemy.IgnoreCriticalRate,
            Alchemy.PenetrationRate, Alchemy.PenetrationResistanceRate, Alchemy.EvasionRate,
            Alchemy.DamageAbsorptionRate, Alchemy.IgnoreDamageAbsorptionRate, Alchemy.AbsorbedDamageRate,
            Alchemy.VitalityRegenerationRate, Alchemy.VitalityRegenerationResistanceRate,
            Alchemy.AccuracyRate, Alchemy.LifestealRate,
            Alchemy.ShieldStrength, Alchemy.Tenacity, Alchemy.ResistanceRate,
            Alchemy.ComboRate, Alchemy.IgnoreComboRate, Alchemy.ComboDamageRate, Alchemy.ComboResistanceRate,
            Alchemy.StunRate, Alchemy.IgnoreStunRate,
            Alchemy.ReflectionRate, Alchemy.IgnoreReflectionRate, Alchemy.ReflectionDamageRate, Alchemy.ReflectionResistanceRate,
            Alchemy.Mana, Alchemy.ManaRegenerationRate,
            Alchemy.DamageToDifferentFactionRate, Alchemy.ResistanceToDifferentFactionRate,
            Alchemy.DamageToSameFactionRate, Alchemy.ResistanceToSameFactionRate,
            Alchemy.NormalDamageRate, Alchemy.NormalResistanceRate,
            Alchemy.SkillDamageRate, Alchemy.SkillResistanceRate
        );
        return Alchemy;
    }
    public async Task<Alchemies> GetNewBreakthroughPowerAsync(Alchemies c, double coefficient)
    {
        IAlchemiesRepository _repository = new AlchemiesRepository();
        AlchemiesService _service = new AlchemiesService(_repository);
        Alchemies orginCard = await _service.GetAlchemyByIdAsync(c.Id);
        Alchemies Alchemy = new Alchemies
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
        Alchemy.Power = EvaluatePower.CalculatePower(
            Alchemy.Health,
            Alchemy.PhysicalAttack, Alchemy.PhysicalDefense,
            Alchemy.MagicalAttack, Alchemy.MagicalDefense,
            Alchemy.ChemicalAttack, Alchemy.ChemicalDefense,
            Alchemy.AtomicAttack, Alchemy.AtomicDefense,
            Alchemy.MentalAttack, Alchemy.MentalDefense,
            Alchemy.Speed,
            Alchemy.CriticalDamageRate, Alchemy.CriticalRate, Alchemy.CriticalResistanceRate, Alchemy.IgnoreCriticalRate,
            Alchemy.PenetrationRate, Alchemy.PenetrationResistanceRate, Alchemy.EvasionRate,
            Alchemy.DamageAbsorptionRate, Alchemy.IgnoreDamageAbsorptionRate, Alchemy.AbsorbedDamageRate,
            Alchemy.VitalityRegenerationRate, Alchemy.VitalityRegenerationResistanceRate,
            Alchemy.AccuracyRate, Alchemy.LifestealRate,
            Alchemy.ShieldStrength, Alchemy.Tenacity, Alchemy.ResistanceRate,
            Alchemy.ComboRate, Alchemy.IgnoreComboRate, Alchemy.ComboDamageRate, Alchemy.ComboResistanceRate,
            Alchemy.StunRate, Alchemy.IgnoreStunRate,
            Alchemy.ReflectionRate, Alchemy.IgnoreReflectionRate, Alchemy.ReflectionDamageRate, Alchemy.ReflectionResistanceRate,
            Alchemy.Mana, Alchemy.ManaRegenerationRate,
            Alchemy.DamageToDifferentFactionRate, Alchemy.ResistanceToDifferentFactionRate,
            Alchemy.DamageToSameFactionRate, Alchemy.ResistanceToSameFactionRate,
            Alchemy.NormalDamageRate, Alchemy.NormalResistanceRate,
            Alchemy.SkillDamageRate, Alchemy.SkillResistanceRate
        );
        return Alchemy;
    }

    public async Task<List<Alchemies>> GetUserAlchemiesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Alchemies> list = await _userAlchemiesRepository.GetUserAlchemiesAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserAlchemiesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userAlchemiesRepository.GetUserAlchemiesCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserAlchemyAsync(Alchemies alchemy, string userId)
    {
        return await _userAlchemiesRepository.InsertUserAlchemyAsync(alchemy, userId);
    }

    public async Task<bool> UpdateAlchemyLevelAsync(Alchemies alchemy, int cardLevel)
    {
        return await _userAlchemiesRepository.UpdateAlchemyLevelAsync(alchemy, cardLevel);
    }

    public async Task<bool> UpdateAlchemyBreakthroughAsync(Alchemies alchemy, int star, double quantity)
    {
        return await _userAlchemiesRepository.UpdateAlchemyBreakthroughAsync(alchemy, star, quantity);
    }

    public async Task<Alchemies> GetUserAlchemyByIdAsync(string user_id, string Id)
    {
        return await _userAlchemiesRepository.GetUserAlchemyByIdAsync(user_id, Id);
    }

    public async Task<Alchemies> SumPowerUserAlchemiesAsync()
    {
        return await _userAlchemiesRepository.SumPowerUserAlchemiesAsync();
    }
}

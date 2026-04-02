using System.Collections.Generic;
using System.Threading.Tasks;

public class UserEmojisService : IUserEmojisService
{
     private static UserEmojisService _instance;
    private readonly IUserEmojisRepository _userEmojisRepository;

    public UserEmojisService(IUserEmojisRepository userEmojisRepository)
    {
        _userEmojisRepository = userEmojisRepository;
    }

    public static UserEmojisService Create()
    {
        if (_instance == null)
        {
            _instance = new UserEmojisService(new UserEmojisRepository());
        }
        return _instance;
    }

    public async Task<Emojis> GetNewLevelPowerAsync(Emojis c, double coefficient)
    {
        IEmojisRepository _repository = new EmojisRepository();
        EmojisService _service = new EmojisService(_repository);
        Emojis orginCard = await _service.GetEmojiByIdAsync(c.Id);
        Emojis emoji = new Emojis
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
        emoji.Power = EvaluatePower.CalculatePower(
            emoji.Health,
            emoji.PhysicalAttack, emoji.PhysicalDefense,
            emoji.MagicalAttack, emoji.MagicalDefense,
            emoji.ChemicalAttack, emoji.ChemicalDefense,
            emoji.AtomicAttack, emoji.AtomicDefense,
            emoji.MentalAttack, emoji.MentalDefense,
            emoji.Speed,
            emoji.CriticalDamageRate, emoji.CriticalRate, emoji.CriticalResistanceRate, emoji.IgnoreCriticalRate,
            emoji.PenetrationRate, emoji.PenetrationResistanceRate, emoji.EvasionRate,
            emoji.DamageAbsorptionRate, emoji.IgnoreDamageAbsorptionRate, emoji.AbsorbedDamageRate,
            emoji.VitalityRegenerationRate, emoji.VitalityRegenerationResistanceRate,
            emoji.AccuracyRate, emoji.LifestealRate,
            emoji.ShieldStrength, emoji.Tenacity, emoji.ResistanceRate,
            emoji.ComboRate, emoji.IgnoreComboRate, emoji.ComboDamageRate, emoji.ComboResistanceRate,
            emoji.StunRate, emoji.IgnoreStunRate,
            emoji.ReflectionRate, emoji.IgnoreReflectionRate, emoji.ReflectionDamageRate, emoji.ReflectionResistanceRate,
            emoji.Mana, emoji.ManaRegenerationRate,
            emoji.DamageToDifferentFactionRate, emoji.ResistanceToDifferentFactionRate,
            emoji.DamageToSameFactionRate, emoji.ResistanceToSameFactionRate,
            emoji.NormalDamageRate, emoji.NormalResistanceRate,
            emoji.SkillDamageRate, emoji.SkillResistanceRate
        );
        return emoji;
    }
    public async Task<Emojis> GetNewBreakthroughPowerAsync(Emojis c, double coefficient)
    {
        IEmojisRepository _repository = new EmojisRepository();
        EmojisService _service = new EmojisService(_repository);
        Emojis orginCard = await _service.GetEmojiByIdAsync(c.Id);
        Emojis emoji = new Emojis
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
        emoji.Power = EvaluatePower.CalculatePower(
            emoji.Health,
            emoji.PhysicalAttack, emoji.PhysicalDefense,
            emoji.MagicalAttack, emoji.MagicalDefense,
            emoji.ChemicalAttack, emoji.ChemicalDefense,
            emoji.AtomicAttack, emoji.AtomicDefense,
            emoji.MentalAttack, emoji.MentalDefense,
            emoji.Speed,
            emoji.CriticalDamageRate, emoji.CriticalRate, emoji.CriticalResistanceRate, emoji.IgnoreCriticalRate,
            emoji.PenetrationRate, emoji.PenetrationResistanceRate, emoji.EvasionRate,
            emoji.DamageAbsorptionRate, emoji.IgnoreDamageAbsorptionRate, emoji.AbsorbedDamageRate,
            emoji.VitalityRegenerationRate, emoji.VitalityRegenerationResistanceRate,
            emoji.AccuracyRate, emoji.LifestealRate,
            emoji.ShieldStrength, emoji.Tenacity, emoji.ResistanceRate,
            emoji.ComboRate, emoji.IgnoreComboRate, emoji.ComboDamageRate, emoji.ComboResistanceRate,
            emoji.StunRate, emoji.IgnoreStunRate,
            emoji.ReflectionRate, emoji.IgnoreReflectionRate, emoji.ReflectionDamageRate, emoji.ReflectionResistanceRate,
            emoji.Mana, emoji.ManaRegenerationRate,
            emoji.DamageToDifferentFactionRate, emoji.ResistanceToDifferentFactionRate,
            emoji.DamageToSameFactionRate, emoji.ResistanceToSameFactionRate,
            emoji.NormalDamageRate, emoji.NormalResistanceRate,
            emoji.SkillDamageRate, emoji.SkillResistanceRate
        );
        return emoji;
    }

    public async Task<List<Emojis>> GetUserEmojisAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Emojis> list = await _userEmojisRepository.GetUserEmojisAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserEmojisCountAsync(string user_id, string search, string rare)
    {
        return await _userEmojisRepository.GetUserEmojisCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserEmojiAsync(Emojis Emojis, string userId)
    {
        return await _userEmojisRepository.InsertUserEmojiAsync(Emojis, userId);
    }

    public async Task<bool> UpdateEmojiLevelAsync(Emojis Emojis, int cardLevel)
    {
        return await _userEmojisRepository.UpdateEmojiLevelAsync(Emojis, cardLevel);
    }

    public async Task<bool> UpdateEmojiBreakthroughAsync(Emojis Emojis, int star, double quantity)
    {
        return await _userEmojisRepository.UpdateEmojiBreakthroughAsync(Emojis, star, quantity);
    }

    public async Task<Emojis> GetUserEmojiByIdAsync(string user_id, string Id)
    {
        return await _userEmojisRepository.GetUserEmojiByIdAsync(user_id, Id);
    }

    public async Task<Emojis> SumPowerUserEmojisAsync()
    {
        return await _userEmojisRepository.SumPowerUserEmojisAsync();
    }
}

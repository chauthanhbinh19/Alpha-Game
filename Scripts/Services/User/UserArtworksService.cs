
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArtworksService : IUserArtworksService
{
     private static UserArtworksService _instance;
    private IUserArtworksRepository _userArtworksRepository;

    public UserArtworksService(IUserArtworksRepository userArtworksRepository)
    {
        _userArtworksRepository = userArtworksRepository;
    }

    public static UserArtworksService Create()
    {
        if (_instance == null)
        {
            _instance = new UserArtworksService(new UserArtworksRepository());
        }
        return _instance;
    }

    public async Task<Artworks> GetNewLevelPowerAsync(Artworks c, double coefficient)
    {
        IArtworksRepository _repository = new ArtworksRepository();
        ArtworksService _service = new ArtworksService(_repository);
        Artworks orginCard = await _service.GetArtworkByIdAsync(c.Id);
        Artworks artwork = new Artworks
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
        artwork.Power = PowerHelper.CalculatePower(
            artwork.Health,
            artwork.PhysicalAttack, artwork.PhysicalDefense,
            artwork.MagicalAttack, artwork.MagicalDefense,
            artwork.ChemicalAttack, artwork.ChemicalDefense,
            artwork.AtomicAttack, artwork.AtomicDefense,
            artwork.MentalAttack, artwork.MentalDefense,
            artwork.Speed,
            artwork.CriticalDamageRate, artwork.CriticalRate, artwork.CriticalResistanceRate, artwork.IgnoreCriticalRate,
            artwork.PenetrationRate, artwork.PenetrationResistanceRate, artwork.EvasionRate,
            artwork.DamageAbsorptionRate, artwork.IgnoreDamageAbsorptionRate, artwork.AbsorbedDamageRate,
            artwork.VitalityRegenerationRate, artwork.VitalityRegenerationResistanceRate,
            artwork.AccuracyRate, artwork.LifestealRate,
            artwork.ShieldStrength, artwork.Tenacity, artwork.ResistanceRate,
            artwork.ComboRate, artwork.IgnoreComboRate, artwork.ComboDamageRate, artwork.ComboResistanceRate,
            artwork.StunRate, artwork.IgnoreStunRate,
            artwork.ReflectionRate, artwork.IgnoreReflectionRate, artwork.ReflectionDamageRate, artwork.ReflectionResistanceRate,
            artwork.Mana, artwork.ManaRegenerationRate,
            artwork.DamageToDifferentFactionRate, artwork.ResistanceToDifferentFactionRate,
            artwork.DamageToSameFactionRate, artwork.ResistanceToSameFactionRate,
            artwork.NormalDamageRate, artwork.NormalResistanceRate,
            artwork.SkillDamageRate, artwork.SkillResistanceRate
        );
        return artwork;
    }
    public async Task<Artworks> GetNewBreakthroughPowerAsync(Artworks c, double coefficient)
    {
        IArtworksRepository _repository = new ArtworksRepository();
        ArtworksService _service = new ArtworksService(_repository);
        Artworks orginCard = await _service.GetArtworkByIdAsync(c.Id);
        Artworks artwork = new Artworks
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
        artwork.Power = PowerHelper.CalculatePower(
            artwork.Health,
            artwork.PhysicalAttack, artwork.PhysicalDefense,
            artwork.MagicalAttack, artwork.MagicalDefense,
            artwork.ChemicalAttack, artwork.ChemicalDefense,
            artwork.AtomicAttack, artwork.AtomicDefense,
            artwork.MentalAttack, artwork.MentalDefense,
            artwork.Speed,
            artwork.CriticalDamageRate, artwork.CriticalRate, artwork.CriticalResistanceRate, artwork.IgnoreCriticalRate,
            artwork.PenetrationRate, artwork.PenetrationResistanceRate, artwork.EvasionRate,
            artwork.DamageAbsorptionRate, artwork.IgnoreDamageAbsorptionRate, artwork.AbsorbedDamageRate,
            artwork.VitalityRegenerationRate, artwork.VitalityRegenerationResistanceRate,
            artwork.AccuracyRate, artwork.LifestealRate,
            artwork.ShieldStrength, artwork.Tenacity, artwork.ResistanceRate,
            artwork.ComboRate, artwork.IgnoreComboRate, artwork.ComboDamageRate, artwork.ComboResistanceRate,
            artwork.StunRate, artwork.IgnoreStunRate,
            artwork.ReflectionRate, artwork.IgnoreReflectionRate, artwork.ReflectionDamageRate, artwork.ReflectionResistanceRate,
            artwork.Mana, artwork.ManaRegenerationRate,
            artwork.DamageToDifferentFactionRate, artwork.ResistanceToDifferentFactionRate,
            artwork.DamageToSameFactionRate, artwork.ResistanceToSameFactionRate,
            artwork.NormalDamageRate, artwork.NormalResistanceRate,
            artwork.SkillDamageRate, artwork.SkillResistanceRate
        );
        return artwork;
    }

    public async Task<List<Artworks>> GetUserArtworksAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Artworks> list = await _userArtworksRepository.GetUserArtworksAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserArtworksCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userArtworksRepository.GetUserArtworksCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserArtworkAsync(Artworks Artwork, string userId)
    {
        return await _userArtworksRepository.InsertUserArtworkAsync(Artwork, userId);
    }

    public async Task<bool> UpdateArtworkLevelAsync(Artworks Artwork, int cardLevel)
    {
        return await _userArtworksRepository.UpdateArtworkLevelAsync(Artwork, cardLevel);
    }

    public async Task<bool> UpdateArtworkBreakthroughAsync(Artworks Artwork, int star, double quantity)
    {
        return await _userArtworksRepository.UpdateArtworkBreakthroughAsync(Artwork, star, quantity);
    }

    public async Task<Artworks> GetUserArtworkByIdAsync(string user_id, string Id)
    {
        return await _userArtworksRepository.GetUserArtworkByIdAsync(user_id, Id);
    }

    public async Task<Artworks> SumPowerUserArtworksAsync()
    {
        return await _userArtworksRepository.SumPowerUserArtworksAsync();
    }
}

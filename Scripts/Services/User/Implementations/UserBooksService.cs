using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBooksService : IUserBooksService
{
    private readonly IUserBooksRepository _userBooksRepository;
    private readonly IBooksGalleryService _booksGalleryService;
    private readonly IBooksService _booksService;
    private readonly IPowerManagerService _powerManagerService;

    public UserBooksService(
        IUserBooksRepository userBooksRepository,
        IBooksGalleryService booksGalleryService,
        IBooksService booksService,
        IPowerManagerService powerManagerService)
    {
        _userBooksRepository = userBooksRepository;
        _booksGalleryService = booksGalleryService;
        _booksService = booksService;
        _powerManagerService = powerManagerService;
    }

    public static IUserBooksService Create() => ServiceContainer.GetService<IUserBooksService>();

    public async Task<List<Books>> GetAllEquipmentPowerAsync(string userId, List<Books> BooksList)
    {
        foreach (var c in BooksList)
        {
            Equipments equipments = await UserEquipmentsService.Create().GetAllUserEquipmentsByBookIdAsync(userId, c.Id);
            c.Health = c.Health + equipments.Health + equipments.SpecialHealth;
            c.PhysicalAttack = c.PhysicalAttack + equipments.PhysicalAttack + equipments.SpecialPhysicalAttack;
            c.PhysicalDefense = c.PhysicalDefense + equipments.PhysicalDefense + equipments.SpecialPhysicalDefense;
            c.MagicalAttack = c.MagicalAttack + equipments.MagicalAttack + equipments.SpecialMagicalAttack;
            c.MagicalDefense = c.MagicalDefense + equipments.MagicalDefense + equipments.SpecialMagicalDefense;
            c.ChemicalAttack = c.ChemicalAttack + equipments.ChemicalAttack + equipments.SpecialChemicalAttack;
            c.ChemicalDefense = c.ChemicalDefense + equipments.ChemicalDefense + equipments.SpecialChemicalDefense;
            c.AtomicAttack = c.AtomicAttack + equipments.AtomicAttack + equipments.SpecialAtomicAttack;
            c.AtomicDefense = c.AtomicDefense + equipments.AtomicDefense + equipments.SpecialAtomicDefense;
            c.MentalAttack = c.MentalAttack + equipments.MentalAttack + equipments.SpecialMentalAttack;
            c.MentalDefense = c.MentalDefense + equipments.MentalDefense + equipments.SpecialMentalDefense;
            c.Speed = c.Speed + equipments.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + equipments.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + equipments.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + equipments.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + equipments.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + equipments.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + equipments.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + equipments.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + equipments.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + equipments.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + equipments.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + equipments.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + equipments.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + equipments.AccuracyRate;
            c.LifestealRate = c.LifestealRate + equipments.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + equipments.ShieldStrength;
            c.Tenacity = c.Tenacity + equipments.Tenacity;
            c.ResistanceRate = c.ResistanceRate + equipments.ResistanceRate;
            c.ComboRate = c.ComboRate + equipments.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + equipments.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + equipments.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + equipments.ComboResistanceRate;
            c.StunRate = c.StunRate + equipments.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + equipments.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + equipments.ReflectionRate;
            c.IgnoreReflectionRate = c.IgnoreReflectionRate + equipments.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + equipments.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + equipments.ReflectionResistanceRate;
            c.Mana = c.Mana + equipments.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + equipments.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + equipments.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + equipments.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + equipments.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + equipments.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + equipments.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + equipments.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + equipments.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + equipments.SkillResistanceRate;

            c.Power = PowerHelper.CalculatePower(
            c.Health,
            c.PhysicalAttack, c.PhysicalDefense,
            c.MagicalAttack, c.MagicalDefense,
            c.ChemicalAttack, c.ChemicalDefense,
            c.AtomicAttack, c.AtomicDefense,
            c.MentalAttack, c.MentalDefense,
            c.Speed,
            c.CriticalDamageRate, c.CriticalRate, c.CriticalResistanceRate, c.IgnoreCriticalRate,
            c.PenetrationRate, c.PenetrationResistanceRate, c.EvasionRate,
            c.DamageAbsorptionRate, c.IgnoreDamageAbsorptionRate, c.AbsorbedDamageRate,
            c.VitalityRegenerationRate, c.VitalityRegenerationResistanceRate,
            c.AccuracyRate, c.LifestealRate,
            c.ShieldStrength, c.Tenacity, c.ResistanceRate,
            c.ComboRate, c.IgnoreComboRate, c.ComboDamageRate, c.ComboResistanceRate,
            c.StunRate, c.IgnoreStunRate,
            c.ReflectionRate, c.IgnoreReflectionRate, c.ReflectionDamageRate, c.ReflectionResistanceRate,
            c.Mana, c.ManaRegenerationRate,
            c.DamageToDifferentFactionRate, c.ResistanceToDifferentFactionRate,
            c.DamageToSameFactionRate, c.ResistanceToSameFactionRate,
            c.NormalDamageRate, c.NormalResistanceRate,
            c.SkillDamageRate, c.SkillResistanceRate
        );
        }
        return BooksList;
    }
    public async Task<List<Books>> GetAllRankPowerAsync(string userId, List<Books> BooksList)
    {
        foreach (var c in BooksList)
        {
            UserRanks rank = await UserBooksRankService.Create().GetSumUserBooksRankAsync(userId, c.Id);
            c.Health = c.Health + rank.Health + c.BaseStats.Health * rank.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + rank.PhysicalAttack + c.BaseStats.PhysicalAttack * rank.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + rank.PhysicalDefense + c.BaseStats.PhysicalDefense * rank.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + rank.MagicalAttack + c.BaseStats.MagicalAttack * rank.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + rank.MagicalDefense + c.BaseStats.MagicalDefense * rank.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + rank.ChemicalAttack + c.BaseStats.ChemicalAttack * rank.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + rank.ChemicalDefense + c.BaseStats.ChemicalDefense * rank.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + rank.AtomicAttack + c.BaseStats.AtomicAttack * rank.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + rank.AtomicDefense + c.BaseStats.AtomicDefense * rank.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + rank.MentalAttack + c.BaseStats.MentalAttack * rank.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + rank.MentalDefense + c.BaseStats.MentalDefense * rank.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + rank.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + rank.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + rank.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + rank.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + rank.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + rank.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + rank.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + rank.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + rank.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + rank.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + rank.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + rank.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + rank.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + rank.AccuracyRate;
            c.LifestealRate = c.LifestealRate + rank.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + rank.ShieldStrength;
            c.Tenacity = c.Tenacity + rank.Tenacity;
            c.ResistanceRate = c.ResistanceRate + rank.ResistanceRate;
            c.ComboRate = c.ComboRate + rank.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + rank.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + rank.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + rank.ComboResistanceRate;
            c.StunRate = c.StunRate + rank.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + rank.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + rank.ReflectionRate;
            c.IgnoreReflectionRate = c.IgnoreReflectionRate + rank.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + rank.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + rank.ReflectionResistanceRate;
            c.Mana = c.Mana + rank.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + rank.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + rank.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + rank.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + rank.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + rank.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + rank.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + rank.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + rank.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + rank.SkillResistanceRate;

            c.Power = PowerHelper.CalculatePower(
            c.Health,
            c.PhysicalAttack, c.PhysicalDefense,
            c.MagicalAttack, c.MagicalDefense,
            c.ChemicalAttack, c.ChemicalDefense,
            c.AtomicAttack, c.AtomicDefense,
            c.MentalAttack, c.MentalDefense,
            c.Speed,
            c.CriticalDamageRate, c.CriticalRate, c.CriticalResistanceRate, c.IgnoreCriticalRate,
            c.PenetrationRate, c.PenetrationResistanceRate, c.EvasionRate,
            c.DamageAbsorptionRate, c.IgnoreDamageAbsorptionRate, c.AbsorbedDamageRate,
            c.VitalityRegenerationRate, c.VitalityRegenerationResistanceRate,
            c.AccuracyRate, c.LifestealRate,
            c.ShieldStrength, c.Tenacity, c.ResistanceRate,
            c.ComboRate, c.IgnoreComboRate, c.ComboDamageRate, c.ComboResistanceRate,
            c.StunRate, c.IgnoreStunRate,
            c.ReflectionRate, c.IgnoreReflectionRate, c.ReflectionDamageRate, c.ReflectionResistanceRate,
            c.Mana, c.ManaRegenerationRate,
            c.DamageToDifferentFactionRate, c.ResistanceToDifferentFactionRate,
            c.DamageToSameFactionRate, c.ResistanceToSameFactionRate,
            c.NormalDamageRate, c.NormalResistanceRate,
            c.SkillDamageRate, c.SkillResistanceRate
        );
        }
        return BooksList;
    }
    public async Task<List<Books>> GetAllMasterPowerAsync(string userId, List<Books> BooksList)
    {
        foreach (var c in BooksList)
        {
            UserMasters master = await UserBooksMasterService.Create().GetSumUserBooksMasterAsync(userId, c.Id);
            c.Health = c.Health + master.Health + c.BaseStats.Health * master.PercentAllHealth / 100;
            c.PhysicalAttack = c.PhysicalAttack + master.PhysicalAttack + c.BaseStats.PhysicalAttack * master.PercentAllPhysicalAttack / 100;
            c.PhysicalDefense = c.PhysicalDefense + master.PhysicalDefense + c.BaseStats.PhysicalDefense * master.PercentAllPhysicalDefense / 100;
            c.MagicalAttack = c.MagicalAttack + master.MagicalAttack + c.BaseStats.MagicalAttack * master.PercentAllMagicalAttack / 100;
            c.MagicalDefense = c.MagicalDefense + master.MagicalDefense + c.BaseStats.MagicalDefense * master.PercentAllMagicalDefense / 100;
            c.ChemicalAttack = c.ChemicalAttack + master.ChemicalAttack + c.BaseStats.ChemicalAttack * master.PercentAllChemicalAttack / 100;
            c.ChemicalDefense = c.ChemicalDefense + master.ChemicalDefense + c.BaseStats.ChemicalDefense * master.PercentAllChemicalDefense / 100;
            c.AtomicAttack = c.AtomicAttack + master.AtomicAttack + c.BaseStats.AtomicAttack * master.PercentAllAtomicAttack / 100;
            c.AtomicDefense = c.AtomicDefense + master.AtomicDefense + c.BaseStats.AtomicDefense * master.PercentAllAtomicDefense / 100;
            c.MentalAttack = c.MentalAttack + master.MentalAttack + c.BaseStats.MentalAttack * master.PercentAllMentalAttack / 100;
            c.MentalDefense = c.MentalDefense + master.MentalDefense + c.BaseStats.MentalDefense * master.PercentAllMentalDefense / 100;
            c.Speed = c.Speed + master.Speed;
            c.CriticalDamageRate = c.CriticalDamageRate + master.CriticalDamageRate;
            c.CriticalRate = c.CriticalRate + master.CriticalRate;
            c.CriticalResistanceRate = c.CriticalResistanceRate + master.CriticalResistanceRate;
            c.IgnoreCriticalRate = c.IgnoreCriticalRate + master.IgnoreCriticalRate;
            c.PenetrationRate = c.PenetrationRate + master.PenetrationRate;
            c.PenetrationResistanceRate = c.PenetrationResistanceRate + master.PenetrationResistanceRate;
            c.EvasionRate = c.EvasionRate + master.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + master.DamageAbsorptionRate;
            c.IgnoreDamageAbsorptionRate = c.IgnoreDamageAbsorptionRate + master.IgnoreDamageAbsorptionRate;
            c.AbsorbedDamageRate = c.AbsorbedDamageRate + master.AbsorbedDamageRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + master.VitalityRegenerationRate;
            c.VitalityRegenerationResistanceRate = c.VitalityRegenerationResistanceRate + master.VitalityRegenerationResistanceRate;
            c.AccuracyRate = c.AccuracyRate + master.AccuracyRate;
            c.LifestealRate = c.LifestealRate + master.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + master.ShieldStrength;
            c.Tenacity = c.Tenacity + master.Tenacity;
            c.ResistanceRate = c.ResistanceRate + master.ResistanceRate;
            c.ComboRate = c.ComboRate + master.ComboRate;
            c.IgnoreComboRate = c.IgnoreComboRate + master.IgnoreComboRate;
            c.ComboDamageRate = c.ComboDamageRate + master.ComboDamageRate;
            c.ComboResistanceRate = c.ComboResistanceRate + master.ComboResistanceRate;
            c.StunRate = c.StunRate + master.StunRate;
            c.IgnoreStunRate = c.IgnoreStunRate + master.IgnoreStunRate;
            c.ReflectionRate = c.ReflectionRate + master.ReflectionRate;
            c.IgnoreReflectionRate = c.IgnoreReflectionRate + master.IgnoreReflectionRate;
            c.ReflectionDamageRate = c.ReflectionDamageRate + master.ReflectionDamageRate;
            c.ReflectionResistanceRate = c.ReflectionResistanceRate + master.ReflectionResistanceRate;
            c.Mana = c.Mana + master.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + master.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + master.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + master.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + master.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + master.ResistanceToSameFactionRate;
            c.NormalDamageRate = c.NormalDamageRate + master.NormalDamageRate;
            c.NormalResistanceRate = c.NormalResistanceRate + master.NormalResistanceRate;
            c.SkillDamageRate = c.SkillDamageRate + master.SkillDamageRate;
            c.SkillResistanceRate = c.SkillResistanceRate + master.SkillResistanceRate;

            c.Power = PowerHelper.CalculatePower(
            c.Health,
            c.PhysicalAttack, c.PhysicalDefense,
            c.MagicalAttack, c.MagicalDefense,
            c.ChemicalAttack, c.ChemicalDefense,
            c.AtomicAttack, c.AtomicDefense,
            c.MentalAttack, c.MentalDefense,
            c.Speed,
            c.CriticalDamageRate, c.CriticalRate, c.CriticalResistanceRate, c.IgnoreCriticalRate,
            c.PenetrationRate, c.PenetrationResistanceRate, c.EvasionRate,
            c.DamageAbsorptionRate, c.IgnoreDamageAbsorptionRate, c.AbsorbedDamageRate,
            c.VitalityRegenerationRate, c.VitalityRegenerationResistanceRate,
            c.AccuracyRate, c.LifestealRate,
            c.ShieldStrength, c.Tenacity, c.ResistanceRate,
            c.ComboRate, c.IgnoreComboRate, c.ComboDamageRate, c.ComboResistanceRate,
            c.StunRate, c.IgnoreStunRate,
            c.ReflectionRate, c.IgnoreReflectionRate, c.ReflectionDamageRate, c.ReflectionResistanceRate,
            c.Mana, c.ManaRegenerationRate,
            c.DamageToDifferentFactionRate, c.ResistanceToDifferentFactionRate,
            c.DamageToSameFactionRate, c.ResistanceToSameFactionRate,
            c.NormalDamageRate, c.NormalResistanceRate,
            c.SkillDamageRate, c.SkillResistanceRate
        );
        }
        return BooksList;
    }

    public async Task<List<Books>> GetUserBooksAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Books> list = await _userBooksRepository.GetUserBooksAsync(userId, search, type, pageSize, offset, rare);

        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(userId);
        var scienceFictionTask = UserScienceFictionsService.Create().GetSumUserScienceFictionsAsync(userId);
        var researchTask = UserResearchsService.Create().GetSumUserResearchsAsync(userId);
        var archiveTask = UserArchivesService.Create().GetSumUserArchivesAsync(userId);
        var universeTask = UserUniversesService.Create().GetSumUserUniversesAsync(userId);
        var hiinTask = UserHIINsService.Create().GetSumUserHIINsAsync(userId);
        var sswnTask = UserSSWNsService.Create().GetSumUserSSWNsAsync(userId);
        var hitnTask = UserHITNsService.Create().GetSumUserHITNsAsync(userId);
        var hihnTask = UserHIHNsService.Create().GetSumUserHIHNsAsync(userId);
        var hienTask = UserHIENsService.Create().GetSumUserHIENsAsync(userId);
        var hicaTask = UserHICAsService.Create().GetSumUserHICAsAsync(userId);
        var hirnTask = UserHIRNsService.Create().GetSumUserHIRNsAsync(userId);
        var hidcTask = UserHIDCsService.Create().GetSumUserHIDCsAsync(userId);
        var hicbTask = UserHICBsService.Create().GetSumUserHICBsAsync(userId);
        var hisnTask = UserHISNsService.Create().GetSumUserHISNsAsync(userId);
        var animeStatsTask = UserAnimesService.Create().GetSumUserAnimesAsync(userId);

        await Task.WhenAll(powerManagerTask, scienceFictionTask, researchTask, archiveTask,
        universeTask, hiinTask, sswnTask, hitnTask, hihnTask, hienTask, hicaTask, hirnTask,
        hidcTask, hicbTask, hisnTask, animeStatsTask);

        var powerManagerData = await powerManagerTask;
        var scienceFictionData = await scienceFictionTask;
        var researchData = await researchTask;
        var archiveData = await archiveTask;
        var universeData = await universeTask;
        var hiinData = await hiinTask;
        var sswnData = await sswnTask;
        var hitnData = await hitnTask;
        var hihnData = await hihnTask;
        var hienData = await hienTask;
        var hicaData = await hicaTask;
        var hirnData = await hirnTask;
        var hidcData = await hidcTask;
        var hicbData = await hicbTask;
        var hisnData = await hisnTask;
        var animeStatsData = await animeStatsTask;

        foreach (var item in list)
        {
            item.BaseStats = new BaseStats(item);
        }

        // list = await GetAllSpiritBeastPowerAsync(userId, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        // list = await GetAllEquipmentPowerAsync(userId, list);
        // list = await GetAllRankPowerAsync(userId, list);
        // list = await GetAllMasterPowerAsync(userId, list);
        foreach (var book in list)
        {
            book.ApplyPowerStats(powerManagerData);
            book.ApplyScienceFictionStats(scienceFictionData);
            book.ApplyResearchStats(researchData);
            book.ApplyArchiveStats(archiveData);
            book.ApplyUniverseStats(universeData);
            book.ApplyHIINStats(hiinData);
            book.ApplySSWNStats(sswnData);
            book.ApplyHITNStats(hitnData);
            book.ApplyHIHNStats(hihnData);
            book.ApplyHIENStats(hienData);
            book.ApplyHICAStats(hicaData);
            book.ApplyHIRNStats(hirnData);
            book.ApplyHIDCStats(hidcData);
            book.ApplyHICBStats(hicbData);
            book.ApplyHISNStats(hisnData);
            book.ApplyAllUserAnimes(animeStatsData);
            book.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<Books>> GetUserBooksTeamAsync(string userId, string teamId, string position)
    {
        List<Books> list = await _userBooksRepository.GetUserBooksTeamAsync(userId, teamId);

        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(userId);
        var scienceFictionTask = UserScienceFictionsService.Create().GetSumUserScienceFictionsAsync(userId);
        var researchTask = UserResearchsService.Create().GetSumUserResearchsAsync(userId);
        var archiveTask = UserArchivesService.Create().GetSumUserArchivesAsync(userId);
        var universeTask = UserUniversesService.Create().GetSumUserUniversesAsync(userId);
        var hiinTask = UserHIINsService.Create().GetSumUserHIINsAsync(userId);
        var sswnTask = UserSSWNsService.Create().GetSumUserSSWNsAsync(userId);
        var hitnTask = UserHITNsService.Create().GetSumUserHITNsAsync(userId);
        var hihnTask = UserHIHNsService.Create().GetSumUserHIHNsAsync(userId);
        var hienTask = UserHIENsService.Create().GetSumUserHIENsAsync(userId);
        var hicaTask = UserHICAsService.Create().GetSumUserHICAsAsync(userId);
        var hirnTask = UserHIRNsService.Create().GetSumUserHIRNsAsync(userId);
        var hidcTask = UserHIDCsService.Create().GetSumUserHIDCsAsync(userId);
        var hicbTask = UserHICBsService.Create().GetSumUserHICBsAsync(userId);
        var hisnTask = UserHISNsService.Create().GetSumUserHISNsAsync(userId);
        var animeStatsTask = UserAnimesService.Create().GetSumUserAnimesAsync(userId);

        await Task.WhenAll(powerManagerTask, scienceFictionTask, researchTask, archiveTask,
        universeTask, hiinTask, sswnTask, hitnTask, hihnTask, hienTask, hicaTask, hirnTask,
        hidcTask, hicbTask, hisnTask, animeStatsTask);

        var powerManagerData = await powerManagerTask;
        var scienceFictionData = await scienceFictionTask;
        var researchData = await researchTask;
        var archiveData = await archiveTask;
        var universeData = await universeTask;
        var hiinData = await hiinTask;
        var sswnData = await sswnTask;
        var hitnData = await hitnTask;
        var hihnData = await hihnTask;
        var hienData = await hienTask;
        var hicaData = await hicaTask;
        var hirnData = await hirnTask;
        var hidcData = await hidcTask;
        var hicbData = await hicbTask;
        var hisnData = await hisnTask;
        var animeStatsData = await animeStatsTask;

        // list = await GetAllSpiritBeastPowerAsync(userId, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        // list = await GetAllEquipmentPowerAsync(userId, list);
        // list = await GetAllRankPowerAsync(userId, list);
        // list = await GetAllMasterPowerAsync(userId, list);
        foreach (var book in list)
        {
            book.ApplyPowerStats(powerManagerData);
            book.ApplyScienceFictionStats(scienceFictionData);
            book.ApplyResearchStats(researchData);
            book.ApplyArchiveStats(archiveData);
            book.ApplyUniverseStats(universeData);
            book.ApplyHIINStats(hiinData);
            book.ApplySSWNStats(sswnData);
            book.ApplyHITNStats(hitnData);
            book.ApplyHIHNStats(hihnData);
            book.ApplyHIENStats(hienData);
            book.ApplyHICAStats(hicaData);
            book.ApplyHIRNStats(hirnData);
            book.ApplyHIDCStats(hidcData);
            book.ApplyHICBStats(hicbData);
            book.ApplyHISNStats(hisnData);
            book.ApplyAllUserAnimes(animeStatsData);
            book.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<Dictionary<string, int>> GetUniqueUserBooksTypesTeamAsync(string userId, string teamId)
    {
        return await _userBooksRepository.GetUniqueUserBooksTypesTeamAsync(userId, teamId);
    }

    public async Task<int> GetUserBooksCountAsync(string userId, string search, string type, string rare)
    {
        return await _userBooksRepository.GetUserBooksCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBookAsync(string userId, Books book)
    {
        var insertOrUpdateResult = await _userBooksRepository.InsertOrUpdateUserBookAsync(userId, book);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _booksGalleryService.InsertBookGalleryAsync(userId, book.Id);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBooksBatchAsync(string userId, List<Books> bookes)
    {
        var repositoryResult = await _userBooksRepository.InsertOrUpdateUserBooksBatchAsync(userId, bookes);

        // 1. Kiểm tra Null hoặc nếu Repository trả về không thành công
        if (repositoryResult?.Data == null || !repositoryResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        // 2. Gộp logic xử lý Gallery nếu có thẻ mới được Insert (dùng cho cả Inserted và Mixed)
        var newlyInsertedCards = repositoryResult.Data.InsertedItems;
        if (newlyInsertedCards != null && newlyInsertedCards.Count > 0)
        {
            await _booksGalleryService.InsertBatchBooksGalleryAsync(userId, newlyInsertedCards);
        }

        // 3. Mapping kết quả OperationType trả về gọn gàng
        return repositoryResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserBookLevelAsync(string userId, Books book)
    {
        var updateResult = await _userBooksRepository.UpdateUserBookLevelAsync(userId, book);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserBookStarAsync(string userId, Books book)
    {
        var updateResult = await _userBooksRepository.UpdateUserBookStarAsync(userId, book);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _booksGalleryService.UpdateTempStarBookGalleryAsync(userId, book.Id, book.Star);

        return true;
    }

    public async Task<bool> UpdateTeamUserBookAsync(string userId, string teamId, string position, string book_id)
    {
        return await _userBooksRepository.UpdateTeamUserBookAsync(userId, teamId, position, book_id);
    }

    public async Task<Books> GetUserBookByIdAsync(string userId, string Id)
    {
        var result =  await _userBooksRepository.GetUserBookByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<BaseStats> GetTeamTotalStatsAsync(string userId, UserStatsContextDTO sharedContext = null)
    {
        var totalStats = await _userBooksRepository.GetTeamTotalStatsAsync(userId);
        var baseStats = await _userBooksRepository.GetTeamTotalStatsWithoutQualityAsync(userId);

        UserStatsContextDTO context = sharedContext;
        if (context == null)
        {
            context = await UserStatsService.Create().GetUserStatsContextAsync(userId);
        }

        TotalBuffs totalBuffs = new TotalBuffs();
        totalBuffs.AddBuff(context.PowerManagerData);
        totalBuffs.AddBuff(context.ScienceFictionData);
        totalBuffs.AddBuff(context.ResearchData);
        totalBuffs.AddBuff(context.ArchiveData);
        totalBuffs.AddBuff(context.UniverseData);
        totalBuffs.AddBuff(context.HiinData);
        totalBuffs.AddBuff(context.SswnData);
        totalBuffs.AddBuff(context.HitnData);
        totalBuffs.AddBuff(context.HihnData);
        totalBuffs.AddBuff(context.HienData);
        totalBuffs.AddBuff(context.HicaData);
        totalBuffs.AddBuff(context.HirnData);
        totalBuffs.AddBuff(context.HidcData);
        totalBuffs.AddBuff(context.HicbData);
        totalBuffs.AddBuff(context.HisnData);
        totalBuffs.AddBuff(context.AnimeStatsData);

        totalStats.ApplyTotalBuffs(baseStats, totalBuffs);
        totalStats.RecalculatePower();

        return totalStats;
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserCardSoldiersService : IUserCardSoldiersService
{
    private readonly IUserCardSoldiersRepository _userCardSoldiersRepository;
    private readonly ICardSoldiersGalleryService _cardSoldiersGalleryService;
    private readonly IUserSkillsRepository _userSkillsRepository;
    private readonly IPatternsService _patternsService;
    private readonly IUserStatsService _userStatsService;

    public UserCardSoldiersService(
        IUserCardSoldiersRepository userCardSoldiersRepository,
        ICardSoldiersGalleryService cardSoldiersGalleryService,
        IUserSkillsRepository userSkillsRepository,
        IPatternsService patternsService,
        IUserStatsService userStatsService)
    {
        _userCardSoldiersRepository = userCardSoldiersRepository;
        _cardSoldiersGalleryService = cardSoldiersGalleryService;
        _userSkillsRepository = userSkillsRepository;
        _patternsService = patternsService;
        _userStatsService = userStatsService;
    }

    public static IUserCardSoldiersService Create() => ServiceContainer.GetService<IUserCardSoldiersService>();

    public async Task<List<CardSoldiers>> GetAllEquipmentPowerAsync(string userId, List<CardSoldiers> CardSoldiersList)
    {
        foreach (var c in CardSoldiersList)
        {
            Equipments equipments = await UserEquipmentsService.Create().GetAllUserEquipmentsByCardSoldierIdAsync(userId, c.Id);
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
        return CardSoldiersList;
    }
    public async Task<List<CardSoldiers>> GetAllRankPowerAsync(string userId, List<CardSoldiers> CardSoldiersList)
    {
        foreach (var c in CardSoldiersList)
        {
            Rank rank = await UserCardSoldiersRankService.Create().GetSumUserCardSoldiersRankAsync(userId, c.Id);
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
        return CardSoldiersList;
    }
    public async Task<List<CardSoldiers>> GetAllMasterPowerAsync(string userId, List<CardSoldiers> CardSoldiersList)
    {
        foreach (var c in CardSoldiersList)
        {
            Master master = await UserCardSoldiersMasterService.Create().GetSumUserCardSoldiersMasterAsync(userId, c.Id);
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
        return CardSoldiersList;
    }
    // public List<CardSoldiers> GetMasterBoardPower(string userId, List<CardSoldiers> CardSoldiersList)
    // {
    //     IUserMasterBoardRepository userMasterBoardRepository = new UserMasterBoardRepository();
    //     UserMasterBoardService userMasterBoardService = new UserMasterBoardService(userMasterBoardRepository);
    //     MasterBoard masterBoard = userMasterBoardService.GetUserMasterBoard(userId);
    //     foreach (var c in CardSoldiersList)
    //     {
    //         CardSoldiers card = _userCardSoldiersRepository.GetUserCardSoldiersById(userId, c.id);
    //         c.health = c.health + masterBoard.health + card.health * masterBoard.percent_all_health / 100;
    //         c.physical_attack = c.physical_attack + masterBoard.physical_attack + card.physical_attack * masterBoard.percent_all_physical_attack / 100;
    //         c.physical_defense = c.physical_defense + masterBoard.physical_defense + card.physical_defense * masterBoard.percent_all_physical_defense / 100;
    //         c.magical_attack = c.magical_attack + masterBoard.magical_attack + card.magical_attack * masterBoard.percent_all_magical_attack / 100;
    //         c.magical_defense = c.magical_defense + masterBoard.magical_defense + card.magical_defense * masterBoard.percent_all_magical_defense / 100;
    //         c.chemical_attack = c.chemical_attack + masterBoard.chemical_attack + card.chemical_attack * masterBoard.percent_all_chemical_attack / 100;
    //         c.chemical_defense = c.chemical_defense + masterBoard.chemical_defense + card.chemical_defense * masterBoard.percent_all_chemical_defense / 100;
    //         c.atomic_attack = c.atomic_attack + masterBoard.atomic_attack + card.atomic_attack * masterBoard.percent_all_atomic_attack / 100;
    //         c.atomic_defense = c.atomic_defense + masterBoard.atomic_defense + card.atomic_defense * masterBoard.percent_all_atomic_defense / 100;
    //         c.mental_attack = c.mental_attack + masterBoard.mental_attack + card.mental_attack * masterBoard.percent_all_mental_attack / 100;
    //         c.mental_defense = c.mental_defense + masterBoard.mental_defense + card.mental_defense * masterBoard.percent_all_mental_defense / 100;
    //         c.speed = c.speed + masterBoard.speed;
    //         c.critical_damage_rate = c.critical_damage_rate + masterBoard.critical_damage_rate;
    //         c.critical_rate = c.critical_rate + masterBoard.critical_rate;
    //         c.critical_resistance_rate = c.critical_resistance_rate + masterBoard.critical_resistance_rate;
    //         c.ignore_critical_rate = c.ignore_critical_rate + masterBoard.ignore_critical_rate;
    //         c.penetration_rate = c.penetration_rate + masterBoard.penetration_rate;
    //         c.penetration_resistance_rate = c.penetration_resistance_rate + masterBoard.penetration_resistance_rate;
    //         c.evasion_rate = c.evasion_rate + masterBoard.evasion_rate;
    //         c.damage_absorption_rate = c.damage_absorption_rate + masterBoard.damage_absorption_rate;
    //         c.ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + masterBoard.ignore_damage_absorption_rate;
    //         c.absorbed_damage_rate = c.absorbed_damage_rate + masterBoard.absorbed_damage_rate;
    //         c.vitality_regeneration_rate = c.vitality_regeneration_rate + masterBoard.vitality_regeneration_rate;
    //         c.vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + masterBoard.vitality_regeneration_resistance_rate;
    //         c.accuracy_rate = c.accuracy_rate + masterBoard.accuracy_rate;
    //         c.lifesteal_rate = c.lifesteal_rate + masterBoard.lifesteal_rate;
    //         c.shield_strength = c.shield_strength + masterBoard.shield_strength;
    //         c.tenacity = c.tenacity + masterBoard.tenacity;
    //         c.resistance_rate = c.resistance_rate + masterBoard.resistance_rate;
    //         c.combo_rate = c.combo_rate + masterBoard.combo_rate;
    //         c.ignore_combo_rate = c.ignore_combo_rate + masterBoard.ignore_combo_rate;
    //         c.combo_damage_rate = c.combo_damage_rate + masterBoard.combo_damage_rate;
    //         c.combo_resistance_rate = c.combo_resistance_rate + masterBoard.combo_resistance_rate;
    //         c.stun_rate = c.stun_rate + masterBoard.stun_rate;
    //         c.ignore_stun_rate = c.ignore_stun_rate + masterBoard.ignore_stun_rate;
    //         c.reflection_rate = c.reflection_rate + masterBoard.reflection_rate;
    //         c.ignore_reflection_rate = c.ignore_reflection_rate + masterBoard.ignore_reflection_rate;
    //         c.reflection_damage_rate = c.reflection_damage_rate + masterBoard.reflection_damage_rate;
    //         c.reflection_resistance_rate = c.reflection_resistance_rate + masterBoard.reflection_resistance_rate;
    //         c.mana = c.mana + masterBoard.mana;
    //         c.mana_regeneration_rate = c.mana_regeneration_rate + masterBoard.mana_regeneration_rate;
    //         c.damage_to_different_faction_rate = c.damage_to_different_faction_rate + masterBoard.damage_to_different_faction_rate;
    //         c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + masterBoard.resistance_to_different_faction_rate;
    //         c.damage_to_same_faction_rate = c.damage_to_same_faction_rate + masterBoard.damage_to_same_faction_rate;
    //         c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + masterBoard.resistance_to_same_faction_rate;
    //         c.normal_damage_rate = c.normal_damage_rate + masterBoard.normal_damage_rate;
    //         c.normal_resistance_rate = c.normal_resistance_rate + masterBoard.normal_resistance_rate;
    //         c.skill_damage_rate = c.skill_damage_rate + masterBoard.skill_damage_rate;
    //         c.skill_resistance_rate = c.skill_resistance_rate + masterBoard.skill_resistance_rate;

    //         c.power = EvaluatePower.CalculatePower(
    //         c.health,
    //         c.physical_attack, c.physical_defense,
    //         c.magical_attack, c.magical_defense,
    //         c.chemical_attack, c.chemical_defense,
    //         c.atomic_attack, c.atomic_defense,
    //         c.mental_attack, c.mental_defense,
    //         c.speed,
    //         c.critical_damage_rate, c.critical_rate, c.critical_resistance_rate, c.ignore_critical_rate,
    //         c.penetration_rate, c.penetration_resistance_rate, c.evasion_rate,
    //         c.damage_absorption_rate, c.ignore_damage_absorption_rate, c.absorbed_damage_rate,
    //         c.vitality_regeneration_rate, c.vitality_regeneration_resistance_rate,
    //         c.accuracy_rate, c.lifesteal_rate,
    //         c.shield_strength, c.tenacity, c.resistance_rate,
    //         c.combo_rate, c.ignore_combo_rate, c.combo_damage_rate, c.combo_resistance_rate,
    //         c.stun_rate, c.ignore_stun_rate,
    //         c.reflection_rate, c.ignore_reflection_rate, c.reflection_damage_rate, c.reflection_resistance_rate,
    //         c.mana, c.mana_regeneration_rate,
    //         c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
    //         c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate,
    //         c.normal_damage_rate, c.normal_resistance_rate,
    //         c.skill_damage_rate, c.skill_resistance_rate
    //     );
    //     }
    //     return CardSoldiersList;
    // }

    public async Task<List<CardSoldiers>> GetUserCardSoldiersAsync(string userId, string search, string type, int pageSize, int offset, string rare, UserStatsContextDTO sharedContext = null)
    {
        List<CardSoldiers> list = await _userCardSoldiersRepository.GetUserCardSoldiersAsync(userId, search, type, pageSize, offset, rare);

        List<string> cardSoldierIds = list.Select(hero => hero.Id).ToList();

        // var skillsTask = _userSkillsRepository.GetUserCardSoldiersSkillsAsync(userId, cardSoldierIds);

        // var skillData = await skillsTask;
        // foreach (var skill in skillData)
        // {
        //     if (skill.Pattern != null && !string.IsNullOrEmpty(skill.Pattern.Id))
        //     {
        //         skill.Pattern = _patternsService.GetPatternFromCache(skill.Pattern.Id);
        //     }
        // }

        UserStatsContextDTO context = sharedContext;
        if (context == null)
        {
            context = await _userStatsService.GetUserStatsContextAsync(userId);
        }

        // var skillsLookup = skillData.ToLookup(s => s.CardId);

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

        // list = await GetAllSpiritBeastPowerAsync(userId, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(userId, list);
        // list = await GetAllRankPowerAsync(userId, list);
        // list = await GetAllMasterPowerAsync(userId, list);
        // list = await GetSkillsAsync(userId, list);
        foreach (var card in list)
        {
            if (card == null) continue; // Phòng hờ phần tử trong list bị null

            // Áp dụng tổng buff (Flat + % Base stats)
            card.ApplyTotalBuffs(totalBuffs);

            // Gán Skills an toàn, tránh tạo List thừa
            // card.Skills = skillsLookup.Contains(card.Id)
            //     ? skillsLookup[card.Id].ToList()
            //     : new List<Skills>();

            // Tính toán lại tổng lực chiến (Sau khi đã có đầy đủ chỉ số và Skills)
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<CardSoldiers>> GetUserCardSoldiersTeamAsync(string userId, string teamId, string position, UserStatsContextDTO sharedContext = null)
    {
        List<CardSoldiers> list = await _userCardSoldiersRepository.GetUserCardSoldiersTeamAsync(userId, teamId, position);

        List<string> cardSoldierIds = list.Select(hero => hero.Id).ToList();

        // var skillsTask = _userSkillsRepository.GetUserCardSoldiersSkillsAsync(userId, cardSoldierIds);

        // var skillData = await skillsTask;
        // foreach (var skill in skillData)
        // {
        //     if (skill.Pattern != null && !string.IsNullOrEmpty(skill.Pattern.Id))
        //     {
        //         skill.Pattern = _patternsService.GetPatternFromCache(skill.Pattern.Id);
        //     }
        // }

        UserStatsContextDTO context = sharedContext;
        if (context == null)
        {
            context = await _userStatsService.GetUserStatsContextAsync(userId);
        }

        // var skillsLookup = skillData.ToLookup(s => s.CardId);

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

        // list = await GetAllSpiritBeastPowerAsync(userId, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(userId, list);
        // list = await GetAllRankPowerAsync(userId, list);
        // list = await GetAllMasterPowerAsync(userId, list);
        // list = await GetSkillsAsync(userId, list);
        foreach (var card in list)
        {
            if (card == null) continue; // Phòng hờ phần tử trong list bị null

            // Áp dụng tổng buff (Flat + % Base stats)
            card.ApplyTotalBuffs(totalBuffs);

            // Gán Skills an toàn, tránh tạo List thừa
            // card.Skills = skillsLookup.Contains(card.Id)
            //     ? skillsLookup[card.Id].ToList()
            //     : new List<Skills>();

            // Tính toán lại tổng lực chiến (Sau khi đã có đầy đủ chỉ số và Skills)
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<CardSoldiers>> GetUserCardSoldiersTeamWithoutPositionAsync(string userId, string teamId, UserStatsContextDTO sharedContext = null)
    {
        List<CardSoldiers> list = await _userCardSoldiersRepository.GetUserCardSoldiersTeamWithoutPositionAsync(userId, teamId);

        List<string> cardSoldierIds = list.Select(hero => hero.Id).ToList();

        // var skillsTask = _userSkillsRepository.GetUserCardSoldiersSkillsAsync(userId, cardSoldierIds);

        // var skillData = await skillsTask;
        // foreach (var skill in skillData)
        // {
        //     if (skill.Pattern != null && !string.IsNullOrEmpty(skill.Pattern.Id))
        //     {
        //         skill.Pattern = _patternsService.GetPatternFromCache(skill.Pattern.Id);
        //     }
        // }

        UserStatsContextDTO context = sharedContext;
        if (context == null)
        {
            context = await _userStatsService.GetUserStatsContextAsync(userId);
        }

        // var skillsLookup = skillData.ToLookup(s => s.CardId);

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

        // list = await GetAllSpiritBeastPowerAsync(userId, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(userId, list);
        // list = await GetAllRankPowerAsync(userId, list);
        // list = await GetAllMasterPowerAsync(userId, list);
        // list = await GetSkillsAsync(userId, list);
        foreach (var card in list)
        {
            if (card == null) continue; // Phòng hờ phần tử trong list bị null

            // Áp dụng tổng buff (Flat + % Base stats)
            card.ApplyTotalBuffs(totalBuffs);

            // Gán Skills an toàn, tránh tạo List thừa
            // card.Skills = skillsLookup.Contains(card.Id)
            //     ? skillsLookup[card.Id].ToList()
            //     : new List<Skills>();

            // Tính toán lại tổng lực chiến (Sau khi đã có đầy đủ chỉ số và Skills)
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<Dictionary<string, int>> GetUniqueUserCardSoldiersTypesTeamAsync(string userId, string teamId)
    {
        return await _userCardSoldiersRepository.GetUniqueCardSoldiersTypesTeamAsync(userId, teamId);
    }

    public async Task<bool> UpdateTeamUserCardSoldierAsync(string userId, string teamId, string position, string cardId)
    {
        return await _userCardSoldiersRepository.UpdateTeamCardSoldierAsync(userId, teamId, position, cardId);
    }

    public async Task<int> GetUserCardSoldiersCountAsync(string userId, string search, string type, string rare)
    {
        return await _userCardSoldiersRepository.GetUserCardSoldiersCountAsync(userId, search, type, rare);
    }

    public async Task<int> GetUserCardSoldiersTeamsPositionCountAsync(string userId, string teamId, string position)
    {
        return await _userCardSoldiersRepository.GetUserCardSoldiersTeamsPositionCountAsync(userId, teamId, position);
    }

    public async Task<int> GetUserCardSoldiersTeamsCountAsync(string userId, string teamId)
    {
        return await _userCardSoldiersRepository.GetUserCardSoldiersTeamsCountAsync(userId, teamId);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardSoldierAsync(string userId, CardSoldiers cardSoldier)
    {
        var insertOrUpdateResult = await _userCardSoldiersRepository.InsertOrUpdateUserCardSoldierAsync(userId, cardSoldier);

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

        await _cardSoldiersGalleryService.InsertCardSoldierGalleryAsync(userId, cardSoldier.Id);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardSoldiersBatchAsync(string userId, List<CardSoldiers> cardSoldieres)
    {
        var repositoryResult = await _userCardSoldiersRepository.InsertOrUpdateUserCardSoldiersBatchAsync(userId, cardSoldieres);

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
            await _cardSoldiersGalleryService.InsertBatchCardSoldiersGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<bool> UpdateUserCardSoldierLevelAsync(string userId, CardSoldiers cardSoldier)
    {
        var updateResult = await _userCardSoldiersRepository.UpdateUserCardSoldierLevelAsync(userId, cardSoldier);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserCardSoldierStarAsync(string userId, CardSoldiers cardSoldier)
    {
        var updateResult = await _userCardSoldiersRepository.UpdateUserCardSoldierStarAsync(userId, cardSoldier);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _cardSoldiersGalleryService.UpdateTempStarCardSoldierGalleryAsync(userId, cardSoldier.Id, cardSoldier.Star);

        return true;
    }

    public async Task<CardSoldiers> GetUserCardSoldierByIdAsync(string userId, string Id, UserStatsContextDTO sharedContext = null)
    {
        CardSoldiers cardAdmiral = await _userCardSoldiersRepository.GetUserCardSoldierByIdAsync(userId, Id);
        if (cardAdmiral == null) return null;

        // Bọc vào list để tái sử dụng logic
        List<CardSoldiers> list = new List<CardSoldiers> { cardAdmiral };

        List<string> cardSoldierIds = list.Select(hero => hero.Id).ToList();

        var skillsTask = _userSkillsRepository.GetUserCardSoldiersSkillsAsync(userId, cardSoldierIds);

        var skillData = await skillsTask;
        foreach (var skill in skillData)
        {
            if (skill.Pattern != null && !string.IsNullOrEmpty(skill.Pattern.Id))
            {
                skill.Pattern = _patternsService.GetPatternFromCache(skill.Pattern.Id);
            }
        }

        UserStatsContextDTO context = sharedContext;
        if (context == null)
        {
            context = await _userStatsService.GetUserStatsContextAsync(userId);
        }

        var skillsLookup = skillData.ToLookup(s => s.CardId);

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

        // list = await GetAllSpiritBeastPowerAsync(userId, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(userId, list);
        // list = await GetAllRankPowerAsync(userId, list);
        // list = await GetAllMasterPowerAsync(userId, list);
        // list = await GetSkillsAsync(userId, list);
        foreach (var card in list)
        {
            if (card == null) continue; // Phòng hờ phần tử trong list bị null

            // Áp dụng tổng buff (Flat + % Base stats)
            card.ApplyTotalBuffs(totalBuffs);

            // Gán Skills an toàn, tránh tạo List thừa
            card.Skills = skillsLookup.Contains(card.Id)
                ? skillsLookup[card.Id].ToList()
                : new List<Skills>();

            // Tính toán lại tổng lực chiến (Sau khi đã có đầy đủ chỉ số và Skills)
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list.FirstOrDefault();
    }

    public async Task<BaseStats> GetTeamTotalStatsAsync(string userId, UserStatsContextDTO sharedContext = null)
    {
        var totalStats = await _userCardSoldiersRepository.GetTeamTotalStatsAsync(userId);
        var baseStats = await _userCardSoldiersRepository.GetTeamTotalStatsWithoutQualityAsync(userId);

        UserStatsContextDTO context = sharedContext;
        if (context == null)
        {
            context = await _userStatsService.GetUserStatsContextAsync(userId);
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

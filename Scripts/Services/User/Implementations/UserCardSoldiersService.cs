using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserCardSoldiersService : IUserCardSoldiersService
{
     private static UserCardSoldiersService _instance;
    private IUserCardSoldiersRepository _userCardSoldiersRepository;

    public UserCardSoldiersService(IUserCardSoldiersRepository userCardSoldiersRepository)
    {
        _userCardSoldiersRepository = userCardSoldiersRepository;
    }

    public static UserCardSoldiersService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardSoldiersService(new UserCardSoldiersRepository());
        }
        return _instance;
    }

    public async Task<List<CardSoldiers>> GetAllEquipmentPowerAsync(string user_id, List<CardSoldiers> CardSoldiersList)
    {
        foreach (var c in CardSoldiersList)
        {
            Equipments equipments = await UserEquipmentsService.Create().GetAllEquipmentsByCardSoldierIdAsync(user_id, c.Id);
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
    public async Task<List<CardSoldiers>> GetAllRankPowerAsync(string user_id, List<CardSoldiers> CardSoldiersList)
    {
        foreach (var c in CardSoldiersList)
        {
            Rank rank = await UserCardSoldiersRankService.Create().GetSumCardSoldiersRankAsync(user_id, c.Id);
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
    public async Task<List<CardSoldiers>> GetAllMasterPowerAsync(string user_id, List<CardSoldiers> CardSoldiersList)
    {
        foreach (var c in CardSoldiersList)
        {
            Master master = await UserCardSoldiersMasterService.Create().GetSumCardSoldiersMasterAsync(user_id, c.Id);
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
    // public List<CardSoldiers> GetMasterBoardPower(string user_id, List<CardSoldiers> CardSoldiersList)
    // {
    //     IUserMasterBoardRepository userMasterBoardRepository = new UserMasterBoardRepository();
    //     UserMasterBoardService userMasterBoardService = new UserMasterBoardService(userMasterBoardRepository);
    //     MasterBoard masterBoard = userMasterBoardService.GetUserMasterBoard(user_id);
    //     foreach (var c in CardSoldiersList)
    //     {
    //         CardSoldiers card = _userCardSoldiersRepository.GetUserCardSoldiersById(user_id, c.id);
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
    
    
    public async Task<List<CardSoldiers>> GetSkillsAsync(string user_id, List<CardSoldiers> CardSoldiersList)
    {
        foreach(CardSoldiers cardAdmiral in CardSoldiersList)
        {
            var skills = await UserSkillsService.Create().GetUserCardSoldiersSkillsAsync(user_id, cardAdmiral.Id);
            skills = skills.Where(x => x.Position != 0).ToList();
            cardAdmiral.Skills = skills;
        }
        return CardSoldiersList;
    }
    public async Task<List<CardSoldiers>> GetUserCardSoldiersAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardSoldiers> list = await _userCardSoldiersRepository.GetUserCardSoldiersAsync(user_id, search, type, pageSize, offset, rare);
        
        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(user_id);
        var scienceFictionTask = ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        var researchTask = ResearchsService.Create().GetSumResearchsAsync(user_id);
        var archiveTask = ArchivesService.Create().GetSumArchivesAsync(user_id);
        var universeTask = UniversesService.Create().GetSumUniversesAsync(user_id);
        var hiinTask = HIINsService.Create().GetSumHIINsAsync(user_id);
        var sswnTask = SSWNsService.Create().GetSumSSWNsAsync(user_id);
        var hitnTask = HITNsService.Create().GetSumHITNsAsync(user_id);
        var hihnTask = HIHNsService.Create().GetSumHIHNsAsync(user_id);
        var hienTask = HIENsService.Create().GetSumHIENsAsync(user_id);
        var hicaTask = HICAsService.Create().GetSumHICAsAsync(user_id);
        var hirnTask = HIRNsService.Create().GetSumHIRNsAsync(user_id);
        var hidcTask = HIDCsService.Create().GetSumHIDCsAsync(user_id);
        var hicbTask = HICBsService.Create().GetSumHICBsAsync(user_id);
        var hisnTask = HISNsService.Create().GetSumHISNsAsync(user_id);
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

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

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        // list = await GetSkillsAsync(user_id, list);
        foreach(var card in list)
        {
            card.ApplyPowerStats(powerManagerData);
            card.ApplyScienceFictionStats(scienceFictionData);
            card.ApplyResearchStats(researchData);
            card.ApplyArchiveStats(archiveData);
            card.ApplyUniverseStats(universeData);
            card.ApplyHIINStats(hiinData);
            card.ApplySSWNStats(sswnData);
            card.ApplyHITNStats(hitnData);
            card.ApplyHIHNStats(hihnData);
            card.ApplyHIENStats(hienData);
            card.ApplyHICAStats(hicaData);
            card.ApplyHIRNStats(hirnData);
            card.ApplyHIDCStats(hidcData);
            card.ApplyHICBStats(hicbData);
            card.ApplyHISNStats(hisnData);
            card.ApplyAllAnimeStats(animeStatsData);
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<CardSoldiers>> GetUserCardSoldiersTeamAsync(string user_id, string teamId, string position)
    {
        List<CardSoldiers> list = await _userCardSoldiersRepository.GetUserCardSoldiersTeamAsync(user_id, teamId, position);
        
        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(user_id);
        var scienceFictionTask = ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        var researchTask = ResearchsService.Create().GetSumResearchsAsync(user_id);
        var archiveTask = ArchivesService.Create().GetSumArchivesAsync(user_id);
        var universeTask = UniversesService.Create().GetSumUniversesAsync(user_id);
        var hiinTask = HIINsService.Create().GetSumHIINsAsync(user_id);
        var sswnTask = SSWNsService.Create().GetSumSSWNsAsync(user_id);
        var hitnTask = HITNsService.Create().GetSumHITNsAsync(user_id);
        var hihnTask = HIHNsService.Create().GetSumHIHNsAsync(user_id);
        var hienTask = HIENsService.Create().GetSumHIENsAsync(user_id);
        var hicaTask = HICAsService.Create().GetSumHICAsAsync(user_id);
        var hirnTask = HIRNsService.Create().GetSumHIRNsAsync(user_id);
        var hidcTask = HIDCsService.Create().GetSumHIDCsAsync(user_id);
        var hicbTask = HICBsService.Create().GetSumHICBsAsync(user_id);
        var hisnTask = HISNsService.Create().GetSumHISNsAsync(user_id);
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

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

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        // list = await GetSkillsAsync(user_id, list);
        foreach(var card in list)
        {
            card.ApplyPowerStats(powerManagerData);
            card.ApplyScienceFictionStats(scienceFictionData);
            card.ApplyResearchStats(researchData);
            card.ApplyArchiveStats(archiveData);
            card.ApplyUniverseStats(universeData);
            card.ApplyHIINStats(hiinData);
            card.ApplySSWNStats(sswnData);
            card.ApplyHITNStats(hitnData);
            card.ApplyHIHNStats(hihnData);
            card.ApplyHIENStats(hienData);
            card.ApplyHICAStats(hicaData);
            card.ApplyHIRNStats(hirnData);
            card.ApplyHIDCStats(hidcData);
            card.ApplyHICBStats(hicbData);
            card.ApplyHISNStats(hisnData);
            card.ApplyAllAnimeStats(animeStatsData);
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<CardSoldiers>> GetUserCardSoldiersTeamWithoutPositionAsync(string user_id, string teamId)
    {
        List<CardSoldiers> list = await _userCardSoldiersRepository.GetUserCardSoldiersTeamWithoutPositionAsync(user_id, teamId);
        
        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(user_id);
        var scienceFictionTask = ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        var researchTask = ResearchsService.Create().GetSumResearchsAsync(user_id);
        var archiveTask = ArchivesService.Create().GetSumArchivesAsync(user_id);
        var universeTask = UniversesService.Create().GetSumUniversesAsync(user_id);
        var hiinTask = HIINsService.Create().GetSumHIINsAsync(user_id);
        var sswnTask = SSWNsService.Create().GetSumSSWNsAsync(user_id);
        var hitnTask = HITNsService.Create().GetSumHITNsAsync(user_id);
        var hihnTask = HIHNsService.Create().GetSumHIHNsAsync(user_id);
        var hienTask = HIENsService.Create().GetSumHIENsAsync(user_id);
        var hicaTask = HICAsService.Create().GetSumHICAsAsync(user_id);
        var hirnTask = HIRNsService.Create().GetSumHIRNsAsync(user_id);
        var hidcTask = HIDCsService.Create().GetSumHIDCsAsync(user_id);
        var hicbTask = HICBsService.Create().GetSumHICBsAsync(user_id);
        var hisnTask = HISNsService.Create().GetSumHISNsAsync(user_id);
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

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

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        // list = await GetSkillsAsync(user_id, list);
        foreach(var card in list)
        {
            card.ApplyPowerStats(powerManagerData);
            card.ApplyScienceFictionStats(scienceFictionData);
            card.ApplyResearchStats(researchData);
            card.ApplyArchiveStats(archiveData);
            card.ApplyUniverseStats(universeData);
            card.ApplyHIINStats(hiinData);
            card.ApplySSWNStats(sswnData);
            card.ApplyHITNStats(hitnData);
            card.ApplyHIHNStats(hihnData);
            card.ApplyHIENStats(hienData);
            card.ApplyHICAStats(hicaData);
            card.ApplyHIRNStats(hirnData);
            card.ApplyHIDCStats(hidcData);
            card.ApplyHICBStats(hicbData);
            card.ApplyHISNStats(hisnData);
            card.ApplyAllAnimeStats(animeStatsData);
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<Dictionary<string, int>> GetUniqueCardSoldiersTypesTeamAsync(string teamId)
    {
        return await _userCardSoldiersRepository.GetUniqueCardSoldiersTypesTeamAsync(teamId);
    }

    public async Task<bool> UpdateTeamCardSoldierAsync(string team_id, string position, string card_id)
    {
        return await _userCardSoldiersRepository.UpdateTeamCardSoldierAsync(team_id, position, card_id);
    }

    public async Task<int> GetUserCardSoldiersCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userCardSoldiersRepository.GetUserCardSoldiersCountAsync(user_id, search, type, rare);
    }

    public async Task<int> GetUserCardSoldiersTeamsPositionCountAsync(string user_id, string team_id, string position)
    {
        return await _userCardSoldiersRepository.GetUserCardSoldiersTeamsPositionCountAsync(user_id, team_id, position);
    }

    public async Task<int> GetUserCardSoldiersTeamsCountAsync(string user_id, string team_id)
    {
        return await _userCardSoldiersRepository.GetUserCardSoldiersTeamsCountAsync(user_id, team_id);
    }

    public async Task<bool> InsertUserCardSoldierAsync(CardSoldiers cardAdmiral)
    {
        return await _userCardSoldiersRepository.InsertUserCardSoldierAsync(cardAdmiral);
    }

    public async Task<bool> UpdateCardSoldierLevelAsync(CardSoldiers cardAdmiral, int level)
    {
        return await _userCardSoldiersRepository.UpdateCardSoldierLevelAsync(cardAdmiral, level);
    }

    public async Task<bool> UpdateCardSoldierBreakthroughAsync(CardSoldiers cardAdmiral, int star, double quantity)
    {
        return await _userCardSoldiersRepository.UpdateCardSoldierBreakthroughAsync(cardAdmiral, star, quantity);
    }

    public async Task<CardSoldiers> GetUserCardSoldierByIdAsync(string user_id, string Id)
    {
        CardSoldiers cardAdmiral = await _userCardSoldiersRepository.GetUserCardSoldierByIdAsync(user_id, Id);
        if (cardAdmiral == null) return null;

        // Bọc vào list để tái sử dụng logic
        List<CardSoldiers> list = new List<CardSoldiers> { cardAdmiral };

        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(user_id);
        var scienceFictionTask = ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        var researchTask = ResearchsService.Create().GetSumResearchsAsync(user_id);
        var archiveTask = ArchivesService.Create().GetSumArchivesAsync(user_id);
        var universeTask = UniversesService.Create().GetSumUniversesAsync(user_id);
        var hiinTask = HIINsService.Create().GetSumHIINsAsync(user_id);
        var sswnTask = SSWNsService.Create().GetSumSSWNsAsync(user_id);
        var hitnTask = HITNsService.Create().GetSumHITNsAsync(user_id);
        var hihnTask = HIHNsService.Create().GetSumHIHNsAsync(user_id);
        var hienTask = HIENsService.Create().GetSumHIENsAsync(user_id);
        var hicaTask = HICAsService.Create().GetSumHICAsAsync(user_id);
        var hirnTask = HIRNsService.Create().GetSumHIRNsAsync(user_id);
        var hidcTask = HIDCsService.Create().GetSumHIDCsAsync(user_id);
        var hicbTask = HICBsService.Create().GetSumHICBsAsync(user_id);
        var hisnTask = HISNsService.Create().GetSumHISNsAsync(user_id);
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

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

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        // list = await GetSkillsAsync(user_id, list);
        foreach(var card in list)
        {
            card.ApplyPowerStats(powerManagerData);
            card.ApplyScienceFictionStats(scienceFictionData);
            card.ApplyResearchStats(researchData);
            card.ApplyArchiveStats(archiveData);
            card.ApplyUniverseStats(universeData);
            card.ApplyHIINStats(hiinData);
            card.ApplySSWNStats(sswnData);
            card.ApplyHITNStats(hitnData);
            card.ApplyHIHNStats(hihnData);
            card.ApplyHIENStats(hienData);
            card.ApplyHICAStats(hicaData);
            card.ApplyHIRNStats(hirnData);
            card.ApplyHIDCStats(hidcData);
            card.ApplyHICBStats(hicbData);
            card.ApplyHISNStats(hisnData);
            card.ApplyAllAnimeStats(animeStatsData);
            card.RecalculatePower();
        }
        return list.FirstOrDefault();
    }

    public async Task<List<CardSoldiers>> GetAllUserCardSoldiersInTeamAsync(string user_id)
    {
        List<CardSoldiers> list = await _userCardSoldiersRepository.GetAllUserCardSoldiersInTeamAsync(user_id);
        
        var powerManagerTask = PowerManagerService.Create().GetUserStatsAsync(user_id);
        var scienceFictionTask = ScienceFictionService.Create().GetSumScienceFictionAsync(user_id);
        var researchTask = ResearchsService.Create().GetSumResearchsAsync(user_id);
        var archiveTask = ArchivesService.Create().GetSumArchivesAsync(user_id);
        var universeTask = UniversesService.Create().GetSumUniversesAsync(user_id);
        var hiinTask = HIINsService.Create().GetSumHIINsAsync(user_id);
        var sswnTask = SSWNsService.Create().GetSumSSWNsAsync(user_id);
        var hitnTask = HITNsService.Create().GetSumHITNsAsync(user_id);
        var hihnTask = HIHNsService.Create().GetSumHIHNsAsync(user_id);
        var hienTask = HIENsService.Create().GetSumHIENsAsync(user_id);
        var hicaTask = HICAsService.Create().GetSumHICAsAsync(user_id);
        var hirnTask = HIRNsService.Create().GetSumHIRNsAsync(user_id);
        var hidcTask = HIDCsService.Create().GetSumHIDCsAsync(user_id);
        var hicbTask = HICBsService.Create().GetSumHICBsAsync(user_id);
        var hisnTask = HISNsService.Create().GetSumHISNsAsync(user_id);
        var animeStatsTask = AnimeStatsService.Create().GetSumAnimeStatsAsync(user_id);

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

        // list = await GetAllSpiritBeastPowerAsync(user_id, list);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        // list = await GetAllEquipmentPowerAsync(user_id, list);
        // list = await GetAllRankPowerAsync(user_id, list);
        // list = await GetAllMasterPowerAsync(user_id, list);
        // list = await GetSkillsAsync(user_id, list);
        foreach(var card in list)
        {
            card.ApplyPowerStats(powerManagerData);
            card.ApplyScienceFictionStats(scienceFictionData);
            card.ApplyResearchStats(researchData);
            card.ApplyArchiveStats(archiveData);
            card.ApplyUniverseStats(universeData);
            card.ApplyHIINStats(hiinData);
            card.ApplySSWNStats(sswnData);
            card.ApplyHITNStats(hitnData);
            card.ApplyHIHNStats(hihnData);
            card.ApplyHIENStats(hienData);
            card.ApplyHICAStats(hicaData);
            card.ApplyHIRNStats(hirnData);
            card.ApplyHIDCStats(hidcData);
            card.ApplyHICBStats(hicbData);
            card.ApplyHISNStats(hisnData);
            card.ApplyAllAnimeStats(animeStatsData);
            card.RecalculatePower();
        }
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<bool> InsertOrUpdateUserCardSoldiersBatchAsync(List<CardSoldiers> cardAdmirals)
    {
        return await _userCardSoldiersRepository.InsertOrUpdateUserCardSoldiersBatchAsync(cardAdmirals);
    }
}

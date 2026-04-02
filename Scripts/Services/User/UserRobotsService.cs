using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRobotsService : IUserRobotsService
{
     private static UserRobotsService _instance;
    private readonly IUserRobotsRepository _userRobotsRepository;

    public UserRobotsService(IUserRobotsRepository userRobotsRepository)
    {
        _userRobotsRepository = userRobotsRepository;
    }

    public static UserRobotsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserRobotsService(new UserRobotsRepository());
        }
        return _instance;
    }

    public async Task<Robots> GetNewLevelPowerAsync(Robots c, double coefficient)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        Robots orginCard = await _service.GetRobotByIdAsync(c.Id);
        Robots robot = new Robots
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
        robot.Power = EvaluatePower.CalculatePower(
            robot.Health,
            robot.PhysicalAttack, robot.PhysicalDefense,
            robot.MagicalAttack, robot.MagicalDefense,
            robot.ChemicalAttack, robot.ChemicalDefense,
            robot.AtomicAttack, robot.AtomicDefense,
            robot.MentalAttack, robot.MentalDefense,
            robot.Speed,
            robot.CriticalDamageRate, robot.CriticalRate, robot.CriticalResistanceRate, robot.IgnoreCriticalRate,
            robot.PenetrationRate, robot.PenetrationResistanceRate, robot.EvasionRate,
            robot.DamageAbsorptionRate, robot.IgnoreDamageAbsorptionRate, robot.AbsorbedDamageRate,
            robot.VitalityRegenerationRate, robot.VitalityRegenerationResistanceRate,
            robot.AccuracyRate, robot.LifestealRate,
            robot.ShieldStrength, robot.Tenacity, robot.ResistanceRate,
            robot.ComboRate, robot.IgnoreComboRate, robot.ComboDamageRate, robot.ComboResistanceRate,
            robot.StunRate, robot.IgnoreStunRate,
            robot.ReflectionRate, robot.IgnoreReflectionRate, robot.ReflectionDamageRate, robot.ReflectionResistanceRate,
            robot.Mana, robot.ManaRegenerationRate,
            robot.DamageToDifferentFactionRate, robot.ResistanceToDifferentFactionRate,
            robot.DamageToSameFactionRate, robot.ResistanceToSameFactionRate,
            robot.NormalDamageRate, robot.NormalResistanceRate,
            robot.SkillDamageRate, robot.SkillResistanceRate
        );
        return robot;
    }
    public async Task<Robots> GetNewBreakthroughPowerAsync(Robots c, double coefficient)
    {
        IRobotsRepository _repository = new RobotsRepository();
        RobotsService _service = new RobotsService(_repository);
        Robots orginCard = await _service.GetRobotByIdAsync(c.Id);
        Robots robot = new Robots
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
        robot.Power = EvaluatePower.CalculatePower(
            robot.Health,
            robot.PhysicalAttack, robot.PhysicalDefense,
            robot.MagicalAttack, robot.MagicalDefense,
            robot.ChemicalAttack, robot.ChemicalDefense,
            robot.AtomicAttack, robot.AtomicDefense,
            robot.MentalAttack, robot.MentalDefense,
            robot.Speed,
            robot.CriticalDamageRate, robot.CriticalRate, robot.CriticalResistanceRate, robot.IgnoreCriticalRate,
            robot.PenetrationRate, robot.PenetrationResistanceRate, robot.EvasionRate,
            robot.DamageAbsorptionRate, robot.IgnoreDamageAbsorptionRate, robot.AbsorbedDamageRate,
            robot.VitalityRegenerationRate, robot.VitalityRegenerationResistanceRate,
            robot.AccuracyRate, robot.LifestealRate,
            robot.ShieldStrength, robot.Tenacity, robot.ResistanceRate,
            robot.ComboRate, robot.IgnoreComboRate, robot.ComboDamageRate, robot.ComboResistanceRate,
            robot.StunRate, robot.IgnoreStunRate,
            robot.ReflectionRate, robot.IgnoreReflectionRate, robot.ReflectionDamageRate, robot.ReflectionResistanceRate,
            robot.Mana, robot.ManaRegenerationRate,
            robot.DamageToDifferentFactionRate, robot.ResistanceToDifferentFactionRate,
            robot.DamageToSameFactionRate, robot.ResistanceToSameFactionRate,
            robot.NormalDamageRate, robot.NormalResistanceRate,
            robot.SkillDamageRate, robot.SkillResistanceRate
        );
        return robot;
    }

    public async Task<List<Robots>> GetUserRobotsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Robots> list = await _userRobotsRepository.GetUserRobotsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserRobotsCountAsync(string user_id, string search, string rare)
    {
        return await _userRobotsRepository.GetUserRobotsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserRobotAsync(Robots Robots, string userId)
    {
        return await _userRobotsRepository.InsertUserRobotAsync(Robots, userId);
    }

    public async Task<bool> UpdateRobotLevelAsync(Robots Robots, int cardLevel)
    {
        return await _userRobotsRepository.UpdateRobotLevelAsync(Robots, cardLevel);
    }

    public async Task<bool> UpdateRobotBreakthroughAsync(Robots Robots, int star, double quantity)
    {
        return await _userRobotsRepository.UpdateRobotBreakthroughAsync(Robots, star, quantity);
    }

    public async Task<Robots> GetUserRobotByIdAsync(string user_id, string Id)
    {
        return await _userRobotsRepository.GetUserRobotByIdAsync(user_id, Id);
    }

    public async Task<Robots> SumPowerUserRobotsAsync()
    {
        return await _userRobotsRepository.SumPowerUserRobotsAsync();
    }
}

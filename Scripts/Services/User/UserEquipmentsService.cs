using System.Collections.Generic;
using System.Threading.Tasks;

public class UserEquipmentsService : IUserEquipmentsService
{
    private static UserEquipmentsService _instance;
    private IUserEquipmentsRepository _userEquipmentsRepository;

    public UserEquipmentsService(IUserEquipmentsRepository userEquipmentsRepository)
    {
        _userEquipmentsRepository = userEquipmentsRepository;
    }

    public static UserEquipmentsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserEquipmentsService(new UserEquipmentsRepository());
        }
        return _instance;
    }

    public async Task<List<Equipments>> GetAllRankPowerAsync(string user_id, List<Equipments> EquipmentsList)
    {
        foreach (var c in EquipmentsList)
        {
            Rank rank = await UserEquipmentsRankService.Create().GetSumEquipmentsRankAsync(user_id, c.Id);
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
            c.PenetrationRate = c.PenetrationRate + rank.PenetrationRate;
            c.EvasionRate = c.EvasionRate + rank.EvasionRate;
            c.DamageAbsorptionRate = c.DamageAbsorptionRate + rank.DamageAbsorptionRate;
            c.VitalityRegenerationRate = c.VitalityRegenerationRate + rank.VitalityRegenerationRate;
            c.AccuracyRate = c.AccuracyRate + rank.AccuracyRate;
            c.LifestealRate = c.LifestealRate + rank.LifestealRate;
            c.ShieldStrength = c.ShieldStrength + rank.ShieldStrength;
            c.Tenacity = c.Tenacity + rank.Tenacity;
            c.ResistanceRate = c.ResistanceRate + rank.ResistanceRate;
            c.ComboRate = c.ComboRate + rank.ComboRate;
            c.ReflectionRate = c.ReflectionRate + rank.ReflectionRate;
            c.Mana = c.Mana + rank.Mana;
            c.ManaRegenerationRate = c.ManaRegenerationRate + rank.ManaRegenerationRate;
            c.DamageToDifferentFactionRate = c.DamageToDifferentFactionRate + rank.DamageToDifferentFactionRate;
            c.ResistanceToDifferentFactionRate = c.ResistanceToDifferentFactionRate + rank.ResistanceToDifferentFactionRate;
            c.DamageToSameFactionRate = c.DamageToSameFactionRate + rank.DamageToSameFactionRate;
            c.ResistanceToSameFactionRate = c.ResistanceToSameFactionRate + rank.ResistanceToSameFactionRate;

            c.Power = EvaluatePower.CalculatePower(
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
        return EquipmentsList;
    }
    public async Task<Equipments> GetNewLevelPowerAsync(Equipments c, double coefficient)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        Equipments orginCard = await _service.GetEquipmentByIdAsync(c.Id);
        Equipments equipments = new Equipments
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
            IgnoreReflectionRate = c.IgnoreReflectionRate + orginCard.IgnoreReflectionRate * coefficient,
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
            SkillResistanceRate = c.SkillResistanceRate + orginCard.SkillResistanceRate * coefficient,
            SpecialHealth = c.SpecialHealth + orginCard.SpecialHealth * coefficient,
            SpecialPhysicalAttack = c.SpecialPhysicalAttack + orginCard.SpecialPhysicalAttack * coefficient,
            SpecialPhysicalDefense = c.SpecialPhysicalDefense + orginCard.SpecialPhysicalDefense * coefficient,
            SpecialMagicalAttack = c.SpecialMagicalAttack + orginCard.SpecialMagicalAttack * coefficient,
            SpecialMagicalDefense = c.SpecialMagicalDefense + orginCard.SpecialMagicalDefense * coefficient,
            SpecialChemicalAttack = c.SpecialChemicalAttack + orginCard.SpecialChemicalAttack * coefficient,
            SpecialChemicalDefense = c.SpecialChemicalDefense + orginCard.SpecialChemicalDefense * coefficient,
            SpecialAtomicAttack = c.SpecialAtomicAttack + orginCard.SpecialAtomicAttack * coefficient,
            SpecialAtomicDefense = c.SpecialAtomicDefense + orginCard.SpecialAtomicDefense * coefficient,
            SpecialMentalAttack = c.SpecialMentalAttack + orginCard.SpecialMentalAttack * coefficient,
            SpecialMentalDefense = c.SpecialMentalDefense + orginCard.SpecialMentalDefense * coefficient,
            SpecialSpeed = c.SpecialSpeed + orginCard.SpecialSpeed * coefficient,
        };
        equipments.Power = EvaluatePower.CalculatePower(
            equipments.Health + equipments.SpecialHealth,
            equipments.PhysicalAttack + equipments.SpecialPhysicalAttack, equipments.PhysicalDefense + equipments.SpecialPhysicalDefense,
            equipments.MagicalAttack + equipments.SpecialMagicalAttack, equipments.MagicalDefense + equipments.SpecialMagicalDefense,
            equipments.ChemicalAttack + equipments.SpecialChemicalAttack, equipments.ChemicalDefense + equipments.SpecialChemicalDefense,
            equipments.AtomicAttack + equipments.SpecialAtomicAttack, equipments.AtomicDefense + equipments.SpecialAtomicDefense,
            equipments.MentalAttack + equipments.MentalAttack, equipments.MentalDefense + equipments.MentalDefense,
            equipments.Speed,
            equipments.CriticalDamageRate, equipments.CriticalRate, equipments.CriticalResistanceRate, equipments.IgnoreCriticalRate,
            equipments.PenetrationRate, equipments.PenetrationResistanceRate, equipments.EvasionRate,
            equipments.DamageAbsorptionRate, equipments.IgnoreDamageAbsorptionRate, equipments.AbsorbedDamageRate,
            equipments.VitalityRegenerationRate, equipments.VitalityRegenerationResistanceRate,
            equipments.AccuracyRate, equipments.LifestealRate,
            equipments.ShieldStrength, equipments.Tenacity, equipments.ResistanceRate,
            equipments.ComboRate, equipments.IgnoreComboRate, equipments.ComboDamageRate, equipments.ComboResistanceRate,
            equipments.StunRate, equipments.IgnoreStunRate,
            equipments.ReflectionRate, equipments.IgnoreReflectionRate, equipments.ReflectionDamageRate, equipments.ReflectionResistanceRate,
            equipments.Mana, equipments.ManaRegenerationRate,
            equipments.DamageToDifferentFactionRate, equipments.ResistanceToDifferentFactionRate,
            equipments.DamageToSameFactionRate, equipments.ResistanceToSameFactionRate,
            equipments.NormalDamageRate, equipments.NormalResistanceRate,
            equipments.SkillDamageRate, equipments.SkillResistanceRate
        );
        return equipments;
    }
    public async Task<Equipments> GetNewBreakthroughPowerAsync(Equipments c, double coefficient)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        Equipments orginCard = await _service.GetEquipmentByIdAsync(c.Id);
        Equipments equipments = new Equipments
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
            IgnoreReflectionRate = c.IgnoreReflectionRate + orginCard.IgnoreReflectionRate * coefficient,
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
            SkillResistanceRate = c.SkillResistanceRate + orginCard.SkillResistanceRate * coefficient,
            SpecialHealth = c.SpecialHealth + orginCard.SpecialHealth * coefficient,
            SpecialPhysicalAttack = c.SpecialPhysicalAttack + orginCard.SpecialPhysicalAttack * coefficient,
            SpecialPhysicalDefense = c.SpecialPhysicalDefense + orginCard.SpecialPhysicalDefense * coefficient,
            SpecialMagicalAttack = c.SpecialMagicalAttack + orginCard.SpecialMagicalAttack * coefficient,
            SpecialMagicalDefense = c.SpecialMagicalDefense + orginCard.SpecialMagicalDefense * coefficient,
            SpecialChemicalAttack = c.SpecialChemicalAttack + orginCard.SpecialChemicalAttack * coefficient,
            SpecialChemicalDefense = c.SpecialChemicalDefense + orginCard.SpecialChemicalDefense * coefficient,
            SpecialAtomicAttack = c.SpecialAtomicAttack + orginCard.SpecialAtomicAttack * coefficient,
            SpecialAtomicDefense = c.SpecialAtomicDefense + orginCard.SpecialAtomicDefense * coefficient,
            SpecialMentalAttack = c.SpecialMentalAttack + orginCard.SpecialMentalAttack * coefficient,
            SpecialMentalDefense = c.SpecialMentalDefense + orginCard.SpecialMentalDefense * coefficient,
            SpecialSpeed = c.SpecialSpeed + orginCard.SpecialSpeed * coefficient,
        };
        equipments.Power = EvaluatePower.CalculatePower(
            equipments.Health + equipments.SpecialHealth,
            equipments.PhysicalAttack + equipments.SpecialPhysicalAttack, equipments.PhysicalDefense + equipments.SpecialPhysicalDefense,
            equipments.MagicalAttack + equipments.SpecialMagicalAttack, equipments.MagicalDefense + equipments.SpecialMagicalDefense,
            equipments.ChemicalAttack + equipments.SpecialChemicalAttack, equipments.ChemicalDefense + equipments.SpecialChemicalDefense,
            equipments.AtomicAttack + equipments.SpecialAtomicAttack, equipments.AtomicDefense + equipments.SpecialAtomicDefense,
            equipments.MentalAttack + equipments.MentalAttack, equipments.MentalDefense + equipments.MentalDefense,
            equipments.Speed,
            equipments.CriticalDamageRate, equipments.CriticalRate, equipments.CriticalResistanceRate, equipments.IgnoreCriticalRate,
            equipments.PenetrationRate, equipments.PenetrationResistanceRate, equipments.EvasionRate,
            equipments.DamageAbsorptionRate, equipments.IgnoreDamageAbsorptionRate, equipments.AbsorbedDamageRate,
            equipments.VitalityRegenerationRate, equipments.VitalityRegenerationResistanceRate,
            equipments.AccuracyRate, equipments.LifestealRate,
            equipments.ShieldStrength, equipments.Tenacity, equipments.ResistanceRate,
            equipments.ComboRate, equipments.IgnoreComboRate, equipments.ComboDamageRate, equipments.ComboResistanceRate,
            equipments.StunRate, equipments.IgnoreStunRate,
            equipments.ReflectionRate, equipments.IgnoreReflectionRate, equipments.ReflectionDamageRate, equipments.ReflectionResistanceRate,
            equipments.Mana, equipments.ManaRegenerationRate,
            equipments.DamageToDifferentFactionRate, equipments.ResistanceToDifferentFactionRate,
            equipments.DamageToSameFactionRate, equipments.ResistanceToSameFactionRate,
            equipments.NormalDamageRate, equipments.NormalResistanceRate,
            equipments.SkillDamageRate, equipments.SkillResistanceRate
        );
        return equipments;
    }

    public async Task<List<Equipments>> GetUserEquipmentsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetUserEquipmentsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetUserAllEquipmentsAsync(string user_id)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetUserAllEquipmentsAsync(user_id);
        list = QualityEvaluator.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserEquipmentsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userEquipmentsRepository.GetUserEquipmentsCountAsync(user_id, search, type, rare);
    }

    public async Task<Equipments> GetUserEquipmentsByIdAsync(string user_id, string Id)
    {
        return await _userEquipmentsRepository.GetUserEquipmentsByIdAsync(user_id, Id);
    }

    public async Task<bool> BuyEquipmentAsync(string Id, double quantity)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        return await _userEquipmentsRepository.BuyEquipmentAsync(Id, await _service.GetEquipmentByIdAsync(Id), quantity);
    }

    public async Task<bool> UpdateEquipmentsLevelAsync(Equipments equipments, int cardLevel)
    {
        return await _userEquipmentsRepository.UpdateEquipmentsLevelAsync(equipments, cardLevel);
    }

    public async Task<bool> UpdateEquipmentsBreakthroughAsync(Equipments equipments, int star, double quantity)
    {
        return await _userEquipmentsRepository.UpdateEquipmentsBreakthroughAsync(equipments, star, quantity);
    }

    public async Task UpdateUserCurrencyAsync(string Id, double quantity)
    {
        await _userEquipmentsRepository.UpdateUserCurrencyAsync(Id, quantity);
    }

    public async Task InsertCardHeroEquipmentsAsync(string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardHeroEquipmentsAsync(Id, equipments, position);
    }

    public async Task InsertCardCaptainEquipmentsAsync(string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardCaptainEquipmentsAsync(Id, equipments, position);
    }

    public async Task InsertCardColonelEquipmentsAsync(string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardColonelEquipmentsAsync(Id, equipments, position);
    }

    public async Task InsertCardGeneralEquipmentsAsync(string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardGeneralEquipmentsAsync(Id, equipments, position);
    }

    public async Task InsertCardAdmiralEquipmentsAsync(string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardAdmiralEquipmentsAsync(Id, equipments, position);
    }

    public async Task InsertCardMonsterEquipmentsAsync(string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardMonsterEquipmentsAsync(Id, equipments, position);
    }

    public async Task InsertCardMilitaryEquipmentsAsync(string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardMilitaryEquipmentsAsync(Id, equipments, position);
    }

    public async Task InsertCardSpellEquipmentsAsync(string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardSpellEquipmentsAsync(Id, equipments, position);
    }

    public async Task InsertBookEquipmentsAsync(string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertBookEquipmentsAsync(Id, equipments, position);
    }

    public async Task InsertPetEquipmentsAsync(string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertPetEquipmentsAsync(Id, equipments, position);
    }

    public async Task<List<Equipments>> GetCardHeroesEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardHeroesEquipmentsAsync(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardCaptainsEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardCaptainsEquipmentsAsync(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardColonelsEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardColonelsEquipmentsAsync(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardGeneralsEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardGeneralsEquipmentsAsync(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardAdmiralsEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardAdmiralsEquipmentsAsync(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardMonstersEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardMonstersEquipmentsAsync(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardMilitariesEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardMilitariesEquipmentsAsync(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardSpellsEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardSpellsEquipmentsAsync(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetBooksEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetBooksEquipmentsAsync(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetPetsEquipmentsAsync(string user_id, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetPetsEquipmentsAsync(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardHeroesEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardHeroesEquipmentsAsync(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardCaptainsEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardCaptainsEquipmentsAsync(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardColonelsEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardColonelsEquipmentsAsync(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardGeneralsEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardGeneralsEquipmentsAsync(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardAdmiralsEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardAdmiralsEquipmentsAsync(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardMonstersEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardMonstersEquipmentsAsync(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardMilitariesEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardMilitariesEquipmentsAsync(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardSpellsEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardSpellsEquipmentsAsync(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllBooksEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllBooksEquipmentsAsync(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllPetsEquipmentsAsync(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllPetsEquipmentsAsync(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<Equipments> GetAllEquipmentsByCardHeorIdAsync(string user_id, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardHeroIdAsync(user_id, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardCaptainIdAsync(string user_id, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardCaptainIdAsync(user_id, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardColonelIdAsync(string user_id, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardColonelIdAsync(user_id, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardGeneralIdAsync(string user_id, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardGeneralIdAsync(user_id, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardAdmiralIdAsync(string user_id, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardAdmiralIdAsync(user_id, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardMonsterIdAsync(string user_id, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardMonsterIdAsync(user_id, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardMilitaryIdAsync(string user_id, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardMilitaryIdAsync(user_id, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardSpellIdAsync(string user_id, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardSpellIdAsync(user_id, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByBookIdAsync(string user_id, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByBookIdAsync(user_id, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByPetIdAsync(string user_id, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByPetIdAsync(user_id, Id);
    }

    // Hàm cho CardHero
    public async Task<bool> EquipAllEquipmentsOfTypeToCardHeroAsync(string cardHeroId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardHeroAsync(cardHeroId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardHeroAsync(string cardHeroId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardHeroAsync(cardHeroId, allEquipments);
    }

    // Hàm cho CardCaptain
    public async Task<bool> EquipAllEquipmentsOfTypeToCardCaptainAsync(string cardCaptainId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardCaptainAsync(cardCaptainId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardCaptainAsync(string cardCaptainId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardCaptainAsync(cardCaptainId, allEquipments);
    }

    // Hàm cho CardColonel
    public async Task<bool> EquipAllEquipmentsOfTypeToCardColonelAsync(string cardColonelId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardColonelAsync(cardColonelId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardColonelAsync(string cardColonelId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardColonelAsync(cardColonelId, allEquipments);
    }

    // Hàm cho CardGeneral
    public async Task<bool> EquipAllEquipmentsOfTypeToCardGeneralAsync(string cardGeneralId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardGeneralAsync(cardGeneralId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardGeneralAsync(string cardGeneralId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardGeneralAsync(cardGeneralId, allEquipments);
    }

    // Hàm cho CardAdmiral
    public async Task<bool> EquipAllEquipmentsOfTypeToCardAdmiralAsync(string cardAdmiralId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardAdmiralAsync(cardAdmiralId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardAdmiralAsync(string cardAdmiralId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardAdmiralAsync(cardAdmiralId, allEquipments);
    }

    // Hàm cho CardMonster
    public async Task<bool> EquipAllEquipmentsOfTypeToCardMonsterAsync(string cardMonsterId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardMonsterAsync(cardMonsterId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardMonsterAsync(string cardMonsterId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardMonsterAsync(cardMonsterId, allEquipments);
    }

    // Hàm cho CardMilitary
    public async Task<bool> EquipAllEquipmentsOfTypeToCardMilitaryAsync(string cardMilitaryId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardMilitaryAsync(cardMilitaryId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardMilitaryAsync(string cardMilitaryId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardMilitaryAsync(cardMilitaryId, allEquipments);
    }

    // Hàm cho CardSpell
    public async Task<bool> EquipAllEquipmentsOfTypeToCardSpellAsync(string cardSpellId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardSpellAsync(cardSpellId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardSpellAsync(string cardSpellId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardSpellAsync(cardSpellId, allEquipments);
    }

    // Hàm cho Book
    public async Task<bool> EquipAllEquipmentsOfTypeToBookAsync(string bookId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToBookAsync(bookId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToBookAsync(string bookId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToBookAsync(bookId, allEquipments);
    }

    // Hàm cho Pet
    public async Task<bool> EquipAllEquipmentsOfTypeToPetAsync(string petId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToPetAsync(petId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToPetAsync(string petId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(User.CurrentUserId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToPetAsync(petId, allEquipments);
    }

}

using System.Collections.Generic;

public class UserEquipmentsService : IUserEquipmentsService
{
    private IUserEquipmentsRepository _userEquipmentsRepository;

    public UserEquipmentsService(IUserEquipmentsRepository userEquipmentsRepository)
    {
        _userEquipmentsRepository = userEquipmentsRepository;
    }

    public static UserEquipmentsService Create()
    {
        return new UserEquipmentsService(new UserEquipmentsRepository());
    }

    public List<Equipments> GetAllRankPower(string user_id, List<Equipments> EquipmentsList)
    {
        foreach (var c in EquipmentsList)
        {
            Rank rank = UserEquipmentsRankService.Create().GetSumEquipmentsRank(user_id, c.Id);
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
    public Equipments GetNewLevelPower(Equipments c, double coefficient)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        Equipments orginCard = _service.GetEquipmentById(c.Id);
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
    public Equipments GetNewBreakthroughPower(Equipments c, double coefficient)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        Equipments orginCard = _service.GetEquipmentById(c.Id);
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

    public List<Equipments> GetUserEquipments(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Equipments> list = _userEquipmentsRepository.GetUserEquipments(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserEquipmentsCount(string user_id, string type, string rare)
    {
        return _userEquipmentsRepository.GetUserEquipmentsCount(user_id, type, rare);
    }

    public Equipments GetUserEquipmentsById(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetUserEquipmentsById(user_id, Id);
    }

    public bool BuyEquipment(string Id)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        return _userEquipmentsRepository.BuyEquipment(Id, _service.GetEquipmentById(Id));
    }

    public bool UpdateEquipmentsLevel(Equipments equipments, int cardLevel)
    {
        return _userEquipmentsRepository.UpdateEquipmentsLevel(equipments, cardLevel);
    }

    public bool UpdateEquipmentsBreakthrough(Equipments equipments, int star, int quantity)
    {
        return _userEquipmentsRepository.UpdateEquipmentsBreakthrough(equipments, star, quantity);
    }

    public void UpdateUserCurrency(string Id)
    {
        _userEquipmentsRepository.UpdateUserCurrency(Id);
    }

    public void InsertCardHeroesEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardHeroesEquipments(Id, equipments, position);
    }

    public void InsertCardCaptainsEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardCaptainsEquipments(Id, equipments, position);
    }

    public void InsertCardColonelsEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardColonelsEquipments(Id, equipments, position);
    }

    public void InsertCardGeneralsEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardGeneralsEquipments(Id, equipments, position);
    }

    public void InsertCardAdmiralsEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardAdmiralsEquipments(Id, equipments, position);
    }

    public void InsertCardMonstersEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardMonstersEquipments(Id, equipments, position);
    }

    public void InsertCardMilitaryEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardMilitaryEquipments(Id, equipments, position);
    }

    public void InsertCardSpellEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardSpellEquipments(Id, equipments, position);
    }

    public void InsertBooksEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertBooksEquipments(Id, equipments, position);
    }

    public void InsertPetsEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertPetsEquipments(Id, equipments, position);
    }

    public List<Equipments> GetCardHeroesEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardHeroesEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardCaptainsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardCaptainsEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardColonelsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardColonelsEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardGeneralsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardGeneralsEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardAdmiralsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardAdmiralsEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardMonstersEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardMonstersEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardMilitaryEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardMilitaryEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardSpellEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardSpellEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetBooksEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetBooksEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetPetsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetPetsEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardHeroesEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardHeroesEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardCaptainsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardCaptainsEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardColonelsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardColonelsEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardGeneralsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardGeneralsEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardAdmiralsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardAdmiralsEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardMonstersEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardMonstersEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardMilitaryEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardMilitaryEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardSpellEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardSpellEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllBooksEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllBooksEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllPetsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllPetsEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public Equipments GetAllEquipmentsByCardHeoresId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardHeoresId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardCaptainsId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardCaptainsId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardColonelsId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardColonelsId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardGeneralsId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardGeneralsId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardAdmiralsId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardAdmiralsId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardMonstersId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardMonstersId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardMilitaryId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardMilitaryId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardSpellId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardSpellId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByBooksId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByBooksId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByPetsId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByPetsId(user_id, Id);
    }
}

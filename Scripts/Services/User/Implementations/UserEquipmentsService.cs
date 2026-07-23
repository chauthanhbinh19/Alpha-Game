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

    public async Task<List<Equipments>> GetAllRankPowerAsync(string userId, List<Equipments> EquipmentsList)
    {
        foreach (var c in EquipmentsList)
        {
            Rank rank = await UserEquipmentsRankService.Create().GetSumUserEquipmentsRankAsync(userId, c.Id);
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
        return EquipmentsList;
    }

    public async Task<List<Equipments>> GetUserEquipmentsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetUserEquipmentsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetUserAllEquipmentsAsync(string userId)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetUserAllEquipmentsAsync(userId);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserEquipmentsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userEquipmentsRepository.GetUserEquipmentsCountAsync(userId, search, type, rare);
    }

    public async Task<Equipments> GetUserEquipmentsByIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetUserEquipmentsByIdAsync(userId, Id);
    }

    public async Task<bool> InsertUserEquipmentAsync(string userId, string Id, double quantity)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        return await _userEquipmentsRepository.InsertUserEquipmentAsync(userId, Id, await _service.GetEquipmentByIdAsync(Id), quantity);
    }

    public async Task<bool> UpdateUserEquipmentLevelAsync(string userId, Equipments equipments)
    {
        return await _userEquipmentsRepository.UpdateUserEquipmentsLevelAsync(userId, equipments);
    }

    public async Task<bool> UpdateUserEquipmentStarAsync(string userId, Equipments equipments)
    {
        return await _userEquipmentsRepository.UpdateUserEquipmentsStarAsync(userId, equipments);
    }

    public async Task<bool> UpdateUserEquipmentsBreakthroughAsync(string userId, Equipments equipments, int star, double quantity)
    {
        return await _userEquipmentsRepository.UpdateUserEquipmentsBreakthroughAsync(userId, equipments, star, quantity);
    }

    public async Task UpdateUserCurrencyAsync(string userId, string Id, double quantity)
    {
        await _userEquipmentsRepository.UpdateUserCurrencyAsync(userId, Id, quantity);
    }

    public async Task InsertCardHeroEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardHeroEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertCardCaptainEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardCaptainEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertCardColonelEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardColonelEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertCardGeneralEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardGeneralEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertCardAdmiralEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardAdmiralEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertCardMonsterEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardMonsterEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertCardMilitaryEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardMilitaryEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertCardSpellEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardSpellEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertBookEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertBookEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertPetEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertPetEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertCardSoldierEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertCardSoldierEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task<List<Equipments>> GetCardHeroesEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardHeroesEquipmentsAsync(userId, card_id, type);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardCaptainsEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardCaptainsEquipmentsAsync(userId, card_id, type);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardColonelsEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardColonelsEquipmentsAsync(userId, card_id, type);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardGeneralsEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardGeneralsEquipmentsAsync(userId, card_id, type);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardAdmiralsEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardAdmiralsEquipmentsAsync(userId, card_id, type);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardMonstersEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardMonstersEquipmentsAsync(userId, card_id, type);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardMilitariesEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardMilitariesEquipmentsAsync(userId, card_id, type);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardSpellsEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardSpellsEquipmentsAsync(userId, card_id, type);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetBooksEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetBooksEquipmentsAsync(userId, card_id, type);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetPetsEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetPetsEquipmentsAsync(userId, card_id, type);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetCardSoldiersEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetCardSoldiersEquipmentsAsync(userId, card_id, type);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardHeroesEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardHeroesEquipmentsAsync(userId, type, limit, offset, status);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardCaptainsEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardCaptainsEquipmentsAsync(userId, type, limit, offset, status);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardColonelsEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardColonelsEquipmentsAsync(userId, type, limit, offset, status);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardGeneralsEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardGeneralsEquipmentsAsync(userId, type, limit, offset, status);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardAdmiralsEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardAdmiralsEquipmentsAsync(userId, type, limit, offset, status);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardMonstersEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardMonstersEquipmentsAsync(userId, type, limit, offset, status);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardMilitariesEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardMilitariesEquipmentsAsync(userId, type, limit, offset, status);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardSpellsEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardSpellsEquipmentsAsync(userId, type, limit, offset, status);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllBooksEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllBooksEquipmentsAsync(userId, type, limit, offset, status);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllPetsEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllPetsEquipmentsAsync(userId, type, limit, offset, status);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<List<Equipments>> GetAllCardSoldiersEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> list = await _userEquipmentsRepository.GetAllCardSoldiersEquipmentsAsync(userId, type, limit, offset, status);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<Equipments> GetAllEquipmentsByCardHeorIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardHeroIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardCaptainIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardCaptainIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardColonelIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardColonelIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardGeneralIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardGeneralIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardAdmiralIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardAdmiralIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardMonsterIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardMonsterIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardMilitaryIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardMilitaryIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardSpellIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardSpellIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByBookIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByBookIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByPetIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByPetIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllEquipmentsByCardSoldierIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllEquipmentsByCardSoldierIdAsync(userId, Id);
    }

    // Hàm cho CardHero
    public async Task<bool> EquipAllEquipmentsOfTypeToCardHeroAsync(string userId, string cardHeroId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardHeroAsync(userId, cardHeroId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardHeroAsync(string userId, string cardHeroId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardHeroAsync(userId, cardHeroId, allEquipments);
    }

    // Hàm cho CardCaptain
    public async Task<bool> EquipAllEquipmentsOfTypeToCardCaptainAsync(string userId, string cardCaptainId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardCaptainAsync(userId, cardCaptainId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardCaptainAsync(string userId, string cardCaptainId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardCaptainAsync(userId, cardCaptainId, allEquipments);
    }

    // Hàm cho CardColonel
    public async Task<bool> EquipAllEquipmentsOfTypeToCardColonelAsync(string userId, string cardColonelId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardColonelAsync(userId, cardColonelId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardColonelAsync(string userId, string cardColonelId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardColonelAsync(userId, cardColonelId, allEquipments);
    }

    // Hàm cho CardGeneral
    public async Task<bool> EquipAllEquipmentsOfTypeToCardGeneralAsync(string userId, string cardGeneralId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardGeneralAsync(userId, cardGeneralId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardGeneralAsync(string userId, string cardGeneralId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardGeneralAsync(userId, cardGeneralId, allEquipments);
    }

    // Hàm cho CardAdmiral
    public async Task<bool> EquipAllEquipmentsOfTypeToCardAdmiralAsync(string userId, string cardAdmiralId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardAdmiralAsync(userId, cardAdmiralId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardAdmiralAsync(string userId, string cardAdmiralId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardAdmiralAsync(userId, cardAdmiralId, allEquipments);
    }

    // Hàm cho CardMonster
    public async Task<bool> EquipAllEquipmentsOfTypeToCardMonsterAsync(string userId, string cardMonsterId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardMonsterAsync(userId, cardMonsterId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardMonsterAsync(string userId, string cardMonsterId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardMonsterAsync(userId, cardMonsterId, allEquipments);
    }

    // Hàm cho CardMilitary
    public async Task<bool> EquipAllEquipmentsOfTypeToCardMilitaryAsync(string userId, string cardMilitaryId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardMilitaryAsync(userId, cardMilitaryId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardMilitaryAsync(string userId, string cardMilitaryId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardMilitaryAsync(userId, cardMilitaryId, allEquipments);
    }

    // Hàm cho CardSpell
    public async Task<bool> EquipAllEquipmentsOfTypeToCardSpellAsync(string userId, string cardSpellId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardSpellAsync(userId, cardSpellId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardSpellAsync(string userId, string cardSpellId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardSpellAsync(userId, cardSpellId, allEquipments);
    }

    // Hàm cho Book
    public async Task<bool> EquipAllEquipmentsOfTypeToBookAsync(string userId, string bookId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToBookAsync(userId, bookId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToBookAsync(string userId, string bookId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToBookAsync(userId, bookId, allEquipments);
    }

    // Hàm cho Pet
    public async Task<bool> EquipAllEquipmentsOfTypeToPetAsync(string userId, string petId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToPetAsync(userId, petId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToPetAsync(string userId, string petId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToPetAsync(userId, petId, allEquipments);
    }

    // Hàm cho Card Soldier
    public async Task<bool> EquipAllEquipmentsOfTypeToCardSoldierAsync(string userId, string cardSoldierId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardSoldierAsync(userId, cardSoldierId, type, allEquipments);
    }

    public async Task<bool> EquipAllEquipmentsToCardSoldierAsync(string userId, string cardSoldierId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        return await _userEquipmentsRepository.EquipAllEquipmentsToCardSoldierAsync(userId, cardSoldierId, allEquipments);
    }

    public async Task<bool> InsertOrUpdateUserEquipmentsBatchAsync(string userId, List<(Equipments data, double quantity)> list)
    {
        return await _userEquipmentsRepository.InsertOrUpdateUserEquipmentsBatchAsync(userId, list);
    }
}

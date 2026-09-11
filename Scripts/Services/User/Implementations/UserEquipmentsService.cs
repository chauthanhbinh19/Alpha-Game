using System.Collections.Generic;
using System.Threading.Tasks;

public class UserEquipmentsService : IUserEquipmentsService
{
    private readonly IUserEquipmentsRepository _userEquipmentsRepository;
    private readonly IEquipmentsGalleryService _equipmentsGalleryService;
    private readonly IEquipmentsService _equipmentsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserEquipmentsService(
        IUserEquipmentsRepository userEquipmentsRepository,
        IEquipmentsGalleryService equipmentsGalleryService,
        IEquipmentsService equipmentsService,
        IPowerManagerService powerManagerService)
    {
        _userEquipmentsRepository = userEquipmentsRepository;
        _equipmentsGalleryService = equipmentsGalleryService;
        _equipmentsService = equipmentsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserEquipmentsService Create() => ServiceContainer.GetService<IUserEquipmentsService>();

    public async Task<List<Equipments>> GetAllRankPowerAsync(string userId, List<Equipments> EquipmentsList)
    {
        foreach (var c in EquipmentsList)
        {
            UserRanks rank = await UserEquipmentsRankService.Create().GetSumUserEquipmentsRankAsync(userId, c.Id);
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
        List<Equipments> result = await _userEquipmentsRepository.GetUserEquipmentsAsync(userId, search, type, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<List<Equipments>> GetUserAllEquipmentsAsync(string userId)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetUserAllEquipmentsAsync(userId);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserEquipmentsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userEquipmentsRepository.GetUserEquipmentsCountAsync(userId, search, type, rare);
    }

    public async Task<Equipments> GetUserEquipmentsByIdAsync(string userId, string Id)
    {
        var result = await _userEquipmentsRepository.GetUserEquipmentsByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);

        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEquipmentAsync(string userId, Equipments equipment)
    {
        var checkEquipmentResult = await _equipmentsService.IsEquipmentDeletedOrInactiveAsync(equipment.Id);
        if (checkEquipmentResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userEquipmentsRepository.InsertOrUpdateUserEquipmentAsync(userId, equipment);

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

        await _equipmentsGalleryService.InsertEquipmentGalleryAsync(userId, equipment.Id);


        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEquipmentsBatchAsync(string userId, List<(Equipments data, double quantity)> result)
    {
        var repositoryResult = await _userEquipmentsRepository.InsertOrUpdateUserEquipmentsBatchAsync(userId, result);

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
            await _equipmentsGalleryService.InsertBatchEquipmentsGalleryAsync(userId, newlyInsertedCards);
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

    public async Task<InsertOrUpdateResult<bool>> UpdateUserEquipmentLevelAsync(string userId, Equipments equipment)
    {
        var checkEquipmentResult = await _equipmentsService.IsEquipmentDeletedOrInactiveAsync(equipment.Id);
        if (checkEquipmentResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userEquipmentsRepository.UpdateUserEquipmentLevelAsync(userId, equipment);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserEquipmentStarAsync(string userId, Equipments equipment)
    {
        var checkEquipmentResult = await _equipmentsService.IsEquipmentDeletedOrInactiveAsync(equipment.Id);
        if (checkEquipmentResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userEquipmentsRepository.UpdateUserEquipmentStarAsync(userId, equipment);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        await _equipmentsGalleryService.UpdateTempStarEquipmentGalleryAsync(userId, equipment.Id, equipment.Star);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task UpdateUserCurrencyAsync(string userId, string Id, double quantity)
    {
        await _userEquipmentsRepository.UpdateUserCurrencyAsync(userId, Id, quantity);
    }

    public async Task InsertUserCardHeroEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertUserCardHeroEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertUserCardCaptainEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertUserCardCaptainEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertUserCardColonelEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertUserCardColonelEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertUserCardGeneralEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertUserCardGeneralEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertUserCardAdmiralEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertUserCardAdmiralEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertUserCardMonsterEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertUserCardMonsterEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertUserCardMilitaryEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertUserCardMilitaryEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertUserCardSpellEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertUserCardSpellEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertUserBookEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertUserBookEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertUserPetEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertUserPetEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task InsertUserCardSoldierEquipmentsAsync(string userId, string Id, Equipments equipments, int position)
    {
        await _userEquipmentsRepository.InsertUserCardSoldierEquipmentsAsync(userId, Id, equipments, position);
    }

    public async Task<List<Equipments>> GetUserCardHeroesEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetUserCardHeroesEquipmentsAsync(userId, card_id, type);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetUserCardCaptainsEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetUserCardCaptainsEquipmentsAsync(userId, card_id, type);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetUserCardColonelsEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetUserCardColonelsEquipmentsAsync(userId, card_id, type);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetUserCardGeneralsEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetUserCardGeneralsEquipmentsAsync(userId, card_id, type);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetUserCardAdmiralsEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetUserCardAdmiralsEquipmentsAsync(userId, card_id, type);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetUserCardMonstersEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetUserCardMonstersEquipmentsAsync(userId, card_id, type);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetUserCardMilitariesEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetUserCardMilitariesEquipmentsAsync(userId, card_id, type);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetUserCardSpellsEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetUserCardSpellsEquipmentsAsync(userId, card_id, type);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetUserBooksEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetUserBooksEquipmentsAsync(userId, card_id, type);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetUserPetsEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetUserPetsEquipmentsAsync(userId, card_id, type);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetUserCardSoldiersEquipmentsAsync(string userId, string card_id, string type)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetUserCardSoldiersEquipmentsAsync(userId, card_id, type);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetAllUserCardHeroesEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetAllUserCardHeroesEquipmentsAsync(userId, type, limit, offset, status);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetAllUserCardCaptainsEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetAllUserCardCaptainsEquipmentsAsync(userId, type, limit, offset, status);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetAllUserCardColonelsEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetAllUserCardColonelsEquipmentsAsync(userId, type, limit, offset, status);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetAllUserCardGeneralsEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetAllUserCardGeneralsEquipmentsAsync(userId, type, limit, offset, status);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetAllUserCardAdmiralsEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetAllUserCardAdmiralsEquipmentsAsync(userId, type, limit, offset, status);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetAllUserCardMonstersEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetAllUserCardMonstersEquipmentsAsync(userId, type, limit, offset, status);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetAllUserCardMilitariesEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetAllUserCardMilitariesEquipmentsAsync(userId, type, limit, offset, status);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetAllUserCardSpellsEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetAllUserCardSpellsEquipmentsAsync(userId, type, limit, offset, status);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetAllUserBooksEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetAllUserBooksEquipmentsAsync(userId, type, limit, offset, status);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetAllUserPetsEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetAllUserPetsEquipmentsAsync(userId, type, limit, offset, status);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<List<Equipments>> GetAllUserCardSoldiersEquipmentsAsync(string userId, string type, int limit, int offset, string status)
    {
        List<Equipments> result = await _userEquipmentsRepository.GetAllUserCardSoldiersEquipmentsAsync(userId, type, limit, offset, status);
        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);
        result = ModuleEvaluatorHelper.GetModulePower(result);
        result = UpgradeEvaluatorHelper.GetUpgradePower(result);
        return result;
    }

    public async Task<Equipments> GetAllUserEquipmentsByCardHeorIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllUserEquipmentsByCardHeroIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllUserEquipmentsByCardCaptainIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllUserEquipmentsByCardCaptainIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllUserEquipmentsByCardColonelIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllUserEquipmentsByCardColonelIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllUserEquipmentsByCardGeneralIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllUserEquipmentsByCardGeneralIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllUserEquipmentsByCardAdmiralIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllUserEquipmentsByCardAdmiralIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllUserEquipmentsByCardMonsterIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllUserEquipmentsByCardMonsterIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllUserEquipmentsByCardMilitaryIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllUserEquipmentsByCardMilitaryIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllUserEquipmentsByCardSpellIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllUserEquipmentsByCardSpellIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllUserEquipmentsByBookIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllUserEquipmentsByBookIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllUserEquipmentsByPetIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllUserEquipmentsByPetIdAsync(userId, Id);
    }

    public async Task<Equipments> GetAllUserEquipmentsByCardSoldierIdAsync(string userId, string Id)
    {
        return await _userEquipmentsRepository.GetAllUserEquipmentsByCardSoldierIdAsync(userId, Id);
    }

    // Hàm cho CardHero
    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardHeroAsync(string userId, string cardHeroId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardHeroAsync(userId, cardHeroId, type, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardHeroAsync(string userId, string cardHeroId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsToCardHeroAsync(userId, cardHeroId, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    // Hàm cho CardCaptain
    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardCaptainAsync(string userId, string cardCaptainId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardCaptainAsync(userId, cardCaptainId, type, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardCaptainAsync(string userId, string cardCaptainId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsToCardCaptainAsync(userId, cardCaptainId, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    // Hàm cho CardColonel
    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardColonelAsync(string userId, string cardColonelId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardColonelAsync(userId, cardColonelId, type, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardColonelAsync(string userId, string cardColonelId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsToCardColonelAsync(userId, cardColonelId, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    // Hàm cho CardGeneral
    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardGeneralAsync(string userId, string cardGeneralId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardGeneralAsync(userId, cardGeneralId, type, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardGeneralAsync(string userId, string cardGeneralId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsToCardGeneralAsync(userId, cardGeneralId, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    // Hàm cho CardAdmiral
    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardAdmiralAsync(string userId, string cardAdmiralId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardAdmiralAsync(userId, cardAdmiralId, type, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardAdmiralAsync(string userId, string cardAdmiralId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsToCardAdmiralAsync(userId, cardAdmiralId, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    // Hàm cho CardMonster
    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardMonsterAsync(string userId, string cardMonsterId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardMonsterAsync(userId, cardMonsterId, type, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardMonsterAsync(string userId, string cardMonsterId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsToCardMonsterAsync(userId, cardMonsterId, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    // Hàm cho CardMilitary
    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardMilitaryAsync(string userId, string cardMilitaryId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardMilitaryAsync(userId, cardMilitaryId, type, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardMilitaryAsync(string userId, string cardMilitaryId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsToCardMilitaryAsync(userId, cardMilitaryId, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    // Hàm cho CardSpell
    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardSpellAsync(string userId, string cardSpellId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardSpellAsync(userId, cardSpellId, type, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardSpellAsync(string userId, string cardSpellId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsToCardSpellAsync(userId, cardSpellId, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    // Hàm cho Book
    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToBookAsync(string userId, string bookId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToBookAsync(userId, bookId, type, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToBookAsync(string userId, string bookId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsToBookAsync(userId, bookId, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    // Hàm cho Pet
    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToPetAsync(string userId, string petId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToPetAsync(userId, petId, type, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToPetAsync(string userId, string petId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsToPetAsync(userId, petId, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    // Hàm cho Card Soldier
    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardSoldierAsync(string userId, string cardSoldierId, string type)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsOfTypeToCardSoldierAsync(userId, cardSoldierId, type, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardSoldierAsync(string userId, string cardSoldierId)
    {
        List<Equipments> allEquipments = await GetUserAllEquipmentsAsync(userId);
        await _userEquipmentsRepository.EquipAllEquipmentsToCardSoldierAsync(userId, cardSoldierId, allEquipments);
        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<int> GetUserCardHeroesEquipmentsCountAsync(string userId, string search, string type, string rare, string set)
    {
        return await GetUserCardHeroesEquipmentsCountAsync(userId, search, type, rare, set);
    }

    public async Task<int> GetUserCardCaptainsEquipmentsCountAsync(string userId, string search, string type, string rare, string set)
    {
        return await GetUserCardCaptainsEquipmentsCountAsync(userId, search, type, rare, set);
    }

    public async Task<int> GetUserCardColonelsEquipmentsCountAsync(string userId, string search, string type, string rare, string set)
    {
        return await GetUserCardColonelsEquipmentsCountAsync(userId, search, type, rare, set);
    }

    public async Task<int> GetUserCardGeneralsEquipmentsCountAsync(string userId, string search, string type, string rare, string set)
    {
        return await GetUserCardGeneralsEquipmentsCountAsync(userId, search, type, rare, set);
    }

    public async Task<int> GetUserCardAdmiralsEquipmentsCountAsync(string userId, string search, string type, string rare, string set)
    {
        return await GetUserCardAdmiralsEquipmentsCountAsync(userId, search, type, rare, set);
    }

    public async Task<int> GetUserCardMonstersEquipmentsCountAsync(string userId, string search, string type, string rare, string set)
    {
        return await GetUserCardMonstersEquipmentsCountAsync(userId, search, type, rare, set);
    }

    public async Task<int> GetUserCardMilitariesEquipmentsCountAsync(string userId, string search, string type, string rare, string set)
    {
        return await GetUserCardMilitariesEquipmentsCountAsync(userId, search, type, rare, set);
    }

    public async Task<int> GetUserCardSoldiersEquipmentsCountAsync(string userId, string search, string type, string rare, string set)
    {
        return await GetUserCardSoldiersEquipmentsCountAsync(userId, search, type, rare, set);
    }

    public async Task<int> GetUserCardSpellsEquipmentsCountAsync(string userId, string search, string type, string rare, string set)
    {
        return await GetUserCardSpellsEquipmentsCountAsync(userId, search, type, rare, set);
    }

    public async Task<int> GetUserBooksEquipmentsCountAsync(string userId, string search, string type, string rare, string set)
    {
        return await GetUserBooksEquipmentsCountAsync(userId, search, type, rare, set);
    }

    public async Task<int> GetUserPetsEquipmentsCountAsync(string userId, string search, string type, string rare, string set)
    {
        return await GetUserPetsEquipmentsCountAsync(userId, search, type, rare, set);
    }
}

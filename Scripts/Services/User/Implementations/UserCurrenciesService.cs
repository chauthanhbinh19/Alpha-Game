using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCurrenciesService : IUserCurrenciesService
{
     private static UserCurrenciesService _instance;
    private readonly IUserCurrenciesRepository _userCurrenciesRepository;

    public UserCurrenciesService(IUserCurrenciesRepository userCurrenciesRepository)
    {
        _userCurrenciesRepository = userCurrenciesRepository;
    }

    public static UserCurrenciesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCurrenciesService(new UserCurrenciesRepository());
        }
        return _instance;
    }

    public async Task<List<Currencies>> GetUserCurrencyAsync(string userId)
    {
        return await _userCurrenciesRepository.GetUserCurrencyAsync(userId);
    }

    public async Task<Currencies> GetUserCurrencyByIdAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCurrencyByIdAsync(Id);
    }

    public async Task<Currencies> GetUserCurrencyByNameAsync(string currencyName)
    {
        return await _userCurrenciesRepository.GetUserCurrencyByNameAsync(currencyName);
    }

    public async Task InitiateUserCurrencyAsync(string userId)
    {
        await _userCurrenciesRepository.InitiateUserCurrencyAsync(userId);
    }

    public async Task UpdateUserCurrencyAsync(string currency_id, double price)
    {
        await _userCurrenciesRepository.UpdateUserCurrencyAsync(currency_id, price);
    }

    public async Task<List<Currencies>> GetEquipmentsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetEquipmentsCurrencyAsync(type);
    }

    public async Task<Currencies> GetEquipmentsPriceAsync(string type, string equipment_id)
    {
        return await _userCurrenciesRepository.GetEquipmentsPriceAsync(type, equipment_id);
    }

    public async Task<Currencies> GetUserEquipmentsPriceAsync(string type, string equipment_id)
    {
        return await _userCurrenciesRepository.GetUserEquipmentPriceAsync(type, equipment_id);
    }

    public async Task<Currencies> GetUserCardHeroPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCardHeroPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardCaptainPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCardCaptainPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardColonelPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCardColonelPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardGeneralPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCardGeneralPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardAdmiralPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCardAdmiralPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardMonsterPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCardMonsterPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardMilitaryPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCardMilitaryPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardSpellPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCardSpellPriceAsync(Id);
    }

    public async Task<Currencies> GetUserBookPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserBookPriceAsync(Id);
    }

    public async Task<Currencies> GetUserAchievementPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserAchievementPriceAsync(Id);
    }

    public async Task<Currencies> GetUserBorderPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserBorderPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCollaborationPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCollaborationPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCollaborationEquipmentPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCollaborationEquipmentPriceAsync(Id);
    }

    public async Task<Currencies> GetUserItemPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserItemPriceAsync(Id);
    }

    public async Task<Currencies> GetUserMagicFormationCirclePriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserMagicFormationCirclePriceAsync(Id);
    }

    public async Task<Currencies> GetUserMedalPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserMedalPriceAsync(Id);
    }

    public async Task<Currencies> GetUserPetPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserPetPriceAsync(Id);
    }

    public async Task<Currencies> GetUserRelicPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserRelicPriceAsync(Id);
    }

    public async Task<Currencies> GetUserSkillPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserSkillPriceAsync(Id);
    }

    public async Task<Currencies> GetUserSymbolPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserSymbolPriceAsync(Id);
    }

    public async Task<Currencies> GetUserTitlePriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserTitlePriceAsync(Id);
    }

    public async Task<Currencies> GetUserTalismanPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserTalismanPriceAsync(Id);
    }

    public async Task<Currencies> GetUserPuppetPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserPuppetPriceAsync(Id);
    }

    public async Task<Currencies> GetUserAlchemyPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserAlchemyPriceAsync(Id);
    }

    public async Task<Currencies> GetUserForgePriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserForgePriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardLifePriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCardLifePriceAsync(Id);
    }

    public async Task<Currencies> GetUserArtworkPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserArtworkPriceAsync(Id);
    }

    public async Task<Currencies> GetUserSpiritBeastPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserSpiritBeastPriceAsync(Id);
    }

    public async Task<Currencies> GetUserSpiritCardPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserSpiritCardPriceAsync(Id);
    }

    public async Task<Currencies> GetUserArtifactPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserArtifactPriceAsync(Id);
    }

    public async Task<Currencies> GetUserArchitecturePriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserArchitecturePriceAsync(Id);
    }

    public async Task<Currencies> GetUserTechnologyPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserTechnologyPriceAsync(Id);
    }

    public async Task<Currencies> GetUserVehiclePriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserVehiclePriceAsync(Id);
    }

    public async Task<Currencies> GetUserCorePriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCorePriceAsync(Id);
    }

    public async Task<Currencies> GetUserWeaponPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserWeaponPriceAsync(Id);
    }

    public async Task<Currencies> GetUserRobotPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserRobotPriceAsync(Id);
    }

    public async Task<Currencies> GetUserBadgePriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserBadgePriceAsync(Id);
    }

    public async Task<Currencies> GetUserMechaBeastPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserMechaBeastPriceAsync(Id);
    }

    public async Task<Currencies> GetUserRunePriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserRunePriceAsync(Id);
    }

    public async Task<Currencies> GetUserFurniturePriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserFurniturePriceAsync(Id);
    }

    public async Task<Currencies> GetUserFoodPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserFoodPriceAsync(Id);
    }

    public async Task<Currencies> GetUserBeveragePriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserBeveragePriceAsync(Id);
    }

    public async Task<Currencies> GetUserBuildingPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserBuildingPriceAsync(Id);
    }

    public async Task<Currencies> GetUserPlantPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserPlantPriceAsync(Id);
    }

    public async Task<Currencies> GetUserFashionPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserFashionPriceAsync(Id);
    }

    public async Task<Currencies> GetUserEmojiPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserEmojiPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardSoldierPriceAsync(string Id)
    {
        return await _userCurrenciesRepository.GetUserCardSoldierPriceAsync(Id);
    }

    public async Task<List<Currencies>> GetAchievementsCurrencyAsync()
    {
        return await _userCurrenciesRepository.GetAchievementsCurrencyAsync();
    }

    public async Task<List<Currencies>> GetBooksCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetBooksCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardHeroesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCardHeroesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardCaptainsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCardCaptainsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardColonelsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCardColonelsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardGeneralsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCardGeneralsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardAdmiralsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCardAdmiralsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardMonstersCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCardMonstersCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardMilitariesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCardMilitariesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardSpellsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCardSpellsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCollaborationsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCollaborationsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCollaborationEquipmentsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCollaborationEquipmentsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetBordersCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetBordersCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetItemsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetItemsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetMagicFormationCirclesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetMagicFormationCirclesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetMedalsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetMedalsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetPetsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetPetsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetRelicsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetRelicsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetSkillsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetSkillsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetSymbolsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetSymbolsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetTitlesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetTitlesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetTalismansCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetTalismansCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetPuppetsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetPuppetsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetAlchemiesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetAlchemiesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetForgesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetForgesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardLivesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCardLivesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetArtworksCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetArtworksCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetSpiritBeastsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetSpiritBeastsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetSpiritCardsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetSpiritCardsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetArtifactsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetArtifactsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetArchitecturesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetArchitecturesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetTechnologiesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetTechnologiesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetVehiclesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetVehiclesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCoresCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCoresCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetWeaponsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetWeaponsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetRobotsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetRobotsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetBadgesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetBadgesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetMechaBeastsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetMechaBeastsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetRunesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetRunesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetFurnituresCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetFurnituresCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetFoodsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetFoodsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetBeveragesCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetBeveragesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetBuildingsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetBuildingsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetPlantsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetPlantsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetFashionsCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetFashionsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetEmojisCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetEmojisCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardSoldiersCurrencyAsync(string type)
    {
        return await _userCurrenciesRepository.GetCardSoldiersCurrencyAsync(type);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCurrenciesService : IUserCurrenciesService
{
    private readonly IUserCurrenciesRepository _userCurrencyRepository;

    public UserCurrenciesService(IUserCurrenciesRepository userCurrencyRepository)
    {
        _userCurrencyRepository = userCurrencyRepository;
    }

    public static UserCurrenciesService Create()
    {
        return new UserCurrenciesService(new UserCurrenciesRepository());
    }

    public async Task<List<Currencies>> GetUserCurrencyAsync(string userId)
    {
        return await _userCurrencyRepository.GetUserCurrencyAsync(userId);
    }

    public async Task<Currencies> GetUserCurrencyByIdAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCurrencyByIdAsync(Id);
    }

    public async Task<Currencies> GetUserCurrencyByNameAsync(string currencyName)
    {
        return await _userCurrencyRepository.GetUserCurrencyByNameAsync(currencyName);
    }

    public async Task UpdateUserCurrencyAsync(string currency_id, double price)
    {
        await _userCurrencyRepository.UpdateUserCurrencyAsync(currency_id, price);
    }

    public async Task<List<Currencies>> GetEquipmentsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetEquipmentsCurrencyAsync(type);
    }

    public async Task<Currencies> GetEquipmentsPriceAsync(string type, string equipment_id)
    {
        return await _userCurrencyRepository.GetEquipmentsPriceAsync(type, equipment_id);
    }

    public async Task<Currencies> GetUserEquipmentsPriceAsync(string type, string equipment_id)
    {
        return await _userCurrencyRepository.GetUserEquipmentPriceAsync(type, equipment_id);
    }

    public async Task<Currencies> GetUserCardHeroPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCardHeroPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardCaptainPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCardCaptainPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardColonelPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCardColonelPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardGeneralPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCardGeneralPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardAdmiralPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCardAdmiralPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardMonsterPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCardMonsterPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardMilitaryPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCardMilitaryPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardSpellPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCardSpellPriceAsync(Id);
    }

    public async Task<Currencies> GetUserBookPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserBookPriceAsync(Id);
    }

    public async Task<Currencies> GetUserAchievementPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserAchievementPriceAsync(Id);
    }

    public async Task<Currencies> GetUserBorderPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserBorderPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCollaborationPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCollaborationPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCollaborationEquipmentPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCollaborationEquipmentPriceAsync(Id);
    }

    public async Task<Currencies> GetUserItemPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserItemPriceAsync(Id);
    }

    public async Task<Currencies> GetUserMagicFormationCirclePriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserMagicFormationCirclePriceAsync(Id);
    }

    public async Task<Currencies> GetUserMedalPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserMedalPriceAsync(Id);
    }

    public async Task<Currencies> GetUserPetPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserPetPriceAsync(Id);
    }

    public async Task<Currencies> GetUserRelicPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserRelicPriceAsync(Id);
    }

    public async Task<Currencies> GetUserSkillPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserSkillPriceAsync(Id);
    }

    public async Task<Currencies> GetUserSymbolPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserSymbolPriceAsync(Id);
    }

    public async Task<Currencies> GetUserTitlePriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserTitlePriceAsync(Id);
    }

    public async Task<Currencies> GetUserTalismanPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserTalismanPriceAsync(Id);
    }

    public async Task<Currencies> GetUserPuppetPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserPuppetPriceAsync(Id);
    }

    public async Task<Currencies> GetUserAlchemyPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserAlchemyPriceAsync(Id);
    }

    public async Task<Currencies> GetUserForgePriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserForgePriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardLifePriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCardLifePriceAsync(Id);
    }

    public async Task<Currencies> GetUserArtworkPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserArtworkPriceAsync(Id);
    }

    public async Task<Currencies> GetUserSpiritBeastPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserSpiritBeastPriceAsync(Id);
    }

    public async Task<Currencies> GetUserSpiritCardPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserSpiritCardPriceAsync(Id);
    }

    public async Task<Currencies> GetUserCardPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCardPriceAsync(Id);
    }

    public async Task<Currencies> GetUserArchitecturePriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserArchitecturePriceAsync(Id);
    }

    public async Task<Currencies> GetUserTechnologyPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserTechnologyPriceAsync(Id);
    }

    public async Task<Currencies> GetUserVehiclePriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserVehiclePriceAsync(Id);
    }

    public async Task<Currencies> GetUserCorePriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserCorePriceAsync(Id);
    }

    public async Task<Currencies> GetUserWeaponPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserWeaponPriceAsync(Id);
    }

    public async Task<Currencies> GetUserRobotPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserRobotPriceAsync(Id);
    }

    public async Task<Currencies> GetUserBadgePriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserBadgePriceAsync(Id);
    }

    public async Task<Currencies> GetUserMechaBeastPriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserMechaBeastPriceAsync(Id);
    }

    public async Task<Currencies> GetUserRunePriceAsync(string Id)
    {
        return await _userCurrencyRepository.GetUserRunePriceAsync(Id);
    }

    public async Task<List<Currencies>> GetAchievementsCurrencyAsync()
    {
        return await _userCurrencyRepository.GetAchievementsCurrencyAsync();
    }

    public async Task<List<Currencies>> GetBooksCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetBooksCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardHeroesCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCardHeroesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardCaptainsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCardCaptainsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardColonelsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCardColonelsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardGeneralsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCardGeneralsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardAdmiralsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCardAdmiralsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardMonstersCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCardMonstersCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardMilitariesCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCardMilitariesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardSpellsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCardSpellsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCollaborationsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCollaborationsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCollaborationEquipmentsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCollaborationEquipmentsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetBordersCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetBordersCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetItemsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetItemsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetMagicFormationCirclesCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetMagicFormationCirclesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetMedalsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetMedalsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetPetsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetPetsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetRelicsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetRelicsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetSkillsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetSkillsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetSymbolsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetSymbolsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetTitlesCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetTitlesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetTalismansCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetTalismansCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetPuppetsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetPuppetsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetAlchemiesCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetAlchemiesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetForgesCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetForgesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardLivesCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCardLivesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetArtworksCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetArtworksCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetSpiritBeastsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetSpiritBeastsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetSpiritCardsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetSpiritCardsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCardsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCardsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetArchitecturesCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetArchitecturesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetTechnologiesCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetTechnologiesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetVehiclesCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetVehiclesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetCoresCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetCoresCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetWeaponsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetWeaponsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetRobotsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetRobotsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetBadgesCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetBadgesCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetMechaBeastsCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetMechaBeastsCurrencyAsync(type);
    }

    public async Task<List<Currencies>> GetRunesCurrencyAsync(string type)
    {
        return await _userCurrencyRepository.GetRunesCurrencyAsync(type);
    }
}

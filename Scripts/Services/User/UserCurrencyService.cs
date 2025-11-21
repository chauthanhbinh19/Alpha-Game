using System.Collections.Generic;

public class UserCurrencyService : IUserCurrencyService
{
    private readonly IUserCurrencyRepository _userCurrencyRepository;

    public UserCurrencyService(IUserCurrencyRepository userCurrencyRepository)
    {
        _userCurrencyRepository = userCurrencyRepository;
    }

    public static UserCurrencyService Create()
    {
        return new UserCurrencyService(new UserCurrencyRepository());
    }

    public List<Currencies> GetUserCurrency(string userId)
    {
        return _userCurrencyRepository.GetUserCurrency(userId);
    }

    public Currencies GetUserCurrencyById(string Id)
    {
        return _userCurrencyRepository.GetUserCurrencyById(Id);
    }

    public Currencies GetUserCurrencyByName(string currencyName)
    {
        return _userCurrencyRepository.GetUserCurrencyByName(currencyName);
    }

    public void UpdateUserCurrency(string currency_id, double price)
    {
        _userCurrencyRepository.UpdateUserCurrency(currency_id, price);
    }

    public List<Currencies> GetEquipmentsCurrency(string type)
    {
        return _userCurrencyRepository.GetEquipmentsCurrency(type);
    }

    public Currencies GetEquipmentsPrice(string type, string equipment_id)
    {
        return _userCurrencyRepository.GetEquipmentsPrice(type, equipment_id);
    }

    public Currencies GetUserEquipmentsPrice(string type, string equipment_id)
    {
        return _userCurrencyRepository.GetUserEquipmentsPrice(type, equipment_id);
    }

    public Currencies GetUserCardHeroesPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardHeroesPrice(Id);
    }

    public Currencies GetUserCardCaptainsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardCaptainsPrice(Id);
    }

    public Currencies GetUserCardColonelsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardColonelsPrice(Id);
    }

    public Currencies GetUserCardGeneralsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardGeneralsPrice(Id);
    }

    public Currencies GetUserCardAdmiralsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardAdmiralsPrice(Id);
    }

    public Currencies GetUserCardMonstersPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardMonstersPrice(Id);
    }

    public Currencies GetUserCardMilitaryPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardMilitaryPrice(Id);
    }

    public Currencies GetUserCardSpellPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardSpellPrice(Id);
    }

    public Currencies GetUserBooksPrice(string Id)
    {
        return _userCurrencyRepository.GetUserBooksPrice(Id);
    }

    public Currencies GetUserAchievementsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserAchievementsPrice(Id);
    }

    public Currencies GetUserBordersPrice(string Id)
    {
        return _userCurrencyRepository.GetUserBordersPrice(Id);
    }

    public Currencies GetUserCollaborationsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCollaborationsPrice(Id);
    }

    public Currencies GetUserCollaborationEquipmentsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCollaborationEquipmentsPrice(Id);
    }

    public Currencies GetUserItemsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserItemsPrice(Id);
    }

    public Currencies GetUserMagicFormationCirclePrice(string Id)
    {
        return _userCurrencyRepository.GetUserMagicFormationCirclePrice(Id);
    }

    public Currencies GetUserMedalsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserMedalsPrice(Id);
    }

    public Currencies GetUserPetsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserPetsPrice(Id);
    }

    public Currencies GetUserRelicsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserRelicsPrice(Id);
    }

    public Currencies GetUserSkillsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserSkillsPrice(Id);
    }

    public Currencies GetUserSymbolsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserSymbolsPrice(Id);
    }

    public Currencies GetUserTitlesPrice(string Id)
    {
        return _userCurrencyRepository.GetUserTitlesPrice(Id);
    }

    public Currencies GetUserTalismanPrice(string Id)
    {
        return _userCurrencyRepository.GetUserTalismanPrice(Id);
    }

    public Currencies GetUserPuppetPrice(string Id)
    {
        return _userCurrencyRepository.GetUserPuppetPrice(Id);
    }

    public Currencies GetUserAlchemyPrice(string Id)
    {
        return _userCurrencyRepository.GetUserAlchemyPrice(Id);
    }

    public Currencies GetUserForgePrice(string Id)
    {
        return _userCurrencyRepository.GetUserForgePrice(Id);
    }

    public Currencies GetUserCardLifePrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardLifePrice(Id);
    }

    public Currencies GetUserArtworkPrice(string Id)
    {
        return _userCurrencyRepository.GetUserArtworkPrice(Id);
    }

    public Currencies GetUserSpiritBeastPrice(string Id)
    {
        return _userCurrencyRepository.GetUserSpiritBeastPrice(Id);
    }

    public Currencies GetUserSpiritCardPrice(string Id)
    {
        return _userCurrencyRepository.GetUserSpiritCardPrice(Id);
    }

    public Currencies GetUserCardsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardsPrice(Id);
    }

    public Currencies GetUserArchitecturesPrice(string Id)
    {
        return _userCurrencyRepository.GetUserArchitecturesPrice(Id);
    }

    public Currencies GetUserTechnologiesPrice(string Id)
    {
        return _userCurrencyRepository.GetUserTechnologiesPrice(Id);
    }

    public Currencies GetUserVehiclesPrice(string Id)
    {
        return _userCurrencyRepository.GetUserVehiclesPrice(Id);
    }

    public Currencies GetUserCoresPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCoresPrice(Id);
    }

    public Currencies GetUserWeaponsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserWeaponsPrice(Id);
    }

    public Currencies GetUserRobotsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserRobotsPrice(Id);
    }

    public List<Currencies> GetAchievementsCurrency()
    {
        return _userCurrencyRepository.GetAchievementsCurrency();
    }

    public List<Currencies> GetBooksCurrency(string type)
    {
        return _userCurrencyRepository.GetBooksCurrency(type);
    }

    public List<Currencies> GetCardHeroesCurrency(string type)
    {
        return _userCurrencyRepository.GetCardHeroesCurrency(type);
    }

    public List<Currencies> GetCardCaptainsCurrency(string type)
    {
        return _userCurrencyRepository.GetCardCaptainsCurrency(type);
    }

    public List<Currencies> GetCardColonelsCurrency(string type)
    {
        return _userCurrencyRepository.GetCardColonelsCurrency(type);
    }

    public List<Currencies> GetCardGeneralsCurrency(string type)
    {
        return _userCurrencyRepository.GetCardGeneralsCurrency(type);
    }

    public List<Currencies> GetCardAdmiralsCurrency(string type)
    {
        return _userCurrencyRepository.GetCardAdmiralsCurrency(type);
    }

    public List<Currencies> GetCardMonstersCurrency(string type)
    {
        return _userCurrencyRepository.GetCardMonstersCurrency(type);
    }

    public List<Currencies> GetCardMilitaryCurrency(string type)
    {
        return _userCurrencyRepository.GetCardMilitaryCurrency(type);
    }

    public List<Currencies> GetCardSpellCurrency(string type)
    {
        return _userCurrencyRepository.GetCardSpellCurrency(type);
    }

    public List<Currencies> GetCollaborationsCurrency(string type)
    {
        return _userCurrencyRepository.GetCollaborationsCurrency(type);
    }

    public List<Currencies> GetCollaborationEquipmentsCurrency(string type)
    {
        return _userCurrencyRepository.GetCollaborationEquipmentsCurrency(type);
    }

    public List<Currencies> GetBordersCurrency(string type)
    {
        return _userCurrencyRepository.GetBordersCurrency(type);
    }

    public List<Currencies> GetItemsCurrency(string type)
    {
        return _userCurrencyRepository.GetItemsCurrency(type);
    }

    public List<Currencies> GetMagicFormationCircleCurrency(string type)
    {
        return _userCurrencyRepository.GetMagicFormationCircleCurrency(type);
    }

    public List<Currencies> GetMedalsCurrency(string type)
    {
        return _userCurrencyRepository.GetMedalsCurrency(type);
    }

    public List<Currencies> GetPetsCurrency(string type)
    {
        return _userCurrencyRepository.GetPetsCurrency(type);
    }

    public List<Currencies> GetRelicsCurrency(string type)
    {
        return _userCurrencyRepository.GetRelicsCurrency(type);
    }

    public List<Currencies> GetSkillsCurrency(string type)
    {
        return _userCurrencyRepository.GetSkillsCurrency(type);
    }

    public List<Currencies> GetSymbolsCurrency(string type)
    {
        return _userCurrencyRepository.GetSymbolsCurrency(type);
    }

    public List<Currencies> GetTitlesCurrency(string type)
    {
        return _userCurrencyRepository.GetTitlesCurrency(type);
    }

    public List<Currencies> GetTalismanCurrency(string type)
    {
        return _userCurrencyRepository.GetTalismanCurrency(type);
    }

    public List<Currencies> GetPuppetCurrency(string type)
    {
        return _userCurrencyRepository.GetPuppetCurrency(type);
    }

    public List<Currencies> GetAlchemyCurrency(string type)
    {
        return _userCurrencyRepository.GetAlchemyCurrency(type);
    }

    public List<Currencies> GetForgeCurrency(string type)
    {
        return _userCurrencyRepository.GetForgeCurrency(type);
    }

    public List<Currencies> GetCardLifeCurrency(string type)
    {
        return _userCurrencyRepository.GetCardLifeCurrency(type);
    }

    public List<Currencies> GetArtworkCurrency(string type)
    {
        return _userCurrencyRepository.GetArtworkCurrency(type);
    }

    public List<Currencies> GetSpiritBeastCurrency(string type)
    {
        return _userCurrencyRepository.GetSpiritBeastCurrency(type);
    }

    public List<Currencies> GetSpiritCardCurrency(string type)
    {
        return _userCurrencyRepository.GetSpiritCardCurrency(type);
    }

    public List<Currencies> GetCardsCurrency(string type)
    {
        return _userCurrencyRepository.GetCardsCurrency(type);
    }

    public List<Currencies> GetArchitecturesCurrency(string type)
    {
        return _userCurrencyRepository.GetArchitecturesCurrency(type);
    }

    public List<Currencies> GetTechnologiesCurrency(string type)
    {
        return _userCurrencyRepository.GetTechnologiesCurrency(type);
    }

    public List<Currencies> GetVehiclesCurrency(string type)
    {
        return _userCurrencyRepository.GetVehiclesCurrency(type);
    }

    public List<Currencies> GetCoresCurrency(string type)
    {
        return _userCurrencyRepository.GetCoresCurrency(type);
    }

    public List<Currencies> GetWeaponsCurrency(string type)
    {
        return _userCurrencyRepository.GetWeaponsCurrency(type);
    }

    public List<Currencies> GetRobotsCurrency(string type)
    {
        return _userCurrencyRepository.GetRobotsCurrency(type);
    }
}

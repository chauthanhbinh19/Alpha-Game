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

    public List<Currency> GetUserCurrency()
    {
        return _userCurrencyRepository.GetUserCurrency();
    }

    public Currency GetUserCurrencyById(string Id)
    {
        return _userCurrencyRepository.GetUserCurrencyById(Id);
    }

    public void UpdateUserCurrency(string currency_id, double price)
    {
        _userCurrencyRepository.UpdateUserCurrency(currency_id, price);
    }

    public List<Currency> GetEquipmentsCurrency(string type)
    {
        return _userCurrencyRepository.GetEquipmentsCurrency(type);
    }

    public Currency GetEquipmentsPrice(string type, string equipment_id)
    {
        return _userCurrencyRepository.GetEquipmentsPrice(type, equipment_id);
    }

    public Currency GetUserEquipmentsPrice(string type, string equipment_id)
    {
        return _userCurrencyRepository.GetUserEquipmentsPrice(type, equipment_id);
    }

    public Currency GetUserCardHeroesPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardHeroesPrice(Id);
    }

    public Currency GetUserCardCaptainsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardCaptainsPrice(Id);
    }

    public Currency GetUserCardColonelsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardColonelsPrice(Id);
    }

    public Currency GetUserCardGeneralsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardGeneralsPrice(Id);
    }

    public Currency GetUserCardAdmiralsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardAdmiralsPrice(Id);
    }

    public Currency GetUserCardMonstersPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardMonstersPrice(Id);
    }

    public Currency GetUserCardMilitaryPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardMilitaryPrice(Id);
    }

    public Currency GetUserCardSpellPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardSpellPrice(Id);
    }

    public Currency GetUserBooksPrice(string Id)
    {
        return _userCurrencyRepository.GetUserBooksPrice(Id);
    }

    public Currency GetUserAchievementsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserAchievementsPrice(Id);
    }

    public Currency GetUserBordersPrice(string Id)
    {
        return _userCurrencyRepository.GetUserBordersPrice(Id);
    }

    public Currency GetUserCollaborationsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCollaborationsPrice(Id);
    }

    public Currency GetUserCollaborationEquipmentsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserCollaborationEquipmentsPrice(Id);
    }

    public Currency GetUserItemsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserItemsPrice(Id);
    }

    public Currency GetUserMagicFormationCirclePrice(string Id)
    {
        return _userCurrencyRepository.GetUserMagicFormationCirclePrice(Id);
    }

    public Currency GetUserMedalsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserMedalsPrice(Id);
    }

    public Currency GetUserPetsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserPetsPrice(Id);
    }

    public Currency GetUserRelicsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserRelicsPrice(Id);
    }

    public Currency GetUserSkillsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserSkillsPrice(Id);
    }

    public Currency GetUserSymbolsPrice(string Id)
    {
        return _userCurrencyRepository.GetUserSymbolsPrice(Id);
    }

    public Currency GetUserTitlesPrice(string Id)
    {
        return _userCurrencyRepository.GetUserTitlesPrice(Id);
    }

    public Currency GetUserTalismanPrice(string Id)
    {
        return _userCurrencyRepository.GetUserTalismanPrice(Id);
    }

    public Currency GetUserPuppetPrice(string Id)
    {
        return _userCurrencyRepository.GetUserPuppetPrice(Id);
    }

    public Currency GetUserAlchemyPrice(string Id)
    {
        return _userCurrencyRepository.GetUserAlchemyPrice(Id);
    }

    public Currency GetUserForgePrice(string Id)
    {
        return _userCurrencyRepository.GetUserForgePrice(Id);
    }

    public Currency GetUserCardLifePrice(string Id)
    {
        return _userCurrencyRepository.GetUserCardLifePrice(Id);
    }

    public List<Currency> GetAchievementsCurrency()
    {
        return _userCurrencyRepository.GetAchievementsCurrency();
    }

    public List<Currency> GetBooksCurrency(string type)
    {
        return _userCurrencyRepository.GetBooksCurrency(type);
    }

    public List<Currency> GetCardHeroesCurrency(string type)
    {
        return _userCurrencyRepository.GetCardHeroesCurrency(type);
    }

    public List<Currency> GetCardCaptainsCurrency(string type)
    {
        return _userCurrencyRepository.GetCardCaptainsCurrency(type);
    }

    public List<Currency> GetCardColonelsCurrency(string type)
    {
        return _userCurrencyRepository.GetCardColonelsCurrency(type);
    }

    public List<Currency> GetCardGeneralsCurrency(string type)
    {
        return _userCurrencyRepository.GetCardGeneralsCurrency(type);
    }

    public List<Currency> GetCardAdmiralsCurrency(string type)
    {
        return _userCurrencyRepository.GetCardAdmiralsCurrency(type);
    }

    public List<Currency> GetCardMonstersCurrency(string type)
    {
        return _userCurrencyRepository.GetCardMonstersCurrency(type);
    }

    public List<Currency> GetCardMilitaryCurrency(string type)
    {
        return _userCurrencyRepository.GetCardMilitaryCurrency(type);
    }

    public List<Currency> GetCardSpellCurrency(string type)
    {
        return _userCurrencyRepository.GetCardSpellCurrency(type);
    }

    public List<Currency> GetCollaborationsCurrency(string type)
    {
        return _userCurrencyRepository.GetCollaborationsCurrency(type);
    }

    public List<Currency> GetCollaborationEquipmentsCurrency(string type)
    {
        return _userCurrencyRepository.GetCollaborationEquipmentsCurrency(type);
    }

    public List<Currency> GetBordersCurrency(string type)
    {
        return _userCurrencyRepository.GetBordersCurrency(type);
    }

    public List<Currency> GetItemsCurrency(string type)
    {
        return _userCurrencyRepository.GetItemsCurrency(type);
    }

    public List<Currency> GetMagicFormationCircleCurrency(string type)
    {
        return _userCurrencyRepository.GetMagicFormationCircleCurrency(type);
    }

    public List<Currency> GetMedalsCurrency(string type)
    {
        return _userCurrencyRepository.GetMedalsCurrency(type);
    }

    public List<Currency> GetPetsCurrency(string type)
    {
        return _userCurrencyRepository.GetPetsCurrency(type);
    }

    public List<Currency> GetRelicsCurrency(string type)
    {
        return _userCurrencyRepository.GetRelicsCurrency(type);
    }

    public List<Currency> GetSkillsCurrency(string type)
    {
        return _userCurrencyRepository.GetSkillsCurrency(type);
    }

    public List<Currency> GetSymbolsCurrency(string type)
    {
        return _userCurrencyRepository.GetSymbolsCurrency(type);
    }

    public List<Currency> GetTitlesCurrency(string type)
    {
        return _userCurrencyRepository.GetTitlesCurrency(type);
    }

    public List<Currency> GetTalismanCurrency(string type)
    {
        return _userCurrencyRepository.GetTalismanCurrency(type);
    }

    public List<Currency> GetPuppetCurrency(string type)
    {
        return _userCurrencyRepository.GetPuppetCurrency(type);
    }

    public List<Currency> GetAlchemyCurrency(string type)
    {
        return _userCurrencyRepository.GetAlchemyCurrency(type);
    }

    public List<Currency> GetForgeCurrency(string type)
    {
        return _userCurrencyRepository.GetForgeCurrency(type);
    }

    public List<Currency> GetCardLifeCurrency(string type)
    {
        return _userCurrencyRepository.GetCardLifeCurrency(type);
    }
}

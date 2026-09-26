using UnityEngine;

public static class MessageConstants
{

    public const string USERNAME_DOES_NOT_EXIST = "Username does not exist.";
    public const string INCORRECT_PASSWORD = "Incorrect password.";
    public const string RECIPE_NOT_FOUND = "Recipe not found for this level.";
    public const string UPGRADE_SUCCESS_ONE = "Successfully upgraded 1 level.";
    public const string UPGRADE_SUCCESS_MULTIPLE = "Successfully upgraded {0} levels.";
    public const string SYSTEM_ERROR = "System error: {0}.";
    public const string NOT_ENOUGH_ITEM = "Not enough {0}.";
    public const string ITEM_NOT_FOUND = "Item not found!";
    public const string ITEM_NOT_ENOUGH = "Not enough item quantity!";
    public const string ITEM_ALREADY_EXISTS = "Item already exists!";
    public const string ITEM_ADDED = "Item added successfully!";
    public const string ITEM_REMOVED = "Item removed successfully!";
    public const string UPGRADE_PREVIEW_SUCCESS = "Upgrade preview success";
    public const string IMAGE_IS_NULL = "Your image not found in Resources!";
    public const string PREFAB_IS_NULL = "Your prefab is null! Check if the prefab is correctly loaded.";
    public const string USERNAME_NOT_EXIST = "Notification.UsernameNotExist";
    public const string USERNAME_ALREADY_EXIST = "Notification.UsernameAlreadyExist";
    public const string EMAIL_ALREADY_EXIST = "Notification.EmailAlreadyExist";
    public const string INVALID_EMAIL_FORMAT = "Notification.InvalidEmailFormat";
    public const string USERNAME_IS_EMPTY = "Notification.UsernameCanNotBeEmpty";
    public const string USERNAME_LENGTH_INVALID = "Notification.UsernameLengthInvalid";
    public const string EMAIL_IS_EMPTY = "Notification.EmailCanNotBeEmpty";
    public const string PASSWORD_IS_EMPTY = "Notification.PasswordCanNotBeEmpty";
    public const string PASSWORD_LENGTH_INVALID = "Notification.PasswordLengthInvalid";
    public const string CONFIRM_PASSWORD_IS_EMPTY = "Notification.ConfirmPasswordCanNotBeEmpty";
    public const string PASSWORDS_DO_NOT_MATCH = "Notification.PasswordsDoNotMatch";
    public const string INSERTED_SUCCESSFULLY = "Notification.InsertedSuccessfully";
    public const string UPDATED_SUCCESSFULLY = "Notification.UpdatedSuccessfully";
    public const string MIXED_SUCCESSFULLY = "Notification.MixedSuccessfully";
    public const string FAILED_TO_EXECUTE_ACTION = "Notification.FailedToExecuteAction";
    public const string THIS_RECORD_ALREADY_EXISTS_IN_GALLERY = "Notification.ThisRecordAlreadyExistsInGallery";
    public const string NOTHING_WAS_INSERTED = "Notification.NothingWasInserted";
    public const string NOTHING_WAS_UPDATED = "Notification.NothingWasUpdated";
    public const string POWER_UNCHANGED_NO_UPDATE_NEEDED = "Notification.PowerUnchangedNoUpdateNeeded";
    public const string THE_DATA_WAS_DELETED_OR_INACTIVE = "Notification.TheDataWasDeletedOrInactive";
    public const string INSERT_ITEM_INTO_INVENTORY = "Notification.InsertItemIntoInventory";
    public const string UPDATE_ITEM_QUANTITY_IN_INVENTORY = "Notification.UpdateItemQuantityInInventory";
    public const string INVALID_USER_ID = "Notification.InvalidUserId";
    public const string TRANSACTION_PROCESSING = "Notification.TransactionProcessing";
    public const string TRANSACTION_ALREADY_PROCESSED = "Notification.TransactionAlreadyProcessed";
    public const string TRANSACTION_PENDING = "Notification.TransactionPending";
    public const string PURCHASE_FAILED = "Notification.PurchaseFailed";

    public const string USER_NOT_FOUND = "Notification.UserNotFound";
    public const string USER_DELETED = "Notification.UserDeleted";
    public const string USER_INACTIVE = "Notification.UserInactive";

    public const string ITEM_NOT_FOUND_OR_INACTIVE = "Notification.ItemNotFoundOrInactive";
    public const string INSUFFICIENT_BALANCE = "Notification.InsufficientBalance";
    public const string ACHIEVEMENTS_NOT_FOUND = "Notification.AchievementsNotFound";
    public const string CARD_HEROES_NOT_FOUND = "Notification.CardHeroesNotFound";
    public const string BOOKS_NOT_FOUND = "Notification.BooksNotFound";
    public const string PETS_NOT_FOUND = "Notification.PetsNotFound";
    public const string CARD_CAPTAINS_NOT_FOUND = "Notification.CardCaptainsNotFound";
    public const string COLLABORATION_EQUIPMENTS_NOT_FOUND = "Notification.CollaborationEquipmentsNotFound";
    public const string CARD_MILITARIES_NOT_FOUND = "Notification.CardMilitariesNotFound";
    public const string CARD_SPELLS_NOT_FOUND = "Notification.CardSpellsNotFound";
    public const string COLLABORATIONS_NOT_FOUND = "Notification.CollaborationsNotFound";
    public const string CARD_MONSTERS_NOT_FOUND = "Notification.CardMonstersNotFound";
    public const string EQUIPMENTS_NOT_FOUND = "Notification.EquipmentsNotFound";
    public const string MEDALS_NOT_FOUND = "Notification.MedalsNotFound";
    public const string SKILLS_NOT_FOUND = "Notification.SkillsNotFound";
    public const string SYMBOLS_NOT_FOUND = "Notification.SymbolsNotFound";
    public const string TITLES_NOT_FOUND = "Notification.TitlesNotFound";
    public const string MAGIC_FORMATION_CIRCLES_NOT_FOUND = "Notification.MagicFormationCirclesNotFound";
    public const string RELICS_NOT_FOUND = "Notification.RelicsNotFound";
    public const string CARD_COLONELS_NOT_FOUND = "Notification.CardColonelsNotFound";
    public const string CARD_GENERALS_NOT_FOUND = "Notification.CardGeneralsNotFound";
    public const string CARD_ADMIRALS_NOT_FOUND = "Notification.CardAdmiralsNotFound";
    public const string CARD_SOLDIERS_NOT_FOUND = "Notification.CardSoldiersNotFound";
    public const string BORDERS_NOT_FOUND = "Notification.BordersNotFound";
    public const string TALISMANS_NOT_FOUND = "Notification.TalismansNotFound";
    public const string PUPPETS_NOT_FOUND = "Notification.PuppetsNotFound";
    public const string ALCHEMIES_NOT_FOUND = "Notification.AlchemiesNotFound";
    public const string FORGES_NOT_FOUND = "Notification.ForgesNotFound";
    public const string CARD_LIVES_NOT_FOUND = "Notification.CardLivesNotFound";
    public const string ARTWORKS_NOT_FOUND = "Notification.ArtworksNotFound";
    public const string SPIRIT_BEASTS_NOT_FOUND = "Notification.SpiritBeastsNotFound";
    public const string AVATARS_NOT_FOUND = "Notification.AvatarsNotFound";
    public const string SPIRIT_CARDS_NOT_FOUND = "Notification.SpiritCardsNotFound";
    public const string ARTIFACTS_NOT_FOUND = "Notification.ArtifactsNotFound";
    public const string ARCHITECTURES_NOT_FOUND = "Notification.ArchitecturesNotFound";
    public const string TECHNOLOGIES_NOT_FOUND = "Notification.TechnologiesNotFound";
    public const string VEHICLES_NOT_FOUND = "Notification.VehiclesNotFound";
    public const string CORES_NOT_FOUND = "Notification.CoresNotFound";
    public const string WEAPONS_NOT_FOUND = "Notification.WeaponsNotFound";
    public const string ROBOTS_NOT_FOUND = "Notification.RobotsNotFound";
    public const string BADGES_NOT_FOUND = "Notification.BadgesNotFound";
    public const string MECHA_BEASTS_NOT_FOUND = "Notification.MechaBeastsNotFound";
    public const string RUNES_NOT_FOUND = "Notification.RunesNotFound";
    public const string FURNITURES_NOT_FOUND = "Notification.FurnituresNotFound";
    public const string FOODS_NOT_FOUND = "Notification.FoodsNotFound";
    public const string BEVERAGES_NOT_FOUND = "Notification.BeveragesNotFound";
    public const string BUILDINGS_NOT_FOUND = "Notification.BuildingsNotFound";
    public const string PLANTS_NOT_FOUND = "Notification.PlantsNotFound";
    public const string FASHIONS_NOT_FOUND = "Notification.FashionsNotFound";
    public const string EMOJIS_NOT_FOUND = "Notification.EmojisNotFound";
    public const string OUTFITS_NOT_FOUND = "Notification.OutfitsNotFound";
    public const string ERROR_UNSUPPORTED_DATA_TYPE = "Notification.ErrorUnsupportedDataType";

    #region Hệ Thống Thông Báo Nâng Cấp (Upgrade System Keys)
    // Các Key dùng để tra cứu trong file ngôn ngữ (JSON/Database)
    public const string NOT_ENOUGH_MATERIALS = "Notification.NotEnoughMaterials";
    public const string PLEASE_SELECT_QUANTITY = "Notification.PleaseSelectQuantity";
    public const string READY_TO_UPGRADE = "Notification.ReadyToUpgrade";
    public const string MAX_LEVEL_REACHED = "Notification.MaxLevelReached";
    public const string UPGRADE_ALREADY_MAX = "Notification.UpgradeAlreadyMax";
    public const string PROCESSING_UPGRADE = "Notification.ProcessingUpgrade";
    public const string UPGRADE_SUCCESS = "Notification.UpgradeSuccess";
    public const string UPGRADE_FAILED = "Notification.UpgradeFailed";
    public const string SERVER_ERROR = "Notification.ServerError";
    public const string INVALID_FILTER = "Notification.InvalidFilter";
    #endregion

    public static string WaringLevel(int value)
    {
        return $"Your level is too low. Required level: {value}. Please level up and try again.";
    }
}
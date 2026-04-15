using UnityEngine;

public static class MessageConstants
{
    public const string USERNAME_DOES_NOT_EXIST = "Username does not exist.";
    public const string INCORRECT_PASSWORD = "Incorrect password.";
    public const string MAX_LEVEL_REACHED = "Maximum level reached.";
    public const string RECIPE_NOT_FOUND = "Recipe not found for this level.";
    public const string NOT_ENOUGH_MATERIALS = "Not enough materials to upgrade.";
    public const string UPGRADE_SUCCESS_ONE = "Successfully upgraded 1 level.";
    public const string UPGRADE_SUCCESS_MULTIPLE = "Successfully upgraded {0} levels.";
    public const string SYSTEM_ERROR = "System error: {0}.";
    public const string NOT_ENOUGH_ITEM = "Not enough {0}.";
    public const string ITEM_NOT_FOUND = "Item not found!";
    public const string ITEM_NOT_ENOUGH = "Not enough item quantity!";
    public const string ITEM_ALREADY_EXISTS = "Item already exists!";
    public const string ITEM_ADDED = "Item added successfully!";
    public const string ITEM_REMOVED = "Item removed successfully!";
    public const string IMAGE_IS_NULL = "Your image not found in Resources!";
    public const string PREFAB_IS_NULL = "Your prefab is null! Check if the prefab is correctly loaded.";
    public const string USERNAME_NOT_EXIST = "Your account does not exist";
    public const string USERNAME_ALREADY_EXIST = "Account already exists!";
    public const string USERNAME_IS_EMPTY = "Username can not be empty!";
    public const string PASSWORD_IS_EMPTY = "Password can not be empty!";
    public const string CONFIRM_PASSWORD_IS_EMPTY = "Confirm password can not be empty!";
    public const string PASSWORDS_DO_NOT_MATCH = "Passwords do not match!";
    public const string CARD_ALREADY_USED_IN_ANOTHER_POSITION = "Message.MessageNumber1";
    public static string WaringLevel(int value)
    {
        return $"Your level is too low. Required level: {value}. Please level up and try again.";
    }
}
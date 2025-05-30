using System.Collections.Generic;
public static class MessageHelper
{
    public static string WaringLevel(int value)
    {
        return $"Your level is too low. Required level: {value}. Please level up and try again.";
    }
    public static class MessageConstants
    {
        public const string UsernameNotExist = "Your account does not exist";
        public const string UsernameAlreadyExist = "Account already exists!";
        public const string UsernameIsEmpty = "Username can not be empty!";
        public const string PasswordIsEmpty = "Password can not be empty!";
        public const string ConfirmPasswordIsEmpty = "Confirm password can not be empty!";
        public const string PasswordNotMatch = "Passwords do not match!";
    }
}
using UnityEngine;

public static class AuthManager
{
    private const string TokenKey = "auth_token";

    // Lưu token khi login thành công
    public static void SaveUserId(string userId)
    {
        PlayerPrefs.SetString(TokenKey, userId);
        PlayerPrefs.Save();
    }

    // Lấy token (nếu có)
    public static string GetUserId()
    {
        return PlayerPrefs.GetString(TokenKey, "");
    }

    // Kiểm tra đã đăng nhập chưa
    public static bool IsLoggedIn()
    {
        return !string.IsNullOrEmpty(GetUserId());
    }

    // Đăng xuất
    public static void Logout()
    {
        PlayerPrefs.DeleteKey(TokenKey);
        PlayerPrefs.Save();
    }
}

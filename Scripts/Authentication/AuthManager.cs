public static class AuthManager
{
    public static string GetToken()
    {
        return AuthService.GetAccessToken();
    }

    public static string GetUserId()
    {
        return AuthService.GetUserId();
    }

    public static bool IsLoggedIn()
    {
        return AuthService.IsLoggedIn();
    }

    public static string GetBearerToken()
    {
        return AuthService.GetAuthorizationHeader();
    }

    public static void Logout()
    {
        AuthService.Logout();
    }
}

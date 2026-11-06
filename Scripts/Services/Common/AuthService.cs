using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class AuthService
{
    private const string AccessTokenKey = "auth_token";
    private const string RefreshTokenKey = "auth_refresh_token";
    private const string AccessTokenExpirationKey = "auth_access_token_expiration";
    private const string UserIdKey = "auth_user_id";
    private static readonly SemaphoreSlim RefreshLock = new SemaphoreSlim(1, 1);

    public static string GetAccessToken()
    {
        return PlayerPrefs.GetString(AccessTokenKey, string.Empty);
    }

    public static string GetRefreshToken()
    {
        return PlayerPrefs.GetString(RefreshTokenKey, string.Empty);
    }

    public static string GetUserId()
    {
        string userId = GetUserIdFromToken(GetAccessToken());
        if (string.IsNullOrWhiteSpace(userId))
        {
            userId = PlayerPrefs.GetString(UserIdKey, string.Empty);
        }

        if (!string.IsNullOrWhiteSpace(userId))
        {
            User.CurrentUserId = userId;
        }

        return userId;
    }

    public static void SaveUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("A user ID is required.", nameof(userId));
        }

        PlayerPrefs.SetString(UserIdKey, userId);
        PlayerPrefs.Save();
        User.CurrentUserId = userId;
    }

    public static bool IsLoggedIn()
    {
        string accessToken = GetAccessToken();
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            if (IsAccessTokenExpired())
            {
                Logout();
                return false;
            }

            ApplyUserIdFromToken(accessToken);
            return true;
        }

        return !string.IsNullOrWhiteSpace(GetUserId());
    }

    public static void Logout()
    {
        ClearTokens();
        PlayerPrefs.DeleteKey(UserIdKey);
        PlayerPrefs.Save();
        User.CurrentUserId = string.Empty;
    }

    public static string GetAuthorizationHeader()
    {
        string accessToken = GetAccessToken();
        return string.IsNullOrWhiteSpace(accessToken) ? string.Empty : $"Bearer {accessToken}";
    }

    public static void SaveAccessToken(string accessToken)
    {
        string normalizedToken = NormalizeToken(accessToken);
        if (string.IsNullOrWhiteSpace(normalizedToken))
        {
            Logout();
            return;
        }

        PlayerPrefs.DeleteKey(UserIdKey);
        User.CurrentUserId = string.Empty;
        PlayerPrefs.SetString(AccessTokenKey, normalizedToken);
        PlayerPrefs.DeleteKey(RefreshTokenKey);
        SaveExpiration(normalizedToken, 0);
        ApplyUserIdFromToken(normalizedToken);
        PlayerPrefs.Save();
    }

    public static void SaveTokens(string accessToken, string refreshToken, long expiresInSeconds)
    {
        string normalizedAccessToken = NormalizeToken(accessToken);
        if (string.IsNullOrWhiteSpace(normalizedAccessToken))
        {
            throw new ArgumentException("An access token is required.", nameof(accessToken));
        }

        PlayerPrefs.DeleteKey(UserIdKey);
        User.CurrentUserId = string.Empty;
        PlayerPrefs.SetString(AccessTokenKey, normalizedAccessToken);
        StoreRefreshToken(refreshToken);
        SaveExpiration(normalizedAccessToken, expiresInSeconds);
        ApplyUserIdFromToken(normalizedAccessToken);
        PlayerPrefs.Save();
    }

    public static void SaveTokens(AuthTokenResponse tokenResponse)
    {
        if (tokenResponse == null)
        {
            throw new ArgumentNullException(nameof(tokenResponse));
        }

        SaveTokens(tokenResponse.AccessToken, tokenResponse.RefreshToken, tokenResponse.ExpiresInSeconds);
    }

    public static bool IsAccessTokenExpired(int refreshBeforeExpirySeconds = 0)
    {
        string accessToken = GetAccessToken();
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            return true;
        }

        string storedExpirationValue = PlayerPrefs.GetString(AccessTokenExpirationKey, string.Empty);
        if (!string.IsNullOrEmpty(storedExpirationValue))
        {
            long storedExpiration;
            if (!long.TryParse(storedExpirationValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out storedExpiration)
                || storedExpiration <= 0)
            {
                Debug.LogWarning("[AuthService.IsAccessTokenExpired] Invalid stored access-token expiration.");
                return true;
            }

            long threshold = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + Math.Max(0, refreshBeforeExpirySeconds);
            return storedExpiration <= threshold;
        }

        DateTimeOffset? tokenExpiration = GetTokenExpirationUtc(accessToken);
        if (!tokenExpiration.HasValue)
        {
            return false;
        }

        return tokenExpiration.Value <= DateTimeOffset.UtcNow.AddSeconds(Math.Max(0, refreshBeforeExpirySeconds));
    }

    public static async Task<string> GetValidAccessTokenAsync(
        Func<string, Task<AuthTokenResponse>> refreshRequest,
        int refreshBeforeExpirySeconds = 60)
    {
        return await RefreshAccessTokenInternalAsync(refreshRequest, refreshBeforeExpirySeconds, false);
    }

    public static Task<string> RefreshAccessTokenAsync(
        Func<string, Task<AuthTokenResponse>> refreshRequest)
    {
        return RefreshAccessTokenInternalAsync(refreshRequest, 0, true);
    }

    private static async Task<string> RefreshAccessTokenInternalAsync(
        Func<string, Task<AuthTokenResponse>> refreshRequest,
        int refreshBeforeExpirySeconds,
        bool forceRefresh)
    {
        if (refreshRequest == null)
        {
            throw new ArgumentNullException(nameof(refreshRequest));
        }

        await RefreshLock.WaitAsync();
        try
        {
            if (!forceRefresh && !IsAccessTokenExpired(refreshBeforeExpirySeconds))
            {
                return GetAccessToken();
            }

            string refreshToken = GetRefreshToken();
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new InvalidOperationException("No refresh token is available.");
            }

            AuthTokenResponse response;
            try
            {
                response = await refreshRequest(refreshToken);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AuthService.RefreshAccessTokenAsync] Token refresh failed: {ex.Message}");
                throw;
            }

            if (response == null || string.IsNullOrWhiteSpace(response.AccessToken))
            {
                throw new InvalidOperationException("The refresh endpoint returned no access token.");
            }

            SaveRefreshedTokens(response);
            return GetAccessToken();
        }
        finally
        {
            RefreshLock.Release();
        }
    }

    private static void ClearTokens()
    {
        PlayerPrefs.DeleteKey(AccessTokenKey);
        PlayerPrefs.DeleteKey(RefreshTokenKey);
        PlayerPrefs.DeleteKey(AccessTokenExpirationKey);
        PlayerPrefs.Save();
    }

    private static string GetUserIdFromToken(string token)
    {
        string[] parts = NormalizeToken(token).Split('.');
        if (parts.Length < 2)
        {
            return string.Empty;
        }

        string payloadJson = Base64UrlDecode(parts[1]);
        if (string.IsNullOrWhiteSpace(payloadJson))
        {
            return string.Empty;
        }

        try
        {
            JObject payload = JObject.Parse(payloadJson);
            string userId = payload["userId"]?.Value<string>();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                return userId;
            }

            string subject = payload["sub"]?.Value<string>();
            return !string.IsNullOrWhiteSpace(subject)
                ? subject
                : payload["id"]?.Value<string>() ?? string.Empty;
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[AuthService.GetUserIdFromToken] Invalid JWT payload: {ex.Message}");
            return string.Empty;
        }
    }

    private static void ApplyUserIdFromToken(string token)
    {
        string userId = GetUserIdFromToken(token);
        if (!string.IsNullOrWhiteSpace(userId))
        {
            User.CurrentUserId = userId;
        }
    }

    private static DateTimeOffset? GetTokenExpirationUtc(string token)
    {
        string[] parts = NormalizeToken(token).Split('.');
        if (parts.Length < 2)
        {
            return null;
        }

        string payloadJson = Base64UrlDecode(parts[1]);
        if (string.IsNullOrWhiteSpace(payloadJson))
        {
            return null;
        }

        try
        {
            JToken expirationToken = JObject.Parse(payloadJson)["exp"];
            long expiration;
            if (expirationToken == null
                || !long.TryParse(expirationToken.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out expiration))
            {
                return null;
            }

            return DateTimeOffset.FromUnixTimeSeconds(expiration);
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[AuthService.GetTokenExpirationUtc] Invalid JWT payload: {ex.Message}");
            return null;
        }
    }

    private static void SaveRefreshedTokens(AuthTokenResponse response)
    {
        string accessToken = NormalizeToken(response.AccessToken);
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            throw new ArgumentException("The refresh response must contain an access token.", nameof(response));
        }

        PlayerPrefs.SetString(AccessTokenKey, accessToken);
        if (!string.IsNullOrWhiteSpace(response.RefreshToken))
        {
            StoreRefreshToken(response.RefreshToken);
        }

        SaveExpiration(accessToken, response.ExpiresInSeconds);
        PlayerPrefs.Save();
        ApplyUserIdFromToken(accessToken);
    }

    private static void StoreRefreshToken(string refreshToken)
    {
        string normalizedToken = string.IsNullOrWhiteSpace(refreshToken) ? string.Empty : refreshToken.Trim();
        if (string.IsNullOrEmpty(normalizedToken))
        {
            PlayerPrefs.DeleteKey(RefreshTokenKey);
        }
        else
        {
            PlayerPrefs.SetString(RefreshTokenKey, normalizedToken);
        }
    }

    private static void SaveExpiration(string accessToken, long expiresInSeconds)
    {
        long expiration = 0;
        if (expiresInSeconds > 0)
        {
            expiration = DateTimeOffset.UtcNow.AddSeconds(expiresInSeconds).ToUnixTimeSeconds();
        }
        else
        {
            DateTimeOffset? tokenExpiration = GetTokenExpirationUtc(accessToken);
            if (tokenExpiration.HasValue)
            {
                expiration = tokenExpiration.Value.ToUnixTimeSeconds();
            }
        }

        if (expiration > 0)
        {
            PlayerPrefs.SetString(AccessTokenExpirationKey, expiration.ToString(CultureInfo.InvariantCulture));
        }
        else
        {
            PlayerPrefs.DeleteKey(AccessTokenExpirationKey);
        }
    }

    private static string NormalizeToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return string.Empty;
        }

        string normalizedToken = token.Trim();
        if (normalizedToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            normalizedToken = normalizedToken.Substring("Bearer ".Length).Trim();
        }

        return normalizedToken;
    }

    private static string Base64UrlDecode(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        string normalized = input.Replace('-', '+').Replace('_', '/');
        switch (normalized.Length % 4)
        {
            case 2:
                normalized += "==";
                break;
            case 3:
                normalized += "=";
                break;
            case 0:
                break;
            default:
                return string.Empty;
        }

        try
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(normalized));
        }
        catch (FormatException)
        {
            return string.Empty;
        }
    }
}

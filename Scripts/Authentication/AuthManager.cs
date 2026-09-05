using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class AuthManager
{
    private const string TokenKey = "auth_token";
    private const int TokenLifetimeDays = 7;

    public static void SaveToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            Logout();
            return;
        }

        string normalizedToken = token.Trim();
        if (normalizedToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            normalizedToken = normalizedToken.Substring("Bearer ".Length).Trim();
        }

        if (string.IsNullOrWhiteSpace(normalizedToken))
        {
            Logout();
            return;
        }

        PlayerPrefs.SetString(TokenKey, normalizedToken);
        PlayerPrefs.Save();

        ApplyUserIdFromToken(normalizedToken);
    }

    public static void SaveUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            Logout();
            return;
        }

        User.CurrentUserId = userId;
        SaveToken(CreateJwtFromUserId(userId));
    }

    public static string GetToken()
    {
        return PlayerPrefs.GetString(TokenKey, "");
    }

    public static string GetUserId()
    {
        string token = GetToken();
        if (string.IsNullOrWhiteSpace(token))
        {
            return User.CurrentUserId;
        }

        string userIdFromToken = GetUserIdFromToken(token);
        if (!string.IsNullOrWhiteSpace(userIdFromToken))
        {
            User.CurrentUserId = userIdFromToken;
            return userIdFromToken;
        }

        return User.CurrentUserId;
    }

    public static bool IsLoggedIn()
    {
        string token = GetToken();
        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        if (IsTokenExpired(token) || string.IsNullOrWhiteSpace(GetUserIdFromToken(token)))
        {
            Logout();
            return false;
        }

        ApplyUserIdFromToken(token);
        return true;
    }

    public static void Logout()
    {
        PlayerPrefs.DeleteKey(TokenKey);
        PlayerPrefs.Save();

        User.CurrentUserId = string.Empty;
    }

    public static string GetBearerToken()
    {
        string token = GetToken();
        return string.IsNullOrWhiteSpace(token) ? string.Empty : $"Bearer {token}";
    }

    public static string CreateJwtFromUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return string.Empty;
        }

        var payload = new
        {
            sub = userId,
            userId = userId,
            exp = DateTimeOffset.UtcNow.AddDays(TokenLifetimeDays).ToUnixTimeSeconds()
        };

        string header = Base64UrlEncode("{\"alg\":\"HS256\",\"typ\":\"JWT\"}");
        string payloadJson = JsonConvert.SerializeObject(payload);
        string encodedPayload = Base64UrlEncode(payloadJson);

        return $"{header}.{encodedPayload}.signature";
    }

    public static string GetUserIdFromToken(string token)
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

        string[] parts = normalizedToken.Split('.');
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

            if (payload["userId"] != null && !string.IsNullOrWhiteSpace(payload["userId"]?.Value<string>()))
            {
                return payload["userId"].Value<string>();
            }

            if (payload["sub"] != null && !string.IsNullOrWhiteSpace(payload["sub"]?.Value<string>()))
            {
                return payload["sub"].Value<string>();
            }

            if (payload["id"] != null && !string.IsNullOrWhiteSpace(payload["id"]?.Value<string>()))
            {
                return payload["id"].Value<string>();
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[AuthManager.GetUserIdFromToken] Invalid JWT payload: {ex.Message}");
        }

        return string.Empty;
    }

    public static void ApplyUserIdFromToken(string token)
    {
        string userId = GetUserIdFromToken(token);
        if (!string.IsNullOrWhiteSpace(userId))
        {
            User.CurrentUserId = userId;
        }
    }

    public static bool IsTokenExpired(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return true;
        }

        string normalizedToken = token.Trim();
        if (normalizedToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            normalizedToken = normalizedToken.Substring("Bearer ".Length).Trim();
        }

        string[] parts = normalizedToken.Split('.');
        if (parts.Length < 2)
        {
            return true;
        }

        string payloadJson = Base64UrlDecode(parts[1]);
        if (string.IsNullOrWhiteSpace(payloadJson))
        {
            return true;
        }

        try
        {
            JObject payload = JObject.Parse(payloadJson);
            long expiration = payload["exp"]?.Value<long>() ?? 0;
            return expiration <= DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[AuthManager.IsTokenExpired] Invalid JWT payload: {ex.Message}");
            return true;
        }
    }

    private static string Base64UrlEncode(string value)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(bytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
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
            byte[] bytes = Convert.FromBase64String(normalized);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            return string.Empty;
        }
    }
}

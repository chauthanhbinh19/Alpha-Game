using System.Collections.Generic;
using UnityEngine;

public static class TextureHelper
{
    private static readonly Dictionary<string, Texture> cache = new Dictionary<string, Texture>();
    private static readonly Dictionary<string, Texture2D> cache2D = new Dictionary<string, Texture2D>();

    public static Texture LoadTextureCached(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        if (cache.TryGetValue(path, out var texture))
            return texture;

        texture = Resources.Load<Texture>(path);
        cache[path] = texture;
        return texture;
    }

    public static Texture2D LoadTexture2DCached(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        if (cache2D.TryGetValue(path, out var texture2D))
            return texture2D;

        texture2D = Resources.Load<Texture2D>(path);
        cache2D[path] = texture2D;
        return texture2D;
    }

    public static void ClearCache()
    {
        cache.Clear();
        cache2D.Clear();
    }
}

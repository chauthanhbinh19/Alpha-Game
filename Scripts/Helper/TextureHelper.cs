using System.Collections.Generic;
using UnityEngine;

public static class TextureHelper
{
    private static readonly Dictionary<string, Texture> cache = new Dictionary<string, Texture>();

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

    public static void ClearCache()
    {
        cache.Clear();
    }
}

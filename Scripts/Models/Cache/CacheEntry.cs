using System;

public class CacheEntry
{
    public TopupResponseDTO Response { get; set; }
    public DateTime ExpirationTime { get; set; }
}
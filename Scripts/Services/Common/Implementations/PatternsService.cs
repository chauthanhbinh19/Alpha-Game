using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class PatternsService : IPatternsService
{
    private static PatternsService _instance;
    private readonly IPatternsRepository _patternsRepository;
    private readonly Dictionary<string, Patterns> _masterPatterns = new Dictionary<string, Patterns>();

    public PatternsService(IPatternsRepository patternsRepository)
    {
        _patternsRepository = patternsRepository;
    }

    public static PatternsService Create()
    {
        if (_instance == null)
        {
            _instance = new PatternsService(new PatternsRepository());
        }
        return _instance;
    }

    public Task<List<Patterns>> GetAllPatternsAsync()
    {
        return _patternsRepository.GetAllPatternsAsync();
    }

    public Task<Patterns> GetPatternByIdAsync(string patternId)
    {
        return _patternsRepository.GetPatternByIdAsync(patternId);
    }

    public async Task InitializeMasterDataAsync()
    {
        _masterPatterns.Clear();
        List<Patterns> allPatterns = await _patternsRepository.GetPatternsMasterDataAsync();
        foreach (var p in allPatterns)
        {
            _masterPatterns[p.Id] = p;
        }
        Debug.Log($"[PatternService] Đã nạp thành công {_masterPatterns.Count} bộ Pattern AoE!");
    }

    // Hàm lấy nhanh Pattern từ RAM không chạm vào DB
    public Patterns GetPatternFromCache(string patternId)
    {
        if (string.IsNullOrEmpty(patternId)) return null;
        return _masterPatterns.TryGetValue(patternId, out var pattern) ? pattern : null;
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

public class PatternsService : IPatternsService
{
    private static PatternsService _instance;
    private readonly IPatternsRepository _patternsRepository;

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
}
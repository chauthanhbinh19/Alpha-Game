using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPatternsRepository
{
    Task<List<Patterns>> GetAllPatternsAsync();
    Task<Patterns> GetPatternByIdAsync(string patternId);
}
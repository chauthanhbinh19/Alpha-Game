using System.Collections.Generic;
using System.Threading.Tasks;
public interface ISkillsRepository
{
    Task<List<string>> GetUniqueSkillsTypesAsync();
    Task<List<string>> GetUniqueSkillsIdAsync();
    Task<List<Skills>> GetSkillsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<Skills>> GetSkillsWithoutLimitAsync();
    Task<int> GetSkillsCountAsync(string search, string type, string rare);
    Task<List<Skills>> GetSkillsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetSkillsWithPriceCountAsync(string type);
    Task<Skills> GetSkillByIdAsync(string Id);
}

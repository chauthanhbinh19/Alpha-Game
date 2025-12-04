using System.Collections.Generic;
using System.Threading.Tasks;
public interface ISkillsService
{
    Task<List<string>> GetUniqueSkillsTypesAsync();
    Task<List<string>> GetUniqueSkillsIdAsync();
    Task<List<Skills>> GetSkillsAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetSkillsCountAsync(string type, string rare);
    Task<List<Skills>> GetSkillsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetSkillsWithPriceCountAsync(string type);
    Task<Skills> GetSkillByIdAsync(string Id);
}

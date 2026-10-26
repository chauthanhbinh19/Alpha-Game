using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserArchivesRepository
{
    Task<UserArchives> GetUserArchivesAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserArchives>> InsertOrUpdateUserArchivesAsync(string userId, UserArchives Archives, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserArchives> GetSumUserArchivesAsync(string userId);
}
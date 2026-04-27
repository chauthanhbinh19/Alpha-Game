using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMedalsService
{
    Task<List<Medals>> GetUserMedalsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserMedalsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserMedalAsync(Medals Medals, string userId);
    Task<bool> UpdateMedalLevelAsync(Medals Medals, int TitleLevel);
    Task<bool> UpdateMedalBreakthroughAsync(Medals Medals, int star, double quantity);
    Task<Medals> GetUserMedalByIdAsync(string user_id, string Id);
    Task<Medals> SumPowerUserMedalsAsync();

}
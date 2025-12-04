using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAvatarsService
{
    Task<List<string>> GetUniqueAvatarsIdAsync();
    Task<List<Avatars>> GetAvatarsAsync(int pageSize, int offset, string rare);
    Task<int> GetAvatarsCountAsync(string rare);
    Task<List<Avatars>> GetAvatarsWithPriceAsync(int pageSize, int offset);
    Task<int> GetAvatarsWithPriceCountAsync();
    Task<Avatars> GetAvatarByIdAsync(string Id);
    Task<Avatars> SumPowerAvatarsPercentAsync();
}
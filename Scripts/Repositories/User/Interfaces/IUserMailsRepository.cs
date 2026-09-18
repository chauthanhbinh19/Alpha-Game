using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMailsRepository
{
    Task<List<Mails>> GetUserInboxAsync(string userId, string type = null, int page = 1, int pageSize = 20);
    Task<int> GetUnreadMailCountAsync(string userId);
    Task<bool> MarkAsReadAsync(string userId, string mailId);
    Task<int> MarkAllAsReadAsync(string userId);
    Task<bool> MarkAsClaimedAsync(string userId, string mailId);
    Task<bool> SoftDeleteAsync(string userId, string mailId);
}
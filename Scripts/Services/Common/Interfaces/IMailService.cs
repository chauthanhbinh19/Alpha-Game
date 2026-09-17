using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMailService
{
    Task<List<Mail>> GetUserMailsAsync(string receiverId, string type, int page = 1, int pageSize = 20);
    Task<Mail> GetMailByIdAsync(string mailId);
    Task<int> GetUnreadMailCountAsync(string receiverId);
    Task<bool> InsertAsync(Mail mail);
    Task<bool> UpdateAsync(Mail mail);
    Task<bool> MarkAsReadAsync(string mailId);
    Task<int> MarkAllAsReadByReceiverAsync(string receiverId);
    Task<bool> SoftDeleteAsync(string mailId);
    Task<bool> HardDeleteAsync(string mailId);
}
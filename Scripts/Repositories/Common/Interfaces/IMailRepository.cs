using System.Threading.Tasks;

public interface IMailRepository
{
    Task<bool> InsertAsync(Mail mail);
    Task<bool> UpdateAsync(Mail mail);
    Task<bool> MarkAsReadAsync(string mailId);
    Task<int> MarkAllAsReadByReceiverAsync(string receiverId);
    Task<bool> SoftDeleteAsync(string mailId);
    Task<bool> HardDeleteAsync(string mailId);
}
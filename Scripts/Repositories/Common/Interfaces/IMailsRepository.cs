using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMailsRepository
{
    Task<bool> CreateMailAsync(Mails mail);
    Task<bool> UpdateMailAsync(Mails mail);
    Task<bool> DeleteMailAsync(string mailId);
    Task<bool> SendMailToUsersAsync(string mailId, List<string> userIds);
    Task<List<Mails>> GetMailTemplatesAsync(int page = 1, int pageSize = 20);
}
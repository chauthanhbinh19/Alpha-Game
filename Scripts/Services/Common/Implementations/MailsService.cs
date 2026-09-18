using System.Collections.Generic;
using System.Threading.Tasks;

public class MailsService : IMailsService
{
    private readonly IMailsRepository _mailRepository;

    public MailsService(IMailsRepository mailRepository)
    {
        _mailRepository = mailRepository;
    }

    public static IMailsService Create() => ServiceContainer.GetService<IMailsService>();

    

    public async Task<List<Mails>> GetUserMailsAsync(string receiverId, string type, int page = 1, int pageSize = 20)
    {
        return await GetUserMailsAsync(receiverId, type, page, pageSize);
    }

    public async Task<Mails> GetMailByIdAsync(string mailId)
    {
        return await GetMailByIdAsync(mailId);
    }

    public async Task<int> GetUnreadMailCountAsync(string receiverId)
    {
        return await GetUnreadMailCountAsync(receiverId);
    }

    public Task<bool> CreateMailAsync(Mails mail)
    {
        return _mailRepository.CreateMailAsync(mail);
    }

    public Task<bool> UpdateMailAsync(Mails mail)
    {
        return _mailRepository.UpdateMailAsync(mail);
    }

    public Task<bool> DeleteMailAsync(string mailId)
    {
        return _mailRepository.DeleteMailAsync(mailId);
    }

    public Task<bool> SendMailToUsersAsync(string mailId, List<string> userIds)
    {
        return _mailRepository.SendMailToUsersAsync(mailId, userIds);
    }

    public Task<List<Mails>> GetMailTemplatesAsync(int page = 1, int pageSize = 20)
    {
        return _mailRepository.GetMailTemplatesAsync(page, pageSize);
    }
}
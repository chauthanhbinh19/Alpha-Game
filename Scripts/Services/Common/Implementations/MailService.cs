using System.Threading.Tasks;

public class MailService : IMailService
{
    private readonly IMailRepository _mailRepository;

    public MailService(IMailRepository mailRepository)
    {
        _mailRepository = mailRepository;
    }

    public static IMailService Create() => ServiceContainer.GetService<IMailService>();

    public Task<bool> InsertAsync(Mail mail){
        return _mailRepository.InsertAsync(mail);
    }

    public Task<bool> UpdateAsync(Mail mail){
        return _mailRepository.UpdateAsync(mail);
    }

    public Task<bool> MarkAsReadAsync(string mailId){
        return _mailRepository.MarkAsReadAsync(mailId);
    }

    public Task<int> MarkAllAsReadByReceiverAsync(string receiverId){
        return _mailRepository.MarkAllAsReadByReceiverAsync(receiverId);
    }

    public Task<bool> SoftDeleteAsync(string mailId){
        return _mailRepository.SoftDeleteAsync(mailId);
    }

    public Task<bool> HardDeleteAsync(string mailId){
        return _mailRepository.HardDeleteAsync(mailId);
    }
}
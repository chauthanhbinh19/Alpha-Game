using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMailsService : IUserMailsService
{
    private readonly IUserMailsRepository _userMailsRepository;

    public UserMailsService(IUserMailsRepository userMailsRepository)
    {
        _userMailsRepository = userMailsRepository;
    }

    public static IUserMailsService Create() => ServiceContainer.GetService<IUserMailsService>();

    public Task<int> GetUnreadMailCountAsync(string userId)
    {
        return _userMailsRepository.GetUnreadMailCountAsync(userId);
    }

    public Task<List<Mails>> GetUserInboxAsync(string userId, string type = null, int page = 1, int pageSize = 20)
    {
        return _userMailsRepository.GetUserInboxAsync(userId, type, page, pageSize);
    }

    public Task<int> MarkAllAsReadAsync(string userId)
    {
        return _userMailsRepository.MarkAllAsReadAsync(userId);
    }

    public Task<bool> MarkAsClaimedAsync(string userId, string mailId)
    {
        return _userMailsRepository.MarkAsClaimedAsync(userId, mailId);
    }

    public Task<bool> MarkAsReadAsync(string userId, string mailId)
    {
        return _userMailsRepository.MarkAsReadAsync(userId, mailId);
    }

    public Task<bool> SoftDeleteAsync(string userId, string mailId)
    {
        return _userMailsRepository.SoftDeleteAsync(userId, mailId);
    }
}
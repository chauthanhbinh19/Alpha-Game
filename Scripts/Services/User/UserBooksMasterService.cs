public class UserBooksMasterService : IUserBooksMasterService
{
    private readonly IUserBooksMasterRepository _BooksMasterRepository;

    public UserBooksMasterService(IUserBooksMasterRepository BooksMasterRepository)
    {
        _BooksMasterRepository = BooksMasterRepository;
    }

    public static UserBooksMasterService Create()
    {
        return new UserBooksMasterService(new UserBooksMasterRepository());
    }

    public Master GetBooksMaster(string type, string card_id)
    {
        return _BooksMasterRepository.GetBooksMaster(type, card_id);
    }

    public void InsertOrUpdateBooksMaster(Master master, string type, string card_id)
    {
        _BooksMasterRepository.InsertOrUpdateBooksMaster(master, type, card_id);
    }

    public Master GetSumBooksMaster(string user_id, string card_id)
    {
        return _BooksMasterRepository.GetSumBooksMaster(user_id, card_id);
    }
}

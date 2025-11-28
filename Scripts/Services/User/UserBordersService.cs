using System.Collections.Generic;

public class UserBordersService : IUserBordersService
{
    private IUserBordersRepository _userBordersRepository;

    public UserBordersService(IUserBordersRepository userBordersRepository)
    {
        _userBordersRepository = userBordersRepository;
    }

    public static UserBordersService Create()
    {
        return new UserBordersService(new UserBordersRepository());
    }

    public List<Borders> GetUserBorders(string user_id, int pageSize, int offset, string rare)
    {
        List<Borders> list = _userBordersRepository.GetUserBorders(user_id, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserBordersCount(string user_id, string rare)
    {
        return _userBordersRepository.GetUserBordersCount(user_id, rare);
    }

    public bool InsertUserBorders(Borders borders, string userId)
    {
        return _userBordersRepository.InsertUserBorders(borders, userId);
    }

    public bool InsertUserBordersById(string borderId, string userId)
    {
        IBordersRepository _repository = new BordersRepository();
        BordersService _service = new BordersService(_repository);
        return _userBordersRepository.InsertUserBordersById(_service.GetBordersById(borderId), userId);
    }

    public Borders GetBordersByUsed(string user_id)
    {
        return _userBordersRepository.GetBordersByUsed(user_id);
    }

    public void UpdateIsUsedBorders(string borderId, string userId, bool is_used)
    {
        _userBordersRepository.UpdateIsUsedBorders(borderId, userId, is_used);
    }

    public Borders SumPowerUserBorders()
    {
        return _userBordersRepository.SumPowerUserBorders();
    }
}

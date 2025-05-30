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

    public List<Borders> GetUserBorders(string user_id, int pageSize, int offset)
    {
        List<Borders> list = _userBordersRepository.GetUserBorders(user_id, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserBordersCount(string user_id)
    {
        return _userBordersRepository.GetUserBordersCount(user_id);
    }

    public bool InsertUserBorders(Borders borders)
    {
        return _userBordersRepository.InsertUserBorders(borders);
    }

    public bool InsertUserBordersById(string Id)
    {
        IBordersRepository _repository = new BordersRepository();
        BordersService _service = new BordersService(_repository);
        return _userBordersRepository.InsertUserBordersById(Id, _service.GetBordersById(Id));
    }

    public Borders GetBordersByUsed(string user_id)
    {
        return _userBordersRepository.GetBordersByUsed(user_id);
    }

    public void UpdateIsUsedBorders(string Id, bool is_used)
    {
        _userBordersRepository.UpdateIsUsedBorders(Id, is_used);
    }

    public Borders SumPowerUserBorders()
    {
        return _userBordersRepository.SumPowerUserBorders();
    }
}

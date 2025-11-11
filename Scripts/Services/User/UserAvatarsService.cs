using System.Collections.Generic;

public class UserAvatarsService : IUserAvatarsService
{
    private readonly IUserAvatarsRepository _userAvatarsRepository;

    public UserAvatarsService(IUserAvatarsRepository userAvatarsRepository)
    {
        _userAvatarsRepository = userAvatarsRepository;
    }

    public static UserAvatarsService Create()
    {
        return new UserAvatarsService(new UserAvatarsRepository());
    }

    public List<Achievements> GetUserAvatars(string user_id, int pageSize, int offset, string rare)
    {
        List<Achievements> list = _userAvatarsRepository.GetUserAvatars(user_id, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserMedalsCount(string user_id, string rare)
    {
        return _userAvatarsRepository.GetUserMedalsCount(user_id, rare);
    }

    public bool InsertUserAvatars(Achievements avatars)
    {
        return _userAvatarsRepository.InsertUserAvatars(avatars);
    }

    public bool InsertUserAvatarsById(string Id)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        return _userAvatarsRepository.InsertUserAvatarsById(Id, _service.GetAvatarsById(Id));
    }

    public Achievements GetAvatarsByUsed(string user_id)
    {
        return _userAvatarsRepository.GetAvatarsByUsed(user_id);
    }

    public void UpdateIsUsedAvatars(string Id, bool is_used)
    {
        _userAvatarsRepository.UpdateIsUsedAvatars(Id, is_used);
    }

    public Achievements SumPowerUserAvatars()
    {
        return _userAvatarsRepository.SumPowerUserAvatars();
    }
}
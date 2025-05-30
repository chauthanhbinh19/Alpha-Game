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

    public List<Avatars> GetUserAvatars(string user_id, int pageSize, int offset)
    {
        List<Avatars> list = _userAvatarsRepository.GetUserAvatars(user_id, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserMedalsCount(string user_id)
    {
        return _userAvatarsRepository.GetUserMedalsCount(user_id);
    }

    public bool InsertUserAvatars(Avatars avatars)
    {
        return _userAvatarsRepository.InsertUserAvatars(avatars);
    }

    public bool InsertUserAvatarsById(string Id)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        return _userAvatarsRepository.InsertUserAvatarsById(Id, _service.GetAvatarsById(Id));
    }

    public Avatars GetAvatarsByUsed(string user_id)
    {
        return _userAvatarsRepository.GetAvatarsByUsed(user_id);
    }

    public void UpdateIsUsedAvatars(string Id, bool is_used)
    {
        _userAvatarsRepository.UpdateIsUsedAvatars(Id, is_used);
    }

    public Avatars SumPowerUserAvatars()
    {
        return _userAvatarsRepository.SumPowerUserAvatars();
    }
}
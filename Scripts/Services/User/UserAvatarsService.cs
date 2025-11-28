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

    public List<Avatars> GetUserAvatars(string user_id, int pageSize, int offset, string rare)
    {
        List<Avatars> list = _userAvatarsRepository.GetUserAvatars(user_id, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserMedalsCount(string user_id, string rare)
    {
        return _userAvatarsRepository.GetUserMedalsCount(user_id, rare);
    }

    public bool InsertUserAvatars(Avatars avatars, string userId)
    {
        return _userAvatarsRepository.InsertUserAvatars(avatars, userId);
    }

    public bool InsertUserAvatarsById(string avatarId, string userId)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        return _userAvatarsRepository.InsertUserAvatarsById(_service.GetAvatarsById(avatarId), userId);
    }

    public Avatars GetAvatarsByUsed(string user_id)
    {
        return _userAvatarsRepository.GetAvatarsByUsed(user_id);
    }

    public void UpdateIsUsedAvatars(string avatarId, string userId, bool is_used)
    {
        _userAvatarsRepository.UpdateIsUsedAvatars(avatarId, userId, is_used);
    }

    public Avatars SumPowerUserAvatars()
    {
        return _userAvatarsRepository.SumPowerUserAvatars();
    }
}
using System.Collections.Generic;

public interface IUserAvatarsRepository
{
    List<Achievements> GetUserAvatars(string user_id, int pageSize, int offset, string rare);
    int GetUserMedalsCount(string user_id, string rare);
    bool InsertUserAvatars(Achievements avatars);
    bool InsertUserAvatarsById(string Id, Achievements Avatars);
    Achievements GetAvatarsByUsed(string user_id);
    void UpdateIsUsedAvatars(string Id, bool is_used);
    Achievements SumPowerUserAvatars();
}
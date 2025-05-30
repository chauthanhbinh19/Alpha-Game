using System.Collections.Generic;

public interface IUserAvatarsService
{
    List<Avatars> GetUserAvatars(string user_id, int pageSize, int offset);
    int GetUserMedalsCount(string user_id);
    bool InsertUserAvatars(Avatars avatars);
    bool InsertUserAvatarsById(string Id);
    Avatars GetAvatarsByUsed(string user_id);
    void UpdateIsUsedAvatars(string Id, bool is_used);
    Avatars SumPowerUserAvatars();
}
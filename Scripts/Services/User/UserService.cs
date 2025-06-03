using System.Collections.Generic;
using UnityEngine;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public static UserService Create()
    {
        return new UserService(new UserRepository());
    }

    public string RegisterUser(string username, string password)
    {
        string userId = _userRepository.RegisterUser(username, password);
        createUserCurrency(userId);
        User.CurrentUserId = userId;

        UserBordersService.Create().InsertUserBordersById("359");
        BordersGalleryService.Create().InsertBordersGallery("359");
        UserBordersService.Create().UpdateIsUsedBorders("359", true);

        UserAvatarsService.Create().InsertUserAvatarsById("1");
        AvatarsGalleryService.Create().InsertAvatarsGallery("1");
        UserAvatarsService.Create().UpdateIsUsedAvatars("1", true);

        PowerManagerService.Create().InsertUserStats(userId);

        TeamsService.Create().InsertUserTeams(userId);
        return userId;
    }

    public User SignInUser(string username, string password)
    {
        User user = _userRepository.SignInUser(username, password);

        Borders borders = UserBordersService.Create().GetBordersByUsed(user.id);
        string Border = borders.image;

        Avatars avatar = UserAvatarsService.Create().GetAvatarsByUsed(user.id);
        string Image = avatar.image;

        User.CurrentUserAvatar = Image;
        User.CurrentUserBorder = Border;

        user.image = Image;
        user.border = Border;
        return user;

    }

    public User GetUserById(string Id)
    {
        User user = _userRepository.GetUserById(Id);

        Borders borders = UserBordersService.Create().GetBordersByUsed(user.id);
        string Border = borders.image;

        Avatars avatar = UserAvatarsService.Create().GetAvatarsByUsed(user.id);
        string Image = avatar.image;

        User.CurrentUserAvatar = Image;
        User.CurrentUserBorder = Border;

        user.image = Image;
        user.border = Border;

        return user;
    }

    public void UpdateUserName(string user_id, string new_name)
    {
        _userRepository.UpdateUserName(user_id, new_name);
    }

    public void createUserCurrency(string Id)
    {
        _userRepository.createUserCurrency(Id);
    }
}

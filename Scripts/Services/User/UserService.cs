using System;
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

        Items cardHeroesTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardHeroesTicket);
        UserItemsService.Create().InsertUserItems(cardHeroesTicket, 1000000);
        Items cardCaptainsTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardCaptainsTicket);
        UserItemsService.Create().InsertUserItems(cardCaptainsTicket, 1000000);
        Items cardMilitaryTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardMilitaryTicket);
        UserItemsService.Create().InsertUserItems(cardMilitaryTicket, 1000000);
        Items cardSpellTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardSpellTicket);
        UserItemsService.Create().InsertUserItems(cardSpellTicket, 1000000);
        Items cardMonstersTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardMonstersTicket);
        UserItemsService.Create().InsertUserItems(cardMonstersTicket, 1000000);
        Items cardColonelsTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardColonelsTicket);
        UserItemsService.Create().InsertUserItems(cardColonelsTicket, 1000000);
        Items cardGeneralsTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardGeneralsTicket);
        UserItemsService.Create().InsertUserItems(cardGeneralsTicket, 1000000);
        Items cardAdmiralsTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardAdmiralsTicket);
        UserItemsService.Create().InsertUserItems(cardAdmiralsTicket, 1000000);
        return userId;
    }

    public AuthResult SignInUser(string username, string password)
    {
        User user = _userRepository.GetUserByUsername(username);

        if (user != null)
        {
            if (!user.Password.Equals(password))
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorField = AppConstants.Password,
                    ErrorMessage = MessageConstants.IncorrectPassword,
                    User = null
                };
            }

            user = _userRepository.SignInUser(username, password);
            Borders borders = UserBordersService.Create().GetBordersByUsed(user.id);
            string Border = borders.image;

            Avatars avatar = UserAvatarsService.Create().GetAvatarsByUsed(user.id);
            string Image = avatar.image;

            User.CurrentUserAvatar = Image;
            User.CurrentUserBorder = Border;

            user.image = Image;
            user.border = Border;

            DateTime now = DateTime.Now;
            int year = now.Year;
            int month = now.Month;
            if (!UserDailyCheckinService.Create().CheckUserDailyCheckinStatus(User.CurrentUserId, month, year))
            {
                int daysInMonth = DateTime.DaysInMonth(year, month);
                for (int day = 1; day <= daysInMonth; day++)
                {
                    DateTime currentDate = new DateTime(year, month, day);
                    UserDailyCheckinService.Create().DeleteUserDailyCheckin(User.CurrentUserId, day.ToString());
                    UserDailyCheckin userDailyCheckin = new UserDailyCheckin
                    {
                        user_id = User.CurrentUserId,
                        daily_checkin_id = day.ToString(),
                        status = false,
                        day = currentDate,
                        month = month,
                        year = year
                    };
                    UserDailyCheckinService.Create().InsertUserDailyCheckin(User.CurrentUserId, userDailyCheckin);
                }
            }

            return new AuthResult
            {
                Success = true,
                ErrorField = "",
                ErrorMessage = "",
                User = user
            };
        }
        else
        {
            return new AuthResult
            {
                Success = false,
                ErrorField = AppConstants.Username,
                ErrorMessage = MessageConstants.UsernameDoesNotExist,
                User = null
            };
        }
        // Items cardHeroesTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardHeroesTicket);
        // UserItemsService.Create().InsertUserItems(cardHeroesTicket, 1000000);
        // Items cardCaptainsTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardCaptainsTicket);
        // UserItemsService.Create().InsertUserItems(cardCaptainsTicket, 1000000);
        // Items cardMilitaryTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardMilitaryTicket);
        // UserItemsService.Create().InsertUserItems(cardMilitaryTicket, 1000000);
        // Items cardSpellTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardSpellTicket);
        // UserItemsService.Create().InsertUserItems(cardSpellTicket, 1000000);
        // Items cardMonstersTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardMonstersTicket);
        // UserItemsService.Create().InsertUserItems(cardMonstersTicket, 1000000);
        // Items cardColonelsTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardColonelsTicket);
        // UserItemsService.Create().InsertUserItems(cardColonelsTicket, 1000000);
        // Items cardGeneralsTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardGeneralsTicket);
        // UserItemsService.Create().InsertUserItems(cardGeneralsTicket, 1000000);
        // Items cardAdmiralsTicket = UserItemsService.Create().GetUserItemByName(ItemConstants.CardAdmiralsTicket);
        // UserItemsService.Create().InsertUserItems(cardAdmiralsTicket, 1000000);
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

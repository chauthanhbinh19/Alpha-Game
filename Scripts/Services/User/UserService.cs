using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UserService : IUserService
{
     private static UserService _instance;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public static UserService Create()
    {
        if (_instance == null)
        {
            _instance = new UserService(new UserRepository());
        }
        return _instance;
    }

    public async Task<string> RegisterUserAsync(string username, string password)
    {
        string userId = await _userRepository.RegisterUserAsync(username, password);
        if (!String.IsNullOrEmpty(userId))
        {
            await CreateUserCurrencyAsync(userId);
            User.CurrentUserId = userId;

            await UserBordersService.Create().InsertUserBorderByIdAsync("359", userId);
            await BordersGalleryService.Create().InsertBorderGalleryAsync("359");
            await UserBordersService.Create().UpdateIsUsedBorderAsync("359", userId, true);

            await UserAvatarsService.Create().InsertUserAvatarByIdAsync("1", userId);
            await AvatarsGalleryService.Create().InsertAvatarGalleryAsync("1");
            await UserAvatarsService.Create().UpdateIsUsedAvatarAsync("1", userId, true);

            await PowerManagerService.Create().InsertUserStatsAsync(userId);

            Items cardHeroesTicket = await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_HERO_TICKET);
            await UserItemsService.Create().InsertUserItemAsync(cardHeroesTicket, 1000000);
            Items cardCaptainsTicket = await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_CAPTAIN_TICKET);
            await UserItemsService.Create().InsertUserItemAsync(cardCaptainsTicket, 1000000);
            Items cardMilitaryTicket = await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_MILITARY_TICKET);
            await UserItemsService.Create().InsertUserItemAsync(cardMilitaryTicket, 1000000);
            Items cardSpellTicket = await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_SPELL_TICKET);
            await UserItemsService.Create().InsertUserItemAsync(cardSpellTicket, 1000000);
            Items cardMonstersTicket = await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_MONSTER_TICKET);
            await UserItemsService.Create().InsertUserItemAsync(cardMonstersTicket, 1000000);
            Items cardColonelsTicket = await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_COLONEL_TICKET);
            await UserItemsService.Create().InsertUserItemAsync(cardColonelsTicket, 1000000);
            Items cardGeneralsTicket = await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_GENERAL_TICKET);
            await UserItemsService.Create().InsertUserItemAsync(cardGeneralsTicket, 1000000);
            Items cardAdmiralsTicket = await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.CARD_ADMIRAL_TICKET);
            await UserItemsService.Create().InsertUserItemAsync(cardAdmiralsTicket, 1000000);

            for (int i = 0; i < 50; i++)
            {
                await TeamsService.Create().InsertUserTeamsAsync(userId, i + 1);
            }
            await UserSettingsService.Create().CreateInitiateUserSettingsAsync(userId);
        }
        return userId;
    }

    public async Task<AuthResult> SignInWithUsernameAndPasswordAsync(string username, string password)
    {
        User user = await _userRepository.GetUserByUsernameAsync(username);

        if (user != null)
        {
            if (!user.Password.Equals(password))
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorField = AppConstants.MainType.PASSWORD,
                    ErrorMessage = MessageConstants.INCORRECT_PASSWORD,
                    User = null
                };
            }

            user = await _userRepository.SignInWithUsernameAndPasswordAsync(username, password);
            AuthManager.SaveUserId(user.Id);
            Borders borders = await UserBordersService.Create().GetBorderByUsedAsync(user.Id);
            string Border = borders.Image;

            Avatars avatar = await UserAvatarsService.Create().GetAvatarByUsedAsync(user.Id);
            string Image = avatar.Image;

            User.CurrentUserAvatar = Image;
            User.CurrentUserBorder = Border;

            user.Image = Image;
            user.Border = Border;

            DateTime now = DateTime.Now;
            int year = now.Year;
            int month = now.Month;
            if (!await UserDailyCheckinService.Create().CheckUserDailyCheckinStatusAsync(User.CurrentUserId, month, year))
            {
                int daysInMonth = DateTime.DaysInMonth(year, month);
                for (int day = 1; day <= daysInMonth; day++)
                {
                    DateTime currentDate = new DateTime(year, month, day);
                    await UserDailyCheckinService.Create().DeleteUserDailyCheckinAsync(User.CurrentUserId, day.ToString());
                    UserDailyCheckin userDailyCheckin = new UserDailyCheckin
                    {
                        UserId = User.CurrentUserId,
                        DailyCheckinId = day.ToString(),
                        Status = false,
                        Day = currentDate,
                        Month = month,
                        Year = year
                    };
                    await UserDailyCheckinService.Create().InsertUserDailyCheckinAsync(User.CurrentUserId, userDailyCheckin);
                }
            }

            var settingList = await UserSettingsService.Create().GetUserSettingsAsync(User.CurrentUserId);
            UserSettingsManager.Instance.LoadUserSettings(settingList);

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
                ErrorField = AppConstants.MainType.USERNAME,
                ErrorMessage = MessageConstants.USERNAME_DOES_NOT_EXIST,
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

    public async Task<AuthResult> SignInWithoutUsernameAndPasswordAsync(string userId)
    {
        User user = await _userRepository.SignInWithoutUsernameAndPasswordAsync(userId);

        if (user != null)
        {
            Borders borders = await UserBordersService.Create().GetBorderByUsedAsync(user.Id);
            string Border = borders.Image;

            Avatars avatar = await UserAvatarsService.Create().GetAvatarByUsedAsync(user.Id);
            string Image = avatar.Image;

            User.CurrentUserAvatar = Image;
            User.CurrentUserBorder = Border;

            user.Image = Image;
            user.Border = Border;

            DateTime now = DateTime.Now;
            int year = now.Year;
            int month = now.Month;
            if (!await UserDailyCheckinService.Create().CheckUserDailyCheckinStatusAsync(User.CurrentUserId, month, year))
            {
                int daysInMonth = DateTime.DaysInMonth(year, month);
                for (int day = 1; day <= daysInMonth; day++)
                {
                    DateTime currentDate = new DateTime(year, month, day);
                    await UserDailyCheckinService.Create().DeleteUserDailyCheckinAsync(User.CurrentUserId, day.ToString());
                    UserDailyCheckin userDailyCheckin = new UserDailyCheckin
                    {
                        UserId = User.CurrentUserId,
                        DailyCheckinId = day.ToString(),
                        Status = false,
                        Day = currentDate,
                        Month = month,
                        Year = year
                    };
                    await UserDailyCheckinService.Create().InsertUserDailyCheckinAsync(User.CurrentUserId, userDailyCheckin);
                }
            }

            var settingList = await UserSettingsService.Create().GetUserSettingsAsync(User.CurrentUserId);
            UserSettingsManager.Instance.LoadUserSettings(settingList);

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
                ErrorField = AppConstants.MainType.USERNAME,
                ErrorMessage = MessageConstants.USERNAME_DOES_NOT_EXIST,
                User = null
            };
        }
    }

    public async Task<User> GetUserByIdAsync(string Id)
    {
        User user = await _userRepository.GetUserByIdAsync(Id);

        Borders borders = await UserBordersService.Create().GetBorderByUsedAsync(user.Id);
        string Border = borders.Image;

        Avatars avatar = await UserAvatarsService.Create().GetAvatarByUsedAsync(user.Id);
        string Image = avatar.Image;

        User.CurrentUserAvatar = Image;
        User.CurrentUserBorder = Border;

        user.Image = Image;
        user.Border = Border;

        return user;
    }

    public async Task UpdateUserNameAsync(string user_id, string new_name)
    {
        await _userRepository.UpdateUserNameAsync(user_id, new_name);
        User.CurrentUserName = new_name;
    }

    public async Task UpdateUserPowerAsync(string user_id, double power)
    {
        await _userRepository.UpdateUserPowerAsync(user_id, power);
    }

    public async Task CreateUserCurrencyAsync(string Id)
    {
        await _userRepository.CreateUserCurrencyAsync(Id);
    }

    public async Task<bool> CheckNameExistsAsync(string name)
    {
        return await _userRepository.CheckNameExistsAsync(name);
    }
}

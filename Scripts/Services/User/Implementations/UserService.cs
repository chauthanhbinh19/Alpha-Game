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

    private async Task GiveDefaultTicketsAsync(string userId)
    {
        // Danh sách các loại vé cấp cho tài khoản mới
        string[] ticketTypes = new string[]
        {
        ItemConstants.Ticket.CARD_HERO_TICKET,
        ItemConstants.Ticket.CARD_CAPTAIN_TICKET,
        ItemConstants.Ticket.CARD_MILITARY_TICKET,
        ItemConstants.Ticket.CARD_SPELL_TICKET,
        ItemConstants.Ticket.CARD_MONSTER_TICKET,
        ItemConstants.Ticket.CARD_COLONEL_TICKET,
        ItemConstants.Ticket.CARD_GENERAL_TICKET,
        ItemConstants.Ticket.CARD_ADMIRAL_TICKET
        };

        foreach (var ticketName in ticketTypes)
        {
            try
            {
                Items ticket = await UserItemsService.Create().GetUserItemByNameAsync(userId, ticketName);
                if (ticket != null)
                {
                    await UserItemsService.Create().InsertUserItemAsync(userId, ticket, 1000000);
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[GiveDefaultTickets] Lỗi cấp vé {ticketName}: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Đăng ký tài khoản mới và khởi tạo dữ liệu Tân thủ
    /// </summary>
    public async Task<AuthResult> RegisterUserAsync(string username, string email, string password)
    {
        try
        {
            // 1. Thực hiện Đăng ký tài khoản ở Repository
            AuthResult registerResult = await _userRepository.RegisterUserAsync(username, email, password);

            // Nếu trùng username / email hoặc có lỗi DB -> Trả AuthResult lỗi về ngay cho UI
            if (!registerResult.Success)
            {
                return registerResult;
            }

            string userId = registerResult.User.Id;

            // 2. Đăng ký DB thành công -> Khởi tạo các dữ liệu Tân thủ
            await UserCurrenciesService.Create().InitiateUserCurrencyAsync(userId);

            // Border mặc định
            await UserBordersService.Create().InsertUserBorderByIdAsync("BD359", userId);
            await BordersGalleryService.Create().InsertBorderGalleryAsync(userId, "BD359");
            await UserBordersService.Create().UpdateIsUsedUserBorderAsync("BD359", userId, true);

            // Avatar mặc định
            await UserAvatarsService.Create().InsertUserAvatarByIdAsync("AT1", userId);
            await AvatarsGalleryService.Create().InsertAvatarGalleryAsync(userId, "AT1");
            await UserAvatarsService.Create().UpdateIsUsedUserAvatarAsync("AT1", userId, true);

            // Stats & Settings
            await PowerManagerService.Create().InsertUserStatsAsync(userId);
            await UserSettingsService.Create().CreateInitiateUserSettingsAsync(userId);

            // Khởi tạo vé mặc định
            await GiveDefaultTicketsAsync(userId);

            // Trả về AuthResult thành công cùng thông tin User
            return registerResult;
        }
        catch (Exception ex)
        {
            Debug.LogError($"[UserService.RegisterUserAsync Error]: {ex.Message}\n{ex.StackTrace}");
            return new AuthResult
            {
                Success = false,
                ErrorField = "",
                ErrorMessage = "Đăng ký không thành công. Lỗi hệ thống!",
                User = null
            };
        }
    }

    /// <summary>
    /// Đăng nhập bằng Username & Password
    /// </summary>
    public async Task<AuthResult> SignInWithUsernameAndPasswordAsync(string username, string password)
    {
        try
        {
            // 1. Kiểm tra Username có tồn tại không
            User userCheck = await _userRepository.GetUserByUsernameAsync(username);
            if (userCheck == null)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorField = AppConstants.MainType.USERNAME,
                    ErrorMessage = MessageConstants.USERNAME_DOES_NOT_EXIST,
                    User = null
                };
            }

            // 2. Kiểm tra Mật khẩu
            if (!userCheck.Password.Equals(password))
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorField = AppConstants.MainType.PASSWORD,
                    ErrorMessage = MessageConstants.INCORRECT_PASSWORD,
                    User = null
                };
            }

            // 3. Thực hiện Lấy thông tin User từ DB
            User user = await _userRepository.SignInWithUsernameAndPasswordAsync(username, password);
            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorField = "",
                    ErrorMessage = "Đăng nhập thất bại. Vui lòng thử lại!",
                    User = null
                };
            }

            // 4. Load thông tin phụ (Avatar, Border, Daily Checkin, Settings)
            await LoadUserAdditionalDataAsync(user);

            // Lưu Session UserId
            AuthManager.SaveUserId(user.Id);

            return new AuthResult
            {
                Success = true,
                ErrorField = "",
                ErrorMessage = "",
                User = user
            };
        }
        catch (Exception ex)
        {
            Debug.LogError($"[UserService.SignInWithUsernameAndPasswordAsync Error]: {ex.Message}");
            return new AuthResult
            {
                Success = false,
                ErrorField = "",
                ErrorMessage = "Lỗi kết nối máy chủ!",
                User = null
            };
        }
    }

    /// <summary>
    /// Tự động Đăng nhập bằng UserId (Token/Session)
    /// </summary>
    public async Task<AuthResult> SignInWithoutUsernameAndPasswordAsync(string userId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorField = "",
                    ErrorMessage = "Thiếu thông tin ID người dùng!",
                    User = null
                };
            }

            // 1. Lấy thông tin User từ DB
            User user = await _userRepository.SignInWithoutUsernameAndPasswordAsync(userId);
            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorField = AppConstants.MainType.USERNAME,
                    ErrorMessage = MessageConstants.USERNAME_DOES_NOT_EXIST,
                    User = null
                };
            }

            // 2. Load thông tin phụ
            await LoadUserAdditionalDataAsync(user);

            return new AuthResult
            {
                Success = true,
                ErrorField = "",
                ErrorMessage = "",
                User = user
            };
        }
        catch (Exception ex)
        {
            Debug.LogError($"[UserService.SignInWithoutUsernameAndPasswordAsync Error]: {ex.Message}");
            return new AuthResult
            {
                Success = false,
                ErrorField = "",
                ErrorMessage = "Lỗi tự động đăng nhập!",
                User = null
            };
        }
    }

    #region Helper Methods

    /// <summary>
    /// Load các thông tin phụ như Avatar, Khung ảnh, Tiền tệ, Daily Checkin, Settings...
    /// </summary>
    private async Task LoadUserAdditionalDataAsync(User user)
    {
        // 1. Load Border & Avatar
        try
        {
            Borders border = await UserBordersService.Create().GetUserBorderByUsedAsync(user.Id);
            string borderImagePath = border != null ? border.Image : "";

            Avatars avatar = await UserAvatarsService.Create().GetUserAvatarByUsedAsync(user.Id);
            string avatarImagePath = avatar != null ? avatar.Image : "";

            User.CurrentUserAvatar = avatarImagePath;
            User.CurrentUserBorder = borderImagePath;
            user.Image = avatarImagePath;
            user.Border = borderImagePath;
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[LoadUserAdditionalData] Lỗi load Avatar/Border: {ex.Message}");
        }

        // 2. Load Currencies
        try
        {
            List<Currencies> currencies = await UserCurrenciesService.Create().GetUserCurrencyAsync(user.Id);
            user.Currencies = currencies ?? new List<Currencies>();
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[LoadUserAdditionalData] Lỗi load Currencies: {ex.Message}");
        }

        // 3. Xử lý Daily Checkin Tháng mới
        try
        {
            DateTime now = DateTime.Now;
            int year = now.Year;
            int month = now.Month;

            bool isCheckinInit = await UserDailyCheckinService.Create().CheckUserDailyCheckinStatusAsync(user.Id, month, year);
            if (!isCheckinInit)
            {
                int daysInMonth = DateTime.DaysInMonth(year, month);
                for (int day = 1; day <= daysInMonth; day++)
                {
                    DateTime currentDate = new DateTime(year, month, day);
                    await UserDailyCheckinService.Create().DeleteUserDailyCheckinAsync(user.Id, day.ToString());

                    UserDailyCheckin userDailyCheckin = new UserDailyCheckin
                    {
                        UserId = user.Id,
                        DailyCheckinId = day.ToString(),
                        Status = false,
                        Day = currentDate,
                        Month = month,
                        Year = year
                    };
                    await UserDailyCheckinService.Create().InsertUserDailyCheckinAsync(user.Id, userDailyCheckin);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[LoadUserAdditionalData] Lỗi khởi tạo Checkin: {ex.Message}");
        }

        // 4. Load User Settings
        try
        {
            var settingList = await UserSettingsService.Create().GetUserSettingsAsync(user.Id);
            if (settingList != null && UserSettingsManager.Instance != null)
            {
                UserSettingsManager.Instance.LoadUserSettings(settingList);
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"[LoadUserAdditionalData] Lỗi load Settings: {ex.Message}");
        }
    }

    #endregion

    public async Task<User> GetUserByIdAsync(string Id)
    {
        User user = await _userRepository.GetUserByIdAsync(Id);

        Borders border = await UserBordersService.Create().GetUserBorderByUsedAsync(user.Id);
        string borderImagePath = border.Image;

        Avatars avatar = await UserAvatarsService.Create().GetUserAvatarByUsedAsync(user.Id);
        string avatarImagePath = avatar.Image;

        User.CurrentUserAvatar = avatarImagePath;
        User.CurrentUserBorder = borderImagePath;

        user.Image = avatarImagePath;
        user.Border = borderImagePath;

        return user;
    }

    public async Task UpdateUserNameAsync(string userId, string new_name)
    {
        await _userRepository.UpdateUserNameAsync(userId, new_name);
        User.CurrentUserName = new_name;
    }

    public async Task UpdateUserPowerAsync(string userId, double power)
    {
        await _userRepository.UpdateUserPowerAsync(userId, power);
    }

    public async Task CreateUserCurrencyAsync(string userId)
    {
        await _userRepository.CreateUserCurrencyAsync(userId);
    }

    public async Task<bool> CheckNameExistsAsync(string name)
    {
        return await _userRepository.CheckNameExistsAsync(name);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserAvatarsService _userAvatarsService;
    private readonly IUserBordersService _userBordersService;
    private readonly IAvatarsGalleryService _avatarsGalleryService;
    private readonly IBordersGalleryService _bordersGalleryService;
    private readonly IUserCurrenciesService _userCurrenciesService;
    private readonly IUserItemsService _userItemsService;
    private readonly IPowerManagerService _powerManagerService;
    private readonly IUserSettingsService _userSettingsService;
    private readonly IUserDailyCheckinService _userDailyCheckinService;

    public UserService(
    IUserRepository userRepository,
    IUserAvatarsService userAvatarsService,
    IUserBordersService userBordersService,
    IAvatarsGalleryService avatarsGalleryService,
    IBordersGalleryService bordersGalleryService,
    IUserCurrenciesService userCurrenciesService,
    IUserItemsService userItemsService,
    IPowerManagerService powerManagerService,
    IUserSettingsService userSettingsService,
    IUserDailyCheckinService userDailyCheckinService
)
    {
        _userRepository = userRepository;
        _userAvatarsService = userAvatarsService;
        _userBordersService = userBordersService;
        _avatarsGalleryService = avatarsGalleryService;
        _bordersGalleryService = bordersGalleryService;
        _userCurrenciesService = userCurrenciesService;
        _userItemsService = userItemsService;
        _powerManagerService = powerManagerService;
        _userSettingsService = userSettingsService;
        _userDailyCheckinService = userDailyCheckinService;
    }

    public static IUserService Create() => ServiceContainer.GetService<IUserService>();

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
                Items ticket = await _userItemsService.GetUserItemByNameAsync(userId, ticketName);
                if (ticket != null)
                {
                    await _userItemsService.InsertUserItemAsync(userId, ticket, 1000000);
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
            string hashedPassword = PasswordHasher.HashPassword(password);

            // 1. Thực hiện Đăng ký tài khoản ở Repository
            AuthResult registerResult = await _userRepository.RegisterUserAsync(username, email, hashedPassword);

            // Nếu trùng username / email hoặc có lỗi DB -> Trả AuthResult lỗi về ngay cho UI
            if (!registerResult.Success)
            {
                return registerResult;
            }

            string userId = registerResult.User.Id;

            // 2. Đăng ký DB thành công -> Khởi tạo các dữ liệu Tân thủ
            await _userCurrenciesService.InitiateUserCurrencyAsync(userId);

            // Border mặc định
            await _userBordersService.InsertUserBorderByIdAsync("BD359", userId);
            await _bordersGalleryService.InsertBorderGalleryAsync(userId, "BD359");
            await _userBordersService.UpdateIsUsedUserBorderAsync("BD359", userId, true);

            // Avatar mặc định
            await _userAvatarsService.InsertUserAvatarByIdAsync("AT1", userId);
            await _avatarsGalleryService.InsertAvatarGalleryAsync(userId, "AT1");
            await _userAvatarsService.UpdateIsUsedUserAvatarAsync("AT1", userId, true);

            // Stats & Settings
            await _powerManagerService.InsertUserStatsAsync(userId);
            await _userSettingsService.CreateInitiateUserSettingsAsync(userId);

            // Khởi tạo vé mặc định
            // await GiveDefaultTicketsAsync(userId);

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

            // 2. Kiểm tra Mật khẩu đã hash
            if (!PasswordHasher.VerifyPassword(password, userCheck.Password))
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
            User user = await _userRepository.SignInWithUsernameAndPasswordAsync(username, userCheck.Password);
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

            string token = AuthManager.CreateJwtFromUserId(user.Id);
            if (!string.IsNullOrWhiteSpace(token))
            {
                AuthManager.SaveToken(token);
            }
            else
            {
                AuthManager.SaveUserId(user.Id);
            }

            User.CurrentUserId = user.Id;

            return new AuthResult
            {
                Success = true,
                ErrorField = "",
                ErrorMessage = "",
                User = user,
                Token = token
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
            // await LoadUserAdditionalDataAsync(user);

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
            Borders border = await _userBordersService.GetUserBorderByUsedAsync(user.Id);
            string borderImagePath = border != null ? border.Image : "";

            Avatars avatar = await _userAvatarsService.GetUserAvatarByUsedAsync(user.Id);
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
            List<Currencies> currencies = await _userCurrenciesService.GetUserCurrencyAsync(user.Id);
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

            bool isCheckinInit = await _userDailyCheckinService.CheckUserDailyCheckinStatusAsync(user.Id, month, year);
            if (!isCheckinInit)
            {
                int daysInMonth = DateTime.DaysInMonth(year, month);
                for (int day = 1; day <= daysInMonth; day++)
                {
                    DateTime currentDate = new DateTime(year, month, day);
                    await _userDailyCheckinService.DeleteUserDailyCheckinAsync(user.Id, day.ToString());

                    UserDailyCheckin userDailyCheckin = new UserDailyCheckin
                    {
                        UserId = user.Id,
                        DailyCheckinId = day.ToString(),
                        Status = false,
                        Day = currentDate,
                        Month = month,
                        Year = year
                    };
                    await _userDailyCheckinService.InsertUserDailyCheckinAsync(user.Id, userDailyCheckin);
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
            var settingList = await _userSettingsService.GetUserSettingsAsync(user.Id);
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

        Borders border = await _userBordersService.GetUserBorderByUsedAsync(user.Id);
        string borderImagePath = border.Image;

        Avatars avatar = await _userAvatarsService.GetUserAvatarByUsedAsync(user.Id);
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

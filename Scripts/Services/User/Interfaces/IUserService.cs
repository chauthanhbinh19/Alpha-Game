using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{ 
    // Task<User> GetUserByUsername(string username);
    Task<AuthResult> RegisterUserAsync(string username, string email, string password);
    Task<AuthResult> SignInWithUsernameAndPasswordAsync(string username, string password);
    Task<AuthResult> SignInWithoutUsernameAndPasswordAsync(string userId);
    Task<User> GetUserByIdAsync(string Id);
    Task UpdateUserNameAsync(string userId, string new_name);
    Task UpdateUserPowerAsync(string userId, double power);
    Task CreateUserCurrencyAsync(string Id);
    Task<bool> CheckNameExistsAsync(string name);
}
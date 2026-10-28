using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserPowersService
{ 
    Task<UserPowers> GetSumUserPowersAsync(string userId);
}
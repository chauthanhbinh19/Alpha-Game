using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserPowersRepository
{
    Task<UserPowers> GetSumUserPowersAsync(string userId);
}
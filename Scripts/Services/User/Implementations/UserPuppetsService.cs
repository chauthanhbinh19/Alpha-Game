using System.Collections.Generic;
using System.Threading.Tasks;

public class UserPuppetsService : IUserPuppetsService
{
    private static UserPuppetsService _instance;
    private readonly IUserPuppetsRepository _userPuppetsRepository;

    public UserPuppetsService(IUserPuppetsRepository userPuppetsRepository)
    {
        _userPuppetsRepository = userPuppetsRepository;
    }

    public static UserPuppetsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserPuppetsService(new UserPuppetsRepository());
        }
        return _instance;
    }




    public async Task<List<Puppets>> GetUserPuppetsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = await _userPuppetsRepository.GetUserPuppetsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserPuppetsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userPuppetsRepository.GetUserPuppetsCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserPuppetAsync(Puppets puppet, string userId)
    {
        return await _userPuppetsRepository.InsertUserPuppetAsync(puppet, userId);
    }

    public async Task<bool> UpdateUserPuppetLevelAsync(string userId, Puppets puppet)
    {
        return await _userPuppetsRepository.UpdateUserPuppetLevelAsync(userId, puppet);
    }

    public async Task<bool> UpdateUserPuppetStarAsync(string userId, Puppets puppet)
    {
        return await _userPuppetsRepository.UpdateUserPuppetStarAsync(userId, puppet);
    }

    public async Task<bool> UpdatePuppetBreakthroughAsync(string userId, Puppets puppet, int star, double quantity)
    {
        return await _userPuppetsRepository.UpdateUserPuppetBreakthroughAsync(userId, puppet, star, quantity);
    }

    public async Task<Puppets> GetUserPuppetByIdAsync(string userId, string Id)
    {
        return await _userPuppetsRepository.GetUserPuppetByIdAsync(userId, Id);
    }

    public async Task<Puppets> SumPowerUserPuppetsAsync(string userId)
    {
        return await _userPuppetsRepository.SumPowerUserPuppetsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserPuppetsBatchAsync(string userId, List<Puppets> puppets)
    {
        return await _userPuppetsRepository.InsertOrUpdateUserPuppetsBatchAsync(userId, puppets);
    }
}

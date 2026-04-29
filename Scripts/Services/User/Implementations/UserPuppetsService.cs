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

    
    

    public async Task<List<Puppets>> GetUserPuppetsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Puppets> list = await _userPuppetsRepository.GetUserPuppetsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserPuppetsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userPuppetsRepository.GetUserPuppetsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserPuppetAsync(Puppets puppet, string userId)
    {
        return await _userPuppetsRepository.InsertUserPuppetAsync(puppet, userId);
    }

    public async Task<bool> UpdatePuppetLevelAsync(Puppets puppet, int level)
    {
        return await _userPuppetsRepository.UpdatePuppetLevelAsync(puppet, level);
    }

    public async Task<bool> UpdatePuppetBreakthroughAsync(Puppets puppet, int star, double quantity)
    {
        return await _userPuppetsRepository.UpdatePuppetBreakthroughAsync(puppet, star, quantity);
    }

    public async Task<Puppets> GetUserPuppetByIdAsync(string user_id, string Id)
    {
        return await _userPuppetsRepository.GetUserPuppetByIdAsync(user_id, Id);
    }

    public async Task<Puppets> SumPowerUserPuppetsAsync()
    {
        return await _userPuppetsRepository.SumPowerUserPuppetsAsync();
    }

    public async Task<bool> InsertOrUpdateUserPuppetsBatchAsync(List<Puppets> puppets)
    {
        return await _userPuppetsRepository.InsertOrUpdateUserPuppetsBatchAsync(puppets);
    }
}

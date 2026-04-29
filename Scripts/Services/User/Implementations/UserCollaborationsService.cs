using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCollaborationsService : IUserCollaborationsService
{
     private static UserCollaborationsService _instance;
    private readonly IUserCollaborationsRepository _userCollaborationsRepository;

    public UserCollaborationsService(IUserCollaborationsRepository userCollaborationsRepository)
    {
        _userCollaborationsRepository = userCollaborationsRepository;
    }

    public static UserCollaborationsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCollaborationsService(new UserCollaborationsRepository());
        }
        return _instance;
    }

    public async Task<List<Collaborations>> GetUserCollaborationsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Collaborations> list = await _userCollaborationsRepository.GetUserCollaborationsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCollaborationsCountAsync(string user_id, string search, string rare)
    {
        return await _userCollaborationsRepository.GetUserCollaborationsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserCollaborationAsync(Collaborations collaboration, string userId)
    {
        return await _userCollaborationsRepository.InsertUserCollaborationAsync(collaboration, userId);
    }

    public async Task<bool> UpdateCollaborationLevelAsync(Collaborations collaboration, int level)
    {
        return await _userCollaborationsRepository.UpdateCollaborationLevelAsync(collaboration, level);
    }

    public async Task<bool> UpdateCollaborationBreakthroughAsync(Collaborations collaboration, int star, double quantity)
    {
        return await _userCollaborationsRepository.UpdateCollaborationBreakthroughAsync(collaboration, star, quantity);
    }

    public async Task<Collaborations> GetUserCollaborationByIdAsync(string user_id, string Id)
    {
        return await _userCollaborationsRepository.GetUserCollaborationByIdAsync(user_id, Id);
    }

    public async Task<Collaborations> SumPowerUserCollaborationsAsync()
    {
        return await _userCollaborationsRepository.SumPowerUserCollaborationsAsync();
    }

    public async Task<bool> InsertOrUpdateUserCollaborationsBatchAsync(List<Collaborations> collaborations)
    {
        return await _userCollaborationsRepository.InsertOrUpdateUserCollaborationsBatchAsync(collaborations);
    }
}

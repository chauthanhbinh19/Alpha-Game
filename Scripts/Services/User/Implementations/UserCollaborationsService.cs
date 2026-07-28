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

    public async Task<List<Collaborations>> GetUserCollaborationsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Collaborations> list = await _userCollaborationsRepository.GetUserCollaborationsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCollaborationsCountAsync(string userId, string search, string rare)
    {
        return await _userCollaborationsRepository.GetUserCollaborationsCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserCollaborationAsync(Collaborations collaboration, string userId)
    {
        var result = await _userCollaborationsRepository.InsertUserCollaborationAsync(collaboration, userId);
        if (result)
        {
            await CollaborationsGalleryService.Create().InsertCollaborationGalleryAsync(userId, collaboration.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserCollaborationLevelAsync(string userId, Collaborations collaboration)
    {
        return await _userCollaborationsRepository.UpdateUserCollaborationLevelAsync(userId, collaboration);
    }

    public async Task<bool> UpdateUserCollaborationStarAsync(string userId, Collaborations collaboration)
    {
        var result = await _userCollaborationsRepository.UpdateUserCollaborationStarAsync(userId, collaboration);
        if (result)
        {
            await CollaborationsGalleryService.Create().UpdateStarCollaborationGalleryAsync(userId, collaboration.Id, collaboration.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserCollaborationBreakthroughAsync(string userId, Collaborations collaboration, int star, double quantity)
    {
        return await _userCollaborationsRepository.UpdateUserCollaborationBreakthroughAsync(userId, collaboration, star, quantity);
    }

    public async Task<Collaborations> GetUserCollaborationByIdAsync(string userId, string Id)
    {
        return await _userCollaborationsRepository.GetUserCollaborationByIdAsync(userId, Id);
    }

    public async Task<Collaborations> SumPowerUserCollaborationsAsync(string userId)
    {
        return await _userCollaborationsRepository.SumPowerUserCollaborationsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserCollaborationsBatchAsync(string userId, List<Collaborations> collaborations)
    {
        return await _userCollaborationsRepository.InsertOrUpdateUserCollaborationsBatchAsync(userId, collaborations);
    }
}

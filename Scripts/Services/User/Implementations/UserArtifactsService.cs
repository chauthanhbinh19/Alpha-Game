using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArtifactsService : IUserArtifactsService
{
    private static UserArtifactsService _instance;
    private readonly IUserArtifactsRepository _userArtifactsRepository;

    public UserArtifactsService(IUserArtifactsRepository userArtifactsRepository)
    {
        _userArtifactsRepository = userArtifactsRepository;
    }

    public static UserArtifactsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserArtifactsService(new UserArtifactsRepository());
        }
        return _instance;
    }

    public async Task<List<Artifacts>> GetUserArtifactsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Artifacts> list = await _userArtifactsRepository.GetUserArtifactsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserArtifactsCountAsync(string userId, string search, string rare)
    {
        return await _userArtifactsRepository.GetUserArtifactsCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserArtifactAsync(Artifacts artifact, string userId)
    {
        return await _userArtifactsRepository.InsertUserArtifactAsync(artifact, userId);
    }

    public async Task<bool> UpdateUserArtifactLevelAsync(string userId, Artifacts artifact)
    {
        return await _userArtifactsRepository.UpdateUserArtifactLevelAsync(userId, artifact);
    }

    public async Task<bool> UpdateUserArtifactStarAsync(string userId, Artifacts artifact)
    {
        return await _userArtifactsRepository.UpdateUserArtifactStarAsync(userId, artifact);
    }

    public async Task<bool> UpdateUserArtifactBreakthroughAsync(string userId, Artifacts artifact, int star, double quantity)
    {
        return await _userArtifactsRepository.UpdateUserArtifactBreakthroughAsync(userId, artifact, star, quantity);
    }

    public async Task<Artifacts> GetUserArtifactByIdAsync(string userId, string Id)
    {
        return await _userArtifactsRepository.GetUserArtifactByIdAsync(userId, Id);
    }

    public async Task<Artifacts> SumPowerUserArtifactsAsync(string userId)
    {
        return await _userArtifactsRepository.SumPowerUserArtifactsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserArtifactsBatchAsync(string userId, List<Artifacts> artifacts)
    {
        return await _userArtifactsRepository.InsertOrUpdateUserArtifactsBatchAsync(userId, artifacts);
    }
}

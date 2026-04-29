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

    public async Task<List<Artifacts>> GetUserArtifactsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Artifacts> list = await _userArtifactsRepository.GetUserArtifactsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserArtifactsCountAsync(string user_id, string search, string rare)
    {
        return await _userArtifactsRepository.GetUserArtifactsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserArtifactAsync(Artifacts artifact, string userId)
    {
        return await _userArtifactsRepository.InsertUserArtifactAsync(artifact, userId);
    }

    public async Task<bool> UpdateArtifactLevelAsync(Artifacts artifact, int level)
    {
        return await _userArtifactsRepository.UpdateArtifactLevelAsync(artifact, level);
    }

    public async Task<bool> UpdateArtifactBreakthroughAsync(Artifacts artifact, int star, double quantity)
    {
        return await _userArtifactsRepository.UpdateArtifactBreakthroughAsync(artifact, star, quantity);
    }

    public async Task<Artifacts> GetUserArtifactByIdAsync(string user_id, string Id)
    {
        return await _userArtifactsRepository.GetUserArtifactByIdAsync(user_id, Id);
    }

    public async Task<Artifacts> SumPowerUserArtifactsAsync()
    {
        return await _userArtifactsRepository.SumPowerUserArtifactsAsync();
    }

    public async Task<bool> InsertOrUpdateUserArtifactsBatchAsync(List<Artifacts> artifacts)
    {
        return await _userArtifactsRepository.InsertOrUpdateUserArtifactsBatchAsync(artifacts);
    }
}

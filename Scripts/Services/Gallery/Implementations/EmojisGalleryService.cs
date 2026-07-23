using System.Collections.Generic;
using System.Threading.Tasks;

public class EmojisGalleryService : IEmojisGalleryService
{
    private static EmojisGalleryService _instance;
    private readonly IEmojisGalleryRepository _emojisGalleryRepository;

    public EmojisGalleryService(IEmojisGalleryRepository emojisGalleryRepository)
    {
        _emojisGalleryRepository = emojisGalleryRepository;
    }

    public static EmojisGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new EmojisGalleryService(new EmojisGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Emojis>> GetEmojisCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Emojis> list = await _emojisGalleryRepository.GetEmojisCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEmojisCountAsync(string search, string rare)
    {
        return await _emojisGalleryRepository.GetEmojisCountAsync(search, rare);
    }

    public async Task InsertEmojiGalleryAsync(string userId, string Id)
    {
        IEmojisRepository _repository = new EmojisRepository();
        EmojisService _service = new EmojisService(_repository);
        await _emojisGalleryRepository.InsertEmojiGalleryAsync(userId, Id, await _service.GetEmojiByIdAsync(Id));
    }

    public async Task UpdateStatusEmojiGalleryAsync(string userId, string Id)
    {
        await _emojisGalleryRepository.UpdateStatusEmojiGalleryAsync(userId, Id);
    }

    public async Task<Emojis> SumPowerEmojisGalleryAsync(string userId)
    {
        return await _emojisGalleryRepository.SumPowerEmojisGalleryAsync(userId);
    }

    public async Task UpdateStarEmojiGalleryAsync(string userId, string Id, double star)
    {
        await _emojisGalleryRepository.UpdateStarEmojiGalleryAsync(userId, Id, star);
    }

    public async Task UpdateEmojiGalleryPowerAsync(string userId, string Id)
    {
        IEmojisRepository _repository = new EmojisRepository();
        EmojisService _service = new EmojisService(_repository);
        await _emojisGalleryRepository.UpdateEmojiGalleryPowerAsync(userId, Id, await _service.GetEmojiByIdAsync(Id));
    }
}

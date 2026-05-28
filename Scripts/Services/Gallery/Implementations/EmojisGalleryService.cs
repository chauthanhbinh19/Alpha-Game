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

    public async Task<List<Emojis>> GetEmojisCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Emojis> list = await _emojisGalleryRepository.GetEmojisCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEmojisCountAsync(string search, string rare)
    {
        return await _emojisGalleryRepository.GetEmojisCountAsync(search, rare);
    }

    public async Task InsertEmojiGalleryAsync(string Id)
    {
        IEmojisRepository _repository = new EmojisRepository();
        EmojisService _service = new EmojisService(_repository);
        await _emojisGalleryRepository.InsertEmojiGalleryAsync(Id, await _service.GetEmojiByIdAsync(Id));
    }

    public async Task UpdateStatusEmojiGalleryAsync(string Id)
    {
        await _emojisGalleryRepository.UpdateStatusEmojiGalleryAsync(Id);
    }

    public async Task<Emojis> SumPowerEmojisGalleryAsync()
    {
        return await _emojisGalleryRepository.SumPowerEmojisGalleryAsync();
    }

    public async Task UpdateStarEmojiGalleryAsync(string Id, double star)
    {
        await _emojisGalleryRepository.UpdateStarEmojiGalleryAsync(Id, star);
    }

    public async Task UpdateEmojiGalleryPowerAsync(string Id)
    {
        IEmojisRepository _repository = new EmojisRepository();
        EmojisService _service = new EmojisService(_repository);
        await _emojisGalleryRepository.UpdateEmojiGalleryPowerAsync(Id, await _service.GetEmojiByIdAsync(Id));
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

public class EmojisGalleryService : IEmojisGalleryService
{
    private static EmojisGalleryService _instance;
    private readonly IEmojisGalleryRepository _coresGalleryRepository;

    public EmojisGalleryService(IEmojisGalleryRepository coresGalleryRepository)
    {
        _coresGalleryRepository = coresGalleryRepository;
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
        List<Emojis> list = await _coresGalleryRepository.GetEmojisCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEmojisCountAsync(string search, string rare)
    {
        return await _coresGalleryRepository.GetEmojisCountAsync(search, rare);
    }

    public async Task InsertEmojiGalleryAsync(string Id)
    {
        IEmojisRepository _repository = new EmojisRepository();
        EmojisService _service = new EmojisService(_repository);
        await _coresGalleryRepository.InsertEmojiGalleryAsync(Id, await _service.GetEmojiByIdAsync(Id));
    }

    public async Task UpdateStatusEmojiGalleryAsync(string Id)
    {
        await _coresGalleryRepository.UpdateStatusEmojiGalleryAsync(Id);
    }

    public async Task<Emojis> SumPowerEmojisGalleryAsync()
    {
        return await _coresGalleryRepository.SumPowerEmojisGalleryAsync();
    }

    public async Task UpdateStarEmojiGalleryAsync(string Id, double star)
    {
        await _coresGalleryRepository.UpdateStarEmojiGalleryAsync(Id, star);
    }

    public async Task UpdateEmojiGalleryPowerAsync(string Id)
    {
        IEmojisRepository _repository = new EmojisRepository();
        EmojisService _service = new EmojisService(_repository);
        await _coresGalleryRepository.UpdateEmojiGalleryPowerAsync(Id, await _service.GetEmojiByIdAsync(Id));
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

public class CardSoldiersGalleryService : ICardSoldiersGalleryService
{
    private static CardSoldiersGalleryService _instance;
    private readonly ICardSoldiersGalleryRepository _cardAdmiralsGalleryRepository;

    public CardSoldiersGalleryService(ICardSoldiersGalleryRepository cardAdmiralsGalleryRepository)
    {
        _cardAdmiralsGalleryRepository = cardAdmiralsGalleryRepository;
    }

    public static CardSoldiersGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CardSoldiersGalleryService(new CardSoldiersGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<CardSoldiers>> GetCardSoldiersCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardSoldiers> list = await _cardAdmiralsGalleryRepository.GetCardSoldiersCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCardSoldiersCountAsync(string search, string type, string rare)
    {
        return await _cardAdmiralsGalleryRepository.GetCardSoldiersCountAsync(search, type, rare);
    }

    public async Task InsertCardSoldierGalleryAsync(string userId, string Id)
    {
        ICardSoldiersRepository _repository = new CardSoldiersRepository();
        CardSoldiersService _service = new CardSoldiersService(_repository);
        await _cardAdmiralsGalleryRepository.InsertCardSoldierGalleryAsync(userId, Id, await _service.GetCardSoldierByIdAsync(Id));
    }

    public async Task UpdateStatusCardSoldierGalleryAsync(string userId, string Id)
    {
        await _cardAdmiralsGalleryRepository.UpdateStatusCardSoldierGalleryAsync(userId, Id);
    }

    public async Task<CardSoldiers> SumPowerCardSoldiersGalleryAsync(string userId)
    {
        return await _cardAdmiralsGalleryRepository.SumPowerCardSoldiersGalleryAsync(userId);
    }

    public async Task UpdateStarCardSoldierGalleryAsync(string userId, string Id, double star)
    {
        await _cardAdmiralsGalleryRepository.UpdateStarCardSoldierGalleryAsync(userId, Id, star);
    }

    public async Task UpdateCardSoldierGalleryPowerAsync(string userId, string Id)
    {
        ICardSoldiersRepository _repository = new CardSoldiersRepository();
        CardSoldiersService _service = new CardSoldiersService(_repository);
        await _cardAdmiralsGalleryRepository.UpdateCardSoldierGalleryPowerAsync(userId, Id, await _service.GetCardSoldierByIdAsync(Id));
    }
}

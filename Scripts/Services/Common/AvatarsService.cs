using System.Collections.Generic;

public class AvatarsService : IAvatarsService
{
    private readonly IAvatarsRepository _avatarsRepository;

    public AvatarsService(IAvatarsRepository avatarsRepository)
    {
        _avatarsRepository = avatarsRepository;
    }

    public static AvatarsService Create()
    {
        return new AvatarsService(new AvatarsRepository());
    }

    public List<Achievements> GetAvatars(int pageSize, int offset, string rare)
    {
        List<Achievements> list = _avatarsRepository.GetAvatars(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetAvatarsCount(string rare)
    {
        return _avatarsRepository.GetAvatarsCount(rare);
    }

    public List<Achievements> GetAvatarsWithPrice(int pageSize, int offset)
    {
        List<Achievements> list = _avatarsRepository.GetAvatarsWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetAvatarsWithPriceCount()
    {
        return _avatarsRepository.GetAvatarsWithPriceCount();
    }

    public Achievements GetAvatarsById(string Id)
    {
        return _avatarsRepository.GetAvatarsById(Id);
    }

    public Achievements SumPowerAvatarsPercent()
    {
        return _avatarsRepository.SumPowerAvatarsPercent();
    }

    public List<string> GetUniqueAvatarsId()
    {
        return _avatarsRepository.GetUniqueAvatarsId();
    }
}
using System.Collections.Generic;

public class WorldsService : IWorldsService
{
    private readonly IWorldsRepository _WorldsRepository;

    public WorldsService(IWorldsRepository titleRepository)
    {
        _WorldsRepository = titleRepository;
    }

    public static WorldsService Create()
    {
        return new WorldsService(new WorldsRepository());
    }

    public List<Worlds> GetWorlds(string userId, int pageSize, int offset)
    {
        List<Worlds> list = _WorldsRepository.GetWorlds(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetWorldsCount(string rare)
    {
        return _WorldsRepository.GetWorldsCount(rare);
    }

    public List<Worlds> GetWorldsWithPrice(int pageSize, int offset)
    {
        List<Worlds> list = _WorldsRepository.GetWorldsWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetWorldsWithPriceCount()
    {
        return _WorldsRepository.GetWorldsWithPriceCount();
    }

    public Worlds GetWorldsById(string Id)
    {
        return _WorldsRepository.GetWorldsById(Id);
    }

    public Worlds SumPowerWorldsPercent()
    {
        return _WorldsRepository.SumPowerWorldsPercent();
    }

    public List<string> GetUniqueWorldId()
    {
        return _WorldsRepository.GetUniqueWorldId();
    }
}

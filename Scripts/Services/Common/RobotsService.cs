using System.Collections.Generic;

public class RobotsService : IRobotsService
{
    private readonly IRobotsRepository _RobotsRepository;

    public RobotsService(IRobotsRepository titleRepository)
    {
        _RobotsRepository = titleRepository;
    }

    public static RobotsService Create()
    {
        return new RobotsService(new RobotsRepository());
    }

    public List<Robots> GetRobots(int pageSize, int offset, string rare)
    {
        List<Robots> list = _RobotsRepository.GetRobots(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetRobotsCount(string rare)
    {
        return _RobotsRepository.GetRobotsCount(rare);
    }

    public List<Robots> GetRobotsWithPrice(int pageSize, int offset)
    {
        List<Robots> list = _RobotsRepository.GetRobotsWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetRobotsWithPriceCount()
    {
        return _RobotsRepository.GetRobotsWithPriceCount();
    }

    public Robots GetRobotsById(string Id)
    {
        return _RobotsRepository.GetRobotsById(Id);
    }

    public Robots SumPowerRobotsPercent()
    {
        return _RobotsRepository.SumPowerRobotsPercent();
    }

    public List<string> GetUniqueRobotId()
    {
        return _RobotsRepository.GetUniqueRobotId();
    }
}

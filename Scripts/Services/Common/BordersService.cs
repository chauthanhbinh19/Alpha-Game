using System.Collections.Generic;

public class BordersService : IBordersService
{
    private readonly IBordersRepository _bordersRepository;

    public BordersService(IBordersRepository bordersRepository)
    {
        _bordersRepository = bordersRepository;
    }

    public static BordersService Create()
    {
        return new BordersService(new BordersRepository());
    }

    public List<Borders> GetBorders(int pageSize, int offset)
    {
        List<Borders> list = _bordersRepository.GetBorders(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetBordersCount()
    {
        return _bordersRepository.GetBordersCount();
    }

    public List<Borders> GetBordersWithPrice(int pageSize, int offset)
    {
        List<Borders> list = _bordersRepository.GetBordersWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetBordersWithPriceCount()
    {
        return _bordersRepository.GetBordersWithPriceCount();
    }

    public Borders GetBordersById(string Id)
    {
        return _bordersRepository.GetBordersById(Id);
    }

    public Borders SumPowerBordersPercent()
    {
        return _bordersRepository.SumPowerBordersPercent();
    }
}
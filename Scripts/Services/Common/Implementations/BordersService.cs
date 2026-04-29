using System.Collections.Generic;
using System.Threading.Tasks;

public class BordersService : IBordersService
{
    private static BordersService _instance;
    private readonly IBordersRepository _bordersRepository;

    public BordersService(IBordersRepository bordersRepository)
    {
        _bordersRepository = bordersRepository;
    }

    public static BordersService Create()
    {
        if (_instance == null)
        {
            _instance = new BordersService(new BordersRepository());
        }
        return _instance;
    }

    public async Task<List<Borders>> GetBordersAsync(string search, string rare, int pageSize, int offset)
    {
        List<Borders> list = await _bordersRepository.GetBordersAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBordersCountAsync(string search, string rare)
    {
        return await _bordersRepository.GetBordersCountAsync(search, rare);
    }

    public async Task<List<Borders>> GetBordersWithPriceAsync(int pageSize, int offset)
    {
        List<Borders> list = await _bordersRepository.GetBordersWithPriceAsync(pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBordersWithPriceCountAsync()
    {
        return await _bordersRepository.GetBordersWithPriceCountAsync();
    }

    public async Task<Borders> GetBorderByIdAsync(string Id)
    {
        return await _bordersRepository.GetBorderByIdAsync(Id);
    }

    public async Task<Borders> SumPowerBordersPercentAsync()
    {
        return await _bordersRepository.SumPowerBordersPercentAsync();
    }

    public async Task<List<string>> GetUniqueBordersIdAsync()
    {
        return await _bordersRepository.GetUniqueBordersIdAsync();
    }
}
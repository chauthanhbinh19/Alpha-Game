using System.Collections.Generic;
using System.Threading.Tasks;

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

    public async Task<List<Borders>> GetBordersAsync(int pageSize, int offset, string rare)
    {
        List<Borders> list = await _bordersRepository.GetBordersAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetBordersCountAsync(string rare)
    {
        return await _bordersRepository.GetBordersCountAsync(rare);
    }

    public async Task<List<Borders>> GetBordersWithPriceAsync(int pageSize, int offset)
    {
        List<Borders> list = await _bordersRepository.GetBordersWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
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
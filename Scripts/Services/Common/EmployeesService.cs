using System.Collections.Generic;
using System.Threading.Tasks;

public class EmployeesService : IEmployeesService
{
    private readonly IEmployeesRepository _EmployeesRepository;

    public EmployeesService(IEmployeesRepository titleRepository)
    {
        _EmployeesRepository = titleRepository;
    }

    public static EmployeesService Create()
    {
        return new EmployeesService(new EmployeesRepository());
    }

    public async Task<List<Employees>> GetEmployeesAsync(string userId, int pageSize, int offset)
    {
        List<Employees> list = await _EmployeesRepository.GetEmployeesAsync(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEmployeesCountAsync(string rare)
    {
        return await _EmployeesRepository.GetEmployeesCountAsync(rare);
    }

    public async Task<List<Employees>> GetEmployeesWithPriceAsync(int pageSize, int offset)
    {
        List<Employees> list = await _EmployeesRepository.GetEmployeesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEmployeesWithPriceCountAsync()
    {
        return await _EmployeesRepository.GetEmployeesWithPriceCountAsync();
    }

    public async Task<Employees> GetEmployeeByIdAsync(string Id)
    {
        return await _EmployeesRepository.GetEmployeeByIdAsync(Id);
    }

    public async Task<Employees> SumPowerEmployeesPercentAsync()
    {
        return await _EmployeesRepository.SumPowerEmployeesPercentAsync();
    }

    public async Task<List<string>> GetUniqueEmployeesIdAsync()
    {
        return await _EmployeesRepository.GetUniqueEmployeesIdAsync();
    }
}

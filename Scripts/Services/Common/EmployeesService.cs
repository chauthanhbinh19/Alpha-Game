using System.Collections.Generic;
using System.Threading.Tasks;

public class EmployeesService : IEmployeesService
{
    private static EmployeesService _instance;
    private readonly IEmployeesRepository _employeesRepository;

    public EmployeesService(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public static EmployeesService Create()
    {
        if (_instance == null)
        {
            _instance = new EmployeesService(new EmployeesRepository());
        }
        return _instance;
    }

    public async Task<List<Employees>> GetEmployeesAsync(string userId, int pageSize, int offset)
    {
        List<Employees> list = await _employeesRepository.GetEmployeesAsync(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEmployeesCountAsync(string rare)
    {
        return await _employeesRepository.GetEmployeesCountAsync(rare);
    }

    public async Task<List<Employees>> GetEmployeesWithPriceAsync(int pageSize, int offset)
    {
        List<Employees> list = await _employeesRepository.GetEmployeesWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetEmployeesWithPriceCountAsync()
    {
        return await _employeesRepository.GetEmployeesWithPriceCountAsync();
    }

    public async Task<Employees> GetEmployeeByIdAsync(string Id)
    {
        return await _employeesRepository.GetEmployeeByIdAsync(Id);
    }

    public async Task<Employees> SumPowerEmployeesPercentAsync()
    {
        return await _employeesRepository.SumPowerEmployeesPercentAsync();
    }

    public async Task<List<string>> GetUniqueEmployeesIdAsync()
    {
        return await _employeesRepository.GetUniqueEmployeesIdAsync();
    }
}

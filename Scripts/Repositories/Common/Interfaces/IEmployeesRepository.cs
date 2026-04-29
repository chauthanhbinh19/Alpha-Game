using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmployeesRepository
{
    Task<List<string>> GetUniqueEmployeesIdAsync();
    Task<List<Employees>> GetEmployeesAsync(string userId, int pageSize, int offset);
    Task<int> GetEmployeesCountAsync(string rare);
    Task<List<Employees>> GetEmployeesWithPriceAsync(int pageSize, int offset);
    Task<int> GetEmployeesWithPriceCountAsync();
    Task<Employees> GetEmployeeByIdAsync(string id);
    Task<Employees> SumPowerEmployeesPercentAsync();
}

using System.Collections.Generic;

public interface IEmployeesRepository
{
    List<string> GetUniqueEmployeeId();
    List<Employees> GetEmployees(string userId, int pageSize, int offset);
    int GetEmployeesCount(string rare);
    List<Employees> GetEmployeesWithPrice(int pageSize, int offset);
    int GetEmployeesWithPriceCount();
    Employees GetEmployeesById(string Id);
    Employees SumPowerEmployeesPercent();
}

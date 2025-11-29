using System.Collections.Generic;

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

    public List<Employees> GetEmployees(string userId, int pageSize, int offset)
    {
        List<Employees> list = _EmployeesRepository.GetEmployees(userId, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetEmployeesCount(string rare)
    {
        return _EmployeesRepository.GetEmployeesCount(rare);
    }

    public List<Employees> GetEmployeesWithPrice(int pageSize, int offset)
    {
        List<Employees> list = _EmployeesRepository.GetEmployeesWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetEmployeesWithPriceCount()
    {
        return _EmployeesRepository.GetEmployeesWithPriceCount();
    }

    public Employees GetEmployeesById(string Id)
    {
        return _EmployeesRepository.GetEmployeesById(Id);
    }

    public Employees SumPowerEmployeesPercent()
    {
        return _EmployeesRepository.SumPowerEmployeesPercent();
    }

    public List<string> GetUniqueEmployeeId()
    {
        return _EmployeesRepository.GetUniqueEmployeeId();
    }
}

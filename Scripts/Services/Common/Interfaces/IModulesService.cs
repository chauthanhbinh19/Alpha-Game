using System.Threading.Tasks;

public interface IModulesService
{
    Task<Modules> GetModuleByIdAsync(string id);
    Task<InsertOrUpdateResult<Modules>> InsertModuleAsync(Modules module);
    Task<InsertOrUpdateResult<Modules>> UpdateModuleAsync(Modules module);
}
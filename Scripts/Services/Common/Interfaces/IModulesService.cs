using System.Threading.Tasks;

public interface IModulesService
{
    Task<Modules> GetModuleByIdAsync(string id);
}
using System.Threading.Tasks;

public interface IModulesRepository
{
    Task<Modules> GetModuleByIdAsync(string id);
}
using System.Collections.Generic;
using System.Threading.Tasks;
public class EnergyService : IEnergyService
{
    private static EnergyService _instance;
    private readonly IEnergyRepository _energyRepository;

    public EnergyService(IEnergyRepository energyRepository)
    {
        _energyRepository = energyRepository;
    }

    public static EnergyService Create()
    {
        if (_instance == null)
        {
            _instance = new EnergyService(new EnergyRepository());
        }
        return _instance;
    }

    public async Task<Energy> GetEnergyAsync(string id)
    {
        return await _energyRepository.GetEnergyAsync(id);
    }

    public async Task<Energy> GetSumEnergyAsync(string user_id)
    {
        return await _energyRepository.GetSumEnergyAsync(user_id);
    }

    public async Task InsertOrUpdateEnergyAsync(string userId, Energy energy, string id)
    {
        await _energyRepository.InsertOrUpdateEnergyAsync(userId, energy, id);
    }
}
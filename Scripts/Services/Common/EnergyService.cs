using System.Collections.Generic;
using System.Threading.Tasks;
public class EnergyService : IEnergyService
{
    private readonly IEnergyRepository _EnergyRepository;

    public EnergyService(IEnergyRepository EnergyRepository)
    {
        _EnergyRepository = EnergyRepository;
    }

    public static EnergyService Create()
    {
        return new EnergyService(new EnergyRepository());
    }

    public async Task<Energy> GetEnergyAsync(string id)
    {
        return await _EnergyRepository.GetEnergyAsync(id);
    }

    public async Task<Energy> GetSumEnergyAsync(string user_id)
    {
        return await _EnergyRepository.GetSumEnergyAsync(user_id);
    }

    public async Task InsertOrUpdateEnergyAsync(string userId, Energy energy, string id)
    {
        await _EnergyRepository.InsertOrUpdateEnergyAsync(userId, energy, id);
    }
}
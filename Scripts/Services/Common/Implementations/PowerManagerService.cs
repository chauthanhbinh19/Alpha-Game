using System.Collections.Generic;
using System.Threading.Tasks;

public class PowerManagerService : IPowerManagerService
{
    private readonly IPowerManagerRepository _powerManagerRepository;

    public PowerManagerService(IPowerManagerRepository powerManagerRepository)
    {
        _powerManagerRepository = powerManagerRepository;
    }

    public static IPowerManagerService Create() => ServiceContainer.GetService<IPowerManagerService>();

    public async Task InsertUserStatsAsync(string userId)
    {
        PowerManager powerManager = new PowerManager();
        await _powerManagerRepository.InsertUserStatsAsync(userId, powerManager);
    }
    public async Task UpdateUserStatsAsync(string userId)
    {
        // PowerManager powerManager = await CalculateTotalPowerAsync(userId);
        // await _powerManagerRepository.UpdateUserStatsAsync(userId, powerManager);
    }
    public async Task UpdateUserStatsAsync(string userId, PowerManager powerManager)
    {
        await _powerManagerRepository.UpdateUserStatsAsync(userId, powerManager);
    }
    public async Task<PowerManager> GetUserStatsAsync(string userId)
    {
        return await _powerManagerRepository.GetUserStatsAsync(userId);
    }

}
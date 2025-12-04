using System.Collections.Generic;
using System.Threading.Tasks;

public class ArenaService : IArenaService
{
    private readonly IArenaRepository _arenaRepository;

    public ArenaService(IArenaRepository arenaRepository)
    {
        _arenaRepository = arenaRepository;
    }

    public static ArenaService Create()
    {
        return new ArenaService(new ArenaRepository());
    }

    public async Task<List<string>> GetUniqueTypesAsync()
    {
        return await _arenaRepository.GetUniqueTypesAsync();
    }

    public async Task<string> GetArenaModeIdAsync(string type)
    {
        return await _arenaRepository.GetArenaModeIdAsync(type);
    }

    public async Task<Dictionary<string, int>> GetArenaParticipantByRankingAsync(string arena_id)
    {
        return await _arenaRepository.GetArenaParticipantByRankingAsync(arena_id);
    }

    public async Task<int> GetArenaParticipantPointAsync(string user_id, string arena_id)
    {
        return await _arenaRepository.GetArenaParticipantPointAsync(user_id, arena_id);
    } 

    public async Task InsertArenaParticipantAsync(string user_id, string arena_id)
    {
        await _arenaRepository.InsertArenaParticipantAsync(user_id, arena_id);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArenaRepository
{
    Task<List<string>> GetUniqueTypesAsync();
    Task<string> GetArenaModeIdAsync(string type);
    Task<Dictionary<string, int>> GetArenaParticipantByRankingAsync(string arena_id);
    Task<int> GetArenaParticipantPointAsync(string user_id, string arena_id);
    Task InsertArenaParticipantAsync(string user_id, string arena_id);
}
using System.Collections.Generic;

public interface IArenaRepository
{
    List<string> GetUniqueTypes();
    string GetArenaModeId(string type);
    Dictionary<string, int> GetArenaParticipantByRanking(string arena_id);
    int GetArenaParticipantPoint(string user_id, string arena_id);
    void InsertArenaParticipant(string user_id, string arena_id);
}
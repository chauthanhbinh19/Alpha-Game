using System.Collections.Generic;

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

    public List<string> GetUniqueTypes()
    {
        return _arenaRepository.GetUniqueTypes();
    }

    public string GetArenaModeId(string type)
    {
        return _arenaRepository.GetArenaModeId(type);
    }

    public Dictionary<string, int> GetArenaParticipantByRanking(string arena_id)
    {
        return _arenaRepository.GetArenaParticipantByRanking(arena_id);
    }

    public int GetArenaParticipantPoint(string user_id, string arena_id)
    {
        return _arenaRepository.GetArenaParticipantPoint(user_id, arena_id);
    }

    public void InsertArenaParticipant(string user_id, string arena_id)
    {
        _arenaRepository.InsertArenaParticipant(user_id, arena_id);
    }
}
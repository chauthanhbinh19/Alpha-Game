using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class RanksService : IRanksService
{
    private readonly IRanksRepository _ranksRepository;

    public RanksService(IRanksRepository ranksRepository)
    {
        _ranksRepository = ranksRepository;
    }

    public static IRanksService Create() => ServiceContainer.GetService<IRanksService>();

    public async Task<Ranks> GetRankByIdAsync(string id)
    {
        return await _ranksRepository.GetRankByIdAsync(id);
    }
    public Task<InsertOrUpdateResult<Ranks>> InsertRankAsync(Ranks rank)
    {
        return _ranksRepository.InsertRankAsync(rank);
    }

    public Task<InsertOrUpdateResult<Ranks>> UpdateRankAsync(Ranks rank)
    {
        return _ranksRepository.UpdateRankAsync(rank);
    }
}
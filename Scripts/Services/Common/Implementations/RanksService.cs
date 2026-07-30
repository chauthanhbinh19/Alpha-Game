using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class RanksService : IRanksService
{
    private readonly IRanksRepository _universesRepository;

    public RanksService(IRanksRepository universesRepository)
    {
        _universesRepository = universesRepository;
    }

    public static IRanksService Create() => ServiceContainer.GetService<IRanksService>();

    public async Task<Ranks> GetRankByIdAsync(string id)
    {
        return await _universesRepository.GetRankByIdAsync(id);
    }
}
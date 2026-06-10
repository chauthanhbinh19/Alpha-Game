using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class RanksService : IRanksService
{
    private static RanksService _instance;
    private readonly IRanksRepository _universesRepository;

    public RanksService(IRanksRepository universesRepository)
    {
        _universesRepository = universesRepository;
    }

    public static RanksService Create()
    {
        if (_instance == null)
        {
            _instance = new RanksService(new RanksRepository());
        }
        return _instance;
    }

    public async Task<Ranks> GetRankByIdAsync(string id)
    {
        return await _universesRepository.GetRankByIdAsync(id);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class AnimesService : IAnimesService
{
    private static AnimesService _instance;
    private readonly IAnimesRepository _universesRepository;

    public AnimesService(IAnimesRepository universesRepository)
    {
        _universesRepository = universesRepository;
    }

    public static AnimesService Create()
    {
        if (_instance == null)
        {
            _instance = new AnimesService(new AnimesRepository());
        }
        return _instance;
    }

    public async Task<Animes> GetAnimeByIdAsync(string id)
    {
        return await _universesRepository.GetAnimeByIdAsync(id);
    }
}
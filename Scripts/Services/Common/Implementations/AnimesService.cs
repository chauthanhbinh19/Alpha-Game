using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class AnimesService : IAnimesService
{
    private readonly IAnimesRepository _animesRepository;

    public AnimesService(IAnimesRepository animesRepository)
    {
        _animesRepository = animesRepository;
    }

    public static IAnimesService Create() => ServiceContainer.GetService<IAnimesService>();

    public async Task<Animes> GetAnimeByIdAsync(string id)
    {
        return await _animesRepository.GetAnimeByIdAsync(id);
    }
}
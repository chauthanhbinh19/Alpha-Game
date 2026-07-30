using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UniversesService : IUniversesService
{
    private readonly IUniversesRepository _universesRepository;

    public UniversesService(IUniversesRepository universesRepository)
    {
        _universesRepository = universesRepository;
    }

    public static IUniversesService Create() => ServiceContainer.GetService<IUniversesService>();

    public async Task<Universes> GetUniverseByIdAsync(string id)
    {
        return await _universesRepository.GetUniverseByIdAsync(id);
    }
}
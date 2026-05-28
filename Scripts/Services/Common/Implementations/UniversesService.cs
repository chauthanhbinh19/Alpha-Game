using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UniversesService : IUniversesService
{
    private static UniversesService _instance;
    private readonly IUniversesRepository _universesRepository;

    public UniversesService(IUniversesRepository universesRepository)
    {
        _universesRepository = universesRepository;
    }

    public static UniversesService Create()
    {
        if (_instance == null)
        {
            _instance = new UniversesService(new UniversesRepository());
        }
        return _instance;
    }

    public async Task<Universes> GetUniverseByIdAsync(string id)
    {
        return await _universesRepository.GetUniverseByIdAsync(id);
    }
}
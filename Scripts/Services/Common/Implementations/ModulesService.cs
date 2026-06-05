using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class ModulesService : IModulesService
{
    private static ModulesService _instance;
    private readonly IModulesRepository _universesRepository;

    public ModulesService(IModulesRepository universesRepository)
    {
        _universesRepository = universesRepository;
    }

    public static ModulesService Create()
    {
        if (_instance == null)
        {
            _instance = new ModulesService(new ModulesRepository());
        }
        return _instance;
    }

    public async Task<Modules> GetModuleByIdAsync(string id)
    {
        return await _universesRepository.GetModuleByIdAsync(id);
    }
}
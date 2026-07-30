using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class ModulesService : IModulesService
{
    private readonly IModulesRepository _modulesRepository;

    public ModulesService(IModulesRepository modulesRepository)
    {
        _modulesRepository = modulesRepository;
    }

    public static IModulesService Create() => ServiceContainer.GetService<IModulesService>();

    public async Task<Modules> GetModuleByIdAsync(string id)
    {
        return await _modulesRepository.GetModuleByIdAsync(id);
    }
}
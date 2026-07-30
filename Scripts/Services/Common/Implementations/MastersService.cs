using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class MastersService : IMastersService
{
    private readonly IMastersRepository _mastersRepository;

    public MastersService(IMastersRepository mastersRepository)
    {
        _mastersRepository = mastersRepository;
    }

    public static IMastersService Create() => ServiceContainer.GetService<IMastersService>();

    public async Task<Masters> GetMasterByIdAsync(string id)
    {
        return await _mastersRepository.GetMasterByIdAsync(id);
    }
}
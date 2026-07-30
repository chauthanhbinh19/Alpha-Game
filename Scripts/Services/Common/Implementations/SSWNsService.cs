using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class SSWNsService : ISSWNsService
{
    private readonly ISSWNsRepository _sswnsRepository;

    public SSWNsService(ISSWNsRepository sswnsRepository)
    {
        _sswnsRepository = sswnsRepository;
    }

    public static ISSWNsService Create() => ServiceContainer.GetService<ISSWNsService>();

    public async Task<SSWNs> GetSSWNByIdAsync(string id)
    {
        return await _sswnsRepository.GetSSWNByIdAsync(id);
    }
}
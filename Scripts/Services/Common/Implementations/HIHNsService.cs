using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIHNsService : IHIHNsService
{
    private readonly IHIHNsRepository _hihnsRepository;

    public HIHNsService(IHIHNsRepository hihnsRepository)
    {
        _hihnsRepository = hihnsRepository;
    }

    public static IHIHNsService Create() => ServiceContainer.GetService<IHIHNsService>();

    public async Task<HIHNs> GetHIHNByIdAsync(string id)
    {
        return await _hihnsRepository.GetHIHNByIdAsync(id);
    }
}
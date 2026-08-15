using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HITNsService : IHITNsService
{
    private readonly IHITNsRepository _hitnsRepository;

    public HITNsService(IHITNsRepository hitnsRepository)
    {
        _hitnsRepository = hitnsRepository;
    }

    public static IHITNsService Create() => ServiceContainer.GetService<IHITNsService>();

    public async Task<HITNs> GetHITNByIdAsync(string id)
    {
        return await _hitnsRepository.GetHITNByIdAsync(id);
    }
    public Task<InsertOrUpdateResult<HITNs>> InsertHITNAsync(HITNs hitn)
    {
        return _hitnsRepository.InsertHITNAsync(hitn);
    }

    public Task<InsertOrUpdateResult<HITNs>> UpdateHITNAsync(HITNs hitn)
    {
        return _hitnsRepository.UpdateHITNAsync(hitn);
    }
}
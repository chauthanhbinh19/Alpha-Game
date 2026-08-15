using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HISNsService : IHISNsService
{
    private readonly IHISNsRepository _hisnsRepository;

    public HISNsService(IHISNsRepository hisnsRepository)
    {
        _hisnsRepository = hisnsRepository;
    }

    public static IHISNsService Create() => ServiceContainer.GetService<IHISNsService>();

    public async Task<HISNs> GetHISNByIdAsync(string id)
    {
        return await _hisnsRepository.GetHISNByIdAsync(id);
    }
    public Task<InsertOrUpdateResult<HISNs>> InsertHISNAsync(HISNs hisn)
    {
        return _hisnsRepository.InsertHISNAsync(hisn);
    }

    public Task<InsertOrUpdateResult<HISNs>> UpdateHISNAsync(HISNs hisn)
    {
        return _hisnsRepository.UpdateHISNAsync(hisn);
    }
}
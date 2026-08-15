using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIDCsService : IHIDCsService
{
    private readonly IHIDCsRepository _hidcsRepository;

    public HIDCsService(IHIDCsRepository hidcsRepository)
    {
        _hidcsRepository = hidcsRepository;
    }

    public static IHIDCsService Create() => ServiceContainer.GetService<IHIDCsService>();

    public async Task<HIDCs> GetHIDCByIdAsync(string id)
    {
        return await _hidcsRepository.GetHIDCByIdAsync(id);
    }
    public Task<InsertOrUpdateResult<HIDCs>> InsertHIDCAsync(HIDCs hidc)
    {
        return _hidcsRepository.InsertHIDCAsync(hidc);
    }

    public Task<InsertOrUpdateResult<HIDCs>> UpdateHIDCAsync(HIDCs hidc)
    {
        return _hidcsRepository.UpdateHIDCAsync(hidc);
    }
}
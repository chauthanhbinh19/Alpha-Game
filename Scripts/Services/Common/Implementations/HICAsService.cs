using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HICAsService : IHICAsService
{
    private readonly IHICAsRepository _hicasRepository;

    public HICAsService(IHICAsRepository hicasRepository)
    {
        _hicasRepository = hicasRepository;
    }

    public static IHICAsService Create() => ServiceContainer.GetService<IHICAsService>();

    public async Task<HICAs> GetHICAByIdAsync(string id)
    {
        return await _hicasRepository.GetHICAByIdAsync(id);
    }
    public Task<InsertOrUpdateResult<HICAs>> InsertHICAAsync(HICAs hica)
    {
        return _hicasRepository.InsertHICAAsync(hica);
    }

    public Task<InsertOrUpdateResult<HICAs>> UpdateHICAAsync(HICAs hica)
    {
        return _hicasRepository.UpdateHICAAsync(hica);
    }
}
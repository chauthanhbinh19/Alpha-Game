using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIENsService : IHIENsService
{
    private readonly IHIENsRepository _hiensRepository;

    public HIENsService(IHIENsRepository hiensRepository)
    {
        _hiensRepository = hiensRepository;
    }

    public static IHIENsService Create() => ServiceContainer.GetService<IHIENsService>();

    public async Task<HIENs> GetHIENByIdAsync(string id)
    {
        return await _hiensRepository.GetHIENByIdAsync(id);
    }
    public Task<InsertOrUpdateResult<HIENs>> InsertHIENAsync(HIENs hien)
    {
        return _hiensRepository.InsertHIENAsync(hien);
    }

    public Task<InsertOrUpdateResult<HIENs>> UpdateHIENAsync(HIENs hien)
    {
        return _hiensRepository.UpdateHIENAsync(hien);
    }
}
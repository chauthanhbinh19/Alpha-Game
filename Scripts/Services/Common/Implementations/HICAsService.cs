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
}
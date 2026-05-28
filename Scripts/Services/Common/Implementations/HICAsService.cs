using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HICAsService : IHICAsService
{
    private static HICAsService _instance;
    private readonly IHICAsRepository _hicasRepository;

    public HICAsService(IHICAsRepository hicasRepository)
    {
        _hicasRepository = hicasRepository;
    }

    public static HICAsService Create()
    {
        if (_instance == null)
        {
            _instance = new HICAsService(new HICAsRepository());
        }
        return _instance;
    }
    public async Task<HICAs> GetHICAByIdAsync(string id)
    {
        return await _hicasRepository.GetHICAByIdAsync(id);
    }
}
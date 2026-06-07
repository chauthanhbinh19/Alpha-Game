using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class MastersService : IMastersService
{
    private static MastersService _instance;
    private readonly IMastersRepository _universesRepository;

    public MastersService(IMastersRepository universesRepository)
    {
        _universesRepository = universesRepository;
    }

    public static MastersService Create()
    {
        if (_instance == null)
        {
            _instance = new MastersService(new MastersRepository());
        }
        return _instance;
    }

    public async Task<Masters> GetMasterByIdAsync(string id)
    {
        return await _universesRepository.GetMasterByIdAsync(id);
    }
}
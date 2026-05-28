using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIHNsService : IHIHNsService
{
    private static HIHNsService _instance;
    private readonly IHIHNsRepository _hihnsRepository;

    public HIHNsService(IHIHNsRepository hihnsRepository)
    {
        _hihnsRepository = hihnsRepository;
    }

    public static HIHNsService Create()
    {
        if (_instance == null)
        {
            _instance = new HIHNsService(new HIHNsRepository());
        }
        return _instance;
    }

    public async Task<HIHNs> GetHIHNByIdAsync(string id)
    {
        return await _hihnsRepository.GetHIHNByIdAsync(id);
    }
}
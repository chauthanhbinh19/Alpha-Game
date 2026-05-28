using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HITNsService : IHITNsService
{
    private static HITNsService _instance;
    private readonly IHITNsRepository _hitnsRepository;

    public HITNsService(IHITNsRepository hitnsRepository)
    {
        _hitnsRepository = hitnsRepository;
    }

    public static HITNsService Create()
    {
        if (_instance == null)
        {
            _instance = new HITNsService(new HITNsRepository());
        }
        return _instance;
    }

    public async Task<HITNs> GetHITNByIdAsync(string id)
    {
        return await _hitnsRepository.GetHITNByIdAsync(id);
    }
}
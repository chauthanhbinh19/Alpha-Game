using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIRNsService : IHIRNsService
{
    private static HIRNsService _instance;
    private readonly IHIRNsRepository _hirnsRepository;

    public HIRNsService(IHIRNsRepository hirnsRepository)
    {
        _hirnsRepository = hirnsRepository;
    }

    public static HIRNsService Create()
    {
        if (_instance == null)
        {
            _instance = new HIRNsService(new HIRNsRepository());
        }
        return _instance;
    }

    public async Task<HIRNs> GetHIRNByIdAsync(string id)
    {
        return await _hirnsRepository.GetHIRNByIdAsync(id);
    }
}
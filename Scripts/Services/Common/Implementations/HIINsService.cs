using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIINsService : IHIINsService
{
    private static HIINsService _instance;
    private readonly IHIINsRepository _hiinsRepository;

    public HIINsService(IHIINsRepository hiinsRepository)
    {
        _hiinsRepository = hiinsRepository;
    }

    public static HIINsService Create()
    {
        if (_instance == null)
        {
            _instance = new HIINsService(new HIINsRepository());
        }
        return _instance;
    }

    public async Task<HIINs> GetHIINByIdAsync(string id)
    {
        return await _hiinsRepository.GetHIINByIdAsync(id);
    }
}
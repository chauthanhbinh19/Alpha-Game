using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIENsService : IHIENsService
{
    private static HIENsService _instance;
    private readonly IHIENsRepository _hiensRepository;

    public HIENsService(IHIENsRepository hiensRepository)
    {
        _hiensRepository = hiensRepository;
    }

    public static HIENsService Create()
    {
        if (_instance == null)
        {
            _instance = new HIENsService(new HIENsRepository());
        }
        return _instance;
    }

    public async Task<HIENs> GetHIENByIdAsync(string id)
    {
        return await _hiensRepository.GetHIENByIdAsync(id);
    }
}
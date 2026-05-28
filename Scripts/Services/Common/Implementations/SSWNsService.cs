using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class SSWNsService : ISSWNsService
{
    private static SSWNsService _instance;
    private readonly ISSWNsRepository _sswnsRepository;

    public SSWNsService(ISSWNsRepository sswnsRepository)
    {
        _sswnsRepository = sswnsRepository;
    }

    public static SSWNsService Create()
    {
        if (_instance == null)
        {
            _instance = new SSWNsService(new SSWNsRepository());
        }
        return _instance;
    }

    public async Task<SSWNs> GetSSWNByIdAsync(string id)
    {
        return await _sswnsRepository.GetSSWNByIdAsync(id);
    }
}
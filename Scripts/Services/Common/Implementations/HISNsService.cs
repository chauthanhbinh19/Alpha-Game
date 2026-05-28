using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HISNsService : IHISNsService
{
    private static HISNsService _instance;
    private readonly IHISNsRepository _hisnsRepository;

    public HISNsService(IHISNsRepository hisnsRepository)
    {
        _hisnsRepository = hisnsRepository;
    }

    public static HISNsService Create()
    {
        if (_instance == null)
        {
            _instance = new HISNsService(new HISNsRepository());
        }
        return _instance;
    }

    public async Task<HISNs> GetHISNByIdAsync(string id)
    {
        return await _hisnsRepository.GetHISNByIdAsync(id);
    }
}
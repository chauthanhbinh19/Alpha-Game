using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIDCsService : IHIDCsService
{
    private static HIDCsService _instance;
    private readonly IHIDCsRepository _hidcsRepository;

    public HIDCsService(IHIDCsRepository hidcsRepository)
    {
        _hidcsRepository = hidcsRepository;
    }

    public static HIDCsService Create()
    {
        if (_instance == null)
        {
            _instance = new HIDCsService(new HIDCsRepository());
        }
        return _instance;
    }

    public async Task<HIDCs> GetHIDCByIdAsync(string id)
    {
        return await _hidcsRepository.GetHIDCByIdAsync(id);
    }
}
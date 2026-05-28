using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HICBsService : IHICBsService
{
    private static HICBsService _instance;
    private readonly IHICBsRepository _hicbsRepository;

    public HICBsService(IHICBsRepository hicbsRepository)
    {
        _hicbsRepository = hicbsRepository;
    }

    public static HICBsService Create()
    {
        if (_instance == null)
        {
            _instance = new HICBsService(new HICBsRepository());
        }
        return _instance;
    }

    public async Task<HICBs> GetHICBByIdAsync(string id)
    {
        return await _hicbsRepository.GetHICBByIdAsync(id);
    }
}
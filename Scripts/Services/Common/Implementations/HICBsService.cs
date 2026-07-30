using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HICBsService : IHICBsService
{
    private readonly IHICBsRepository _hicbsRepository;

    public HICBsService(IHICBsRepository hicbsRepository)
    {
        _hicbsRepository = hicbsRepository;
    }

    public static IHICBsService Create() => ServiceContainer.GetService<IHICBsService>();

    public async Task<HICBs> GetHICBByIdAsync(string id)
    {
        return await _hicbsRepository.GetHICBByIdAsync(id);
    }
}
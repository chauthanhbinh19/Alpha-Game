using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIINsService : IHIINsService
{
    private readonly IHIINsRepository _hiinsRepository;

    public HIINsService(IHIINsRepository hiinsRepository)
    {
        _hiinsRepository = hiinsRepository;
    }

    public static IHIINsService Create() => ServiceContainer.GetService<IHIINsService>();

    public async Task<HIINs> GetHIINByIdAsync(string id)
    {
        return await _hiinsRepository.GetHIINByIdAsync(id);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIRNsService : IHIRNsService
{
    private readonly IHIRNsRepository _hirnsRepository;

    public HIRNsService(IHIRNsRepository hirnsRepository)
    {
        _hirnsRepository = hirnsRepository;
    }

    public static IHIRNsService Create() => ServiceContainer.GetService<IHIRNsService>();

    public async Task<HIRNs> GetHIRNByIdAsync(string id)
    {
        return await _hirnsRepository.GetHIRNByIdAsync(id);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UpgradesService : IUpgradesService
{
    private static UpgradesService _instance;
    private readonly IUpgradesRepository _upgradesRepository;

    public UpgradesService(IUpgradesRepository upgradesRepository)
    {
        _upgradesRepository = upgradesRepository;
    }

    public static UpgradesService Create()
    {
        if (_instance == null)
        {
            _instance = new UpgradesService(new UpgradesRepository());
        }
        return _instance;
    }

    public async Task<Upgrades> GetUpgradeByIdAsync(string id)
    {
        return await _upgradesRepository.GetUpgradeByIdAsync(id);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UpgradesService : IUpgradesService
{
    private readonly IUpgradesRepository _upgradesRepository;

    public UpgradesService(IUpgradesRepository upgradesRepository)
    {
        _upgradesRepository = upgradesRepository;
    }

    public static IUpgradesService Create() => ServiceContainer.GetService<IUpgradesService>();

    public async Task<Upgrades> GetUpgradeByIdAsync(string id)
    {
        return await _upgradesRepository.GetUpgradeByIdAsync(id);
    }
}
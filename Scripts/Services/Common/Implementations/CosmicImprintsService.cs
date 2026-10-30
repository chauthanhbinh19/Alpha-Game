using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class CosmicImprintsService : ICosmicImprintsService
{
    private readonly ICosmicImprintsRepository _hisnsRepository;

    public CosmicImprintsService(ICosmicImprintsRepository hisnsRepository)
    {
        _hisnsRepository = hisnsRepository;
    }

    public static ICosmicImprintsService Create() => ServiceContainer.GetService<ICosmicImprintsService>();

    public async Task<CosmicImprints> GetCosmicImprintByIdAsync(string id)
    {
        return await _hisnsRepository.GetCosmicImprintByIdAsync(id);
    }
    public Task<InsertOrUpdateResult<CosmicImprints>> InsertCosmicImprintAsync(CosmicImprints hisn)
    {
        return _hisnsRepository.InsertCosmicImprintAsync(hisn);
    }

    public Task<InsertOrUpdateResult<CosmicImprints>> UpdateCosmicImprintAsync(CosmicImprints hisn)
    {
        return _hisnsRepository.UpdateCosmicImprintAsync(hisn);
    }
}
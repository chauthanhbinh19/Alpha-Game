using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class PathOfLordImprintsService : IPathOfLordImprintsService
{
    private readonly IPathOfLordImprintsRepository _hisnsRepository;

    public PathOfLordImprintsService(IPathOfLordImprintsRepository hisnsRepository)
    {
        _hisnsRepository = hisnsRepository;
    }

    public static IPathOfLordImprintsService Create() => ServiceContainer.GetService<IPathOfLordImprintsService>();

    public async Task<PathOfLordImprints> GetPathOfLordImprintByIdAsync(string id)
    {
        return await _hisnsRepository.GetPathOfLordImprintByIdAsync(id);
    }
    public Task<InsertOrUpdateResult<PathOfLordImprints>> InsertPathOfLordImprintAsync(PathOfLordImprints hisn)
    {
        return _hisnsRepository.InsertPathOfLordImprintAsync(hisn);
    }

    public Task<InsertOrUpdateResult<PathOfLordImprints>> UpdatePathOfLordImprintAsync(PathOfLordImprints hisn)
    {
        return _hisnsRepository.UpdatePathOfLordImprintAsync(hisn);
    }
}
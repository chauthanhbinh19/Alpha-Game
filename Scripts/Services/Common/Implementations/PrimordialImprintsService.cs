using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class PrimordialImprintsService : IPrimordialImprintsService
{
    private readonly IPrimordialImprintsRepository _hisnsRepository;

    public PrimordialImprintsService(IPrimordialImprintsRepository hisnsRepository)
    {
        _hisnsRepository = hisnsRepository;
    }

    public static IPrimordialImprintsService Create() => ServiceContainer.GetService<IPrimordialImprintsService>();

    public async Task<PrimordialImprints> GetPrimordialImprintByIdAsync(string id)
    {
        return await _hisnsRepository.GetPrimordialImprintByIdAsync(id);
    }
    public Task<InsertOrUpdateResult<PrimordialImprints>> InsertPrimordialImprintAsync(PrimordialImprints hisn)
    {
        return _hisnsRepository.InsertPrimordialImprintAsync(hisn);
    }

    public Task<InsertOrUpdateResult<PrimordialImprints>> UpdatePrimordialImprintAsync(PrimordialImprints hisn)
    {
        return _hisnsRepository.UpdatePrimordialImprintAsync(hisn);
    }
}
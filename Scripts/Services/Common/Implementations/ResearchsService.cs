using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class ResearchsService : IResearchsService
{
    private readonly IResearchsRepository _researchsRepository;

    public ResearchsService(IResearchsRepository researchsRepository)
    {
        _researchsRepository = researchsRepository;
    }

    public static IResearchsService Create() => ServiceContainer.GetService<IResearchsService>();

    public async Task<Researchs> GetResearchByIdAsync(string id)
    {
        return await _researchsRepository.GetResearchByIdAsync(id);
    }
    public Task<InsertOrUpdateResult<Researchs>> InsertResearchAsync(Researchs research)
    {
        return _researchsRepository.InsertResearchAsync(research);
    }

    public Task<InsertOrUpdateResult<Researchs>> UpdateResearchAsync(Researchs research)
    {
        return _researchsRepository.UpdateResearchAsync(research);
    }
}
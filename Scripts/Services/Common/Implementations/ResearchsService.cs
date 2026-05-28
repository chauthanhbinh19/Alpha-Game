using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class ResearchsService : IResearchsService
{
    private static ResearchsService _instance;
    private readonly IResearchsRepository _researchsRepository;

    public ResearchsService(IResearchsRepository researchsRepository)
    {
        _researchsRepository = researchsRepository;
    }

    public static ResearchsService Create()
    {
        if (_instance == null)
        {
            _instance = new ResearchsService(new ResearchsRepository());
        }
        return _instance;
    }

    public async Task<Researchs> GetResearchByIdAsync(string id)
    {
        return await _researchsRepository.GetResearchByIdAsync(id);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class ArchivesService : IArchivesService
{
    private static ArchivesService _instance;
    private readonly IArchivesRepository _archivesRepository;

    public ArchivesService(IArchivesRepository archivesRepository)
    {
        _archivesRepository = archivesRepository;
    }

    public static ArchivesService Create()
    {
        if (_instance == null)
        {
            _instance = new ArchivesService(new ArchivesRepository());
        }
        return _instance;
    }

    public async Task<Archives> GetArchiveByIdAsync(string id)
    {
        return await _archivesRepository.GetArchiveByIdAsync(id);
    }
}
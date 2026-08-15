using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class ArchivesService : IArchivesService
{
    private readonly IArchivesRepository _archivesRepository;

    public ArchivesService(IArchivesRepository archivesRepository)
    {
        _archivesRepository = archivesRepository;
    }

    public static IArchivesService Create() => ServiceContainer.GetService<IArchivesService>();

    public async Task<Archives> GetArchiveByIdAsync(string id)
    {
        return await _archivesRepository.GetArchiveByIdAsync(id);
    }
    public Task<InsertOrUpdateResult<Archives>> InsertArchiveAsync(Archives archive)
    {
        return _archivesRepository.InsertArchiveAsync(archive);
    }

    public Task<InsertOrUpdateResult<Archives>> UpdateArchiveAsync(Archives archive)
    {
        return _archivesRepository.UpdateArchiveAsync(archive);
    }
}
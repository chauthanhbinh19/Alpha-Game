using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public interface IUserArtworksService
{
    Task<List<Artworks>> GetUserArtworksAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserArtworksCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtworkAsync(string userId, Artworks artwork);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtworksBatchAsync(string userId, List<Artworks> artworks);
    Task<bool> UpdateUserArtworkLevelAsync(string userId, Artworks artwork);
    Task<bool> UpdateUserArtworkStarAsync(string userId, Artworks artwork);
    Task<Artworks> GetUserArtworkByIdAsync(string userId, string Id);
    Task<Artworks> SumPowerUserArtworksAsync(string userId);
}

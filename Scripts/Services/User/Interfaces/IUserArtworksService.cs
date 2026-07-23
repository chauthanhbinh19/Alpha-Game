using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public interface IUserArtworksService
{
    Task<List<Artworks>> GetUserArtworksAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserArtworksCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserArtworkAsync(Artworks artwork, string userId);
    Task<bool> InsertOrUpdateUserArtworksBatchAsync(string userId, List<Artworks> artworks);
    Task<bool> UpdateUserArtworkLevelAsync(string userId, Artworks artwork);
    Task<bool> UpdateUserArtworkStarAsync(string userId, Artworks artwork);
    Task<bool> UpdateUserArtworkBreakthroughAsync(string userId, Artworks artwork, int star, double quantity);
    Task<Artworks> GetUserArtworkByIdAsync(string userId, string Id);
    Task<Artworks> SumPowerUserArtworksAsync(string userId);
}

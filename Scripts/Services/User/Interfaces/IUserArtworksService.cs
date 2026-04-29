using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public interface IUserArtworksService
{
    Task<List<Artworks>> GetUserArtworksAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserArtworksCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserArtworkAsync(Artworks artwork, string userId);
    Task<bool> InsertOrUpdateUserArtworksBatchAsync(List<Artworks> artworks);
    Task<bool> UpdateArtworkLevelAsync(Artworks artwork, int cardLevel);
    Task<bool> UpdateArtworkBreakthroughAsync(Artworks artwork, int star, double quantity);
    Task<Artworks> GetUserArtworkByIdAsync(string user_id, string Id);
    Task<Artworks> SumPowerUserArtworksAsync();
}

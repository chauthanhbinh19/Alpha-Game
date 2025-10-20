using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IUserArtworkService
{
    Artworks GetNewLevelPower(Artworks c, double coefficient);
    Artworks GetNewBreakthroughPower(Artworks c, double coefficient);
    List<Artworks> GetUserArtwork(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserArtworkCount(string user_id, string type, string rare);
    bool InsertUserArtwork(Artworks Artwork);
    bool UpdateArtworkLevel(Artworks Artwork, int cardLevel);
    bool UpdateArtworkBreakthrough(Artworks Artwork, int star, int quantity);
    Artworks GetUserArtworkById(string user_id, string Id);
    Artworks SumPowerUserArtwork();
}

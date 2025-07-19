using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IUserArtworkService
{
    Artwork GetNewLevelPower(Artwork c, double coefficient);
    Artwork GetNewBreakthroughPower(Artwork c, double coefficient);
    List<Artwork> GetUserArtwork(string user_id, string type, int pageSize, int offset);
    int GetUserArtworkCount(string user_id, string type);
    bool InsertUserArtwork(Artwork Artwork);
    bool UpdateArtworkLevel(Artwork Artwork, int cardLevel);
    bool UpdateArtworkBreakthrough(Artwork Artwork, int star, int quantity);
    Artwork GetUserArtworkById(string user_id, string Id);
    Artwork SumPowerUserArtwork();
}

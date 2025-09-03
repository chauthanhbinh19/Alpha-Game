using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IArtworkGalleryService
{
    List<Artwork> GetArtworkCollection(string type, int pageSize, int offset, string rare);
    int GetArtworkCount(string type, string rare);
    void InsertArtworkGallery(string Id);
    void UpdateStatusArtworkGallery(string Id);
    void UpdateStarArtworkGallery(string Id, double star);
    void UpdateArtworkGalleryPower(string Id);
    Artwork SumPowerArtworkGallery();
}

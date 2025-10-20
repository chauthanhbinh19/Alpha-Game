using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IArtworkGalleryRepository
{
    List<Artworks> GetArtworkCollection(string type, int pageSize, int offset, string rare);
    int GetArtworkCount(string type, string rare);
    void InsertArtworkGallery(string Id, Artworks ArtworkFromDB);
    void UpdateStatusArtworkGallery(string Id);
    void UpdateStarArtworkGallery(string Id, double star);
    void UpdateArtworkGalleryPower(string Id, Artworks ArtworkFromDB);
    Artworks SumPowerArtworkGallery();
}

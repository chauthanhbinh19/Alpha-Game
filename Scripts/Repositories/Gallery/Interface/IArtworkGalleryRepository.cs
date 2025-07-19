using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IArtworkGalleryRepository
{
    List<Artwork> GetArtworkCollection(string type, int pageSize, int offset);
    int GetArtworkCount(string type);
    void InsertArtworkGallery(string Id, Artwork ArtworkFromDB);
    void UpdateStatusArtworkGallery(string Id);
    Artwork SumPowerArtworkGallery();
}

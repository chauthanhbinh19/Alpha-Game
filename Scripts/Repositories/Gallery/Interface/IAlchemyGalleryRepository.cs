using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IAlchemyGalleryRepository
{
    List<Alchemies> GetAlchemyCollection(string type, int pageSize, int offset, string rare);
    int GetAlchemyCount(string type, string rare);
    void InsertAlchemyGallery(string Id, Alchemies AlchemyFromDB);
    void UpdateStatusAlchemyGallery(string Id);
    void UpdateStarAlchemyGallery(string Id, double star);
    void UpdateAlchemyGalleryPower(string Id, Alchemies AlchemyFromDB);
    Alchemies SumPowerAlchemyGallery();
}

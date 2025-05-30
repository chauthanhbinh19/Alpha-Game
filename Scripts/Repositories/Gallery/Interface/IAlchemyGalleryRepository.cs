using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IAlchemyGalleryRepository
{
    List<Alchemy> GetAlchemyCollection(string type, int pageSize, int offset);
    int GetAlchemyCount(string type);
    void InsertAlchemyGallery(string Id, Alchemy AlchemyFromDB);
    void UpdateStatusAlchemyGallery(string Id);
    Alchemy SumPowerAlchemyGallery();
}

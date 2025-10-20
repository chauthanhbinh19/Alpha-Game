using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IUserAlchemyRepository
{
    List<Alchemies> GetUserAlchemy(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserAlchemyCount(string user_id, string type, string rare);
    bool InsertUserAlchemy(Alchemies Alchemy);
    bool UpdateAlchemyLevel(Alchemies Alchemy, int cardLevel);
    bool UpdateAlchemyBreakthrough(Alchemies Alchemy, int star, int quantity);
    Alchemies GetUserAlchemyById(string user_id, string Id);
    Alchemies SumPowerUserAlchemy();
}

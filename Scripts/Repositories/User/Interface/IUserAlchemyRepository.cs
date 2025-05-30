using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IUserAlchemyRepository
{
    List<Alchemy> GetUserAlchemy(string user_id, string type, int pageSize, int offset);
    int GetUserAlchemyCount(string user_id, string type);
    bool InsertUserAlchemy(Alchemy Alchemy);
    bool UpdateAlchemyLevel(Alchemy Alchemy, int cardLevel);
    bool UpdateAlchemyBreakthrough(Alchemy Alchemy, int star, int quantity);
    Alchemy GetUserAlchemyById(string user_id, string Id);
    Alchemy SumPowerUserAlchemy();
}

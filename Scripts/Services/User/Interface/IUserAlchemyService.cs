using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IUserAlchemyService
{
    Alchemies GetNewLevelPower(Alchemies c, double coefficient);
    Alchemies GetNewBreakthroughPower(Alchemies c, double coefficient);
    List<Alchemies> GetUserAlchemy(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserAlchemyCount(string user_id, string type, string rare);
    bool InsertUserAlchemy(Alchemies Alchemy, string userId);
    bool UpdateAlchemyLevel(Alchemies Alchemy, int cardLevel);
    bool UpdateAlchemyBreakthrough(Alchemies Alchemy, int star, double quantity);
    Alchemies GetUserAlchemyById(string user_id, string Id);
    Alchemies SumPowerUserAlchemy();
}

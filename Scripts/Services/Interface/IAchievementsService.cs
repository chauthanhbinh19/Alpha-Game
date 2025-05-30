using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IAchievementsService
{
    List<Achievements> GetAchievement(int pageSize, int offset);
    int GetAchievementCount();
    Achievements GetAchievementsById(string Id);
    List<Achievements> GetAchievementsWithPrice(int pageSize, int offset);
    int GetAchievementsWithPriceCount();
    Achievements SumPowerAchievementsPercent();
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IAchievementsRepository
{
    List<Achievements> GetAchievement(int pageSize, int offset, string rare);
    int GetAchievementCount(string rare);
    Achievements GetAchievementsById(string Id);
    List<Achievements> GetAchievementsWithPrice(int pageSize, int offset);
    int GetAchievementsWithPriceCount();
    Achievements SumPowerAchievementsPercent();
}
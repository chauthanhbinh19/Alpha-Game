using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IAchievementsGalleryService
{
    List<Achievements> GetAchievementCollection(int pageSize, int offset, string rare);
    int GetAchievementsCount(string rare);
    void InsertAchievementsGallery(string Id, Achievements AchievementFromDB);
    void UpdateStatusAchievementsGallery(string Id);
    void UpdateStarAchievementsGallery(string Id, double star);
    void UpdateAchievementsGalleryPower(string Id, Achievements AchievementFromDB);
    Achievements SumPowerAchievementsGallery();
}
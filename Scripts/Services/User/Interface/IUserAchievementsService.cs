using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IUserAchievementsService
{
    public Achievements GetNewLevelPower(Achievements c, double coefficient);
    public Achievements GetNewBreakthroughPower(Achievements c, double coefficient);
    List<Achievements> GetUserAchievements(string user_id, int pageSize, int offset);
    int GetUserCollaborationCount(string user_id);
    bool InsertUserAchievements(Achievements Achievements);
    bool UpdateAchievementLevel(Achievements achievements, int cardLevel);
    bool UpdateAchievementsBreakthrough(Achievements achievements, int star, int quantity);
    Achievements GetUserAchievementsById(string user_id, string Id);
    Achievements SumPowerUserAchievements();
}

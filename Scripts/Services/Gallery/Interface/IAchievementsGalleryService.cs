using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IAchievementsGalleryService
{
    List<Achievements> GetAchievementCollection(int pageSize, int offset);
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class AchievementsGalleryService : IAchievementsGalleryService
{
    private IAchievementsGalleryRepository _achievementsGalleryRepository;

    public AchievementsGalleryService(IAchievementsGalleryRepository achievementsGalleryRepository)
    {
        _achievementsGalleryRepository = achievementsGalleryRepository;
    }

    public static AchievementsGalleryService Create()
    {
        return new AchievementsGalleryService(new AchievementsGalleryRepository());
    }

    public List<Achievements> GetAchievementCollection(int pageSize, int offset)
    {
        List<Achievements> list = _achievementsGalleryRepository.GetAchievementCollection(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }
}
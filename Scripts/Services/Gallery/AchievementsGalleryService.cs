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

    public List<Achievements> GetAchievementCollection(int pageSize, int offset, string rare)
    {
        List<Achievements> list = _achievementsGalleryRepository.GetAchievementCollection(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetAchievementsCount(string rare)
    {
        return _achievementsGalleryRepository.GetAchievementsCount(rare);
    }

    public void InsertAchievementsGallery(string Id, Achievements AchievementFromDB)
    {
        _achievementsGalleryRepository.InsertAchievementsGallery(Id, AchievementFromDB);
    }

    public Achievements SumPowerAchievementsGallery()
    {
        return _achievementsGalleryRepository.SumPowerAchievementsGallery();
    }

    public void UpdateAchievementsGalleryPower(string Id, Achievements AchievementFromDB)
    {
        _achievementsGalleryRepository.UpdateAchievementsGalleryPower(Id, AchievementFromDB);
    }

    public void UpdateStarAchievementsGallery(string Id, double star)
    {
        _achievementsGalleryRepository.UpdateStarAchievementsGallery(Id, star);
    }

    public void UpdateStatusAchievementsGallery(string Id)
    {
        _achievementsGalleryRepository.UpdateStatusAchievementsGallery(Id);
    }
}
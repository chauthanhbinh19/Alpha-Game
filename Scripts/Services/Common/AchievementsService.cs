using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class AchievementsService : IAchievementsService
{
    private IAchievementsRepository _achievementsRepository;

    public AchievementsService(IAchievementsRepository achievementsRepository)
    {
        _achievementsRepository = achievementsRepository;
    }

    public static AchievementsService Create()
    {
        return new AchievementsService(new AchievementsRepository());
    }


    public List<Achievements> GetAchievement(int pageSize, int offset, string rare)
    {
        List<Achievements> list = _achievementsRepository.GetAchievement(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetAchievementCount(string rare)
    {
        return _achievementsRepository.GetAchievementCount(rare);
    }

    public Achievements GetAchievementsById(string Id)
    {
        return _achievementsRepository.GetAchievementsById(Id);
    }

    public List<Achievements> GetAchievementsWithPrice(int pageSize, int offset)
    {
        List<Achievements> list = _achievementsRepository.GetAchievementsWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetAchievementsWithPriceCount()
    {
        return _achievementsRepository.GetAchievementsWithPriceCount();
    }
    
    public Achievements SumPowerAchievementsPercent()
    {
        return _achievementsRepository.SumPowerAchievementsPercent();
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campaigns 
{
    public int Id { get; set; }
    public string Chapter { get; set; }
    public string Type { get; set; }
    public string SubType { get; set; }
    public string Difficulty { get; set; }
    public int LevelRequired { get; set; }
    public string Description { get; set; }
    public List<CampaignDetail> CampaignDetails { get; set; }

    // Constructor
    public Campaigns(int id, string chapter, string type, string subType, string difficulty, int levelRequired, string description)
    {
        this.Id = id;
        this.Chapter = chapter;
        this.Type = type;
        this.SubType = subType;
        this.Difficulty = difficulty;
        this.LevelRequired = levelRequired;
        this.Description = description;
        CampaignDetails = new List<CampaignDetail>();
    }
    public Campaigns(){
        CampaignDetails = new List<CampaignDetail>();
    }
    // Phương thức truy vấn các CampaignDetails của chiến dịch
    public CampaignDetail GetCampaignDetailById(int detailId)
    {
        return CampaignDetails.Find(detail => detail.Id == detailId);
    }
    // Phương thức truy vấn các thẻ (cards) của chiến dịch
    public List<CardHeroes> GetCardsForCampaign(List<CardHeroes> allCards)
    {
        List<CardHeroes> cards = new List<CardHeroes>();
        foreach (var campaignDetail in CampaignDetails)
        {
            cards.AddRange(campaignDetail.GetCards(allCards));
        }
        return cards;
    }
}

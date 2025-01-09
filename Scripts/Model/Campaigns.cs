using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campaigns 
{
    public int id { get; set; }
    public string chapter { get; set; }
    public string type { get; set; }
    public string subType { get; set; }
    public string difficulty { get; set; }
    public int levelRequired { get; set; }
    public string description { get; set; }
    public List<CampaignDetail> campaignDetails { get; set; }

    // Constructor
    public Campaigns(int id, string chapter, string type, string subType, string difficulty, int levelRequired, string description)
    {
        this.id = id;
        this.chapter = chapter;
        this.type = type;
        this.subType = subType;
        this.difficulty = difficulty;
        this.levelRequired = levelRequired;
        this.description = description;
        campaignDetails = new List<CampaignDetail>();
    }
    public Campaigns(){
        campaignDetails = new List<CampaignDetail>();
    }
    // Phương thức truy vấn các CampaignDetails của chiến dịch
    public CampaignDetail GetCampaignDetailById(int detailId)
    {
        return campaignDetails.Find(detail => detail.id == detailId);
    }
    // Phương thức truy vấn các thẻ (cards) của chiến dịch
    public List<CardHeroes> GetCardsForCampaign(List<CardHeroes> allCards)
    {
        List<CardHeroes> cards = new List<CardHeroes>();
        foreach (var campaignDetail in campaignDetails)
        {
            cards.AddRange(campaignDetail.GetCards(allCards));
        }
        return cards;
    }
}

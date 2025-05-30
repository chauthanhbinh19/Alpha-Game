using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campaigns 
{
    private int id1;
    private string chapter1;
    private string type1;
    private string subType1;
    private string difficulty1;
    private int levelRequired1;
    private string description1;

    public int id { get => id1; set => id1 = value; }
    public string chapter { get => chapter1; set => chapter1 = value; }
    public string type { get => type1; set => type1 = value; }
    public string subType { get => subType1; set => subType1 = value; }
    public string difficulty { get => difficulty1; set => difficulty1 = value; }
    public int levelRequired { get => levelRequired1; set => levelRequired1 = value; }
    public string description { get => description1; set => description1 = value; }
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

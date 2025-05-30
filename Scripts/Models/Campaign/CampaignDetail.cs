using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignDetail
{
    private int campaignId1;
    private int id1;
    private string chapter1;
    private string name1;
    private string difficulty1;
    private int levelRequired1;
    private double strengthMultiplier1;
    private bool isActive1;
    private string description1;
    private string status1;
    private int stars1;

    public int campaignId { get => campaignId1; set => campaignId1 = value; }
    public int id { get => id1; set => id1 = value; }
    public string chapter { get => chapter1; set => chapter1 = value; }
    public string name { get => name1; set => name1 = value; }
    public string difficulty { get => difficulty1; set => difficulty1 = value; }
    public int levelRequired { get => levelRequired1; set => levelRequired1 = value; }
    public double strengthMultiplier { get => strengthMultiplier1; set => strengthMultiplier1 = value; }
    public bool isActive { get => isActive1; set => isActive1 = value; }
    public string description { get => description1; set => description1 = value; }
    public string status { get => status1; set => status1 = value; }
    public int stars { get => stars1; set => stars1 = value; }
    // Danh sách các CampaignReward (mối quan hệ one-to-many)
    public List<CampaignReward> campaignRewards { get; set; }
    // Danh sách các CampaignDetailCard (mối quan hệ many-to-many)
    public List<CampaignDetailCard> campaignDetailCards { get; set; }

    // Constructor
    public CampaignDetail(int campaignId, int id, string chapter, string name, string difficulty, int levelRequired, double strengthMultiplier, bool isActive, string description)
    {
        this.campaignId = campaignId;
        this.id = id;
        this.chapter = chapter;
        this.name = name;
        this.difficulty = difficulty;
        this.levelRequired = levelRequired;
        this.strengthMultiplier = strengthMultiplier;
        this.isActive = isActive;
        this.description = description;
        campaignRewards = new List<CampaignReward>();
        campaignDetailCards = new List<CampaignDetailCard>();
    }
    public CampaignDetail(){
        campaignRewards = new List<CampaignReward>();
        campaignDetailCards = new List<CampaignDetailCard>();
    }
    // Phương thức truy vấn phần thưởng (rewards) cho CampaignDetail
    public CampaignReward GetRewardByItemId(int itemId)
    {
        return campaignRewards.Find(reward => reward.itemId == itemId);
    }
    // Phương thức truy vấn thẻ (cards) cho CampaignDetail
    public List<CardHeroes> GetCards(List<CardHeroes> allCards)
    {
        List<CardHeroes> result = new List<CardHeroes>();
        foreach (var detailCard in campaignDetailCards)
        {
            var card = allCards.Find(c => c.id.Equals(detailCard.cardId));
            if (card != null)
                result.Add(card);
        }
        return result;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignDetail
{
    public int campaignId { get; set; }
    public int id{ get; set; }
    public string chapter { get; set; }
    public string name { get; set; }
    public string difficulty { get; set; }
    public int levelRequired { get; set; }
    public double strengthMultiplier { get; set; }
    public bool isActive { get; set; }
    public string description { get; set; }
    public string status { get; set; }
    public int stars { get; set; }
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
            var card = allCards.Find(c => c.id == detailCard.cardId);
            if (card != null)
                result.Add(card);
        }
        return result;
    }

}

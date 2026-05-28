using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignDetail
{
    public int CampaignId { get; set; }
    public int Id { get; set; }
    public string Chapter { get; set; }
    public string Name { get; set; }
    public string Difficulty { get; set; }
    public int LevelRequired { get; set; }
    public double StrengthMultiplier { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public int Stars { get; set; }
    // Danh sách các CampaignReward (mối quan hệ one-to-many)
    public List<CampaignReward> CampaignRewards { get; set; }
    // Danh sách các CampaignDetailCard (mối quan hệ many-to-many)
    public List<CampaignDetailCard> CampaignDetailCards { get; set; }

    // Constructor
    public CampaignDetail(int campaignId, int id, string chapter, string name, string difficulty, int levelRequired, double strengthMultiplier, bool isActive, string description)
    {
        this.CampaignId = campaignId;
        this.Id = id;
        this.Chapter = chapter;
        this.Name = name;
        this.Difficulty = difficulty;
        this.LevelRequired = levelRequired;
        this.StrengthMultiplier = strengthMultiplier;
        this.IsActive = isActive;
        this.Description = description;
        CampaignRewards = new List<CampaignReward>();
        CampaignDetailCards = new List<CampaignDetailCard>();
    }
    public CampaignDetail(){
        CampaignRewards = new List<CampaignReward>();
        CampaignDetailCards = new List<CampaignDetailCard>();
    }
    // Phương thức truy vấn phần thưởng (rewards) cho CampaignDetail
    public CampaignReward GetRewardByItemId(int itemId)
    {
        return CampaignRewards.Find(reward => reward.ItemId == itemId);
    }
    // Phương thức truy vấn thẻ (cards) cho CampaignDetail
    public List<Equipments> GetCards(List<Equipments> allCards)
    {
        List<Equipments> result = new List<Equipments>();
        foreach (var detailCard in CampaignDetailCards)
        {
            var card = allCards.Find(c => c.Id.Equals(detailCard.CardId));
            if (card != null)
                result.Add(card);
        }
        return result;
    }

}

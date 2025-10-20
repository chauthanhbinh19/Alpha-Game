using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignDetailCard
{
    public int CampaignId { get; set; }
    public int CampaignDetailId { get; set; }
    public int CardId { get; set; }
    public string Chapter { get; set; }
    public CardHeroes Cards{ get; set; }
    // Constructor
    public CampaignDetailCard(int campaignId, int campaignDetailId, int cardId, string chapter)
    {
        this.CampaignId = campaignId;
        this.CampaignDetailId = campaignDetailId;
        this.CardId = cardId;
        this.Chapter = chapter;
    }
    public CampaignDetailCard(){
        
    }
}

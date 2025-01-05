using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignDetailCard
{
    public int campaignId { get; set; }
    public int campaignDetailId { get; set; }
    public int cardId { get; set; }
    public string chapter { get; set; }
    public Cards cards{ get; set; }
    // Constructor
    public CampaignDetailCard(int campaignId, int campaignDetailId, int cardId, string chapter)
    {
        this.campaignId = campaignId;
        this.campaignDetailId = campaignDetailId;
        this.cardId = cardId;
        this.chapter = chapter;
    }
    public CampaignDetailCard(){
        
    }
}

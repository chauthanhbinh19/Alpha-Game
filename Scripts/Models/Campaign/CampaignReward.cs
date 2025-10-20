using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignReward
{
    public int CampaignId { get; set; }
    public int CampaignDetailId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public string Chapter { get; set; }
    public Items Items{ get; set; }
    // Constructor
    public CampaignReward(int campaignId, int campaignDetailId, int itemId, int quantity, string chapter)
    {
        this.CampaignId = campaignId;
        this.CampaignDetailId = campaignDetailId;
        this.ItemId = itemId;
        this.Quantity = quantity;
        this.Chapter = chapter;
    }
    public CampaignReward(){
        
    }
}

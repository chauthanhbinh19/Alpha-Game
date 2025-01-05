using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignReward
{
    public int campaignId { get; set; }
    public int campaignDetailId { get; set; }
    public int itemId { get; set; }
    public int quantity { get; set; }
    public string chapter { get; set; }
    public Items items{ get; set; }
    // Constructor
    public CampaignReward(int campaignId, int campaignDetailId, int itemId, int quantity, string chapter)
    {
        this.campaignId = campaignId;
        this.campaignDetailId = campaignDetailId;
        this.itemId = itemId;
        this.quantity = quantity;
        this.chapter = chapter;
    }
    public CampaignReward(){
        
    }
}

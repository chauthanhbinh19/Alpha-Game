using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignReward
{
    private int campaignId1;
    private int campaignDetailId1;
    private int itemId1;
    private int quantity1;
    private string chapter1;

    public int campaignId { get => campaignId1; set => campaignId1 = value; }
    public int campaignDetailId { get => campaignDetailId1; set => campaignDetailId1 = value; }
    public int itemId { get => itemId1; set => itemId1 = value; }
    public int quantity { get => quantity1; set => quantity1 = value; }
    public string chapter { get => chapter1; set => chapter1 = value; }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignDetailCard
{
    private int campaignId1;
    private int campaignDetailId1;
    private int cardId1;
    private string chapter1;

    public int campaignId { get => campaignId1; set => campaignId1 = value; }
    public int campaignDetailId { get => campaignDetailId1; set => campaignDetailId1 = value; }
    public int cardId { get => cardId1; set => cardId1 = value; }
    public string chapter { get => chapter1; set => chapter1 = value; }
    public CardHeroes cards{ get; set; }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserCampaign
{
    private int userId1;
    private int campaignId1;
    private int campaignDetailId1;
    private int stars1;
    private DateTime completionTime1;
    private bool isCompleted1;

    public int userId { get => userId1; set => userId1 = value; }
    public int campaignId { get => campaignId1; set => campaignId1 = value; }
    public int campaignDetailId { get => campaignDetailId1; set => campaignDetailId1 = value; }
    public int stars { get => stars1; set => stars1 = value; }
    public DateTime completionTime { get => completionTime1; set => completionTime1 = value; }
    public bool isCompleted { get => isCompleted1; set => isCompleted1 = value; }

    // Constructor
    public UserCampaign(int userId, int campaignId, int campaignDetailId, int stars, System.DateTime completionTime, bool isCompleted)
    {
        this.userId = userId;
        this.campaignId = campaignId;
        this.campaignDetailId = campaignDetailId;
        this.stars = stars;
        this.completionTime = completionTime;
        this.isCompleted = isCompleted;
    }
    public UserCampaign()
    {

    }

    // Phương thức truy vấn Campaign của UserCampaign
    public Campaigns GetCampaign(List<Campaigns> allCampaigns)
    {
        return allCampaigns.Find(campaign => campaign.Id == campaignId);
    }

    // Phương thức truy vấn CampaignDetail của UserCampaign
    public CampaignDetail GetCampaignDetail(List<Campaigns> allCampaigns)
    {
        var campaign = GetCampaign(allCampaigns);
        if (campaign != null)
        {
            return campaign.GetCampaignDetailById(campaignDetailId);
        }
        return null;
    }
    // Phương thức truy vấn thẻ (cards) của UserCampaign
    public List<Equipments> GetCards(List<Campaigns> allCampaigns, List<Equipments> allCards)
    {
        var campaign = GetCampaign(allCampaigns);
        return campaign?.GetCardsForCampaign(allCards);
    }
    // public Campaigns GetCampaignsForUser(string chapter, string type)
    // {
    //     Campaigns campaign = new Campaigns();
    //     List<CampaignDetail> campaignDetails = new List<CampaignDetail>();
    //     List<CampaignReward> campaignRewards = new List<CampaignReward>();
    //     List<CampaignDetailCard> campaignDetailCards = new List<CampaignDetailCard>();
    //     string connectionString = DatabaseConfig.ConnectionString;
    //     using (MySqlConnection connection = new MySqlConnection(connectionString))
    //     {

    //         try
    //         {
    //             connection.Open();
    //             string query = @"select *
    //             from campaigns c
    //             where  c.chapter = @chapter and c.type =@type;";
    //             MySqlCommand command = new MySqlCommand(query, connection);
    //             command.Parameters.AddWithValue("@chapter", chapter);
    //             command.Parameters.AddWithValue("@type", type);
    //             MySqlDataReader reader = command.ExecuteReader();
    //             while (reader.Read())
    //             {
    //                 campaign = new Campaigns
    //                 {
    //                     Id = reader.GetInt32("id"),
    //                     Chapter = reader.GetString("chapter"),
    //                     Type = reader.GetString("type"),
    //                     SubType = reader.GetString("sub_type"),
    //                     Difficulty = reader.GetString("difficulty"),
    //                     LevelRequired = reader.GetInt32("level_required"),
    //                     Description = reader.GetString("description"),
    //                 };
    //             }

    //             reader.Close();
    //             query = @"SELECT cd.*, CASE WHEN uc.campaign_detail_id IS NULL THEN 'block' ELSE 'available' END AS status,
    //             CASE WHEN uc.stars IS NULL THEN '0' ELSE uc.stars END AS stars
    //             FROM campaigns c LEFT JOIN campaign_details cd ON c.id = cd.campaign_id AND c.chapter = cd.chapter
    //             LEFT JOIN user_campaign uc ON uc.campaign_id = c.id AND uc.campaign_detail_id = cd.id AND uc.user_id = @user_id
    //             WHERE c.chapter = @chapter AND c.type = @type ;";
    //             command = new MySqlCommand(query, connection);
    //             command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
    //             command.Parameters.AddWithValue("@chapter", chapter);
    //             command.Parameters.AddWithValue("@type", type);
    //             reader = command.ExecuteReader();
    //             while (reader.Read())
    //             {
    //                 CampaignDetail campaignDetail = new CampaignDetail
    //                 {
    //                     CampaignId = reader.GetInt32("campaign_id"),
    //                     Id = reader.GetInt32("id"),
    //                     Chapter = reader.GetString("chapter"),
    //                     Name = reader.GetString("name"),
    //                     Difficulty = reader.GetString("difficulty"),
    //                     LevelRequired = reader.GetInt32("level_required"),
    //                     StrengthMultiplier = reader.GetDouble("strength_multiplier"),
    //                     IsActive = reader.GetBoolean("is_active"),
    //                     Description = reader.GetString("description"),
    //                     Status = reader.GetString("status"),
    //                     Stars = reader.GetInt32("stars"),
    //                 };
    //                 campaignDetails.Add(campaignDetail);
    //             }
    //             reader.Close();
    //             foreach (CampaignDetail cd in campaignDetails)
    //             {
    //                 query = @"
    //             select distinct cr.*, i.name, i.image
    //             from campaigns c, campaign_details cd, campaign_rewards cr, items i
    //             where c.id = cd.campaign_id and cd.chapter=c.chapter and c.chapter = @chapter and c.type =@type
    //             	and cr.campaign_id = c.id and cr.campaign_detail_id and i.id=cr.item_id and cr.chapter = @chapter1 and cr.campaign_detail_id=@campaign_detail_id;";
    //                 command = new MySqlCommand(query, connection);
    //                 command.Parameters.AddWithValue("@chapter", chapter);
    //                 command.Parameters.AddWithValue("@chapter1", chapter);
    //                 command.Parameters.AddWithValue("@type", type);
    //                 command.Parameters.AddWithValue("@campaign_detail_id", cd.Id);
    //                 reader = command.ExecuteReader();
    //                 while (reader.Read())
    //                 {
    //                     CampaignReward campaignReward = new CampaignReward
    //                     {
    //                         CampaignId = reader.GetInt32("campaign_id"),
    //                         CampaignDetailId = reader.GetInt32("campaign_detail_id"),
    //                         ItemId = reader.GetInt32("item_id"),
    //                         Quantity = reader.GetInt32("quantity"),
    //                         Chapter = reader.GetString("chapter"),
    //                     };
    //                     campaignReward.Items=new Items{
    //                         Name = reader.GetString("name"),
    //                         Image = reader.GetString("image"),
    //                     };
    //                     cd.CampaignRewards.Add(campaignReward);
    //                 }
    //                 // cd.campaignRewards = campaignRewards;
    //                 reader.Close();
    //             }
    //             reader.Close();
    //             foreach (CampaignDetail cd in campaignDetails)
    //             {
    //                 query = @"
    //             select distinct cdc.*, card_heroes.image
    //             from campaigns c, campaign_details cd, campaign_detail_card_heroes cdc, card_heroes
    //             where c.id = cd.campaign_id and cd.chapter=c.chapter and c.chapter = @chapter and c.type =@type and card_heroes.id=cdc.card_hero_id
	//             and cdc.campaign_id = c.id and cdc.campaign_detail_id and cdc.campaign_detail_id=@campaign_detail_id and cdc.chapter= @chapter1;";
    //                 command = new MySqlCommand(query, connection);
    //                 command.Parameters.AddWithValue("@chapter", chapter);
    //                 command.Parameters.AddWithValue("@chapter1", chapter);
    //                 command.Parameters.AddWithValue("@type", type);
    //                 command.Parameters.AddWithValue("@campaign_detail_id", cd.Id);
    //                 reader = command.ExecuteReader();
    //                 while (reader.Read())
    //                 {
    //                     CampaignDetailCard campaignDetailCard = new CampaignDetailCard
    //                     {
    //                         CampaignId = reader.GetInt32("campaign_id"),
    //                         CampaignDetailId = reader.GetInt32("campaign_detail_id"),
    //                         CardId = reader.GetInt32("card_hero_id"),
    //                         Chapter = reader.GetString("chapter"),
    //                     };
    //                     campaignDetailCard.Cards=new CardHeroes{
    //                         Image = reader.GetString("image"),
    //                     };
    //                     cd.CampaignDetailCards.Add(campaignDetailCard);
    //                 }
    //                 // cd.campaignDetailCards = campaignDetailCards;
    //                 reader.Close();
    //             }
    //             campaign.CampaignDetails = new List<CampaignDetail>();
    //             campaign.CampaignDetails = campaignDetails;
    //         }
    //         catch (MySqlException ex)
    //         {
    //             Debug.LogError("Error: " + ex.Message);
    //         }
    //     }

    //     return campaign;
    // }
}

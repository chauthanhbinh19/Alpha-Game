using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class UserCampaign
{
    public int userId { get; set; }
    public int campaignId { get; set; }
    public int campaignDetailId { get; set; }
    public int stars { get; set; }
    public System.DateTime completionTime { get; set; }
    public bool isCompleted { get; set; }

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
        return allCampaigns.Find(campaign => campaign.id == campaignId);
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
    public List<CardHeroes> GetCards(List<Campaigns> allCampaigns, List<CardHeroes> allCards)
    {
        var campaign = GetCampaign(allCampaigns);
        return campaign?.GetCardsForCampaign(allCards);
    }
    public Campaigns GetCampaignsForUser(string chapter, string type)
    {
        Campaigns campaign = new Campaigns();
        List<CampaignDetail> campaignDetails = new List<CampaignDetail>();
        List<CampaignReward> campaignRewards = new List<CampaignReward>();
        List<CampaignDetailCard> campaignDetailCards = new List<CampaignDetailCard>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {

            try
            {
                connection.Open();
                string query = @"select *
                from campaigns c
                where  c.chapter = @chapter and c.type =@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@chapter", chapter);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    campaign = new Campaigns
                    {
                        id = reader.GetInt32("id"),
                        chapter = reader.GetString("chapter"),
                        type = reader.GetString("type"),
                        subType = reader.GetString("sub_type"),
                        difficulty = reader.GetString("difficulty"),
                        levelRequired = reader.GetInt32("level_required"),
                        description = reader.GetString("description"),
                    };
                }

                reader.Close();
                query = @"SELECT cd.*, CASE WHEN uc.campaign_detail_id IS NULL THEN 'block' ELSE 'available' END AS status,
                CASE WHEN uc.stars IS NULL THEN '0' ELSE uc.stars END AS stars
                FROM campaigns c LEFT JOIN campaign_details cd ON c.id = cd.campaign_id AND c.chapter = cd.chapter
                LEFT JOIN user_campaign uc ON uc.campaign_id = c.id AND uc.campaign_detail_id = cd.id AND uc.user_id = @user_id
                WHERE c.chapter = @chapter AND c.type = @type ;";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@chapter", chapter);
                command.Parameters.AddWithValue("@type", type);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CampaignDetail campaignDetail = new CampaignDetail
                    {
                        campaignId = reader.GetInt32("campaign_id"),
                        id = reader.GetInt32("id"),
                        chapter = reader.GetString("chapter"),
                        name = reader.GetString("name"),
                        difficulty = reader.GetString("difficulty"),
                        levelRequired = reader.GetInt32("level_required"),
                        strengthMultiplier = reader.GetDouble("strength_multiplier"),
                        isActive = reader.GetBoolean("is_active"),
                        description = reader.GetString("description"),
                        status = reader.GetString("status"),
                        stars = reader.GetInt32("stars"),
                    };
                    campaignDetails.Add(campaignDetail);
                }
                reader.Close();
                foreach (CampaignDetail cd in campaignDetails)
                {
                    query = @"
                select distinct cr.*, i.name, i.image
                from campaigns c, campaign_details cd, campaign_rewards cr, items i
                where c.id = cd.campaign_id and cd.chapter=c.chapter and c.chapter = @chapter and c.type =@type
                	and cr.campaign_id = c.id and cr.campaign_detail_id and i.id=cr.item_id and cr.chapter = @chapter1 and cr.campaign_detail_id=@campaign_detail_id;";
                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@chapter", chapter);
                    command.Parameters.AddWithValue("@chapter1", chapter);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@campaign_detail_id", cd.id);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CampaignReward campaignReward = new CampaignReward
                        {
                            campaignId = reader.GetInt32("campaign_id"),
                            campaignDetailId = reader.GetInt32("campaign_detail_id"),
                            itemId = reader.GetInt32("item_id"),
                            quantity = reader.GetInt32("quantity"),
                            chapter = reader.GetString("chapter"),
                        };
                        campaignReward.items=new Items{
                            name = reader.GetString("name"),
                            image = reader.GetString("image"),
                        };
                        cd.campaignRewards.Add(campaignReward);
                    }
                    // cd.campaignRewards = campaignRewards;
                    reader.Close();
                }
                reader.Close();
                foreach (CampaignDetail cd in campaignDetails)
                {
                    query = @"
                select distinct cdc.*, card_heroes.image
                from campaigns c, campaign_details cd, campaign_detail_card_heroes cdc, card_heroes
                where c.id = cd.campaign_id and cd.chapter=c.chapter and c.chapter = @chapter and c.type =@type and card_heroes.id=cdc.card_hero_id
	            and cdc.campaign_id = c.id and cdc.campaign_detail_id and cdc.campaign_detail_id=@campaign_detail_id and cdc.chapter= @chapter1;";
                    command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@chapter", chapter);
                    command.Parameters.AddWithValue("@chapter1", chapter);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@campaign_detail_id", cd.id);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CampaignDetailCard campaignDetailCard = new CampaignDetailCard
                        {
                            campaignId = reader.GetInt32("campaign_id"),
                            campaignDetailId = reader.GetInt32("campaign_detail_id"),
                            cardId = reader.GetInt32("card_hero_id"),
                            chapter = reader.GetString("chapter"),
                        };
                        campaignDetailCard.cards=new CardHeroes{
                            image = reader.GetString("image"),
                        };
                        cd.campaignDetailCards.Add(campaignDetailCard);
                    }
                    // cd.campaignDetailCards = campaignDetailCards;
                    reader.Close();
                }
                campaign.campaignDetails = new List<CampaignDetail>();
                campaign.campaignDetails = campaignDetails;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return campaign;
    }
}

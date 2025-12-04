using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserSpiritCardsRepository : IUserSpiritCardsRepository
{
    public async Task<List<SpiritCards>> GetUserSpiritCardsAsync(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> SpiritCardList = new List<SpiritCards>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                Select ut.*, t.id, t.name, t.image, t.rare, t.description 
                from spirit_cards t, user_spirit_cards ut 
                where t.id = ut.spirit_card_id 
                and ut.user_id = @userId 
                and t.type = @type
                AND (@rare = 'All' OR t.rare = @rare)
                ORDER BY t.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED), 
                         t.name 
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@rare", rare);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            SpiritCards title = new SpiritCards
                            {
                                Id = reader.GetString("spirit_card_id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetDouble("quality"),
                                Star = reader.GetInt32("star"),
                                Level = reader.GetInt32("level"),
                                Experiment = reader.GetDouble("experiment"),
                                Quantity = reader.GetDouble("quantity"),
                                Power = reader.GetDouble("power"),
                                Health = reader.GetDouble("health"),
                                PhysicalAttack = reader.GetDouble("physical_attack"),
                                PhysicalDefense = reader.GetDouble("physical_defense"),
                                MagicalAttack = reader.GetDouble("magical_attack"),
                                MagicalDefense = reader.GetDouble("magical_defense"),
                                ChemicalAttack = reader.GetDouble("chemical_attack"),
                                ChemicalDefense = reader.GetDouble("chemical_defense"),
                                AtomicAttack = reader.GetDouble("atomic_attack"),
                                AtomicDefense = reader.GetDouble("atomic_defense"),
                                MentalAttack = reader.GetDouble("mental_attack"),
                                MentalDefense = reader.GetDouble("mental_defense"),
                                Speed = reader.GetDouble("speed"),
                                CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                                CriticalRate = reader.GetDouble("critical_rate"),
                                CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                                PenetrationRate = reader.GetDouble("penetration_rate"),
                                PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                                EvasionRate = reader.GetDouble("evasion_rate"),
                                DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.GetDouble("accuracy_rate"),
                                LifestealRate = reader.GetDouble("lifesteal_rate"),
                                ShieldStrength = reader.GetDouble("shield_strength"),
                                Tenacity = reader.GetDouble("tenacity"),
                                ResistanceRate = reader.GetDouble("resistance_rate"),
                                ComboRate = reader.GetDouble("combo_rate"),
                                IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                                ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                                ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                                StunRate = reader.GetDouble("stun_rate"),
                                IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                                ReflectionRate = reader.GetDouble("reflection_rate"),
                                IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                                Mana = reader.GetDouble("mana"),
                                ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                                Description = reader.GetString("description")
                            };

                            SpiritCardList.Add(title);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return SpiritCardList;
    }
    public async Task<List<SpiritCards>> GetAllUserSpiritCardsAsync(string user_id, int pageSize, int offset)
    {
        List<SpiritCards> SpiritCardList = new List<SpiritCards>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                Select ut.*, t.id, t.name, t.image, t.rare, t.description 
                from spirit_cards t, user_spirit_cards ut
                where t.id = ut.spirit_card_id 
                and ut.user_id = @userId
                ORDER BY t.name REGEXP '[0-9]+$',
                         CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED),
                         t.name
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            SpiritCards title = new SpiritCards
                            {
                                Id = reader.GetString("spirit_card_id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetDouble("quality"),
                                Star = reader.GetInt32("star"),
                                Level = reader.GetInt32("level"),
                                Experiment = reader.GetDouble("experiment"),
                                Quantity = reader.GetDouble("quantity"),
                                Power = reader.GetDouble("power"),
                                Health = reader.GetDouble("health"),
                                PhysicalAttack = reader.GetDouble("physical_attack"),
                                PhysicalDefense = reader.GetDouble("physical_defense"),
                                MagicalAttack = reader.GetDouble("magical_attack"),
                                MagicalDefense = reader.GetDouble("magical_defense"),
                                ChemicalAttack = reader.GetDouble("chemical_attack"),
                                ChemicalDefense = reader.GetDouble("chemical_defense"),
                                AtomicAttack = reader.GetDouble("atomic_attack"),
                                AtomicDefense = reader.GetDouble("atomic_defense"),
                                MentalAttack = reader.GetDouble("mental_attack"),
                                MentalDefense = reader.GetDouble("mental_defense"),
                                Speed = reader.GetDouble("speed"),
                                CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                                CriticalRate = reader.GetDouble("critical_rate"),
                                CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                                PenetrationRate = reader.GetDouble("penetration_rate"),
                                PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                                EvasionRate = reader.GetDouble("evasion_rate"),
                                DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.GetDouble("accuracy_rate"),
                                LifestealRate = reader.GetDouble("lifesteal_rate"),
                                ShieldStrength = reader.GetDouble("shield_strength"),
                                Tenacity = reader.GetDouble("tenacity"),
                                ResistanceRate = reader.GetDouble("resistance_rate"),
                                ComboRate = reader.GetDouble("combo_rate"),
                                IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                                ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                                ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                                StunRate = reader.GetDouble("stun_rate"),
                                IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                                ReflectionRate = reader.GetDouble("reflection_rate"),
                                IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                                Mana = reader.GetDouble("mana"),
                                ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                                Description = reader.GetString("description")
                            };

                            SpiritCardList.Add(title);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return SpiritCardList;
    }
    public async Task<int> GetUserSpiritCardsCountAsync(string user_id, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                Select count(*) 
                from spirit_cards t, user_spirit_cards ut
                where t.id = ut.spirit_card_id
                and t.type = @type
                and ut.user_id = @userId
                AND (@rare = 'All' or t.rare = @rare)
                ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@rare", rare);

                    object result = await command.ExecuteScalarAsync();
                    count = Convert.ToInt32(result);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return count;
    }
    public async Task<SpiritCards> GetUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHeroes)
    {
        SpiritCards SpiritCard = new SpiritCards();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                select ue.*, e.*
                from spirit_cards e 
                left join user_spirit_cards ue 
                    on e.id = ue.spirit_card_id
                left join card_heroes_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                and che.user_card_hero_id = @user_card_hero_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            SpiritCard.Id = reader.GetString("spirit_card_id");
                            SpiritCard.Name = reader.GetString("name");
                            SpiritCard.Image = reader.GetString("image");
                            SpiritCard.Rare = reader.GetString("rare");
                            SpiritCard.Quality = reader.GetDouble("quality");
                            SpiritCard.Star = reader.GetInt32("star");
                            SpiritCard.Level = reader.GetInt32("level");
                            SpiritCard.Experiment = reader.GetDouble("experiment");
                            SpiritCard.Quantity = reader.GetDouble("quantity");
                            SpiritCard.Power = reader.GetDouble("power");
                            SpiritCard.Health = reader.GetDouble("health");
                            SpiritCard.PhysicalAttack = reader.GetDouble("physical_attack");
                            SpiritCard.PhysicalDefense = reader.GetDouble("physical_defense");
                            SpiritCard.MagicalAttack = reader.GetDouble("magical_attack");
                            SpiritCard.MagicalDefense = reader.GetDouble("magical_defense");
                            SpiritCard.ChemicalAttack = reader.GetDouble("chemical_attack");
                            SpiritCard.ChemicalDefense = reader.GetDouble("chemical_defense");
                            SpiritCard.AtomicAttack = reader.GetDouble("atomic_attack");
                            SpiritCard.AtomicDefense = reader.GetDouble("atomic_defense");
                            SpiritCard.MentalAttack = reader.GetDouble("mental_attack");
                            SpiritCard.MentalDefense = reader.GetDouble("mental_defense");
                            SpiritCard.Speed = reader.GetDouble("speed");
                            SpiritCard.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            SpiritCard.CriticalRate = reader.GetDouble("critical_rate");
                            SpiritCard.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            SpiritCard.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            SpiritCard.PenetrationRate = reader.GetDouble("penetration_rate");
                            SpiritCard.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            SpiritCard.EvasionRate = reader.GetDouble("evasion_rate");
                            SpiritCard.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            SpiritCard.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            SpiritCard.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            SpiritCard.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            SpiritCard.AccuracyRate = reader.GetDouble("accuracy_rate");
                            SpiritCard.LifestealRate = reader.GetDouble("lifesteal_rate");
                            SpiritCard.ShieldStrength = reader.GetDouble("shield_strength");
                            SpiritCard.Tenacity = reader.GetDouble("tenacity");
                            SpiritCard.ResistanceRate = reader.GetDouble("resistance_rate");
                            SpiritCard.ComboRate = reader.GetDouble("combo_rate");
                            SpiritCard.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            SpiritCard.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            SpiritCard.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            SpiritCard.StunRate = reader.GetDouble("stun_rate");
                            SpiritCard.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            SpiritCard.ReflectionRate = reader.GetDouble("reflection_rate");
                            SpiritCard.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            SpiritCard.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            SpiritCard.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            SpiritCard.Mana = reader.GetDouble("mana");
                            SpiritCard.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            SpiritCard.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            SpiritCard.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            SpiritCard.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            SpiritCard.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            SpiritCard.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            SpiritCard.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            SpiritCard.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            SpiritCard.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            SpiritCard.PercentAllHealth = reader.GetDouble("percent_all_health");
                            SpiritCard.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            SpiritCard.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            SpiritCard.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            SpiritCard.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            SpiritCard.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            SpiritCard.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            SpiritCard.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            SpiritCard.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            SpiritCard.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            SpiritCard.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return SpiritCard;
    }
    public async Task<SpiritCards> GetUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptains)
    {
        SpiritCards SpiritCard = new SpiritCards();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                select ue.*, e.*
                from spirit_cards e 
                left join user_spirit_cards ue 
                    on e.id = ue.spirit_card_id
                left join card_captains_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                  and che.user_card_captain_id = @user_card_captain_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            SpiritCard.Id = reader.GetString("spirit_card_id");
                            SpiritCard.Name = reader.GetString("name");
                            SpiritCard.Image = reader.GetString("image");
                            SpiritCard.Rare = reader.GetString("rare");
                            SpiritCard.Quality = reader.GetDouble("quality");
                            SpiritCard.Star = reader.GetInt32("star");
                            SpiritCard.Level = reader.GetInt32("level");
                            SpiritCard.Experiment = reader.GetDouble("experiment");
                            SpiritCard.Quantity = reader.GetDouble("quantity");
                            SpiritCard.Power = reader.GetDouble("power");
                            SpiritCard.Health = reader.GetDouble("health");
                            SpiritCard.PhysicalAttack = reader.GetDouble("physical_attack");
                            SpiritCard.PhysicalDefense = reader.GetDouble("physical_defense");
                            SpiritCard.MagicalAttack = reader.GetDouble("magical_attack");
                            SpiritCard.MagicalDefense = reader.GetDouble("magical_defense");
                            SpiritCard.ChemicalAttack = reader.GetDouble("chemical_attack");
                            SpiritCard.ChemicalDefense = reader.GetDouble("chemical_defense");
                            SpiritCard.AtomicAttack = reader.GetDouble("atomic_attack");
                            SpiritCard.AtomicDefense = reader.GetDouble("atomic_defense");
                            SpiritCard.MentalAttack = reader.GetDouble("mental_attack");
                            SpiritCard.MentalDefense = reader.GetDouble("mental_defense");
                            SpiritCard.Speed = reader.GetDouble("speed");
                            SpiritCard.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            SpiritCard.CriticalRate = reader.GetDouble("critical_rate");
                            SpiritCard.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            SpiritCard.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            SpiritCard.PenetrationRate = reader.GetDouble("penetration_rate");
                            SpiritCard.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            SpiritCard.EvasionRate = reader.GetDouble("evasion_rate");
                            SpiritCard.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            SpiritCard.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            SpiritCard.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            SpiritCard.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            SpiritCard.AccuracyRate = reader.GetDouble("accuracy_rate");
                            SpiritCard.LifestealRate = reader.GetDouble("lifesteal_rate");
                            SpiritCard.ShieldStrength = reader.GetDouble("shield_strength");
                            SpiritCard.Tenacity = reader.GetDouble("tenacity");
                            SpiritCard.ResistanceRate = reader.GetDouble("resistance_rate");
                            SpiritCard.ComboRate = reader.GetDouble("combo_rate");
                            SpiritCard.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            SpiritCard.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            SpiritCard.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            SpiritCard.StunRate = reader.GetDouble("stun_rate");
                            SpiritCard.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            SpiritCard.ReflectionRate = reader.GetDouble("reflection_rate");
                            SpiritCard.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            SpiritCard.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            SpiritCard.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            SpiritCard.Mana = reader.GetDouble("mana");
                            SpiritCard.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            SpiritCard.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            SpiritCard.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            SpiritCard.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            SpiritCard.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            SpiritCard.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            SpiritCard.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            SpiritCard.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            SpiritCard.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            SpiritCard.PercentAllHealth = reader.GetDouble("percent_all_health");
                            SpiritCard.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            SpiritCard.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            SpiritCard.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            SpiritCard.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            SpiritCard.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            SpiritCard.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            SpiritCard.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            SpiritCard.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            SpiritCard.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            SpiritCard.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return SpiritCard;
    }
    public async Task<SpiritCards> GetUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonels)
    {
        SpiritCards SpiritCard = new SpiritCards();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                select ue.*, e.*
                from spirit_cards e 
                left join user_spirit_cards ue 
                    on e.id = ue.spirit_card_id
                left join card_colonels_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                  and che.user_card_colonel_id = @user_card_colonel_id;
            ";

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.Id);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    SpiritCard.Id = reader.GetString("spirit_card_id");
                    SpiritCard.Name = reader.GetString("name");
                    SpiritCard.Image = reader.GetString("image");
                    SpiritCard.Rare = reader.GetString("rare");
                    SpiritCard.Quality = reader.GetDouble("quality");
                    SpiritCard.Star = reader.GetInt32("star");
                    SpiritCard.Level = reader.GetInt32("level");
                    SpiritCard.Experiment = reader.GetDouble("experiment");
                    SpiritCard.Quantity = reader.GetDouble("quantity");
                    SpiritCard.Power = reader.GetDouble("power");
                    SpiritCard.Health = reader.GetDouble("health");
                    SpiritCard.PhysicalAttack = reader.GetDouble("physical_attack");
                    SpiritCard.PhysicalDefense = reader.GetDouble("physical_defense");
                    SpiritCard.MagicalAttack = reader.GetDouble("magical_attack");
                    SpiritCard.MagicalDefense = reader.GetDouble("magical_defense");
                    SpiritCard.ChemicalAttack = reader.GetDouble("chemical_attack");
                    SpiritCard.ChemicalDefense = reader.GetDouble("chemical_defense");
                    SpiritCard.AtomicAttack = reader.GetDouble("atomic_attack");
                    SpiritCard.AtomicDefense = reader.GetDouble("atomic_defense");
                    SpiritCard.MentalAttack = reader.GetDouble("mental_attack");
                    SpiritCard.MentalDefense = reader.GetDouble("mental_defense");
                    SpiritCard.Speed = reader.GetDouble("speed");
                    SpiritCard.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                    SpiritCard.CriticalRate = reader.GetDouble("critical_rate");
                    SpiritCard.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                    SpiritCard.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                    SpiritCard.PenetrationRate = reader.GetDouble("penetration_rate");
                    SpiritCard.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                    SpiritCard.EvasionRate = reader.GetDouble("evasion_rate");
                    SpiritCard.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                    SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                    SpiritCard.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                    SpiritCard.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                    SpiritCard.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                    SpiritCard.AccuracyRate = reader.GetDouble("accuracy_rate");
                    SpiritCard.LifestealRate = reader.GetDouble("lifesteal_rate");
                    SpiritCard.ShieldStrength = reader.GetDouble("shield_strength");
                    SpiritCard.Tenacity = reader.GetDouble("tenacity");
                    SpiritCard.ResistanceRate = reader.GetDouble("resistance_rate");
                    SpiritCard.ComboRate = reader.GetDouble("combo_rate");
                    SpiritCard.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                    SpiritCard.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                    SpiritCard.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                    SpiritCard.StunRate = reader.GetDouble("stun_rate");
                    SpiritCard.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                    SpiritCard.ReflectionRate = reader.GetDouble("reflection_rate");
                    SpiritCard.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                    SpiritCard.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                    SpiritCard.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                    SpiritCard.Mana = reader.GetDouble("mana");
                    SpiritCard.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                    SpiritCard.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                    SpiritCard.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                    SpiritCard.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                    SpiritCard.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                    SpiritCard.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                    SpiritCard.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                    SpiritCard.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                    SpiritCard.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                    SpiritCard.PercentAllHealth = reader.GetDouble("percent_all_health");
                    SpiritCard.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                    SpiritCard.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                    SpiritCard.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                    SpiritCard.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                    SpiritCard.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                    SpiritCard.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                    SpiritCard.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                    SpiritCard.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                    SpiritCard.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                    SpiritCard.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return SpiritCard;
    }
    public async Task<SpiritCards> GetUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGenerals)
    {
        SpiritCards SpiritCard = new SpiritCards();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                select ue.*, e.*
                from spirit_cards e 
                left join user_spirit_cards ue 
                    on e.id = ue.spirit_card_id
                left join card_generals_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                    and che.user_card_general_id = @user_card_general_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_general_id", cardGenerals.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            SpiritCard.Id = reader.GetString("spirit_card_id");
                            SpiritCard.Name = reader.GetString("name");
                            SpiritCard.Image = reader.GetString("image");
                            SpiritCard.Rare = reader.GetString("rare");
                            SpiritCard.Quality = reader.GetDouble("quality");
                            SpiritCard.Star = reader.GetInt32("star");
                            SpiritCard.Level = reader.GetInt32("level");
                            SpiritCard.Experiment = reader.GetDouble("experiment");
                            SpiritCard.Quantity = reader.GetDouble("quantity");
                            SpiritCard.Power = reader.GetDouble("power");
                            SpiritCard.Health = reader.GetDouble("health");
                            SpiritCard.PhysicalAttack = reader.GetDouble("physical_attack");
                            SpiritCard.PhysicalDefense = reader.GetDouble("physical_defense");
                            SpiritCard.MagicalAttack = reader.GetDouble("magical_attack");
                            SpiritCard.MagicalDefense = reader.GetDouble("magical_defense");
                            SpiritCard.ChemicalAttack = reader.GetDouble("chemical_attack");
                            SpiritCard.ChemicalDefense = reader.GetDouble("chemical_defense");
                            SpiritCard.AtomicAttack = reader.GetDouble("atomic_attack");
                            SpiritCard.AtomicDefense = reader.GetDouble("atomic_defense");
                            SpiritCard.MentalAttack = reader.GetDouble("mental_attack");
                            SpiritCard.MentalDefense = reader.GetDouble("mental_defense");
                            SpiritCard.Speed = reader.GetDouble("speed");
                            SpiritCard.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            SpiritCard.CriticalRate = reader.GetDouble("critical_rate");
                            SpiritCard.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            SpiritCard.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            SpiritCard.PenetrationRate = reader.GetDouble("penetration_rate");
                            SpiritCard.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            SpiritCard.EvasionRate = reader.GetDouble("evasion_rate");
                            SpiritCard.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            SpiritCard.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            SpiritCard.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            SpiritCard.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            SpiritCard.AccuracyRate = reader.GetDouble("accuracy_rate");
                            SpiritCard.LifestealRate = reader.GetDouble("lifesteal_rate");
                            SpiritCard.ShieldStrength = reader.GetDouble("shield_strength");
                            SpiritCard.Tenacity = reader.GetDouble("tenacity");
                            SpiritCard.ResistanceRate = reader.GetDouble("resistance_rate");
                            SpiritCard.ComboRate = reader.GetDouble("combo_rate");
                            SpiritCard.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            SpiritCard.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            SpiritCard.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            SpiritCard.StunRate = reader.GetDouble("stun_rate");
                            SpiritCard.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            SpiritCard.ReflectionRate = reader.GetDouble("reflection_rate");
                            SpiritCard.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            SpiritCard.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            SpiritCard.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            SpiritCard.Mana = reader.GetDouble("mana");
                            SpiritCard.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            SpiritCard.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            SpiritCard.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            SpiritCard.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            SpiritCard.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            SpiritCard.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            SpiritCard.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            SpiritCard.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            SpiritCard.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            SpiritCard.PercentAllHealth = reader.GetDouble("percent_all_health");
                            SpiritCard.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            SpiritCard.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            SpiritCard.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            SpiritCard.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            SpiritCard.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            SpiritCard.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            SpiritCard.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            SpiritCard.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            SpiritCard.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            SpiritCard.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return SpiritCard;
    }
    public async Task<SpiritCards> GetUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmirals)
    {
        SpiritCards SpiritCard = new SpiritCards();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                select ue.*, e.*
                from spirit_cards e 
                left join user_spirit_cards ue 
                    on e.id = ue.spirit_card_id
                left join card_admirals_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                    and che.user_card_admiral_id = @user_card_admiral_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            SpiritCard.Id = reader.GetString("spirit_card_id");
                            SpiritCard.Name = reader.GetString("name");
                            SpiritCard.Image = reader.GetString("image");
                            SpiritCard.Rare = reader.GetString("rare");
                            SpiritCard.Quality = reader.GetDouble("quality");
                            SpiritCard.Star = reader.GetInt32("star");
                            SpiritCard.Level = reader.GetInt32("level");
                            SpiritCard.Experiment = reader.GetDouble("experiment");
                            SpiritCard.Quantity = reader.GetDouble("quantity");
                            SpiritCard.Power = reader.GetDouble("power");
                            SpiritCard.Health = reader.GetDouble("health");
                            SpiritCard.PhysicalAttack = reader.GetDouble("physical_attack");
                            SpiritCard.PhysicalDefense = reader.GetDouble("physical_defense");
                            SpiritCard.MagicalAttack = reader.GetDouble("magical_attack");
                            SpiritCard.MagicalDefense = reader.GetDouble("magical_defense");
                            SpiritCard.ChemicalAttack = reader.GetDouble("chemical_attack");
                            SpiritCard.ChemicalDefense = reader.GetDouble("chemical_defense");
                            SpiritCard.AtomicAttack = reader.GetDouble("atomic_attack");
                            SpiritCard.AtomicDefense = reader.GetDouble("atomic_defense");
                            SpiritCard.MentalAttack = reader.GetDouble("mental_attack");
                            SpiritCard.MentalDefense = reader.GetDouble("mental_defense");
                            SpiritCard.Speed = reader.GetDouble("speed");
                            SpiritCard.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            SpiritCard.CriticalRate = reader.GetDouble("critical_rate");
                            SpiritCard.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            SpiritCard.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            SpiritCard.PenetrationRate = reader.GetDouble("penetration_rate");
                            SpiritCard.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            SpiritCard.EvasionRate = reader.GetDouble("evasion_rate");
                            SpiritCard.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            SpiritCard.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            SpiritCard.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            SpiritCard.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            SpiritCard.AccuracyRate = reader.GetDouble("accuracy_rate");
                            SpiritCard.LifestealRate = reader.GetDouble("lifesteal_rate");
                            SpiritCard.ShieldStrength = reader.GetDouble("shield_strength");
                            SpiritCard.Tenacity = reader.GetDouble("tenacity");
                            SpiritCard.ResistanceRate = reader.GetDouble("resistance_rate");
                            SpiritCard.ComboRate = reader.GetDouble("combo_rate");
                            SpiritCard.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            SpiritCard.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            SpiritCard.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            SpiritCard.StunRate = reader.GetDouble("stun_rate");
                            SpiritCard.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            SpiritCard.ReflectionRate = reader.GetDouble("reflection_rate");
                            SpiritCard.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            SpiritCard.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            SpiritCard.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            SpiritCard.Mana = reader.GetDouble("mana");
                            SpiritCard.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            SpiritCard.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            SpiritCard.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            SpiritCard.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            SpiritCard.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            SpiritCard.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            SpiritCard.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            SpiritCard.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            SpiritCard.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            SpiritCard.PercentAllHealth = reader.GetDouble("percent_all_health");
                            SpiritCard.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            SpiritCard.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            SpiritCard.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            SpiritCard.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            SpiritCard.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            SpiritCard.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            SpiritCard.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            SpiritCard.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            SpiritCard.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            SpiritCard.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return SpiritCard;
    }
    public async Task<SpiritCards> GetUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitary)
    {
        SpiritCards SpiritCard = new SpiritCards();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                select ue.*, e.*
                from spirit_cards e 
                left join user_spirit_cards ue 
                    on e.id = ue.spirit_card_id
                left join card_militaries_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                    and che.user_card_military_id = @user_card_military_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            SpiritCard.Id = reader.GetString("spirit_card_id");
                            SpiritCard.Name = reader.GetString("name");
                            SpiritCard.Image = reader.GetString("image");
                            SpiritCard.Rare = reader.GetString("rare");
                            SpiritCard.Quality = reader.GetDouble("quality");
                            SpiritCard.Star = reader.GetInt32("star");
                            SpiritCard.Level = reader.GetInt32("level");
                            SpiritCard.Experiment = reader.GetDouble("experiment");
                            SpiritCard.Quantity = reader.GetDouble("quantity");
                            SpiritCard.Power = reader.GetDouble("power");
                            SpiritCard.Health = reader.GetDouble("health");
                            SpiritCard.PhysicalAttack = reader.GetDouble("physical_attack");
                            SpiritCard.PhysicalDefense = reader.GetDouble("physical_defense");
                            SpiritCard.MagicalAttack = reader.GetDouble("magical_attack");
                            SpiritCard.MagicalDefense = reader.GetDouble("magical_defense");
                            SpiritCard.ChemicalAttack = reader.GetDouble("chemical_attack");
                            SpiritCard.ChemicalDefense = reader.GetDouble("chemical_defense");
                            SpiritCard.AtomicAttack = reader.GetDouble("atomic_attack");
                            SpiritCard.AtomicDefense = reader.GetDouble("atomic_defense");
                            SpiritCard.MentalAttack = reader.GetDouble("mental_attack");
                            SpiritCard.MentalDefense = reader.GetDouble("mental_defense");
                            SpiritCard.Speed = reader.GetDouble("speed");
                            SpiritCard.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            SpiritCard.CriticalRate = reader.GetDouble("critical_rate");
                            SpiritCard.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            SpiritCard.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            SpiritCard.PenetrationRate = reader.GetDouble("penetration_rate");
                            SpiritCard.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            SpiritCard.EvasionRate = reader.GetDouble("evasion_rate");
                            SpiritCard.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            SpiritCard.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            SpiritCard.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            SpiritCard.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            SpiritCard.AccuracyRate = reader.GetDouble("accuracy_rate");
                            SpiritCard.LifestealRate = reader.GetDouble("lifesteal_rate");
                            SpiritCard.ShieldStrength = reader.GetDouble("shield_strength");
                            SpiritCard.Tenacity = reader.GetDouble("tenacity");
                            SpiritCard.ResistanceRate = reader.GetDouble("resistance_rate");
                            SpiritCard.ComboRate = reader.GetDouble("combo_rate");
                            SpiritCard.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            SpiritCard.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            SpiritCard.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            SpiritCard.StunRate = reader.GetDouble("stun_rate");
                            SpiritCard.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            SpiritCard.ReflectionRate = reader.GetDouble("reflection_rate");
                            SpiritCard.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            SpiritCard.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            SpiritCard.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            SpiritCard.Mana = reader.GetDouble("mana");
                            SpiritCard.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            SpiritCard.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            SpiritCard.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            SpiritCard.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            SpiritCard.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            SpiritCard.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            SpiritCard.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            SpiritCard.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            SpiritCard.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            SpiritCard.PercentAllHealth = reader.GetDouble("percent_all_health");
                            SpiritCard.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            SpiritCard.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            SpiritCard.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            SpiritCard.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            SpiritCard.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            SpiritCard.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            SpiritCard.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            SpiritCard.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            SpiritCard.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            SpiritCard.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return SpiritCard;
    }
    public async Task<SpiritCards> GetUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonsters)
    {
        SpiritCards SpiritCard = new SpiritCards();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                select ue.*, e.*
                from spirit_cards e 
                left join user_spirit_cards ue 
                    on e.id = ue.spirit_card_id
                left join card_monsters_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                    and che.user_card_monster_id = @user_card_monster_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            SpiritCard.Id = reader.GetString("spirit_card_id");
                            SpiritCard.Name = reader.GetString("name");
                            SpiritCard.Image = reader.GetString("image");
                            SpiritCard.Rare = reader.GetString("rare");
                            SpiritCard.Quality = reader.GetDouble("quality");
                            SpiritCard.Star = reader.GetInt32("star");
                            SpiritCard.Level = reader.GetInt32("level");
                            SpiritCard.Experiment = reader.GetDouble("experiment");
                            SpiritCard.Quantity = reader.GetDouble("quantity");
                            SpiritCard.Power = reader.GetDouble("power");
                            SpiritCard.Health = reader.GetDouble("health");
                            SpiritCard.PhysicalAttack = reader.GetDouble("physical_attack");
                            SpiritCard.PhysicalDefense = reader.GetDouble("physical_defense");
                            SpiritCard.MagicalAttack = reader.GetDouble("magical_attack");
                            SpiritCard.MagicalDefense = reader.GetDouble("magical_defense");
                            SpiritCard.ChemicalAttack = reader.GetDouble("chemical_attack");
                            SpiritCard.ChemicalDefense = reader.GetDouble("chemical_defense");
                            SpiritCard.AtomicAttack = reader.GetDouble("atomic_attack");
                            SpiritCard.AtomicDefense = reader.GetDouble("atomic_defense");
                            SpiritCard.MentalAttack = reader.GetDouble("mental_attack");
                            SpiritCard.MentalDefense = reader.GetDouble("mental_defense");
                            SpiritCard.Speed = reader.GetDouble("speed");
                            SpiritCard.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            SpiritCard.CriticalRate = reader.GetDouble("critical_rate");
                            SpiritCard.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            SpiritCard.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            SpiritCard.PenetrationRate = reader.GetDouble("penetration_rate");
                            SpiritCard.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            SpiritCard.EvasionRate = reader.GetDouble("evasion_rate");
                            SpiritCard.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            SpiritCard.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            SpiritCard.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            SpiritCard.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            SpiritCard.AccuracyRate = reader.GetDouble("accuracy_rate");
                            SpiritCard.LifestealRate = reader.GetDouble("lifesteal_rate");
                            SpiritCard.ShieldStrength = reader.GetDouble("shield_strength");
                            SpiritCard.Tenacity = reader.GetDouble("tenacity");
                            SpiritCard.ResistanceRate = reader.GetDouble("resistance_rate");
                            SpiritCard.ComboRate = reader.GetDouble("combo_rate");
                            SpiritCard.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            SpiritCard.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            SpiritCard.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            SpiritCard.StunRate = reader.GetDouble("stun_rate");
                            SpiritCard.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            SpiritCard.ReflectionRate = reader.GetDouble("reflection_rate");
                            SpiritCard.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            SpiritCard.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            SpiritCard.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            SpiritCard.Mana = reader.GetDouble("mana");
                            SpiritCard.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            SpiritCard.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            SpiritCard.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            SpiritCard.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            SpiritCard.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            SpiritCard.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            SpiritCard.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            SpiritCard.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            SpiritCard.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            SpiritCard.PercentAllHealth = reader.GetDouble("percent_all_health");
                            SpiritCard.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            SpiritCard.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            SpiritCard.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            SpiritCard.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            SpiritCard.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            SpiritCard.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            SpiritCard.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            SpiritCard.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            SpiritCard.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            SpiritCard.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return SpiritCard;
    }
    public async Task<SpiritCards> GetUserCardSpellSpiritCardAsync(string userId, CardSpells cardSpell)
    {
        SpiritCards SpiritCard = new SpiritCards();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                select ue.*, e.*
                from spirit_cards e 
                left join user_spirit_cards ue 
                    on e.id = ue.spirit_card_id
                left join card_spells_spirit_card che 
                    on che.user_spirit_card_id = ue.spirit_card_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                    and che.user_card_spell_id = @user_card_spell_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_spell_id", cardSpell.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            SpiritCard.Id = reader.GetString("spirit_card_id");
                            SpiritCard.Name = reader.GetString("name");
                            SpiritCard.Image = reader.GetString("image");
                            SpiritCard.Rare = reader.GetString("rare");
                            SpiritCard.Quality = reader.GetDouble("quality");
                            SpiritCard.Star = reader.GetInt32("star");
                            SpiritCard.Level = reader.GetInt32("level");
                            SpiritCard.Experiment = reader.GetDouble("experiment");
                            SpiritCard.Quantity = reader.GetDouble("quantity");
                            SpiritCard.Power = reader.GetDouble("power");
                            SpiritCard.Health = reader.GetDouble("health");
                            SpiritCard.PhysicalAttack = reader.GetDouble("physical_attack");
                            SpiritCard.PhysicalDefense = reader.GetDouble("physical_defense");
                            SpiritCard.MagicalAttack = reader.GetDouble("magical_attack");
                            SpiritCard.MagicalDefense = reader.GetDouble("magical_defense");
                            SpiritCard.ChemicalAttack = reader.GetDouble("chemical_attack");
                            SpiritCard.ChemicalDefense = reader.GetDouble("chemical_defense");
                            SpiritCard.AtomicAttack = reader.GetDouble("atomic_attack");
                            SpiritCard.AtomicDefense = reader.GetDouble("atomic_defense");
                            SpiritCard.MentalAttack = reader.GetDouble("mental_attack");
                            SpiritCard.MentalDefense = reader.GetDouble("mental_defense");
                            SpiritCard.Speed = reader.GetDouble("speed");
                            SpiritCard.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            SpiritCard.CriticalRate = reader.GetDouble("critical_rate");
                            SpiritCard.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            SpiritCard.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            SpiritCard.PenetrationRate = reader.GetDouble("penetration_rate");
                            SpiritCard.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            SpiritCard.EvasionRate = reader.GetDouble("evasion_rate");
                            SpiritCard.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            SpiritCard.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            SpiritCard.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            SpiritCard.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            SpiritCard.AccuracyRate = reader.GetDouble("accuracy_rate");
                            SpiritCard.LifestealRate = reader.GetDouble("lifesteal_rate");
                            SpiritCard.ShieldStrength = reader.GetDouble("shield_strength");
                            SpiritCard.Tenacity = reader.GetDouble("tenacity");
                            SpiritCard.ResistanceRate = reader.GetDouble("resistance_rate");
                            SpiritCard.ComboRate = reader.GetDouble("combo_rate");
                            SpiritCard.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            SpiritCard.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            SpiritCard.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            SpiritCard.StunRate = reader.GetDouble("stun_rate");
                            SpiritCard.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            SpiritCard.ReflectionRate = reader.GetDouble("reflection_rate");
                            SpiritCard.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            SpiritCard.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            SpiritCard.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            SpiritCard.Mana = reader.GetDouble("mana");
                            SpiritCard.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            SpiritCard.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            SpiritCard.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            SpiritCard.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            SpiritCard.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            SpiritCard.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            SpiritCard.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            SpiritCard.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            SpiritCard.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            SpiritCard.PercentAllHealth = reader.GetDouble("percent_all_health");
                            SpiritCard.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            SpiritCard.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            SpiritCard.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            SpiritCard.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            SpiritCard.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            SpiritCard.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            SpiritCard.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            SpiritCard.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            SpiritCard.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            SpiritCard.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return SpiritCard;
    }
    public async Task<bool> InsertUserSpiritCardAsync(SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_spirit_cards 
                WHERE user_id = @user_id AND spirit_card_id = @spirit_card_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@spirit_card_id", SpiritCard.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string query = @"
                        INSERT INTO user_spirit_cards (
                            user_id, spirit_card_id, rare, level, experiment, star, quality, block, quantity,
                            power, health, physical_attack, physical_defense, magical_attack, magical_defense,
                            chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                            speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                            penetration_rate, penetration_resistance_rate,
                            evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                            vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                            accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                            combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                            stun_rate, ignore_stun_rate,
                            reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                            mana, mana_regeneration_rate,
                            damage_to_different_faction_rate, resistance_to_different_faction_rate,
                            damage_to_same_faction_rate, resistance_to_same_faction_rate,
                            normal_damage_rate, normal_resistance_rate,
                            skill_damage_rate, skill_resistance_rate
                        ) VALUES (
                            @user_id, @spirit_card_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
                            @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                            @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                            @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                            @penetration_rate, @penetration_resistance_rate,
                            @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                            @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                            @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                            @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                            @stun_rate, @ignore_stun_rate,
                            @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                            @mana, @mana_regeneration_rate,
                            @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                            @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                            @normal_damage_rate, @normal_resistance_rate,
                            @skill_damage_rate, @skill_resistance_rate
                        );
                    ";

                        await using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            command.Parameters.AddWithValue("@spirit_card_id", SpiritCard.Id);
                            command.Parameters.AddWithValue("@rare", SpiritCard.Rare);
                            command.Parameters.AddWithValue("@level", 0);
                            command.Parameters.AddWithValue("@experiment", 0);
                            command.Parameters.AddWithValue("@star", 0);
                            command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(SpiritCard.Rare));
                            command.Parameters.AddWithValue("@block", false);
                            command.Parameters.AddWithValue("@quantity", SpiritCard.Quantity);
                            command.Parameters.AddWithValue("@power", SpiritCard.Power);
                            command.Parameters.AddWithValue("@health", SpiritCard.Health);
                            command.Parameters.AddWithValue("@physical_attack", SpiritCard.PhysicalAttack);
                            command.Parameters.AddWithValue("@physical_defense", SpiritCard.PhysicalDefense);
                            command.Parameters.AddWithValue("@magical_attack", SpiritCard.MagicalAttack);
                            command.Parameters.AddWithValue("@magical_defense", SpiritCard.MagicalDefense);
                            command.Parameters.AddWithValue("@chemical_attack", SpiritCard.ChemicalAttack);
                            command.Parameters.AddWithValue("@chemical_defense", SpiritCard.ChemicalDefense);
                            command.Parameters.AddWithValue("@atomic_attack", SpiritCard.AtomicAttack);
                            command.Parameters.AddWithValue("@atomic_defense", SpiritCard.AtomicDefense);
                            command.Parameters.AddWithValue("@mental_attack", SpiritCard.MentalAttack);
                            command.Parameters.AddWithValue("@mental_defense", SpiritCard.MentalDefense);
                            command.Parameters.AddWithValue("@speed", SpiritCard.Speed);
                            command.Parameters.AddWithValue("@critical_damage_rate", SpiritCard.CriticalDamageRate);
                            command.Parameters.AddWithValue("@critical_rate", SpiritCard.CriticalRate);
                            command.Parameters.AddWithValue("@critical_resistance_rate", SpiritCard.CriticalResistanceRate);
                            command.Parameters.AddWithValue("@ignore_critical_rate", SpiritCard.IgnoreCriticalRate);
                            command.Parameters.AddWithValue("@penetration_rate", SpiritCard.PenetrationRate);
                            command.Parameters.AddWithValue("@penetration_resistance_rate", SpiritCard.PenetrationResistanceRate);
                            command.Parameters.AddWithValue("@evasion_rate", SpiritCard.EvasionRate);
                            command.Parameters.AddWithValue("@damage_absorption_rate", SpiritCard.DamageAbsorptionRate);
                            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", SpiritCard.IgnoreDamageAbsorptionRate);
                            command.Parameters.AddWithValue("@absorbed_damage_rate", SpiritCard.AbsorbedDamageRate);
                            command.Parameters.AddWithValue("@vitality_regeneration_rate", SpiritCard.VitalityRegenerationRate);
                            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", SpiritCard.VitalityRegenerationResistanceRate);
                            command.Parameters.AddWithValue("@accuracy_rate", SpiritCard.AccuracyRate);
                            command.Parameters.AddWithValue("@lifesteal_rate", SpiritCard.LifestealRate);
                            command.Parameters.AddWithValue("@shield_strength", SpiritCard.ShieldStrength);
                            command.Parameters.AddWithValue("@tenacity", SpiritCard.Tenacity);
                            command.Parameters.AddWithValue("@resistance_rate", SpiritCard.ResistanceRate);
                            command.Parameters.AddWithValue("@combo_rate", SpiritCard.ComboRate);
                            command.Parameters.AddWithValue("@ignore_combo_rate", SpiritCard.IgnoreComboRate);
                            command.Parameters.AddWithValue("@combo_damage_rate", SpiritCard.ComboDamageRate);
                            command.Parameters.AddWithValue("@combo_resistance_rate", SpiritCard.ComboResistanceRate);
                            command.Parameters.AddWithValue("@stun_rate", SpiritCard.StunRate);
                            command.Parameters.AddWithValue("@ignore_stun_rate", SpiritCard.IgnoreStunRate);
                            command.Parameters.AddWithValue("@reflection_rate", SpiritCard.ReflectionRate);
                            command.Parameters.AddWithValue("@ignore_reflection_rate", SpiritCard.IgnoreReflectionRate);
                            command.Parameters.AddWithValue("@reflection_damage_rate", SpiritCard.ReflectionDamageRate);
                            command.Parameters.AddWithValue("@reflection_resistance_rate", SpiritCard.ReflectionResistanceRate);
                            command.Parameters.AddWithValue("@mana", SpiritCard.Mana);
                            command.Parameters.AddWithValue("@mana_regeneration_rate", SpiritCard.ManaRegenerationRate);
                            command.Parameters.AddWithValue("@damage_to_different_faction_rate", SpiritCard.DamageToDifferentFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", SpiritCard.ResistanceToDifferentFactionRate);
                            command.Parameters.AddWithValue("@damage_to_same_faction_rate", SpiritCard.DamageToSameFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", SpiritCard.ResistanceToSameFactionRate);
                            command.Parameters.AddWithValue("@normal_damage_rate", SpiritCard.NormalDamageRate);
                            command.Parameters.AddWithValue("@normal_resistance_rate", SpiritCard.NormalResistanceRate);
                            command.Parameters.AddWithValue("@skill_damage_rate", SpiritCard.SkillDamageRate);
                            command.Parameters.AddWithValue("@skill_resistance_rate", SpiritCard.SkillResistanceRate);

                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_spirit_cards
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND spirit_card_id = @spirit_card_id;
                    ";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCommand.Parameters.AddWithValue("@spirit_card_id", SpiritCard.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", SpiritCard.Quantity);

                            await updateCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> UpdateSpiritCardLevelAsync(SpiritCards SpiritCard, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_spirit_cards
                SET 
                    level = @level, power = @power, health = @health, 
                    physical_attack = @physical_attack, physical_defense = @physical_defense, 
                    magical_attack = @magical_attack, magical_defense = @magical_defense, 
                    chemical_attack = @chemical_attack, chemical_defense = @chemical_defense, 
                    atomic_attack = @atomic_attack, atomic_defense = @atomic_defense, 
                    mental_attack = @mental_attack, mental_defense = @mental_defense, 
                    speed = @speed, critical_damage_rate = @critical_damage_rate, 
                    critical_rate = @critical_rate, critical_resistance_rate = @critical_resistance_rate, 
                    ignore_critical_rate = @ignore_critical_rate,
                    penetration_rate = @penetration_rate, penetration_resistance_rate = @penetration_resistance_rate,
                    evasion_rate = @evasion_rate, damage_absorption_rate = @damage_absorption_rate, 
                    ignore_damage_absorption_rate = @ignore_damage_absorption_rate, absorbed_damage_rate = @absorbed_damage_rate,
                    vitality_regeneration_rate = @vitality_regeneration_rate, vitality_regeneration_resistance_rate = @vitality_regeneration_resistance_rate, 
                    accuracy_rate = @accuracy_rate, lifesteal_rate = @lifesteal_rate, shield_strength = @shield_strength, 
                    tenacity = @tenacity, resistance_rate = @resistance_rate, 
                    combo_rate = @combo_rate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
                    stun_rate = @stun_rate, ignore_stun_rate = @ignore_stun_rate,
                    reflection_rate = @reflection_rate, ignore_reflection_rate = @ignore_reflection_rate, 
                    reflection_damage_rate = @reflection_damage_rate, reflection_resistance_rate = @reflection_resistance_rate,
                    mana = @mana, mana_regeneration_rate = @mana_regeneration_rate, 
                    damage_to_different_faction_rate = @damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate = @resistance_to_different_faction_rate, 
                    damage_to_same_faction_rate = @damage_to_same_faction_rate, 
                    resistance_to_same_faction_rate = @resistance_to_same_faction_rate,
                    normal_damage_rate = @normal_damage_rate, normal_resistance_rate = @normal_resistance_rate,
                    skill_damage_rate = @skill_damage_rate, skill_resistance_rate = @skill_resistance_rate
                WHERE user_id = @user_id AND spirit_card_id = @spirit_card_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@spirit_card_id", SpiritCard.Id);
                    command.Parameters.AddWithValue("@level", cardLevel);
                    command.Parameters.AddWithValue("@power", SpiritCard.Power);
                    command.Parameters.AddWithValue("@health", SpiritCard.Health);
                    command.Parameters.AddWithValue("@physical_attack", SpiritCard.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", SpiritCard.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", SpiritCard.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", SpiritCard.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", SpiritCard.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", SpiritCard.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", SpiritCard.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", SpiritCard.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", SpiritCard.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", SpiritCard.MentalDefense);
                    command.Parameters.AddWithValue("@speed", SpiritCard.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", SpiritCard.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", SpiritCard.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", SpiritCard.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", SpiritCard.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", SpiritCard.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", SpiritCard.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", SpiritCard.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", SpiritCard.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", SpiritCard.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", SpiritCard.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", SpiritCard.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", SpiritCard.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", SpiritCard.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", SpiritCard.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", SpiritCard.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", SpiritCard.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", SpiritCard.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", SpiritCard.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", SpiritCard.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", SpiritCard.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", SpiritCard.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", SpiritCard.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", SpiritCard.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", SpiritCard.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", SpiritCard.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", SpiritCard.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", SpiritCard.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", SpiritCard.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", SpiritCard.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", SpiritCard.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", SpiritCard.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", SpiritCard.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", SpiritCard.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", SpiritCard.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", SpiritCard.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", SpiritCard.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", SpiritCard.SkillResistanceRate);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> UpdateSpiritCardBreakthroughAsync(SpiritCards SpiritCard, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_spirit_cards
                SET 
                    star = @star, quantity = @quantity, power=@power, health = @health, 
                    physical_attack = @physical_attack, physical_defense = @physical_defense, 
                    magical_attack = @magical_attack, magical_defense = @magical_defense, 
                    chemical_attack = @chemical_attack, chemical_defense = @chemical_defense, 
                    atomic_attack = @atomic_attack, atomic_defense = @atomic_defense, 
                    mental_attack = @mental_attack, mental_defense = @mental_defense, 
                    speed = @speed, critical_damage_rate = @critical_damage_rate, 
                    critical_rate = @critical_rate, critical_resistance_rate = @critical_resistance_rate, ignore_critical_rate = @ignore_critical_rate,
                    penetration_rate = @penetration_rate, penetration_resistance_rate = @penetration_resistance_rate,
                    evasion_rate = @evasion_rate, damage_absorption_rate = @damage_absorption_rate, 
                    ignore_damage_absorption_rate = @ignore_damage_absorption_rate, absorbed_damage_rate = @absorbed_damage_rate,
                    vitality_regeneration_rate = @vitality_regeneration_rate, vitality_regeneration_resistance_rate = @vitality_regeneration_resistance_rate, 
                    accuracy_rate = @accuracy_rate, lifesteal_rate = @lifesteal_rate, shield_strength = @shield_strength, 
                    tenacity = @tenacity, resistance_rate = @resistance_rate, 
                    combo_rate = @combo_rate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
                    stun_rate = @stun_rate, ignore_stun_rate = @ignore_stun_rate,
                    reflection_rate = @reflection_rate, ignore_reflection_rate = @ignore_reflection_rate, 
                    reflection_damage_rate = @reflection_damage_rate, reflection_resistance_rate = @reflection_resistance_rate,
                    mana = @mana, mana_regeneration_rate = @mana_regeneration_rate, 
                    damage_to_different_faction_rate = @damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate = @resistance_to_different_faction_rate, 
                    damage_to_same_faction_rate = @damage_to_same_faction_rate, 
                    resistance_to_same_faction_rate = @resistance_to_same_faction_rate,
                    normal_damage_rate = @normal_damage_rate, normal_resistance_rate = @normal_resistance_rate,
                    skill_damage_rate = @skill_damage_rate, skill_resistance_rate = @skill_resistance_rate
                WHERE user_id = @user_id AND spirit_card_id = @spirit_card_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@spirit_card_id", SpiritCard.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@power", SpiritCard.Power);
                    command.Parameters.AddWithValue("@health", SpiritCard.Health);
                    command.Parameters.AddWithValue("@physical_attack", SpiritCard.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", SpiritCard.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", SpiritCard.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", SpiritCard.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", SpiritCard.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", SpiritCard.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", SpiritCard.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", SpiritCard.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", SpiritCard.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", SpiritCard.MentalDefense);
                    command.Parameters.AddWithValue("@speed", SpiritCard.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", SpiritCard.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", SpiritCard.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", SpiritCard.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", SpiritCard.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", SpiritCard.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", SpiritCard.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", SpiritCard.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", SpiritCard.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", SpiritCard.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", SpiritCard.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", SpiritCard.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", SpiritCard.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", SpiritCard.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", SpiritCard.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", SpiritCard.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", SpiritCard.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", SpiritCard.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", SpiritCard.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", SpiritCard.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", SpiritCard.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", SpiritCard.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", SpiritCard.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", SpiritCard.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", SpiritCard.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", SpiritCard.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", SpiritCard.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", SpiritCard.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", SpiritCard.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", SpiritCard.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", SpiritCard.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", SpiritCard.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", SpiritCard.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", SpiritCard.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", SpiritCard.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", SpiritCard.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", SpiritCard.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", SpiritCard.SkillResistanceRate);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> InsertOrUpdateUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHeroes, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"SELECT COUNT(*) FROM card_heroes_spirit_card WHERE user_id = @user_id AND user_card_hero_id = @user_card_hero_id;";

                await using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_heroes_spirit_card (user_id, user_card_hero_id, user_spirit_card_id) VALUES (@user_id, @user_card_hero_id, @user_spirit_card_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"UPDATE card_heroes_spirit_card SET user_spirit_card_id = @user_spirit_card_id WHERE user_id = @user_id AND user_card_hero_id = @user_card_hero_id;";

                        await using var updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> InsertOrUpdateUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptains, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"SELECT COUNT(*) FROM card_captains_spirit_card 
                              WHERE user_id = @user_id AND user_card_captain_id = @user_card_captain_id;";

                await using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_captains_spirit_card 
                                      (user_id, user_card_captain_id, user_spirit_card_id)
                                       VALUES (@user_id, @user_card_captain_id, @user_spirit_card_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"UPDATE card_captains_spirit_card
                                       SET user_spirit_card_id = @user_spirit_card_id
                                       WHERE user_id = @user_id AND user_card_captain_id = @user_card_captain_id;";

                        await using var updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return true;
    }
    public async Task<bool> InsertOrUpdateUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonels, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"SELECT COUNT(*) FROM card_colonels_spirit_card 
                              WHERE user_id = @user_id AND user_card_colonel_id = @user_card_colonel_id;";

                await using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_colonels_spirit_card
                                      (user_id, user_card_colonel_id, user_spirit_card_id)
                                      VALUES (@user_id, @user_card_colonel_id, @user_spirit_card_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"UPDATE card_colonels_spirit_card
                                       SET user_spirit_card_id = @user_spirit_card_id
                                       WHERE user_id = @user_id AND user_card_colonel_id = @user_card_colonel_id;";

                        await using var updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return true;
    }
    public async Task<bool> InsertOrUpdateUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGenerals, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"SELECT COUNT(*) FROM card_generals_spirit_card 
                              WHERE user_id = @user_id AND user_card_general_id = @user_card_general_id;";

                await using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_generals_spirit_card
                                      (user_id, user_card_general_id, user_spirit_card_id)
                                      VALUES (@user_id, @user_card_general_id, @user_spirit_card_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"UPDATE card_generals_spirit_card
                                       SET user_spirit_card_id = @user_spirit_card_id
                                       WHERE user_id = @user_id AND user_card_general_id = @user_card_general_id;";

                        await using var updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId); // sửa lỗi User.CurrentUserId
                        updateCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return true;
    }
    public async Task<bool> InsertOrUpdateUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmirals, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"SELECT COUNT(*) FROM card_admirals_spirit_card 
                              WHERE user_id = @user_id AND user_card_admiral_id = @user_card_admiral_id;";

                await using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_admirals_spirit_card
                                      (user_id, user_card_admiral_id, user_spirit_card_id)
                                      VALUES (@user_id, @user_card_admiral_id, @user_spirit_card_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.Id); // sửa lỗi
                        insertCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"UPDATE card_admirals_spirit_card
                                       SET user_spirit_card_id = @user_spirit_card_id
                                       WHERE user_id = @user_id AND user_card_admiral_id = @user_card_admiral_id;";

                        await using var updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId); // sửa lỗi User.CurrentUserId
                        updateCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return true;
    }
    public async Task<bool> InsertOrUpdateUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitary, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra bản ghi tồn tại
                string checkQuery = @"
                SELECT COUNT(*) FROM card_military_spirit_card 
                WHERE user_id = @user_id AND user_card_military_id = @user_card_military_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO card_military_spirit_card (
                            user_id, user_card_military_id, user_spirit_card_id
                        ) VALUES (
                            @user_id, @user_card_military_id, @user_spirit_card_id
                        );";

                        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"
                        UPDATE card_military_spirit_card
                        SET user_spirit_card_id = @user_spirit_card_id
                        WHERE user_id = @user_id AND user_card_military_id = @user_card_military_id;";

                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> InsertOrUpdateUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonsters, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_monsters_spirit_card 
                WHERE user_id = @user_id AND user_card_monster_id = @user_card_monster_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        // INSERT
                        string insertQuery = @"
                        INSERT INTO card_monsters_spirit_card (
                            user_id, user_card_monster_id, user_spirit_card_id
                        ) VALUES (
                            @user_id, @user_card_monster_id, @user_spirit_card_id
                        );";

                        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        // UPDATE
                        string updateQuery = @"
                        UPDATE card_monsters_spirit_card
                        SET user_spirit_card_id = @user_spirit_card_id
                        WHERE user_id = @user_id AND user_card_monster_id = @user_card_monster_id;";

                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> InsertOrUpdateUserCardSpellSpiritCardAsync(string userId, CardSpells cardSpell, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_spell_spirit_card 
                WHERE user_id = @user_id AND user_card_spell_id = @user_card_spell_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpell.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        // INSERT
                        string insertQuery = @"
                        INSERT INTO card_spell_spirit_card (
                            user_id, user_card_spell_id, user_spirit_card_id
                        ) VALUES (
                            @user_id, @user_card_spell_id, @user_spirit_card_id
                        );";

                        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpell.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        // UPDATE
                        string updateQuery = @"
                        UPDATE card_spell_spirit_card
                        SET user_spirit_card_id = @user_spirit_card_id
                        WHERE user_id = @user_id AND user_card_spell_id = @user_card_spell_id;";

                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpell.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await updateCommand.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<List<SpiritCards>> GetAllUserCardHeroesSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> SpiritCardList = new List<SpiritCards>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_card_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_cards e
            LEFT JOIN user_spirit_cards ue ON e.id = ue.spirit_card_id
            LEFT JOIN card_heroes_spirit_card che 
                ON che.user_spirit_card_id = ue.spirit_card_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_card_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_card_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritCards sb = new SpiritCards
                {
                    Id = reader.GetString("spirit_card_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
                    Star = reader.GetInt32("star"),
                    Level = reader.GetInt32("level"),
                    Experiment = reader.GetDouble("experiment"),
                    Quantity = reader.GetDouble("quantity"),
                    Power = reader.GetDouble("power"),
                    Health = reader.GetDouble("health"),
                    PhysicalAttack = reader.GetDouble("physical_attack"),
                    PhysicalDefense = reader.GetDouble("physical_defense"),
                    MagicalAttack = reader.GetDouble("magical_attack"),
                    MagicalDefense = reader.GetDouble("magical_defense"),
                    ChemicalAttack = reader.GetDouble("chemical_attack"),
                    ChemicalDefense = reader.GetDouble("chemical_defense"),
                    AtomicAttack = reader.GetDouble("atomic_attack"),
                    AtomicDefense = reader.GetDouble("atomic_defense"),
                    MentalAttack = reader.GetDouble("mental_attack"),
                    MentalDefense = reader.GetDouble("mental_defense"),
                    Speed = reader.GetDouble("speed"),
                    CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                    CriticalRate = reader.GetDouble("critical_rate"),
                    CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                    PenetrationRate = reader.GetDouble("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                    EvasionRate = reader.GetDouble("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDouble("accuracy_rate"),
                    LifestealRate = reader.GetDouble("lifesteal_rate"),
                    ShieldStrength = reader.GetDouble("shield_strength"),
                    Tenacity = reader.GetDouble("tenacity"),
                    ResistanceRate = reader.GetDouble("resistance_rate"),
                    ComboRate = reader.GetDouble("combo_rate"),
                    IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                    StunRate = reader.GetDouble("stun_rate"),
                    IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                    ReflectionRate = reader.GetDouble("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                    Mana = reader.GetDouble("mana"),
                    ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                    // EquipStatus = reader.GetString("equip_status")
                };

                SpiritCardList.Add(sb);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }

        return SpiritCardList;
    }
    public async Task<List<SpiritCards>> GetAllUserCardCaptainsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> SpiritCardList = new List<SpiritCards>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_card_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_cards e
            LEFT JOIN user_spirit_cards ue ON e.id = ue.spirit_card_id
            LEFT JOIN card_captains_spirit_card che 
                ON che.user_spirit_card_id = ue.spirit_card_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_card_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_card_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritCards sb = new SpiritCards
                {
                    Id = reader.GetString("spirit_card_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
                    Star = reader.GetInt32("star"),
                    Level = reader.GetInt32("level"),
                    Experiment = reader.GetDouble("experiment"),
                    Quantity = reader.GetDouble("quantity"),
                    Power = reader.GetDouble("power"),
                    Health = reader.GetDouble("health"),
                    PhysicalAttack = reader.GetDouble("physical_attack"),
                    PhysicalDefense = reader.GetDouble("physical_defense"),
                    MagicalAttack = reader.GetDouble("magical_attack"),
                    MagicalDefense = reader.GetDouble("magical_defense"),
                    ChemicalAttack = reader.GetDouble("chemical_attack"),
                    ChemicalDefense = reader.GetDouble("chemical_defense"),
                    AtomicAttack = reader.GetDouble("atomic_attack"),
                    AtomicDefense = reader.GetDouble("atomic_defense"),
                    MentalAttack = reader.GetDouble("mental_attack"),
                    MentalDefense = reader.GetDouble("mental_defense"),
                    Speed = reader.GetDouble("speed"),
                    CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                    CriticalRate = reader.GetDouble("critical_rate"),
                    CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                    PenetrationRate = reader.GetDouble("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                    EvasionRate = reader.GetDouble("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDouble("accuracy_rate"),
                    LifestealRate = reader.GetDouble("lifesteal_rate"),
                    ShieldStrength = reader.GetDouble("shield_strength"),
                    Tenacity = reader.GetDouble("tenacity"),
                    ResistanceRate = reader.GetDouble("resistance_rate"),
                    ComboRate = reader.GetDouble("combo_rate"),
                    IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                    StunRate = reader.GetDouble("stun_rate"),
                    IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                    ReflectionRate = reader.GetDouble("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                    Mana = reader.GetDouble("mana"),
                    ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                    // EquipStatus = reader.GetString("equip_status")
                };

                SpiritCardList.Add(sb);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }

        return SpiritCardList;
    }
    public async Task<List<SpiritCards>> GetAllUserCardColonelsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> SpiritCardList = new List<SpiritCards>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_card_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_cards e
            LEFT JOIN user_spirit_cards ue ON e.id = ue.spirit_card_id
            LEFT JOIN card_colonels_spirit_card che 
                ON che.user_spirit_card_id = ue.spirit_card_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_card_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_card_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritCards sb = new SpiritCards
                {
                    Id = reader.GetString("spirit_card_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
                    Star = reader.GetInt32("star"),
                    Level = reader.GetInt32("level"),
                    Experiment = reader.GetDouble("experiment"),
                    Quantity = reader.GetDouble("quantity"),
                    Power = reader.GetDouble("power"),
                    Health = reader.GetDouble("health"),
                    PhysicalAttack = reader.GetDouble("physical_attack"),
                    PhysicalDefense = reader.GetDouble("physical_defense"),
                    MagicalAttack = reader.GetDouble("magical_attack"),
                    MagicalDefense = reader.GetDouble("magical_defense"),
                    ChemicalAttack = reader.GetDouble("chemical_attack"),
                    ChemicalDefense = reader.GetDouble("chemical_defense"),
                    AtomicAttack = reader.GetDouble("atomic_attack"),
                    AtomicDefense = reader.GetDouble("atomic_defense"),
                    MentalAttack = reader.GetDouble("mental_attack"),
                    MentalDefense = reader.GetDouble("mental_defense"),
                    Speed = reader.GetDouble("speed"),
                    CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                    CriticalRate = reader.GetDouble("critical_rate"),
                    CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                    PenetrationRate = reader.GetDouble("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                    EvasionRate = reader.GetDouble("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDouble("accuracy_rate"),
                    LifestealRate = reader.GetDouble("lifesteal_rate"),
                    ShieldStrength = reader.GetDouble("shield_strength"),
                    Tenacity = reader.GetDouble("tenacity"),
                    ResistanceRate = reader.GetDouble("resistance_rate"),
                    ComboRate = reader.GetDouble("combo_rate"),
                    IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                    StunRate = reader.GetDouble("stun_rate"),
                    IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                    ReflectionRate = reader.GetDouble("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                    Mana = reader.GetDouble("mana"),
                    ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                    // EquipStatus = reader.GetString("equip_status")
                };

                SpiritCardList.Add(sb);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }

        return SpiritCardList;
    }
    public async Task<List<SpiritCards>> GetAllUserCardGeneralsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> SpiritCardList = new List<SpiritCards>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_card_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_cards e
            LEFT JOIN user_spirit_cards ue ON e.id = ue.spirit_card_id
            LEFT JOIN card_generals_spirit_card che 
                ON che.user_spirit_card_id = ue.spirit_card_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_card_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_card_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritCards sb = new SpiritCards
                {
                    Id = reader.GetString("spirit_card_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
                    Star = reader.GetInt32("star"),
                    Level = reader.GetInt32("level"),
                    Experiment = reader.GetDouble("experiment"),
                    Quantity = reader.GetDouble("quantity"),
                    Power = reader.GetDouble("power"),
                    Health = reader.GetDouble("health"),
                    PhysicalAttack = reader.GetDouble("physical_attack"),
                    PhysicalDefense = reader.GetDouble("physical_defense"),
                    MagicalAttack = reader.GetDouble("magical_attack"),
                    MagicalDefense = reader.GetDouble("magical_defense"),
                    ChemicalAttack = reader.GetDouble("chemical_attack"),
                    ChemicalDefense = reader.GetDouble("chemical_defense"),
                    AtomicAttack = reader.GetDouble("atomic_attack"),
                    AtomicDefense = reader.GetDouble("atomic_defense"),
                    MentalAttack = reader.GetDouble("mental_attack"),
                    MentalDefense = reader.GetDouble("mental_defense"),
                    Speed = reader.GetDouble("speed"),
                    CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                    CriticalRate = reader.GetDouble("critical_rate"),
                    CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                    PenetrationRate = reader.GetDouble("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                    EvasionRate = reader.GetDouble("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDouble("accuracy_rate"),
                    LifestealRate = reader.GetDouble("lifesteal_rate"),
                    ShieldStrength = reader.GetDouble("shield_strength"),
                    Tenacity = reader.GetDouble("tenacity"),
                    ResistanceRate = reader.GetDouble("resistance_rate"),
                    ComboRate = reader.GetDouble("combo_rate"),
                    IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                    StunRate = reader.GetDouble("stun_rate"),
                    IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                    ReflectionRate = reader.GetDouble("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                    Mana = reader.GetDouble("mana"),
                    ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                    // EquipStatus = reader.GetString("equip_status")
                };

                SpiritCardList.Add(sb);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }

        return SpiritCardList;
    }
    public async Task<List<SpiritCards>> GetAllUserCardAdmiralsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> SpiritCardList = new List<SpiritCards>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_card_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_cards e
            LEFT JOIN user_spirit_cards ue ON e.id = ue.spirit_card_id
            LEFT JOIN card_admirals_spirit_card che 
                ON che.user_spirit_card_id = ue.spirit_card_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_card_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_card_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritCards sb = new SpiritCards
                {
                    Id = reader.GetString("spirit_card_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
                    Star = reader.GetInt32("star"),
                    Level = reader.GetInt32("level"),
                    Experiment = reader.GetDouble("experiment"),
                    Quantity = reader.GetDouble("quantity"),
                    Power = reader.GetDouble("power"),
                    Health = reader.GetDouble("health"),
                    PhysicalAttack = reader.GetDouble("physical_attack"),
                    PhysicalDefense = reader.GetDouble("physical_defense"),
                    MagicalAttack = reader.GetDouble("magical_attack"),
                    MagicalDefense = reader.GetDouble("magical_defense"),
                    ChemicalAttack = reader.GetDouble("chemical_attack"),
                    ChemicalDefense = reader.GetDouble("chemical_defense"),
                    AtomicAttack = reader.GetDouble("atomic_attack"),
                    AtomicDefense = reader.GetDouble("atomic_defense"),
                    MentalAttack = reader.GetDouble("mental_attack"),
                    MentalDefense = reader.GetDouble("mental_defense"),
                    Speed = reader.GetDouble("speed"),
                    CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                    CriticalRate = reader.GetDouble("critical_rate"),
                    CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                    PenetrationRate = reader.GetDouble("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                    EvasionRate = reader.GetDouble("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDouble("accuracy_rate"),
                    LifestealRate = reader.GetDouble("lifesteal_rate"),
                    ShieldStrength = reader.GetDouble("shield_strength"),
                    Tenacity = reader.GetDouble("tenacity"),
                    ResistanceRate = reader.GetDouble("resistance_rate"),
                    ComboRate = reader.GetDouble("combo_rate"),
                    IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                    StunRate = reader.GetDouble("stun_rate"),
                    IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                    ReflectionRate = reader.GetDouble("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                    Mana = reader.GetDouble("mana"),
                    ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                    // EquipStatus = reader.GetString("equip_status")
                };

                SpiritCardList.Add(sb);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }

        return SpiritCardList;
    }
    public async Task<List<SpiritCards>> GetAllUserCardMilitariesSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> SpiritCardList = new List<SpiritCards>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_card_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_cards e
            LEFT JOIN user_spirit_cards ue ON e.id = ue.spirit_card_id
            LEFT JOIN card_militaries_spirit_card che 
                ON che.user_spirit_card_id = ue.spirit_card_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_card_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_card_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritCards sb = new SpiritCards
                {
                    Id = reader.GetString("spirit_card_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
                    Star = reader.GetInt32("star"),
                    Level = reader.GetInt32("level"),
                    Experiment = reader.GetDouble("experiment"),
                    Quantity = reader.GetDouble("quantity"),
                    Power = reader.GetDouble("power"),
                    Health = reader.GetDouble("health"),
                    PhysicalAttack = reader.GetDouble("physical_attack"),
                    PhysicalDefense = reader.GetDouble("physical_defense"),
                    MagicalAttack = reader.GetDouble("magical_attack"),
                    MagicalDefense = reader.GetDouble("magical_defense"),
                    ChemicalAttack = reader.GetDouble("chemical_attack"),
                    ChemicalDefense = reader.GetDouble("chemical_defense"),
                    AtomicAttack = reader.GetDouble("atomic_attack"),
                    AtomicDefense = reader.GetDouble("atomic_defense"),
                    MentalAttack = reader.GetDouble("mental_attack"),
                    MentalDefense = reader.GetDouble("mental_defense"),
                    Speed = reader.GetDouble("speed"),
                    CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                    CriticalRate = reader.GetDouble("critical_rate"),
                    CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                    PenetrationRate = reader.GetDouble("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                    EvasionRate = reader.GetDouble("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDouble("accuracy_rate"),
                    LifestealRate = reader.GetDouble("lifesteal_rate"),
                    ShieldStrength = reader.GetDouble("shield_strength"),
                    Tenacity = reader.GetDouble("tenacity"),
                    ResistanceRate = reader.GetDouble("resistance_rate"),
                    ComboRate = reader.GetDouble("combo_rate"),
                    IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                    StunRate = reader.GetDouble("stun_rate"),
                    IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                    ReflectionRate = reader.GetDouble("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                    Mana = reader.GetDouble("mana"),
                    ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                    // EquipStatus = reader.GetString("equip_status")
                };

                SpiritCardList.Add(sb);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }

        return SpiritCardList;
    }
    public async Task<List<SpiritCards>> GetAllUserCardMonstersSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> SpiritCardList = new List<SpiritCards>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_card_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_cards e
            LEFT JOIN user_spirit_cards ue ON e.id = ue.spirit_card_id
            LEFT JOIN card_monsters_spirit_card che 
                ON che.user_spirit_card_id = ue.spirit_card_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_card_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_card_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritCards sb = new SpiritCards
                {
                    Id = reader.GetString("spirit_card_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
                    Star = reader.GetInt32("star"),
                    Level = reader.GetInt32("level"),
                    Experiment = reader.GetDouble("experiment"),
                    Quantity = reader.GetDouble("quantity"),
                    Power = reader.GetDouble("power"),
                    Health = reader.GetDouble("health"),
                    PhysicalAttack = reader.GetDouble("physical_attack"),
                    PhysicalDefense = reader.GetDouble("physical_defense"),
                    MagicalAttack = reader.GetDouble("magical_attack"),
                    MagicalDefense = reader.GetDouble("magical_defense"),
                    ChemicalAttack = reader.GetDouble("chemical_attack"),
                    ChemicalDefense = reader.GetDouble("chemical_defense"),
                    AtomicAttack = reader.GetDouble("atomic_attack"),
                    AtomicDefense = reader.GetDouble("atomic_defense"),
                    MentalAttack = reader.GetDouble("mental_attack"),
                    MentalDefense = reader.GetDouble("mental_defense"),
                    Speed = reader.GetDouble("speed"),
                    CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                    CriticalRate = reader.GetDouble("critical_rate"),
                    CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                    PenetrationRate = reader.GetDouble("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                    EvasionRate = reader.GetDouble("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDouble("accuracy_rate"),
                    LifestealRate = reader.GetDouble("lifesteal_rate"),
                    ShieldStrength = reader.GetDouble("shield_strength"),
                    Tenacity = reader.GetDouble("tenacity"),
                    ResistanceRate = reader.GetDouble("resistance_rate"),
                    ComboRate = reader.GetDouble("combo_rate"),
                    IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                    StunRate = reader.GetDouble("stun_rate"),
                    IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                    ReflectionRate = reader.GetDouble("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                    Mana = reader.GetDouble("mana"),
                    ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                    // EquipStatus = reader.GetString("equip_status")
                };

                SpiritCardList.Add(sb);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }

        return SpiritCardList;
    }
    public async Task<List<SpiritCards>> GetAllUserCardSpellsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> SpiritCardList = new List<SpiritCards>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_card_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_cards e
            LEFT JOIN user_spirit_cards ue ON e.id = ue.spirit_card_id
            LEFT JOIN card_spells_spirit_card che 
                ON che.user_spirit_card_id = ue.spirit_card_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_card_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_card_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritCards sb = new SpiritCards
                {
                    Id = reader.GetString("spirit_card_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetDouble("quality"),
                    Star = reader.GetInt32("star"),
                    Level = reader.GetInt32("level"),
                    Experiment = reader.GetDouble("experiment"),
                    Quantity = reader.GetDouble("quantity"),
                    Power = reader.GetDouble("power"),
                    Health = reader.GetDouble("health"),
                    PhysicalAttack = reader.GetDouble("physical_attack"),
                    PhysicalDefense = reader.GetDouble("physical_defense"),
                    MagicalAttack = reader.GetDouble("magical_attack"),
                    MagicalDefense = reader.GetDouble("magical_defense"),
                    ChemicalAttack = reader.GetDouble("chemical_attack"),
                    ChemicalDefense = reader.GetDouble("chemical_defense"),
                    AtomicAttack = reader.GetDouble("atomic_attack"),
                    AtomicDefense = reader.GetDouble("atomic_defense"),
                    MentalAttack = reader.GetDouble("mental_attack"),
                    MentalDefense = reader.GetDouble("mental_defense"),
                    Speed = reader.GetDouble("speed"),
                    CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                    CriticalRate = reader.GetDouble("critical_rate"),
                    CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                    PenetrationRate = reader.GetDouble("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                    EvasionRate = reader.GetDouble("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDouble("accuracy_rate"),
                    LifestealRate = reader.GetDouble("lifesteal_rate"),
                    ShieldStrength = reader.GetDouble("shield_strength"),
                    Tenacity = reader.GetDouble("tenacity"),
                    ResistanceRate = reader.GetDouble("resistance_rate"),
                    ComboRate = reader.GetDouble("combo_rate"),
                    IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                    StunRate = reader.GetDouble("stun_rate"),
                    IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                    ReflectionRate = reader.GetDouble("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                    Mana = reader.GetDouble("mana"),
                    ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                    // EquipStatus = reader.GetString("equip_status")
                };

                SpiritCardList.Add(sb);
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }

        return SpiritCardList;
    }
    public async Task<bool> DeleteUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHeroes, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM card_heroes_spirit_card 
                WHERE user_id = @user_id 
                  AND user_card_hero_id = @user_card_hero_id 
                  AND user_spirit_card_id = @user_spirit_card_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_heroes_spirit_card
                        WHERE user_id = @user_id 
                          AND user_card_hero_id = @user_card_hero_id 
                          AND user_spirit_card_id = @user_spirit_card_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> DeleteUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptains, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM card_captains_spirit_card 
                WHERE user_id = @user_id 
                  AND user_card_captain_id = @user_card_captain_id 
                  AND user_spirit_card_id = @user_spirit_card_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_captains_spirit_card
                        WHERE user_id = @user_id 
                          AND user_card_captain_id = @user_card_captain_id 
                          AND user_spirit_card_id = @user_spirit_card_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> DeleteUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonels, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM card_colonels_spirit_card 
                WHERE user_id = @user_id 
                  AND user_card_colonel_id = @user_card_colonel_id 
                  AND user_spirit_card_id = @user_spirit_card_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_colonels_spirit_card
                        WHERE user_id = @user_id 
                          AND user_card_colonel_id = @user_card_colonel_id 
                          AND user_spirit_card_id = @user_spirit_card_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> DeleteUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGenerals, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM card_generals_spirit_card 
                WHERE user_id = @user_id 
                  AND user_card_general_id = @user_card_general_id 
                  AND user_spirit_card_id = @user_spirit_card_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_generals_spirit_card
                        WHERE user_id = @user_id 
                          AND user_card_general_id = @user_card_general_id 
                          AND user_spirit_card_id = @user_spirit_card_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> DeleteUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmirals, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM card_admirals_spirit_card 
                WHERE user_id = @user_id 
                  AND user_card_admiral_id = @user_card_admiral_id 
                  AND user_spirit_card_id = @user_spirit_card_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_admirals_spirit_card
                        WHERE user_id = @user_id 
                          AND user_card_admiral_id = @user_card_admiral_id 
                          AND user_spirit_card_id = @user_spirit_card_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> DeleteUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitaries, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM card_militaries_spirit_card 
                WHERE user_id = @user_id 
                  AND user_card_military_id = @user_card_military_id 
                  AND user_spirit_card_id = @user_spirit_card_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitaries.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_militaries_spirit_card
                        WHERE user_id = @user_id 
                          AND user_card_military_id = @user_card_military_id 
                          AND user_spirit_card_id = @user_spirit_card_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitaries.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> DeleteUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonsters, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM card_monsters_spirit_card 
                WHERE user_id = @user_id 
                  AND user_card_monster_id = @user_card_monster_id 
                  AND user_spirit_card_id = @user_spirit_card_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_monsters_spirit_card
                        WHERE user_id = @user_id 
                          AND user_card_monster_id = @user_card_monster_id 
                          AND user_spirit_card_id = @user_spirit_card_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> DeleteUserCardSpellSpiritCardAsync(string userId, CardSpells cardSpells, SpiritCards SpiritCard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM card_spells_spirit_card 
                WHERE user_id = @user_id 
                  AND user_card_spell_id = @user_card_spell_id 
                  AND user_spirit_card_id = @user_spirit_card_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpells.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_spells_spirit_card
                        WHERE user_id = @user_id 
                          AND user_card_spell_id = @user_card_spell_id 
                          AND user_spirit_card_id = @user_spirit_card_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpells.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<SpiritCards> GetUserSpiritCardByIdAsync(string user_id, string Id)
    {
        SpiritCards card = new SpiritCards();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT * 
                FROM user_spirit_cards 
                WHERE spirit_card_id = @id AND user_id = @user_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            card = new SpiritCards
                            {
                                Id = reader.GetString("spirit_card_id"),
                                Level = reader.GetInt32("level"),
                                Quality = reader.GetDouble("quality"),
                                Experiment = reader.GetDouble("experiment"),
                                Star = reader.GetInt32("star"),
                                Power = reader.GetDouble("power"),
                                Health = reader.GetDouble("health"),
                                PhysicalAttack = reader.GetDouble("physical_attack"),
                                PhysicalDefense = reader.GetDouble("physical_defense"),
                                MagicalAttack = reader.GetDouble("magical_attack"),
                                MagicalDefense = reader.GetDouble("magical_defense"),
                                ChemicalAttack = reader.GetDouble("chemical_attack"),
                                ChemicalDefense = reader.GetDouble("chemical_defense"),
                                AtomicAttack = reader.GetDouble("atomic_attack"),
                                AtomicDefense = reader.GetDouble("atomic_defense"),
                                MentalAttack = reader.GetDouble("mental_attack"),
                                MentalDefense = reader.GetDouble("mental_defense"),
                                Speed = reader.GetDouble("speed"),
                                CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                                CriticalRate = reader.GetDouble("critical_rate"),
                                CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                                PenetrationRate = reader.GetDouble("penetration_rate"),
                                PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                                EvasionRate = reader.GetDouble("evasion_rate"),
                                DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.GetDouble("accuracy_rate"),
                                LifestealRate = reader.GetDouble("lifesteal_rate"),
                                ShieldStrength = reader.GetDouble("shield_strength"),
                                Tenacity = reader.GetDouble("tenacity"),
                                ResistanceRate = reader.GetDouble("resistance_rate"),
                                ComboRate = reader.GetDouble("combo_rate"),
                                IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                                ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                                ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                                StunRate = reader.GetDouble("stun_rate"),
                                IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                                ReflectionRate = reader.GetDouble("reflection_rate"),
                                IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                                Mana = reader.GetDouble("mana"),
                                ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDouble("skill_resistance_rate")
                            };
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return card;
    }
    public async Task<SpiritCards> SumPowerUserSpiritCardsAsync()
    {
        SpiritCards sumSpiritCard = new SpiritCards();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    SUM(power * (1 + quality / 10.0)) AS total_power,
                    SUM(health * (1 + quality / 10.0)) AS total_health,
                    SUM(mana * (1 + quality / 10.0)) AS total_mana,
                    SUM(physical_attack * (1 + quality / 10.0)) AS total_physical_attack,
                    SUM(physical_defense * (1 + quality / 10.0)) AS total_physical_defense,
                    SUM(magical_attack * (1 + quality / 10.0)) AS total_magical_attack,
                    SUM(magical_defense * (1 + quality / 10.0)) AS total_magical_defense,
                    SUM(chemical_attack * (1 + quality / 10.0)) AS total_chemical_attack,
                    SUM(chemical_defense * (1 + quality / 10.0)) AS total_chemical_defense,
                    SUM(atomic_attack * (1 + quality / 10.0)) AS total_atomic_attack,
                    SUM(atomic_defense * (1 + quality / 10.0)) AS total_atomic_defense,
                    SUM(mental_attack * (1 + quality / 10.0)) AS total_mental_attack,
                    SUM(mental_defense * (1 + quality / 10.0)) AS total_mental_defense,
                    SUM(speed * (1 + quality / 10.0)) AS total_speed,
                    SUM(critical_damage_rate * (1 + quality / 10.0)) AS total_critical_damage_rate,
                    SUM(critical_rate * (1 + quality / 10.0)) AS total_critical_rate,
                    SUM(critical_resistance_rate * (1 + quality / 10.0)) AS total_critical_resistance_rate,
                    SUM(ignore_critical_rate * (1 + quality / 10.0)) AS total_ignore_critical_rate,
                    SUM(penetration_rate * (1 + quality / 10.0)) AS total_penetration_rate,
                    SUM(penetration_resistance_rate * (1 + quality / 10.0)) AS total_penetration_resistance_rate,
                    SUM(evasion_rate * (1 + quality / 10.0)) AS total_evasion_rate,
                    SUM(damage_absorption_rate * (1 + quality / 10.0)) AS total_damage_absorption_rate,
                    SUM(ignore_damage_absorption_rate * (1 + quality / 10.0)) AS total_ignore_damage_absorption_rate,
                    SUM(absorbed_damage_rate * (1 + quality / 10.0)) AS total_absorbed_damage_rate,
                    SUM(vitality_regeneration_rate * (1 + quality / 10.0)) AS total_vitality_regeneration_rate,
                    SUM(vitality_regeneration_resistance_rate * (1 + quality / 10.0)) AS total_vitality_regeneration_resistance_rate,
                    SUM(accuracy_rate * (1 + quality / 10.0)) AS total_accuracy_rate,
                    SUM(lifesteal_rate * (1 + quality / 10.0)) AS total_lifesteal_rate,
                    SUM(shield_strength * (1 + quality / 10.0)) AS total_shield_strength,
                    SUM(tenacity * (1 + quality / 10.0)) AS total_tenacity,
                    SUM(resistance_rate * (1 + quality / 10.0)) AS total_resistance_rate,
                    SUM(combo_rate * (1 + quality / 10.0)) AS total_combo_rate,
                    SUM(ignore_combo_rate * (1 + quality / 10.0)) AS total_ignore_combo_rate,
                    SUM(combo_damage_rate * (1 + quality / 10.0)) AS total_combo_damage_rate,
                    SUM(combo_resistance_rate * (1 + quality / 10.0)) AS total_combo_resistance_rate,
                    SUM(stun_rate * (1 + quality / 10.0)) AS total_stun_rate,
                    SUM(ignore_stun_rate * (1 + quality / 10.0)) AS total_ignore_stun_rate,
                    SUM(reflection_rate * (1 + quality / 10.0)) AS total_reflection_rate,
                    SUM(ignore_reflection_rate * (1 + quality / 10.0)) AS total_ignore_reflection_rate,
                    SUM(reflection_damage_rate * (1 + quality / 10.0)) AS total_reflection_damage_rate,
                    SUM(reflection_resistance_rate * (1 + quality / 10.0)) AS total_reflection_resistance_rate,
                    SUM(mana_regeneration_rate * (1 + quality / 10.0)) AS total_mana_regeneration_rate,
                    SUM(damage_to_different_faction_rate * (1 + quality / 10.0)) AS total_damage_to_different_faction_rate,
                    SUM(resistance_to_different_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_different_faction_rate,
                    SUM(damage_to_same_faction_rate * (1 + quality / 10.0)) AS total_damage_to_same_faction_rate,
                    SUM(resistance_to_same_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_same_faction_rate,
                    SUM(normal_damage_rate * (1 + quality / 10.0)) AS total_normal_damage_rate,
                    SUM(normal_resistance_rate * (1 + quality / 10.0)) AS total_normal_resistance_rate,
                    SUM(skill_damage_rate * (1 + quality / 10.0)) AS total_skill_damage_rate,
                    SUM(skill_resistance_rate * (1 + quality / 10.0)) AS total_skill_resistance_rate
                FROM user_spirit_cards
                WHERE user_id = @user_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumSpiritCard.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                            sumSpiritCard.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                            sumSpiritCard.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDouble("total_mana");
                            sumSpiritCard.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                            sumSpiritCard.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                            sumSpiritCard.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                            sumSpiritCard.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                            sumSpiritCard.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                            sumSpiritCard.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                            sumSpiritCard.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                            sumSpiritCard.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                            sumSpiritCard.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                            sumSpiritCard.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                            sumSpiritCard.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                            sumSpiritCard.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                            sumSpiritCard.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                            sumSpiritCard.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                            sumSpiritCard.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                            sumSpiritCard.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                            sumSpiritCard.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                            sumSpiritCard.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                            sumSpiritCard.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                            sumSpiritCard.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                            sumSpiritCard.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                            sumSpiritCard.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                            sumSpiritCard.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                            sumSpiritCard.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                            sumSpiritCard.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                            sumSpiritCard.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                            sumSpiritCard.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                            sumSpiritCard.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                            sumSpiritCard.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                            sumSpiritCard.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                            sumSpiritCard.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                            sumSpiritCard.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                            sumSpiritCard.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                            sumSpiritCard.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                            sumSpiritCard.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                            sumSpiritCard.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                            sumSpiritCard.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                            sumSpiritCard.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                            sumSpiritCard.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                            sumSpiritCard.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                            sumSpiritCard.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                            sumSpiritCard.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                            sumSpiritCard.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                            sumSpiritCard.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                            sumSpiritCard.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                            sumSpiritCard.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                            sumSpiritCard.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return sumSpiritCard;
    }
}
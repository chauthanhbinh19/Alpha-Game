using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserSpiritCardsRepository : IUserSpiritCardsRepository
{
    public async Task<List<SpiritCards>> GetUserSpiritCardsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> spiritCards = new List<SpiritCards>();
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
                    and ut.user_id = @userId ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    query += " AND t.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND t.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND t.name LIKE CONCAT('%', @search, '%')";
                }

                query += " ORDER BY t.name";
                query += " LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    if (!string.IsNullOrEmpty(type) && type != "All")
                    {
                        command.Parameters.AddWithValue("@type", type);
                    }

                    if (!string.IsNullOrEmpty(rare) && rare != "All")
                    {
                        command.Parameters.AddWithValue("@rare", rare);
                    }

                    if (!string.IsNullOrEmpty(search))
                    {
                        command.Parameters.AddWithValue("@search", search);
                    }
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            SpiritCards spiritCard = new SpiritCards
                            {
                                Id = reader.GetStringSafe("spirit_card_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Star = reader.GetIntSafe("star"),
                                Level = reader.GetIntSafe("level"),
                                Experiment = reader.GetDoubleSafe("experiment"),
                                Quantity = reader.GetDoubleSafe("quantity"),
                                Power = reader.GetDoubleSafe("power"),
                                Health = reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                Description = reader.GetStringSafe("description")
                            };

                            spiritCards.Add(spiritCard);
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

        return spiritCards;
    }
    public async Task<List<SpiritCards>> GetAllUserSpiritCardsAsync(string user_id, int pageSize, int offset)
    {
        List<SpiritCards> spiritCards = new List<SpiritCards>();
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
                            SpiritCards spiritCard = new SpiritCards
                            {
                                Id = reader.GetStringSafe("spirit_card_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Star = reader.GetIntSafe("star"),
                                Level = reader.GetIntSafe("level"),
                                Experiment = reader.GetDoubleSafe("experiment"),
                                Quantity = reader.GetDoubleSafe("quantity"),
                                Power = reader.GetDoubleSafe("power"),
                                Health = reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                Description = reader.GetStringSafe("description")
                            };

                            spiritCards.Add(spiritCard);
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

        return spiritCards;
    }
    public async Task<List<SpiritCards>> GetSpiritCardsByCardIdsAsync(string user_id, List<string> cardIds)
    {
        List<SpiritCards> spiritCards = new List<SpiritCards>();
        if (cardIds == null || cardIds.Count == 0) return spiritCards;

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            // 1. Tạo các tên tham số động: @id0, @id1, @id2...
            var paramNames = cardIds.Select((id, i) => "@id" + i).ToArray();
            string inClause = string.Join(",", paramNames);

            // 2. Ghép vào câu Query
            string query = $@"
            SELECT ut.*, t.id, t.name, t.image, t.rare, t.description 
            FROM spirit_cards t, user_spirit_cards ut
            WHERE t.id = ut.spirit_card_id 
                AND ut.user_id = @userId
                AND ut.card_hero_id IN ({inClause})";

            await using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                // 3. Add tham số cố định
                command.Parameters.AddWithValue("@userId", user_id);

                // 4. Add danh sách tham số động
                for (int i = 0; i < cardIds.Count; i++)
                {
                    command.Parameters.AddWithValue("@id" + i, cardIds[i]);
                }

                // 5. Đọc dữ liệu (Vẫn dùng các hàm Get...Safe của bạn)
                await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        spiritCards.Add(new SpiritCards
                        {
                            Id = reader.GetStringSafe("spirit_beast_id"),
                            Name = reader.GetStringSafe("name"),
                            Image = reader.GetStringSafe("image"),
                            Rare = reader.GetStringSafe("rare"),
                            Quality = reader.GetDoubleSafe("quality"),
                            Star = reader.GetIntSafe("star"),
                            Level = reader.GetIntSafe("level"),
                            Experiment = reader.GetDoubleSafe("experiment"),
                            Quantity = reader.GetDoubleSafe("quantity"),
                            Power = reader.GetDoubleSafe("power"),
                            Health = reader.GetDoubleSafe("health"),
                            PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                            PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                            MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                            MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                            ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                            ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                            AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                            AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                            MentalAttack = reader.GetDoubleSafe("mental_attack"),
                            MentalDefense = reader.GetDoubleSafe("mental_defense"),
                            Speed = reader.GetDoubleSafe("speed"),
                            CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                            CriticalRate = reader.GetDoubleSafe("critical_rate"),
                            CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                            IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                            PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                            PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                            EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                            DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                            IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                            AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                            VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                            VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                            AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                            LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                            ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                            Tenacity = reader.GetDoubleSafe("tenacity"),
                            ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                            ComboRate = reader.GetDoubleSafe("combo_rate"),
                            IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                            ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                            ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                            StunRate = reader.GetDoubleSafe("stun_rate"),
                            IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                            ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                            IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                            ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                            ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                            Mana = reader.GetDoubleSafe("mana"),
                            ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                            DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                            ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                            DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                            ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                            NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                            NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                            SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                            SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                            Description = reader.GetStringSafe("description")
                        });
                    }
                }
            }
        }
        return spiritCards;
    }
    public async Task<int> GetUserSpiritCardsCountAsync(string user_id, string search, string type, string rare)
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
                    and ut.user_id = @userId
                ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    query += " AND t.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND t.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND t.name LIKE CONCAT('%', @search, '%')";
                }

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    if (!string.IsNullOrEmpty(type) && type != "All")
                    {
                        command.Parameters.AddWithValue("@type", type);
                    }

                    if (!string.IsNullOrEmpty(rare) && rare != "All")
                    {
                        command.Parameters.AddWithValue("@rare", rare);
                    }

                    if (!string.IsNullOrEmpty(search))
                    {
                        command.Parameters.AddWithValue("@search", search);
                    }

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
    public async Task<SpiritCards> GetUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHero)
    {
        SpiritCards spiritCard = new SpiritCards();
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
                    command.Parameters.AddWithValue("@user_card_hero_id", cardHero.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritCard.Id = reader.GetStringSafe("spirit_card_id");
                            spiritCard.Name = reader.GetStringSafe("name");
                            spiritCard.Image = reader.GetStringSafe("image");
                            spiritCard.Rare = reader.GetStringSafe("rare");
                            spiritCard.Quality = reader.GetDoubleSafe("quality");
                            spiritCard.Star = reader.GetIntSafe("star");
                            spiritCard.Level = reader.GetIntSafe("level");
                            spiritCard.Experiment = reader.GetDoubleSafe("experiment");
                            spiritCard.Quantity = reader.GetDoubleSafe("quantity");
                            spiritCard.Power = reader.GetDoubleSafe("power");
                            spiritCard.Health = reader.GetDoubleSafe("health");
                            spiritCard.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            spiritCard.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            spiritCard.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            spiritCard.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            spiritCard.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            spiritCard.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            spiritCard.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            spiritCard.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            spiritCard.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            spiritCard.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            spiritCard.Speed = reader.GetDoubleSafe("speed");
                            spiritCard.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            spiritCard.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            spiritCard.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            spiritCard.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            spiritCard.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            spiritCard.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            spiritCard.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            spiritCard.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            spiritCard.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            spiritCard.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            spiritCard.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            spiritCard.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            spiritCard.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            spiritCard.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            spiritCard.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            spiritCard.Tenacity = reader.GetDoubleSafe("tenacity");
                            spiritCard.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            spiritCard.ComboRate = reader.GetDoubleSafe("combo_rate");
                            spiritCard.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            spiritCard.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            spiritCard.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            spiritCard.StunRate = reader.GetDoubleSafe("stun_rate");
                            spiritCard.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            spiritCard.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            spiritCard.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            spiritCard.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            spiritCard.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            spiritCard.Mana = reader.GetDoubleSafe("mana");
                            spiritCard.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            spiritCard.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            spiritCard.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            spiritCard.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            spiritCard.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            spiritCard.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            spiritCard.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            spiritCard.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            spiritCard.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            spiritCard.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            spiritCard.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            spiritCard.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            spiritCard.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            spiritCard.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            spiritCard.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            spiritCard.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            spiritCard.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            spiritCard.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            spiritCard.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            spiritCard.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return spiritCard;
    }
    public async Task<SpiritCards> GetUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptain)
    {
        SpiritCards spiritCard = new SpiritCards();
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
                    command.Parameters.AddWithValue("@user_card_captain_id", cardCaptain.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritCard.Id = reader.GetStringSafe("spirit_card_id");
                            spiritCard.Name = reader.GetStringSafe("name");
                            spiritCard.Image = reader.GetStringSafe("image");
                            spiritCard.Rare = reader.GetStringSafe("rare");
                            spiritCard.Quality = reader.GetDoubleSafe("quality");
                            spiritCard.Star = reader.GetIntSafe("star");
                            spiritCard.Level = reader.GetIntSafe("level");
                            spiritCard.Experiment = reader.GetDoubleSafe("experiment");
                            spiritCard.Quantity = reader.GetDoubleSafe("quantity");
                            spiritCard.Power = reader.GetDoubleSafe("power");
                            spiritCard.Health = reader.GetDoubleSafe("health");
                            spiritCard.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            spiritCard.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            spiritCard.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            spiritCard.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            spiritCard.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            spiritCard.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            spiritCard.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            spiritCard.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            spiritCard.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            spiritCard.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            spiritCard.Speed = reader.GetDoubleSafe("speed");
                            spiritCard.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            spiritCard.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            spiritCard.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            spiritCard.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            spiritCard.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            spiritCard.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            spiritCard.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            spiritCard.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            spiritCard.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            spiritCard.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            spiritCard.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            spiritCard.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            spiritCard.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            spiritCard.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            spiritCard.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            spiritCard.Tenacity = reader.GetDoubleSafe("tenacity");
                            spiritCard.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            spiritCard.ComboRate = reader.GetDoubleSafe("combo_rate");
                            spiritCard.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            spiritCard.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            spiritCard.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            spiritCard.StunRate = reader.GetDoubleSafe("stun_rate");
                            spiritCard.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            spiritCard.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            spiritCard.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            spiritCard.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            spiritCard.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            spiritCard.Mana = reader.GetDoubleSafe("mana");
                            spiritCard.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            spiritCard.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            spiritCard.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            spiritCard.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            spiritCard.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            spiritCard.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            spiritCard.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            spiritCard.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            spiritCard.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            spiritCard.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            spiritCard.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            spiritCard.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            spiritCard.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            spiritCard.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            spiritCard.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            spiritCard.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            spiritCard.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            spiritCard.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            spiritCard.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            spiritCard.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return spiritCard;
    }
    public async Task<SpiritCards> GetUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonel)
    {
        SpiritCards spiritCard = new SpiritCards();
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
                command.Parameters.AddWithValue("@user_card_colonel_id", cardColonel.Id);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    spiritCard.Id = reader.GetStringSafe("spirit_card_id");
                    spiritCard.Name = reader.GetStringSafe("name");
                    spiritCard.Image = reader.GetStringSafe("image");
                    spiritCard.Rare = reader.GetStringSafe("rare");
                    spiritCard.Quality = reader.GetDoubleSafe("quality");
                    spiritCard.Star = reader.GetIntSafe("star");
                    spiritCard.Level = reader.GetIntSafe("level");
                    spiritCard.Experiment = reader.GetDoubleSafe("experiment");
                    spiritCard.Quantity = reader.GetDoubleSafe("quantity");
                    spiritCard.Power = reader.GetDoubleSafe("power");
                    spiritCard.Health = reader.GetDoubleSafe("health");
                    spiritCard.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                    spiritCard.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                    spiritCard.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                    spiritCard.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                    spiritCard.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                    spiritCard.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                    spiritCard.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                    spiritCard.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                    spiritCard.MentalAttack = reader.GetDoubleSafe("mental_attack");
                    spiritCard.MentalDefense = reader.GetDoubleSafe("mental_defense");
                    spiritCard.Speed = reader.GetDoubleSafe("speed");
                    spiritCard.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                    spiritCard.CriticalRate = reader.GetDoubleSafe("critical_rate");
                    spiritCard.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                    spiritCard.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                    spiritCard.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                    spiritCard.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                    spiritCard.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                    spiritCard.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                    spiritCard.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                    spiritCard.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                    spiritCard.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                    spiritCard.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                    spiritCard.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                    spiritCard.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                    spiritCard.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                    spiritCard.Tenacity = reader.GetDoubleSafe("tenacity");
                    spiritCard.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                    spiritCard.ComboRate = reader.GetDoubleSafe("combo_rate");
                    spiritCard.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                    spiritCard.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                    spiritCard.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                    spiritCard.StunRate = reader.GetDoubleSafe("stun_rate");
                    spiritCard.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                    spiritCard.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                    spiritCard.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                    spiritCard.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                    spiritCard.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                    spiritCard.Mana = reader.GetDoubleSafe("mana");
                    spiritCard.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                    spiritCard.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                    spiritCard.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                    spiritCard.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                    spiritCard.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                    spiritCard.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                    spiritCard.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                    spiritCard.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                    spiritCard.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                    spiritCard.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                    spiritCard.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                    spiritCard.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                    spiritCard.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                    spiritCard.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                    spiritCard.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                    spiritCard.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                    spiritCard.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                    spiritCard.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                    spiritCard.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                    spiritCard.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return spiritCard;
    }
    public async Task<SpiritCards> GetUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGeneral)
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
                    command.Parameters.AddWithValue("@user_card_general_id", cardGeneral.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            SpiritCard.Id = reader.GetStringSafe("spirit_card_id");
                            SpiritCard.Name = reader.GetStringSafe("name");
                            SpiritCard.Image = reader.GetStringSafe("image");
                            SpiritCard.Rare = reader.GetStringSafe("rare");
                            SpiritCard.Quality = reader.GetDoubleSafe("quality");
                            SpiritCard.Star = reader.GetIntSafe("star");
                            SpiritCard.Level = reader.GetIntSafe("level");
                            SpiritCard.Experiment = reader.GetDoubleSafe("experiment");
                            SpiritCard.Quantity = reader.GetDoubleSafe("quantity");
                            SpiritCard.Power = reader.GetDoubleSafe("power");
                            SpiritCard.Health = reader.GetDoubleSafe("health");
                            SpiritCard.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            SpiritCard.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            SpiritCard.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            SpiritCard.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            SpiritCard.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            SpiritCard.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            SpiritCard.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            SpiritCard.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            SpiritCard.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            SpiritCard.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            SpiritCard.Speed = reader.GetDoubleSafe("speed");
                            SpiritCard.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            SpiritCard.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            SpiritCard.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            SpiritCard.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            SpiritCard.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            SpiritCard.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            SpiritCard.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            SpiritCard.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            SpiritCard.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            SpiritCard.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            SpiritCard.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            SpiritCard.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            SpiritCard.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            SpiritCard.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            SpiritCard.Tenacity = reader.GetDoubleSafe("tenacity");
                            SpiritCard.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            SpiritCard.ComboRate = reader.GetDoubleSafe("combo_rate");
                            SpiritCard.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            SpiritCard.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            SpiritCard.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            SpiritCard.StunRate = reader.GetDoubleSafe("stun_rate");
                            SpiritCard.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            SpiritCard.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            SpiritCard.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            SpiritCard.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            SpiritCard.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            SpiritCard.Mana = reader.GetDoubleSafe("mana");
                            SpiritCard.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            SpiritCard.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            SpiritCard.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            SpiritCard.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            SpiritCard.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            SpiritCard.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            SpiritCard.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            SpiritCard.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            SpiritCard.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            SpiritCard.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            SpiritCard.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            SpiritCard.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            SpiritCard.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            SpiritCard.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            SpiritCard.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            SpiritCard.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            SpiritCard.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            SpiritCard.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            SpiritCard.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            SpiritCard.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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
    public async Task<SpiritCards> GetUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmiral)
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
                    command.Parameters.AddWithValue("@user_card_admiral_id", cardAdmiral.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            SpiritCard.Id = reader.GetStringSafe("spirit_card_id");
                            SpiritCard.Name = reader.GetStringSafe("name");
                            SpiritCard.Image = reader.GetStringSafe("image");
                            SpiritCard.Rare = reader.GetStringSafe("rare");
                            SpiritCard.Quality = reader.GetDoubleSafe("quality");
                            SpiritCard.Star = reader.GetIntSafe("star");
                            SpiritCard.Level = reader.GetIntSafe("level");
                            SpiritCard.Experiment = reader.GetDoubleSafe("experiment");
                            SpiritCard.Quantity = reader.GetDoubleSafe("quantity");
                            SpiritCard.Power = reader.GetDoubleSafe("power");
                            SpiritCard.Health = reader.GetDoubleSafe("health");
                            SpiritCard.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            SpiritCard.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            SpiritCard.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            SpiritCard.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            SpiritCard.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            SpiritCard.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            SpiritCard.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            SpiritCard.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            SpiritCard.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            SpiritCard.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            SpiritCard.Speed = reader.GetDoubleSafe("speed");
                            SpiritCard.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            SpiritCard.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            SpiritCard.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            SpiritCard.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            SpiritCard.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            SpiritCard.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            SpiritCard.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            SpiritCard.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            SpiritCard.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            SpiritCard.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            SpiritCard.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            SpiritCard.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            SpiritCard.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            SpiritCard.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            SpiritCard.Tenacity = reader.GetDoubleSafe("tenacity");
                            SpiritCard.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            SpiritCard.ComboRate = reader.GetDoubleSafe("combo_rate");
                            SpiritCard.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            SpiritCard.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            SpiritCard.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            SpiritCard.StunRate = reader.GetDoubleSafe("stun_rate");
                            SpiritCard.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            SpiritCard.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            SpiritCard.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            SpiritCard.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            SpiritCard.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            SpiritCard.Mana = reader.GetDoubleSafe("mana");
                            SpiritCard.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            SpiritCard.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            SpiritCard.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            SpiritCard.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            SpiritCard.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            SpiritCard.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            SpiritCard.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            SpiritCard.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            SpiritCard.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            SpiritCard.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            SpiritCard.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            SpiritCard.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            SpiritCard.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            SpiritCard.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            SpiritCard.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            SpiritCard.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            SpiritCard.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            SpiritCard.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            SpiritCard.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            SpiritCard.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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
                            SpiritCard.Id = reader.GetStringSafe("spirit_card_id");
                            SpiritCard.Name = reader.GetStringSafe("name");
                            SpiritCard.Image = reader.GetStringSafe("image");
                            SpiritCard.Rare = reader.GetStringSafe("rare");
                            SpiritCard.Quality = reader.GetDoubleSafe("quality");
                            SpiritCard.Star = reader.GetIntSafe("star");
                            SpiritCard.Level = reader.GetIntSafe("level");
                            SpiritCard.Experiment = reader.GetDoubleSafe("experiment");
                            SpiritCard.Quantity = reader.GetDoubleSafe("quantity");
                            SpiritCard.Power = reader.GetDoubleSafe("power");
                            SpiritCard.Health = reader.GetDoubleSafe("health");
                            SpiritCard.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            SpiritCard.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            SpiritCard.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            SpiritCard.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            SpiritCard.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            SpiritCard.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            SpiritCard.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            SpiritCard.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            SpiritCard.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            SpiritCard.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            SpiritCard.Speed = reader.GetDoubleSafe("speed");
                            SpiritCard.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            SpiritCard.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            SpiritCard.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            SpiritCard.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            SpiritCard.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            SpiritCard.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            SpiritCard.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            SpiritCard.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            SpiritCard.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            SpiritCard.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            SpiritCard.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            SpiritCard.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            SpiritCard.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            SpiritCard.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            SpiritCard.Tenacity = reader.GetDoubleSafe("tenacity");
                            SpiritCard.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            SpiritCard.ComboRate = reader.GetDoubleSafe("combo_rate");
                            SpiritCard.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            SpiritCard.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            SpiritCard.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            SpiritCard.StunRate = reader.GetDoubleSafe("stun_rate");
                            SpiritCard.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            SpiritCard.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            SpiritCard.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            SpiritCard.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            SpiritCard.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            SpiritCard.Mana = reader.GetDoubleSafe("mana");
                            SpiritCard.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            SpiritCard.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            SpiritCard.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            SpiritCard.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            SpiritCard.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            SpiritCard.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            SpiritCard.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            SpiritCard.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            SpiritCard.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            SpiritCard.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            SpiritCard.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            SpiritCard.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            SpiritCard.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            SpiritCard.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            SpiritCard.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            SpiritCard.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            SpiritCard.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            SpiritCard.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            SpiritCard.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            SpiritCard.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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
    public async Task<SpiritCards> GetUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonster)
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
                    command.Parameters.AddWithValue("@user_card_monster_id", cardMonster.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            SpiritCard.Id = reader.GetStringSafe("spirit_card_id");
                            SpiritCard.Name = reader.GetStringSafe("name");
                            SpiritCard.Image = reader.GetStringSafe("image");
                            SpiritCard.Rare = reader.GetStringSafe("rare");
                            SpiritCard.Quality = reader.GetDoubleSafe("quality");
                            SpiritCard.Star = reader.GetIntSafe("star");
                            SpiritCard.Level = reader.GetIntSafe("level");
                            SpiritCard.Experiment = reader.GetDoubleSafe("experiment");
                            SpiritCard.Quantity = reader.GetDoubleSafe("quantity");
                            SpiritCard.Power = reader.GetDoubleSafe("power");
                            SpiritCard.Health = reader.GetDoubleSafe("health");
                            SpiritCard.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            SpiritCard.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            SpiritCard.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            SpiritCard.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            SpiritCard.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            SpiritCard.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            SpiritCard.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            SpiritCard.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            SpiritCard.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            SpiritCard.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            SpiritCard.Speed = reader.GetDoubleSafe("speed");
                            SpiritCard.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            SpiritCard.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            SpiritCard.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            SpiritCard.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            SpiritCard.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            SpiritCard.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            SpiritCard.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            SpiritCard.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            SpiritCard.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            SpiritCard.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            SpiritCard.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            SpiritCard.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            SpiritCard.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            SpiritCard.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            SpiritCard.Tenacity = reader.GetDoubleSafe("tenacity");
                            SpiritCard.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            SpiritCard.ComboRate = reader.GetDoubleSafe("combo_rate");
                            SpiritCard.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            SpiritCard.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            SpiritCard.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            SpiritCard.StunRate = reader.GetDoubleSafe("stun_rate");
                            SpiritCard.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            SpiritCard.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            SpiritCard.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            SpiritCard.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            SpiritCard.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            SpiritCard.Mana = reader.GetDoubleSafe("mana");
                            SpiritCard.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            SpiritCard.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            SpiritCard.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            SpiritCard.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            SpiritCard.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            SpiritCard.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            SpiritCard.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            SpiritCard.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            SpiritCard.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            SpiritCard.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            SpiritCard.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            SpiritCard.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            SpiritCard.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            SpiritCard.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            SpiritCard.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            SpiritCard.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            SpiritCard.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            SpiritCard.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            SpiritCard.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            SpiritCard.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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
                            SpiritCard.Id = reader.GetStringSafe("spirit_card_id");
                            SpiritCard.Name = reader.GetStringSafe("name");
                            SpiritCard.Image = reader.GetStringSafe("image");
                            SpiritCard.Rare = reader.GetStringSafe("rare");
                            SpiritCard.Quality = reader.GetDoubleSafe("quality");
                            SpiritCard.Star = reader.GetIntSafe("star");
                            SpiritCard.Level = reader.GetIntSafe("level");
                            SpiritCard.Experiment = reader.GetDoubleSafe("experiment");
                            SpiritCard.Quantity = reader.GetDoubleSafe("quantity");
                            SpiritCard.Power = reader.GetDoubleSafe("power");
                            SpiritCard.Health = reader.GetDoubleSafe("health");
                            SpiritCard.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            SpiritCard.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            SpiritCard.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            SpiritCard.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            SpiritCard.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            SpiritCard.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            SpiritCard.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            SpiritCard.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            SpiritCard.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            SpiritCard.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            SpiritCard.Speed = reader.GetDoubleSafe("speed");
                            SpiritCard.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            SpiritCard.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            SpiritCard.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            SpiritCard.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            SpiritCard.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            SpiritCard.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            SpiritCard.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            SpiritCard.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            SpiritCard.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            SpiritCard.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            SpiritCard.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            SpiritCard.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            SpiritCard.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            SpiritCard.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            SpiritCard.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            SpiritCard.Tenacity = reader.GetDoubleSafe("tenacity");
                            SpiritCard.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            SpiritCard.ComboRate = reader.GetDoubleSafe("combo_rate");
                            SpiritCard.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            SpiritCard.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            SpiritCard.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            SpiritCard.StunRate = reader.GetDoubleSafe("stun_rate");
                            SpiritCard.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            SpiritCard.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            SpiritCard.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            SpiritCard.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            SpiritCard.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            SpiritCard.Mana = reader.GetDoubleSafe("mana");
                            SpiritCard.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            SpiritCard.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            SpiritCard.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            SpiritCard.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            SpiritCard.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            SpiritCard.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            SpiritCard.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            SpiritCard.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            SpiritCard.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            SpiritCard.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            SpiritCard.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            SpiritCard.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            SpiritCard.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            SpiritCard.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            SpiritCard.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            SpiritCard.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            SpiritCard.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            SpiritCard.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            SpiritCard.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            SpiritCard.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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
    public async Task<bool> InsertOrUpdateUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHero, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_hero_id", cardHero.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_heroes_spirit_card (user_id, user_card_hero_id, user_spirit_card_id) VALUES (@user_id, @user_card_hero_id, @user_spirit_card_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_hero_id", cardHero.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_card_id", SpiritCard.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"UPDATE card_heroes_spirit_card SET user_spirit_card_id = @user_spirit_card_id WHERE user_id = @user_id AND user_card_hero_id = @user_card_hero_id;";

                        await using var updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_hero_id", cardHero.Id);
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
    public async Task<bool> InsertOrUpdateUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptain, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptain.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_captains_spirit_card 
                                      (user_id, user_card_captain_id, user_spirit_card_id)
                                       VALUES (@user_id, @user_card_captain_id, @user_spirit_card_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptain.Id);
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
                        updateCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptain.Id);
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
    public async Task<bool> InsertOrUpdateUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonel, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonel.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_colonels_spirit_card
                                      (user_id, user_card_colonel_id, user_spirit_card_id)
                                      VALUES (@user_id, @user_card_colonel_id, @user_spirit_card_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonel.Id);
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
                        updateCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonel.Id);
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
    public async Task<bool> InsertOrUpdateUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGeneral, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_general_id", cardGeneral.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_generals_spirit_card
                                      (user_id, user_card_general_id, user_spirit_card_id)
                                      VALUES (@user_id, @user_card_general_id, @user_spirit_card_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_general_id", cardGeneral.Id);
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
                        updateCommand.Parameters.AddWithValue("@user_card_general_id", cardGeneral.Id);
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
    public async Task<bool> InsertOrUpdateUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmiral, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmiral.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_admirals_spirit_card
                                      (user_id, user_card_admiral_id, user_spirit_card_id)
                                      VALUES (@user_id, @user_card_admiral_id, @user_spirit_card_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmiral.Id); // sửa lỗi
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
                        updateCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmiral.Id);
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
    public async Task<bool> InsertOrUpdateUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonster, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonster.Id);

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
                        insertCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonster.Id);
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
                        updateCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonster.Id);
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
        List<SpiritCards> spiritCards = new List<SpiritCards>();
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
                    Id = reader.GetStringSafe("spirit_card_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experiment = reader.GetDoubleSafe("experiment"),
                    Quantity = reader.GetDoubleSafe("quantity"),
                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritCards.Add(sb);
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

        return spiritCards;
    }
    public async Task<List<SpiritCards>> GetAllUserCardCaptainsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> spiritCards = new List<SpiritCards>();
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
                    Id = reader.GetStringSafe("spirit_card_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experiment = reader.GetDoubleSafe("experiment"),
                    Quantity = reader.GetDoubleSafe("quantity"),
                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritCards.Add(sb);
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

        return spiritCards;
    }
    public async Task<List<SpiritCards>> GetAllUserCardColonelsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> spiritCards = new List<SpiritCards>();
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
                    Id = reader.GetStringSafe("spirit_card_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experiment = reader.GetDoubleSafe("experiment"),
                    Quantity = reader.GetDoubleSafe("quantity"),
                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritCards.Add(sb);
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

        return spiritCards;
    }
    public async Task<List<SpiritCards>> GetAllUserCardGeneralsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> spiritCards = new List<SpiritCards>();
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
                    Id = reader.GetStringSafe("spirit_card_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experiment = reader.GetDoubleSafe("experiment"),
                    Quantity = reader.GetDoubleSafe("quantity"),
                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritCards.Add(sb);
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

        return spiritCards;
    }
    public async Task<List<SpiritCards>> GetAllUserCardAdmiralsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> spiritCards = new List<SpiritCards>();
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
                    Id = reader.GetStringSafe("spirit_card_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experiment = reader.GetDoubleSafe("experiment"),
                    Quantity = reader.GetDoubleSafe("quantity"),
                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritCards.Add(sb);
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

        return spiritCards;
    }
    public async Task<List<SpiritCards>> GetAllUserCardMilitariesSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> spiritCards = new List<SpiritCards>();
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
                    Id = reader.GetStringSafe("spirit_card_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experiment = reader.GetDoubleSafe("experiment"),
                    Quantity = reader.GetDoubleSafe("quantity"),
                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritCards.Add(sb);
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

        return spiritCards;
    }
    public async Task<List<SpiritCards>> GetAllUserCardMonstersSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> spiritCards = new List<SpiritCards>();
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
                    Id = reader.GetStringSafe("spirit_card_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experiment = reader.GetDoubleSafe("experiment"),
                    Quantity = reader.GetDoubleSafe("quantity"),
                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritCards.Add(sb);
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

        return spiritCards;
    }
    public async Task<List<SpiritCards>> GetAllUserCardSpellsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritCards> spiritCards = new List<SpiritCards>();
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
                    Id = reader.GetStringSafe("spirit_card_id"),
                    Name = reader.GetStringSafe("name"),
                    Image = reader.GetStringSafe("image"),
                    Rare = reader.GetStringSafe("rare"),
                    Quality = reader.GetDoubleSafe("quality"),
                    Star = reader.GetIntSafe("star"),
                    Level = reader.GetIntSafe("level"),
                    Experiment = reader.GetDoubleSafe("experiment"),
                    Quantity = reader.GetDoubleSafe("quantity"),
                    Power = reader.GetDoubleSafe("power"),
                    Health = reader.GetDoubleSafe("health"),
                    PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                    PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                    MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                    MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                    ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                    ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                    AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                    AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                    MentalAttack = reader.GetDoubleSafe("mental_attack"),
                    MentalDefense = reader.GetDoubleSafe("mental_defense"),
                    Speed = reader.GetDoubleSafe("speed"),
                    CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                    CriticalRate = reader.GetDoubleSafe("critical_rate"),
                    CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                    IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                    PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                    PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                    EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                    DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                    IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                    AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                    VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                    VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                    AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                    LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                    ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                    Tenacity = reader.GetDoubleSafe("tenacity"),
                    ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                    ComboRate = reader.GetDoubleSafe("combo_rate"),
                    IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                    ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                    ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                    StunRate = reader.GetDoubleSafe("stun_rate"),
                    IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                    ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                    IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                    ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                    ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                    Mana = reader.GetDoubleSafe("mana"),
                    ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                    DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                    ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                    DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                    ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                    NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                    NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                    SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                    SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritCards.Add(sb);
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

        return spiritCards;
    }
    public async Task<bool> DeleteUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHero, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_hero_id", cardHero.Id);
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
                            deleteCommand.Parameters.AddWithValue("@user_card_hero_id", cardHero.Id);
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
    public async Task<bool> DeleteUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptain, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptain.Id);
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
                            deleteCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptain.Id);
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
    public async Task<bool> DeleteUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonel, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonel.Id);
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
                            deleteCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonel.Id);
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
    public async Task<bool> DeleteUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGeneral, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_general_id", cardGeneral.Id);
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
                            deleteCommand.Parameters.AddWithValue("@user_card_general_id", cardGeneral.Id);
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
    public async Task<bool> DeleteUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmiral, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmiral.Id);
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
                            deleteCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmiral.Id);
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
    public async Task<bool> DeleteUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitary, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);
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
                            deleteCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);
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
    public async Task<bool> DeleteUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonster, SpiritCards SpiritCard)
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
                    checkCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonster.Id);
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
                            deleteCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonster.Id);
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
        SpiritCards spiritCard = new SpiritCards();
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
                            spiritCard = new SpiritCards
                            {
                                Id = reader.GetStringSafe("spirit_card_id"),
                                Level = reader.GetIntSafe("level"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Experiment = reader.GetDoubleSafe("experiment"),
                                Star = reader.GetIntSafe("star"),
                                Power = reader.GetDoubleSafe("power"),
                                Health = reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate")
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

        return spiritCard;
    }
    public async Task<SpiritCards> SumPowerUserSpiritCardsAsync()
    {
        SpiritCards sumSpiritCards = new SpiritCards();
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
                            sumSpiritCards.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            sumSpiritCards.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            sumSpiritCards.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            sumSpiritCards.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            sumSpiritCards.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            sumSpiritCards.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            sumSpiritCards.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            sumSpiritCards.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            sumSpiritCards.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            sumSpiritCards.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            sumSpiritCards.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            sumSpiritCards.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            sumSpiritCards.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            sumSpiritCards.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            sumSpiritCards.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            sumSpiritCards.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            sumSpiritCards.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            sumSpiritCards.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            sumSpiritCards.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            sumSpiritCards.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            sumSpiritCards.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            sumSpiritCards.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            sumSpiritCards.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            sumSpiritCards.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            sumSpiritCards.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            sumSpiritCards.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            sumSpiritCards.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            sumSpiritCards.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            sumSpiritCards.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            sumSpiritCards.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            sumSpiritCards.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            sumSpiritCards.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            sumSpiritCards.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            sumSpiritCards.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            sumSpiritCards.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            sumSpiritCards.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            sumSpiritCards.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            sumSpiritCards.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            sumSpiritCards.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            sumSpiritCards.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            sumSpiritCards.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            sumSpiritCards.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            sumSpiritCards.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            sumSpiritCards.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            sumSpiritCards.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            sumSpiritCards.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            sumSpiritCards.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            sumSpiritCards.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            sumSpiritCards.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            sumSpiritCards.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
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

        return sumSpiritCards;
    }
}
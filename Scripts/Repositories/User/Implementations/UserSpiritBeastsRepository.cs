using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserSpiritBeastsRepository : IUserSpiritBeastsRepository
{
    public async Task<List<SpiritBeasts>> GetUserSpiritBeastsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                Select ut.*, t.id, t.name, t.image, t.rare, t.description 
                from spirit_beasts t, user_spirit_beasts ut 
                where t.id = ut.spirit_beast_id ";

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND t.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND t.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += " ORDER BY t.name";
                selectSQL += " LIMIT @limit OFFSET @offset";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);

                    if (!string.IsNullOrEmpty(rare) && rare != "All")
                    {
                        selectCommand.Parameters.AddWithValue("@rare", rare);
                    }

                    if (!string.IsNullOrEmpty(search))
                    {
                        selectCommand.Parameters.AddWithValue("@search", search);
                    }
                    selectCommand.Parameters.AddWithValue("@limit", pageSize);
                    selectCommand.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            SpiritBeasts spiritBeast = new SpiritBeasts
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
                            };

                            spiritBeasts.Add(spiritBeast);
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

        return spiritBeasts;
    }
    public async Task<List<SpiritBeasts>> GetAllUserSpiritBeastsAsync(string userId, int pageSize, int offset)
    {
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                Select ut.*, t.id, t.name, t.image, t.rare, t.description 
                from spirit_beasts t, user_spirit_beasts ut
                where t.id = ut.spirit_beast_id 
                    and ut.user_id = @userId
                ORDER BY t.name REGEXP '[0-9]+$',
                         CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED),
                         t.name
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    selectCommand.Parameters.AddWithValue("@limit", pageSize);
                    selectCommand.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            SpiritBeasts spiritBeast = new SpiritBeasts
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
                            };

                            spiritBeasts.Add(spiritBeast);
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

        return spiritBeasts;
    }
    public async Task<List<SpiritBeasts>> GetSpiritBeastsByCardIdsAsync(string userId, List<string> cardIds)
    {
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        if (cardIds == null || cardIds.Count == 0) return spiritBeasts;

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            // 1. Tạo các tên tham số động: @id0, @id1, @id2...
            var paramNames = cardIds.Select((id, i) => "@id" + i).ToArray();
            string inClause = string.Join(",", paramNames);

            // 2. Ghép vào câu Query
            string selectSQL = $@"
            SELECT ut.*, t.id, t.name, t.image, t.rare, t.description 
            FROM spirit_beasts t, user_spirit_beasts ut
            WHERE t.id = ut.spirit_beast_id 
                AND ut.user_id = @userId
                AND ut.card_hero_id IN ({inClause})";

            await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                // 3. Add tham số cố định
                selectCommand.Parameters.AddWithValue("@userId", userId);

                // 4. Add danh sách tham số động
                for (int i = 0; i < cardIds.Count; i++)
                {
                    selectCommand.Parameters.AddWithValue("@id" + i, cardIds[i]);
                }

                // 5. Đọc dữ liệu (Vẫn dùng các hàm Get...Safe của bạn)
                await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        spiritBeasts.Add(new SpiritBeasts
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
        return spiritBeasts;
    }
    public async Task<int> GetUserSpiritBeastsCountAsync(string userId, string search, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                Select count(*) 
                from spirit_beasts t, user_spirit_beasts ut
                where t.id = ut.spirit_beast_id
                ";

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND t.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND t.name LIKE CONCAT('%', @search, '%')";
                }

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);

                    if (!string.IsNullOrEmpty(rare) && rare != "All")
                    {
                        selectCommand.Parameters.AddWithValue("@rare", rare);
                    }

                    if (!string.IsNullOrEmpty(search))
                    {
                        selectCommand.Parameters.AddWithValue("@search", search);
                    }

                    object result = await selectCommand.ExecuteScalarAsync();
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
    public async Task<SpiritBeasts> GetUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHero)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                select ue.*, e.*
                from spirit_beasts e 
                left join user_spirit_beasts ue 
                    on e.id = ue.spirit_beast_id
                left join card_heroes_spirit_beast che 
                    on che.user_spirit_beast_id = ue.spirit_beast_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                and che.user_card_hero_id = @user_card_hero_id;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    selectCommand.Parameters.AddWithValue("@user_card_hero_id", cardHero.Id);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetStringSafe("spirit_beast_id");
                            spiritBeast.Name = reader.GetStringSafe("name");
                            spiritBeast.Image = reader.GetStringSafe("image");
                            spiritBeast.Rare = reader.GetStringSafe("rare");
                            spiritBeast.Quality = reader.GetDoubleSafe("quality");
                            spiritBeast.Star = reader.GetIntSafe("star");
                            spiritBeast.Level = reader.GetIntSafe("level");
                            spiritBeast.Experiment = reader.GetDoubleSafe("experiment");
                            spiritBeast.Quantity = reader.GetDoubleSafe("quantity");
                            spiritBeast.Power = reader.GetDoubleSafe("power");
                            spiritBeast.Health = reader.GetDoubleSafe("health");
                            spiritBeast.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            spiritBeast.Speed = reader.GetDoubleSafe("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            spiritBeast.Tenacity = reader.GetDoubleSafe("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDoubleSafe("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDoubleSafe("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetDoubleSafe("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return spiritBeast;
    }
    public async Task<SpiritBeasts> GetUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptain)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                select ue.*, e.*
                from spirit_beasts e 
                left join user_spirit_beasts ue 
                    on e.id = ue.spirit_beast_id
                left join card_captains_spirit_beast che 
                    on che.user_spirit_beast_id = ue.spirit_beast_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                  and che.user_card_captain_id = @user_card_captain_id;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    selectCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptain.Id);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetStringSafe("spirit_beast_id");
                            spiritBeast.Name = reader.GetStringSafe("name");
                            spiritBeast.Image = reader.GetStringSafe("image");
                            spiritBeast.Rare = reader.GetStringSafe("rare");
                            spiritBeast.Quality = reader.GetDoubleSafe("quality");
                            spiritBeast.Star = reader.GetIntSafe("star");
                            spiritBeast.Level = reader.GetIntSafe("level");
                            spiritBeast.Experiment = reader.GetDoubleSafe("experiment");
                            spiritBeast.Quantity = reader.GetDoubleSafe("quantity");
                            spiritBeast.Power = reader.GetDoubleSafe("power");
                            spiritBeast.Health = reader.GetDoubleSafe("health");
                            spiritBeast.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            spiritBeast.Speed = reader.GetDoubleSafe("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            spiritBeast.Tenacity = reader.GetDoubleSafe("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDoubleSafe("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDoubleSafe("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetDoubleSafe("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return spiritBeast;
    }
    public async Task<SpiritBeasts> GetUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonel)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                select ue.*, e.*
                from spirit_beasts e 
                left join user_spirit_beasts ue 
                    on e.id = ue.spirit_beast_id
                left join card_colonels_spirit_beast che 
                    on che.user_spirit_beast_id = ue.spirit_beast_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                  and che.user_card_colonel_id = @user_card_colonel_id;
            ";

                await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonel.Id);

                await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    spiritBeast.Id = reader.GetStringSafe("spirit_beast_id");
                    spiritBeast.Name = reader.GetStringSafe("name");
                    spiritBeast.Image = reader.GetStringSafe("image");
                    spiritBeast.Rare = reader.GetStringSafe("rare");
                    spiritBeast.Quality = reader.GetDoubleSafe("quality");
                    spiritBeast.Star = reader.GetIntSafe("star");
                    spiritBeast.Level = reader.GetIntSafe("level");
                    spiritBeast.Experiment = reader.GetDoubleSafe("experiment");
                    spiritBeast.Quantity = reader.GetDoubleSafe("quantity");
                    spiritBeast.Power = reader.GetDoubleSafe("power");
                    spiritBeast.Health = reader.GetDoubleSafe("health");
                    spiritBeast.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                    spiritBeast.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                    spiritBeast.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                    spiritBeast.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                    spiritBeast.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                    spiritBeast.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                    spiritBeast.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                    spiritBeast.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                    spiritBeast.MentalAttack = reader.GetDoubleSafe("mental_attack");
                    spiritBeast.MentalDefense = reader.GetDoubleSafe("mental_defense");
                    spiritBeast.Speed = reader.GetDoubleSafe("speed");
                    spiritBeast.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                    spiritBeast.CriticalRate = reader.GetDoubleSafe("critical_rate");
                    spiritBeast.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                    spiritBeast.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                    spiritBeast.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                    spiritBeast.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                    spiritBeast.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                    spiritBeast.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                    spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                    spiritBeast.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                    spiritBeast.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                    spiritBeast.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                    spiritBeast.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                    spiritBeast.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                    spiritBeast.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                    spiritBeast.Tenacity = reader.GetDoubleSafe("tenacity");
                    spiritBeast.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                    spiritBeast.ComboRate = reader.GetDoubleSafe("combo_rate");
                    spiritBeast.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                    spiritBeast.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                    spiritBeast.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                    spiritBeast.StunRate = reader.GetDoubleSafe("stun_rate");
                    spiritBeast.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                    spiritBeast.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                    spiritBeast.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                    spiritBeast.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                    spiritBeast.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                    spiritBeast.Mana = reader.GetDoubleSafe("mana");
                    spiritBeast.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                    spiritBeast.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                    spiritBeast.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                    spiritBeast.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                    spiritBeast.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                    spiritBeast.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                    spiritBeast.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                    spiritBeast.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                    spiritBeast.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                    spiritBeast.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                    spiritBeast.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                    spiritBeast.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                    spiritBeast.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                    spiritBeast.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                    spiritBeast.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                    spiritBeast.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                    spiritBeast.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                    spiritBeast.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                    spiritBeast.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                    spiritBeast.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return spiritBeast;
    }
    public async Task<SpiritBeasts> GetUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGeneral)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                select ue.*, e.*
                from spirit_beasts e 
                left join user_spirit_beasts ue 
                    on e.id = ue.spirit_beast_id
                left join card_generals_spirit_beast che 
                    on che.user_spirit_beast_id = ue.spirit_beast_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                    and che.user_card_general_id = @user_card_general_id;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    selectCommand.Parameters.AddWithValue("@user_card_general_id", cardGeneral.Id);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetStringSafe("spirit_beast_id");
                            spiritBeast.Name = reader.GetStringSafe("name");
                            spiritBeast.Image = reader.GetStringSafe("image");
                            spiritBeast.Rare = reader.GetStringSafe("rare");
                            spiritBeast.Quality = reader.GetDoubleSafe("quality");
                            spiritBeast.Star = reader.GetIntSafe("star");
                            spiritBeast.Level = reader.GetIntSafe("level");
                            spiritBeast.Experiment = reader.GetDoubleSafe("experiment");
                            spiritBeast.Quantity = reader.GetDoubleSafe("quantity");
                            spiritBeast.Power = reader.GetDoubleSafe("power");
                            spiritBeast.Health = reader.GetDoubleSafe("health");
                            spiritBeast.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            spiritBeast.Speed = reader.GetDoubleSafe("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            spiritBeast.Tenacity = reader.GetDoubleSafe("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDoubleSafe("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDoubleSafe("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetDoubleSafe("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return spiritBeast;
    }
    public async Task<SpiritBeasts> GetUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmiral)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                select ue.*, e.*
                from spirit_beasts e 
                left join user_spirit_beasts ue 
                    on e.id = ue.spirit_beast_id
                left join card_admirals_spirit_beast che 
                    on che.user_spirit_beast_id = ue.spirit_beast_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                    and che.user_card_admiral_id = @user_card_admiral_id;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    selectCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmiral.Id);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetStringSafe("spirit_beast_id");
                            spiritBeast.Name = reader.GetStringSafe("name");
                            spiritBeast.Image = reader.GetStringSafe("image");
                            spiritBeast.Rare = reader.GetStringSafe("rare");
                            spiritBeast.Quality = reader.GetDoubleSafe("quality");
                            spiritBeast.Star = reader.GetIntSafe("star");
                            spiritBeast.Level = reader.GetIntSafe("level");
                            spiritBeast.Experiment = reader.GetDoubleSafe("experiment");
                            spiritBeast.Quantity = reader.GetDoubleSafe("quantity");
                            spiritBeast.Power = reader.GetDoubleSafe("power");
                            spiritBeast.Health = reader.GetDoubleSafe("health");
                            spiritBeast.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            spiritBeast.Speed = reader.GetDoubleSafe("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            spiritBeast.Tenacity = reader.GetDoubleSafe("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDoubleSafe("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDoubleSafe("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetDoubleSafe("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return spiritBeast;
    }
    public async Task<SpiritBeasts> GetUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitary)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                select ue.*, e.*
                from spirit_beasts e 
                left join user_spirit_beasts ue 
                    on e.id = ue.spirit_beast_id
                left join card_militaries_spirit_beast che 
                    on che.user_spirit_beast_id = ue.spirit_beast_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                    and che.user_card_military_id = @user_card_military_id;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    selectCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetStringSafe("spirit_beast_id");
                            spiritBeast.Name = reader.GetStringSafe("name");
                            spiritBeast.Image = reader.GetStringSafe("image");
                            spiritBeast.Rare = reader.GetStringSafe("rare");
                            spiritBeast.Quality = reader.GetDoubleSafe("quality");
                            spiritBeast.Star = reader.GetIntSafe("star");
                            spiritBeast.Level = reader.GetIntSafe("level");
                            spiritBeast.Experiment = reader.GetDoubleSafe("experiment");
                            spiritBeast.Quantity = reader.GetDoubleSafe("quantity");
                            spiritBeast.Power = reader.GetDoubleSafe("power");
                            spiritBeast.Health = reader.GetDoubleSafe("health");
                            spiritBeast.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            spiritBeast.Speed = reader.GetDoubleSafe("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            spiritBeast.Tenacity = reader.GetDoubleSafe("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDoubleSafe("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDoubleSafe("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetDoubleSafe("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return spiritBeast;
    }
    public async Task<SpiritBeasts> GetUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonster)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                select ue.*, e.*
                from spirit_beasts e 
                left join user_spirit_beasts ue 
                    on e.id = ue.spirit_beast_id
                left join card_monsters_spirit_beast che 
                    on che.user_spirit_beast_id = ue.spirit_beast_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                    and che.user_card_monster_id = @user_card_monster_id;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    selectCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonster.Id);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetStringSafe("spirit_beast_id");
                            spiritBeast.Name = reader.GetStringSafe("name");
                            spiritBeast.Image = reader.GetStringSafe("image");
                            spiritBeast.Rare = reader.GetStringSafe("rare");
                            spiritBeast.Quality = reader.GetDoubleSafe("quality");
                            spiritBeast.Star = reader.GetIntSafe("star");
                            spiritBeast.Level = reader.GetIntSafe("level");
                            spiritBeast.Experiment = reader.GetDoubleSafe("experiment");
                            spiritBeast.Quantity = reader.GetDoubleSafe("quantity");
                            spiritBeast.Power = reader.GetDoubleSafe("power");
                            spiritBeast.Health = reader.GetDoubleSafe("health");
                            spiritBeast.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            spiritBeast.Speed = reader.GetDoubleSafe("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            spiritBeast.Tenacity = reader.GetDoubleSafe("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDoubleSafe("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDoubleSafe("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetDoubleSafe("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return spiritBeast;
    }
    public async Task<SpiritBeasts> GetUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpell)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                select ue.*, e.*
                from spirit_beasts e 
                left join user_spirit_beasts ue 
                    on e.id = ue.spirit_beast_id
                left join card_spells_spirit_beast che 
                    on che.user_spirit_beast_id = ue.spirit_beast_id 
                    and che.user_id = ue.user_id
                where ue.user_id = @userId 
                    and che.user_card_spell_id = @user_card_spell_id;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    selectCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpell.Id);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetStringSafe("spirit_beast_id");
                            spiritBeast.Name = reader.GetStringSafe("name");
                            spiritBeast.Image = reader.GetStringSafe("image");
                            spiritBeast.Rare = reader.GetStringSafe("rare");
                            spiritBeast.Quality = reader.GetDoubleSafe("quality");
                            spiritBeast.Star = reader.GetIntSafe("star");
                            spiritBeast.Level = reader.GetIntSafe("level");
                            spiritBeast.Experiment = reader.GetDoubleSafe("experiment");
                            spiritBeast.Quantity = reader.GetDoubleSafe("quantity");
                            spiritBeast.Power = reader.GetDoubleSafe("power");
                            spiritBeast.Health = reader.GetDoubleSafe("health");
                            spiritBeast.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            spiritBeast.Speed = reader.GetDoubleSafe("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            spiritBeast.Tenacity = reader.GetDoubleSafe("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDoubleSafe("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDoubleSafe("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetDoubleSafe("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return spiritBeast;
    }
    public async Task<bool> InsertUserSpiritBeastAsync(SpiritBeasts SpiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) FROM user_spirit_beasts 
                WHERE user_id = @user_id AND spirit_beast_id = @spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@spirit_beast_id", SpiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertSQL = @"
                        INSERT INTO user_spirit_beasts (
                            user_id, spirit_beast_id, rare, level, experiment, star, quality, block, quantity,
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
                            @user_id, @spirit_beast_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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

                        await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCommand.Parameters.AddWithValue("@spirit_beast_id", SpiritBeast.Id);
                            insertCommand.Parameters.AddWithValue("@rare", SpiritBeast.Rare);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experiment", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(SpiritBeast.Rare));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", SpiritBeast.Quantity);
                            insertCommand.Parameters.AddWithValue("@power", SpiritBeast.Power);
                            insertCommand.Parameters.AddWithValue("@health", SpiritBeast.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", SpiritBeast.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", SpiritBeast.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", SpiritBeast.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", SpiritBeast.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", SpiritBeast.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", SpiritBeast.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", SpiritBeast.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", SpiritBeast.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", SpiritBeast.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", SpiritBeast.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", SpiritBeast.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", SpiritBeast.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", SpiritBeast.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", SpiritBeast.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", SpiritBeast.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", SpiritBeast.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", SpiritBeast.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", SpiritBeast.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", SpiritBeast.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", SpiritBeast.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", SpiritBeast.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", SpiritBeast.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", SpiritBeast.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", SpiritBeast.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", SpiritBeast.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", SpiritBeast.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", SpiritBeast.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", SpiritBeast.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", SpiritBeast.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", SpiritBeast.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", SpiritBeast.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", SpiritBeast.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", SpiritBeast.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", SpiritBeast.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", SpiritBeast.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", SpiritBeast.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", SpiritBeast.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", SpiritBeast.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", SpiritBeast.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", SpiritBeast.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", SpiritBeast.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", SpiritBeast.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", SpiritBeast.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", SpiritBeast.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", SpiritBeast.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", SpiritBeast.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", SpiritBeast.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", SpiritBeast.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateSQL = @"
                        UPDATE user_spirit_beasts
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND spirit_beast_id = @spirit_beast_id;
                    ";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCommand.Parameters.AddWithValue("@spirit_beast_id", SpiritBeast.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", SpiritBeast.Quantity);

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
    public async Task<bool> InsertOrUpdateUserSpiritBeastsBatchAsync(List<SpiritBeasts> spiritBeasts)
    {
        if (spiritBeasts == null || spiritBeasts.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 500; // vì nhiều column → giảm size

            for (int i = 0; i < spiritBeasts.Count; i += batchSize)
            {
                var batch = spiritBeasts.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
                INSERT INTO user_spirit_beasts (
                    user_id, spirit_beast_id, rare, level, experiment, star, quality, block, quantity,
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
                ) VALUES ");

                for (int j = 0; j < batch.Count; j++)
                {
                    var c = batch[j];

                    stringBuilder.Append($@"
                    (@user_id, @spirit_beast_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
                    @power_{j}, @health_{j}, @physical_attack_{j}, @physical_defense_{j}, @magical_attack_{j}, @magical_defense_{j},
                    @chemical_attack_{j}, @chemical_defense_{j}, @atomic_attack_{j}, @atomic_defense_{j}, @mental_attack_{j}, @mental_defense_{j},
                    @speed_{j}, @critical_damage_rate_{j}, @critical_rate_{j}, @critical_resistance_rate_{j}, @ignore_critical_rate_{j},
                    @penetration_rate_{j}, @penetration_resistance_rate_{j},
                    @evasion_rate_{j}, @damage_absorption_rate_{j}, @ignore_damage_absorption_rate_{j}, @absorbed_damage_rate_{j},
                    @vitality_regeneration_rate_{j}, @vitality_regeneration_resistance_rate_{j},
                    @accuracy_rate_{j}, @lifesteal_rate_{j}, @shield_strength_{j}, @tenacity_{j}, @resistance_rate_{j},
                    @combo_rate_{j}, @ignore_combo_rate_{j}, @combo_damage_rate_{j}, @combo_resistance_rate_{j},
                    @stun_rate_{j}, @ignore_stun_rate_{j},
                    @reflection_rate_{j}, @ignore_reflection_rate_{j}, @reflection_damage_rate_{j}, @reflection_resistance_rate_{j},
                    @mana_{j}, @mana_regeneration_rate_{j},
                    @damage_to_different_faction_rate_{j}, @resistance_to_different_faction_rate_{j},
                    @damage_to_same_faction_rate_{j}, @resistance_to_same_faction_rate_{j},
                    @normal_damage_rate_{j}, @normal_resistance_rate_{j},
                    @skill_damage_rate_{j}, @skill_resistance_rate_{j}
                    ),");

                    parameters.AddRange(new[]
                    {
                        new MySqlParameter($"@spirit_beast_id_{j}", c.Id),
                        new MySqlParameter($"@rare_{j}", c.Rare),
                        new MySqlParameter($"@quality_{j}", QualityEvaluatorHelper.CheckQuality(c.Rare)),
                        new MySqlParameter($"@quantity_{j}", c.Quantity),
                        new MySqlParameter($"@power_{j}", c.Power),
                        new MySqlParameter($"@health_{j}", c.Health),
                        new MySqlParameter($"@physical_attack_{j}", c.PhysicalAttack),
                        new MySqlParameter($"@physical_defense_{j}", c.PhysicalDefense),
                        new MySqlParameter($"@magical_attack_{j}", c.MagicalAttack),
                        new MySqlParameter($"@magical_defense_{j}", c.MagicalDefense),
                        new MySqlParameter($"@chemical_attack_{j}", c.ChemicalAttack),
                        new MySqlParameter($"@chemical_defense_{j}", c.ChemicalDefense),
                        new MySqlParameter($"@atomic_attack_{j}", c.AtomicAttack),
                        new MySqlParameter($"@atomic_defense_{j}", c.AtomicDefense),
                        new MySqlParameter($"@mental_attack_{j}", c.MentalAttack),
                        new MySqlParameter($"@mental_defense_{j}", c.MentalDefense),
                        new MySqlParameter($"@speed_{j}", c.Speed),
                        new MySqlParameter($"@critical_damage_rate_{j}", c.CriticalDamageRate),
                        new MySqlParameter($"@critical_rate_{j}", c.CriticalRate),
                        new MySqlParameter($"@critical_resistance_rate_{j}", c.CriticalResistanceRate),
                        new MySqlParameter($"@ignore_critical_rate_{j}", c.IgnoreCriticalRate),
                        new MySqlParameter($"@penetration_rate_{j}", c.PenetrationRate),
                        new MySqlParameter($"@penetration_resistance_rate_{j}", c.PenetrationResistanceRate),
                        new MySqlParameter($"@evasion_rate_{j}", c.EvasionRate),
                        new MySqlParameter($"@damage_absorption_rate_{j}", c.DamageAbsorptionRate),
                        new MySqlParameter($"@ignore_damage_absorption_rate_{j}", c.IgnoreDamageAbsorptionRate),
                        new MySqlParameter($"@absorbed_damage_rate_{j}", c.AbsorbedDamageRate),
                        new MySqlParameter($"@vitality_regeneration_rate_{j}", c.VitalityRegenerationRate),
                        new MySqlParameter($"@vitality_regeneration_resistance_rate_{j}", c.VitalityRegenerationResistanceRate),
                        new MySqlParameter($"@accuracy_rate_{j}", c.AccuracyRate),
                        new MySqlParameter($"@lifesteal_rate_{j}", c.LifestealRate),
                        new MySqlParameter($"@shield_strength_{j}", c.ShieldStrength),
                        new MySqlParameter($"@tenacity_{j}", c.Tenacity),
                        new MySqlParameter($"@resistance_rate_{j}", c.ResistanceRate),
                        new MySqlParameter($"@combo_rate_{j}", c.ComboRate),
                        new MySqlParameter($"@ignore_combo_rate_{j}", c.IgnoreComboRate),
                        new MySqlParameter($"@combo_damage_rate_{j}", c.ComboDamageRate),
                        new MySqlParameter($"@combo_resistance_rate_{j}", c.ComboResistanceRate),
                        new MySqlParameter($"@stun_rate_{j}", c.StunRate),
                        new MySqlParameter($"@ignore_stun_rate_{j}", c.IgnoreStunRate),
                        new MySqlParameter($"@reflection_rate_{j}", c.ReflectionRate),
                        new MySqlParameter($"@ignore_reflection_rate_{j}", c.IgnoreReflectionRate),
                        new MySqlParameter($"@reflection_damage_rate_{j}", c.ReflectionDamageRate),
                        new MySqlParameter($"@reflection_resistance_rate_{j}", c.ReflectionResistanceRate),
                        new MySqlParameter($"@mana_{j}", c.Mana),
                        new MySqlParameter($"@mana_regeneration_rate_{j}", c.ManaRegenerationRate),
                        new MySqlParameter($"@damage_to_different_faction_rate_{j}", c.DamageToDifferentFactionRate),
                        new MySqlParameter($"@resistance_to_different_faction_rate_{j}", c.ResistanceToDifferentFactionRate),
                        new MySqlParameter($"@damage_to_same_faction_rate_{j}", c.DamageToSameFactionRate),
                        new MySqlParameter($"@resistance_to_same_faction_rate_{j}", c.ResistanceToSameFactionRate),
                        new MySqlParameter($"@normal_damage_rate_{j}", c.NormalDamageRate),
                        new MySqlParameter($"@normal_resistance_rate_{j}", c.NormalResistanceRate),
                        new MySqlParameter($"@skill_damage_rate_{j}", c.SkillDamageRate),
                        new MySqlParameter($"@skill_resistance_rate_{j}", c.SkillResistanceRate),
                });
                }

                stringBuilder.Length--; // remove dấu ,

                stringBuilder.Append(@"
                ON DUPLICATE KEY UPDATE
                    quantity = user_spirit_beasts.quantity + VALUES(quantity);
                ");

                await using var command = new MySqlCommand(stringBuilder.ToString(), connection, (MySqlTransaction)transaction);

                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddRange(parameters.ToArray());

                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError("Batch Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<bool> UpdateSpiritBeastLevelAsync(SpiritBeasts SpiritBeast, int level)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_spirit_beasts
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
                WHERE user_id = @user_id AND spirit_beast_id = @spirit_beast_id;
            ";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@spirit_beast_id", SpiritBeast.Id);
                    updateCommand.Parameters.AddWithValue("@level", level);
                    updateCommand.Parameters.AddWithValue("@power", SpiritBeast.Power);
                    updateCommand.Parameters.AddWithValue("@health", SpiritBeast.Health);
                    updateCommand.Parameters.AddWithValue("@physical_attack", SpiritBeast.PhysicalAttack);
                    updateCommand.Parameters.AddWithValue("@physical_defense", SpiritBeast.PhysicalDefense);
                    updateCommand.Parameters.AddWithValue("@magical_attack", SpiritBeast.MagicalAttack);
                    updateCommand.Parameters.AddWithValue("@magical_defense", SpiritBeast.MagicalDefense);
                    updateCommand.Parameters.AddWithValue("@chemical_attack", SpiritBeast.ChemicalAttack);
                    updateCommand.Parameters.AddWithValue("@chemical_defense", SpiritBeast.ChemicalDefense);
                    updateCommand.Parameters.AddWithValue("@atomic_attack", SpiritBeast.AtomicAttack);
                    updateCommand.Parameters.AddWithValue("@atomic_defense", SpiritBeast.AtomicDefense);
                    updateCommand.Parameters.AddWithValue("@mental_attack", SpiritBeast.MentalAttack);
                    updateCommand.Parameters.AddWithValue("@mental_defense", SpiritBeast.MentalDefense);
                    updateCommand.Parameters.AddWithValue("@speed", SpiritBeast.Speed);
                    updateCommand.Parameters.AddWithValue("@critical_damage_rate", SpiritBeast.CriticalDamageRate);
                    updateCommand.Parameters.AddWithValue("@critical_rate", SpiritBeast.CriticalRate);
                    updateCommand.Parameters.AddWithValue("@critical_resistance_rate", SpiritBeast.CriticalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@ignore_critical_rate", SpiritBeast.IgnoreCriticalRate);
                    updateCommand.Parameters.AddWithValue("@penetration_rate", SpiritBeast.PenetrationRate);
                    updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", SpiritBeast.PenetrationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@evasion_rate", SpiritBeast.EvasionRate);
                    updateCommand.Parameters.AddWithValue("@damage_absorption_rate", SpiritBeast.DamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", SpiritBeast.IgnoreDamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", SpiritBeast.AbsorbedDamageRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", SpiritBeast.VitalityRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", SpiritBeast.VitalityRegenerationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@accuracy_rate", SpiritBeast.AccuracyRate);
                    updateCommand.Parameters.AddWithValue("@lifesteal_rate", SpiritBeast.LifestealRate);
                    updateCommand.Parameters.AddWithValue("@shield_strength", SpiritBeast.ShieldStrength);
                    updateCommand.Parameters.AddWithValue("@tenacity", SpiritBeast.Tenacity);
                    updateCommand.Parameters.AddWithValue("@resistance_rate", SpiritBeast.ResistanceRate);
                    updateCommand.Parameters.AddWithValue("@combo_rate", SpiritBeast.ComboRate);
                    updateCommand.Parameters.AddWithValue("@ignore_combo_rate", SpiritBeast.IgnoreComboRate);
                    updateCommand.Parameters.AddWithValue("@combo_damage_rate", SpiritBeast.ComboDamageRate);
                    updateCommand.Parameters.AddWithValue("@combo_resistance_rate", SpiritBeast.ComboResistanceRate);
                    updateCommand.Parameters.AddWithValue("@stun_rate", SpiritBeast.StunRate);
                    updateCommand.Parameters.AddWithValue("@ignore_stun_rate", SpiritBeast.IgnoreStunRate);
                    updateCommand.Parameters.AddWithValue("@reflection_rate", SpiritBeast.ReflectionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", SpiritBeast.IgnoreReflectionRate);
                    updateCommand.Parameters.AddWithValue("@reflection_damage_rate", SpiritBeast.ReflectionDamageRate);
                    updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", SpiritBeast.ReflectionResistanceRate);
                    updateCommand.Parameters.AddWithValue("@mana", SpiritBeast.Mana);
                    updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", SpiritBeast.ManaRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", SpiritBeast.DamageToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", SpiritBeast.ResistanceToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", SpiritBeast.DamageToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", SpiritBeast.ResistanceToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@normal_damage_rate", SpiritBeast.NormalDamageRate);
                    updateCommand.Parameters.AddWithValue("@normal_resistance_rate", SpiritBeast.NormalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@skill_damage_rate", SpiritBeast.SkillDamageRate);
                    updateCommand.Parameters.AddWithValue("@skill_resistance_rate", SpiritBeast.SkillResistanceRate);

                    await updateCommand.ExecuteNonQueryAsync();
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
    public async Task<bool> UpdateSpiritBeastBreakthroughAsync(SpiritBeasts SpiritBeast, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_spirit_beasts
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
                WHERE user_id = @user_id AND spirit_beast_id = @spirit_beast_id;
            ";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@spirit_beast_id", SpiritBeast.Id);
                    updateCommand.Parameters.AddWithValue("@star", star);
                    updateCommand.Parameters.AddWithValue("@quantity", quantity);
                    updateCommand.Parameters.AddWithValue("@power", SpiritBeast.Power);
                    updateCommand.Parameters.AddWithValue("@health", SpiritBeast.Health);
                    updateCommand.Parameters.AddWithValue("@physical_attack", SpiritBeast.PhysicalAttack);
                    updateCommand.Parameters.AddWithValue("@physical_defense", SpiritBeast.PhysicalDefense);
                    updateCommand.Parameters.AddWithValue("@magical_attack", SpiritBeast.MagicalAttack);
                    updateCommand.Parameters.AddWithValue("@magical_defense", SpiritBeast.MagicalDefense);
                    updateCommand.Parameters.AddWithValue("@chemical_attack", SpiritBeast.ChemicalAttack);
                    updateCommand.Parameters.AddWithValue("@chemical_defense", SpiritBeast.ChemicalDefense);
                    updateCommand.Parameters.AddWithValue("@atomic_attack", SpiritBeast.AtomicAttack);
                    updateCommand.Parameters.AddWithValue("@atomic_defense", SpiritBeast.AtomicDefense);
                    updateCommand.Parameters.AddWithValue("@mental_attack", SpiritBeast.MentalAttack);
                    updateCommand.Parameters.AddWithValue("@mental_defense", SpiritBeast.MentalDefense);
                    updateCommand.Parameters.AddWithValue("@speed", SpiritBeast.Speed);
                    updateCommand.Parameters.AddWithValue("@critical_damage_rate", SpiritBeast.CriticalDamageRate);
                    updateCommand.Parameters.AddWithValue("@critical_rate", SpiritBeast.CriticalRate);
                    updateCommand.Parameters.AddWithValue("@critical_resistance_rate", SpiritBeast.CriticalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@ignore_critical_rate", SpiritBeast.IgnoreCriticalRate);
                    updateCommand.Parameters.AddWithValue("@penetration_rate", SpiritBeast.PenetrationRate);
                    updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", SpiritBeast.PenetrationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@evasion_rate", SpiritBeast.EvasionRate);
                    updateCommand.Parameters.AddWithValue("@damage_absorption_rate", SpiritBeast.DamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", SpiritBeast.IgnoreDamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", SpiritBeast.AbsorbedDamageRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", SpiritBeast.VitalityRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", SpiritBeast.VitalityRegenerationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@accuracy_rate", SpiritBeast.AccuracyRate);
                    updateCommand.Parameters.AddWithValue("@lifesteal_rate", SpiritBeast.LifestealRate);
                    updateCommand.Parameters.AddWithValue("@shield_strength", SpiritBeast.ShieldStrength);
                    updateCommand.Parameters.AddWithValue("@tenacity", SpiritBeast.Tenacity);
                    updateCommand.Parameters.AddWithValue("@resistance_rate", SpiritBeast.ResistanceRate);
                    updateCommand.Parameters.AddWithValue("@combo_rate", SpiritBeast.ComboRate);
                    updateCommand.Parameters.AddWithValue("@ignore_combo_rate", SpiritBeast.IgnoreComboRate);
                    updateCommand.Parameters.AddWithValue("@combo_damage_rate", SpiritBeast.ComboDamageRate);
                    updateCommand.Parameters.AddWithValue("@combo_resistance_rate", SpiritBeast.ComboResistanceRate);
                    updateCommand.Parameters.AddWithValue("@stun_rate", SpiritBeast.StunRate);
                    updateCommand.Parameters.AddWithValue("@ignore_stun_rate", SpiritBeast.IgnoreStunRate);
                    updateCommand.Parameters.AddWithValue("@reflection_rate", SpiritBeast.ReflectionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", SpiritBeast.IgnoreReflectionRate);
                    updateCommand.Parameters.AddWithValue("@reflection_damage_rate", SpiritBeast.ReflectionDamageRate);
                    updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", SpiritBeast.ReflectionResistanceRate);
                    updateCommand.Parameters.AddWithValue("@mana", SpiritBeast.Mana);
                    updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", SpiritBeast.ManaRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", SpiritBeast.DamageToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", SpiritBeast.ResistanceToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", SpiritBeast.DamageToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", SpiritBeast.ResistanceToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@normal_damage_rate", SpiritBeast.NormalDamageRate);
                    updateCommand.Parameters.AddWithValue("@normal_resistance_rate", SpiritBeast.NormalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@skill_damage_rate", SpiritBeast.SkillDamageRate);
                    updateCommand.Parameters.AddWithValue("@skill_resistance_rate", SpiritBeast.SkillResistanceRate);

                    await updateCommand.ExecuteNonQueryAsync();
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
    public async Task<bool> InsertOrUpdateUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHero, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkSQL = @"SELECT COUNT(*) FROM card_heroes_spirit_beast WHERE user_id = @user_id AND user_card_hero_id = @user_card_hero_id;";

                await using (var checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_hero_id", cardHero.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertSQL = @"INSERT INTO card_heroes_spirit_beast (user_id, user_card_hero_id, user_spirit_beast_id) VALUES (@user_id, @user_card_hero_id, @user_spirit_beast_id);";

                        await using var insertCommand = new MySqlCommand(insertSQL, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_hero_id", cardHero.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateSQL = @"UPDATE card_heroes_spirit_beast SET user_spirit_beast_id = @user_spirit_beast_id WHERE user_id = @user_id AND user_card_hero_id = @user_card_hero_id;";

                        await using var updateCommand = new MySqlCommand(updateSQL, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_hero_id", cardHero.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> InsertOrUpdateUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptain, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkSQL = @"SELECT COUNT(*) FROM card_captains_spirit_beast 
                              WHERE user_id = @user_id AND user_card_captain_id = @user_card_captain_id;";

                await using (var checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptain.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertSQL = @"INSERT INTO card_captains_spirit_beast 
                                      (user_id, user_card_captain_id, user_spirit_beast_id)
                                       VALUES (@user_id, @user_card_captain_id, @user_spirit_beast_id);";

                        await using var insertCommand = new MySqlCommand(insertSQL, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptain.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateSQL = @"UPDATE card_captains_spirit_beast
                                       SET user_spirit_beast_id = @user_spirit_beast_id
                                       WHERE user_id = @user_id AND user_card_captain_id = @user_card_captain_id;";

                        await using var updateCommand = new MySqlCommand(updateSQL, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptain.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> InsertOrUpdateUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonel, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkSQL = @"SELECT COUNT(*) FROM card_colonels_spirit_beast 
                              WHERE user_id = @user_id AND user_card_colonel_id = @user_card_colonel_id;";

                await using (var checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonel.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertSQL = @"INSERT INTO card_colonels_spirit_beast
                                      (user_id, user_card_colonel_id, user_spirit_beast_id)
                                      VALUES (@user_id, @user_card_colonel_id, @user_spirit_beast_id);";

                        await using var insertCommand = new MySqlCommand(insertSQL, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonel.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateSQL = @"UPDATE card_colonels_spirit_beast
                                       SET user_spirit_beast_id = @user_spirit_beast_id
                                       WHERE user_id = @user_id AND user_card_colonel_id = @user_card_colonel_id;";

                        await using var updateCommand = new MySqlCommand(updateSQL, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonel.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> InsertOrUpdateUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGeneral, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkSQL = @"SELECT COUNT(*) FROM card_generals_spirit_beast 
                              WHERE user_id = @user_id AND user_card_general_id = @user_card_general_id;";

                await using (var checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_general_id", cardGeneral.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertSQL = @"INSERT INTO card_generals_spirit_beast
                                      (user_id, user_card_general_id, user_spirit_beast_id)
                                      VALUES (@user_id, @user_card_general_id, @user_spirit_beast_id);";

                        await using var insertCommand = new MySqlCommand(insertSQL, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_general_id", cardGeneral.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateSQL = @"UPDATE card_generals_spirit_beast
                                       SET user_spirit_beast_id = @user_spirit_beast_id
                                       WHERE user_id = @user_id AND user_card_general_id = @user_card_general_id;";

                        await using var updateCommand = new MySqlCommand(updateSQL, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId); // sửa lỗi User.CurrentUserId
                        updateCommand.Parameters.AddWithValue("@user_card_general_id", cardGeneral.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> InsertOrUpdateUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmiral, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkSQL = @"SELECT COUNT(*) FROM card_admirals_spirit_beast 
                              WHERE user_id = @user_id AND user_card_admiral_id = @user_card_admiral_id;";

                await using (var checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmiral.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertSQL = @"INSERT INTO card_admirals_spirit_beast
                                      (user_id, user_card_admiral_id, user_spirit_beast_id)
                                      VALUES (@user_id, @user_card_admiral_id, @user_spirit_beast_id);";

                        await using var insertCommand = new MySqlCommand(insertSQL, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmiral.Id); // sửa lỗi
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateSQL = @"UPDATE card_admirals_spirit_beast
                                       SET user_spirit_beast_id = @user_spirit_beast_id
                                       WHERE user_id = @user_id AND user_card_admiral_id = @user_card_admiral_id;";

                        await using var updateCommand = new MySqlCommand(updateSQL, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId); // sửa lỗi User.CurrentUserId
                        updateCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmiral.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> InsertOrUpdateUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitary, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra bản ghi tồn tại
                string checkSQL = @"
                SELECT COUNT(*) FROM card_military_spirit_beast 
                WHERE user_id = @user_id AND user_card_military_id = @user_card_military_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertSQL = @"
                        INSERT INTO card_military_spirit_beast (
                            user_id, user_card_military_id, user_spirit_beast_id
                        ) VALUES (
                            @user_id, @user_card_military_id, @user_spirit_beast_id
                        );";

                        MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateSQL = @"
                        UPDATE card_military_spirit_beast
                        SET user_spirit_beast_id = @user_spirit_beast_id
                        WHERE user_id = @user_id AND user_card_military_id = @user_card_military_id;";

                        MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> InsertOrUpdateUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonster, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) FROM card_monsters_spirit_beast 
                WHERE user_id = @user_id AND user_card_monster_id = @user_card_monster_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonster.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        // INSERT
                        string insertSQL = @"
                        INSERT INTO card_monsters_spirit_beast (
                            user_id, user_card_monster_id, user_spirit_beast_id
                        ) VALUES (
                            @user_id, @user_card_monster_id, @user_spirit_beast_id
                        );";

                        MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonster.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        // UPDATE
                        string updateSQL = @"
                        UPDATE card_monsters_spirit_beast
                        SET user_spirit_beast_id = @user_spirit_beast_id
                        WHERE user_id = @user_id AND user_card_monster_id = @user_card_monster_id;";

                        MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonster.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> InsertOrUpdateUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpell, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) FROM card_spell_spirit_beast 
                WHERE user_id = @user_id AND user_card_spell_id = @user_card_spell_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpell.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        // INSERT
                        string insertSQL = @"
                        INSERT INTO card_spell_spirit_beast (
                            user_id, user_card_spell_id, user_spirit_beast_id
                        ) VALUES (
                            @user_id, @user_card_spell_id, @user_spirit_beast_id
                        );";

                        MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpell.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        // UPDATE
                        string updateSQL = @"
                        UPDATE card_spell_spirit_beast
                        SET user_spirit_beast_id = @user_spirit_beast_id
                        WHERE user_id = @user_id AND user_card_spell_id = @user_card_spell_id;";

                        MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpell.Id);
                        updateCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<List<SpiritBeasts>> GetAllUserCardHeroesSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_beast_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_beasts e
            LEFT JOIN user_spirit_beasts ue ON e.id = ue.spirit_beast_id
            LEFT JOIN card_heroes_spirit_beast che 
                ON che.user_spirit_beast_id = ue.spirit_beast_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_beast_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_beast_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", userId);
            selectCommand.Parameters.AddWithValue("@limit", pageSize);
            selectCommand.Parameters.AddWithValue("@offset", offset);
            selectCommand.Parameters.AddWithValue("@status", status);

            await using var reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts spiritBeast = new SpiritBeasts
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
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritBeasts.Add(spiritBeast);
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

        return spiritBeasts;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardCaptainsSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_beast_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_beasts e
            LEFT JOIN user_spirit_beasts ue ON e.id = ue.spirit_beast_id
            LEFT JOIN card_captains_spirit_beast che 
                ON che.user_spirit_beast_id = ue.spirit_beast_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_beast_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_beast_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", userId);
            selectCommand.Parameters.AddWithValue("@limit", pageSize);
            selectCommand.Parameters.AddWithValue("@offset", offset);
            selectCommand.Parameters.AddWithValue("@status", status);

            await using var reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts spiritBeast = new SpiritBeasts
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
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritBeasts.Add(spiritBeast);
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

        return spiritBeasts;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardColonelsSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_beast_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_beasts e
            LEFT JOIN user_spirit_beasts ue ON e.id = ue.spirit_beast_id
            LEFT JOIN card_colonels_spirit_beast che 
                ON che.user_spirit_beast_id = ue.spirit_beast_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_beast_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_beast_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", userId);
            selectCommand.Parameters.AddWithValue("@limit", pageSize);
            selectCommand.Parameters.AddWithValue("@offset", offset);
            selectCommand.Parameters.AddWithValue("@status", status);

            await using var reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts spiritBeast = new SpiritBeasts
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
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritBeasts.Add(spiritBeast);
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

        return spiritBeasts;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardGeneralsSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_beast_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_beasts e
            LEFT JOIN user_spirit_beasts ue ON e.id = ue.spirit_beast_id
            LEFT JOIN card_generals_spirit_beast che 
                ON che.user_spirit_beast_id = ue.spirit_beast_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_beast_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_beast_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", userId);
            selectCommand.Parameters.AddWithValue("@limit", pageSize);
            selectCommand.Parameters.AddWithValue("@offset", offset);
            selectCommand.Parameters.AddWithValue("@status", status);

            await using var reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts spiritBeast = new SpiritBeasts
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
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritBeasts.Add(spiritBeast);
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

        return spiritBeasts;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardAdmiralsSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_beast_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_beasts e
            LEFT JOIN user_spirit_beasts ue ON e.id = ue.spirit_beast_id
            LEFT JOIN card_admirals_spirit_beast che 
                ON che.user_spirit_beast_id = ue.spirit_beast_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_beast_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_beast_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", userId);
            selectCommand.Parameters.AddWithValue("@limit", pageSize);
            selectCommand.Parameters.AddWithValue("@offset", offset);
            selectCommand.Parameters.AddWithValue("@status", status);

            await using var reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts spiritBeast = new SpiritBeasts
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
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritBeasts.Add(spiritBeast);
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

        return spiritBeasts;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardMilitariesSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_beast_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_beasts e
            LEFT JOIN user_spirit_beasts ue ON e.id = ue.spirit_beast_id
            LEFT JOIN card_militaries_spirit_beast che 
                ON che.user_spirit_beast_id = ue.spirit_beast_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_beast_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_beast_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", userId);
            selectCommand.Parameters.AddWithValue("@limit", pageSize);
            selectCommand.Parameters.AddWithValue("@offset", offset);
            selectCommand.Parameters.AddWithValue("@status", status);

            await using var reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts spiritBeast = new SpiritBeasts
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
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritBeasts.Add(spiritBeast);
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

        return spiritBeasts;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardMonstersSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_beast_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_beasts e
            LEFT JOIN user_spirit_beasts ue ON e.id = ue.spirit_beast_id
            LEFT JOIN card_monsters_spirit_beast che 
                ON che.user_spirit_beast_id = ue.spirit_beast_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_beast_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_beast_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", userId);
            selectCommand.Parameters.AddWithValue("@limit", pageSize);
            selectCommand.Parameters.AddWithValue("@offset", offset);
            selectCommand.Parameters.AddWithValue("@status", status);

            await using var reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts spiritBeast = new SpiritBeasts
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
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritBeasts.Add(spiritBeast);
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

        return spiritBeasts;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardSpellsSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> spiritBeasts = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT e.name, ue.*, e.image, e.rare, 
                CASE WHEN che.user_spirit_beast_id IS NULL THEN 'NOT EQUIP' ELSE 'EQUIP' END AS equip_status
            from spirit_beasts e
            LEFT JOIN user_spirit_beasts ue ON e.id = ue.spirit_beast_id
            LEFT JOIN card_spells_spirit_beast che 
                ON che.user_spirit_beast_id = ue.spirit_beast_id 
                AND che.user_id = ue.user_id
            WHERE ue.user_id = @user_id
            AND (
                @status = 'ALL'
                OR (@status = 'EQUIP' AND che.user_spirit_beast_id IS NOT NULL)
                OR (@status = 'NOT EQUIP' AND che.user_spirit_beast_id IS NULL)
            )
            LIMIT @limit OFFSET @offset;";

            await using var selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", userId);
            selectCommand.Parameters.AddWithValue("@limit", pageSize);
            selectCommand.Parameters.AddWithValue("@offset", offset);
            selectCommand.Parameters.AddWithValue("@status", status);

            await using var reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts spiritBeast = new SpiritBeasts
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
                    // EquipStatus = reader.GetStringSafe("equip_status")
                };

                spiritBeasts.Add(spiritBeast);
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

        return spiritBeasts;
    }
    public async Task<bool> DeleteUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHero, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM card_heroes_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_hero_id = @user_card_hero_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_hero_id", cardHero.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteSQL = @"
                        DELETE FROM card_heroes_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_hero_id = @user_card_hero_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_hero_id", cardHero.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> DeleteUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptain, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM card_captains_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_captain_id = @user_card_captain_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptain.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteSQL = @"
                        DELETE FROM card_captains_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_captain_id = @user_card_captain_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptain.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> DeleteUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonel, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM card_colonels_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_colonel_id = @user_card_colonel_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonel.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteSQL = @"
                        DELETE FROM card_colonels_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_colonel_id = @user_card_colonel_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonel.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> DeleteUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGeneral, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM card_generals_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_general_id = @user_card_general_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_general_id", cardGeneral.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteSQL = @"
                        DELETE FROM card_generals_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_general_id = @user_card_general_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_general_id", cardGeneral.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> DeleteUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmiral, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM card_admirals_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_admiral_id = @user_card_admiral_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmiral.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteSQL = @"
                        DELETE FROM card_admirals_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_admiral_id = @user_card_admiral_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmiral.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> DeleteUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitary, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM card_militaries_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_military_id = @user_card_military_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteSQL = @"
                        DELETE FROM card_militaries_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_military_id = @user_card_military_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> DeleteUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonster, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM card_monsters_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_monster_id = @user_card_monster_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonster.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteSQL = @"
                        DELETE FROM card_monsters_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_monster_id = @user_card_monster_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonster.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<bool> DeleteUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpells, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) 
                FROM card_spells_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_spell_id = @user_card_spell_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpells.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteSQL = @"
                        DELETE FROM card_spells_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_spell_id = @user_card_spell_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteSQL, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpells.Id);
                            deleteCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

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
    public async Task<SpiritBeasts> GetUserSpiritBeastByIdAsync(string userId, string Id)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT * 
                FROM user_spirit_beasts 
                WHERE spirit_beast_id = @id AND user_id = @user_id;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", Id);
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast = new SpiritBeasts
                            {
                                Id = reader.GetStringSafe("spirit_beast_id"),
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

        return spiritBeast;
    }
    public async Task<SpiritBeasts> SumPowerUserSpiritBeastsAsync()
    {
        SpiritBeasts sumSpiritBeasts = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
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
                FROM user_spirit_beasts
                WHERE user_id = @user_id;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumSpiritBeasts.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            sumSpiritBeasts.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            sumSpiritBeasts.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            sumSpiritBeasts.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            sumSpiritBeasts.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            sumSpiritBeasts.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            sumSpiritBeasts.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            sumSpiritBeasts.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            sumSpiritBeasts.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            sumSpiritBeasts.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            sumSpiritBeasts.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            sumSpiritBeasts.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            sumSpiritBeasts.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            sumSpiritBeasts.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            sumSpiritBeasts.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            sumSpiritBeasts.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            sumSpiritBeasts.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            sumSpiritBeasts.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            sumSpiritBeasts.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            sumSpiritBeasts.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            sumSpiritBeasts.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            sumSpiritBeasts.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            sumSpiritBeasts.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            sumSpiritBeasts.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            sumSpiritBeasts.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            sumSpiritBeasts.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            sumSpiritBeasts.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            sumSpiritBeasts.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            sumSpiritBeasts.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            sumSpiritBeasts.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            sumSpiritBeasts.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            sumSpiritBeasts.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            sumSpiritBeasts.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            sumSpiritBeasts.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            sumSpiritBeasts.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            sumSpiritBeasts.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            sumSpiritBeasts.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            sumSpiritBeasts.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            sumSpiritBeasts.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            sumSpiritBeasts.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            sumSpiritBeasts.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            sumSpiritBeasts.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            sumSpiritBeasts.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            sumSpiritBeasts.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            sumSpiritBeasts.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            sumSpiritBeasts.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            sumSpiritBeasts.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            sumSpiritBeasts.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            sumSpiritBeasts.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            sumSpiritBeasts.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
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

        return sumSpiritBeasts;
    }
}
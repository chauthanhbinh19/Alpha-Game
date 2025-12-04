using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserSpiritBeastsRepository : IUserSpiritBeastsRepository
{
    public async Task<List<SpiritBeasts>> GetUserSpiritBeastsAsync(string user_id, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> SpiritBeastList = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                Select ut.*, t.id, t.name, t.image, t.rare, t.description 
                from spirit_beasts t, user_spirit_beasts ut 
                where t.id = ut.spirit_beast_id 
                and ut.user_id = @userId 
                AND (@rare = 'All' OR t.rare = @rare)
                ORDER BY t.name REGEXP '[0-9]+$', 
                         CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED), 
                         t.name 
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@rare", rare);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            SpiritBeasts title = new SpiritBeasts
                            {
                                Id = reader.GetString("spirit_beast_id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetInt32("quality"),
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
                                Mana = reader.GetFloat("mana"),
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

                            SpiritBeastList.Add(title);
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

        return SpiritBeastList;
    }
    public async Task<List<SpiritBeasts>> GetAllUserSpiritBeastsAsync(string user_id, int pageSize, int offset)
    {
        List<SpiritBeasts> SpiritBeastList = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                Select ut.*, t.id, t.name, t.image, t.rare, t.description 
                from spirit_beasts t, user_spirit_beasts ut
                where t.id = ut.spirit_beast_id 
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
                            SpiritBeasts title = new SpiritBeasts
                            {
                                Id = reader.GetString("spirit_beast_id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetInt32("quality"),
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
                                Mana = reader.GetFloat("mana"),
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

                            SpiritBeastList.Add(title);
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

        return SpiritBeastList;
    }
    public async Task<int> GetUserSpiritBeastsCountAsync(string user_id, string rare)
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
                from spirit_beasts t, user_spirit_beasts ut
                where t.id = ut.spirit_beast_id
                and ut.user_id = @userId
                AND (@rare = 'All' or t.rare = @rare)
                ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
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
    public async Task<SpiritBeasts> GetUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHeroes)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetString("spirit_beast_id");
                            spiritBeast.Name = reader.GetString("name");
                            spiritBeast.Image = reader.GetString("image");
                            spiritBeast.Rare = reader.GetString("rare");
                            spiritBeast.Quality = reader.GetInt32("quality");
                            spiritBeast.Star = reader.GetInt32("star");
                            spiritBeast.Level = reader.GetInt32("level");
                            spiritBeast.Experiment = reader.GetDouble("experiment");
                            spiritBeast.Quantity = reader.GetDouble("quantity");
                            spiritBeast.Power = reader.GetDouble("power");
                            spiritBeast.Health = reader.GetDouble("health");
                            spiritBeast.PhysicalAttack = reader.GetDouble("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDouble("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDouble("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDouble("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDouble("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDouble("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDouble("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDouble("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDouble("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDouble("mental_defense");
                            spiritBeast.Speed = reader.GetDouble("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDouble("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDouble("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDouble("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDouble("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDouble("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDouble("shield_strength");
                            spiritBeast.Tenacity = reader.GetDouble("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDouble("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDouble("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDouble("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDouble("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetFloat("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDouble("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
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
    public async Task<SpiritBeasts> GetUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptains)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetString("spirit_beast_id");
                            spiritBeast.Name = reader.GetString("name");
                            spiritBeast.Image = reader.GetString("image");
                            spiritBeast.Rare = reader.GetString("rare");
                            spiritBeast.Quality = reader.GetInt32("quality");
                            spiritBeast.Star = reader.GetInt32("star");
                            spiritBeast.Level = reader.GetInt32("level");
                            spiritBeast.Experiment = reader.GetDouble("experiment");
                            spiritBeast.Quantity = reader.GetDouble("quantity");
                            spiritBeast.Power = reader.GetDouble("power");
                            spiritBeast.Health = reader.GetDouble("health");
                            spiritBeast.PhysicalAttack = reader.GetDouble("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDouble("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDouble("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDouble("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDouble("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDouble("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDouble("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDouble("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDouble("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDouble("mental_defense");
                            spiritBeast.Speed = reader.GetDouble("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDouble("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDouble("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDouble("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDouble("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDouble("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDouble("shield_strength");
                            spiritBeast.Tenacity = reader.GetDouble("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDouble("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDouble("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDouble("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDouble("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetFloat("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDouble("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
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
    public async Task<SpiritBeasts> GetUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonels)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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

                await using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.Id);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    spiritBeast.Id = reader.GetString("spirit_beast_id");
                    spiritBeast.Name = reader.GetString("name");
                    spiritBeast.Image = reader.GetString("image");
                    spiritBeast.Rare = reader.GetString("rare");
                    spiritBeast.Quality = reader.GetInt32("quality");
                    spiritBeast.Star = reader.GetInt32("star");
                    spiritBeast.Level = reader.GetInt32("level");
                    spiritBeast.Experiment = reader.GetDouble("experiment");
                    spiritBeast.Quantity = reader.GetDouble("quantity");
                    spiritBeast.Power = reader.GetDouble("power");
                    spiritBeast.Health = reader.GetDouble("health");
                    spiritBeast.PhysicalAttack = reader.GetDouble("physical_attack");
                    spiritBeast.PhysicalDefense = reader.GetDouble("physical_defense");
                    spiritBeast.MagicalAttack = reader.GetDouble("magical_attack");
                    spiritBeast.MagicalDefense = reader.GetDouble("magical_defense");
                    spiritBeast.ChemicalAttack = reader.GetDouble("chemical_attack");
                    spiritBeast.ChemicalDefense = reader.GetDouble("chemical_defense");
                    spiritBeast.AtomicAttack = reader.GetDouble("atomic_attack");
                    spiritBeast.AtomicDefense = reader.GetDouble("atomic_defense");
                    spiritBeast.MentalAttack = reader.GetDouble("mental_attack");
                    spiritBeast.MentalDefense = reader.GetDouble("mental_defense");
                    spiritBeast.Speed = reader.GetDouble("speed");
                    spiritBeast.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                    spiritBeast.CriticalRate = reader.GetDouble("critical_rate");
                    spiritBeast.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                    spiritBeast.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                    spiritBeast.PenetrationRate = reader.GetDouble("penetration_rate");
                    spiritBeast.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                    spiritBeast.EvasionRate = reader.GetDouble("evasion_rate");
                    spiritBeast.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                    spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                    spiritBeast.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                    spiritBeast.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                    spiritBeast.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                    spiritBeast.AccuracyRate = reader.GetDouble("accuracy_rate");
                    spiritBeast.LifestealRate = reader.GetDouble("lifesteal_rate");
                    spiritBeast.ShieldStrength = reader.GetDouble("shield_strength");
                    spiritBeast.Tenacity = reader.GetDouble("tenacity");
                    spiritBeast.ResistanceRate = reader.GetDouble("resistance_rate");
                    spiritBeast.ComboRate = reader.GetDouble("combo_rate");
                    spiritBeast.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                    spiritBeast.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                    spiritBeast.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                    spiritBeast.StunRate = reader.GetDouble("stun_rate");
                    spiritBeast.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                    spiritBeast.ReflectionRate = reader.GetDouble("reflection_rate");
                    spiritBeast.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                    spiritBeast.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                    spiritBeast.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                    spiritBeast.Mana = reader.GetFloat("mana");
                    spiritBeast.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                    spiritBeast.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                    spiritBeast.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                    spiritBeast.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                    spiritBeast.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                    spiritBeast.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                    spiritBeast.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                    spiritBeast.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                    spiritBeast.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                    spiritBeast.PercentAllHealth = reader.GetDouble("percent_all_health");
                    spiritBeast.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                    spiritBeast.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                    spiritBeast.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                    spiritBeast.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                    spiritBeast.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                    spiritBeast.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                    spiritBeast.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                    spiritBeast.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                    spiritBeast.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                    spiritBeast.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
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
    public async Task<SpiritBeasts> GetUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGenerals)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_general_id", cardGenerals.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetString("spirit_beast_id");
                            spiritBeast.Name = reader.GetString("name");
                            spiritBeast.Image = reader.GetString("image");
                            spiritBeast.Rare = reader.GetString("rare");
                            spiritBeast.Quality = reader.GetInt32("quality");
                            spiritBeast.Star = reader.GetInt32("star");
                            spiritBeast.Level = reader.GetInt32("level");
                            spiritBeast.Experiment = reader.GetDouble("experiment");
                            spiritBeast.Quantity = reader.GetDouble("quantity");
                            spiritBeast.Power = reader.GetDouble("power");
                            spiritBeast.Health = reader.GetDouble("health");
                            spiritBeast.PhysicalAttack = reader.GetDouble("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDouble("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDouble("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDouble("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDouble("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDouble("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDouble("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDouble("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDouble("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDouble("mental_defense");
                            spiritBeast.Speed = reader.GetDouble("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDouble("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDouble("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDouble("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDouble("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDouble("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDouble("shield_strength");
                            spiritBeast.Tenacity = reader.GetDouble("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDouble("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDouble("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDouble("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDouble("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetFloat("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDouble("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
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
    public async Task<SpiritBeasts> GetUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmirals)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetString("spirit_beast_id");
                            spiritBeast.Name = reader.GetString("name");
                            spiritBeast.Image = reader.GetString("image");
                            spiritBeast.Rare = reader.GetString("rare");
                            spiritBeast.Quality = reader.GetInt32("quality");
                            spiritBeast.Star = reader.GetInt32("star");
                            spiritBeast.Level = reader.GetInt32("level");
                            spiritBeast.Experiment = reader.GetDouble("experiment");
                            spiritBeast.Quantity = reader.GetDouble("quantity");
                            spiritBeast.Power = reader.GetDouble("power");
                            spiritBeast.Health = reader.GetDouble("health");
                            spiritBeast.PhysicalAttack = reader.GetDouble("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDouble("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDouble("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDouble("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDouble("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDouble("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDouble("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDouble("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDouble("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDouble("mental_defense");
                            spiritBeast.Speed = reader.GetDouble("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDouble("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDouble("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDouble("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDouble("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDouble("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDouble("shield_strength");
                            spiritBeast.Tenacity = reader.GetDouble("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDouble("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDouble("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDouble("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDouble("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetFloat("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDouble("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
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

                string query = @"
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

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetString("spirit_beast_id");
                            spiritBeast.Name = reader.GetString("name");
                            spiritBeast.Image = reader.GetString("image");
                            spiritBeast.Rare = reader.GetString("rare");
                            spiritBeast.Quality = reader.GetInt32("quality");
                            spiritBeast.Star = reader.GetInt32("star");
                            spiritBeast.Level = reader.GetInt32("level");
                            spiritBeast.Experiment = reader.GetDouble("experiment");
                            spiritBeast.Quantity = reader.GetDouble("quantity");
                            spiritBeast.Power = reader.GetDouble("power");
                            spiritBeast.Health = reader.GetDouble("health");
                            spiritBeast.PhysicalAttack = reader.GetDouble("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDouble("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDouble("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDouble("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDouble("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDouble("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDouble("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDouble("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDouble("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDouble("mental_defense");
                            spiritBeast.Speed = reader.GetDouble("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDouble("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDouble("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDouble("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDouble("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDouble("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDouble("shield_strength");
                            spiritBeast.Tenacity = reader.GetDouble("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDouble("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDouble("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDouble("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDouble("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetFloat("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDouble("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
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
    public async Task<SpiritBeasts> GetUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonsters)
    {
        SpiritBeasts spiritBeast = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetString("spirit_beast_id");
                            spiritBeast.Name = reader.GetString("name");
                            spiritBeast.Image = reader.GetString("image");
                            spiritBeast.Rare = reader.GetString("rare");
                            spiritBeast.Quality = reader.GetInt32("quality");
                            spiritBeast.Star = reader.GetInt32("star");
                            spiritBeast.Level = reader.GetInt32("level");
                            spiritBeast.Experiment = reader.GetDouble("experiment");
                            spiritBeast.Quantity = reader.GetDouble("quantity");
                            spiritBeast.Power = reader.GetDouble("power");
                            spiritBeast.Health = reader.GetDouble("health");
                            spiritBeast.PhysicalAttack = reader.GetDouble("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDouble("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDouble("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDouble("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDouble("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDouble("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDouble("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDouble("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDouble("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDouble("mental_defense");
                            spiritBeast.Speed = reader.GetDouble("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDouble("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDouble("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDouble("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDouble("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDouble("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDouble("shield_strength");
                            spiritBeast.Tenacity = reader.GetDouble("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDouble("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDouble("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDouble("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDouble("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetFloat("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDouble("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
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

                string query = @"
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

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@user_card_spell_id", cardSpell.Id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            spiritBeast.Id = reader.GetString("spirit_beast_id");
                            spiritBeast.Name = reader.GetString("name");
                            spiritBeast.Image = reader.GetString("image");
                            spiritBeast.Rare = reader.GetString("rare");
                            spiritBeast.Quality = reader.GetInt32("quality");
                            spiritBeast.Star = reader.GetInt32("star");
                            spiritBeast.Level = reader.GetInt32("level");
                            spiritBeast.Experiment = reader.GetDouble("experiment");
                            spiritBeast.Quantity = reader.GetDouble("quantity");
                            spiritBeast.Power = reader.GetDouble("power");
                            spiritBeast.Health = reader.GetDouble("health");
                            spiritBeast.PhysicalAttack = reader.GetDouble("physical_attack");
                            spiritBeast.PhysicalDefense = reader.GetDouble("physical_defense");
                            spiritBeast.MagicalAttack = reader.GetDouble("magical_attack");
                            spiritBeast.MagicalDefense = reader.GetDouble("magical_defense");
                            spiritBeast.ChemicalAttack = reader.GetDouble("chemical_attack");
                            spiritBeast.ChemicalDefense = reader.GetDouble("chemical_defense");
                            spiritBeast.AtomicAttack = reader.GetDouble("atomic_attack");
                            spiritBeast.AtomicDefense = reader.GetDouble("atomic_defense");
                            spiritBeast.MentalAttack = reader.GetDouble("mental_attack");
                            spiritBeast.MentalDefense = reader.GetDouble("mental_defense");
                            spiritBeast.Speed = reader.GetDouble("speed");
                            spiritBeast.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            spiritBeast.CriticalRate = reader.GetDouble("critical_rate");
                            spiritBeast.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            spiritBeast.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            spiritBeast.PenetrationRate = reader.GetDouble("penetration_rate");
                            spiritBeast.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            spiritBeast.EvasionRate = reader.GetDouble("evasion_rate");
                            spiritBeast.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            spiritBeast.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            spiritBeast.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            spiritBeast.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            spiritBeast.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            spiritBeast.AccuracyRate = reader.GetDouble("accuracy_rate");
                            spiritBeast.LifestealRate = reader.GetDouble("lifesteal_rate");
                            spiritBeast.ShieldStrength = reader.GetDouble("shield_strength");
                            spiritBeast.Tenacity = reader.GetDouble("tenacity");
                            spiritBeast.ResistanceRate = reader.GetDouble("resistance_rate");
                            spiritBeast.ComboRate = reader.GetDouble("combo_rate");
                            spiritBeast.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            spiritBeast.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            spiritBeast.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            spiritBeast.StunRate = reader.GetDouble("stun_rate");
                            spiritBeast.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            spiritBeast.ReflectionRate = reader.GetDouble("reflection_rate");
                            spiritBeast.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            spiritBeast.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            spiritBeast.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            spiritBeast.Mana = reader.GetFloat("mana");
                            spiritBeast.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            spiritBeast.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            spiritBeast.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            spiritBeast.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            spiritBeast.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            spiritBeast.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            spiritBeast.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            spiritBeast.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            spiritBeast.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            spiritBeast.PercentAllHealth = reader.GetDouble("percent_all_health");
                            spiritBeast.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            spiritBeast.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            spiritBeast.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            spiritBeast.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            spiritBeast.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            spiritBeast.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            spiritBeast.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            spiritBeast.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            spiritBeast.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            spiritBeast.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
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
                string checkQuery = @"
                SELECT COUNT(*) FROM user_spirit_beasts 
                WHERE user_id = @user_id AND spirit_beast_id = @spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@spirit_beast_id", SpiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string query = @"
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

                        await using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            command.Parameters.AddWithValue("@spirit_beast_id", SpiritBeast.Id);
                            command.Parameters.AddWithValue("@rare", SpiritBeast.Rare);
                            command.Parameters.AddWithValue("@level", 0);
                            command.Parameters.AddWithValue("@experiment", 0);
                            command.Parameters.AddWithValue("@star", 0);
                            command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(SpiritBeast.Rare));
                            command.Parameters.AddWithValue("@block", false);
                            command.Parameters.AddWithValue("@quantity", SpiritBeast.Quantity);
                            command.Parameters.AddWithValue("@power", SpiritBeast.Power);
                            command.Parameters.AddWithValue("@health", SpiritBeast.Health);
                            command.Parameters.AddWithValue("@physical_attack", SpiritBeast.PhysicalAttack);
                            command.Parameters.AddWithValue("@physical_defense", SpiritBeast.PhysicalDefense);
                            command.Parameters.AddWithValue("@magical_attack", SpiritBeast.MagicalAttack);
                            command.Parameters.AddWithValue("@magical_defense", SpiritBeast.MagicalDefense);
                            command.Parameters.AddWithValue("@chemical_attack", SpiritBeast.ChemicalAttack);
                            command.Parameters.AddWithValue("@chemical_defense", SpiritBeast.ChemicalDefense);
                            command.Parameters.AddWithValue("@atomic_attack", SpiritBeast.AtomicAttack);
                            command.Parameters.AddWithValue("@atomic_defense", SpiritBeast.AtomicDefense);
                            command.Parameters.AddWithValue("@mental_attack", SpiritBeast.MentalAttack);
                            command.Parameters.AddWithValue("@mental_defense", SpiritBeast.MentalDefense);
                            command.Parameters.AddWithValue("@speed", SpiritBeast.Speed);
                            command.Parameters.AddWithValue("@critical_damage_rate", SpiritBeast.CriticalDamageRate);
                            command.Parameters.AddWithValue("@critical_rate", SpiritBeast.CriticalRate);
                            command.Parameters.AddWithValue("@critical_resistance_rate", SpiritBeast.CriticalResistanceRate);
                            command.Parameters.AddWithValue("@ignore_critical_rate", SpiritBeast.IgnoreCriticalRate);
                            command.Parameters.AddWithValue("@penetration_rate", SpiritBeast.PenetrationRate);
                            command.Parameters.AddWithValue("@penetration_resistance_rate", SpiritBeast.PenetrationResistanceRate);
                            command.Parameters.AddWithValue("@evasion_rate", SpiritBeast.EvasionRate);
                            command.Parameters.AddWithValue("@damage_absorption_rate", SpiritBeast.DamageAbsorptionRate);
                            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", SpiritBeast.IgnoreDamageAbsorptionRate);
                            command.Parameters.AddWithValue("@absorbed_damage_rate", SpiritBeast.AbsorbedDamageRate);
                            command.Parameters.AddWithValue("@vitality_regeneration_rate", SpiritBeast.VitalityRegenerationRate);
                            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", SpiritBeast.VitalityRegenerationResistanceRate);
                            command.Parameters.AddWithValue("@accuracy_rate", SpiritBeast.AccuracyRate);
                            command.Parameters.AddWithValue("@lifesteal_rate", SpiritBeast.LifestealRate);
                            command.Parameters.AddWithValue("@shield_strength", SpiritBeast.ShieldStrength);
                            command.Parameters.AddWithValue("@tenacity", SpiritBeast.Tenacity);
                            command.Parameters.AddWithValue("@resistance_rate", SpiritBeast.ResistanceRate);
                            command.Parameters.AddWithValue("@combo_rate", SpiritBeast.ComboRate);
                            command.Parameters.AddWithValue("@ignore_combo_rate", SpiritBeast.IgnoreComboRate);
                            command.Parameters.AddWithValue("@combo_damage_rate", SpiritBeast.ComboDamageRate);
                            command.Parameters.AddWithValue("@combo_resistance_rate", SpiritBeast.ComboResistanceRate);
                            command.Parameters.AddWithValue("@stun_rate", SpiritBeast.StunRate);
                            command.Parameters.AddWithValue("@ignore_stun_rate", SpiritBeast.IgnoreStunRate);
                            command.Parameters.AddWithValue("@reflection_rate", SpiritBeast.ReflectionRate);
                            command.Parameters.AddWithValue("@ignore_reflection_rate", SpiritBeast.IgnoreReflectionRate);
                            command.Parameters.AddWithValue("@reflection_damage_rate", SpiritBeast.ReflectionDamageRate);
                            command.Parameters.AddWithValue("@reflection_resistance_rate", SpiritBeast.ReflectionResistanceRate);
                            command.Parameters.AddWithValue("@mana", SpiritBeast.Mana);
                            command.Parameters.AddWithValue("@mana_regeneration_rate", SpiritBeast.ManaRegenerationRate);
                            command.Parameters.AddWithValue("@damage_to_different_faction_rate", SpiritBeast.DamageToDifferentFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", SpiritBeast.ResistanceToDifferentFactionRate);
                            command.Parameters.AddWithValue("@damage_to_same_faction_rate", SpiritBeast.DamageToSameFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", SpiritBeast.ResistanceToSameFactionRate);
                            command.Parameters.AddWithValue("@normal_damage_rate", SpiritBeast.NormalDamageRate);
                            command.Parameters.AddWithValue("@normal_resistance_rate", SpiritBeast.NormalResistanceRate);
                            command.Parameters.AddWithValue("@skill_damage_rate", SpiritBeast.SkillDamageRate);
                            command.Parameters.AddWithValue("@skill_resistance_rate", SpiritBeast.SkillResistanceRate);

                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_spirit_beasts
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND spirit_beast_id = @spirit_beast_id;
                    ";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
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
    public async Task<bool> UpdateSpiritBeastLevelAsync(SpiritBeasts SpiritBeast, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@spirit_beast_id", SpiritBeast.Id);
                    command.Parameters.AddWithValue("@level", cardLevel);
                    command.Parameters.AddWithValue("@power", SpiritBeast.Power);
                    command.Parameters.AddWithValue("@health", SpiritBeast.Health);
                    command.Parameters.AddWithValue("@physical_attack", SpiritBeast.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", SpiritBeast.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", SpiritBeast.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", SpiritBeast.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", SpiritBeast.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", SpiritBeast.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", SpiritBeast.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", SpiritBeast.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", SpiritBeast.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", SpiritBeast.MentalDefense);
                    command.Parameters.AddWithValue("@speed", SpiritBeast.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", SpiritBeast.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", SpiritBeast.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", SpiritBeast.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", SpiritBeast.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", SpiritBeast.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", SpiritBeast.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", SpiritBeast.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", SpiritBeast.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", SpiritBeast.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", SpiritBeast.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", SpiritBeast.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", SpiritBeast.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", SpiritBeast.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", SpiritBeast.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", SpiritBeast.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", SpiritBeast.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", SpiritBeast.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", SpiritBeast.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", SpiritBeast.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", SpiritBeast.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", SpiritBeast.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", SpiritBeast.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", SpiritBeast.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", SpiritBeast.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", SpiritBeast.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", SpiritBeast.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", SpiritBeast.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", SpiritBeast.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", SpiritBeast.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", SpiritBeast.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", SpiritBeast.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", SpiritBeast.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", SpiritBeast.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", SpiritBeast.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", SpiritBeast.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", SpiritBeast.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", SpiritBeast.SkillResistanceRate);

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
    public async Task<bool> UpdateSpiritBeastBreakthroughAsync(SpiritBeasts SpiritBeast, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@spirit_beast_id", SpiritBeast.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@power", SpiritBeast.Power);
                    command.Parameters.AddWithValue("@health", SpiritBeast.Health);
                    command.Parameters.AddWithValue("@physical_attack", SpiritBeast.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", SpiritBeast.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", SpiritBeast.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", SpiritBeast.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", SpiritBeast.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", SpiritBeast.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", SpiritBeast.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", SpiritBeast.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", SpiritBeast.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", SpiritBeast.MentalDefense);
                    command.Parameters.AddWithValue("@speed", SpiritBeast.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", SpiritBeast.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", SpiritBeast.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", SpiritBeast.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", SpiritBeast.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", SpiritBeast.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", SpiritBeast.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", SpiritBeast.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", SpiritBeast.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", SpiritBeast.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", SpiritBeast.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", SpiritBeast.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", SpiritBeast.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", SpiritBeast.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", SpiritBeast.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", SpiritBeast.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", SpiritBeast.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", SpiritBeast.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", SpiritBeast.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", SpiritBeast.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", SpiritBeast.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", SpiritBeast.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", SpiritBeast.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", SpiritBeast.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", SpiritBeast.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", SpiritBeast.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", SpiritBeast.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", SpiritBeast.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", SpiritBeast.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", SpiritBeast.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", SpiritBeast.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", SpiritBeast.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", SpiritBeast.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", SpiritBeast.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", SpiritBeast.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", SpiritBeast.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", SpiritBeast.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", SpiritBeast.SkillResistanceRate);

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
    public async Task<bool> InsertOrUpdateUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHeroes, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"SELECT COUNT(*) FROM card_heroes_spirit_beast WHERE user_id = @user_id AND user_card_hero_id = @user_card_hero_id;";

                await using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_heroes_spirit_beast (user_id, user_card_hero_id, user_spirit_beast_id) VALUES (@user_id, @user_card_hero_id, @user_spirit_beast_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"UPDATE card_heroes_spirit_beast SET user_spirit_beast_id = @user_spirit_beast_id WHERE user_id = @user_id AND user_card_hero_id = @user_card_hero_id;";

                        await using var updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.Id);
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
    public async Task<bool> InsertOrUpdateUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptains, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"SELECT COUNT(*) FROM card_captains_spirit_beast 
                              WHERE user_id = @user_id AND user_card_captain_id = @user_card_captain_id;";

                await using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_captains_spirit_beast 
                                      (user_id, user_card_captain_id, user_spirit_beast_id)
                                       VALUES (@user_id, @user_card_captain_id, @user_spirit_beast_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"UPDATE card_captains_spirit_beast
                                       SET user_spirit_beast_id = @user_spirit_beast_id
                                       WHERE user_id = @user_id AND user_card_captain_id = @user_card_captain_id;";

                        await using var updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.Id);
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
    public async Task<bool> InsertOrUpdateUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonels, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"SELECT COUNT(*) FROM card_colonels_spirit_beast 
                              WHERE user_id = @user_id AND user_card_colonel_id = @user_card_colonel_id;";

                await using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_colonels_spirit_beast
                                      (user_id, user_card_colonel_id, user_spirit_beast_id)
                                      VALUES (@user_id, @user_card_colonel_id, @user_spirit_beast_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"UPDATE card_colonels_spirit_beast
                                       SET user_spirit_beast_id = @user_spirit_beast_id
                                       WHERE user_id = @user_id AND user_card_colonel_id = @user_card_colonel_id;";

                        await using var updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.Id);
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
    public async Task<bool> InsertOrUpdateUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGenerals, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"SELECT COUNT(*) FROM card_generals_spirit_beast 
                              WHERE user_id = @user_id AND user_card_general_id = @user_card_general_id;";

                await using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_generals_spirit_beast
                                      (user_id, user_card_general_id, user_spirit_beast_id)
                                      VALUES (@user_id, @user_card_general_id, @user_spirit_beast_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"UPDATE card_generals_spirit_beast
                                       SET user_spirit_beast_id = @user_spirit_beast_id
                                       WHERE user_id = @user_id AND user_card_general_id = @user_card_general_id;";

                        await using var updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId); // sửa lỗi User.CurrentUserId
                        updateCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.Id);
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
    public async Task<bool> InsertOrUpdateUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmirals, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"SELECT COUNT(*) FROM card_admirals_spirit_beast 
                              WHERE user_id = @user_id AND user_card_admiral_id = @user_card_admiral_id;";

                await using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"INSERT INTO card_admirals_spirit_beast
                                      (user_id, user_card_admiral_id, user_spirit_beast_id)
                                      VALUES (@user_id, @user_card_admiral_id, @user_spirit_beast_id);";

                        await using var insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.Id); // sửa lỗi
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"UPDATE card_admirals_spirit_beast
                                       SET user_spirit_beast_id = @user_spirit_beast_id
                                       WHERE user_id = @user_id AND user_card_admiral_id = @user_card_admiral_id;";

                        await using var updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId); // sửa lỗi User.CurrentUserId
                        updateCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.Id);
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
                string checkQuery = @"
                SELECT COUNT(*) FROM card_military_spirit_beast 
                WHERE user_id = @user_id AND user_card_military_id = @user_card_military_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO card_military_spirit_beast (
                            user_id, user_card_military_id, user_spirit_beast_id
                        ) VALUES (
                            @user_id, @user_card_military_id, @user_spirit_beast_id
                        );";

                        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitary.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string updateQuery = @"
                        UPDATE card_military_spirit_beast
                        SET user_spirit_beast_id = @user_spirit_beast_id
                        WHERE user_id = @user_id AND user_card_military_id = @user_card_military_id;";

                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
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
    public async Task<bool> InsertOrUpdateUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonsters, SpiritBeasts spiritBeast)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM card_monsters_spirit_beast 
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
                        INSERT INTO card_monsters_spirit_beast (
                            user_id, user_card_monster_id, user_spirit_beast_id
                        ) VALUES (
                            @user_id, @user_card_monster_id, @user_spirit_beast_id
                        );";

                        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        // UPDATE
                        string updateQuery = @"
                        UPDATE card_monsters_spirit_beast
                        SET user_spirit_beast_id = @user_spirit_beast_id
                        WHERE user_id = @user_id AND user_card_monster_id = @user_card_monster_id;";

                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);
                        updateCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.Id);
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
                string checkQuery = @"
                SELECT COUNT(*) FROM card_spell_spirit_beast 
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
                        INSERT INTO card_spell_spirit_beast (
                            user_id, user_card_spell_id, user_spirit_beast_id
                        ) VALUES (
                            @user_id, @user_card_spell_id, @user_spirit_beast_id
                        );";

                        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@user_id", userId);
                        insertCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpell.Id);
                        insertCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        // UPDATE
                        string updateQuery = @"
                        UPDATE card_spell_spirit_beast
                        SET user_spirit_beast_id = @user_spirit_beast_id
                        WHERE user_id = @user_id AND user_card_spell_id = @user_card_spell_id;";

                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
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
    public async Task<List<SpiritBeasts>> GetAllUserCardHeroesSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> SpiritBeastList = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
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

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts sb = new SpiritBeasts
                {
                    Id = reader.GetString("spirit_beast_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetInt32("quality"),
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
                    Mana = reader.GetFloat("mana"),
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

                SpiritBeastList.Add(sb);
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

        return SpiritBeastList;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardCaptainsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> SpiritBeastList = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
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

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts sb = new SpiritBeasts
                {
                    Id = reader.GetString("spirit_beast_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetInt32("quality"),
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
                    Mana = reader.GetFloat("mana"),
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

                SpiritBeastList.Add(sb);
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

        return SpiritBeastList;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardColonelsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> SpiritBeastList = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
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

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts sb = new SpiritBeasts
                {
                    Id = reader.GetString("spirit_beast_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetInt32("quality"),
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
                    Mana = reader.GetFloat("mana"),
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

                SpiritBeastList.Add(sb);
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

        return SpiritBeastList;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardGeneralsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> SpiritBeastList = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
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

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts sb = new SpiritBeasts
                {
                    Id = reader.GetString("spirit_beast_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetInt32("quality"),
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
                    Mana = reader.GetFloat("mana"),
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

                SpiritBeastList.Add(sb);
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

        return SpiritBeastList;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardAdmiralsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> SpiritBeastList = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
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

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts sb = new SpiritBeasts
                {
                    Id = reader.GetString("spirit_beast_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetInt32("quality"),
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
                    Mana = reader.GetFloat("mana"),
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

                SpiritBeastList.Add(sb);
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

        return SpiritBeastList;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardMilitariesSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> SpiritBeastList = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
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

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts sb = new SpiritBeasts
                {
                    Id = reader.GetString("spirit_beast_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetInt32("quality"),
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
                    Mana = reader.GetFloat("mana"),
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

                SpiritBeastList.Add(sb);
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

        return SpiritBeastList;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardMonstersSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> SpiritBeastList = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
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

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts sb = new SpiritBeasts
                {
                    Id = reader.GetString("spirit_beast_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetInt32("quality"),
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
                    Mana = reader.GetFloat("mana"),
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

                SpiritBeastList.Add(sb);
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

        return SpiritBeastList;
    }
    public async Task<List<SpiritBeasts>> GetAllUserCardSpellsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        List<SpiritBeasts> SpiritBeastList = new List<SpiritBeasts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
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

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@limit", pageSize);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@status", status);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                SpiritBeasts sb = new SpiritBeasts
                {
                    Id = reader.GetString("spirit_beast_id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Rare = reader.GetString("rare"),
                    Quality = reader.GetInt32("quality"),
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
                    Mana = reader.GetFloat("mana"),
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

                SpiritBeastList.Add(sb);
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

        return SpiritBeastList;
    }
    public async Task<bool> DeleteUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHeroes, SpiritBeasts spiritBeast)
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
                FROM card_heroes_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_hero_id = @user_card_hero_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_heroes_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_hero_id = @user_card_hero_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_hero_id", cardHeroes.Id);
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
    public async Task<bool> DeleteUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptains, SpiritBeasts spiritBeast)
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
                FROM card_captains_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_captain_id = @user_card_captain_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_captains_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_captain_id = @user_card_captain_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.Id);
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
    public async Task<bool> DeleteUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonels, SpiritBeasts spiritBeast)
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
                FROM card_colonels_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_colonel_id = @user_card_colonel_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_colonels_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_colonel_id = @user_card_colonel_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.Id);
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
    public async Task<bool> DeleteUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGenerals, SpiritBeasts spiritBeast)
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
                FROM card_generals_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_general_id = @user_card_general_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_generals_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_general_id = @user_card_general_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_general_id", cardGenerals.Id);
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
    public async Task<bool> DeleteUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmirals, SpiritBeasts spiritBeast)
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
                FROM card_admirals_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_admiral_id = @user_card_admiral_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_admirals_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_admiral_id = @user_card_admiral_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.Id);
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
    public async Task<bool> DeleteUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitaries, SpiritBeasts spiritBeast)
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
                FROM card_militaries_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_military_id = @user_card_military_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitaries.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_militaries_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_military_id = @user_card_military_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_military_id", cardMilitaries.Id);
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
    public async Task<bool> DeleteUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonsters, SpiritBeasts spiritBeast)
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
                FROM card_monsters_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_monster_id = @user_card_monster_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_monsters_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_monster_id = @user_card_monster_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@user_id", userId);
                            deleteCommand.Parameters.AddWithValue("@user_card_monster_id", cardMonsters.Id);
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
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM card_spells_spirit_beast 
                WHERE user_id = @user_id 
                  AND user_card_spell_id = @user_card_spell_id 
                  AND user_spirit_beast_id = @user_spirit_beast_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@user_card_spell_id", cardSpells.Id);
                    checkCommand.Parameters.AddWithValue("@user_spirit_beast_id", spiritBeast.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count != 0)
                    {
                        string deleteQuery = @"
                        DELETE FROM card_spells_spirit_beast
                        WHERE user_id = @user_id 
                          AND user_card_spell_id = @user_card_spell_id 
                          AND user_spirit_beast_id = @user_spirit_beast_id;
                    ";

                        await using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
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
    public async Task<SpiritBeasts> GetUserSpiritBeastByIdAsync(string user_id, string Id)
    {
        SpiritBeasts card = new SpiritBeasts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT * 
                FROM user_spirit_beasts 
                WHERE spirit_beast_id = @id AND user_id = @user_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            card = new SpiritBeasts
                            {
                                Id = reader.GetString("spirit_beast_id"),
                                Level = reader.GetInt32("level"),
                                Quality = reader.GetInt32("quality"),
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
                                Mana = reader.GetFloat("mana"),
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
    public async Task<SpiritBeasts> SumPowerUserSpiritBeastsAsync()
    {
        SpiritBeasts sumSpiritBeast = new SpiritBeasts();
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
                FROM user_spirit_beasts
                WHERE user_id = @user_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumSpiritBeast.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                            sumSpiritBeast.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                            sumSpiritBeast.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            sumSpiritBeast.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                            sumSpiritBeast.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                            sumSpiritBeast.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                            sumSpiritBeast.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                            sumSpiritBeast.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                            sumSpiritBeast.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                            sumSpiritBeast.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                            sumSpiritBeast.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                            sumSpiritBeast.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                            sumSpiritBeast.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                            sumSpiritBeast.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                            sumSpiritBeast.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                            sumSpiritBeast.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                            sumSpiritBeast.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                            sumSpiritBeast.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                            sumSpiritBeast.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                            sumSpiritBeast.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                            sumSpiritBeast.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                            sumSpiritBeast.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                            sumSpiritBeast.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                            sumSpiritBeast.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                            sumSpiritBeast.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                            sumSpiritBeast.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                            sumSpiritBeast.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                            sumSpiritBeast.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                            sumSpiritBeast.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                            sumSpiritBeast.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                            sumSpiritBeast.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                            sumSpiritBeast.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                            sumSpiritBeast.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                            sumSpiritBeast.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                            sumSpiritBeast.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                            sumSpiritBeast.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                            sumSpiritBeast.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                            sumSpiritBeast.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                            sumSpiritBeast.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                            sumSpiritBeast.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                            sumSpiritBeast.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                            sumSpiritBeast.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                            sumSpiritBeast.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                            sumSpiritBeast.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                            sumSpiritBeast.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                            sumSpiritBeast.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                            sumSpiritBeast.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                            sumSpiritBeast.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                            sumSpiritBeast.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                            sumSpiritBeast.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
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

        return sumSpiritBeast;
    }
}
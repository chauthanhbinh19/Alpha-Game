using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;
public class UserBooksRepository : IUserBooksRepository
{
    public async Task<List<Books>> GetUserBooksAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Books> books = new List<Books>();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string selectSQL = @"SELECT ub.*, b.name, b.image, b.type, b.description
                FROM user_books ub
                LEFT JOIN books b ON ub.book_id = b.id 
                WHERE ub.user_id = @userId 
                ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND b.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND b.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND b.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += " LIMIT @limit OFFSET @offset";
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    if (!string.IsNullOrEmpty(type) && type != "All")
                    {
                        selectCommand.Parameters.AddWithValue("@type", type);
                    }

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
                            Books book = new Books
                            {
                                Id = reader.GetStringSafe("book_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rarity = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Type = reader.GetStringSafe("type"),
                                Star = reader.GetIntSafe("star"),
                                Level = reader.GetIntSafe("level"),
                                Experience = reader.GetDoubleSafe("experience"),
                                Quantity = reader.GetIntSafe("quantity"),
                                Block = reader.GetBoolean("block"),
                                TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetStringSafe("team_id"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetStringSafe("position"),

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
                                Description = reader.GetStringSafe("description"),

                                BaseStats = new BaseStats
                                {
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
                                }
                            };

                            books.Add(book);
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
        return books;
    }
    public async Task<List<Books>> GetUserBooksTeamAsync(string userId, string teamId)
    {
        List<Books> books = new List<Books>();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string selectSQL = @"SELECT ub.*, b.name, b.image, b.type, b.description
                FROM user_books ub
                LEFT JOIN books b ON ub.book_id = b.id 
                WHERE ub.user_id = @userId AND ub.team_id=@team_id
                ";
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    selectCommand.Parameters.AddWithValue("@team_id", teamId);

                    await using (MySqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            Books book = new Books
                            {
                                Id = reader.GetStringSafe("book_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rarity = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Star = reader.GetIntSafe("star"),
                                Level = reader.GetIntSafe("level"),
                                Experience = reader.GetDoubleSafe("experience"),
                                Quantity = reader.GetIntSafe("quantity"),
                                Block = reader.GetBoolean("block"),
                                TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetStringSafe("team_id"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetStringSafe("position"),

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
                                Description = reader.GetStringSafe("description"),

                                BaseStats = new BaseStats
                                {
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
                                }
                            };

                            books.Add(book);
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
        return books;
    }
    public async Task<Dictionary<string, int>> GetUniqueUserBooksTypesTeamAsync(string userId, string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT distinct c.type, count(c.type) as number
                FROM user_card_books uc
                LEFT JOIN books c ON uc.book_id = c.id 
                WHERE uc.user_id =@userId and uc.team_id=@team_id
                group by c.type, c.type";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    selectCommand.Parameters.AddWithValue("@team_id", teamId);

                    await using (MySqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            string type = reader["type"].ToString();
                            int number = Convert.ToInt32(reader["number"]);

                            result[type] = number;
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
        return result;
    }
    public async Task<int> GetUserBooksCountAsync(string userId, string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"Select count(*) 
                             from books b, user_books ub 
                             where b.id = ub.book_id 
                               and ub.user_id = @userId ";
                
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND b.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND b.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND b.name LIKE CONCAT('%', @search, '%')";
                }

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    if (!string.IsNullOrEmpty(type) && type != "All")
                    {
                        selectCommand.Parameters.AddWithValue("@type", type);
                    }

                    if (!string.IsNullOrEmpty(rare) && rare != "All")
                    {
                        selectCommand.Parameters.AddWithValue("@rare", rare);
                    }

                    if (!string.IsNullOrEmpty(search))
                    {
                        selectCommand.Parameters.AddWithValue("@search", search);
                    }

                    object result = await selectCommand.ExecuteScalarAsync();
                    count = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);

                }

                return count;
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
    public async Task<bool> InsertUserBookAsync(string userId, Books book)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // 1. Check existing record
                string checkSQL = @"
                SELECT COUNT(*) FROM user_books 
                WHERE user_id = @user_id AND book_id = @book_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@book_id", book.Id);

                    object result = await checkCommand.ExecuteScalarAsync();
                    int count = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);

                    // 2. INSERT NEW
                    if (count == 0)
                    {
                        string insertSQL = @"
                        INSERT INTO user_books (
                            user_id, book_id, rare, level, experience, star, quality, block, quantity,
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
                            @user_id, @book_id, @rare, @level, @experience, @star, @quality, @block, @quantity,
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
                        );";

                        await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@user_id", userId);
                            insertCommand.Parameters.AddWithValue("@book_id", book.Id);
                            insertCommand.Parameters.AddWithValue("@rare", book.Rarity);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experience", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(book.Rarity));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", book.Quantity);

                            // → Add remaining stats
                            insertCommand.Parameters.AddWithValue("@power", book.Power);
                            insertCommand.Parameters.AddWithValue("@health", book.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", book.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", book.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", book.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", book.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", book.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", book.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", book.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", book.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", book.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", book.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", book.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", book.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", book.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", book.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", book.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", book.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", book.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", book.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", book.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", book.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", book.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", book.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", book.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", book.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", book.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", book.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", book.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", book.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", book.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", book.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", book.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", book.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", book.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", book.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", book.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", book.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", book.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", book.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", book.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", book.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", book.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", book.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", book.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", book.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", book.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", book.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", book.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", book.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // 3. UPDATE EXISTING
                        string updateSQL = @"
                            UPDATE user_books
                            SET quantity = @quantity
                            WHERE user_id = @user_id AND book_id = @book_id;";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@book_id", book.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", book.Quantity);

                            await updateCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error InsertUserBooksAsync: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> InsertOrUpdateUserBooksBatchAsync(string userId, List<Books> books)
    {
        if (books == null || books.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 500; // vì nhiều column → giảm size

            for (int i = 0; i < books.Count; i += batchSize)
            {
                var batch = books.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
                INSERT INTO user_books (
                    user_id, book_id, rare, level, experience, star, quality, block, quantity,
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
                    (@user_id, @book_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
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
                        new MySqlParameter($"@book_id_{j}", c.Id),
                        new MySqlParameter($"@rare_{j}", c.Rarity),
                        new MySqlParameter($"@quality_{j}", QualityEvaluatorHelper.CheckQuality(c.Rarity)),
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
                    quantity = COALESCE(user_books.quantity, 0) + VALUES(quantity);
                ");

                await using var command = new MySqlCommand(stringBuilder.ToString(), connection, (MySqlTransaction)transaction);

                command.Parameters.AddWithValue("@user_id", userId);
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
    public async Task<bool> UpdateUserBookLevelAsync(string userId, Books book)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_books
                SET 
                    level = @level, experience = @experience
                WHERE user_id = @user_id AND book_id = @book_id;";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@book_id", book.Id);
                    updateCommand.Parameters.AddWithValue("@level", book.Level);
                    updateCommand.Parameters.AddWithValue("@experience", book.Experience);

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
    public async Task<bool> UpdateUserBookStarAsync(string userId, Books book)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_books
                SET 
                    star = @star, quantity = @quantity
                WHERE user_id = @user_id AND book_id = @book_id;
            ";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@book_id", book.Id);
                    updateCommand.Parameters.AddWithValue("@star", book.Star);
                    updateCommand.Parameters.AddWithValue("@quantity", book.Quantity);

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
    public async Task<bool> UpdateUserBookBreakthroughAsync(string userId, Books book, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_books
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
                    combo_rate = @comboRate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
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
                WHERE user_id = @user_id AND book_id = @book_id;";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@book_id", book.Id);
                    updateCommand.Parameters.AddWithValue("@star", star);
                    updateCommand.Parameters.AddWithValue("@quantity", quantity);

                    updateCommand.Parameters.AddWithValue("@power", book.Power);
                    updateCommand.Parameters.AddWithValue("@health", book.Health);
                    updateCommand.Parameters.AddWithValue("@physical_attack", book.PhysicalAttack);
                    updateCommand.Parameters.AddWithValue("@physical_defense", book.PhysicalDefense);
                    updateCommand.Parameters.AddWithValue("@magical_attack", book.MagicalAttack);
                    updateCommand.Parameters.AddWithValue("@magical_defense", book.MagicalDefense);
                    updateCommand.Parameters.AddWithValue("@chemical_attack", book.ChemicalAttack);
                    updateCommand.Parameters.AddWithValue("@chemical_defense", book.ChemicalDefense);
                    updateCommand.Parameters.AddWithValue("@atomic_attack", book.AtomicAttack);
                    updateCommand.Parameters.AddWithValue("@atomic_defense", book.AtomicDefense);
                    updateCommand.Parameters.AddWithValue("@mental_attack", book.MentalAttack);
                    updateCommand.Parameters.AddWithValue("@mental_defense", book.MentalDefense);
                    updateCommand.Parameters.AddWithValue("@speed", book.Speed);
                    updateCommand.Parameters.AddWithValue("@critical_damage_rate", book.CriticalDamageRate);
                    updateCommand.Parameters.AddWithValue("@critical_rate", book.CriticalRate);
                    updateCommand.Parameters.AddWithValue("@critical_resistance_rate", book.CriticalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@ignore_critical_rate", book.IgnoreCriticalRate);
                    updateCommand.Parameters.AddWithValue("@penetration_rate", book.PenetrationRate);
                    updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", book.PenetrationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@evasion_rate", book.EvasionRate);
                    updateCommand.Parameters.AddWithValue("@damage_absorption_rate", book.DamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", book.IgnoreDamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", book.AbsorbedDamageRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", book.VitalityRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", book.VitalityRegenerationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@accuracy_rate", book.AccuracyRate);
                    updateCommand.Parameters.AddWithValue("@lifesteal_rate", book.LifestealRate);
                    updateCommand.Parameters.AddWithValue("@shield_strength", book.ShieldStrength);
                    updateCommand.Parameters.AddWithValue("@tenacity", book.Tenacity);
                    updateCommand.Parameters.AddWithValue("@resistance_rate", book.ResistanceRate);
                    updateCommand.Parameters.AddWithValue("@combo_rate", book.ComboRate);
                    updateCommand.Parameters.AddWithValue("@ignore_combo_rate", book.IgnoreComboRate);
                    updateCommand.Parameters.AddWithValue("@combo_damage_rate", book.ComboDamageRate);
                    updateCommand.Parameters.AddWithValue("@combo_resistance_rate", book.ComboResistanceRate);
                    updateCommand.Parameters.AddWithValue("@stun_rate", book.StunRate);
                    updateCommand.Parameters.AddWithValue("@ignore_stun_rate", book.IgnoreStunRate);
                    updateCommand.Parameters.AddWithValue("@reflection_rate", book.ReflectionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", book.IgnoreReflectionRate);
                    updateCommand.Parameters.AddWithValue("@reflection_damage_rate", book.ReflectionDamageRate);
                    updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", book.ReflectionResistanceRate);
                    updateCommand.Parameters.AddWithValue("@mana", book.Mana);
                    updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", book.ManaRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", book.DamageToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", book.ResistanceToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", book.DamageToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", book.ResistanceToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@normal_damage_rate", book.NormalDamageRate);
                    updateCommand.Parameters.AddWithValue("@normal_resistance_rate", book.NormalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@skill_damage_rate", book.SkillDamageRate);
                    updateCommand.Parameters.AddWithValue("@skill_resistance_rate", book.SkillResistanceRate);

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
    public async Task<bool> UpdateTeamUserBookAsync(string userId, string teamId, string position, string bookId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_books 
                SET team_id = @team_id, position = @position 
                WHERE user_id = @user_id AND book_id = @book_id;
            ";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@team_id", teamId);
                    updateCommand.Parameters.AddWithValue("@position", position);
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@book_id", bookId);

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
    public async Task<Books> GetUserBookByIdAsync(string userId, string Id)
    {
        Books book = new Books();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT * 
                FROM user_books 
                WHERE book_id = @id AND user_id = @user_id
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", Id);
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            book = new Books
                            {
                                Id = reader.GetStringSafe("book_id"),
                                Level = reader.GetIntSafe("level"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Experience = reader.GetDoubleSafe("experience"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                BaseStats = new BaseStats
                                {
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
                                }
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

        return book;
    }
    public async Task<BaseStats> GetTeamTotalStatsAsync(string userId)
    {
        BaseStats totalStats = new BaseStats();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT 
                -- Tính SUM trực tiếp áp dụng Quality, Star (min = 1) và Level (min = 1)
                SUM(uc.health * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS health,
                SUM(uc.physical_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS physical_attack,
                SUM(uc.physical_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS physical_defense,
                SUM(uc.magical_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS magical_attack,
                SUM(uc.magical_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS magical_defense,
                SUM(uc.chemical_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS chemical_attack,
                SUM(uc.chemical_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS chemical_defense,
                SUM(uc.atomic_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS atomic_attack,
                SUM(uc.atomic_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS atomic_defense,
                SUM(uc.mental_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS mental_attack,
                SUM(uc.mental_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS mental_defense,
                SUM(uc.speed * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS speed,
                SUM(uc.critical_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS critical_damage_rate,
                SUM(uc.critical_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS critical_rate,
                SUM(uc.critical_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS critical_resistance_rate,
                SUM(uc.ignore_critical_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_critical_rate,
                SUM(uc.penetration_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS penetration_rate,
                SUM(uc.penetration_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS penetration_resistance_rate,
                SUM(uc.evasion_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS evasion_rate,
                SUM(uc.damage_absorption_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS damage_absorption_rate,
                SUM(uc.ignore_damage_absorption_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_damage_absorption_rate,
                SUM(uc.absorbed_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS absorbed_damage_rate,
                SUM(uc.vitality_regeneration_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS vitality_regeneration_rate,
                SUM(uc.vitality_regeneration_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS vitality_regeneration_resistance_rate,
                SUM(uc.accuracy_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS accuracy_rate,
                SUM(uc.lifesteal_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS lifesteal_rate,
                SUM(uc.shield_strength * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS shield_strength,
                SUM(uc.tenacity * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS tenacity,
                SUM(uc.resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS resistance_rate,
                SUM(uc.combo_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS combo_rate,
                SUM(uc.ignore_combo_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_combo_rate,
                SUM(uc.combo_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS combo_damage_rate,
                SUM(uc.combo_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS combo_resistance_rate,
                SUM(uc.stun_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS stun_rate,
                SUM(uc.ignore_stun_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_stun_rate,
                SUM(uc.reflection_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS reflection_rate,
                SUM(uc.ignore_reflection_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_reflection_rate,
                SUM(uc.reflection_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS reflection_damage_rate,
                SUM(uc.reflection_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS reflection_resistance_rate,
                SUM(uc.mana * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS mana,
                SUM(uc.mana_regeneration_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS mana_regeneration_rate,
                SUM(uc.damage_to_different_faction_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS damage_to_different_faction_rate,
                SUM(uc.resistance_to_different_faction_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS resistance_to_different_faction_rate,
                SUM(uc.damage_to_same_faction_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS damage_to_same_faction_rate,
                SUM(uc.resistance_to_same_faction_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS resistance_to_same_faction_rate,
                SUM(uc.normal_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS normal_damage_rate,
                SUM(uc.normal_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS normal_resistance_rate,
                SUM(uc.skill_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS skill_damage_rate,
                SUM(uc.skill_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS skill_resistance_rate
            FROM user_books uc
            WHERE uc.user_id = @user_id AND uc.team_id IS NOT NULL;";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", userId);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
            // Chỉ có đúng 1 dòng trả về
            if (await reader.ReadAsync())
            {
                totalStats.Health = reader.GetDoubleSafe("health");
                totalStats.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                totalStats.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                totalStats.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                totalStats.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                totalStats.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                totalStats.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                totalStats.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                totalStats.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                totalStats.MentalAttack = reader.GetDoubleSafe("mental_attack");
                totalStats.MentalDefense = reader.GetDoubleSafe("mental_defense");
                totalStats.Speed = reader.GetDoubleSafe("speed");
                totalStats.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                totalStats.CriticalRate = reader.GetDoubleSafe("critical_rate");
                totalStats.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                totalStats.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                totalStats.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                totalStats.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                totalStats.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                totalStats.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                totalStats.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                totalStats.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                totalStats.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                totalStats.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                totalStats.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                totalStats.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                totalStats.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                totalStats.Tenacity = reader.GetDoubleSafe("tenacity");
                totalStats.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                totalStats.ComboRate = reader.GetDoubleSafe("combo_rate");
                totalStats.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                totalStats.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                totalStats.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                totalStats.StunRate = reader.GetDoubleSafe("stun_rate");
                totalStats.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                totalStats.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                totalStats.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                totalStats.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                totalStats.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                totalStats.Mana = reader.GetDoubleSafe("mana");
                totalStats.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                totalStats.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                totalStats.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                totalStats.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                totalStats.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                totalStats.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                totalStats.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                totalStats.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                totalStats.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");

            }

        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return totalStats;
    }
    public async Task<BaseStats> GetTeamTotalStatsWithoutQualityAsync(string userId)
    {
        BaseStats totalStats = new BaseStats();
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
           SELECT 
                SUM(uc.health) AS health,
                SUM(uc.physical_attack) AS physical_attack,
                SUM(uc.physical_defense) AS physical_defense,
                SUM(uc.magical_attack) AS magical_attack,
                SUM(uc.magical_defense) AS magical_defense,
                SUM(uc.chemical_attack) AS chemical_attack,
                SUM(uc.chemical_defense) AS chemical_defense,
                SUM(uc.atomic_attack) AS atomic_attack,
                SUM(uc.atomic_defense) AS atomic_defense,
                SUM(uc.mental_attack) AS mental_attack,
                SUM(uc.mental_defense) AS mental_defense,
                SUM(uc.speed) AS speed,
                SUM(uc.critical_damage_rate) AS critical_damage_rate,
                SUM(uc.critical_rate) AS critical_rate,
                SUM(uc.critical_resistance_rate) AS critical_resistance_rate,
                SUM(uc.ignore_critical_rate) AS ignore_critical_rate,
                SUM(uc.penetration_rate) AS penetration_rate,
                SUM(uc.penetration_resistance_rate) AS penetration_resistance_rate,
                SUM(uc.evasion_rate) AS evasion_rate,
                SUM(uc.damage_absorption_rate) AS damage_absorption_rate,
                SUM(uc.ignore_damage_absorption_rate) AS ignore_damage_absorption_rate,
                SUM(uc.absorbed_damage_rate) AS absorbed_damage_rate,
                SUM(uc.vitality_regeneration_rate) AS vitality_regeneration_rate,
                SUM(uc.vitality_regeneration_resistance_rate) AS vitality_regeneration_resistance_rate,
                SUM(uc.accuracy_rate) AS accuracy_rate,
                SUM(uc.lifesteal_rate) AS lifesteal_rate,
                SUM(uc.shield_strength) AS shield_strength,
                SUM(uc.tenacity) AS tenacity,
                SUM(uc.resistance_rate) AS resistance_rate,
                SUM(uc.combo_rate) AS combo_rate,
                SUM(uc.ignore_combo_rate) AS ignore_combo_rate,
                SUM(uc.combo_damage_rate) AS combo_damage_rate,
                SUM(uc.combo_resistance_rate) AS combo_resistance_rate,
                SUM(uc.stun_rate) AS stun_rate,
                SUM(uc.ignore_stun_rate) AS ignore_stun_rate,
                SUM(uc.reflection_rate) AS reflection_rate,
                SUM(uc.ignore_reflection_rate) AS ignore_reflection_rate,
                SUM(uc.reflection_damage_rate) AS reflection_damage_rate,
                SUM(uc.reflection_resistance_rate) AS reflection_resistance_rate,
                SUM(uc.mana) AS mana,
                SUM(uc.mana_regeneration_rate) AS mana_regeneration_rate,
                SUM(uc.damage_to_different_faction_rate) AS damage_to_different_faction_rate,
                SUM(uc.resistance_to_different_faction_rate) AS resistance_to_different_faction_rate,
                SUM(uc.damage_to_same_faction_rate) AS damage_to_same_faction_rate,
                SUM(uc.resistance_to_same_faction_rate) AS resistance_to_same_faction_rate,
                SUM(uc.normal_damage_rate) AS normal_damage_rate,
                SUM(uc.normal_resistance_rate) AS normal_resistance_rate,
                SUM(uc.skill_damage_rate) AS skill_damage_rate,
                SUM(uc.skill_resistance_rate) AS skill_resistance_rate
            FROM user_books uc
            WHERE uc.user_id = @user_id AND uc.team_id IS NOT NULL;";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@user_id", userId);

            await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
            // Chỉ có đúng 1 dòng trả về
            if (await reader.ReadAsync())
            {
                totalStats.Health = reader.GetDoubleSafe("health");
                totalStats.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                totalStats.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                totalStats.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                totalStats.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                totalStats.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                totalStats.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                totalStats.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                totalStats.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                totalStats.MentalAttack = reader.GetDoubleSafe("mental_attack");
                totalStats.MentalDefense = reader.GetDoubleSafe("mental_defense");
                totalStats.Speed = reader.GetDoubleSafe("speed");
                totalStats.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                totalStats.CriticalRate = reader.GetDoubleSafe("critical_rate");
                totalStats.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                totalStats.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                totalStats.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                totalStats.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                totalStats.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                totalStats.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                totalStats.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                totalStats.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                totalStats.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                totalStats.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                totalStats.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                totalStats.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                totalStats.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                totalStats.Tenacity = reader.GetDoubleSafe("tenacity");
                totalStats.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                totalStats.ComboRate = reader.GetDoubleSafe("combo_rate");
                totalStats.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                totalStats.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                totalStats.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                totalStats.StunRate = reader.GetDoubleSafe("stun_rate");
                totalStats.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                totalStats.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                totalStats.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                totalStats.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                totalStats.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                totalStats.Mana = reader.GetDoubleSafe("mana");
                totalStats.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                totalStats.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                totalStats.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                totalStats.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                totalStats.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                totalStats.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                totalStats.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                totalStats.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                totalStats.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");

            }

        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return totalStats;
    }
}
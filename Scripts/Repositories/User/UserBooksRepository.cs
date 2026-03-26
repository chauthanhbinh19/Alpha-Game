using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserBooksRepository : IUserBooksRepository
{
    public async Task<List<Books>> GetUserBooksAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Books> books = new List<Books>();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = @"SELECT ub.*, b.name, b.image, b.type, b.description
                FROM user_books ub
                LEFT JOIN books b ON ub.book_id = b.id 
                WHERE ub.user_id = @userId 
                ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    query += " AND b.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND b.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND b.name LIKE CONCAT('%', @search, '%')";
                }

                query += " ORDER BY b.name";
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
                            Books book = new Books
                            {
                                Id = reader.GetStringSafe("book_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Type = reader.GetStringSafe("type"),
                                Star = reader.GetIntSafe("star"),
                                Level = reader.GetIntSafe("level"),
                                Experiment = reader.GetDoubleSafe("experiment"),
                                Quantity = reader.GetDoubleSafe("quantity"),
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
    public async Task<List<Books>> GetUserBooksTeamAsync(string teamId)
    {
        List<Books> books = new List<Books>();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = @"SELECT ub.*, b.name, b.image, b.type, b.description
                FROM user_books ub
                LEFT JOIN books b ON ub.book_id = b.id 
                WHERE ub.user_id = @userId AND ub.team_id=@team_id
                ORDER BY b.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(b.name, '[0-9]+$') AS UNSIGNED), b.name;
                ";
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                    command.Parameters.AddWithValue("@team_id", teamId);

                    await using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            Books book = new Books
                            {
                                Id = reader.GetStringSafe("book_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Type = reader.GetStringSafe("type"),
                                Star = reader.GetIntSafe("star"),
                                Level = reader.GetIntSafe("level"),
                                Experiment = reader.GetDoubleSafe("experiment"),
                                Quantity = reader.GetDoubleSafe("quantity"),
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
    public async Task<Dictionary<string, int>> GetUniqueBooksTypesTeamAsync(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT distinct c.type, count(c.type) as number
                FROM user_card_books uc
                LEFT JOIN books c ON uc.book_id = c.id 
                WHERE uc.user_id =@userId and uc.team_id=@team_id
                group by c.type, c.type";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                    command.Parameters.AddWithValue("@team_id", teamId);

                    await using (MySqlDataReader reader = command.ExecuteReader())
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
    public async Task<int> GetUserBooksCountAsync(string user_id, string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"Select count(*) 
                             from books b, user_books ub 
                             where b.id = ub.book_id 
                               and ub.user_id = @userId ";
                
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    query += " AND b.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND b.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND b.name LIKE CONCAT('%', @search, '%')";
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
    public async Task<bool> InsertUserBookAsync(Books book)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // 1. Check existing record
                string checkQuery = @"
                SELECT COUNT(*) FROM user_books 
                WHERE user_id = @user_id AND book_id = @book_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@book_id", book.Id);

                    object result = await checkCommand.ExecuteScalarAsync();
                    int count = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);

                    // 2. INSERT NEW
                    if (count == 0)
                    {
                        string insertQuery = @"
                INSERT INTO user_books (
                    user_id, book_id, rare, level, experiment, star, quality, block, quantity,
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
                    @user_id, @book_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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

                        await using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            command.Parameters.AddWithValue("@book_id", book.Id);
                            command.Parameters.AddWithValue("@rare", book.Rare);
                            command.Parameters.AddWithValue("@level", 0);
                            command.Parameters.AddWithValue("@experiment", 0);
                            command.Parameters.AddWithValue("@star", 0);
                            command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(book.Rare));
                            command.Parameters.AddWithValue("@block", false);
                            command.Parameters.AddWithValue("@quantity", book.Quantity);

                            // → Add remaining stats
                            command.Parameters.AddWithValue("@power", book.Power);
                            command.Parameters.AddWithValue("@health", book.Health);
                            command.Parameters.AddWithValue("@physical_attack", book.PhysicalAttack);
                            command.Parameters.AddWithValue("@physical_defense", book.PhysicalDefense);
                            command.Parameters.AddWithValue("@magical_attack", book.MagicalAttack);
                            command.Parameters.AddWithValue("@magical_defense", book.MagicalDefense);
                            command.Parameters.AddWithValue("@chemical_attack", book.ChemicalAttack);
                            command.Parameters.AddWithValue("@chemical_defense", book.ChemicalDefense);
                            command.Parameters.AddWithValue("@atomic_attack", book.AtomicAttack);
                            command.Parameters.AddWithValue("@atomic_defense", book.AtomicDefense);
                            command.Parameters.AddWithValue("@mental_attack", book.MentalAttack);
                            command.Parameters.AddWithValue("@mental_defense", book.MentalDefense);
                            command.Parameters.AddWithValue("@speed", book.Speed);
                            command.Parameters.AddWithValue("@critical_damage_rate", book.CriticalDamageRate);
                            command.Parameters.AddWithValue("@critical_rate", book.CriticalRate);
                            command.Parameters.AddWithValue("@critical_resistance_rate", book.CriticalResistanceRate);
                            command.Parameters.AddWithValue("@ignore_critical_rate", book.IgnoreCriticalRate);
                            command.Parameters.AddWithValue("@penetration_rate", book.PenetrationRate);
                            command.Parameters.AddWithValue("@penetration_resistance_rate", book.PenetrationResistanceRate);
                            command.Parameters.AddWithValue("@evasion_rate", book.EvasionRate);
                            command.Parameters.AddWithValue("@damage_absorption_rate", book.DamageAbsorptionRate);
                            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", book.IgnoreDamageAbsorptionRate);
                            command.Parameters.AddWithValue("@absorbed_damage_rate", book.AbsorbedDamageRate);
                            command.Parameters.AddWithValue("@vitality_regeneration_rate", book.VitalityRegenerationRate);
                            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", book.VitalityRegenerationResistanceRate);
                            command.Parameters.AddWithValue("@accuracy_rate", book.AccuracyRate);
                            command.Parameters.AddWithValue("@lifesteal_rate", book.LifestealRate);
                            command.Parameters.AddWithValue("@shield_strength", book.ShieldStrength);
                            command.Parameters.AddWithValue("@tenacity", book.Tenacity);
                            command.Parameters.AddWithValue("@resistance_rate", book.ResistanceRate);
                            command.Parameters.AddWithValue("@combo_rate", book.ComboRate);
                            command.Parameters.AddWithValue("@ignore_combo_rate", book.IgnoreComboRate);
                            command.Parameters.AddWithValue("@combo_damage_rate", book.ComboDamageRate);
                            command.Parameters.AddWithValue("@combo_resistance_rate", book.ComboResistanceRate);
                            command.Parameters.AddWithValue("@stun_rate", book.StunRate);
                            command.Parameters.AddWithValue("@ignore_stun_rate", book.IgnoreStunRate);
                            command.Parameters.AddWithValue("@reflection_rate", book.ReflectionRate);
                            command.Parameters.AddWithValue("@ignore_reflection_rate", book.IgnoreReflectionRate);
                            command.Parameters.AddWithValue("@reflection_damage_rate", book.ReflectionDamageRate);
                            command.Parameters.AddWithValue("@reflection_resistance_rate", book.ReflectionResistanceRate);
                            command.Parameters.AddWithValue("@mana", book.Mana);
                            command.Parameters.AddWithValue("@mana_regeneration_rate", book.ManaRegenerationRate);
                            command.Parameters.AddWithValue("@damage_to_different_faction_rate", book.DamageToDifferentFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", book.ResistanceToDifferentFactionRate);
                            command.Parameters.AddWithValue("@damage_to_same_faction_rate", book.DamageToSameFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", book.ResistanceToSameFactionRate);
                            command.Parameters.AddWithValue("@normal_damage_rate", book.NormalDamageRate);
                            command.Parameters.AddWithValue("@normal_resistance_rate", book.NormalResistanceRate);
                            command.Parameters.AddWithValue("@skill_damage_rate", book.SkillDamageRate);
                            command.Parameters.AddWithValue("@skill_resistance_rate", book.SkillResistanceRate);

                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // 3. UPDATE EXISTING
                        string updateQuery = @"
                            UPDATE user_books
                            SET quantity = @quantity
                            WHERE user_id = @user_id AND book_id = @book_id;";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
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
    public async Task<bool> UpdateBookLevelAsync(Books book, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_books
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
                    vitality_regeneration_rate = @vitality_regeneration_rate, 
                    vitality_regeneration_resistance_rate = @vitality_regeneration_resistance_rate, 
                    accuracy_rate = @accuracy_rate, lifesteal_rate = @lifesteal_rate, 
                    shield_strength = @shield_strength, 
                    tenacity = @tenacity, resistance_rate = @resistance_rate, 
                    combo_rate = @comboRate, ignore_combo_rate = @ignore_combo_rate, 
                    combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
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

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@book_id", book.Id);
                    command.Parameters.AddWithValue("@level", cardLevel);

                    command.Parameters.AddWithValue("@power", book.Power);
                    command.Parameters.AddWithValue("@health", book.Health);
                    command.Parameters.AddWithValue("@physical_attack", book.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", book.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", book.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", book.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", book.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", book.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", book.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", book.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", book.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", book.MentalDefense);
                    command.Parameters.AddWithValue("@speed", book.Speed);

                    command.Parameters.AddWithValue("@critical_damage_rate", book.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", book.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", book.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", book.IgnoreCriticalRate);

                    command.Parameters.AddWithValue("@penetration_rate", book.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", book.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", book.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", book.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", book.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", book.AbsorbedDamageRate);

                    command.Parameters.AddWithValue("@vitality_regeneration_rate", book.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", book.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", book.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", book.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", book.ShieldStrength);

                    command.Parameters.AddWithValue("@tenacity", book.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", book.ResistanceRate);
                    command.Parameters.AddWithValue("@comboRate", book.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", book.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", book.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", book.ComboResistanceRate);

                    command.Parameters.AddWithValue("@stun_rate", book.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", book.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", book.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", book.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", book.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", book.ReflectionResistanceRate);

                    command.Parameters.AddWithValue("@mana", book.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", book.ManaRegenerationRate);

                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", book.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", book.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", book.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", book.ResistanceToSameFactionRate);

                    command.Parameters.AddWithValue("@normal_damage_rate", book.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", book.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", book.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", book.SkillResistanceRate);

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
    public async Task<bool> UpdateBookBreakthroughAsync(Books book, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@book_id", book.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);

                    command.Parameters.AddWithValue("@power", book.Power);
                    command.Parameters.AddWithValue("@health", book.Health);
                    command.Parameters.AddWithValue("@physical_attack", book.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", book.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", book.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", book.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", book.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", book.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", book.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", book.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", book.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", book.MentalDefense);
                    command.Parameters.AddWithValue("@speed", book.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", book.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", book.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", book.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", book.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", book.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", book.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", book.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", book.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", book.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", book.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", book.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", book.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", book.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", book.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", book.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", book.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", book.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", book.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", book.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", book.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", book.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", book.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", book.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", book.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", book.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", book.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", book.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", book.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", book.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", book.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", book.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", book.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", book.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", book.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", book.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", book.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", book.SkillResistanceRate);

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
    public async Task<bool> UpdateTeamBookAsync(string team_id, string position, string book_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_books 
                SET team_id = @team_id, position = @position 
                WHERE user_id = @user_id AND book_id = @book_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@team_id", team_id);
                    command.Parameters.AddWithValue("@position", position);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@book_id", book_id);

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
    public async Task<Books> GetUserBookByIdAsync(string user_id, string Id)
    {
        Books book = new Books();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT * 
                FROM user_books 
                WHERE book_id = @id AND user_id = @user_id
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            book = new Books
                            {
                                Id = reader.GetStringSafe("book_id"),
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
    public async Task<List<Books>> GetAllUserBooksInTeamAsync(string user_id)
    {
        List<Books> books = new List<Books>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT uc.*, c.name, c.image, c.type, c.description
                FROM user_books uc
                LEFT JOIN books c ON uc.book_id = c.id 
                WHERE uc.user_id = @user_id AND uc.team_id IS NOT NULL";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Books book = new Books
                            {
                                Id = reader.GetStringSafe("book_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Type = reader.GetStringSafe("type"),
                                Star = reader.GetIntSafe("star"),
                                Level = reader.GetIntSafe("level"),
                                Experiment = reader.GetIntSafe("experiment"),
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
            catch (Exception ex)
            {
                Debug.LogError("Error GetAllUserBooksInTeamAsync: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return books;
    }
}
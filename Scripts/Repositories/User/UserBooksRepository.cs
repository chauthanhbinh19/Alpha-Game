using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserBooksRepository : IUserBooksRepository
{
    public async Task<List<Books>> GetUserBooksAsync(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<Books> bookslist = new List<Books>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = @"SELECT ub.*, b.name, b.image, b.type, b.description
                FROM user_books ub
                LEFT JOIN books b ON ub.book_id = b.id 
                WHERE ub.user_id = @userId AND b.type = @type AND (@rare = 'All' or b.rare = @rare)
                ORDER BY b.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(b.name, '[0-9]+$') AS UNSIGNED), b.name
                LIMIT @limit OFFSET @offset;
                ";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@rare", rare);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Books book = new Books
                            {
                                Id = reader.GetString("book_id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetInt32("quality"),
                                Type = reader.GetString("type"),
                                Star = reader.GetInt32("star"),
                                Level = reader.GetInt32("level"),
                                Experiment = reader.GetDouble("experiment"),
                                Quantity = reader.GetDouble("quantity"),
                                Block = reader.GetBoolean("block"),
                                TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetString("team_id"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),

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
                                Description = reader.GetString("description"),

                                BaseStats = new BaseStats
                                {
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
                                }
                            };

                            bookslist.Add(book);
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
        return bookslist;
    }
    public async Task<List<Books>> GetUserBooksTeamAsync(string teamId)
    {
        List<Books> bookslist = new List<Books>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
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
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                    command.Parameters.AddWithValue("@team_id", teamId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            Books book = new Books
                            {
                                Id = reader.GetString("book_id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Type = reader.GetString("type"),
                                Star = reader.GetInt32("star"),
                                Level = reader.GetInt32("level"),
                                Experiment = reader.GetDouble("experiment"),
                                Quantity = reader.GetDouble("quantity"),
                                Block = reader.GetBoolean("block"),
                                TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetString("team_id"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),

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
                                Description = reader.GetString("description"),

                                BaseStats = new BaseStats
                                {
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
                                }
                            };

                            bookslist.Add(book);
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
        return bookslist;
    }
    public async Task<Dictionary<string, int>> GetUniqueBooksTypesTeamAsync(string teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT distinct c.type, count(c.type) as number
                FROM user_card_books uc
                LEFT JOIN books c ON uc.book_id = c.id 
                WHERE uc.user_id =@userId and uc.team_id=@team_id
                group by c.type, c.type";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                    command.Parameters.AddWithValue("@team_id", teamId);

                    using (MySqlDataReader reader = command.ExecuteReader())
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
    public async Task<int> GetUserBooksCountAsync(string user_id, string type, string rare)
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
                               and ub.user_id = @userId 
                               and b.type = @type 
                               AND (@rare = 'All' or b.rare = @rare)";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@rare", rare);

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
    public async Task<bool> InsertUserBookAsync(Books books)
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
                    checkCommand.Parameters.AddWithValue("@book_id", books.Id);

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
                            command.Parameters.AddWithValue("@book_id", books.Id);
                            command.Parameters.AddWithValue("@rare", books.Rare);
                            command.Parameters.AddWithValue("@level", 0);
                            command.Parameters.AddWithValue("@experiment", 0);
                            command.Parameters.AddWithValue("@star", 0);
                            command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(books.Rare));
                            command.Parameters.AddWithValue("@block", false);
                            command.Parameters.AddWithValue("@quantity", books.Quantity);

                            // → Add remaining stats
                            command.Parameters.AddWithValue("@power", books.Power);
                            command.Parameters.AddWithValue("@health", books.Health);
                            command.Parameters.AddWithValue("@physical_attack", books.PhysicalAttack);
                            command.Parameters.AddWithValue("@physical_defense", books.PhysicalDefense);
                            command.Parameters.AddWithValue("@magical_attack", books.MagicalAttack);
                            command.Parameters.AddWithValue("@magical_defense", books.MagicalDefense);
                            command.Parameters.AddWithValue("@chemical_attack", books.ChemicalAttack);
                            command.Parameters.AddWithValue("@chemical_defense", books.ChemicalDefense);
                            command.Parameters.AddWithValue("@atomic_attack", books.AtomicAttack);
                            command.Parameters.AddWithValue("@atomic_defense", books.AtomicDefense);
                            command.Parameters.AddWithValue("@mental_attack", books.MentalAttack);
                            command.Parameters.AddWithValue("@mental_defense", books.MentalDefense);
                            command.Parameters.AddWithValue("@speed", books.Speed);
                            command.Parameters.AddWithValue("@critical_damage_rate", books.CriticalDamageRate);
                            command.Parameters.AddWithValue("@critical_rate", books.CriticalRate);
                            command.Parameters.AddWithValue("@critical_resistance_rate", books.CriticalResistanceRate);
                            command.Parameters.AddWithValue("@ignore_critical_rate", books.IgnoreCriticalRate);
                            command.Parameters.AddWithValue("@penetration_rate", books.PenetrationRate);
                            command.Parameters.AddWithValue("@penetration_resistance_rate", books.PenetrationResistanceRate);
                            command.Parameters.AddWithValue("@evasion_rate", books.EvasionRate);
                            command.Parameters.AddWithValue("@damage_absorption_rate", books.DamageAbsorptionRate);
                            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", books.IgnoreDamageAbsorptionRate);
                            command.Parameters.AddWithValue("@absorbed_damage_rate", books.AbsorbedDamageRate);
                            command.Parameters.AddWithValue("@vitality_regeneration_rate", books.VitalityRegenerationRate);
                            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", books.VitalityRegenerationResistanceRate);
                            command.Parameters.AddWithValue("@accuracy_rate", books.AccuracyRate);
                            command.Parameters.AddWithValue("@lifesteal_rate", books.LifestealRate);
                            command.Parameters.AddWithValue("@shield_strength", books.ShieldStrength);
                            command.Parameters.AddWithValue("@tenacity", books.Tenacity);
                            command.Parameters.AddWithValue("@resistance_rate", books.ResistanceRate);
                            command.Parameters.AddWithValue("@combo_rate", books.ComboRate);
                            command.Parameters.AddWithValue("@ignore_combo_rate", books.IgnoreComboRate);
                            command.Parameters.AddWithValue("@combo_damage_rate", books.ComboDamageRate);
                            command.Parameters.AddWithValue("@combo_resistance_rate", books.ComboResistanceRate);
                            command.Parameters.AddWithValue("@stun_rate", books.StunRate);
                            command.Parameters.AddWithValue("@ignore_stun_rate", books.IgnoreStunRate);
                            command.Parameters.AddWithValue("@reflection_rate", books.ReflectionRate);
                            command.Parameters.AddWithValue("@ignore_reflection_rate", books.IgnoreReflectionRate);
                            command.Parameters.AddWithValue("@reflection_damage_rate", books.ReflectionDamageRate);
                            command.Parameters.AddWithValue("@reflection_resistance_rate", books.ReflectionResistanceRate);
                            command.Parameters.AddWithValue("@mana", books.Mana);
                            command.Parameters.AddWithValue("@mana_regeneration_rate", books.ManaRegenerationRate);
                            command.Parameters.AddWithValue("@damage_to_different_faction_rate", books.DamageToDifferentFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", books.ResistanceToDifferentFactionRate);
                            command.Parameters.AddWithValue("@damage_to_same_faction_rate", books.DamageToSameFactionRate);
                            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", books.ResistanceToSameFactionRate);
                            command.Parameters.AddWithValue("@normal_damage_rate", books.NormalDamageRate);
                            command.Parameters.AddWithValue("@normal_resistance_rate", books.NormalResistanceRate);
                            command.Parameters.AddWithValue("@skill_damage_rate", books.SkillDamageRate);
                            command.Parameters.AddWithValue("@skill_resistance_rate", books.SkillResistanceRate);

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
                            updateCommand.Parameters.AddWithValue("@book_id", books.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", books.Quantity);

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
    public async Task<bool> UpdateBookLevelAsync(Books books, int cardLevel)
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
                    command.Parameters.AddWithValue("@book_id", books.Id);
                    command.Parameters.AddWithValue("@level", cardLevel);

                    command.Parameters.AddWithValue("@power", books.Power);
                    command.Parameters.AddWithValue("@health", books.Health);
                    command.Parameters.AddWithValue("@physical_attack", books.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", books.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", books.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", books.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", books.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", books.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", books.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", books.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", books.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", books.MentalDefense);
                    command.Parameters.AddWithValue("@speed", books.Speed);

                    command.Parameters.AddWithValue("@critical_damage_rate", books.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", books.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", books.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", books.IgnoreCriticalRate);

                    command.Parameters.AddWithValue("@penetration_rate", books.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", books.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", books.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", books.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", books.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", books.AbsorbedDamageRate);

                    command.Parameters.AddWithValue("@vitality_regeneration_rate", books.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", books.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", books.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", books.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", books.ShieldStrength);

                    command.Parameters.AddWithValue("@tenacity", books.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", books.ResistanceRate);
                    command.Parameters.AddWithValue("@comboRate", books.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", books.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", books.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", books.ComboResistanceRate);

                    command.Parameters.AddWithValue("@stun_rate", books.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", books.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", books.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", books.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", books.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", books.ReflectionResistanceRate);

                    command.Parameters.AddWithValue("@mana", books.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", books.ManaRegenerationRate);

                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", books.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", books.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", books.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", books.ResistanceToSameFactionRate);

                    command.Parameters.AddWithValue("@normal_damage_rate", books.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", books.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", books.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", books.SkillResistanceRate);

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
    public async Task<bool> UpdateBookBreakthroughAsync(Books books, int star, double quantity)
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
                    command.Parameters.AddWithValue("@book_id", books.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);

                    command.Parameters.AddWithValue("@power", books.Power);
                    command.Parameters.AddWithValue("@health", books.Health);
                    command.Parameters.AddWithValue("@physical_attack", books.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", books.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", books.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", books.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", books.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", books.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", books.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", books.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", books.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", books.MentalDefense);
                    command.Parameters.AddWithValue("@speed", books.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", books.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", books.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", books.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", books.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", books.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", books.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", books.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", books.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", books.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", books.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", books.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", books.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", books.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", books.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", books.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", books.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", books.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", books.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", books.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", books.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", books.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", books.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", books.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", books.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", books.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", books.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", books.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", books.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", books.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", books.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", books.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", books.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", books.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", books.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", books.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", books.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", books.SkillResistanceRate);

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
        Books card = new Books();
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
                            card = new Books
                            {
                                Id = reader.GetString("book_id"),
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
                                SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                                BaseStats = new BaseStats
                                {
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

        return card;
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
                                Id = reader.GetString("book_id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetInt32("quality"),
                                Type = reader.GetString("type"),
                                Star = reader.GetInt32("star"),
                                Level = reader.GetInt32("level"),
                                Experiment = reader.GetInt32("experiment"),
                                Quantity = reader.GetInt32("quantity"),
                                Block = reader.GetBoolean("block"),
                                TeamId = reader.IsDBNull(reader.GetOrdinal("team_id")) ? null : reader.GetString("team_id"),
                                Position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
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
                                Description = reader.GetString("description"),

                                BaseStats = new BaseStats
                                {
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
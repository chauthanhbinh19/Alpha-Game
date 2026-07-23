using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

public class UserSkillsRepository : IUserSkillsRepository
{
    private static readonly System.Random _rng = new System.Random();
    private readonly List<(string TableName, string ColumnId)> _configs = new List<(string, string)>
    {
        ("card_heroes_skills",    "card_hero_id"),
        ("card_captains_skills",  "card_captain_id"),
        ("card_colonels_skills",  "card_colonel_id"),
        ("card_generals_skills",  "card_general_id"),
        ("card_admirals_skills",  "card_admiral_id"),
        ("card_monsters_skills",  "card_monster_id"),
        ("card_militaries_skills", "card_military_id"),
        ("card_soldiers_skills",  "card_soldier_id"),
        ("card_spells_skills",    "card_spell_id")
    };
    public async Task<List<Skills>> GetUserSkillsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Skills> skills = new List<Skills>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description,
                    sp.pattern_id,
                    -- Subquery gom nhóm hiệu ứng thành JSON ngay tại dòng dữ liệu
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', se.min_value,
                                'max_value', se.max_value,
                                'trigger_phase', se.trigger_phase,
                                'trigger_condition', se.trigger_condition,
                                'is_stackable', se.is_stackable,
                                'is_removable', se.is_removable,
                                'effect_id', e.id,
                                'effect_name', e.name,
                                'effect_type', e.effect_type,
                                'duration', e.duration,
                                'effect_description', e.description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        )
                        FROM skill_effect se
                        LEFT JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                        LEFT JOIN effect_property_action epa ON e.id = epa.effect_id
                        LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                        LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                        WHERE se.skill_id = s.id -- Mối liên kết map ngược lại với Skill đang xét ở bảng ngoài
                    ) AS skill_effects_json
                FROM skills s
                INNER JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN skill_patterns sp ON s.id = sp.skill_id
                WHERE us.user_id = @userId";

                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND s.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND s.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND s.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += @" LIMIT @limit OFFSET @offset";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
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

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rarity = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        SkillType = reader.GetStringSafe("skill_type"),
                        Experience = reader.GetDoubleSafe("experience"),
                        Quantity = reader.GetIntSafe("quantity"),
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

                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe("pattern_id")
                        }
                    };

                    string effectsJson = reader.GetStringSafe("skill_effects_json");

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        try
                        {
                            // Chuyển đổi chuỗi JSON thành List<Emblem> trong C#
                            skill.Effects = JsonHelper.DeserializeEffects(effectsJson);
                        }
                        catch
                        {
                            // Phòng trường hợp Hero không có emblem, MySQL sinh ra chuỗi "[null]"
                            skill.Effects = new List<Effects>();
                        }
                    }


                    skills.Add(skill);
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

        return skills;
    }
    public async Task<int> GetUserSkillsCountAsync(string userId, string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT COUNT(*) 
                FROM skills s
                INNER JOIN user_skills us ON s.id = us.skill_id
                WHERE us.user_id = @userId ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND s.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND s.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND s.name LIKE CONCAT('%', @search, '%')";
                }

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
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

                var result = await selectCommand.ExecuteScalarAsync();
                count = Convert.ToInt32(result);

                return count;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return 0;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> InsertUserSkillAsync(string userId, Skills skill)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) FROM user_skills 
                WHERE user_id = @user_id AND skill_id = @skill_id;";

                await using var checkCommand = new MySqlCommand(checkSQL, connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@skill_id", skill.Id);

                var countObj = await checkCommand.ExecuteScalarAsync();
                int count = Convert.ToInt32(countObj);

                if (count == 0)
                {
                    string insertSQL = @"
                INSERT INTO user_skills (
                    user_id, skill_id, rare, level, experience, star, quality, block, quantity,
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
                    @user_id, @skill_id, @rare, @level, @experience, @star, @quality, @block, @quantity,
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

                    await using var insertCommand = new MySqlCommand(insertSQL, connection);

                    insertCommand.Parameters.AddWithValue("@user_id", userId);
                    insertCommand.Parameters.AddWithValue("@skill_id", skill.Id);
                    insertCommand.Parameters.AddWithValue("@rare", skill.Rarity);
                    insertCommand.Parameters.AddWithValue("@level", 0);
                    insertCommand.Parameters.AddWithValue("@experience", 0);
                    insertCommand.Parameters.AddWithValue("@star", 0);
                    insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(skill.Rarity));
                    insertCommand.Parameters.AddWithValue("@block", false);
                    insertCommand.Parameters.AddWithValue("@quantity", skill.Quantity);
                    insertCommand.Parameters.AddWithValue("@power", skill.Power);
                    insertCommand.Parameters.AddWithValue("@health", skill.Health);
                    insertCommand.Parameters.AddWithValue("@physical_attack", skill.PhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@physical_defense", skill.PhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@magical_attack", skill.MagicalAttack);
                    insertCommand.Parameters.AddWithValue("@magical_defense", skill.MagicalDefense);
                    insertCommand.Parameters.AddWithValue("@chemical_attack", skill.ChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@chemical_defense", skill.ChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@atomic_attack", skill.AtomicAttack);
                    insertCommand.Parameters.AddWithValue("@atomic_defense", skill.AtomicDefense);
                    insertCommand.Parameters.AddWithValue("@mental_attack", skill.MentalAttack);
                    insertCommand.Parameters.AddWithValue("@mental_defense", skill.MentalDefense);
                    insertCommand.Parameters.AddWithValue("@speed", skill.Speed);
                    insertCommand.Parameters.AddWithValue("@critical_damage_rate", skill.CriticalDamageRate);
                    insertCommand.Parameters.AddWithValue("@critical_rate", skill.CriticalRate);
                    insertCommand.Parameters.AddWithValue("@critical_resistance_rate", skill.CriticalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@ignore_critical_rate", skill.IgnoreCriticalRate);
                    insertCommand.Parameters.AddWithValue("@penetration_rate", skill.PenetrationRate);
                    insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", skill.PenetrationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@evasion_rate", skill.EvasionRate);
                    insertCommand.Parameters.AddWithValue("@damage_absorption_rate", skill.DamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", skill.IgnoreDamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", skill.AbsorbedDamageRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", skill.VitalityRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skill.VitalityRegenerationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@accuracy_rate", skill.AccuracyRate);
                    insertCommand.Parameters.AddWithValue("@lifesteal_rate", skill.LifestealRate);
                    insertCommand.Parameters.AddWithValue("@shield_strength", skill.ShieldStrength);
                    insertCommand.Parameters.AddWithValue("@tenacity", skill.Tenacity);
                    insertCommand.Parameters.AddWithValue("@resistance_rate", skill.ResistanceRate);
                    insertCommand.Parameters.AddWithValue("@combo_rate", skill.ComboRate);
                    insertCommand.Parameters.AddWithValue("@ignore_combo_rate", skill.IgnoreComboRate);
                    insertCommand.Parameters.AddWithValue("@combo_damage_rate", skill.ComboDamageRate);
                    insertCommand.Parameters.AddWithValue("@combo_resistance_rate", skill.ComboResistanceRate);
                    insertCommand.Parameters.AddWithValue("@stun_rate", skill.StunRate);
                    insertCommand.Parameters.AddWithValue("@ignore_stun_rate", skill.IgnoreStunRate);
                    insertCommand.Parameters.AddWithValue("@reflection_rate", skill.ReflectionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", skill.IgnoreReflectionRate);
                    insertCommand.Parameters.AddWithValue("@reflection_damage_rate", skill.ReflectionDamageRate);
                    insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", skill.ReflectionResistanceRate);
                    insertCommand.Parameters.AddWithValue("@mana", skill.Mana);
                    insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", skill.ManaRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", skill.DamageToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", skill.ResistanceToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", skill.DamageToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", skill.ResistanceToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@normal_damage_rate", skill.NormalDamageRate);
                    insertCommand.Parameters.AddWithValue("@normal_resistance_rate", skill.NormalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@skill_damage_rate", skill.SkillDamageRate);
                    insertCommand.Parameters.AddWithValue("@skill_resistance_rate", skill.SkillResistanceRate);

                    await insertCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateSQL = @"
                    UPDATE user_skills
                    SET quantity = @quantity
                    WHERE user_id = @user_id AND skill_id = @skill_id;";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@skill_id", skill.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", skill.Quantity);

                    await updateCommand.ExecuteNonQueryAsync();
                }

                return true;
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
    }
    public async Task<bool> InsertOrUpdateUserSkillsBatchAsync(string userId, List<Skills> skills)
    {
        if (skills == null || skills.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 500; // vì nhiều column → giảm size

            for (int i = 0; i < skills.Count; i += batchSize)
            {
                var batch = skills.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
                INSERT INTO user_skills (
                    user_id, skill_id, rare, level, experience, star, quality, block, quantity,
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
                    (@user_id, @skill_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
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
                        new MySqlParameter($"@skill_id_{j}", c.Id),
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
                    quantity = COALESCE(user_skills.quantity, 0) + VALUES(quantity);
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
    public async Task<bool> UpdateUserSkillLevelAsync(string userId, Skills skill)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_skills
                SET 
                    level = @level, experience = @experience
                WHERE user_id = @user_id AND skill_id = @skill_id;
            ";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@skill_id", skill.Id);
                    updateCommand.Parameters.AddWithValue("@level", skill.Level);
                    updateCommand.Parameters.AddWithValue("@experience", skill.Experience);

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
    public async Task<bool> UpdateUserSkillStarAsync(string userId, Skills skill)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_skills
                SET 
                    star = @star, quantity = @quantity
                WHERE user_id = @user_id AND skill_id = @skill_id;
            ";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@skill_id", skill.Id);
                    updateCommand.Parameters.AddWithValue("@star", skill.Star);
                    updateCommand.Parameters.AddWithValue("@quantity", skill.Quantity);

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
    public async Task<bool> UpdateUserSkillBreakthroughAsync(string userId, Skills skill, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_skills
                SET 
                    star = @star, quantity = @quantity, power = @power, health = @health, 
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
                WHERE user_id = @user_id AND skill_id = @skill_id;";

                await using var updateCommand = new MySqlCommand(updateSQL, connection);

                updateCommand.Parameters.AddWithValue("@user_id", userId);
                updateCommand.Parameters.AddWithValue("@skill_id", skill.Id);
                updateCommand.Parameters.AddWithValue("@star", star);
                updateCommand.Parameters.AddWithValue("@quantity", quantity);
                updateCommand.Parameters.AddWithValue("@power", skill.Power);
                updateCommand.Parameters.AddWithValue("@health", skill.Health);
                updateCommand.Parameters.AddWithValue("@physical_attack", skill.PhysicalAttack);
                updateCommand.Parameters.AddWithValue("@physical_defense", skill.PhysicalDefense);
                updateCommand.Parameters.AddWithValue("@magical_attack", skill.MagicalAttack);
                updateCommand.Parameters.AddWithValue("@magical_defense", skill.MagicalDefense);
                updateCommand.Parameters.AddWithValue("@chemical_attack", skill.ChemicalAttack);
                updateCommand.Parameters.AddWithValue("@chemical_defense", skill.ChemicalDefense);
                updateCommand.Parameters.AddWithValue("@atomic_attack", skill.AtomicAttack);
                updateCommand.Parameters.AddWithValue("@atomic_defense", skill.AtomicDefense);
                updateCommand.Parameters.AddWithValue("@mental_attack", skill.MentalAttack);
                updateCommand.Parameters.AddWithValue("@mental_defense", skill.MentalDefense);
                updateCommand.Parameters.AddWithValue("@speed", skill.Speed);
                updateCommand.Parameters.AddWithValue("@critical_damage_rate", skill.CriticalDamageRate);
                updateCommand.Parameters.AddWithValue("@critical_rate", skill.CriticalRate);
                updateCommand.Parameters.AddWithValue("@critical_resistance_rate", skill.CriticalResistanceRate);
                updateCommand.Parameters.AddWithValue("@ignore_critical_rate", skill.IgnoreCriticalRate);
                updateCommand.Parameters.AddWithValue("@penetration_rate", skill.PenetrationRate);
                updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", skill.PenetrationResistanceRate);
                updateCommand.Parameters.AddWithValue("@evasion_rate", skill.EvasionRate);
                updateCommand.Parameters.AddWithValue("@damage_absorption_rate", skill.DamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", skill.IgnoreDamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", skill.AbsorbedDamageRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", skill.VitalityRegenerationRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skill.VitalityRegenerationResistanceRate);
                updateCommand.Parameters.AddWithValue("@accuracy_rate", skill.AccuracyRate);
                updateCommand.Parameters.AddWithValue("@lifesteal_rate", skill.LifestealRate);
                updateCommand.Parameters.AddWithValue("@shield_strength", skill.ShieldStrength);
                updateCommand.Parameters.AddWithValue("@tenacity", skill.Tenacity);
                updateCommand.Parameters.AddWithValue("@resistance_rate", skill.ResistanceRate);
                updateCommand.Parameters.AddWithValue("@combo_rate", skill.ComboRate);
                updateCommand.Parameters.AddWithValue("@ignore_combo_rate", skill.IgnoreComboRate);
                updateCommand.Parameters.AddWithValue("@combo_damage_rate", skill.ComboDamageRate);
                updateCommand.Parameters.AddWithValue("@combo_resistance_rate", skill.ComboResistanceRate);
                updateCommand.Parameters.AddWithValue("@stun_rate", skill.StunRate);
                updateCommand.Parameters.AddWithValue("@ignore_stun_rate", skill.IgnoreStunRate);
                updateCommand.Parameters.AddWithValue("@reflection_rate", skill.ReflectionRate);
                updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", skill.IgnoreReflectionRate);
                updateCommand.Parameters.AddWithValue("@reflection_damage_rate", skill.ReflectionDamageRate);
                updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", skill.ReflectionResistanceRate);
                updateCommand.Parameters.AddWithValue("@mana", skill.Mana);
                updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", skill.ManaRegenerationRate);
                updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", skill.DamageToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", skill.ResistanceToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", skill.DamageToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", skill.ResistanceToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@normal_damage_rate", skill.NormalDamageRate);
                updateCommand.Parameters.AddWithValue("@normal_resistance_rate", skill.NormalResistanceRate);
                updateCommand.Parameters.AddWithValue("@skill_damage_rate", skill.SkillDamageRate);
                updateCommand.Parameters.AddWithValue("@skill_resistance_rate", skill.SkillResistanceRate);

                await updateCommand.ExecuteNonQueryAsync();
                return true;
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
    }
    public async Task<Skills> GetUserSkillsByIdAsync(string userId, string Id)
    {
        Skills skill = new Skills();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT * FROM user_skills WHERE skill_id = @id AND user_id = @user_id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", Id);
                selectCommand.Parameters.AddWithValue("@user_id", userId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
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
                    };
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

        return skill;
    }
    public async Task<List<Skills>> GetUserCardHeroesSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position, sp.pattern_id,
                       -- Subquery gom nhóm hiệu ứng thành JSON ngay tại dòng dữ liệu
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', se.min_value,
                                'max_value', se.max_value,
                                'trigger_phase', se.trigger_phase,
                                'trigger_condition', se.trigger_condition,
                                'is_stackable', se.is_stackable,
                                'is_removable', se.is_removable,
                                'target_id', se.target_id,
                                'effect_id', e.id,
                                'effect_name', e.name,
                                'effect_type', e.effect_type,
                                'duration', e.duration,
                                'effect_description', e.description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        )
                        FROM skill_effect se
                        LEFT JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                        LEFT JOIN effect_property_action epa ON e.id = epa.effect_id
                        LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                        LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                        WHERE se.skill_id = s.id -- Mối liên kết map ngược lại với Skill đang xét ở bảng ngoài
                    ) AS skill_effects_json
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_heroes_skills chs
                    ON chs.skill_id = us.skill_id AND chs.skill_id = @skill_id
                LEFT JOIN skill_patterns sp ON s.id = sp.skill_id
                WHERE us.user_id = @userId";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@skill_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rarity = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
                        Experience = reader.GetDoubleSafe("experience"),
                        Quantity = reader.GetIntSafe("quantity"),
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

                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe("pattern_id")
                        }
                    };

                    string effectsJson = reader.GetStringSafe("skill_effects_json");

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                foreach (var item in pendingJsonList)
                {
                    try
                    {
                        item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                        item.skill.Effects = new List<Effects>(); // Fallback an toàn
                    }
                }

                // Load Effects cho toàn bộ Skills
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardCaptainsSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position, sp.pattern_id,
                       -- Subquery gom nhóm hiệu ứng thành JSON ngay tại dòng dữ liệu
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', se.min_value,
                                'max_value', se.max_value,
                                'trigger_phase', se.trigger_phase,
                                'trigger_condition', se.trigger_condition,
                                'is_stackable', se.is_stackable,
                                'is_removable', se.is_removable,
                                'target_id', se.target_id,
                                'effect_id', e.id,
                                'effect_name', e.name,
                                'effect_type', e.effect_type,
                                'duration', e.duration,
                                'effect_description', e.description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        )
                        FROM skill_effect se
                        LEFT JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                        LEFT JOIN effect_property_action epa ON e.id = epa.effect_id
                        LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                        LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                        WHERE se.skill_id = s.id -- Mối liên kết map ngược lại với Skill đang xét ở bảng ngoài
                    ) AS skill_effects_json
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_captains_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_captain_id = @card_captain_id
                LEFT JOIN skill_patterns sp ON s.id = sp.skill_id
                WHERE us.user_id = @userId";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_captain_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rarity = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
                        Experience = reader.GetDoubleSafe("experience"),
                        Quantity = reader.GetIntSafe("quantity"),
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

                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe("pattern_id")
                        }
                    };

                    string effectsJson = reader.GetStringSafe("skill_effects_json");

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                foreach (var item in pendingJsonList)
                {
                    try
                    {
                        item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                        item.skill.Effects = new List<Effects>(); // Fallback an toàn
                    }
                }

                // Load Effects cho toàn bộ Skills
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardColonelsSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position, sp.pattern_id,
                       -- Subquery gom nhóm hiệu ứng thành JSON ngay tại dòng dữ liệu
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', se.min_value,
                                'max_value', se.max_value,
                                'trigger_phase', se.trigger_phase,
                                'trigger_condition', se.trigger_condition,
                                'is_stackable', se.is_stackable,
                                'is_removable', se.is_removable,
                                'target_id', se.target_id,
                                'effect_id', e.id,
                                'effect_name', e.name,
                                'effect_type', e.effect_type,
                                'duration', e.duration,
                                'effect_description', e.description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        )
                        FROM skill_effect se
                        LEFT JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                        LEFT JOIN effect_property_action epa ON e.id = epa.effect_id
                        LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                        LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                        WHERE se.skill_id = s.id -- Mối liên kết map ngược lại với Skill đang xét ở bảng ngoài
                    ) AS skill_effects_json
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_colonels_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_colonel_id = @card_colonel_id
                LEFT JOIN skill_patterns sp ON s.id = sp.skill_id
                WHERE us.user_id = @userId";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_colonel_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rarity = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
                        Experience = reader.GetDoubleSafe("experience"),
                        Quantity = reader.GetIntSafe("quantity"),
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

                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe("pattern_id")
                        }
                    };

                    string effectsJson = reader.GetStringSafe("skill_effects_json");

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                foreach (var item in pendingJsonList)
                {
                    try
                    {
                        item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                        item.skill.Effects = new List<Effects>(); // Fallback an toàn
                    }
                }

                // Load Effects cho toàn bộ Skills
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardGeneralsSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position, sp.pattern_id,
                       -- Subquery gom nhóm hiệu ứng thành JSON ngay tại dòng dữ liệu
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', se.min_value,
                                'max_value', se.max_value,
                                'trigger_phase', se.trigger_phase,
                                'trigger_condition', se.trigger_condition,
                                'is_stackable', se.is_stackable,
                                'is_removable', se.is_removable,
                                'target_id', se.target_id,
                                'effect_id', e.id,
                                'effect_name', e.name,
                                'effect_type', e.effect_type,
                                'duration', e.duration,
                                'effect_description', e.description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        )
                        FROM skill_effect se
                        LEFT JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                        LEFT JOIN effect_property_action epa ON e.id = epa.effect_id
                        LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                        LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                        WHERE se.skill_id = s.id -- Mối liên kết map ngược lại với Skill đang xét ở bảng ngoài
                    ) AS skill_effects_json
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_generals_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_general_id = @card_general_id
                LEFT JOIN skill_patterns sp ON s.id = sp.skill_id
                WHERE us.user_id = @userId";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_general_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rarity = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
                        Experience = reader.GetDoubleSafe("experience"),
                        Quantity = reader.GetIntSafe("quantity"),
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

                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe("pattern_id")
                        }
                    };

                    string effectsJson = reader.GetStringSafe("skill_effects_json");

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                foreach (var item in pendingJsonList)
                {
                    try
                    {
                        item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                        item.skill.Effects = new List<Effects>(); // Fallback an toàn
                    }
                }

                // Load Effects cho toàn bộ Skills
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardAdmiralsSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position, sp.pattern_id,
                       -- Subquery gom nhóm hiệu ứng thành JSON ngay tại dòng dữ liệu
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', se.min_value,
                                'max_value', se.max_value,
                                'trigger_phase', se.trigger_phase,
                                'trigger_condition', se.trigger_condition,
                                'is_stackable', se.is_stackable,
                                'is_removable', se.is_removable,
                                'target_id', se.target_id,
                                'effect_id', e.id,
                                'effect_name', e.name,
                                'effect_type', e.effect_type,
                                'duration', e.duration,
                                'effect_description', e.description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        )
                        FROM skill_effect se
                        LEFT JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                        LEFT JOIN effect_property_action epa ON e.id = epa.effect_id
                        LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                        LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                        WHERE se.skill_id = s.id -- Mối liên kết map ngược lại với Skill đang xét ở bảng ngoài
                    ) AS skill_effects_json
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_admirals_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_admiral_id = @card_admiral_id
                LEFT JOIN skill_patterns sp ON s.id = sp.skill_id
                WHERE us.user_id = @userId";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_admiral_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rarity = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
                        Experience = reader.GetDoubleSafe("experience"),
                        Quantity = reader.GetIntSafe("quantity"),
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

                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe("pattern_id")
                        }
                    };

                    string effectsJson = reader.GetStringSafe("skill_effects_json");

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                foreach (var item in pendingJsonList)
                {
                    try
                    {
                        item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                        item.skill.Effects = new List<Effects>(); // Fallback an toàn
                    }
                }

                // Load Effects cho toàn bộ Skills đã lấy
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardMilitariesSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position, sp.pattern_id,
                       -- Subquery gom nhóm hiệu ứng thành JSON ngay tại dòng dữ liệu
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', se.min_value,
                                'max_value', se.max_value,
                                'trigger_phase', se.trigger_phase,
                                'trigger_condition', se.trigger_condition,
                                'is_stackable', se.is_stackable,
                                'is_removable', se.is_removable,
                                'target_id', se.target_id,
                                'effect_id', e.id,
                                'effect_name', e.name,
                                'effect_type', e.effect_type,
                                'duration', e.duration,
                                'effect_description', e.description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        )
                        FROM skill_effect se
                        LEFT JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                        LEFT JOIN effect_property_action epa ON e.id = epa.effect_id
                        LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                        LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                        WHERE se.skill_id = s.id -- Mối liên kết map ngược lại với Skill đang xét ở bảng ngoài
                    ) AS skill_effects_json
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_militaries_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_military_id = @card_military_id
                LEFT JOIN skill_patterns sp ON s.id = sp.skill_id
                WHERE us.user_id = @userId";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_military_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rarity = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
                        Experience = reader.GetDoubleSafe("experience"),
                        Quantity = reader.GetIntSafe("quantity"),
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

                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe("pattern_id")
                        }
                    };

                    string effectsJson = reader.GetStringSafe("skill_effects_json");

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                foreach (var item in pendingJsonList)
                {
                    try
                    {
                        item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                        item.skill.Effects = new List<Effects>(); // Fallback an toàn
                    }
                }

                // Load Effects cho toàn bộ Skills đã lấy
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardMonstersSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position, sp.pattern_id,
                       -- Subquery gom nhóm hiệu ứng thành JSON ngay tại dòng dữ liệu
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', se.min_value,
                                'max_value', se.max_value,
                                'trigger_phase', se.trigger_phase,
                                'trigger_condition', se.trigger_condition,
                                'is_stackable', se.is_stackable,
                                'is_removable', se.is_removable,
                                'target_id', se.target_id,
                                'effect_id', e.id,
                                'effect_name', e.name,
                                'effect_type', e.effect_type,
                                'duration', e.duration,
                                'effect_description', e.description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        )
                        FROM skill_effect se
                        LEFT JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                        LEFT JOIN effect_property_action epa ON e.id = epa.effect_id
                        LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                        LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                        WHERE se.skill_id = s.id -- Mối liên kết map ngược lại với Skill đang xét ở bảng ngoài
                    ) AS skill_effects_json
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_monsters_skills chs
                    ON chs.skill_id = us.skill_id AND chs.card_monster_id = @card_monster_id
                LEFT JOIN skill_patterns sp ON s.id = sp.skill_id
                WHERE us.user_id = @userId";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_monster_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rarity = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
                        Experience = reader.GetDoubleSafe("experience"),
                        Quantity = reader.GetIntSafe("quantity"),
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

                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe("pattern_id")
                        }
                    };

                    string effectsJson = reader.GetStringSafe("skill_effects_json");

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                foreach (var item in pendingJsonList)
                {
                    try
                    {
                        item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                        item.skill.Effects = new List<Effects>(); // Fallback an toàn
                    }
                }

                // Load Effects cho toàn bộ Skills đã lấy
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardSpellsSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position, sp.pattern_id,
                       -- Subquery gom nhóm hiệu ứng thành JSON ngay tại dòng dữ liệu
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', se.min_value,
                                'max_value', se.max_value,
                                'trigger_phase', se.trigger_phase,
                                'trigger_condition', se.trigger_condition,
                                'is_stackable', se.is_stackable,
                                'is_removable', se.is_removable,
                                'target_id', se.target_id,
                                'effect_id', e.id,
                                'effect_name', e.name,
                                'effect_type', e.effect_type,
                                'duration', e.duration,
                                'effect_description', e.description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        )
                        FROM skill_effect se
                        LEFT JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                        LEFT JOIN effect_property_action epa ON e.id = epa.effect_id
                        LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                        LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                        WHERE se.skill_id = s.id -- Mối liên kết map ngược lại với Skill đang xét ở bảng ngoài
                    ) AS skill_effects_json
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_spells_skills chs 
                    ON chs.skill_id = us.skill_id AND chs.card_spell_id = @card_spell_id
                LEFT JOIN skill_patterns sp ON s.id = sp.skill_id
                WHERE us.user_id = @userId";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_spell_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rarity = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
                        Experience = reader.GetDoubleSafe("experience"),
                        Quantity = reader.GetIntSafe("quantity"),
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

                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe("pattern_id")
                        }
                    };

                    string effectsJson = reader.GetStringSafe("skill_effects_json");

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                foreach (var item in pendingJsonList)
                {
                    try
                    {
                        item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                        item.skill.Effects = new List<Effects>(); // Fallback an toàn
                    }
                }

                // Load Effects cho toàn bộ Skills đã lấy
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardSoldiersSkillsAsync(string userId, string cardId)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, 
                       IFNULL(chs.position, 0) AS position, sp.pattern_id,
                       -- Subquery gom nhóm hiệu ứng thành JSON ngay tại dòng dữ liệu
                    (
                        SELECT JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', se.min_value,
                                'max_value', se.max_value,
                                'trigger_phase', se.trigger_phase,
                                'trigger_condition', se.trigger_condition,
                                'is_stackable', se.is_stackable,
                                'is_removable', se.is_removable,
                                'target_id', se.target_id,
                                'effect_id', e.id,
                                'effect_name', e.name,
                                'effect_type', e.effect_type,
                                'duration', e.duration,
                                'effect_description', e.description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        )
                        FROM skill_effect se
                        LEFT JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                        LEFT JOIN effect_property_action epa ON e.id = epa.effect_id
                        LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                        LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                        WHERE se.skill_id = s.id -- Mối liên kết map ngược lại với Skill đang xét ở bảng ngoài
                    ) AS skill_effects_json
                FROM Skills s
                JOIN user_skills us ON s.id = us.skill_id
                LEFT JOIN card_soldiers_skills chs 
                    ON chs.skill_id = us.skill_id AND chs.card_soldier_id = @card_soldier_id
                LEFT JOIN skill_patterns sp ON s.id = sp.skill_id
                WHERE us.user_id = @userId";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);
                selectCommand.Parameters.AddWithValue("@card_soldier_id", cardId);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe("skill_id"),
                        Name = reader.GetStringSafe("name"),
                        Image = reader.GetStringSafe("image"),
                        Rarity = reader.GetStringSafe("rare"),
                        Quality = reader.GetDoubleSafe("quality"),
                        Type = reader.GetStringSafe("type"),
                        Star = reader.GetIntSafe("star"),
                        Level = reader.GetIntSafe("level"),
                        Position = reader.GetIntSafe("position"),
                        SkillType = reader.GetStringSafe("skill_type"),
                        Experience = reader.GetDoubleSafe("experience"),
                        Quantity = reader.GetIntSafe("quantity"),
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

                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe("pattern_id")
                        }
                    };

                    string effectsJson = reader.GetStringSafe("skill_effects_json");

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                foreach (var item in pendingJsonList)
                {
                    try
                    {
                        item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                        item.skill.Effects = new List<Effects>(); // Fallback an toàn
                    }
                }

                // Load Effects cho toàn bộ Skills đã lấy
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
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

        return skills;
    }
    public async Task<List<Skills>> GetUserCardHeroesSkillsAsync(string userId, List<string> cardHeroIds)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();

        // Kiểm tra danh sách đầu vào để tránh lỗi SQL khi danh sách rỗng
        if (cardHeroIds == null || cardHeroIds.Count == 0)
        {
            return skills;
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // 1. Tạo danh sách tham số dạng @heroId0, @heroId1,... động để tránh SQL Injection
                var paramNames = new List<string>();
                for (int i = 0; i < cardHeroIds.Count; i++)
                {
                    paramNames.Add($"@cardHeroId{i}");
                }

                // Nối các tên tham số lại thành chuỗi: "@heroId0, @heroId1, @heroId2"
                string inClause = string.Join(", ", paramNames);

                // 2. Cập nhật lại SQL Query với điều kiện IN danh sách các Hero ID
                string selectSQL = $@"
                WITH TargetSkills AS (
                    -- Bước 1: Lấy danh sách ID skill duy nhất của các Card cần tìm
                    SELECT DISTINCT us.skill_id
                    FROM user_skills us
                    JOIN card_heroes_skills chs ON chs.skill_id = us.skill_id
                    WHERE us.user_id = @userId
                    AND chs.card_hero_id IN ({inClause})
                ),
                BaseEffects AS (
                    -- Bước 2: Lấy thông tin cơ bản của Effect trước để tránh nhân dòng chéo
                    SELECT 
                        se.skill_id,
                        se.effect_id,
                        se.min_value,
                        se.max_value,
                        se.trigger_phase,
                        se.trigger_condition,
                        se.is_stackable,
                        se.is_removable,
                        se.target_id,
                        e.name AS effect_name,
                        e.effect_type,
                        e.duration,
                        e.description AS effect_description
                    FROM skill_effect se
                    JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                    WHERE se.skill_id IN (SELECT skill_id FROM TargetSkills)
                ),
                AggregatedEffects AS (
                    -- Bước 3: Gom nhóm JSON. Lúc này dữ liệu đã gọn, không bị nhân dòng ảo
                    SELECT 
                        be.skill_id,
                        JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', be.min_value,
                                'max_value', be.max_value,
                                'trigger_phase', be.trigger_phase,
                                'trigger_condition', be.trigger_condition,
                                'is_stackable', be.is_stackable,
                                'is_removable', be.is_removable,
                                'target_id', be.target_id,
                                'effect_id', be.effect_id,
                                'effect_name', be.effect_name,
                                'effect_type', be.effect_type,
                                'duration', be.duration,
                                'effect_description', be.effect_description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        ) AS skill_effects_json
                    FROM BaseEffects be
                    LEFT JOIN effect_property_action epa ON be.effect_id = epa.effect_id
                    LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                    LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                    GROUP BY be.skill_id
                )
                -- Bước 4: Xuất dữ liệu cuối cùng phẳng, gom gọn theo card_hero_id
                SELECT 
                    us.skill_id,
                    s.name, 
                    s.image, 
                    us.rare, 
                    us.star, 
                    us.level, 
                    us.experience, 
                    us.quality, 
                    us.quantity,
                    s.type,
                    s.skill_type,
                    s.skill_sub_type,
                    chs.position AS skill_position, 
                    sp.pattern_id, 
                    chs.card_hero_id,
                    ae.skill_effects_json
                FROM card_heroes_skills chs
                JOIN user_skills us ON chs.skill_id = us.skill_id AND us.user_id = @userId
                JOIN Skills s ON chs.skill_id = s.id
                LEFT JOIN skill_patterns sp ON chs.skill_id = sp.skill_id
                LEFT JOIN AggregatedEffects ae ON chs.skill_id = ae.skill_id
                WHERE chs.card_hero_id IN ({inClause});";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);

                // Gán tham số userId cố định
                selectCommand.Parameters.AddWithValue("@userId", userId);

                // Gán các tham số động từ danh sách mảng heroIds
                for (int i = 0; i < cardHeroIds.Count; i++)
                {
                    selectCommand.Parameters.AddWithValue(paramNames[i], cardHeroIds[i]);
                }

                // 3. Thực thi và đọc dữ liệu
                await using var reader = await selectCommand.ExecuteReaderAsync();

                // --- MẸO TỐI ƯU ---
                // Đọc trước cấu trúc các cột trả về và lưu tên cột viết thường (lowercase) vào một Dictionary mapping Index.
                // Điều này giúp driver ADO.NET (MySqlConnector) tìm kiếm trực tiếp bằng bộ nhớ cache cực nhanh bên trong thay vì quét chuỗi.
                var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnMap[reader.GetName(i)] = i;
                }

                // Hàm Helper nội bộ (Local Function) để lấy nhanh tên cột chuẩn từ database nhằm tránh việc driver đi quét chuỗi liên tục.
                // Hàm này sẽ trả về đúng tên cột gốc (đã được cache vị trí) giúp tối ưu hóa 100% cho các hàm GetXXXSafe của bạn.
                string GetCol(string columnName)
                {
                    return columnMap.TryGetValue(columnName, out int index) ? reader.GetName(index) : columnName;
                }

                // ==========================================
                // 1. LẤY VÀ CACHE SẴN INDEX CỦA TẤT CẢ CÁC CỘT (Chỉ chạy đúng 1 lần trước vòng lặp)
                // ==========================================
                int colSkillId = reader.GetOrdinal(GetCol("skill_id"));
                int colName = reader.GetOrdinal(GetCol("name"));
                int colImage = reader.GetOrdinal(GetCol("image"));
                int colRare = reader.GetOrdinal(GetCol("rare"));
                int colQuality = reader.GetOrdinal(GetCol("quality"));
                int colType = reader.GetOrdinal(GetCol("type"));
                int colStar = reader.GetOrdinal(GetCol("star"));
                int colLevel = reader.GetOrdinal(GetCol("level"));
                int colSkillPosition = reader.GetOrdinal(GetCol("skill_position"));
                int colSkillType = reader.GetOrdinal(GetCol("skill_type"));
                int colExperience = reader.GetOrdinal(GetCol("experience"));
                int colQuantity = reader.GetOrdinal(GetCol("quantity"));
                // int colPower = reader.GetOrdinal(GetCol("power"));
                // int colHealth = reader.GetOrdinal(GetCol("health"));
                // int colPhysicalAttack = reader.GetOrdinal(GetCol("physical_attack"));
                // int colPhysicalDefense = reader.GetOrdinal(GetCol("physical_defense"));
                // int colMagicalAttack = reader.GetOrdinal(GetCol("magical_attack"));
                // int colMagicalDefense = reader.GetOrdinal(GetCol("magical_defense"));
                // int colChemicalAttack = reader.GetOrdinal(GetCol("chemical_attack"));
                // int colChemicalDefense = reader.GetOrdinal(GetCol("chemical_defense"));
                // int colAtomicAttack = reader.GetOrdinal(GetCol("atomic_attack"));
                // int colAtomicDefense = reader.GetOrdinal(GetCol("atomic_defense"));
                // int colMentalAttack = reader.GetOrdinal(GetCol("mental_attack"));
                // int colMentalDefense = reader.GetOrdinal(GetCol("mental_defense"));
                // int colSpeed = reader.GetOrdinal(GetCol("speed"));
                // int colCriticalDamageRate = reader.GetOrdinal(GetCol("critical_damage_rate"));
                // int colCriticalRate = reader.GetOrdinal(GetCol("critical_rate"));
                // int colCriticalResistanceRate = reader.GetOrdinal(GetCol("critical_resistance_rate"));
                // int colIgnoreCriticalRate = reader.GetOrdinal(GetCol("ignore_critical_rate"));
                // int colPenetrationRate = reader.GetOrdinal(GetCol("penetration_rate"));
                // int colPenetrationResistanceRate = reader.GetOrdinal(GetCol("penetration_resistance_rate"));
                // int colEvasionRate = reader.GetOrdinal(GetCol("evasion_rate"));
                // int colDamageAbsorptionRate = reader.GetOrdinal(GetCol("damage_absorption_rate"));
                // int colIgnoreDamageAbsorptionRate = reader.GetOrdinal(GetCol("ignore_damage_absorption_rate"));
                // int colAbsorbedDamageRate = reader.GetOrdinal(GetCol("absorbed_damage_rate"));
                // int colVitalityRegenerationRate = reader.GetOrdinal(GetCol("vitality_regeneration_rate"));
                // int colVitalityRegenerationResistanceRate = reader.GetOrdinal(GetCol("vitality_regeneration_resistance_rate"));
                // int colAccuracyRate = reader.GetOrdinal(GetCol("accuracy_rate"));
                // int colLifestealRate = reader.GetOrdinal(GetCol("lifesteal_rate"));
                // int colShieldStrength = reader.GetOrdinal(GetCol("shield_strength"));
                // int colTenacity = reader.GetOrdinal(GetCol("tenacity"));
                // int colResistanceRate = reader.GetOrdinal(GetCol("resistance_rate"));
                // int colComboRate = reader.GetOrdinal(GetCol("combo_rate"));
                // int colIgnoreComboRate = reader.GetOrdinal(GetCol("ignore_combo_rate"));
                // int colComboDamageRate = reader.GetOrdinal(GetCol("combo_damage_rate"));
                // int colComboResistanceRate = reader.GetOrdinal(GetCol("combo_resistance_rate"));
                // int colStunRate = reader.GetOrdinal(GetCol("stun_rate"));
                // int colIgnoreStunRate = reader.GetOrdinal(GetCol("ignore_stun_rate"));
                // int colReflectionRate = reader.GetOrdinal(GetCol("reflection_rate"));
                // int colIgnoreReflectionRate = reader.GetOrdinal(GetCol("ignore_reflection_rate"));
                // int colReflectionDamageRate = reader.GetOrdinal(GetCol("reflection_damage_rate"));
                // int colReflectionResistanceRate = reader.GetOrdinal(GetCol("reflection_resistance_rate"));
                // int colMana = reader.GetOrdinal(GetCol("mana"));
                // int colManaRegenerationRate = reader.GetOrdinal(GetCol("mana_regeneration_rate"));
                // int colDamageToDifferentFactionRate = reader.GetOrdinal(GetCol("damage_to_different_faction_rate"));
                // int colResistanceToDifferentFactionRate = reader.GetOrdinal(GetCol("resistance_to_different_faction_rate"));
                // int colDamageToSameFactionRate = reader.GetOrdinal(GetCol("damage_to_same_faction_rate"));
                // int colResistanceToSameFactionRate = reader.GetOrdinal(GetCol("resistance_to_same_faction_rate"));
                // int colNormalDamageRate = reader.GetOrdinal(GetCol("normal_damage_rate"));
                // int colNormalResistanceRate = reader.GetOrdinal(GetCol("normal_resistance_rate"));
                // int colSkillDamageRate = reader.GetOrdinal(GetCol("skill_damage_rate"));
                // int colSkillResistanceRate = reader.GetOrdinal(GetCol("skill_resistance_rate"));
                // int colDescription = reader.GetOrdinal(GetCol("description"));
                int colCardHeroId = reader.GetOrdinal(GetCol("card_hero_id"));
                int colPatternId = reader.GetOrdinal(GetCol("pattern_id"));
                int colSkillSubType = reader.GetOrdinal(GetCol("skill_sub_type"));
                int colSkillEffectsJson = reader.GetOrdinal(GetCol("skill_effects_json"));

                // ==========================================
                // 2. VÒNG LẶP ĐỌC DỮ LIỆU CỰC NHANH VỚI INDEX (ORDINAL)
                // ==========================================
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe(colSkillId),
                        Name = reader.GetStringSafe(colName),
                        Image = reader.GetStringSafe(colImage),
                        Rarity = reader.GetStringSafe(colRare),
                        Quality = reader.GetDoubleSafe(colQuality),
                        Type = reader.GetStringSafe(colType),
                        Star = reader.GetIntSafe(colStar),
                        Level = reader.GetIntSafe(colLevel),
                        Position = reader.GetIntSafe(colSkillPosition),
                        SkillType = reader.GetStringSafe(colSkillType),
                        Experience = reader.GetDoubleSafe(colExperience),
                        Quantity = reader.GetIntSafe(colQuantity),
                        // Power = reader.GetDoubleSafe(colPower),
                        // Health = reader.GetDoubleSafe(colHealth),
                        // PhysicalAttack = reader.GetDoubleSafe(colPhysicalAttack),
                        // PhysicalDefense = reader.GetDoubleSafe(colPhysicalDefense),
                        // MagicalAttack = reader.GetDoubleSafe(colMagicalAttack),
                        // MagicalDefense = reader.GetDoubleSafe(colMagicalDefense),
                        // ChemicalAttack = reader.GetDoubleSafe(colChemicalAttack),
                        // ChemicalDefense = reader.GetDoubleSafe(colChemicalDefense),
                        // AtomicAttack = reader.GetDoubleSafe(colAtomicAttack),
                        // AtomicDefense = reader.GetDoubleSafe(colAtomicDefense),
                        // MentalAttack = reader.GetDoubleSafe(colMentalAttack),
                        // MentalDefense = reader.GetDoubleSafe(colMentalDefense),
                        // Speed = reader.GetDoubleSafe(colSpeed),
                        // CriticalDamageRate = reader.GetDoubleSafe(colCriticalDamageRate),
                        // CriticalRate = reader.GetDoubleSafe(colCriticalRate),
                        // CriticalResistanceRate = reader.GetDoubleSafe(colCriticalResistanceRate),
                        // IgnoreCriticalRate = reader.GetDoubleSafe(colIgnoreCriticalRate),
                        // PenetrationRate = reader.GetDoubleSafe(colPenetrationRate),
                        // PenetrationResistanceRate = reader.GetDoubleSafe(colPenetrationResistanceRate),
                        // EvasionRate = reader.GetDoubleSafe(colEvasionRate),
                        // DamageAbsorptionRate = reader.GetDoubleSafe(colDamageAbsorptionRate),
                        // IgnoreDamageAbsorptionRate = reader.GetDoubleSafe(colIgnoreDamageAbsorptionRate),
                        // AbsorbedDamageRate = reader.GetDoubleSafe(colAbsorbedDamageRate),
                        // VitalityRegenerationRate = reader.GetDoubleSafe(colVitalityRegenerationRate),
                        // VitalityRegenerationResistanceRate = reader.GetDoubleSafe(colVitalityRegenerationResistanceRate),
                        // AccuracyRate = reader.GetDoubleSafe(colAccuracyRate),
                        // LifestealRate = reader.GetDoubleSafe(colLifestealRate),
                        // ShieldStrength = reader.GetDoubleSafe(colShieldStrength),
                        // Tenacity = reader.GetDoubleSafe(colTenacity),
                        // ResistanceRate = reader.GetDoubleSafe(colResistanceRate),
                        // ComboRate = reader.GetDoubleSafe(colComboRate),
                        // IgnoreComboRate = reader.GetDoubleSafe(colIgnoreComboRate),
                        // ComboDamageRate = reader.GetDoubleSafe(colComboDamageRate),
                        // ComboResistanceRate = reader.GetDoubleSafe(colComboResistanceRate),
                        // StunRate = reader.GetDoubleSafe(colStunRate),
                        // IgnoreStunRate = reader.GetDoubleSafe(colIgnoreStunRate),
                        // ReflectionRate = reader.GetDoubleSafe(colReflectionRate),
                        // IgnoreReflectionRate = reader.GetDoubleSafe(colIgnoreReflectionRate),
                        // ReflectionDamageRate = reader.GetDoubleSafe(colReflectionDamageRate),
                        // ReflectionResistanceRate = reader.GetDoubleSafe(colReflectionResistanceRate),
                        // Mana = reader.GetDoubleSafe(colMana),
                        // ManaRegenerationRate = reader.GetDoubleSafe(colManaRegenerationRate),
                        // DamageToDifferentFactionRate = reader.GetDoubleSafe(colDamageToDifferentFactionRate),
                        // ResistanceToDifferentFactionRate = reader.GetDoubleSafe(colResistanceToDifferentFactionRate),
                        // DamageToSameFactionRate = reader.GetDoubleSafe(colDamageToSameFactionRate),
                        // ResistanceToSameFactionRate = reader.GetDoubleSafe(colResistanceToSameFactionRate),
                        // NormalDamageRate = reader.GetDoubleSafe(colNormalDamageRate),
                        // NormalResistanceRate = reader.GetDoubleSafe(colNormalResistanceRate),
                        // SkillDamageRate = reader.GetDoubleSafe(colSkillDamageRate),
                        // SkillResistanceRate = reader.GetDoubleSafe(colSkillResistanceRate),
                        // Description = reader.GetStringSafe(colDescription),

                        CardId = reader.GetStringSafe(colCardHeroId),
                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe(colPatternId)
                        },
                        SkillSubType = new SkillSubTypes
                        {
                            SubTypeCode = reader.GetStringSafe(colSkillSubType)
                        }
                    };

                    string effectsJson = reader.GetStringSafe(colSkillEffectsJson);

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                // Đóng reader sớm để giải phóng tài nguyên mạng trước khi CPU thực hiện giải mã JSON
                await reader.CloseAsync();

                // ==========================================
                // TỐI ƯU HÓA: GIẢI MÃ JSON SONG SONG BẰNG JSONHELPER CỦA BẠN
                // ==========================================
                if (pendingJsonList.Count > 0)
                {
                    Parallel.ForEach(pendingJsonList, item =>
                    {
                        try
                        {
                            item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                            item.skill.Effects = new List<Effects>(); // Fallback an toàn
                        }
                    });
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return skills;
    }
    public async Task<List<Skills>> GetUserCardCaptainsSkillsAsync(string userId, List<string> cardCaptainIds)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();

        // Kiểm tra danh sách đầu vào để tránh lỗi SQL khi danh sách rỗng
        if (cardCaptainIds == null || cardCaptainIds.Count == 0)
        {
            return skills;
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // 1. Tạo danh sách tham số dạng @heroId0, @heroId1,... động để tránh SQL Injection
                var paramNames = new List<string>();
                for (int i = 0; i < cardCaptainIds.Count; i++)
                {
                    paramNames.Add($"@cardCaptainId{i}");
                }

                // Nối các tên tham số lại thành chuỗi: "@heroId0, @heroId1, @heroId2"
                string inClause = string.Join(", ", paramNames);

                // 2. Cập nhật lại SQL Query với điều kiện IN danh sách các Hero ID
                // Sửa lại logic JOIN chính xác: chs.card_hero_id IN (...) thay vì gán nhầm vào skill_id
                string selectSQL = $@"
                WITH TargetSkills AS (
                    -- Bước 1: Lấy danh sách ID skill duy nhất của các Card cần tìm
                    SELECT DISTINCT us.skill_id
                    FROM user_skills us
                    JOIN card_captains_skills chs ON chs.skill_id = us.skill_id
                    WHERE us.user_id = @userId
                    AND chs.card_captain_id IN ({inClause})
                ),
                BaseEffects AS (
                    -- Bước 2: Lấy thông tin cơ bản của Effect trước để tránh nhân dòng chéo
                    SELECT 
                        se.skill_id,
                        se.effect_id,
                        se.min_value,
                        se.max_value,
                        se.trigger_phase,
                        se.trigger_condition,
                        se.is_stackable,
                        se.is_removable,
                        se.target_id,
                        e.name AS effect_name,
                        e.effect_type,
                        e.duration,
                        e.description AS effect_description
                    FROM skill_effect se
                    JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                    WHERE se.skill_id IN (SELECT skill_id FROM TargetSkills)
                ),
                AggregatedEffects AS (
                    -- Bước 3: Gom nhóm JSON. Lúc này dữ liệu đã gọn, không bị nhân dòng ảo
                    SELECT 
                        be.skill_id,
                        JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', be.min_value,
                                'max_value', be.max_value,
                                'trigger_phase', be.trigger_phase,
                                'trigger_condition', be.trigger_condition,
                                'is_stackable', be.is_stackable,
                                'is_removable', be.is_removable,
                                'target_id', be.target_id,
                                'effect_id', be.effect_id,
                                'effect_name', be.effect_name,
                                'effect_type', be.effect_type,
                                'duration', be.duration,
                                'effect_description', be.effect_description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        ) AS skill_effects_json
                    FROM BaseEffects be
                    LEFT JOIN effect_property_action epa ON be.effect_id = epa.effect_id
                    LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                    LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                    GROUP BY be.skill_id
                )
                -- Bước 4: Xuất dữ liệu cuối cùng phẳng, gom gọn theo card_captain_id
                SELECT 
                    us.skill_id,
                    s.name, 
                    s.image, 
                    us.rare, 
                    us.star, 
                    us.level, 
                    us.experience, 
                    us.quality, 
                    us.quantity,
                    s.type,
                    s.skill_type,
                    s.skill_sub_type,
                    chs.position AS skill_position, 
                    sp.pattern_id, 
                    chs.card_captain_id,
                    ae.skill_effects_json
                FROM card_captains_skills chs
                JOIN user_skills us ON chs.skill_id = us.skill_id AND us.user_id = @userId
                JOIN Skills s ON chs.skill_id = s.id
                LEFT JOIN skill_patterns sp ON chs.skill_id = sp.skill_id
                LEFT JOIN AggregatedEffects ae ON chs.skill_id = ae.skill_id
                WHERE chs.card_captain_id IN ({inClause});";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);

                // Gán tham số userId cố định
                selectCommand.Parameters.AddWithValue("@userId", userId);

                // Gán các tham số động từ danh sách mảng heroIds
                for (int i = 0; i < cardCaptainIds.Count; i++)
                {
                    selectCommand.Parameters.AddWithValue(paramNames[i], cardCaptainIds[i]);
                }

                // 3. Thực thi và đọc dữ liệu
                await using var reader = await selectCommand.ExecuteReaderAsync();

                // --- MẸO TỐI ƯU ---
                // Đọc trước cấu trúc các cột trả về và lưu tên cột viết thường (lowercase) vào một Dictionary mapping Index.
                // Điều này giúp driver ADO.NET (MySqlConnector) tìm kiếm trực tiếp bằng bộ nhớ cache cực nhanh bên trong thay vì quét chuỗi.
                var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnMap[reader.GetName(i)] = i;
                }

                // Hàm Helper nội bộ (Local Function) để lấy nhanh tên cột chuẩn từ database nhằm tránh việc driver đi quét chuỗi liên tục.
                // Hàm này sẽ trả về đúng tên cột gốc (đã được cache vị trí) giúp tối ưu hóa 100% cho các hàm GetXXXSafe của bạn.
                string GetCol(string columnName)
                {
                    return columnMap.TryGetValue(columnName, out int index) ? reader.GetName(index) : columnName;
                }

                // ==========================================
                // 1. LẤY VÀ CACHE SẴN INDEX CỦA TẤT CẢ CÁC CỘT (Chỉ chạy đúng 1 lần trước vòng lặp)
                // ==========================================
                int colSkillId = reader.GetOrdinal(GetCol("skill_id"));
                int colName = reader.GetOrdinal(GetCol("name"));
                int colImage = reader.GetOrdinal(GetCol("image"));
                int colRare = reader.GetOrdinal(GetCol("rare"));
                int colQuality = reader.GetOrdinal(GetCol("quality"));
                int colType = reader.GetOrdinal(GetCol("type"));
                int colStar = reader.GetOrdinal(GetCol("star"));
                int colLevel = reader.GetOrdinal(GetCol("level"));
                int colSkillPosition = reader.GetOrdinal(GetCol("skill_position"));
                int colSkillType = reader.GetOrdinal(GetCol("skill_type"));
                int colExperience = reader.GetOrdinal(GetCol("experience"));
                int colQuantity = reader.GetOrdinal(GetCol("quantity"));
                // int colPower = reader.GetOrdinal(GetCol("power"));
                // int colHealth = reader.GetOrdinal(GetCol("health"));
                // int colPhysicalAttack = reader.GetOrdinal(GetCol("physical_attack"));
                // int colPhysicalDefense = reader.GetOrdinal(GetCol("physical_defense"));
                // int colMagicalAttack = reader.GetOrdinal(GetCol("magical_attack"));
                // int colMagicalDefense = reader.GetOrdinal(GetCol("magical_defense"));
                // int colChemicalAttack = reader.GetOrdinal(GetCol("chemical_attack"));
                // int colChemicalDefense = reader.GetOrdinal(GetCol("chemical_defense"));
                // int colAtomicAttack = reader.GetOrdinal(GetCol("atomic_attack"));
                // int colAtomicDefense = reader.GetOrdinal(GetCol("atomic_defense"));
                // int colMentalAttack = reader.GetOrdinal(GetCol("mental_attack"));
                // int colMentalDefense = reader.GetOrdinal(GetCol("mental_defense"));
                // int colSpeed = reader.GetOrdinal(GetCol("speed"));
                // int colCriticalDamageRate = reader.GetOrdinal(GetCol("critical_damage_rate"));
                // int colCriticalRate = reader.GetOrdinal(GetCol("critical_rate"));
                // int colCriticalResistanceRate = reader.GetOrdinal(GetCol("critical_resistance_rate"));
                // int colIgnoreCriticalRate = reader.GetOrdinal(GetCol("ignore_critical_rate"));
                // int colPenetrationRate = reader.GetOrdinal(GetCol("penetration_rate"));
                // int colPenetrationResistanceRate = reader.GetOrdinal(GetCol("penetration_resistance_rate"));
                // int colEvasionRate = reader.GetOrdinal(GetCol("evasion_rate"));
                // int colDamageAbsorptionRate = reader.GetOrdinal(GetCol("damage_absorption_rate"));
                // int colIgnoreDamageAbsorptionRate = reader.GetOrdinal(GetCol("ignore_damage_absorption_rate"));
                // int colAbsorbedDamageRate = reader.GetOrdinal(GetCol("absorbed_damage_rate"));
                // int colVitalityRegenerationRate = reader.GetOrdinal(GetCol("vitality_regeneration_rate"));
                // int colVitalityRegenerationResistanceRate = reader.GetOrdinal(GetCol("vitality_regeneration_resistance_rate"));
                // int colAccuracyRate = reader.GetOrdinal(GetCol("accuracy_rate"));
                // int colLifestealRate = reader.GetOrdinal(GetCol("lifesteal_rate"));
                // int colShieldStrength = reader.GetOrdinal(GetCol("shield_strength"));
                // int colTenacity = reader.GetOrdinal(GetCol("tenacity"));
                // int colResistanceRate = reader.GetOrdinal(GetCol("resistance_rate"));
                // int colComboRate = reader.GetOrdinal(GetCol("combo_rate"));
                // int colIgnoreComboRate = reader.GetOrdinal(GetCol("ignore_combo_rate"));
                // int colComboDamageRate = reader.GetOrdinal(GetCol("combo_damage_rate"));
                // int colComboResistanceRate = reader.GetOrdinal(GetCol("combo_resistance_rate"));
                // int colStunRate = reader.GetOrdinal(GetCol("stun_rate"));
                // int colIgnoreStunRate = reader.GetOrdinal(GetCol("ignore_stun_rate"));
                // int colReflectionRate = reader.GetOrdinal(GetCol("reflection_rate"));
                // int colIgnoreReflectionRate = reader.GetOrdinal(GetCol("ignore_reflection_rate"));
                // int colReflectionDamageRate = reader.GetOrdinal(GetCol("reflection_damage_rate"));
                // int colReflectionResistanceRate = reader.GetOrdinal(GetCol("reflection_resistance_rate"));
                // int colMana = reader.GetOrdinal(GetCol("mana"));
                // int colManaRegenerationRate = reader.GetOrdinal(GetCol("mana_regeneration_rate"));
                // int colDamageToDifferentFactionRate = reader.GetOrdinal(GetCol("damage_to_different_faction_rate"));
                // int colResistanceToDifferentFactionRate = reader.GetOrdinal(GetCol("resistance_to_different_faction_rate"));
                // int colDamageToSameFactionRate = reader.GetOrdinal(GetCol("damage_to_same_faction_rate"));
                // int colResistanceToSameFactionRate = reader.GetOrdinal(GetCol("resistance_to_same_faction_rate"));
                // int colNormalDamageRate = reader.GetOrdinal(GetCol("normal_damage_rate"));
                // int colNormalResistanceRate = reader.GetOrdinal(GetCol("normal_resistance_rate"));
                // int colSkillDamageRate = reader.GetOrdinal(GetCol("skill_damage_rate"));
                // int colSkillResistanceRate = reader.GetOrdinal(GetCol("skill_resistance_rate"));
                // int colDescription = reader.GetOrdinal(GetCol("description"));
                int colCardCaptainId = reader.GetOrdinal(GetCol("card_captain_id"));
                int colPatternId = reader.GetOrdinal(GetCol("pattern_id"));
                int colSkillSubType = reader.GetOrdinal(GetCol("skill_sub_type"));
                int colSkillEffectsJson = reader.GetOrdinal(GetCol("skill_effects_json"));

                // ==========================================
                // 2. VÒNG LẶP ĐỌC DỮ LIỆU CỰC NHANH VỚI INDEX (ORDINAL)
                // ==========================================
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe(colSkillId),
                        Name = reader.GetStringSafe(colName),
                        Image = reader.GetStringSafe(colImage),
                        Rarity = reader.GetStringSafe(colRare),
                        Quality = reader.GetDoubleSafe(colQuality),
                        Type = reader.GetStringSafe(colType),
                        Star = reader.GetIntSafe(colStar),
                        Level = reader.GetIntSafe(colLevel),
                        Position = reader.GetIntSafe(colSkillPosition),
                        SkillType = reader.GetStringSafe(colSkillType),
                        Experience = reader.GetDoubleSafe(colExperience),
                        Quantity = reader.GetIntSafe(colQuantity),
                        // Power = reader.GetDoubleSafe(colPower),
                        // Health = reader.GetDoubleSafe(colHealth),
                        // PhysicalAttack = reader.GetDoubleSafe(colPhysicalAttack),
                        // PhysicalDefense = reader.GetDoubleSafe(colPhysicalDefense),
                        // MagicalAttack = reader.GetDoubleSafe(colMagicalAttack),
                        // MagicalDefense = reader.GetDoubleSafe(colMagicalDefense),
                        // ChemicalAttack = reader.GetDoubleSafe(colChemicalAttack),
                        // ChemicalDefense = reader.GetDoubleSafe(colChemicalDefense),
                        // AtomicAttack = reader.GetDoubleSafe(colAtomicAttack),
                        // AtomicDefense = reader.GetDoubleSafe(colAtomicDefense),
                        // MentalAttack = reader.GetDoubleSafe(colMentalAttack),
                        // MentalDefense = reader.GetDoubleSafe(colMentalDefense),
                        // Speed = reader.GetDoubleSafe(colSpeed),
                        // CriticalDamageRate = reader.GetDoubleSafe(colCriticalDamageRate),
                        // CriticalRate = reader.GetDoubleSafe(colCriticalRate),
                        // CriticalResistanceRate = reader.GetDoubleSafe(colCriticalResistanceRate),
                        // IgnoreCriticalRate = reader.GetDoubleSafe(colIgnoreCriticalRate),
                        // PenetrationRate = reader.GetDoubleSafe(colPenetrationRate),
                        // PenetrationResistanceRate = reader.GetDoubleSafe(colPenetrationResistanceRate),
                        // EvasionRate = reader.GetDoubleSafe(colEvasionRate),
                        // DamageAbsorptionRate = reader.GetDoubleSafe(colDamageAbsorptionRate),
                        // IgnoreDamageAbsorptionRate = reader.GetDoubleSafe(colIgnoreDamageAbsorptionRate),
                        // AbsorbedDamageRate = reader.GetDoubleSafe(colAbsorbedDamageRate),
                        // VitalityRegenerationRate = reader.GetDoubleSafe(colVitalityRegenerationRate),
                        // VitalityRegenerationResistanceRate = reader.GetDoubleSafe(colVitalityRegenerationResistanceRate),
                        // AccuracyRate = reader.GetDoubleSafe(colAccuracyRate),
                        // LifestealRate = reader.GetDoubleSafe(colLifestealRate),
                        // ShieldStrength = reader.GetDoubleSafe(colShieldStrength),
                        // Tenacity = reader.GetDoubleSafe(colTenacity),
                        // ResistanceRate = reader.GetDoubleSafe(colResistanceRate),
                        // ComboRate = reader.GetDoubleSafe(colComboRate),
                        // IgnoreComboRate = reader.GetDoubleSafe(colIgnoreComboRate),
                        // ComboDamageRate = reader.GetDoubleSafe(colComboDamageRate),
                        // ComboResistanceRate = reader.GetDoubleSafe(colComboResistanceRate),
                        // StunRate = reader.GetDoubleSafe(colStunRate),
                        // IgnoreStunRate = reader.GetDoubleSafe(colIgnoreStunRate),
                        // ReflectionRate = reader.GetDoubleSafe(colReflectionRate),
                        // IgnoreReflectionRate = reader.GetDoubleSafe(colIgnoreReflectionRate),
                        // ReflectionDamageRate = reader.GetDoubleSafe(colReflectionDamageRate),
                        // ReflectionResistanceRate = reader.GetDoubleSafe(colReflectionResistanceRate),
                        // Mana = reader.GetDoubleSafe(colMana),
                        // ManaRegenerationRate = reader.GetDoubleSafe(colManaRegenerationRate),
                        // DamageToDifferentFactionRate = reader.GetDoubleSafe(colDamageToDifferentFactionRate),
                        // ResistanceToDifferentFactionRate = reader.GetDoubleSafe(colResistanceToDifferentFactionRate),
                        // DamageToSameFactionRate = reader.GetDoubleSafe(colDamageToSameFactionRate),
                        // ResistanceToSameFactionRate = reader.GetDoubleSafe(colResistanceToSameFactionRate),
                        // NormalDamageRate = reader.GetDoubleSafe(colNormalDamageRate),
                        // NormalResistanceRate = reader.GetDoubleSafe(colNormalResistanceRate),
                        // SkillDamageRate = reader.GetDoubleSafe(colSkillDamageRate),
                        // SkillResistanceRate = reader.GetDoubleSafe(colSkillResistanceRate),
                        // Description = reader.GetStringSafe(colDescription),

                        CardId = reader.GetStringSafe(colCardCaptainId),
                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe(colPatternId)
                        },
                        SkillSubType = new SkillSubTypes
                        {
                            SubTypeCode = reader.GetStringSafe(colSkillSubType)
                        }
                    };

                    string effectsJson = reader.GetStringSafe(colSkillEffectsJson);

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                // Đóng reader sớm để giải phóng tài nguyên mạng trước khi CPU thực hiện giải mã JSON
                await reader.CloseAsync();

                // ==========================================
                // TỐI ƯU HÓA: GIẢI MÃ JSON SONG SONG BẰNG JSONHELPER CỦA BẠN
                // ==========================================
                if (pendingJsonList.Count > 0)
                {
                    Parallel.ForEach(pendingJsonList, item =>
                    {
                        try
                        {
                            item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                            item.skill.Effects = new List<Effects>(); // Fallback an toàn
                        }
                    });
                }

                // Load Effects cho toàn bộ Skills cùng một lúc
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            // block 'finally' không cần CloseAsync() thủ công nữa vì 'await using' đã tự động xử lý giải phóng kết nối một cách an toàn.
        }

        return skills;
    }
    public async Task<List<Skills>> GetUserCardColonelsSkillsAsync(string userId, List<string> cardColonelIds)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();

        // Kiểm tra danh sách đầu vào để tránh lỗi SQL khi danh sách rỗng
        if (cardColonelIds == null || cardColonelIds.Count == 0)
        {
            return skills;
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // 1. Tạo danh sách tham số dạng @heroId0, @heroId1,... động để tránh SQL Injection
                var paramNames = new List<string>();
                for (int i = 0; i < cardColonelIds.Count; i++)
                {
                    paramNames.Add($"@cardColonelId{i}");
                }

                // Nối các tên tham số lại thành chuỗi: "@heroId0, @heroId1, @heroId2"
                string inClause = string.Join(", ", paramNames);

                // 2. Cập nhật lại SQL Query với điều kiện IN danh sách các Hero ID
                // Sửa lại logic JOIN chính xác: chs.card_hero_id IN (...) thay vì gán nhầm vào skill_id
                string selectSQL = $@"
                WITH TargetSkills AS (
                    -- Bước 1: Lấy danh sách ID skill duy nhất của các Card cần tìm
                    SELECT DISTINCT us.skill_id
                    FROM user_skills us
                    JOIN card_colonels_skills chs ON chs.skill_id = us.skill_id
                    WHERE us.user_id = @userId
                    AND chs.card_colonel_id IN ({inClause})
                ),
                BaseEffects AS (
                    -- Bước 2: Lấy thông tin cơ bản của Effect trước để tránh nhân dòng chéo
                    SELECT 
                        se.skill_id,
                        se.effect_id,
                        se.min_value,
                        se.max_value,
                        se.trigger_phase,
                        se.trigger_condition,
                        se.is_stackable,
                        se.is_removable,
                        se.target_id,
                        e.name AS effect_name,
                        e.effect_type,
                        e.duration,
                        e.description AS effect_description
                    FROM skill_effect se
                    JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                    WHERE se.skill_id IN (SELECT skill_id FROM TargetSkills)
                ),
                AggregatedEffects AS (
                    -- Bước 3: Gom nhóm JSON. Lúc này dữ liệu đã gọn, không bị nhân dòng ảo
                    SELECT 
                        be.skill_id,
                        JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', be.min_value,
                                'max_value', be.max_value,
                                'trigger_phase', be.trigger_phase,
                                'trigger_condition', be.trigger_condition,
                                'is_stackable', be.is_stackable,
                                'is_removable', be.is_removable,
                                'target_id', be.target_id,
                                'effect_id', be.effect_id,
                                'effect_name', be.effect_name,
                                'effect_type', be.effect_type,
                                'duration', be.duration,
                                'effect_description', be.effect_description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        ) AS skill_effects_json
                    FROM BaseEffects be
                    LEFT JOIN effect_property_action epa ON be.effect_id = epa.effect_id
                    LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                    LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                    GROUP BY be.skill_id
                )
                -- Bước 4: Xuất dữ liệu cuối cùng phẳng, gom gọn theo card_colonel_id
                SELECT 
                    us.skill_id,
                    s.name, 
                    s.image, 
                    us.rare, 
                    us.star, 
                    us.level, 
                    us.experience, 
                    us.quality, 
                    us.quantity,
                    s.type,
                    s.skill_type,
                    s.skill_sub_type,
                    chs.position AS skill_position, 
                    sp.pattern_id, 
                    chs.card_colonel_id,
                    ae.skill_effects_json
                FROM card_colonels_skills chs
                JOIN user_skills us ON chs.skill_id = us.skill_id AND us.user_id = @userId
                JOIN Skills s ON chs.skill_id = s.id
                LEFT JOIN skill_patterns sp ON chs.skill_id = sp.skill_id
                LEFT JOIN AggregatedEffects ae ON chs.skill_id = ae.skill_id
                WHERE chs.card_colonel_id IN ({inClause});";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);

                // Gán tham số userId cố định
                selectCommand.Parameters.AddWithValue("@userId", userId);

                // Gán các tham số động từ danh sách mảng heroIds
                for (int i = 0; i < cardColonelIds.Count; i++)
                {
                    selectCommand.Parameters.AddWithValue(paramNames[i], cardColonelIds[i]);
                }

                // 3. Thực thi và đọc dữ liệu
                await using var reader = await selectCommand.ExecuteReaderAsync();

                // --- MẸO TỐI ƯU ---
                // Đọc trước cấu trúc các cột trả về và lưu tên cột viết thường (lowercase) vào một Dictionary mapping Index.
                // Điều này giúp driver ADO.NET (MySqlConnector) tìm kiếm trực tiếp bằng bộ nhớ cache cực nhanh bên trong thay vì quét chuỗi.
                var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnMap[reader.GetName(i)] = i;
                }

                // Hàm Helper nội bộ (Local Function) để lấy nhanh tên cột chuẩn từ database nhằm tránh việc driver đi quét chuỗi liên tục.
                // Hàm này sẽ trả về đúng tên cột gốc (đã được cache vị trí) giúp tối ưu hóa 100% cho các hàm GetXXXSafe của bạn.
                string GetCol(string columnName)
                {
                    return columnMap.TryGetValue(columnName, out int index) ? reader.GetName(index) : columnName;
                }

                // ==========================================
                // 1. LẤY VÀ CACHE SẴN INDEX CỦA TẤT CẢ CÁC CỘT (Chỉ chạy đúng 1 lần trước vòng lặp)
                // ==========================================
                int colSkillId = reader.GetOrdinal(GetCol("skill_id"));
                int colName = reader.GetOrdinal(GetCol("name"));
                int colImage = reader.GetOrdinal(GetCol("image"));
                int colRare = reader.GetOrdinal(GetCol("rare"));
                int colQuality = reader.GetOrdinal(GetCol("quality"));
                int colType = reader.GetOrdinal(GetCol("type"));
                int colStar = reader.GetOrdinal(GetCol("star"));
                int colLevel = reader.GetOrdinal(GetCol("level"));
                int colSkillPosition = reader.GetOrdinal(GetCol("skill_position"));
                int colSkillType = reader.GetOrdinal(GetCol("skill_type"));
                int colExperience = reader.GetOrdinal(GetCol("experience"));
                int colQuantity = reader.GetOrdinal(GetCol("quantity"));
                // int colPower = reader.GetOrdinal(GetCol("power"));
                // int colHealth = reader.GetOrdinal(GetCol("health"));
                // int colPhysicalAttack = reader.GetOrdinal(GetCol("physical_attack"));
                // int colPhysicalDefense = reader.GetOrdinal(GetCol("physical_defense"));
                // int colMagicalAttack = reader.GetOrdinal(GetCol("magical_attack"));
                // int colMagicalDefense = reader.GetOrdinal(GetCol("magical_defense"));
                // int colChemicalAttack = reader.GetOrdinal(GetCol("chemical_attack"));
                // int colChemicalDefense = reader.GetOrdinal(GetCol("chemical_defense"));
                // int colAtomicAttack = reader.GetOrdinal(GetCol("atomic_attack"));
                // int colAtomicDefense = reader.GetOrdinal(GetCol("atomic_defense"));
                // int colMentalAttack = reader.GetOrdinal(GetCol("mental_attack"));
                // int colMentalDefense = reader.GetOrdinal(GetCol("mental_defense"));
                // int colSpeed = reader.GetOrdinal(GetCol("speed"));
                // int colCriticalDamageRate = reader.GetOrdinal(GetCol("critical_damage_rate"));
                // int colCriticalRate = reader.GetOrdinal(GetCol("critical_rate"));
                // int colCriticalResistanceRate = reader.GetOrdinal(GetCol("critical_resistance_rate"));
                // int colIgnoreCriticalRate = reader.GetOrdinal(GetCol("ignore_critical_rate"));
                // int colPenetrationRate = reader.GetOrdinal(GetCol("penetration_rate"));
                // int colPenetrationResistanceRate = reader.GetOrdinal(GetCol("penetration_resistance_rate"));
                // int colEvasionRate = reader.GetOrdinal(GetCol("evasion_rate"));
                // int colDamageAbsorptionRate = reader.GetOrdinal(GetCol("damage_absorption_rate"));
                // int colIgnoreDamageAbsorptionRate = reader.GetOrdinal(GetCol("ignore_damage_absorption_rate"));
                // int colAbsorbedDamageRate = reader.GetOrdinal(GetCol("absorbed_damage_rate"));
                // int colVitalityRegenerationRate = reader.GetOrdinal(GetCol("vitality_regeneration_rate"));
                // int colVitalityRegenerationResistanceRate = reader.GetOrdinal(GetCol("vitality_regeneration_resistance_rate"));
                // int colAccuracyRate = reader.GetOrdinal(GetCol("accuracy_rate"));
                // int colLifestealRate = reader.GetOrdinal(GetCol("lifesteal_rate"));
                // int colShieldStrength = reader.GetOrdinal(GetCol("shield_strength"));
                // int colTenacity = reader.GetOrdinal(GetCol("tenacity"));
                // int colResistanceRate = reader.GetOrdinal(GetCol("resistance_rate"));
                // int colComboRate = reader.GetOrdinal(GetCol("combo_rate"));
                // int colIgnoreComboRate = reader.GetOrdinal(GetCol("ignore_combo_rate"));
                // int colComboDamageRate = reader.GetOrdinal(GetCol("combo_damage_rate"));
                // int colComboResistanceRate = reader.GetOrdinal(GetCol("combo_resistance_rate"));
                // int colStunRate = reader.GetOrdinal(GetCol("stun_rate"));
                // int colIgnoreStunRate = reader.GetOrdinal(GetCol("ignore_stun_rate"));
                // int colReflectionRate = reader.GetOrdinal(GetCol("reflection_rate"));
                // int colIgnoreReflectionRate = reader.GetOrdinal(GetCol("ignore_reflection_rate"));
                // int colReflectionDamageRate = reader.GetOrdinal(GetCol("reflection_damage_rate"));
                // int colReflectionResistanceRate = reader.GetOrdinal(GetCol("reflection_resistance_rate"));
                // int colMana = reader.GetOrdinal(GetCol("mana"));
                // int colManaRegenerationRate = reader.GetOrdinal(GetCol("mana_regeneration_rate"));
                // int colDamageToDifferentFactionRate = reader.GetOrdinal(GetCol("damage_to_different_faction_rate"));
                // int colResistanceToDifferentFactionRate = reader.GetOrdinal(GetCol("resistance_to_different_faction_rate"));
                // int colDamageToSameFactionRate = reader.GetOrdinal(GetCol("damage_to_same_faction_rate"));
                // int colResistanceToSameFactionRate = reader.GetOrdinal(GetCol("resistance_to_same_faction_rate"));
                // int colNormalDamageRate = reader.GetOrdinal(GetCol("normal_damage_rate"));
                // int colNormalResistanceRate = reader.GetOrdinal(GetCol("normal_resistance_rate"));
                // int colSkillDamageRate = reader.GetOrdinal(GetCol("skill_damage_rate"));
                // int colSkillResistanceRate = reader.GetOrdinal(GetCol("skill_resistance_rate"));
                // int colDescription = reader.GetOrdinal(GetCol("description"));
                int colCardColonelId = reader.GetOrdinal(GetCol("card_colonel_id"));
                int colPatternId = reader.GetOrdinal(GetCol("pattern_id"));
                int colSkillSubType = reader.GetOrdinal(GetCol("skill_sub_type"));
                int colSkillEffectsJson = reader.GetOrdinal(GetCol("skill_effects_json"));

                // ==========================================
                // 2. VÒNG LẶP ĐỌC DỮ LIỆU CỰC NHANH VỚI INDEX (ORDINAL)
                // ==========================================
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe(colSkillId),
                        Name = reader.GetStringSafe(colName),
                        Image = reader.GetStringSafe(colImage),
                        Rarity = reader.GetStringSafe(colRare),
                        Quality = reader.GetDoubleSafe(colQuality),
                        Type = reader.GetStringSafe(colType),
                        Star = reader.GetIntSafe(colStar),
                        Level = reader.GetIntSafe(colLevel),
                        Position = reader.GetIntSafe(colSkillPosition),
                        SkillType = reader.GetStringSafe(colSkillType),
                        Experience = reader.GetDoubleSafe(colExperience),
                        Quantity = reader.GetIntSafe(colQuantity),
                        // Power = reader.GetDoubleSafe(colPower),
                        // Health = reader.GetDoubleSafe(colHealth),
                        // PhysicalAttack = reader.GetDoubleSafe(colPhysicalAttack),
                        // PhysicalDefense = reader.GetDoubleSafe(colPhysicalDefense),
                        // MagicalAttack = reader.GetDoubleSafe(colMagicalAttack),
                        // MagicalDefense = reader.GetDoubleSafe(colMagicalDefense),
                        // ChemicalAttack = reader.GetDoubleSafe(colChemicalAttack),
                        // ChemicalDefense = reader.GetDoubleSafe(colChemicalDefense),
                        // AtomicAttack = reader.GetDoubleSafe(colAtomicAttack),
                        // AtomicDefense = reader.GetDoubleSafe(colAtomicDefense),
                        // MentalAttack = reader.GetDoubleSafe(colMentalAttack),
                        // MentalDefense = reader.GetDoubleSafe(colMentalDefense),
                        // Speed = reader.GetDoubleSafe(colSpeed),
                        // CriticalDamageRate = reader.GetDoubleSafe(colCriticalDamageRate),
                        // CriticalRate = reader.GetDoubleSafe(colCriticalRate),
                        // CriticalResistanceRate = reader.GetDoubleSafe(colCriticalResistanceRate),
                        // IgnoreCriticalRate = reader.GetDoubleSafe(colIgnoreCriticalRate),
                        // PenetrationRate = reader.GetDoubleSafe(colPenetrationRate),
                        // PenetrationResistanceRate = reader.GetDoubleSafe(colPenetrationResistanceRate),
                        // EvasionRate = reader.GetDoubleSafe(colEvasionRate),
                        // DamageAbsorptionRate = reader.GetDoubleSafe(colDamageAbsorptionRate),
                        // IgnoreDamageAbsorptionRate = reader.GetDoubleSafe(colIgnoreDamageAbsorptionRate),
                        // AbsorbedDamageRate = reader.GetDoubleSafe(colAbsorbedDamageRate),
                        // VitalityRegenerationRate = reader.GetDoubleSafe(colVitalityRegenerationRate),
                        // VitalityRegenerationResistanceRate = reader.GetDoubleSafe(colVitalityRegenerationResistanceRate),
                        // AccuracyRate = reader.GetDoubleSafe(colAccuracyRate),
                        // LifestealRate = reader.GetDoubleSafe(colLifestealRate),
                        // ShieldStrength = reader.GetDoubleSafe(colShieldStrength),
                        // Tenacity = reader.GetDoubleSafe(colTenacity),
                        // ResistanceRate = reader.GetDoubleSafe(colResistanceRate),
                        // ComboRate = reader.GetDoubleSafe(colComboRate),
                        // IgnoreComboRate = reader.GetDoubleSafe(colIgnoreComboRate),
                        // ComboDamageRate = reader.GetDoubleSafe(colComboDamageRate),
                        // ComboResistanceRate = reader.GetDoubleSafe(colComboResistanceRate),
                        // StunRate = reader.GetDoubleSafe(colStunRate),
                        // IgnoreStunRate = reader.GetDoubleSafe(colIgnoreStunRate),
                        // ReflectionRate = reader.GetDoubleSafe(colReflectionRate),
                        // IgnoreReflectionRate = reader.GetDoubleSafe(colIgnoreReflectionRate),
                        // ReflectionDamageRate = reader.GetDoubleSafe(colReflectionDamageRate),
                        // ReflectionResistanceRate = reader.GetDoubleSafe(colReflectionResistanceRate),
                        // Mana = reader.GetDoubleSafe(colMana),
                        // ManaRegenerationRate = reader.GetDoubleSafe(colManaRegenerationRate),
                        // DamageToDifferentFactionRate = reader.GetDoubleSafe(colDamageToDifferentFactionRate),
                        // ResistanceToDifferentFactionRate = reader.GetDoubleSafe(colResistanceToDifferentFactionRate),
                        // DamageToSameFactionRate = reader.GetDoubleSafe(colDamageToSameFactionRate),
                        // ResistanceToSameFactionRate = reader.GetDoubleSafe(colResistanceToSameFactionRate),
                        // NormalDamageRate = reader.GetDoubleSafe(colNormalDamageRate),
                        // NormalResistanceRate = reader.GetDoubleSafe(colNormalResistanceRate),
                        // SkillDamageRate = reader.GetDoubleSafe(colSkillDamageRate),
                        // SkillResistanceRate = reader.GetDoubleSafe(colSkillResistanceRate),
                        // Description = reader.GetStringSafe(colDescription),

                        CardId = reader.GetStringSafe(colCardColonelId),
                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe(colPatternId)
                        },
                        SkillSubType = new SkillSubTypes
                        {
                            SubTypeCode = reader.GetStringSafe(colSkillSubType)
                        }
                    };

                    string effectsJson = reader.GetStringSafe(colSkillEffectsJson);

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                // Đóng reader sớm để giải phóng tài nguyên mạng trước khi CPU thực hiện giải mã JSON
                await reader.CloseAsync();

                // ==========================================
                // TỐI ƯU HÓA: GIẢI MÃ JSON SONG SONG BẰNG JSONHELPER CỦA BẠN
                // ==========================================
                if (pendingJsonList.Count > 0)
                {
                    Parallel.ForEach(pendingJsonList, item =>
                    {
                        try
                        {
                            item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                            item.skill.Effects = new List<Effects>(); // Fallback an toàn
                        }
                    });
                }

                // Load Effects cho toàn bộ Skills cùng một lúc
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            // block 'finally' không cần CloseAsync() thủ công nữa vì 'await using' đã tự động xử lý giải phóng kết nối một cách an toàn.
        }

        return skills;
    }
    public async Task<List<Skills>> GetUserCardGeneralsSkillsAsync(string userId, List<string> cardGeneralIds)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();

        // Kiểm tra danh sách đầu vào để tránh lỗi SQL khi danh sách rỗng
        if (cardGeneralIds == null || cardGeneralIds.Count == 0)
        {
            return skills;
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // 1. Tạo danh sách tham số dạng @heroId0, @heroId1,... động để tránh SQL Injection
                var paramNames = new List<string>();
                for (int i = 0; i < cardGeneralIds.Count; i++)
                {
                    paramNames.Add($"@cardGeneralId{i}");
                }

                // Nối các tên tham số lại thành chuỗi: "@heroId0, @heroId1, @heroId2"
                string inClause = string.Join(", ", paramNames);

                // 2. Cập nhật lại SQL Query với điều kiện IN danh sách các Hero ID
                // Sửa lại logic JOIN chính xác: chs.card_hero_id IN (...) thay vì gán nhầm vào skill_id
                string selectSQL = $@"
                WITH TargetSkills AS (
                    -- Bước 1: Lấy danh sách ID skill duy nhất của các Card cần tìm
                    SELECT DISTINCT us.skill_id
                    FROM user_skills us
                    JOIN card_generals_skills chs ON chs.skill_id = us.skill_id
                    WHERE us.user_id = @userId
                    AND chs.card_general_id IN ({inClause})
                ),
                BaseEffects AS (
                    -- Bước 2: Lấy thông tin cơ bản của Effect trước để tránh nhân dòng chéo
                    SELECT 
                        se.skill_id,
                        se.effect_id,
                        se.min_value,
                        se.max_value,
                        se.trigger_phase,
                        se.trigger_condition,
                        se.is_stackable,
                        se.is_removable,
                        se.target_id,
                        e.name AS effect_name,
                        e.effect_type,
                        e.duration,
                        e.description AS effect_description
                    FROM skill_effect se
                    JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                    WHERE se.skill_id IN (SELECT skill_id FROM TargetSkills)
                ),
                AggregatedEffects AS (
                    -- Bước 3: Gom nhóm JSON. Lúc này dữ liệu đã gọn, không bị nhân dòng ảo
                    SELECT 
                        be.skill_id,
                        JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', be.min_value,
                                'max_value', be.max_value,
                                'trigger_phase', be.trigger_phase,
                                'trigger_condition', be.trigger_condition,
                                'is_stackable', be.is_stackable,
                                'is_removable', be.is_removable,
                                'target_id', be.target_id,
                                'effect_id', be.effect_id,
                                'effect_name', be.effect_name,
                                'effect_type', be.effect_type,
                                'duration', be.duration,
                                'effect_description', be.effect_description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        ) AS skill_effects_json
                    FROM BaseEffects be
                    LEFT JOIN effect_property_action epa ON be.effect_id = epa.effect_id
                    LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                    LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                    GROUP BY be.skill_id
                )
                -- Bước 4: Xuất dữ liệu cuối cùng phẳng, gom gọn theo card_general_id
                SELECT 
                    us.skill_id,
                    s.name, 
                    s.image, 
                    us.rare, 
                    us.star, 
                    us.level, 
                    us.experience, 
                    us.quality,
                    us.quantity, 
                    s.type,
                    s.skill_type,
                    s.skill_sub_type,
                    chs.position AS skill_position, 
                    sp.pattern_id, 
                    chs.card_general_id,
                    ae.skill_effects_json
                FROM card_generals_skills chs
                JOIN user_skills us ON chs.skill_id = us.skill_id AND us.user_id = @userId
                JOIN Skills s ON chs.skill_id = s.id
                LEFT JOIN skill_patterns sp ON chs.skill_id = sp.skill_id
                LEFT JOIN AggregatedEffects ae ON chs.skill_id = ae.skill_id
                WHERE chs.card_general_id IN ({inClause});";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);

                // Gán tham số userId cố định
                selectCommand.Parameters.AddWithValue("@userId", userId);

                // Gán các tham số động từ danh sách mảng heroIds
                for (int i = 0; i < cardGeneralIds.Count; i++)
                {
                    selectCommand.Parameters.AddWithValue(paramNames[i], cardGeneralIds[i]);
                }

                // 3. Thực thi và đọc dữ liệu
                await using var reader = await selectCommand.ExecuteReaderAsync();

                // --- MẸO TỐI ƯU ---
                // Đọc trước cấu trúc các cột trả về và lưu tên cột viết thường (lowercase) vào một Dictionary mapping Index.
                // Điều này giúp driver ADO.NET (MySqlConnector) tìm kiếm trực tiếp bằng bộ nhớ cache cực nhanh bên trong thay vì quét chuỗi.
                var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnMap[reader.GetName(i)] = i;
                }

                // Hàm Helper nội bộ (Local Function) để lấy nhanh tên cột chuẩn từ database nhằm tránh việc driver đi quét chuỗi liên tục.
                // Hàm này sẽ trả về đúng tên cột gốc (đã được cache vị trí) giúp tối ưu hóa 100% cho các hàm GetXXXSafe của bạn.
                string GetCol(string columnName)
                {
                    return columnMap.TryGetValue(columnName, out int index) ? reader.GetName(index) : columnName;
                }

                // ==========================================
                // 1. LẤY VÀ CACHE SẴN INDEX CỦA TẤT CẢ CÁC CỘT (Chỉ chạy đúng 1 lần trước vòng lặp)
                // ==========================================
                int colSkillId = reader.GetOrdinal(GetCol("skill_id"));
                int colName = reader.GetOrdinal(GetCol("name"));
                int colImage = reader.GetOrdinal(GetCol("image"));
                int colRare = reader.GetOrdinal(GetCol("rare"));
                int colQuality = reader.GetOrdinal(GetCol("quality"));
                int colType = reader.GetOrdinal(GetCol("type"));
                int colStar = reader.GetOrdinal(GetCol("star"));
                int colLevel = reader.GetOrdinal(GetCol("level"));
                int colSkillPosition = reader.GetOrdinal(GetCol("skill_position"));
                int colSkillType = reader.GetOrdinal(GetCol("skill_type"));
                int colExperience = reader.GetOrdinal(GetCol("experience"));
                int colQuantity = reader.GetOrdinal(GetCol("quantity"));
                // int colPower = reader.GetOrdinal(GetCol("power"));
                // int colHealth = reader.GetOrdinal(GetCol("health"));
                // int colPhysicalAttack = reader.GetOrdinal(GetCol("physical_attack"));
                // int colPhysicalDefense = reader.GetOrdinal(GetCol("physical_defense"));
                // int colMagicalAttack = reader.GetOrdinal(GetCol("magical_attack"));
                // int colMagicalDefense = reader.GetOrdinal(GetCol("magical_defense"));
                // int colChemicalAttack = reader.GetOrdinal(GetCol("chemical_attack"));
                // int colChemicalDefense = reader.GetOrdinal(GetCol("chemical_defense"));
                // int colAtomicAttack = reader.GetOrdinal(GetCol("atomic_attack"));
                // int colAtomicDefense = reader.GetOrdinal(GetCol("atomic_defense"));
                // int colMentalAttack = reader.GetOrdinal(GetCol("mental_attack"));
                // int colMentalDefense = reader.GetOrdinal(GetCol("mental_defense"));
                // int colSpeed = reader.GetOrdinal(GetCol("speed"));
                // int colCriticalDamageRate = reader.GetOrdinal(GetCol("critical_damage_rate"));
                // int colCriticalRate = reader.GetOrdinal(GetCol("critical_rate"));
                // int colCriticalResistanceRate = reader.GetOrdinal(GetCol("critical_resistance_rate"));
                // int colIgnoreCriticalRate = reader.GetOrdinal(GetCol("ignore_critical_rate"));
                // int colPenetrationRate = reader.GetOrdinal(GetCol("penetration_rate"));
                // int colPenetrationResistanceRate = reader.GetOrdinal(GetCol("penetration_resistance_rate"));
                // int colEvasionRate = reader.GetOrdinal(GetCol("evasion_rate"));
                // int colDamageAbsorptionRate = reader.GetOrdinal(GetCol("damage_absorption_rate"));
                // int colIgnoreDamageAbsorptionRate = reader.GetOrdinal(GetCol("ignore_damage_absorption_rate"));
                // int colAbsorbedDamageRate = reader.GetOrdinal(GetCol("absorbed_damage_rate"));
                // int colVitalityRegenerationRate = reader.GetOrdinal(GetCol("vitality_regeneration_rate"));
                // int colVitalityRegenerationResistanceRate = reader.GetOrdinal(GetCol("vitality_regeneration_resistance_rate"));
                // int colAccuracyRate = reader.GetOrdinal(GetCol("accuracy_rate"));
                // int colLifestealRate = reader.GetOrdinal(GetCol("lifesteal_rate"));
                // int colShieldStrength = reader.GetOrdinal(GetCol("shield_strength"));
                // int colTenacity = reader.GetOrdinal(GetCol("tenacity"));
                // int colResistanceRate = reader.GetOrdinal(GetCol("resistance_rate"));
                // int colComboRate = reader.GetOrdinal(GetCol("combo_rate"));
                // int colIgnoreComboRate = reader.GetOrdinal(GetCol("ignore_combo_rate"));
                // int colComboDamageRate = reader.GetOrdinal(GetCol("combo_damage_rate"));
                // int colComboResistanceRate = reader.GetOrdinal(GetCol("combo_resistance_rate"));
                // int colStunRate = reader.GetOrdinal(GetCol("stun_rate"));
                // int colIgnoreStunRate = reader.GetOrdinal(GetCol("ignore_stun_rate"));
                // int colReflectionRate = reader.GetOrdinal(GetCol("reflection_rate"));
                // int colIgnoreReflectionRate = reader.GetOrdinal(GetCol("ignore_reflection_rate"));
                // int colReflectionDamageRate = reader.GetOrdinal(GetCol("reflection_damage_rate"));
                // int colReflectionResistanceRate = reader.GetOrdinal(GetCol("reflection_resistance_rate"));
                // int colMana = reader.GetOrdinal(GetCol("mana"));
                // int colManaRegenerationRate = reader.GetOrdinal(GetCol("mana_regeneration_rate"));
                // int colDamageToDifferentFactionRate = reader.GetOrdinal(GetCol("damage_to_different_faction_rate"));
                // int colResistanceToDifferentFactionRate = reader.GetOrdinal(GetCol("resistance_to_different_faction_rate"));
                // int colDamageToSameFactionRate = reader.GetOrdinal(GetCol("damage_to_same_faction_rate"));
                // int colResistanceToSameFactionRate = reader.GetOrdinal(GetCol("resistance_to_same_faction_rate"));
                // int colNormalDamageRate = reader.GetOrdinal(GetCol("normal_damage_rate"));
                // int colNormalResistanceRate = reader.GetOrdinal(GetCol("normal_resistance_rate"));
                // int colSkillDamageRate = reader.GetOrdinal(GetCol("skill_damage_rate"));
                // int colSkillResistanceRate = reader.GetOrdinal(GetCol("skill_resistance_rate"));
                // int colDescription = reader.GetOrdinal(GetCol("description"));
                int colCardGeneralId = reader.GetOrdinal(GetCol("card_general_id"));
                int colPatternId = reader.GetOrdinal(GetCol("pattern_id"));
                int colSkillSubType = reader.GetOrdinal(GetCol("skill_sub_type"));
                int colSkillEffectsJson = reader.GetOrdinal(GetCol("skill_effects_json"));

                // ==========================================
                // 2. VÒNG LẶP ĐỌC DỮ LIỆU CỰC NHANH VỚI INDEX (ORDINAL)
                // ==========================================
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe(colSkillId),
                        Name = reader.GetStringSafe(colName),
                        Image = reader.GetStringSafe(colImage),
                        Rarity = reader.GetStringSafe(colRare),
                        Quality = reader.GetDoubleSafe(colQuality),
                        Type = reader.GetStringSafe(colType),
                        Star = reader.GetIntSafe(colStar),
                        Level = reader.GetIntSafe(colLevel),
                        Position = reader.GetIntSafe(colSkillPosition),
                        SkillType = reader.GetStringSafe(colSkillType),
                        Experience = reader.GetDoubleSafe(colExperience),
                        Quantity = reader.GetIntSafe(colQuantity),
                        // Power = reader.GetDoubleSafe(colPower),
                        // Health = reader.GetDoubleSafe(colHealth),
                        // PhysicalAttack = reader.GetDoubleSafe(colPhysicalAttack),
                        // PhysicalDefense = reader.GetDoubleSafe(colPhysicalDefense),
                        // MagicalAttack = reader.GetDoubleSafe(colMagicalAttack),
                        // MagicalDefense = reader.GetDoubleSafe(colMagicalDefense),
                        // ChemicalAttack = reader.GetDoubleSafe(colChemicalAttack),
                        // ChemicalDefense = reader.GetDoubleSafe(colChemicalDefense),
                        // AtomicAttack = reader.GetDoubleSafe(colAtomicAttack),
                        // AtomicDefense = reader.GetDoubleSafe(colAtomicDefense),
                        // MentalAttack = reader.GetDoubleSafe(colMentalAttack),
                        // MentalDefense = reader.GetDoubleSafe(colMentalDefense),
                        // Speed = reader.GetDoubleSafe(colSpeed),
                        // CriticalDamageRate = reader.GetDoubleSafe(colCriticalDamageRate),
                        // CriticalRate = reader.GetDoubleSafe(colCriticalRate),
                        // CriticalResistanceRate = reader.GetDoubleSafe(colCriticalResistanceRate),
                        // IgnoreCriticalRate = reader.GetDoubleSafe(colIgnoreCriticalRate),
                        // PenetrationRate = reader.GetDoubleSafe(colPenetrationRate),
                        // PenetrationResistanceRate = reader.GetDoubleSafe(colPenetrationResistanceRate),
                        // EvasionRate = reader.GetDoubleSafe(colEvasionRate),
                        // DamageAbsorptionRate = reader.GetDoubleSafe(colDamageAbsorptionRate),
                        // IgnoreDamageAbsorptionRate = reader.GetDoubleSafe(colIgnoreDamageAbsorptionRate),
                        // AbsorbedDamageRate = reader.GetDoubleSafe(colAbsorbedDamageRate),
                        // VitalityRegenerationRate = reader.GetDoubleSafe(colVitalityRegenerationRate),
                        // VitalityRegenerationResistanceRate = reader.GetDoubleSafe(colVitalityRegenerationResistanceRate),
                        // AccuracyRate = reader.GetDoubleSafe(colAccuracyRate),
                        // LifestealRate = reader.GetDoubleSafe(colLifestealRate),
                        // ShieldStrength = reader.GetDoubleSafe(colShieldStrength),
                        // Tenacity = reader.GetDoubleSafe(colTenacity),
                        // ResistanceRate = reader.GetDoubleSafe(colResistanceRate),
                        // ComboRate = reader.GetDoubleSafe(colComboRate),
                        // IgnoreComboRate = reader.GetDoubleSafe(colIgnoreComboRate),
                        // ComboDamageRate = reader.GetDoubleSafe(colComboDamageRate),
                        // ComboResistanceRate = reader.GetDoubleSafe(colComboResistanceRate),
                        // StunRate = reader.GetDoubleSafe(colStunRate),
                        // IgnoreStunRate = reader.GetDoubleSafe(colIgnoreStunRate),
                        // ReflectionRate = reader.GetDoubleSafe(colReflectionRate),
                        // IgnoreReflectionRate = reader.GetDoubleSafe(colIgnoreReflectionRate),
                        // ReflectionDamageRate = reader.GetDoubleSafe(colReflectionDamageRate),
                        // ReflectionResistanceRate = reader.GetDoubleSafe(colReflectionResistanceRate),
                        // Mana = reader.GetDoubleSafe(colMana),
                        // ManaRegenerationRate = reader.GetDoubleSafe(colManaRegenerationRate),
                        // DamageToDifferentFactionRate = reader.GetDoubleSafe(colDamageToDifferentFactionRate),
                        // ResistanceToDifferentFactionRate = reader.GetDoubleSafe(colResistanceToDifferentFactionRate),
                        // DamageToSameFactionRate = reader.GetDoubleSafe(colDamageToSameFactionRate),
                        // ResistanceToSameFactionRate = reader.GetDoubleSafe(colResistanceToSameFactionRate),
                        // NormalDamageRate = reader.GetDoubleSafe(colNormalDamageRate),
                        // NormalResistanceRate = reader.GetDoubleSafe(colNormalResistanceRate),
                        // SkillDamageRate = reader.GetDoubleSafe(colSkillDamageRate),
                        // SkillResistanceRate = reader.GetDoubleSafe(colSkillResistanceRate),
                        // Description = reader.GetStringSafe(colDescription),

                        CardId = reader.GetStringSafe(colCardGeneralId),
                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe(colPatternId)
                        },
                        SkillSubType = new SkillSubTypes
                        {
                            SubTypeCode = reader.GetStringSafe(colSkillSubType)
                        }
                    };

                    string effectsJson = reader.GetStringSafe(colSkillEffectsJson);

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                // Đóng reader sớm để giải phóng tài nguyên mạng trước khi CPU thực hiện giải mã JSON
                await reader.CloseAsync();

                // ==========================================
                // TỐI ƯU HÓA: GIẢI MÃ JSON SONG SONG BẰNG JSONHELPER CỦA BẠN
                // ==========================================
                if (pendingJsonList.Count > 0)
                {
                    Parallel.ForEach(pendingJsonList, item =>
                    {
                        try
                        {
                            item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                            item.skill.Effects = new List<Effects>(); // Fallback an toàn
                        }
                    });
                }

                // Load Effects cho toàn bộ Skills cùng một lúc
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            // block 'finally' không cần CloseAsync() thủ công nữa vì 'await using' đã tự động xử lý giải phóng kết nối một cách an toàn.
        }

        return skills;
    }
    public async Task<List<Skills>> GetUserCardAdmiralsSkillsAsync(string userId, List<string> cardAdmiralIds)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();

        // Kiểm tra danh sách đầu vào để tránh lỗi SQL khi danh sách rỗng
        if (cardAdmiralIds == null || cardAdmiralIds.Count == 0)
        {
            return skills;
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // 1. Tạo danh sách tham số dạng @heroId0, @heroId1,... động để tránh SQL Injection
                var paramNames = new List<string>();
                for (int i = 0; i < cardAdmiralIds.Count; i++)
                {
                    paramNames.Add($"@cardAdmiralId{i}");
                }

                // Nối các tên tham số lại thành chuỗi: "@heroId0, @heroId1, @heroId2"
                string inClause = string.Join(", ", paramNames);

                // 2. Cập nhật lại SQL Query với điều kiện IN danh sách các Hero ID
                // Sửa lại logic JOIN chính xác: chs.card_hero_id IN (...) thay vì gán nhầm vào skill_id
                string selectSQL = $@"
                WITH TargetSkills AS (
                    -- Bước 1: Lấy danh sách ID skill duy nhất của các Card cần tìm
                    SELECT DISTINCT us.skill_id
                    FROM user_skills us
                    JOIN card_admirals_skills chs ON chs.skill_id = us.skill_id
                    WHERE us.user_id = @userId
                    AND chs.card_admiral_id IN ({inClause})
                ),
                BaseEffects AS (
                    -- Bước 2: Lấy thông tin cơ bản của Effect trước để tránh nhân dòng chéo
                    SELECT 
                        se.skill_id,
                        se.effect_id,
                        se.min_value,
                        se.max_value,
                        se.trigger_phase,
                        se.trigger_condition,
                        se.is_stackable,
                        se.is_removable,
                        se.target_id,
                        e.name AS effect_name,
                        e.effect_type,
                        e.duration,
                        e.description AS effect_description
                    FROM skill_effect se
                    JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                    WHERE se.skill_id IN (SELECT skill_id FROM TargetSkills)
                ),
                AggregatedEffects AS (
                    -- Bước 3: Gom nhóm JSON. Lúc này dữ liệu đã gọn, không bị nhân dòng ảo
                    SELECT 
                        be.skill_id,
                        JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', be.min_value,
                                'max_value', be.max_value,
                                'trigger_phase', be.trigger_phase,
                                'trigger_condition', be.trigger_condition,
                                'is_stackable', be.is_stackable,
                                'is_removable', be.is_removable,
                                'target_id', be.target_id,
                                'effect_id', be.effect_id,
                                'effect_name', be.effect_name,
                                'effect_type', be.effect_type,
                                'duration', be.duration,
                                'effect_description', be.effect_description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        ) AS skill_effects_json
                    FROM BaseEffects be
                    LEFT JOIN effect_property_action epa ON be.effect_id = epa.effect_id
                    LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                    LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                    GROUP BY be.skill_id
                )
                -- Bước 4: Xuất dữ liệu cuối cùng phẳng, gom gọn theo card_admiral_id
                SELECT 
                    us.skill_id,
                    s.name, 
                    s.image, 
                    us.rare, 
                    us.star, 
                    us.level, 
                    us.experience, 
                    us.quality, 
                    us.quantity,
                    s.type,
                    s.skill_type,
                    s.skill_sub_type,
                    chs.position AS skill_position, 
                    sp.pattern_id, 
                    chs.card_admiral_id,
                    ae.skill_effects_json
                FROM card_admirals_skills chs
                JOIN user_skills us ON chs.skill_id = us.skill_id AND us.user_id = @userId
                JOIN Skills s ON chs.skill_id = s.id
                LEFT JOIN skill_patterns sp ON chs.skill_id = sp.skill_id
                LEFT JOIN AggregatedEffects ae ON chs.skill_id = ae.skill_id
                WHERE chs.card_admiral_id IN ({inClause});";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);

                // Gán tham số userId cố định
                selectCommand.Parameters.AddWithValue("@userId", userId);

                // Gán các tham số động từ danh sách mảng heroIds
                for (int i = 0; i < cardAdmiralIds.Count; i++)
                {
                    selectCommand.Parameters.AddWithValue(paramNames[i], cardAdmiralIds[i]);
                }

                // 3. Thực thi và đọc dữ liệu
                await using var reader = await selectCommand.ExecuteReaderAsync();

                // --- MẸO TỐI ƯU ---
                // Đọc trước cấu trúc các cột trả về và lưu tên cột viết thường (lowercase) vào một Dictionary mapping Index.
                // Điều này giúp driver ADO.NET (MySqlConnector) tìm kiếm trực tiếp bằng bộ nhớ cache cực nhanh bên trong thay vì quét chuỗi.
                var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnMap[reader.GetName(i)] = i;
                }

                // Hàm Helper nội bộ (Local Function) để lấy nhanh tên cột chuẩn từ database nhằm tránh việc driver đi quét chuỗi liên tục.
                // Hàm này sẽ trả về đúng tên cột gốc (đã được cache vị trí) giúp tối ưu hóa 100% cho các hàm GetXXXSafe của bạn.
                string GetCol(string columnName)
                {
                    return columnMap.TryGetValue(columnName, out int index) ? reader.GetName(index) : columnName;
                }

                // ==========================================
                // 1. LẤY VÀ CACHE SẴN INDEX CỦA TẤT CẢ CÁC CỘT (Chỉ chạy đúng 1 lần trước vòng lặp)
                // ==========================================
                int colSkillId = reader.GetOrdinal(GetCol("skill_id"));
                int colName = reader.GetOrdinal(GetCol("name"));
                int colImage = reader.GetOrdinal(GetCol("image"));
                int colRare = reader.GetOrdinal(GetCol("rare"));
                int colQuality = reader.GetOrdinal(GetCol("quality"));
                int colType = reader.GetOrdinal(GetCol("type"));
                int colStar = reader.GetOrdinal(GetCol("star"));
                int colLevel = reader.GetOrdinal(GetCol("level"));
                int colSkillPosition = reader.GetOrdinal(GetCol("skill_position"));
                int colSkillType = reader.GetOrdinal(GetCol("skill_type"));
                int colExperience = reader.GetOrdinal(GetCol("experience"));
                int colQuantity = reader.GetOrdinal(GetCol("quantity"));
                // int colPower = reader.GetOrdinal(GetCol("power"));
                // int colHealth = reader.GetOrdinal(GetCol("health"));
                // int colPhysicalAttack = reader.GetOrdinal(GetCol("physical_attack"));
                // int colPhysicalDefense = reader.GetOrdinal(GetCol("physical_defense"));
                // int colMagicalAttack = reader.GetOrdinal(GetCol("magical_attack"));
                // int colMagicalDefense = reader.GetOrdinal(GetCol("magical_defense"));
                // int colChemicalAttack = reader.GetOrdinal(GetCol("chemical_attack"));
                // int colChemicalDefense = reader.GetOrdinal(GetCol("chemical_defense"));
                // int colAtomicAttack = reader.GetOrdinal(GetCol("atomic_attack"));
                // int colAtomicDefense = reader.GetOrdinal(GetCol("atomic_defense"));
                // int colMentalAttack = reader.GetOrdinal(GetCol("mental_attack"));
                // int colMentalDefense = reader.GetOrdinal(GetCol("mental_defense"));
                // int colSpeed = reader.GetOrdinal(GetCol("speed"));
                // int colCriticalDamageRate = reader.GetOrdinal(GetCol("critical_damage_rate"));
                // int colCriticalRate = reader.GetOrdinal(GetCol("critical_rate"));
                // int colCriticalResistanceRate = reader.GetOrdinal(GetCol("critical_resistance_rate"));
                // int colIgnoreCriticalRate = reader.GetOrdinal(GetCol("ignore_critical_rate"));
                // int colPenetrationRate = reader.GetOrdinal(GetCol("penetration_rate"));
                // int colPenetrationResistanceRate = reader.GetOrdinal(GetCol("penetration_resistance_rate"));
                // int colEvasionRate = reader.GetOrdinal(GetCol("evasion_rate"));
                // int colDamageAbsorptionRate = reader.GetOrdinal(GetCol("damage_absorption_rate"));
                // int colIgnoreDamageAbsorptionRate = reader.GetOrdinal(GetCol("ignore_damage_absorption_rate"));
                // int colAbsorbedDamageRate = reader.GetOrdinal(GetCol("absorbed_damage_rate"));
                // int colVitalityRegenerationRate = reader.GetOrdinal(GetCol("vitality_regeneration_rate"));
                // int colVitalityRegenerationResistanceRate = reader.GetOrdinal(GetCol("vitality_regeneration_resistance_rate"));
                // int colAccuracyRate = reader.GetOrdinal(GetCol("accuracy_rate"));
                // int colLifestealRate = reader.GetOrdinal(GetCol("lifesteal_rate"));
                // int colShieldStrength = reader.GetOrdinal(GetCol("shield_strength"));
                // int colTenacity = reader.GetOrdinal(GetCol("tenacity"));
                // int colResistanceRate = reader.GetOrdinal(GetCol("resistance_rate"));
                // int colComboRate = reader.GetOrdinal(GetCol("combo_rate"));
                // int colIgnoreComboRate = reader.GetOrdinal(GetCol("ignore_combo_rate"));
                // int colComboDamageRate = reader.GetOrdinal(GetCol("combo_damage_rate"));
                // int colComboResistanceRate = reader.GetOrdinal(GetCol("combo_resistance_rate"));
                // int colStunRate = reader.GetOrdinal(GetCol("stun_rate"));
                // int colIgnoreStunRate = reader.GetOrdinal(GetCol("ignore_stun_rate"));
                // int colReflectionRate = reader.GetOrdinal(GetCol("reflection_rate"));
                // int colIgnoreReflectionRate = reader.GetOrdinal(GetCol("ignore_reflection_rate"));
                // int colReflectionDamageRate = reader.GetOrdinal(GetCol("reflection_damage_rate"));
                // int colReflectionResistanceRate = reader.GetOrdinal(GetCol("reflection_resistance_rate"));
                // int colMana = reader.GetOrdinal(GetCol("mana"));
                // int colManaRegenerationRate = reader.GetOrdinal(GetCol("mana_regeneration_rate"));
                // int colDamageToDifferentFactionRate = reader.GetOrdinal(GetCol("damage_to_different_faction_rate"));
                // int colResistanceToDifferentFactionRate = reader.GetOrdinal(GetCol("resistance_to_different_faction_rate"));
                // int colDamageToSameFactionRate = reader.GetOrdinal(GetCol("damage_to_same_faction_rate"));
                // int colResistanceToSameFactionRate = reader.GetOrdinal(GetCol("resistance_to_same_faction_rate"));
                // int colNormalDamageRate = reader.GetOrdinal(GetCol("normal_damage_rate"));
                // int colNormalResistanceRate = reader.GetOrdinal(GetCol("normal_resistance_rate"));
                // int colSkillDamageRate = reader.GetOrdinal(GetCol("skill_damage_rate"));
                // int colSkillResistanceRate = reader.GetOrdinal(GetCol("skill_resistance_rate"));
                // int colDescription = reader.GetOrdinal(GetCol("description"));
                int colCardAdmiralId = reader.GetOrdinal(GetCol("card_admiral_id"));
                int colPatternId = reader.GetOrdinal(GetCol("pattern_id"));
                int colSkillSubType = reader.GetOrdinal(GetCol("skill_sub_type"));
                int colSkillEffectsJson = reader.GetOrdinal(GetCol("skill_effects_json"));

                // ==========================================
                // 2. VÒNG LẶP ĐỌC DỮ LIỆU CỰC NHANH VỚI INDEX (ORDINAL)
                // ==========================================
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe(colSkillId),
                        Name = reader.GetStringSafe(colName),
                        Image = reader.GetStringSafe(colImage),
                        Rarity = reader.GetStringSafe(colRare),
                        Quality = reader.GetDoubleSafe(colQuality),
                        Type = reader.GetStringSafe(colType),
                        Star = reader.GetIntSafe(colStar),
                        Level = reader.GetIntSafe(colLevel),
                        Position = reader.GetIntSafe(colSkillPosition),
                        SkillType = reader.GetStringSafe(colSkillType),
                        Experience = reader.GetDoubleSafe(colExperience),
                        Quantity = reader.GetIntSafe(colQuantity),
                        // Power = reader.GetDoubleSafe(colPower),
                        // Health = reader.GetDoubleSafe(colHealth),
                        // PhysicalAttack = reader.GetDoubleSafe(colPhysicalAttack),
                        // PhysicalDefense = reader.GetDoubleSafe(colPhysicalDefense),
                        // MagicalAttack = reader.GetDoubleSafe(colMagicalAttack),
                        // MagicalDefense = reader.GetDoubleSafe(colMagicalDefense),
                        // ChemicalAttack = reader.GetDoubleSafe(colChemicalAttack),
                        // ChemicalDefense = reader.GetDoubleSafe(colChemicalDefense),
                        // AtomicAttack = reader.GetDoubleSafe(colAtomicAttack),
                        // AtomicDefense = reader.GetDoubleSafe(colAtomicDefense),
                        // MentalAttack = reader.GetDoubleSafe(colMentalAttack),
                        // MentalDefense = reader.GetDoubleSafe(colMentalDefense),
                        // Speed = reader.GetDoubleSafe(colSpeed),
                        // CriticalDamageRate = reader.GetDoubleSafe(colCriticalDamageRate),
                        // CriticalRate = reader.GetDoubleSafe(colCriticalRate),
                        // CriticalResistanceRate = reader.GetDoubleSafe(colCriticalResistanceRate),
                        // IgnoreCriticalRate = reader.GetDoubleSafe(colIgnoreCriticalRate),
                        // PenetrationRate = reader.GetDoubleSafe(colPenetrationRate),
                        // PenetrationResistanceRate = reader.GetDoubleSafe(colPenetrationResistanceRate),
                        // EvasionRate = reader.GetDoubleSafe(colEvasionRate),
                        // DamageAbsorptionRate = reader.GetDoubleSafe(colDamageAbsorptionRate),
                        // IgnoreDamageAbsorptionRate = reader.GetDoubleSafe(colIgnoreDamageAbsorptionRate),
                        // AbsorbedDamageRate = reader.GetDoubleSafe(colAbsorbedDamageRate),
                        // VitalityRegenerationRate = reader.GetDoubleSafe(colVitalityRegenerationRate),
                        // VitalityRegenerationResistanceRate = reader.GetDoubleSafe(colVitalityRegenerationResistanceRate),
                        // AccuracyRate = reader.GetDoubleSafe(colAccuracyRate),
                        // LifestealRate = reader.GetDoubleSafe(colLifestealRate),
                        // ShieldStrength = reader.GetDoubleSafe(colShieldStrength),
                        // Tenacity = reader.GetDoubleSafe(colTenacity),
                        // ResistanceRate = reader.GetDoubleSafe(colResistanceRate),
                        // ComboRate = reader.GetDoubleSafe(colComboRate),
                        // IgnoreComboRate = reader.GetDoubleSafe(colIgnoreComboRate),
                        // ComboDamageRate = reader.GetDoubleSafe(colComboDamageRate),
                        // ComboResistanceRate = reader.GetDoubleSafe(colComboResistanceRate),
                        // StunRate = reader.GetDoubleSafe(colStunRate),
                        // IgnoreStunRate = reader.GetDoubleSafe(colIgnoreStunRate),
                        // ReflectionRate = reader.GetDoubleSafe(colReflectionRate),
                        // IgnoreReflectionRate = reader.GetDoubleSafe(colIgnoreReflectionRate),
                        // ReflectionDamageRate = reader.GetDoubleSafe(colReflectionDamageRate),
                        // ReflectionResistanceRate = reader.GetDoubleSafe(colReflectionResistanceRate),
                        // Mana = reader.GetDoubleSafe(colMana),
                        // ManaRegenerationRate = reader.GetDoubleSafe(colManaRegenerationRate),
                        // DamageToDifferentFactionRate = reader.GetDoubleSafe(colDamageToDifferentFactionRate),
                        // ResistanceToDifferentFactionRate = reader.GetDoubleSafe(colResistanceToDifferentFactionRate),
                        // DamageToSameFactionRate = reader.GetDoubleSafe(colDamageToSameFactionRate),
                        // ResistanceToSameFactionRate = reader.GetDoubleSafe(colResistanceToSameFactionRate),
                        // NormalDamageRate = reader.GetDoubleSafe(colNormalDamageRate),
                        // NormalResistanceRate = reader.GetDoubleSafe(colNormalResistanceRate),
                        // SkillDamageRate = reader.GetDoubleSafe(colSkillDamageRate),
                        // SkillResistanceRate = reader.GetDoubleSafe(colSkillResistanceRate),
                        // Description = reader.GetStringSafe(colDescription),

                        CardId = reader.GetStringSafe(colCardAdmiralId),
                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe(colPatternId)
                        },
                        SkillSubType = new SkillSubTypes
                        {
                            SubTypeCode = reader.GetStringSafe(colSkillSubType)
                        }
                    };

                    string effectsJson = reader.GetStringSafe(colSkillEffectsJson);

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                // Đóng reader sớm để giải phóng tài nguyên mạng trước khi CPU thực hiện giải mã JSON
                await reader.CloseAsync();

                // ==========================================
                // TỐI ƯU HÓA: GIẢI MÃ JSON SONG SONG BẰNG JSONHELPER CỦA BẠN
                // ==========================================
                if (pendingJsonList.Count > 0)
                {
                    Parallel.ForEach(pendingJsonList, item =>
                    {
                        try
                        {
                            item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                            item.skill.Effects = new List<Effects>(); // Fallback an toàn
                        }
                    });
                }

                // Load Effects cho toàn bộ Skills cùng một lúc
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            // block 'finally' không cần CloseAsync() thủ công nữa vì 'await using' đã tự động xử lý giải phóng kết nối một cách an toàn.
        }

        return skills;
    }
    public async Task<List<Skills>> GetUserCardMonstersSkillsAsync(string userId, List<string> cardMonsterIds)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();

        // Kiểm tra danh sách đầu vào để tránh lỗi SQL khi danh sách rỗng
        if (cardMonsterIds == null || cardMonsterIds.Count == 0)
        {
            return skills;
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // 1. Tạo danh sách tham số dạng @heroId0, @heroId1,... động để tránh SQL Injection
                var paramNames = new List<string>();
                for (int i = 0; i < cardMonsterIds.Count; i++)
                {
                    paramNames.Add($"@cardMonsterId{i}");
                }

                // Nối các tên tham số lại thành chuỗi: "@heroId0, @heroId1, @heroId2"
                string inClause = string.Join(", ", paramNames);

                // 2. Cập nhật lại SQL Query với điều kiện IN danh sách các Hero ID
                // Sửa lại logic JOIN chính xác: chs.card_hero_id IN (...) thay vì gán nhầm vào skill_id
                string selectSQL = $@"
                WITH TargetSkills AS (
                    -- Bước 1: Lấy danh sách ID skill duy nhất của các Card cần tìm
                    SELECT DISTINCT us.skill_id
                    FROM user_skills us
                    JOIN card_monsters_skills chs ON chs.skill_id = us.skill_id
                    WHERE us.user_id = @userId
                    AND chs.card_monster_id IN ({inClause})
                ),
                BaseEffects AS (
                    -- Bước 2: Lấy thông tin cơ bản của Effect trước để tránh nhân dòng chéo
                    SELECT 
                        se.skill_id,
                        se.effect_id,
                        se.min_value,
                        se.max_value,
                        se.trigger_phase,
                        se.trigger_condition,
                        se.is_stackable,
                        se.is_removable,
                        se.target_id,
                        e.name AS effect_name,
                        e.effect_type,
                        e.duration,
                        e.description AS effect_description
                    FROM skill_effect se
                    JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                    WHERE se.skill_id IN (SELECT skill_id FROM TargetSkills)
                ),
                AggregatedEffects AS (
                    -- Bước 3: Gom nhóm JSON. Lúc này dữ liệu đã gọn, không bị nhân dòng ảo
                    SELECT 
                        be.skill_id,
                        JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', be.min_value,
                                'max_value', be.max_value,
                                'trigger_phase', be.trigger_phase,
                                'trigger_condition', be.trigger_condition,
                                'is_stackable', be.is_stackable,
                                'is_removable', be.is_removable,
                                'target_id', be.target_id,
                                'effect_id', be.effect_id,
                                'effect_name', be.effect_name,
                                'effect_type', be.effect_type,
                                'duration', be.duration,
                                'effect_description', be.effect_description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        ) AS skill_effects_json
                    FROM BaseEffects be
                    LEFT JOIN effect_property_action epa ON be.effect_id = epa.effect_id
                    LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                    LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                    GROUP BY be.skill_id
                )
                -- Bước 4: Xuất dữ liệu cuối cùng phẳng, gom gọn theo card_monster_id
                SELECT 
                    us.skill_id,
                    s.name, 
                    s.image, 
                    us.rare, 
                    us.star, 
                    us.level, 
                    us.experience, 
                    us.quality, 
                    us.quantity,
                    s.type,
                    s.skill_type,
                    s.skill_sub_type,
                    chs.position AS skill_position, 
                    sp.pattern_id, 
                    chs.card_monster_id,
                    ae.skill_effects_json
                FROM card_monsters_skills chs
                JOIN user_skills us ON chs.skill_id = us.skill_id AND us.user_id = @userId
                JOIN Skills s ON chs.skill_id = s.id
                LEFT JOIN skill_patterns sp ON chs.skill_id = sp.skill_id
                LEFT JOIN AggregatedEffects ae ON chs.skill_id = ae.skill_id
                WHERE chs.card_monster_id IN ({inClause});";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);

                // Gán tham số userId cố định
                selectCommand.Parameters.AddWithValue("@userId", userId);

                // Gán các tham số động từ danh sách mảng heroIds
                for (int i = 0; i < cardMonsterIds.Count; i++)
                {
                    selectCommand.Parameters.AddWithValue(paramNames[i], cardMonsterIds[i]);
                }

                // 3. Thực thi và đọc dữ liệu
                await using var reader = await selectCommand.ExecuteReaderAsync();

                // --- MẸO TỐI ƯU ---
                // Đọc trước cấu trúc các cột trả về và lưu tên cột viết thường (lowercase) vào một Dictionary mapping Index.
                // Điều này giúp driver ADO.NET (MySqlConnector) tìm kiếm trực tiếp bằng bộ nhớ cache cực nhanh bên trong thay vì quét chuỗi.
                var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnMap[reader.GetName(i)] = i;
                }

                // Hàm Helper nội bộ (Local Function) để lấy nhanh tên cột chuẩn từ database nhằm tránh việc driver đi quét chuỗi liên tục.
                // Hàm này sẽ trả về đúng tên cột gốc (đã được cache vị trí) giúp tối ưu hóa 100% cho các hàm GetXXXSafe của bạn.
                string GetCol(string columnName)
                {
                    return columnMap.TryGetValue(columnName, out int index) ? reader.GetName(index) : columnName;
                }

                // ==========================================
                // 1. LẤY VÀ CACHE SẴN INDEX CỦA TẤT CẢ CÁC CỘT (Chỉ chạy đúng 1 lần trước vòng lặp)
                // ==========================================
                int colSkillId = reader.GetOrdinal(GetCol("skill_id"));
                int colName = reader.GetOrdinal(GetCol("name"));
                int colImage = reader.GetOrdinal(GetCol("image"));
                int colRare = reader.GetOrdinal(GetCol("rare"));
                int colQuality = reader.GetOrdinal(GetCol("quality"));
                int colType = reader.GetOrdinal(GetCol("type"));
                int colStar = reader.GetOrdinal(GetCol("star"));
                int colLevel = reader.GetOrdinal(GetCol("level"));
                int colSkillPosition = reader.GetOrdinal(GetCol("skill_position"));
                int colSkillType = reader.GetOrdinal(GetCol("skill_type"));
                int colExperience = reader.GetOrdinal(GetCol("experience"));
                int colQuantity = reader.GetOrdinal(GetCol("quantity"));
                // int colPower = reader.GetOrdinal(GetCol("power"));
                // int colHealth = reader.GetOrdinal(GetCol("health"));
                // int colPhysicalAttack = reader.GetOrdinal(GetCol("physical_attack"));
                // int colPhysicalDefense = reader.GetOrdinal(GetCol("physical_defense"));
                // int colMagicalAttack = reader.GetOrdinal(GetCol("magical_attack"));
                // int colMagicalDefense = reader.GetOrdinal(GetCol("magical_defense"));
                // int colChemicalAttack = reader.GetOrdinal(GetCol("chemical_attack"));
                // int colChemicalDefense = reader.GetOrdinal(GetCol("chemical_defense"));
                // int colAtomicAttack = reader.GetOrdinal(GetCol("atomic_attack"));
                // int colAtomicDefense = reader.GetOrdinal(GetCol("atomic_defense"));
                // int colMentalAttack = reader.GetOrdinal(GetCol("mental_attack"));
                // int colMentalDefense = reader.GetOrdinal(GetCol("mental_defense"));
                // int colSpeed = reader.GetOrdinal(GetCol("speed"));
                // int colCriticalDamageRate = reader.GetOrdinal(GetCol("critical_damage_rate"));
                // int colCriticalRate = reader.GetOrdinal(GetCol("critical_rate"));
                // int colCriticalResistanceRate = reader.GetOrdinal(GetCol("critical_resistance_rate"));
                // int colIgnoreCriticalRate = reader.GetOrdinal(GetCol("ignore_critical_rate"));
                // int colPenetrationRate = reader.GetOrdinal(GetCol("penetration_rate"));
                // int colPenetrationResistanceRate = reader.GetOrdinal(GetCol("penetration_resistance_rate"));
                // int colEvasionRate = reader.GetOrdinal(GetCol("evasion_rate"));
                // int colDamageAbsorptionRate = reader.GetOrdinal(GetCol("damage_absorption_rate"));
                // int colIgnoreDamageAbsorptionRate = reader.GetOrdinal(GetCol("ignore_damage_absorption_rate"));
                // int colAbsorbedDamageRate = reader.GetOrdinal(GetCol("absorbed_damage_rate"));
                // int colVitalityRegenerationRate = reader.GetOrdinal(GetCol("vitality_regeneration_rate"));
                // int colVitalityRegenerationResistanceRate = reader.GetOrdinal(GetCol("vitality_regeneration_resistance_rate"));
                // int colAccuracyRate = reader.GetOrdinal(GetCol("accuracy_rate"));
                // int colLifestealRate = reader.GetOrdinal(GetCol("lifesteal_rate"));
                // int colShieldStrength = reader.GetOrdinal(GetCol("shield_strength"));
                // int colTenacity = reader.GetOrdinal(GetCol("tenacity"));
                // int colResistanceRate = reader.GetOrdinal(GetCol("resistance_rate"));
                // int colComboRate = reader.GetOrdinal(GetCol("combo_rate"));
                // int colIgnoreComboRate = reader.GetOrdinal(GetCol("ignore_combo_rate"));
                // int colComboDamageRate = reader.GetOrdinal(GetCol("combo_damage_rate"));
                // int colComboResistanceRate = reader.GetOrdinal(GetCol("combo_resistance_rate"));
                // int colStunRate = reader.GetOrdinal(GetCol("stun_rate"));
                // int colIgnoreStunRate = reader.GetOrdinal(GetCol("ignore_stun_rate"));
                // int colReflectionRate = reader.GetOrdinal(GetCol("reflection_rate"));
                // int colIgnoreReflectionRate = reader.GetOrdinal(GetCol("ignore_reflection_rate"));
                // int colReflectionDamageRate = reader.GetOrdinal(GetCol("reflection_damage_rate"));
                // int colReflectionResistanceRate = reader.GetOrdinal(GetCol("reflection_resistance_rate"));
                // int colMana = reader.GetOrdinal(GetCol("mana"));
                // int colManaRegenerationRate = reader.GetOrdinal(GetCol("mana_regeneration_rate"));
                // int colDamageToDifferentFactionRate = reader.GetOrdinal(GetCol("damage_to_different_faction_rate"));
                // int colResistanceToDifferentFactionRate = reader.GetOrdinal(GetCol("resistance_to_different_faction_rate"));
                // int colDamageToSameFactionRate = reader.GetOrdinal(GetCol("damage_to_same_faction_rate"));
                // int colResistanceToSameFactionRate = reader.GetOrdinal(GetCol("resistance_to_same_faction_rate"));
                // int colNormalDamageRate = reader.GetOrdinal(GetCol("normal_damage_rate"));
                // int colNormalResistanceRate = reader.GetOrdinal(GetCol("normal_resistance_rate"));
                // int colSkillDamageRate = reader.GetOrdinal(GetCol("skill_damage_rate"));
                // int colSkillResistanceRate = reader.GetOrdinal(GetCol("skill_resistance_rate"));
                // int colDescription = reader.GetOrdinal(GetCol("description"));
                int colCardMonsterId = reader.GetOrdinal(GetCol("card_monster_id"));
                int colPatternId = reader.GetOrdinal(GetCol("pattern_id"));
                int colSkillSubType = reader.GetOrdinal(GetCol("skill_sub_type"));
                int colSkillEffectsJson = reader.GetOrdinal(GetCol("skill_effects_json"));

                // ==========================================
                // 2. VÒNG LẶP ĐỌC DỮ LIỆU CỰC NHANH VỚI INDEX (ORDINAL)
                // ==========================================
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe(colSkillId),
                        Name = reader.GetStringSafe(colName),
                        Image = reader.GetStringSafe(colImage),
                        Rarity = reader.GetStringSafe(colRare),
                        Quality = reader.GetDoubleSafe(colQuality),
                        Type = reader.GetStringSafe(colType),
                        Star = reader.GetIntSafe(colStar),
                        Level = reader.GetIntSafe(colLevel),
                        Position = reader.GetIntSafe(colSkillPosition),
                        SkillType = reader.GetStringSafe(colSkillType),
                        Experience = reader.GetDoubleSafe(colExperience),
                        Quantity = reader.GetIntSafe(colQuantity),
                        // Power = reader.GetDoubleSafe(colPower),
                        // Health = reader.GetDoubleSafe(colHealth),
                        // PhysicalAttack = reader.GetDoubleSafe(colPhysicalAttack),
                        // PhysicalDefense = reader.GetDoubleSafe(colPhysicalDefense),
                        // MagicalAttack = reader.GetDoubleSafe(colMagicalAttack),
                        // MagicalDefense = reader.GetDoubleSafe(colMagicalDefense),
                        // ChemicalAttack = reader.GetDoubleSafe(colChemicalAttack),
                        // ChemicalDefense = reader.GetDoubleSafe(colChemicalDefense),
                        // AtomicAttack = reader.GetDoubleSafe(colAtomicAttack),
                        // AtomicDefense = reader.GetDoubleSafe(colAtomicDefense),
                        // MentalAttack = reader.GetDoubleSafe(colMentalAttack),
                        // MentalDefense = reader.GetDoubleSafe(colMentalDefense),
                        // Speed = reader.GetDoubleSafe(colSpeed),
                        // CriticalDamageRate = reader.GetDoubleSafe(colCriticalDamageRate),
                        // CriticalRate = reader.GetDoubleSafe(colCriticalRate),
                        // CriticalResistanceRate = reader.GetDoubleSafe(colCriticalResistanceRate),
                        // IgnoreCriticalRate = reader.GetDoubleSafe(colIgnoreCriticalRate),
                        // PenetrationRate = reader.GetDoubleSafe(colPenetrationRate),
                        // PenetrationResistanceRate = reader.GetDoubleSafe(colPenetrationResistanceRate),
                        // EvasionRate = reader.GetDoubleSafe(colEvasionRate),
                        // DamageAbsorptionRate = reader.GetDoubleSafe(colDamageAbsorptionRate),
                        // IgnoreDamageAbsorptionRate = reader.GetDoubleSafe(colIgnoreDamageAbsorptionRate),
                        // AbsorbedDamageRate = reader.GetDoubleSafe(colAbsorbedDamageRate),
                        // VitalityRegenerationRate = reader.GetDoubleSafe(colVitalityRegenerationRate),
                        // VitalityRegenerationResistanceRate = reader.GetDoubleSafe(colVitalityRegenerationResistanceRate),
                        // AccuracyRate = reader.GetDoubleSafe(colAccuracyRate),
                        // LifestealRate = reader.GetDoubleSafe(colLifestealRate),
                        // ShieldStrength = reader.GetDoubleSafe(colShieldStrength),
                        // Tenacity = reader.GetDoubleSafe(colTenacity),
                        // ResistanceRate = reader.GetDoubleSafe(colResistanceRate),
                        // ComboRate = reader.GetDoubleSafe(colComboRate),
                        // IgnoreComboRate = reader.GetDoubleSafe(colIgnoreComboRate),
                        // ComboDamageRate = reader.GetDoubleSafe(colComboDamageRate),
                        // ComboResistanceRate = reader.GetDoubleSafe(colComboResistanceRate),
                        // StunRate = reader.GetDoubleSafe(colStunRate),
                        // IgnoreStunRate = reader.GetDoubleSafe(colIgnoreStunRate),
                        // ReflectionRate = reader.GetDoubleSafe(colReflectionRate),
                        // IgnoreReflectionRate = reader.GetDoubleSafe(colIgnoreReflectionRate),
                        // ReflectionDamageRate = reader.GetDoubleSafe(colReflectionDamageRate),
                        // ReflectionResistanceRate = reader.GetDoubleSafe(colReflectionResistanceRate),
                        // Mana = reader.GetDoubleSafe(colMana),
                        // ManaRegenerationRate = reader.GetDoubleSafe(colManaRegenerationRate),
                        // DamageToDifferentFactionRate = reader.GetDoubleSafe(colDamageToDifferentFactionRate),
                        // ResistanceToDifferentFactionRate = reader.GetDoubleSafe(colResistanceToDifferentFactionRate),
                        // DamageToSameFactionRate = reader.GetDoubleSafe(colDamageToSameFactionRate),
                        // ResistanceToSameFactionRate = reader.GetDoubleSafe(colResistanceToSameFactionRate),
                        // NormalDamageRate = reader.GetDoubleSafe(colNormalDamageRate),
                        // NormalResistanceRate = reader.GetDoubleSafe(colNormalResistanceRate),
                        // SkillDamageRate = reader.GetDoubleSafe(colSkillDamageRate),
                        // SkillResistanceRate = reader.GetDoubleSafe(colSkillResistanceRate),
                        // Description = reader.GetStringSafe(colDescription),

                        CardId = reader.GetStringSafe(colCardMonsterId),
                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe(colPatternId)
                        },
                        SkillSubType = new SkillSubTypes
                        {
                            SubTypeCode = reader.GetStringSafe(colSkillSubType)
                        }
                    };

                    string effectsJson = reader.GetStringSafe(colSkillEffectsJson);

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                // Đóng reader sớm để giải phóng tài nguyên mạng trước khi CPU thực hiện giải mã JSON
                await reader.CloseAsync();

                // ==========================================
                // TỐI ƯU HÓA: GIẢI MÃ JSON SONG SONG BẰNG JSONHELPER CỦA BẠN
                // ==========================================
                if (pendingJsonList.Count > 0)
                {
                    Parallel.ForEach(pendingJsonList, item =>
                    {
                        try
                        {
                            item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                            item.skill.Effects = new List<Effects>(); // Fallback an toàn
                        }
                    });
                }

                // Load Effects cho toàn bộ Skills cùng một lúc
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            // block 'finally' không cần CloseAsync() thủ công nữa vì 'await using' đã tự động xử lý giải phóng kết nối một cách an toàn.
        }

        return skills;
    }
    public async Task<List<Skills>> GetUserCardMilitariesSkillsAsync(string userId, List<string> cardMilitaryIds)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();

        // Kiểm tra danh sách đầu vào để tránh lỗi SQL khi danh sách rỗng
        if (cardMilitaryIds == null || cardMilitaryIds.Count == 0)
        {
            return skills;
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // 1. Tạo danh sách tham số dạng @heroId0, @heroId1,... động để tránh SQL Injection
                var paramNames = new List<string>();
                for (int i = 0; i < cardMilitaryIds.Count; i++)
                {
                    paramNames.Add($"@cardMilitaryId{i}");
                }

                // Nối các tên tham số lại thành chuỗi: "@heroId0, @heroId1, @heroId2"
                string inClause = string.Join(", ", paramNames);

                // 2. Cập nhật lại SQL Query với điều kiện IN danh sách các Hero ID
                // Sửa lại logic JOIN chính xác: chs.card_hero_id IN (...) thay vì gán nhầm vào skill_id
                string selectSQL = $@"
                WITH TargetSkills AS (
                    -- Bước 1: Lấy danh sách ID skill duy nhất của các Card cần tìm
                    SELECT DISTINCT us.skill_id
                    FROM user_skills us
                    JOIN card_militaries_skills chs ON chs.skill_id = us.skill_id
                    WHERE us.user_id = @userId
                    AND chs.card_military_id IN ({inClause})
                ),
                BaseEffects AS (
                    -- Bước 2: Lấy thông tin cơ bản của Effect trước để tránh nhân dòng chéo
                    SELECT 
                        se.skill_id,
                        se.effect_id,
                        se.min_value,
                        se.max_value,
                        se.trigger_phase,
                        se.trigger_condition,
                        se.is_stackable,
                        se.is_removable,
                        se.target_id,
                        e.name AS effect_name,
                        e.effect_type,
                        e.duration,
                        e.description AS effect_description
                    FROM skill_effect se
                    JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                    WHERE se.skill_id IN (SELECT skill_id FROM TargetSkills)
                ),
                AggregatedEffects AS (
                    -- Bước 3: Gom nhóm JSON. Lúc này dữ liệu đã gọn, không bị nhân dòng ảo
                    SELECT 
                        be.skill_id,
                        JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', be.min_value,
                                'max_value', be.max_value,
                                'trigger_phase', be.trigger_phase,
                                'trigger_condition', be.trigger_condition,
                                'is_stackable', be.is_stackable,
                                'is_removable', be.is_removable,
                                'target_id', be.target_id,
                                'effect_id', be.effect_id,
                                'effect_name', be.effect_name,
                                'effect_type', be.effect_type,
                                'duration', be.duration,
                                'effect_description', be.effect_description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        ) AS skill_effects_json
                    FROM BaseEffects be
                    LEFT JOIN effect_property_action epa ON be.effect_id = epa.effect_id
                    LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                    LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                    GROUP BY be.skill_id
                )
                -- Bước 4: Xuất dữ liệu cuối cùng phẳng, gom gọn theo card_military_id
                SELECT 
                    us.skill_id,
                    s.name, 
                    s.image, 
                    us.rare, 
                    us.star, 
                    us.level, 
                    us.experience, 
                    us.quality, 
                    us.quantity,
                    s.type,
                    s.skill_type,
                    s.skill_sub_type,
                    chs.position AS skill_position, 
                    sp.pattern_id, 
                    chs.card_military_id,
                    ae.skill_effects_json
                FROM card_militaries_skills chs
                JOIN user_skills us ON chs.skill_id = us.skill_id AND us.user_id = @userId
                JOIN Skills s ON chs.skill_id = s.id
                LEFT JOIN skill_patterns sp ON chs.skill_id = sp.skill_id
                LEFT JOIN AggregatedEffects ae ON chs.skill_id = ae.skill_id
                WHERE chs.card_military_id IN ({inClause});";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);

                // Gán tham số userId cố định
                selectCommand.Parameters.AddWithValue("@userId", userId);

                // Gán các tham số động từ danh sách mảng heroIds
                for (int i = 0; i < cardMilitaryIds.Count; i++)
                {
                    selectCommand.Parameters.AddWithValue(paramNames[i], cardMilitaryIds[i]);
                }

                // 3. Thực thi và đọc dữ liệu
                await using var reader = await selectCommand.ExecuteReaderAsync();

                // --- MẸO TỐI ƯU ---
                // Đọc trước cấu trúc các cột trả về và lưu tên cột viết thường (lowercase) vào một Dictionary mapping Index.
                // Điều này giúp driver ADO.NET (MySqlConnector) tìm kiếm trực tiếp bằng bộ nhớ cache cực nhanh bên trong thay vì quét chuỗi.
                var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnMap[reader.GetName(i)] = i;
                }

                // Hàm Helper nội bộ (Local Function) để lấy nhanh tên cột chuẩn từ database nhằm tránh việc driver đi quét chuỗi liên tục.
                // Hàm này sẽ trả về đúng tên cột gốc (đã được cache vị trí) giúp tối ưu hóa 100% cho các hàm GetXXXSafe của bạn.
                string GetCol(string columnName)
                {
                    return columnMap.TryGetValue(columnName, out int index) ? reader.GetName(index) : columnName;
                }

                // ==========================================
                // 1. LẤY VÀ CACHE SẴN INDEX CỦA TẤT CẢ CÁC CỘT (Chỉ chạy đúng 1 lần trước vòng lặp)
                // ==========================================
                int colSkillId = reader.GetOrdinal(GetCol("skill_id"));
                int colName = reader.GetOrdinal(GetCol("name"));
                int colImage = reader.GetOrdinal(GetCol("image"));
                int colRare = reader.GetOrdinal(GetCol("rare"));
                int colQuality = reader.GetOrdinal(GetCol("quality"));
                int colType = reader.GetOrdinal(GetCol("type"));
                int colStar = reader.GetOrdinal(GetCol("star"));
                int colLevel = reader.GetOrdinal(GetCol("level"));
                int colSkillPosition = reader.GetOrdinal(GetCol("skill_position"));
                int colSkillType = reader.GetOrdinal(GetCol("skill_type"));
                int colExperience = reader.GetOrdinal(GetCol("experience"));
                int colQuantity = reader.GetOrdinal(GetCol("quantity"));
                // int colPower = reader.GetOrdinal(GetCol("power"));
                // int colHealth = reader.GetOrdinal(GetCol("health"));
                // int colPhysicalAttack = reader.GetOrdinal(GetCol("physical_attack"));
                // int colPhysicalDefense = reader.GetOrdinal(GetCol("physical_defense"));
                // int colMagicalAttack = reader.GetOrdinal(GetCol("magical_attack"));
                // int colMagicalDefense = reader.GetOrdinal(GetCol("magical_defense"));
                // int colChemicalAttack = reader.GetOrdinal(GetCol("chemical_attack"));
                // int colChemicalDefense = reader.GetOrdinal(GetCol("chemical_defense"));
                // int colAtomicAttack = reader.GetOrdinal(GetCol("atomic_attack"));
                // int colAtomicDefense = reader.GetOrdinal(GetCol("atomic_defense"));
                // int colMentalAttack = reader.GetOrdinal(GetCol("mental_attack"));
                // int colMentalDefense = reader.GetOrdinal(GetCol("mental_defense"));
                // int colSpeed = reader.GetOrdinal(GetCol("speed"));
                // int colCriticalDamageRate = reader.GetOrdinal(GetCol("critical_damage_rate"));
                // int colCriticalRate = reader.GetOrdinal(GetCol("critical_rate"));
                // int colCriticalResistanceRate = reader.GetOrdinal(GetCol("critical_resistance_rate"));
                // int colIgnoreCriticalRate = reader.GetOrdinal(GetCol("ignore_critical_rate"));
                // int colPenetrationRate = reader.GetOrdinal(GetCol("penetration_rate"));
                // int colPenetrationResistanceRate = reader.GetOrdinal(GetCol("penetration_resistance_rate"));
                // int colEvasionRate = reader.GetOrdinal(GetCol("evasion_rate"));
                // int colDamageAbsorptionRate = reader.GetOrdinal(GetCol("damage_absorption_rate"));
                // int colIgnoreDamageAbsorptionRate = reader.GetOrdinal(GetCol("ignore_damage_absorption_rate"));
                // int colAbsorbedDamageRate = reader.GetOrdinal(GetCol("absorbed_damage_rate"));
                // int colVitalityRegenerationRate = reader.GetOrdinal(GetCol("vitality_regeneration_rate"));
                // int colVitalityRegenerationResistanceRate = reader.GetOrdinal(GetCol("vitality_regeneration_resistance_rate"));
                // int colAccuracyRate = reader.GetOrdinal(GetCol("accuracy_rate"));
                // int colLifestealRate = reader.GetOrdinal(GetCol("lifesteal_rate"));
                // int colShieldStrength = reader.GetOrdinal(GetCol("shield_strength"));
                // int colTenacity = reader.GetOrdinal(GetCol("tenacity"));
                // int colResistanceRate = reader.GetOrdinal(GetCol("resistance_rate"));
                // int colComboRate = reader.GetOrdinal(GetCol("combo_rate"));
                // int colIgnoreComboRate = reader.GetOrdinal(GetCol("ignore_combo_rate"));
                // int colComboDamageRate = reader.GetOrdinal(GetCol("combo_damage_rate"));
                // int colComboResistanceRate = reader.GetOrdinal(GetCol("combo_resistance_rate"));
                // int colStunRate = reader.GetOrdinal(GetCol("stun_rate"));
                // int colIgnoreStunRate = reader.GetOrdinal(GetCol("ignore_stun_rate"));
                // int colReflectionRate = reader.GetOrdinal(GetCol("reflection_rate"));
                // int colIgnoreReflectionRate = reader.GetOrdinal(GetCol("ignore_reflection_rate"));
                // int colReflectionDamageRate = reader.GetOrdinal(GetCol("reflection_damage_rate"));
                // int colReflectionResistanceRate = reader.GetOrdinal(GetCol("reflection_resistance_rate"));
                // int colMana = reader.GetOrdinal(GetCol("mana"));
                // int colManaRegenerationRate = reader.GetOrdinal(GetCol("mana_regeneration_rate"));
                // int colDamageToDifferentFactionRate = reader.GetOrdinal(GetCol("damage_to_different_faction_rate"));
                // int colResistanceToDifferentFactionRate = reader.GetOrdinal(GetCol("resistance_to_different_faction_rate"));
                // int colDamageToSameFactionRate = reader.GetOrdinal(GetCol("damage_to_same_faction_rate"));
                // int colResistanceToSameFactionRate = reader.GetOrdinal(GetCol("resistance_to_same_faction_rate"));
                // int colNormalDamageRate = reader.GetOrdinal(GetCol("normal_damage_rate"));
                // int colNormalResistanceRate = reader.GetOrdinal(GetCol("normal_resistance_rate"));
                // int colSkillDamageRate = reader.GetOrdinal(GetCol("skill_damage_rate"));
                // int colSkillResistanceRate = reader.GetOrdinal(GetCol("skill_resistance_rate"));
                // int colDescription = reader.GetOrdinal(GetCol("description"));
                int colCardMilitaryId = reader.GetOrdinal(GetCol("card_military_id"));
                int colPatternId = reader.GetOrdinal(GetCol("pattern_id"));
                int colSkillSubType = reader.GetOrdinal(GetCol("skill_sub_type"));
                int colSkillEffectsJson = reader.GetOrdinal(GetCol("skill_effects_json"));

                // ==========================================
                // 2. VÒNG LẶP ĐỌC DỮ LIỆU CỰC NHANH VỚI INDEX (ORDINAL)
                // ==========================================
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe(colSkillId),
                        Name = reader.GetStringSafe(colName),
                        Image = reader.GetStringSafe(colImage),
                        Rarity = reader.GetStringSafe(colRare),
                        Quality = reader.GetDoubleSafe(colQuality),
                        Type = reader.GetStringSafe(colType),
                        Star = reader.GetIntSafe(colStar),
                        Level = reader.GetIntSafe(colLevel),
                        Position = reader.GetIntSafe(colSkillPosition),
                        SkillType = reader.GetStringSafe(colSkillType),
                        Experience = reader.GetDoubleSafe(colExperience),
                        Quantity = reader.GetIntSafe(colQuantity),
                        // Power = reader.GetDoubleSafe(colPower),
                        // Health = reader.GetDoubleSafe(colHealth),
                        // PhysicalAttack = reader.GetDoubleSafe(colPhysicalAttack),
                        // PhysicalDefense = reader.GetDoubleSafe(colPhysicalDefense),
                        // MagicalAttack = reader.GetDoubleSafe(colMagicalAttack),
                        // MagicalDefense = reader.GetDoubleSafe(colMagicalDefense),
                        // ChemicalAttack = reader.GetDoubleSafe(colChemicalAttack),
                        // ChemicalDefense = reader.GetDoubleSafe(colChemicalDefense),
                        // AtomicAttack = reader.GetDoubleSafe(colAtomicAttack),
                        // AtomicDefense = reader.GetDoubleSafe(colAtomicDefense),
                        // MentalAttack = reader.GetDoubleSafe(colMentalAttack),
                        // MentalDefense = reader.GetDoubleSafe(colMentalDefense),
                        // Speed = reader.GetDoubleSafe(colSpeed),
                        // CriticalDamageRate = reader.GetDoubleSafe(colCriticalDamageRate),
                        // CriticalRate = reader.GetDoubleSafe(colCriticalRate),
                        // CriticalResistanceRate = reader.GetDoubleSafe(colCriticalResistanceRate),
                        // IgnoreCriticalRate = reader.GetDoubleSafe(colIgnoreCriticalRate),
                        // PenetrationRate = reader.GetDoubleSafe(colPenetrationRate),
                        // PenetrationResistanceRate = reader.GetDoubleSafe(colPenetrationResistanceRate),
                        // EvasionRate = reader.GetDoubleSafe(colEvasionRate),
                        // DamageAbsorptionRate = reader.GetDoubleSafe(colDamageAbsorptionRate),
                        // IgnoreDamageAbsorptionRate = reader.GetDoubleSafe(colIgnoreDamageAbsorptionRate),
                        // AbsorbedDamageRate = reader.GetDoubleSafe(colAbsorbedDamageRate),
                        // VitalityRegenerationRate = reader.GetDoubleSafe(colVitalityRegenerationRate),
                        // VitalityRegenerationResistanceRate = reader.GetDoubleSafe(colVitalityRegenerationResistanceRate),
                        // AccuracyRate = reader.GetDoubleSafe(colAccuracyRate),
                        // LifestealRate = reader.GetDoubleSafe(colLifestealRate),
                        // ShieldStrength = reader.GetDoubleSafe(colShieldStrength),
                        // Tenacity = reader.GetDoubleSafe(colTenacity),
                        // ResistanceRate = reader.GetDoubleSafe(colResistanceRate),
                        // ComboRate = reader.GetDoubleSafe(colComboRate),
                        // IgnoreComboRate = reader.GetDoubleSafe(colIgnoreComboRate),
                        // ComboDamageRate = reader.GetDoubleSafe(colComboDamageRate),
                        // ComboResistanceRate = reader.GetDoubleSafe(colComboResistanceRate),
                        // StunRate = reader.GetDoubleSafe(colStunRate),
                        // IgnoreStunRate = reader.GetDoubleSafe(colIgnoreStunRate),
                        // ReflectionRate = reader.GetDoubleSafe(colReflectionRate),
                        // IgnoreReflectionRate = reader.GetDoubleSafe(colIgnoreReflectionRate),
                        // ReflectionDamageRate = reader.GetDoubleSafe(colReflectionDamageRate),
                        // ReflectionResistanceRate = reader.GetDoubleSafe(colReflectionResistanceRate),
                        // Mana = reader.GetDoubleSafe(colMana),
                        // ManaRegenerationRate = reader.GetDoubleSafe(colManaRegenerationRate),
                        // DamageToDifferentFactionRate = reader.GetDoubleSafe(colDamageToDifferentFactionRate),
                        // ResistanceToDifferentFactionRate = reader.GetDoubleSafe(colResistanceToDifferentFactionRate),
                        // DamageToSameFactionRate = reader.GetDoubleSafe(colDamageToSameFactionRate),
                        // ResistanceToSameFactionRate = reader.GetDoubleSafe(colResistanceToSameFactionRate),
                        // NormalDamageRate = reader.GetDoubleSafe(colNormalDamageRate),
                        // NormalResistanceRate = reader.GetDoubleSafe(colNormalResistanceRate),
                        // SkillDamageRate = reader.GetDoubleSafe(colSkillDamageRate),
                        // SkillResistanceRate = reader.GetDoubleSafe(colSkillResistanceRate),
                        // Description = reader.GetStringSafe(colDescription),

                        CardId = reader.GetStringSafe(colCardMilitaryId),
                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe(colPatternId)
                        },
                        SkillSubType = new SkillSubTypes
                        {
                            SubTypeCode = reader.GetStringSafe(colSkillSubType)
                        }
                    };

                    string effectsJson = reader.GetStringSafe(colSkillEffectsJson);

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                // Đóng reader sớm để giải phóng tài nguyên mạng trước khi CPU thực hiện giải mã JSON
                await reader.CloseAsync();

                // ==========================================
                // TỐI ƯU HÓA: GIẢI MÃ JSON SONG SONG BẰNG JSONHELPER CỦA BẠN
                // ==========================================
                if (pendingJsonList.Count > 0)
                {
                    Parallel.ForEach(pendingJsonList, item =>
                    {
                        try
                        {
                            item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                            item.skill.Effects = new List<Effects>(); // Fallback an toàn
                        }
                    });
                }

                // Load Effects cho toàn bộ Skills cùng một lúc
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            // block 'finally' không cần CloseAsync() thủ công nữa vì 'await using' đã tự động xử lý giải phóng kết nối một cách an toàn.
        }

        return skills;
    }
    public async Task<List<Skills>> GetUserCardSpellsSkillsAsync(string userId, List<string> cardSpellIds)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();

        // Kiểm tra danh sách đầu vào để tránh lỗi SQL khi danh sách rỗng
        if (cardSpellIds == null || cardSpellIds.Count == 0)
        {
            return skills;
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // 1. Tạo danh sách tham số dạng @heroId0, @heroId1,... động để tránh SQL Injection
                var paramNames = new List<string>();
                for (int i = 0; i < cardSpellIds.Count; i++)
                {
                    paramNames.Add($"@cardSpellId{i}");
                }

                // Nối các tên tham số lại thành chuỗi: "@heroId0, @heroId1, @heroId2"
                string inClause = string.Join(", ", paramNames);

                // 2. Cập nhật lại SQL Query với điều kiện IN danh sách các Hero ID
                // Sửa lại logic JOIN chính xác: chs.card_hero_id IN (...) thay vì gán nhầm vào skill_id
                string selectSQL = $@"
                WITH TargetSkills AS (
                    -- Bước 1: Lấy danh sách ID skill duy nhất của các Card cần tìm
                    SELECT DISTINCT us.skill_id
                    FROM user_skills us
                    JOIN card_spells_skills chs ON chs.skill_id = us.skill_id
                    WHERE us.user_id = @userId
                    AND chs.card_spell_id IN ({inClause})
                ),
                BaseEffects AS (
                    -- Bước 2: Lấy thông tin cơ bản của Effect trước để tránh nhân dòng chéo
                    SELECT 
                        se.skill_id,
                        se.effect_id,
                        se.min_value,
                        se.max_value,
                        se.trigger_phase,
                        se.trigger_condition,
                        se.is_stackable,
                        se.is_removable,
                        se.target_id,
                        e.name AS effect_name,
                        e.effect_type,
                        e.duration,
                        e.description AS effect_description
                    FROM skill_effect se
                    JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                    WHERE se.skill_id IN (SELECT skill_id FROM TargetSkills)
                ),
                AggregatedEffects AS (
                    -- Bước 3: Gom nhóm JSON. Lúc này dữ liệu đã gọn, không bị nhân dòng ảo
                    SELECT 
                        be.skill_id,
                        JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', be.min_value,
                                'max_value', be.max_value,
                                'trigger_phase', be.trigger_phase,
                                'trigger_condition', be.trigger_condition,
                                'is_stackable', be.is_stackable,
                                'is_removable', be.is_removable,
                                'target_id', be.target_id,
                                'effect_id', be.effect_id,
                                'effect_name', be.effect_name,
                                'effect_type', be.effect_type,
                                'duration', be.duration,
                                'effect_description', be.effect_description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        ) AS skill_effects_json
                    FROM BaseEffects be
                    LEFT JOIN effect_property_action epa ON be.effect_id = epa.effect_id
                    LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                    LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                    GROUP BY be.skill_id
                )
                -- Bước 4: Xuất dữ liệu cuối cùng phẳng, gom gọn theo card_spell_id
                SELECT 
                    us.skill_id,
                    s.name, 
                    s.image, 
                    us.rare, 
                    us.star, 
                    us.level, 
                    us.experience, 
                    us.quality, 
                    us.quantity,
                    s.type,
                    s.skill_type,
                    s.skill_sub_type,
                    chs.position AS skill_position, 
                    sp.pattern_id, 
                    chs.card_spell_id,
                    ae.skill_effects_json
                FROM card_spells_skills chs
                JOIN user_skills us ON chs.skill_id = us.skill_id AND us.user_id = @userId
                JOIN Skills s ON chs.skill_id = s.id
                LEFT JOIN skill_patterns sp ON chs.skill_id = sp.skill_id
                LEFT JOIN AggregatedEffects ae ON chs.skill_id = ae.skill_id
                WHERE chs.card_spell_id IN ({inClause});";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);

                // Gán tham số userId cố định
                selectCommand.Parameters.AddWithValue("@userId", userId);

                // Gán các tham số động từ danh sách mảng heroIds
                for (int i = 0; i < cardSpellIds.Count; i++)
                {
                    selectCommand.Parameters.AddWithValue(paramNames[i], cardSpellIds[i]);
                }

                // 3. Thực thi và đọc dữ liệu
                await using var reader = await selectCommand.ExecuteReaderAsync();

                // --- MẸO TỐI ƯU ---
                // Đọc trước cấu trúc các cột trả về và lưu tên cột viết thường (lowercase) vào một Dictionary mapping Index.
                // Điều này giúp driver ADO.NET (MySqlConnector) tìm kiếm trực tiếp bằng bộ nhớ cache cực nhanh bên trong thay vì quét chuỗi.
                var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnMap[reader.GetName(i)] = i;
                }

                // Hàm Helper nội bộ (Local Function) để lấy nhanh tên cột chuẩn từ database nhằm tránh việc driver đi quét chuỗi liên tục.
                // Hàm này sẽ trả về đúng tên cột gốc (đã được cache vị trí) giúp tối ưu hóa 100% cho các hàm GetXXXSafe của bạn.
                string GetCol(string columnName)
                {
                    return columnMap.TryGetValue(columnName, out int index) ? reader.GetName(index) : columnName;
                }

                // ==========================================
                // 1. LẤY VÀ CACHE SẴN INDEX CỦA TẤT CẢ CÁC CỘT (Chỉ chạy đúng 1 lần trước vòng lặp)
                // ==========================================
                int colSkillId = reader.GetOrdinal(GetCol("skill_id"));
                int colName = reader.GetOrdinal(GetCol("name"));
                int colImage = reader.GetOrdinal(GetCol("image"));
                int colRare = reader.GetOrdinal(GetCol("rare"));
                int colQuality = reader.GetOrdinal(GetCol("quality"));
                int colType = reader.GetOrdinal(GetCol("type"));
                int colStar = reader.GetOrdinal(GetCol("star"));
                int colLevel = reader.GetOrdinal(GetCol("level"));
                int colSkillPosition = reader.GetOrdinal(GetCol("skill_position"));
                int colSkillType = reader.GetOrdinal(GetCol("skill_type"));
                int colExperience = reader.GetOrdinal(GetCol("experience"));
                int colQuantity = reader.GetOrdinal(GetCol("quantity"));
                // int colPower = reader.GetOrdinal(GetCol("power"));
                // int colHealth = reader.GetOrdinal(GetCol("health"));
                // int colPhysicalAttack = reader.GetOrdinal(GetCol("physical_attack"));
                // int colPhysicalDefense = reader.GetOrdinal(GetCol("physical_defense"));
                // int colMagicalAttack = reader.GetOrdinal(GetCol("magical_attack"));
                // int colMagicalDefense = reader.GetOrdinal(GetCol("magical_defense"));
                // int colChemicalAttack = reader.GetOrdinal(GetCol("chemical_attack"));
                // int colChemicalDefense = reader.GetOrdinal(GetCol("chemical_defense"));
                // int colAtomicAttack = reader.GetOrdinal(GetCol("atomic_attack"));
                // int colAtomicDefense = reader.GetOrdinal(GetCol("atomic_defense"));
                // int colMentalAttack = reader.GetOrdinal(GetCol("mental_attack"));
                // int colMentalDefense = reader.GetOrdinal(GetCol("mental_defense"));
                // int colSpeed = reader.GetOrdinal(GetCol("speed"));
                // int colCriticalDamageRate = reader.GetOrdinal(GetCol("critical_damage_rate"));
                // int colCriticalRate = reader.GetOrdinal(GetCol("critical_rate"));
                // int colCriticalResistanceRate = reader.GetOrdinal(GetCol("critical_resistance_rate"));
                // int colIgnoreCriticalRate = reader.GetOrdinal(GetCol("ignore_critical_rate"));
                // int colPenetrationRate = reader.GetOrdinal(GetCol("penetration_rate"));
                // int colPenetrationResistanceRate = reader.GetOrdinal(GetCol("penetration_resistance_rate"));
                // int colEvasionRate = reader.GetOrdinal(GetCol("evasion_rate"));
                // int colDamageAbsorptionRate = reader.GetOrdinal(GetCol("damage_absorption_rate"));
                // int colIgnoreDamageAbsorptionRate = reader.GetOrdinal(GetCol("ignore_damage_absorption_rate"));
                // int colAbsorbedDamageRate = reader.GetOrdinal(GetCol("absorbed_damage_rate"));
                // int colVitalityRegenerationRate = reader.GetOrdinal(GetCol("vitality_regeneration_rate"));
                // int colVitalityRegenerationResistanceRate = reader.GetOrdinal(GetCol("vitality_regeneration_resistance_rate"));
                // int colAccuracyRate = reader.GetOrdinal(GetCol("accuracy_rate"));
                // int colLifestealRate = reader.GetOrdinal(GetCol("lifesteal_rate"));
                // int colShieldStrength = reader.GetOrdinal(GetCol("shield_strength"));
                // int colTenacity = reader.GetOrdinal(GetCol("tenacity"));
                // int colResistanceRate = reader.GetOrdinal(GetCol("resistance_rate"));
                // int colComboRate = reader.GetOrdinal(GetCol("combo_rate"));
                // int colIgnoreComboRate = reader.GetOrdinal(GetCol("ignore_combo_rate"));
                // int colComboDamageRate = reader.GetOrdinal(GetCol("combo_damage_rate"));
                // int colComboResistanceRate = reader.GetOrdinal(GetCol("combo_resistance_rate"));
                // int colStunRate = reader.GetOrdinal(GetCol("stun_rate"));
                // int colIgnoreStunRate = reader.GetOrdinal(GetCol("ignore_stun_rate"));
                // int colReflectionRate = reader.GetOrdinal(GetCol("reflection_rate"));
                // int colIgnoreReflectionRate = reader.GetOrdinal(GetCol("ignore_reflection_rate"));
                // int colReflectionDamageRate = reader.GetOrdinal(GetCol("reflection_damage_rate"));
                // int colReflectionResistanceRate = reader.GetOrdinal(GetCol("reflection_resistance_rate"));
                // int colMana = reader.GetOrdinal(GetCol("mana"));
                // int colManaRegenerationRate = reader.GetOrdinal(GetCol("mana_regeneration_rate"));
                // int colDamageToDifferentFactionRate = reader.GetOrdinal(GetCol("damage_to_different_faction_rate"));
                // int colResistanceToDifferentFactionRate = reader.GetOrdinal(GetCol("resistance_to_different_faction_rate"));
                // int colDamageToSameFactionRate = reader.GetOrdinal(GetCol("damage_to_same_faction_rate"));
                // int colResistanceToSameFactionRate = reader.GetOrdinal(GetCol("resistance_to_same_faction_rate"));
                // int colNormalDamageRate = reader.GetOrdinal(GetCol("normal_damage_rate"));
                // int colNormalResistanceRate = reader.GetOrdinal(GetCol("normal_resistance_rate"));
                // int colSkillDamageRate = reader.GetOrdinal(GetCol("skill_damage_rate"));
                // int colSkillResistanceRate = reader.GetOrdinal(GetCol("skill_resistance_rate"));
                // int colDescription = reader.GetOrdinal(GetCol("description"));
                int colCardSpellId = reader.GetOrdinal(GetCol("card_spell_id"));
                int colPatternId = reader.GetOrdinal(GetCol("pattern_id"));
                int colSkillSubType = reader.GetOrdinal(GetCol("skill_sub_type"));
                int colSkillEffectsJson = reader.GetOrdinal(GetCol("skill_effects_json"));

                // ==========================================
                // 2. VÒNG LẶP ĐỌC DỮ LIỆU CỰC NHANH VỚI INDEX (ORDINAL)
                // ==========================================
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe(colSkillId),
                        Name = reader.GetStringSafe(colName),
                        Image = reader.GetStringSafe(colImage),
                        Rarity = reader.GetStringSafe(colRare),
                        Quality = reader.GetDoubleSafe(colQuality),
                        Type = reader.GetStringSafe(colType),
                        Star = reader.GetIntSafe(colStar),
                        Level = reader.GetIntSafe(colLevel),
                        Position = reader.GetIntSafe(colSkillPosition),
                        SkillType = reader.GetStringSafe(colSkillType),
                        Experience = reader.GetDoubleSafe(colExperience),
                        Quantity = reader.GetIntSafe(colQuantity),
                        // Power = reader.GetDoubleSafe(colPower),
                        // Health = reader.GetDoubleSafe(colHealth),
                        // PhysicalAttack = reader.GetDoubleSafe(colPhysicalAttack),
                        // PhysicalDefense = reader.GetDoubleSafe(colPhysicalDefense),
                        // MagicalAttack = reader.GetDoubleSafe(colMagicalAttack),
                        // MagicalDefense = reader.GetDoubleSafe(colMagicalDefense),
                        // ChemicalAttack = reader.GetDoubleSafe(colChemicalAttack),
                        // ChemicalDefense = reader.GetDoubleSafe(colChemicalDefense),
                        // AtomicAttack = reader.GetDoubleSafe(colAtomicAttack),
                        // AtomicDefense = reader.GetDoubleSafe(colAtomicDefense),
                        // MentalAttack = reader.GetDoubleSafe(colMentalAttack),
                        // MentalDefense = reader.GetDoubleSafe(colMentalDefense),
                        // Speed = reader.GetDoubleSafe(colSpeed),
                        // CriticalDamageRate = reader.GetDoubleSafe(colCriticalDamageRate),
                        // CriticalRate = reader.GetDoubleSafe(colCriticalRate),
                        // CriticalResistanceRate = reader.GetDoubleSafe(colCriticalResistanceRate),
                        // IgnoreCriticalRate = reader.GetDoubleSafe(colIgnoreCriticalRate),
                        // PenetrationRate = reader.GetDoubleSafe(colPenetrationRate),
                        // PenetrationResistanceRate = reader.GetDoubleSafe(colPenetrationResistanceRate),
                        // EvasionRate = reader.GetDoubleSafe(colEvasionRate),
                        // DamageAbsorptionRate = reader.GetDoubleSafe(colDamageAbsorptionRate),
                        // IgnoreDamageAbsorptionRate = reader.GetDoubleSafe(colIgnoreDamageAbsorptionRate),
                        // AbsorbedDamageRate = reader.GetDoubleSafe(colAbsorbedDamageRate),
                        // VitalityRegenerationRate = reader.GetDoubleSafe(colVitalityRegenerationRate),
                        // VitalityRegenerationResistanceRate = reader.GetDoubleSafe(colVitalityRegenerationResistanceRate),
                        // AccuracyRate = reader.GetDoubleSafe(colAccuracyRate),
                        // LifestealRate = reader.GetDoubleSafe(colLifestealRate),
                        // ShieldStrength = reader.GetDoubleSafe(colShieldStrength),
                        // Tenacity = reader.GetDoubleSafe(colTenacity),
                        // ResistanceRate = reader.GetDoubleSafe(colResistanceRate),
                        // ComboRate = reader.GetDoubleSafe(colComboRate),
                        // IgnoreComboRate = reader.GetDoubleSafe(colIgnoreComboRate),
                        // ComboDamageRate = reader.GetDoubleSafe(colComboDamageRate),
                        // ComboResistanceRate = reader.GetDoubleSafe(colComboResistanceRate),
                        // StunRate = reader.GetDoubleSafe(colStunRate),
                        // IgnoreStunRate = reader.GetDoubleSafe(colIgnoreStunRate),
                        // ReflectionRate = reader.GetDoubleSafe(colReflectionRate),
                        // IgnoreReflectionRate = reader.GetDoubleSafe(colIgnoreReflectionRate),
                        // ReflectionDamageRate = reader.GetDoubleSafe(colReflectionDamageRate),
                        // ReflectionResistanceRate = reader.GetDoubleSafe(colReflectionResistanceRate),
                        // Mana = reader.GetDoubleSafe(colMana),
                        // ManaRegenerationRate = reader.GetDoubleSafe(colManaRegenerationRate),
                        // DamageToDifferentFactionRate = reader.GetDoubleSafe(colDamageToDifferentFactionRate),
                        // ResistanceToDifferentFactionRate = reader.GetDoubleSafe(colResistanceToDifferentFactionRate),
                        // DamageToSameFactionRate = reader.GetDoubleSafe(colDamageToSameFactionRate),
                        // ResistanceToSameFactionRate = reader.GetDoubleSafe(colResistanceToSameFactionRate),
                        // NormalDamageRate = reader.GetDoubleSafe(colNormalDamageRate),
                        // NormalResistanceRate = reader.GetDoubleSafe(colNormalResistanceRate),
                        // SkillDamageRate = reader.GetDoubleSafe(colSkillDamageRate),
                        // SkillResistanceRate = reader.GetDoubleSafe(colSkillResistanceRate),
                        // Description = reader.GetStringSafe(colDescription),

                        CardId = reader.GetStringSafe(colCardSpellId),
                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe(colPatternId)
                        },
                        SkillSubType = new SkillSubTypes
                        {
                            SubTypeCode = reader.GetStringSafe(colSkillSubType)
                        }
                    };

                    string effectsJson = reader.GetStringSafe(colSkillEffectsJson);

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                // Đóng reader sớm để giải phóng tài nguyên mạng trước khi CPU thực hiện giải mã JSON
                await reader.CloseAsync();

                // ==========================================
                // TỐI ƯU HÓA: GIẢI MÃ JSON SONG SONG BẰNG JSONHELPER CỦA BẠN
                // ==========================================
                if (pendingJsonList.Count > 0)
                {
                    Parallel.ForEach(pendingJsonList, item =>
                    {
                        try
                        {
                            item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                            item.skill.Effects = new List<Effects>(); // Fallback an toàn
                        }
                    });
                }

                // Load Effects cho toàn bộ Skills cùng một lúc
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            // block 'finally' không cần CloseAsync() thủ công nữa vì 'await using' đã tự động xử lý giải phóng kết nối một cách an toàn.
        }

        return skills;
    }
    public async Task<List<Skills>> GetUserCardSoldiersSkillsAsync(string userId, List<string> cardSoldierIds)
    {
        List<Skills> skills = new List<Skills>();
        List<(Skills skill, string jsonRaw)> pendingJsonList = new List<(Skills, string)>();

        // Kiểm tra danh sách đầu vào để tránh lỗi SQL khi danh sách rỗng
        if (cardSoldierIds == null || cardSoldierIds.Count == 0)
        {
            return skills;
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // 1. Tạo danh sách tham số dạng @heroId0, @heroId1,... động để tránh SQL Injection
                var paramNames = new List<string>();
                for (int i = 0; i < cardSoldierIds.Count; i++)
                {
                    paramNames.Add($"@cardSoldierId{i}");
                }

                // Nối các tên tham số lại thành chuỗi: "@heroId0, @heroId1, @heroId2"
                string inClause = string.Join(", ", paramNames);

                // 2. Cập nhật lại SQL Query với điều kiện IN danh sách các Hero ID
                // Sửa lại logic JOIN chính xác: chs.card_hero_id IN (...) thay vì gán nhầm vào skill_id
                string selectSQL = $@"
                WITH TargetSkills AS (
                    -- Bước 1: Lấy danh sách ID skill duy nhất của các Card cần tìm
                    SELECT DISTINCT us.skill_id
                    FROM user_skills us
                    JOIN card_soldiers_skills chs ON chs.skill_id = us.skill_id
                    WHERE us.user_id = @userId
                    AND chs.card_soldier_id IN ({inClause})
                ),
                BaseEffects AS (
                    -- Bước 2: Lấy thông tin cơ bản của Effect trước để tránh nhân dòng chéo
                    SELECT 
                        se.skill_id,
                        se.effect_id,
                        se.min_value,
                        se.max_value,
                        se.trigger_phase,
                        se.trigger_condition,
                        se.is_stackable,
                        se.is_removable,
                        se.target_id,
                        e.name AS effect_name,
                        e.effect_type,
                        e.duration,
                        e.description AS effect_description
                    FROM skill_effect se
                    JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                    WHERE se.skill_id IN (SELECT skill_id FROM TargetSkills)
                ),
                AggregatedEffects AS (
                    -- Bước 3: Gom nhóm JSON. Lúc này dữ liệu đã gọn, không bị nhân dòng ảo
                    SELECT 
                        be.skill_id,
                        JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', be.min_value,
                                'max_value', be.max_value,
                                'trigger_phase', be.trigger_phase,
                                'trigger_condition', be.trigger_condition,
                                'is_stackable', be.is_stackable,
                                'is_removable', be.is_removable,
                                'target_id', be.target_id,
                                'effect_id', be.effect_id,
                                'effect_name', be.effect_name,
                                'effect_type', be.effect_type,
                                'duration', be.duration,
                                'effect_description', be.effect_description,
                                'value_type', epa.value_type,
                                'value', epa.value,
                                'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code,
                                'property_name', ep.property_name,
                                'action_code', ea.action_code,
                                'action_name', ea.action_name
                            )
                        ) AS skill_effects_json
                    FROM BaseEffects be
                    LEFT JOIN effect_property_action epa ON be.effect_id = epa.effect_id
                    LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                    LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                    GROUP BY be.skill_id
                )
                -- Bước 4: Xuất dữ liệu cuối cùng phẳng, gom gọn theo card_soldier_id
                SELECT 
                    us.skill_id,
                    s.name, 
                    s.image, 
                    us.rare, 
                    us.star, 
                    us.level, 
                    us.experience, 
                    us.quality, 
                    us.quantity,
                    s.type,
                    s.skill_type,
                    s.skill_sub_type,
                    chs.position AS skill_position, 
                    sp.pattern_id, 
                    chs.card_soldier_id,
                    ae.skill_effects_json
                FROM card_soldiers_skills chs
                JOIN user_skills us ON chs.skill_id = us.skill_id AND us.user_id = @userId
                JOIN Skills s ON chs.skill_id = s.id
                LEFT JOIN skill_patterns sp ON chs.skill_id = sp.skill_id
                LEFT JOIN AggregatedEffects ae ON chs.skill_id = ae.skill_id
                WHERE chs.card_soldier_id IN ({inClause});";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);

                // Gán tham số userId cố định
                selectCommand.Parameters.AddWithValue("@userId", userId);

                // Gán các tham số động từ danh sách mảng heroIds
                for (int i = 0; i < cardSoldierIds.Count; i++)
                {
                    selectCommand.Parameters.AddWithValue(paramNames[i], cardSoldierIds[i]);
                }

                // 3. Thực thi và đọc dữ liệu
                await using var reader = await selectCommand.ExecuteReaderAsync();

                // --- MẸO TỐI ƯU ---
                // Đọc trước cấu trúc các cột trả về và lưu tên cột viết thường (lowercase) vào một Dictionary mapping Index.
                // Điều này giúp driver ADO.NET (MySqlConnector) tìm kiếm trực tiếp bằng bộ nhớ cache cực nhanh bên trong thay vì quét chuỗi.
                var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnMap[reader.GetName(i)] = i;
                }

                // Hàm Helper nội bộ (Local Function) để lấy nhanh tên cột chuẩn từ database nhằm tránh việc driver đi quét chuỗi liên tục.
                // Hàm này sẽ trả về đúng tên cột gốc (đã được cache vị trí) giúp tối ưu hóa 100% cho các hàm GetXXXSafe của bạn.
                string GetCol(string columnName)
                {
                    return columnMap.TryGetValue(columnName, out int index) ? reader.GetName(index) : columnName;
                }

                // ==========================================
                // 1. LẤY VÀ CACHE SẴN INDEX CỦA TẤT CẢ CÁC CỘT (Chỉ chạy đúng 1 lần trước vòng lặp)
                // ==========================================
                int colSkillId = reader.GetOrdinal(GetCol("skill_id"));
                int colName = reader.GetOrdinal(GetCol("name"));
                int colImage = reader.GetOrdinal(GetCol("image"));
                int colRare = reader.GetOrdinal(GetCol("rare"));
                int colQuality = reader.GetOrdinal(GetCol("quality"));
                int colType = reader.GetOrdinal(GetCol("type"));
                int colStar = reader.GetOrdinal(GetCol("star"));
                int colLevel = reader.GetOrdinal(GetCol("level"));
                int colSkillPosition = reader.GetOrdinal(GetCol("skill_position"));
                int colSkillType = reader.GetOrdinal(GetCol("skill_type"));
                int colExperience = reader.GetOrdinal(GetCol("experience"));
                int colQuantity = reader.GetOrdinal(GetCol("quantity"));
                // int colPower = reader.GetOrdinal(GetCol("power"));
                // int colHealth = reader.GetOrdinal(GetCol("health"));
                // int colPhysicalAttack = reader.GetOrdinal(GetCol("physical_attack"));
                // int colPhysicalDefense = reader.GetOrdinal(GetCol("physical_defense"));
                // int colMagicalAttack = reader.GetOrdinal(GetCol("magical_attack"));
                // int colMagicalDefense = reader.GetOrdinal(GetCol("magical_defense"));
                // int colChemicalAttack = reader.GetOrdinal(GetCol("chemical_attack"));
                // int colChemicalDefense = reader.GetOrdinal(GetCol("chemical_defense"));
                // int colAtomicAttack = reader.GetOrdinal(GetCol("atomic_attack"));
                // int colAtomicDefense = reader.GetOrdinal(GetCol("atomic_defense"));
                // int colMentalAttack = reader.GetOrdinal(GetCol("mental_attack"));
                // int colMentalDefense = reader.GetOrdinal(GetCol("mental_defense"));
                // int colSpeed = reader.GetOrdinal(GetCol("speed"));
                // int colCriticalDamageRate = reader.GetOrdinal(GetCol("critical_damage_rate"));
                // int colCriticalRate = reader.GetOrdinal(GetCol("critical_rate"));
                // int colCriticalResistanceRate = reader.GetOrdinal(GetCol("critical_resistance_rate"));
                // int colIgnoreCriticalRate = reader.GetOrdinal(GetCol("ignore_critical_rate"));
                // int colPenetrationRate = reader.GetOrdinal(GetCol("penetration_rate"));
                // int colPenetrationResistanceRate = reader.GetOrdinal(GetCol("penetration_resistance_rate"));
                // int colEvasionRate = reader.GetOrdinal(GetCol("evasion_rate"));
                // int colDamageAbsorptionRate = reader.GetOrdinal(GetCol("damage_absorption_rate"));
                // int colIgnoreDamageAbsorptionRate = reader.GetOrdinal(GetCol("ignore_damage_absorption_rate"));
                // int colAbsorbedDamageRate = reader.GetOrdinal(GetCol("absorbed_damage_rate"));
                // int colVitalityRegenerationRate = reader.GetOrdinal(GetCol("vitality_regeneration_rate"));
                // int colVitalityRegenerationResistanceRate = reader.GetOrdinal(GetCol("vitality_regeneration_resistance_rate"));
                // int colAccuracyRate = reader.GetOrdinal(GetCol("accuracy_rate"));
                // int colLifestealRate = reader.GetOrdinal(GetCol("lifesteal_rate"));
                // int colShieldStrength = reader.GetOrdinal(GetCol("shield_strength"));
                // int colTenacity = reader.GetOrdinal(GetCol("tenacity"));
                // int colResistanceRate = reader.GetOrdinal(GetCol("resistance_rate"));
                // int colComboRate = reader.GetOrdinal(GetCol("combo_rate"));
                // int colIgnoreComboRate = reader.GetOrdinal(GetCol("ignore_combo_rate"));
                // int colComboDamageRate = reader.GetOrdinal(GetCol("combo_damage_rate"));
                // int colComboResistanceRate = reader.GetOrdinal(GetCol("combo_resistance_rate"));
                // int colStunRate = reader.GetOrdinal(GetCol("stun_rate"));
                // int colIgnoreStunRate = reader.GetOrdinal(GetCol("ignore_stun_rate"));
                // int colReflectionRate = reader.GetOrdinal(GetCol("reflection_rate"));
                // int colIgnoreReflectionRate = reader.GetOrdinal(GetCol("ignore_reflection_rate"));
                // int colReflectionDamageRate = reader.GetOrdinal(GetCol("reflection_damage_rate"));
                // int colReflectionResistanceRate = reader.GetOrdinal(GetCol("reflection_resistance_rate"));
                // int colMana = reader.GetOrdinal(GetCol("mana"));
                // int colManaRegenerationRate = reader.GetOrdinal(GetCol("mana_regeneration_rate"));
                // int colDamageToDifferentFactionRate = reader.GetOrdinal(GetCol("damage_to_different_faction_rate"));
                // int colResistanceToDifferentFactionRate = reader.GetOrdinal(GetCol("resistance_to_different_faction_rate"));
                // int colDamageToSameFactionRate = reader.GetOrdinal(GetCol("damage_to_same_faction_rate"));
                // int colResistanceToSameFactionRate = reader.GetOrdinal(GetCol("resistance_to_same_faction_rate"));
                // int colNormalDamageRate = reader.GetOrdinal(GetCol("normal_damage_rate"));
                // int colNormalResistanceRate = reader.GetOrdinal(GetCol("normal_resistance_rate"));
                // int colSkillDamageRate = reader.GetOrdinal(GetCol("skill_damage_rate"));
                // int colSkillResistanceRate = reader.GetOrdinal(GetCol("skill_resistance_rate"));
                // int colDescription = reader.GetOrdinal(GetCol("description"));
                int colCardSoldierId = reader.GetOrdinal(GetCol("card_soldier_id"));
                int colPatternId = reader.GetOrdinal(GetCol("pattern_id"));
                int colSkillSubType = reader.GetOrdinal(GetCol("skill_sub_type"));
                int colSkillEffectsJson = reader.GetOrdinal(GetCol("skill_effects_json"));

                // ==========================================
                // 2. VÒNG LẶP ĐỌC DỮ LIỆU CỰC NHANH VỚI INDEX (ORDINAL)
                // ==========================================
                while (await reader.ReadAsync())
                {
                    Skills skill = new Skills
                    {
                        Id = reader.GetStringSafe(colSkillId),
                        Name = reader.GetStringSafe(colName),
                        Image = reader.GetStringSafe(colImage),
                        Rarity = reader.GetStringSafe(colRare),
                        Quality = reader.GetDoubleSafe(colQuality),
                        Type = reader.GetStringSafe(colType),
                        Star = reader.GetIntSafe(colStar),
                        Level = reader.GetIntSafe(colLevel),
                        Position = reader.GetIntSafe(colSkillPosition),
                        SkillType = reader.GetStringSafe(colSkillType),
                        Experience = reader.GetDoubleSafe(colExperience),
                        Quantity = reader.GetIntSafe(colQuantity),
                        // Power = reader.GetDoubleSafe(colPower),
                        // Health = reader.GetDoubleSafe(colHealth),
                        // PhysicalAttack = reader.GetDoubleSafe(colPhysicalAttack),
                        // PhysicalDefense = reader.GetDoubleSafe(colPhysicalDefense),
                        // MagicalAttack = reader.GetDoubleSafe(colMagicalAttack),
                        // MagicalDefense = reader.GetDoubleSafe(colMagicalDefense),
                        // ChemicalAttack = reader.GetDoubleSafe(colChemicalAttack),
                        // ChemicalDefense = reader.GetDoubleSafe(colChemicalDefense),
                        // AtomicAttack = reader.GetDoubleSafe(colAtomicAttack),
                        // AtomicDefense = reader.GetDoubleSafe(colAtomicDefense),
                        // MentalAttack = reader.GetDoubleSafe(colMentalAttack),
                        // MentalDefense = reader.GetDoubleSafe(colMentalDefense),
                        // Speed = reader.GetDoubleSafe(colSpeed),
                        // CriticalDamageRate = reader.GetDoubleSafe(colCriticalDamageRate),
                        // CriticalRate = reader.GetDoubleSafe(colCriticalRate),
                        // CriticalResistanceRate = reader.GetDoubleSafe(colCriticalResistanceRate),
                        // IgnoreCriticalRate = reader.GetDoubleSafe(colIgnoreCriticalRate),
                        // PenetrationRate = reader.GetDoubleSafe(colPenetrationRate),
                        // PenetrationResistanceRate = reader.GetDoubleSafe(colPenetrationResistanceRate),
                        // EvasionRate = reader.GetDoubleSafe(colEvasionRate),
                        // DamageAbsorptionRate = reader.GetDoubleSafe(colDamageAbsorptionRate),
                        // IgnoreDamageAbsorptionRate = reader.GetDoubleSafe(colIgnoreDamageAbsorptionRate),
                        // AbsorbedDamageRate = reader.GetDoubleSafe(colAbsorbedDamageRate),
                        // VitalityRegenerationRate = reader.GetDoubleSafe(colVitalityRegenerationRate),
                        // VitalityRegenerationResistanceRate = reader.GetDoubleSafe(colVitalityRegenerationResistanceRate),
                        // AccuracyRate = reader.GetDoubleSafe(colAccuracyRate),
                        // LifestealRate = reader.GetDoubleSafe(colLifestealRate),
                        // ShieldStrength = reader.GetDoubleSafe(colShieldStrength),
                        // Tenacity = reader.GetDoubleSafe(colTenacity),
                        // ResistanceRate = reader.GetDoubleSafe(colResistanceRate),
                        // ComboRate = reader.GetDoubleSafe(colComboRate),
                        // IgnoreComboRate = reader.GetDoubleSafe(colIgnoreComboRate),
                        // ComboDamageRate = reader.GetDoubleSafe(colComboDamageRate),
                        // ComboResistanceRate = reader.GetDoubleSafe(colComboResistanceRate),
                        // StunRate = reader.GetDoubleSafe(colStunRate),
                        // IgnoreStunRate = reader.GetDoubleSafe(colIgnoreStunRate),
                        // ReflectionRate = reader.GetDoubleSafe(colReflectionRate),
                        // IgnoreReflectionRate = reader.GetDoubleSafe(colIgnoreReflectionRate),
                        // ReflectionDamageRate = reader.GetDoubleSafe(colReflectionDamageRate),
                        // ReflectionResistanceRate = reader.GetDoubleSafe(colReflectionResistanceRate),
                        // Mana = reader.GetDoubleSafe(colMana),
                        // ManaRegenerationRate = reader.GetDoubleSafe(colManaRegenerationRate),
                        // DamageToDifferentFactionRate = reader.GetDoubleSafe(colDamageToDifferentFactionRate),
                        // ResistanceToDifferentFactionRate = reader.GetDoubleSafe(colResistanceToDifferentFactionRate),
                        // DamageToSameFactionRate = reader.GetDoubleSafe(colDamageToSameFactionRate),
                        // ResistanceToSameFactionRate = reader.GetDoubleSafe(colResistanceToSameFactionRate),
                        // NormalDamageRate = reader.GetDoubleSafe(colNormalDamageRate),
                        // NormalResistanceRate = reader.GetDoubleSafe(colNormalResistanceRate),
                        // SkillDamageRate = reader.GetDoubleSafe(colSkillDamageRate),
                        // SkillResistanceRate = reader.GetDoubleSafe(colSkillResistanceRate),
                        // Description = reader.GetStringSafe(colDescription),

                        CardId = reader.GetStringSafe(colCardSoldierId),
                        Pattern = new Patterns()
                        {
                            Id = reader.GetStringSafe(colPatternId)
                        },
                        SkillSubType = new SkillSubTypes
                        {
                            SubTypeCode = reader.GetStringSafe(colSkillSubType)
                        }
                    };

                    string effectsJson = reader.GetStringSafe(colSkillEffectsJson);

                    if (!string.IsNullOrEmpty(effectsJson))
                    {
                        pendingJsonList.Add((skill, effectsJson));
                    }
                    else
                    {
                        skill.Effects = new List<Effects>(); // Khởi tạo danh sách rỗng nếu không có dữ liệu
                    }

                    skills.Add(skill);
                }

                // Đóng reader sớm để giải phóng tài nguyên mạng trước khi CPU thực hiện giải mã JSON
                await reader.CloseAsync();

                // ==========================================
                // TỐI ƯU HÓA: GIẢI MÃ JSON SONG SONG BẰNG JSONHELPER CỦA BẠN
                // ==========================================
                if (pendingJsonList.Count > 0)
                {
                    Parallel.ForEach(pendingJsonList, item =>
                    {
                        try
                        {
                            item.skill.Effects = JsonHelper.DeserializeEffects(item.jsonRaw);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[JSON Parse Error for Skill {item.skill.Id}]: {ex.Message}");
                            item.skill.Effects = new List<Effects>(); // Fallback an toàn
                        }
                    });
                }

                // Load Effects cho toàn bộ Skills cùng một lúc
                // skills = await LoadSkillsWithEffectsAsync(userId, skills, connection);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            // block 'finally' không cần CloseAsync() thủ công nữa vì 'await using' đã tự động xử lý giải phóng kết nối một cách an toàn.
        }

        return skills;
    }
    public async Task<List<Skills>> GetUserSkillsWithCardsAsync(
        string userId,
        List<string> heroIds,
        List<string> captainIds,
        List<string> colonelIds,
        List<string> generalIds,
        List<string> admiralIds,
        List<string> monsterIds,
        List<string> militaryIds,
        List<string> spellIds,
        List<string> soldierIds)
    {
        var skillList = new List<Skills>();

        try
        {
            var pendingJsonList = new List<(Skills skill, string effectsJson, string heroJson, string captainJson, string colonelJson, string generalJson, string admiralJson, string monsterJson, string militaryJson, string spellJson, string soldierJson)>();
            string connectionString = DatabaseConfig.ConnectionString;

            // 1. Khởi tạo danh sách tham số để add động vào command về sau
            var parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@UserId", userId)
            };

            // 2. Build các chuỗi IN Clause động cho 9 loại thẻ bằng hàm Helper
            string heroIn = BuildInClause(heroIds, "Hero", parameters);
            string captainIn = BuildInClause(captainIds, "Captain", parameters);
            string colonelIn = BuildInClause(colonelIds, "Colonel", parameters);
            string generalIn = BuildInClause(generalIds, "General", parameters);
            string admiralIn = BuildInClause(admiralIds, "Admiral", parameters);
            string monsterIn = BuildInClause(monsterIds, "Monster", parameters);
            string militaryIn = BuildInClause(militaryIds, "Military", parameters);
            string spellIn = BuildInClause(spellIds, "Spell", parameters);
            string soldierIn = BuildInClause(soldierIds, "Soldier", parameters);

            // 3. Khởi tạo câu truy vấn gốc sử dụng các chuỗi IN clause đã build động an toàn
            string query = $@"
                WITH TargetSkillsRaw AS (
                    -- Gom tất cả card_id, position và loại card tương ứng về một mối ngay từ đầu
                    SELECT us.skill_id, c.card_hero_id AS card_id, 'hero' AS card_type, c.position
                    FROM user_skills us 
                    JOIN card_heroes_skills c ON us.skill_id = c.skill_id AND us.user_id = c.user_id
                    WHERE us.user_id = @UserId AND c.card_hero_id IN ({heroIn})
                    
                    UNION ALL
                    SELECT us.skill_id, c.card_captain_id, 'captain', c.position
                    FROM user_skills us 
                    JOIN card_captains_skills c ON us.skill_id = c.skill_id AND us.user_id = c.user_id
                    WHERE us.user_id = @UserId AND c.card_captain_id IN ({captainIn})
                    
                    UNION ALL
                    SELECT us.skill_id, c.card_colonel_id, 'colonel', c.position
                    FROM user_skills us 
                    JOIN card_colonels_skills c ON us.skill_id = c.skill_id AND us.user_id = c.user_id
                    WHERE us.user_id = @UserId AND c.card_colonel_id IN ({colonelIn})
                    
                    UNION ALL
                    SELECT us.skill_id, c.card_general_id, 'general', c.position
                    FROM user_skills us 
                    JOIN card_generals_skills c ON us.skill_id = c.skill_id AND us.user_id = c.user_id
                    WHERE us.user_id = @UserId AND c.card_general_id IN ({generalIn})
                    
                    UNION ALL
                    SELECT us.skill_id, c.card_admiral_id, 'admiral', c.position
                    FROM user_skills us 
                    JOIN card_admirals_skills c ON us.skill_id = c.skill_id AND us.user_id = c.user_id
                    WHERE us.user_id = @UserId AND c.card_admiral_id IN ({admiralIn})
                    
                    UNION ALL
                    SELECT us.skill_id, c.card_monster_id, 'monster', c.position
                    FROM user_skills us 
                    JOIN card_monsters_skills c ON us.skill_id = c.skill_id AND us.user_id = c.user_id
                    WHERE us.user_id = @UserId AND c.card_monster_id IN ({monsterIn})
                    
                    UNION ALL
                    SELECT us.skill_id, c.card_military_id, 'military', c.position
                    FROM user_skills us 
                    JOIN card_militaries_skills c ON us.skill_id = c.skill_id AND us.user_id = c.user_id
                    WHERE us.user_id = @UserId AND c.card_military_id IN ({militaryIn})
                    
                    UNION ALL
                    SELECT us.skill_id, c.card_spell_id, 'spell', c.position
                    FROM user_skills us 
                    JOIN card_spells_skills c ON us.skill_id = c.skill_id AND us.user_id = c.user_id
                    WHERE us.user_id = @UserId AND c.card_spell_id IN ({spellIn})
                    
                    UNION ALL
                    SELECT us.skill_id, c.card_soldier_id, 'soldier', c.position
                    FROM user_skills us 
                    JOIN card_soldiers_skills c ON us.skill_id = c.skill_id AND us.user_id = c.user_id
                    WHERE us.user_id = @UserId AND c.card_soldier_id IN ({soldierIn})
                ),
                UniqueTargetSkills AS (
                    SELECT DISTINCT skill_id FROM TargetSkillsRaw
                ),

                CardsGrouped AS (
                    SELECT 
                        skill_id,
                        card_type,
                        JSON_ARRAYAGG(
                            JSON_OBJECT('id', card_id, 'pos', position)
                        ) AS card_list
                    FROM TargetSkillsRaw
                    GROUP BY skill_id, card_type
                ),
                -- Pivot (Xoay dòng thành cột) để trả về các mảng JSON sạch sẽ cho C#
                AggregatedCards AS (
                    SELECT 
                        skill_id,
                        MAX(CASE WHEN card_type = 'hero' THEN card_list END) AS hero_ids,
                        MAX(CASE WHEN card_type = 'captain' THEN card_list END) AS captain_ids,
                        MAX(CASE WHEN card_type = 'colonel' THEN card_list END) AS colonel_ids,
                        MAX(CASE WHEN card_type = 'general' THEN card_list END) AS general_ids,
                        MAX(CASE WHEN card_type = 'admiral' THEN card_list END) AS admiral_ids,
                        MAX(CASE WHEN card_type = 'monster' THEN card_list END) AS monster_ids,
                        MAX(CASE WHEN card_type = 'military' THEN card_list END) AS military_ids,
                        MAX(CASE WHEN card_type = 'spell' THEN card_list END) AS spell_ids,
                        MAX(CASE WHEN card_type = 'soldier' THEN card_list END) AS soldier_ids
                    FROM CardsGrouped
                    GROUP BY skill_id
                ),
                BaseEffects AS (
                    SELECT 
                        se.skill_id, se.effect_id, se.min_value, se.max_value, se.trigger_phase, 
                        se.trigger_condition, se.is_stackable, se.is_removable, se.target_id, 
                        e.name AS effect_name, e.effect_type, e.duration, e.description AS effect_description
                    FROM skill_effect se 
                    JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE 
                    WHERE se.skill_id IN (SELECT skill_id FROM UniqueTargetSkills)
                ),
                AggregatedEffects AS (
                    SELECT 
                        be.skill_id, 
                        JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', be.min_value, 'max_value', be.max_value, 
                                'trigger_phase', be.trigger_phase, 'trigger_condition', be.trigger_condition, 
                                'is_stackable', be.is_stackable, 'is_removable', be.is_removable, 
                                'target_id', be.target_id, 'effect_id', be.effect_id, 
                                'effect_name', be.effect_name, 'effect_type', be.effect_type, 
                                'duration', be.duration, 'effect_description', be.effect_description, 
                                'value_type', epa.value_type, 'value', epa.value, 
                                'scaling_factor', epa.scaling_factor, 'property_code', ep.property_code, 
                                'property_name', ep.property_name, 'action_code', ea.action_code, 
                                'action_name', ea.action_name
                            )
                        ) AS skill_effects_json
                    FROM BaseEffects be 
                    LEFT JOIN effect_property_action epa ON be.effect_id = epa.effect_id 
                    LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE 
                    LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE 
                    GROUP BY be.skill_id
                )
                SELECT 
                    us.skill_id, s.name, s.image, us.quality, s.type, s.skill_type, s.skill_sub_type, us.star AS current_star,
                    ae.skill_effects_json,
                    ac.hero_ids, ac.captain_ids, ac.colonel_ids, ac.general_ids, ac.admiral_ids, ac.monster_ids, ac.military_ids, ac.spell_ids, ac.soldier_ids
                    ,us.user_id
                FROM user_skills us
                JOIN Skills s ON us.skill_id = s.id
                LEFT JOIN AggregatedEffects ae ON us.skill_id = ae.skill_id
                LEFT JOIN AggregatedCards ac ON us.skill_id = ac.skill_id
                WHERE us.user_id = @UserId
                AND us.skill_id IN (SELECT skill_id FROM UniqueTargetSkills);";

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(query, connection))
                {
                    // command.CommandTimeout = 30;

                    // Gán tất cả các parameter đã tạo từ bước build IN clause động
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var skill = new Skills
                            {
                                Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                Image = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Quality = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                Type = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                SkillType = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                CurrentStar = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                UserId = reader.IsDBNull(18) ? string.Empty : reader.GetString(18),
                            };

                            skill.SkillSubType = new SkillSubTypes
                            {
                                SubTypeCode = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                            };

                            string effectsJson = reader.IsDBNull(8) ? null : reader.GetString(8);
                            string heroJson = reader.IsDBNull(9) ? null : reader.GetString(9);
                            string captainJson = reader.IsDBNull(10) ? null : reader.GetString(10);
                            string colonelJson = reader.IsDBNull(11) ? null : reader.GetString(11);
                            string generalJson = reader.IsDBNull(12) ? null : reader.GetString(12);
                            string admiralJson = reader.IsDBNull(13) ? null : reader.GetString(13);
                            string monsterJson = reader.IsDBNull(14) ? null : reader.GetString(14);
                            string militaryJson = reader.IsDBNull(15) ? null : reader.GetString(15);
                            string spellJson = reader.IsDBNull(16) ? null : reader.GetString(16);
                            string soldierJson = reader.IsDBNull(17) ? null : reader.GetString(17);

                            pendingJsonList.Add((skill, effectsJson, heroJson, captainJson, colonelJson, generalJson, admiralJson, monsterJson, militaryJson, spellJson, soldierJson));
                            skillList.Add(skill);
                        }
                    }
                }
            }

            // 4. Giải mã dữ liệu JSON song song đa luồng (sử dụng Newtonsoft.Json)
            if (pendingJsonList.Count > 0)
            {
                Parallel.ForEach(pendingJsonList, item =>
                {
                    if (!string.IsNullOrEmpty(item.effectsJson))
                    {
                        try
                        {
                            // Bạn có thể đổi sang hàm của Newtonsoft nếu JsonHelper đang bị lỗi
                            item.skill.Effects = JsonConvert.DeserializeObject<List<Effects>>(item.effectsJson) ?? new List<Effects>();
                        }
                        catch (Exception) { item.skill.Effects = new List<Effects>(); }
                    }
                    else { item.skill.Effects = new List<Effects>(); }

                    item.skill.cardHeroIds = ParseCardRelations(item.heroJson);
                    item.skill.cardCaptainIds = ParseCardRelations(item.captainJson);
                    item.skill.cardColonelIds = ParseCardRelations(item.colonelJson);
                    item.skill.cardGeneralIds = ParseCardRelations(item.generalJson);
                    item.skill.cardAdmiralIds = ParseCardRelations(item.admiralJson);
                    item.skill.cardMonsterIds = ParseCardRelations(item.monsterJson);
                    item.skill.cardMilitaryIds = ParseCardRelations(item.militaryJson);
                    item.skill.cardSpellIds = ParseCardRelations(item.spellJson);
                    item.skill.cardSoldierIds = ParseCardRelations(item.soldierJson);
                });
            }
        }
        catch (Exception ex)
        {
            // Xử lý hoặc ghi log lỗi lớn tại đây (ví dụ: LogError(ex);)
            Console.WriteLine($"Error in GetUserSkillsWithCardsAsync: {ex.Message}");
            throw; // Re-throw để tầng trên biết có lỗi hoặc return skillList trống tùy thiết kế ứng dụng
        }

        return skillList;
    }

    // --- HÀM HELPER 1: Giải mã JSON mảng Card IDs bằng Newtonsoft.Json tránh lỗi ép kiểu ---
    private List<CardSkillRelation> ParseCardRelations(string jsonStr)
    {
        if (string.IsNullOrEmpty(jsonStr)) return new List<CardSkillRelation>();
        try
        {
            return JsonConvert.DeserializeObject<List<CardSkillRelation>>(jsonStr) ?? new List<CardSkillRelation>();
        }
        catch (Exception)
        {
            // Trả về list rỗng nếu parse lỗi (ví dụ chuỗi không phải định dạng JSON chuẩn)
            return new List<CardSkillRelation>();
        }
    }

    // --- HÀM HELPER 2: Tự động tạo chuỗi IN Clause động và gắn Parameter an toàn ---
    private string BuildInClause(List<string> ids, string prefix, List<MySqlParameter> paramList)
    {
        // Nếu danh sách trống, gán giá trị không khớp (ví dụ: NULL) để tránh lỗi cú pháp IN () trống của SQL
        if (ids == null || ids.Count == 0)
        {
            return "NULL";
        }

        var sb = new StringBuilder();
        for (int i = 0; i < ids.Count; i++)
        {
            string paramName = $"@{prefix}_{i}";
            sb.Append(i == 0 ? paramName : $", {paramName}");

            // Khởi tạo Parameter riêng biệt tương thích an toàn với ADO.NET thuần
            paramList.Add(new MySqlParameter(paramName, ids[i]));
        }
        return sb.ToString();
    }
    public async Task<List<Skills>> GetUserCardsSkillsAsync(string userId, List<string> allCardIds)
    {
        List<Skills> resultSkills = new List<Skills>();
        if (allCardIds == null || allCardIds.Count == 0 || string.IsNullOrEmpty(userId)) return resultSkills;

        string connectionString = DatabaseConfig.ConnectionString;

        // Cấu trúc lưu trữ mapping: SkillId -> Danh sách các (CardId, Position)
        var skillToCardMapping = new Dictionary<string, List<(string CardId, int Position)>>();
        var uniqueSkillIds = new HashSet<string>();

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // =================================================================
                // BƯỚC 1: DÙNG UNION ALL ĐỂ LẤY MAPPING CỦA TẤT CẢ CÁC BẢNG TRONG 1 LẦN GỌI
                // =================================================================
                var paramNames = new List<string>();
                for (int i = 0; i < allCardIds.Count; i++) paramNames.Add($"@cId{i}");
                string inClause = string.Join(", ", paramNames);

                StringBuilder unionSqlBuilder = new StringBuilder();
                for (int i = 0; i < _configs.Count; i++)
                {
                    var config = _configs[i];
                    unionSqlBuilder.AppendLine($"SELECT `{config.ColumnId}` AS card_id, `skill_id`, `position` FROM `{config.TableName}` WHERE `{config.ColumnId}` IN ({inClause})");
                    if (i < _configs.Count - 1)
                    {
                        unionSqlBuilder.AppendLine("UNION ALL");
                    }
                }

                await using (var mapCmd = new MySqlCommand(unionSqlBuilder.ToString(), connection))
                {
                    // Nạp tham số CardIds một lần duy nhất cho toàn bộ các mệnh đề UNION
                    for (int i = 0; i < allCardIds.Count; i++)
                    {
                        mapCmd.Parameters.AddWithValue(paramNames[i], allCardIds[i]);
                    }

                    await using (var mapReader = await mapCmd.ExecuteReaderAsync())
                    {
                        while (await mapReader.ReadAsync())
                        {
                            string cId = mapReader.GetString(0);
                            string sId = mapReader.GetString(1);
                            int pos = mapReader.GetInt32(2);

                            uniqueSkillIds.Add(sId); // Lọc trùng tuyệt đối trên RAM (Từ 21,000 xuống 4,000)

                            if (!skillToCardMapping.TryGetValue(sId, out var list))
                            {
                                list = new List<(string, int)>();
                                skillToCardMapping[sId] = list;
                            }
                            list.Add((cId, pos));
                        }
                    }
                }

                // Nếu không tìm thấy skill nào đang được trang bị, thoát sớm
                if (uniqueSkillIds.Count == 0) return resultSkills;

                // =================================================================
                // BƯỚC 2: CHỈ XỬ LÝ ĐÓNG GÓI JSON CHO 4,000 SKILL DUY NHẤT
                // =================================================================
                var skillParamNames = new List<string>();
                int sIdx = 0;
                foreach (var sId in uniqueSkillIds) skillParamNames.Add($"@sId{sIdx++}");
                string skillInClause = string.Join(", ", skillParamNames);

                string selectSQL = $@"
                WITH TargetSkills AS (
                    SELECT `id` AS skill_id FROM `skills` WHERE `id` IN ({skillInClause})
                ),
                BaseEffects AS (
                    SELECT 
                        se.skill_id, se.effect_id, se.min_value, se.max_value, se.trigger_phase,
                        se.trigger_condition, se.is_stackable, se.is_removable, se.target_id,
                        e.name AS effect_name, e.effect_type, e.duration, e.description AS effect_description
                    FROM skill_effect se
                    JOIN effects e ON se.effect_id = e.id AND e.is_deleted = FALSE
                    WHERE se.skill_id IN (SELECT skill_id FROM TargetSkills)
                ),
                AggregatedEffects AS (
                    SELECT 
                        be.skill_id,
                        JSON_ARRAYAGG(
                            JSON_OBJECT(
                                'min_value', be.min_value, 'max_value', be.max_value,
                                'trigger_phase', be.trigger_phase, 'trigger_condition', be.trigger_condition,
                                'is_stackable', be.is_stackable, 'is_removable', be.is_removable,
                                'target_id', be.target_id, 'effect_id', be.effect_id,
                                'effect_name', be.effect_name, 'effect_type', be.effect_type,
                                'duration', be.duration, 'effect_description', be.effect_description,
                                'value_type', epa.value_type, 'value', epa.value, 'scaling_factor', epa.scaling_factor,
                                'property_code', ep.property_code, 'property_name', ep.property_name,
                                'action_code', ea.action_code, 'action_name', ea.action_name
                            )
                        ) AS skill_effects_json
                    FROM BaseEffects be
                    LEFT JOIN effect_property_action epa ON be.effect_id = epa.effect_id
                    LEFT JOIN effect_property ep ON epa.property_id = ep.property_id AND ep.is_deleted = FALSE
                    LEFT JOIN effect_action ea ON epa.action_id = ea.action_id AND ea.is_deleted = FALSE
                    GROUP BY be.skill_id
                )
                SELECT 
                    us.*, s.name, s.image, s.rare, s.type, s.skill_type, s.description, s.skill_sub_type,
                    sp.pattern_id, ae.skill_effects_json
                FROM user_skills us
                JOIN skills s ON us.skill_id = s.id
                LEFT JOIN skill_patterns sp ON us.skill_id = sp.skill_id
                LEFT JOIN AggregatedEffects ae ON us.skill_id = ae.skill_id
                WHERE us.user_id = @userId AND us.skill_id IN ({skillInClause});";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@userId", userId);

                sIdx = 0;
                foreach (var sId in uniqueSkillIds)
                {
                    selectCommand.Parameters.AddWithValue(skillParamNames[sIdx++], sId);
                }

                await using var reader = await selectCommand.ExecuteReaderAsync();

                // Đồng bộ Ordinal Index để tối ưu hóa việc đọc dữ liệu
                int colSkillId = reader.GetOrdinal("skill_id");
                int colName = reader.GetOrdinal("name");
                int colImage = reader.GetOrdinal("image");
                int colRare = reader.GetOrdinal("rare");
                int colQuality = reader.GetOrdinal("quality");
                int colType = reader.GetOrdinal("type");
                int colStar = reader.GetOrdinal("star");
                int colLevel = reader.GetOrdinal("level");
                int colSkillType = reader.GetOrdinal("skill_type");
                int colExperience = reader.GetOrdinal("experience");
                int colQuantity = reader.GetOrdinal("quantity");
                int colPower = reader.GetOrdinal("power");
                int colHealth = reader.GetOrdinal("health");
                int colPhysicalAttack = reader.GetOrdinal("physical_attack");
                int colPhysicalDefense = reader.GetOrdinal("physical_defense");
                int colMagicalAttack = reader.GetOrdinal("magical_attack");
                int colMagicalDefense = reader.GetOrdinal("magical_defense");
                int colSpeed = reader.GetOrdinal("speed");
                int colDescription = reader.GetOrdinal("description");
                int colPatternId = reader.GetOrdinal("pattern_id");
                int colSkillSubType = reader.GetOrdinal("skill_sub_type");
                int colSkillEffectsJson = reader.GetOrdinal("skill_effects_json");

                List<(Skills BaseSkill, string JsonRaw)> pendingJsonList = new List<(Skills, string)>();

                while (await reader.ReadAsync())
                {
                    string currentSkillId = reader.GetString(colSkillId);

                    if (skillToCardMapping.TryGetValue(currentSkillId, out var cardsUsingThisSkill))
                    {
                        // Khởi tạo Object mẫu 1 lần duy nhất cho Skill này
                        Skills baseSkill = new Skills
                        {
                            Id = currentSkillId,
                            Name = reader.IsDBNull(colName) ? "" : reader.GetString(colName),
                            Image = reader.IsDBNull(colImage) ? "" : reader.GetString(colImage),
                            Rarity = reader.IsDBNull(colRare) ? "" : reader.GetString(colRare),
                            Quality = reader.IsDBNull(colQuality) ? 0 : reader.GetDouble(colQuality),
                            Type = reader.IsDBNull(colType) ? "" : reader.GetString(colType),
                            Star = reader.IsDBNull(colStar) ? 0 : reader.GetInt32(colStar),
                            Level = reader.IsDBNull(colLevel) ? 0 : reader.GetInt32(colLevel),
                            SkillType = reader.IsDBNull(colSkillType) ? "" : reader.GetString(colSkillType),
                            Experience = reader.IsDBNull(colExperience) ? 0 : reader.GetDouble(colExperience),
                            Quantity = reader.IsDBNull(colQuantity) ? 0 : reader.GetInt32(colQuantity),
                            Power = reader.IsDBNull(colPower) ? 0 : reader.GetDouble(colPower),
                            Health = reader.IsDBNull(colHealth) ? 0 : reader.GetDouble(colHealth),
                            PhysicalAttack = reader.IsDBNull(colPhysicalAttack) ? 0 : reader.GetDouble(colPhysicalAttack),
                            PhysicalDefense = reader.IsDBNull(colPhysicalDefense) ? 0 : reader.GetDouble(colPhysicalDefense),
                            MagicalAttack = reader.IsDBNull(colMagicalAttack) ? 0 : reader.GetDouble(colMagicalAttack),
                            MagicalDefense = reader.IsDBNull(colMagicalDefense) ? 0 : reader.GetDouble(colMagicalDefense),
                            Speed = reader.IsDBNull(colSpeed) ? 0 : reader.GetDouble(colSpeed),
                            Description = reader.IsDBNull(colDescription) ? "" : reader.GetString(colDescription),
                            Pattern = new Patterns { Id = reader.IsDBNull(colPatternId) ? "" : reader.GetString(colPatternId) },
                            SkillSubType = new SkillSubTypes { SubTypeCode = reader.IsDBNull(colSkillSubType) ? "" : reader.GetString(colSkillSubType) }
                        };

                        string effectsJson = reader.IsDBNull(colSkillEffectsJson) ? "" : reader.GetString(colSkillEffectsJson);

                        // NHÂN BẢN TRÊN RAM cho toàn bộ các Card sử dụng chung Skill này
                        foreach (var targetCard in cardsUsingThisSkill)
                        {
                            Skills clonedSkill = baseSkill.Clone();
                            clonedSkill.CardId = targetCard.CardId;
                            clonedSkill.Position = targetCard.Position;

                            if (!string.IsNullOrEmpty(effectsJson))
                            {
                                pendingJsonList.Add((clonedSkill, effectsJson));
                            }
                            else
                            {
                                clonedSkill.Effects = new List<Effects>();
                            }

                            resultSkills.Add(clonedSkill);
                        }
                    }
                }

                // Giải nén JSON trên RAM ngoài luồng đọc DB reader
                foreach (var item in pendingJsonList)
                {
                    try
                    {
                        item.BaseSkill.Effects = JsonHelper.DeserializeEffects(item.JsonRaw);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[JSON Parse Error for Skill {item.BaseSkill.Id}]: {ex.Message}");
                        item.BaseSkill.Effects = new List<Effects>();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error DB: " + ex.Message);
            }
        }

        return resultSkills;
    }
    public async Task<List<Skills>> LoadSkillsWithEffectsAsync(string userId, List<Skills> skillsList, MySqlConnection connection)
    {
        // Kiểm tra danh sách Skills
        var skillIds = skillsList.Select(s => s.Id).ToList();
        if (!skillIds.Any()) return skillsList;

        // Chuyển danh sách ID sang chuỗi cho WHERE IN
        string skillIdInClause = string.Join(",", skillIds.Select(id => $"'{id}'"));

        string combinedQuery = $@"
        SELECT 
            s.id AS Skill_Id,
            e.*, 
            ep.*, 
            ea.*
        FROM skills s
        JOIN skill_effect se ON s.id = se.skill_id
        JOIN effects e ON se.effect_id = e.id
        JOIN effect_property_action epa ON e.id = epa.effect_id
        JOIN effect_property ep ON epa.property_id = ep.property_id
        JOIN effect_action ea ON epa.action_id = ea.action_id
        WHERE s.id IN ({skillIdInClause});";

        // Tạo dictionary Skill ID → Skill
        var skillDict = skillsList.ToDictionary(s => s.Id);

        await using var selectCommand = new MySqlCommand(combinedQuery, connection);
        await using var reader = await selectCommand.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            string currentSkillId = reader.GetStringSafe("Skill_Id");
            if (!skillDict.TryGetValue(currentSkillId, out Skills currentSkill)) continue;

            var newEffect = new Effects
            {
                Id = reader.GetStringSafe("id"),
                Name = reader.GetStringSafe("name"),
                EffectType = reader.GetStringSafe("effect_type"),
                Duration = reader.IsDBNull(reader.GetOrdinal("duration")) ? 0 : reader.GetIntSafe("duration"),
                Description = reader.GetStringSafe("description"),
                EffectProperty = new EffectProperty
                {
                    PropertyId = reader.GetStringSafe("property_id"),
                    PropertyCode = reader.GetStringSafe("property_code"),
                    PropertyName = reader.GetStringSafe("property_name"),
                },
                EffectAction = new EffectAction
                {
                    ActionId = reader.GetStringSafe("action_id"),
                    ActionCode = reader.GetStringSafe("action_code"),
                    ActionName = reader.GetStringSafe("action_name"),
                }
            };

            currentSkill.Effects.Add(newEffect);
        }

        return skillsList;
    }
    public async Task<bool> InsertUserCardHeroSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_heroes_skills 
            WHERE user_id = @user_id AND skill_id = @skill_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@skill_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_heroes_skills (
                    user_id, skill_id, skill_id, level, position
                ) VALUES (
                    @user_id, @skill_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@skill_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardCaptainSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_captains_skills 
            WHERE user_id = @user_id AND card_captain_id = @card_captain_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_captain_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_captains_skills (
                    user_id, card_captain_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_captain_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_captain_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardColonelSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_colonels_skills 
            WHERE user_id = @user_id AND card_colonel_id = @card_colonel_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_colonel_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_colonels_skills (
                    user_id, card_colonel_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_colonel_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_colonel_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardGeneralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_generals_skills 
            WHERE user_id = @user_id AND card_general_id = @card_general_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_general_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_generals_skills (
                    user_id, card_general_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_general_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_general_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardAdmiralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_admirals_skills 
            WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_admiral_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_admirals_skills (
                    user_id, card_admiral_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_admiral_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_admiral_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardMilitarySkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_militaries_skills 
            WHERE user_id = @user_id AND card_military_id = @card_military_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_military_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_militaries_skills (
                    user_id, card_military_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_military_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_military_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardMonsterSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_monsters_skills 
            WHERE user_id = @user_id AND card_monster_id = @card_monster_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_monster_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_monsters_skills (
                    user_id, card_monster_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_monster_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_monster_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardSpellSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_spells_skills 
            WHERE user_id = @user_id AND card_spell_id = @card_spell_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_spell_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_spells_skills (
                    user_id, card_spell_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_spell_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_spell_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertUserCardSoldierSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM card_soldiers_skills 
            WHERE user_id = @user_id AND card_soldier_id = @card_soldier_id AND skill_id = @skill_id AND position = @position;";

            await using var checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@card_soldier_id", cardId);
            checkCommand.Parameters.AddWithValue("@skill_id", skillId);
            checkCommand.Parameters.AddWithValue("@position", position);

            int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            if (count == 0)
            {
                string insertSQL = @"
                INSERT INTO card_spells_skills (
                    user_id, card_spell_id, skill_id, level, position
                ) VALUES (
                    @user_id, @card_spell_id, @skill_id, @level, @position
                );";

                await using var insertCommand = new MySqlCommand(insertSQL, connection);
                insertCommand.Parameters.AddWithValue("@user_id", userId);
                insertCommand.Parameters.AddWithValue("@card_spell_id", cardId);
                insertCommand.Parameters.AddWithValue("@skill_id", skillId);
                insertCommand.Parameters.AddWithValue("@level", 0);
                insertCommand.Parameters.AddWithValue("@position", position);

                await insertCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardHeroSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_heroes_skills 
            WHERE user_id = @user_id AND skill_id = @skill_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@skill_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardCaptainSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_captains_skills 
            WHERE user_id = @user_id AND card_captain_id = @card_captain_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_captain_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardColonelSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_colonels_skills 
            WHERE user_id = @user_id AND card_colonel_id = @card_colonel_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_colonel_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardGeneralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_generals_skills 
            WHERE user_id = @user_id AND card_general_id = @card_general_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_general_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardAdmiralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_admirals_skills 
            WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_admiral_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardMonsterSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_monsters_skills 
            WHERE user_id = @user_id AND card_monster_id = @card_monster_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_monster_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardMilitarySkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_militaries_skills 
            WHERE user_id = @user_id AND card_military_id = @card_military_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_military_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardSpellSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_spells_skills 
            WHERE user_id = @user_id AND card_spell_id = @card_spell_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_spell_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> DeleteUserCardSoldierSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string deleteSQL = @"
            DELETE FROM card_soldiers_skills 
            WHERE user_id = @user_id AND card_soldier_id = @card_soldier_id AND skill_id = @skill_id AND position = @position;";

            await using var deleteCommand = new MySqlCommand(deleteSQL, connection);
            deleteCommand.Parameters.AddWithValue("@user_id", userId);
            deleteCommand.Parameters.AddWithValue("@card_soldier_id", cardId);
            deleteCommand.Parameters.AddWithValue("@skill_id", skillId);
            deleteCommand.Parameters.AddWithValue("@position", position);

            await deleteCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<int> AssignRandomSkillsToUserCardHeroesAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID không được để trống.", nameof(userId));
        }

        string connectionString = DatabaseConfig.ConnectionString;

        // Bước 1: Lấy toàn bộ danh sách card_hero_id của User lên bộ nhớ (C#) trước
        var cardHeroIds = new List<string>();
        string getCardsQuery = "SELECT `card_hero_id` FROM `user_card_heroes` WHERE `user_id` = @userId;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(getCardsQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cardHeroIds.Add(reader.GetString(0));
                    }
                }
            }

            if (cardHeroIds.Count == 0) return 0;

            // Bước 2: Chuẩn bị câu lệnh SQL xử lý Random cho một nhóm (Batch) các Card ID
            // Việc giới hạn danh sách ID giúp ma trận CROSS JOIN thu nhỏ lại, MySQL chạy cực nhẹ
            string batchSqlQuery = @"
            INSERT INTO `card_heroes_skills` (`user_id`, `card_hero_id`, `skill_id`, `level`, `position`)
            WITH TargetCards AS (
                -- Chỉ lọc ra các Card nằm trong Batch hiện tại từ C#
                SELECT `card_hero_id`, `user_id`
                FROM `user_card_heroes`
                WHERE `user_id` = @userId AND `card_hero_id` IN ({0})
            ),
            RankedSkills AS (
                -- SỬA TẠI ĐÂY: Xáo trộn và đánh số thứ tự kỹ năng TRƯỚC khi JOIN
                -- Việc này ép MySQL tính RAND() độc lập cho bảng skills, không bị ảnh hưởng bởi số lượng Card
                SELECT 
                    `id` AS skill_id, 
                    `type`, 
                    `skill_type`,
                    ROW_NUMBER() OVER (
                        PARTITION BY `type`, `skill_type` 
                        ORDER BY RAND()
                    ) AS type_rn
                FROM `skills`
            ),
            FilteredSkills AS (
                -- SỬA TẠI ĐÂY: Lọc đúng 1 Active và 2 Passive của từng hệ ngay lập tức
                -- Tập dữ liệu này bây giờ chỉ còn đúng 3 skills * số lượng hệ (Ví dụ: 18 dòng cố định)
                SELECT 
                    skill_id, 
                    `type`, 
                    `skill_type`,
                    CASE 
                        WHEN `skill_type` = 'Active' THEN 1
                        WHEN `skill_type` = 'Passive' THEN type_rn + 1
                    END AS `position`
                FROM RankedSkills
                WHERE (`skill_type` = 'Active' AND type_rn = 1)
                OR (`skill_type` = 'Passive' AND type_rn IN (1, 2))
            )
            -- Bước cuối: JOIN ma trận siêu nhỏ giữa 500 Cards và ~18 Skills đã được cố định
            SELECT 
                tc.`user_id`, 
                tc.`card_hero_id`, 
                fs.skill_id, 
                0 AS `level`,
                fs.`position`
            FROM TargetCards tc
            CROSS JOIN FilteredSkills fs
            ON DUPLICATE KEY UPDATE `updated_at` = CURRENT_TIMESTAMP;";

            int totalRowsAffected = 0;
            int batchSize = 500; // Mỗi đợt xử lý 500 cards để tránh quá tải MySQL

            // Bước 3: Chia nhỏ 10,000 cards thành các đợt nhỏ và thực thi
            for (int i = 0; i < cardHeroIds.Count; i += batchSize)
            {
                var batchIds = cardHeroIds.GetRange(i, Math.Min(batchSize, cardHeroIds.Count - i));

                using (var batchCommand = new MySqlCommand())
                {
                    batchCommand.Connection = connection;

                    // Tạo param động chống SQL Injection cho danh sách IN (...)
                    var paramNames = new List<string>();
                    for (int j = 0; j < batchIds.Count; j++)
                    {
                        string paramName = $"@cardId_{j}";
                        batchCommand.Parameters.AddWithValue(paramName, batchIds[j]);
                        paramNames.Add(paramName);
                    }

                    // Nhúng danh sách param vào câu SQL: IN (@cardId_0, @cardId_1, ...)
                    batchCommand.CommandText = string.Format(batchSqlQuery, string.Join(",", paramNames));
                    batchCommand.Parameters.AddWithValue("@userId", userId);

                    // Tăng timeout cho command này lên 60s để an toàn tuyệt đối
                    batchCommand.CommandTimeout = 60;

                    totalRowsAffected += await batchCommand.ExecuteNonQueryAsync();
                }
            }

            return totalRowsAffected;
        }
    }
    public async Task<int> AssignRandomSkillsToUserCardCaptainsAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID không được để trống.", nameof(userId));
        }

        string connectionString = DatabaseConfig.ConnectionString;

        // Bước 1: Lấy toàn bộ danh sách card_hero_id của User lên bộ nhớ (C#) trước
        var cardCaptainIds = new List<string>();
        string getCardsQuery = "SELECT `card_captain_id` FROM `user_card_captains` WHERE `user_id` = @userId;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(getCardsQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cardCaptainIds.Add(reader.GetString(0));
                    }
                }
            }

            if (cardCaptainIds.Count == 0) return 0;

            // Bước 2: Chuẩn bị câu lệnh SQL xử lý Random cho một nhóm (Batch) các Card ID
            // Việc giới hạn danh sách ID giúp ma trận CROSS JOIN thu nhỏ lại, MySQL chạy cực nhẹ
            string batchSqlQuery = @"
            INSERT INTO `card_captains_skills` (`user_id`, `card_captain_id`, `skill_id`, `level`, `position`)
            WITH TargetCards AS (
                -- Chỉ lọc ra các Card nằm trong Batch hiện tại từ C#
                SELECT `card_captain_id`, `user_id`
                FROM `user_card_captains`
                WHERE `user_id` = @userId AND `card_captain_id` IN ({0})
            ),
            RankedSkills AS (
                -- SỬA TẠI ĐÂY: Xáo trộn và đánh số thứ tự kỹ năng TRƯỚC khi JOIN
                -- Việc này ép MySQL tính RAND() độc lập cho bảng skills, không bị ảnh hưởng bởi số lượng Card
                SELECT 
                    `id` AS skill_id, 
                    `type`, 
                    `skill_type`,
                    ROW_NUMBER() OVER (
                        PARTITION BY `type`, `skill_type` 
                        ORDER BY RAND()
                    ) AS type_rn
                FROM `skills`
            ),
            FilteredSkills AS (
                -- SỬA TẠI ĐÂY: Lọc đúng 1 Active và 2 Passive của từng hệ ngay lập tức
                -- Tập dữ liệu này bây giờ chỉ còn đúng 3 skills * số lượng hệ (Ví dụ: 18 dòng cố định)
                SELECT 
                    skill_id, 
                    `type`, 
                    `skill_type`,
                    CASE 
                        WHEN `skill_type` = 'Active' THEN 1
                        WHEN `skill_type` = 'Passive' THEN type_rn + 1
                    END AS `position`
                FROM RankedSkills
                WHERE (`skill_type` = 'Active' AND type_rn = 1)
                OR (`skill_type` = 'Passive' AND type_rn IN (1, 2))
            )
            -- Bước cuối: JOIN ma trận siêu nhỏ giữa 500 Cards và ~18 Skills đã được cố định
            SELECT 
                tc.`user_id`, 
                tc.`card_captain_id`, 
                fs.skill_id, 
                0 AS `level`,
                fs.`position`
            FROM TargetCards tc
            CROSS JOIN FilteredSkills fs
            ON DUPLICATE KEY UPDATE `updated_at` = CURRENT_TIMESTAMP;";

            int totalRowsAffected = 0;
            int batchSize = 500; // Mỗi đợt xử lý 500 cards để tránh quá tải MySQL

            // Bước 3: Chia nhỏ 10,000 cards thành các đợt nhỏ và thực thi
            for (int i = 0; i < cardCaptainIds.Count; i += batchSize)
            {
                var batchIds = cardCaptainIds.GetRange(i, Math.Min(batchSize, cardCaptainIds.Count - i));

                using (var batchCommand = new MySqlCommand())
                {
                    batchCommand.Connection = connection;

                    // Tạo param động chống SQL Injection cho danh sách IN (...)
                    var paramNames = new List<string>();
                    for (int j = 0; j < batchIds.Count; j++)
                    {
                        string paramName = $"@cardId_{j}";
                        batchCommand.Parameters.AddWithValue(paramName, batchIds[j]);
                        paramNames.Add(paramName);
                    }

                    // Nhúng danh sách param vào câu SQL: IN (@cardId_0, @cardId_1, ...)
                    batchCommand.CommandText = string.Format(batchSqlQuery, string.Join(",", paramNames));
                    batchCommand.Parameters.AddWithValue("@userId", userId);

                    // Tăng timeout cho command này lên 60s để an toàn tuyệt đối
                    batchCommand.CommandTimeout = 60;

                    totalRowsAffected += await batchCommand.ExecuteNonQueryAsync();
                }
            }

            return totalRowsAffected;
        }
    }
    public async Task<int> AssignRandomSkillsToUserCardColonelsAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID không được để trống.", nameof(userId));
        }

        string connectionString = DatabaseConfig.ConnectionString;

        // Bước 1: Lấy toàn bộ danh sách card_hero_id của User lên bộ nhớ (C#) trước
        var cardColonelIds = new List<string>();
        string getCardsQuery = "SELECT `card_colonel_id` FROM `user_card_colonels` WHERE `user_id` = @userId;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(getCardsQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cardColonelIds.Add(reader.GetString(0));
                    }
                }
            }

            if (cardColonelIds.Count == 0) return 0;

            // Bước 2: Chuẩn bị câu lệnh SQL xử lý Random cho một nhóm (Batch) các Card ID
            // Việc giới hạn danh sách ID giúp ma trận CROSS JOIN thu nhỏ lại, MySQL chạy cực nhẹ
            string batchSqlQuery = @"
            INSERT INTO `card_colonels_skills` (`user_id`, `card_colonel_id`, `skill_id`, `level`, `position`)
            WITH TargetCards AS (
                -- Chỉ lọc ra các Card nằm trong Batch hiện tại từ C#
                SELECT `card_colonel_id`, `user_id`
                FROM `user_card_colonels`
                WHERE `user_id` = @userId AND `card_colonel_id` IN ({0})
            ),
            RankedSkills AS (
                -- SỬA TẠI ĐÂY: Xáo trộn và đánh số thứ tự kỹ năng TRƯỚC khi JOIN
                -- Việc này ép MySQL tính RAND() độc lập cho bảng skills, không bị ảnh hưởng bởi số lượng Card
                SELECT 
                    `id` AS skill_id, 
                    `type`, 
                    `skill_type`,
                    ROW_NUMBER() OVER (
                        PARTITION BY `type`, `skill_type` 
                        ORDER BY RAND()
                    ) AS type_rn
                FROM `skills`
            ),
            FilteredSkills AS (
                -- SỬA TẠI ĐÂY: Lọc đúng 1 Active và 2 Passive của từng hệ ngay lập tức
                -- Tập dữ liệu này bây giờ chỉ còn đúng 3 skills * số lượng hệ (Ví dụ: 18 dòng cố định)
                SELECT 
                    skill_id, 
                    `type`, 
                    `skill_type`,
                    CASE 
                        WHEN `skill_type` = 'Active' THEN 1
                        WHEN `skill_type` = 'Passive' THEN type_rn + 1
                    END AS `position`
                FROM RankedSkills
                WHERE (`skill_type` = 'Active' AND type_rn = 1)
                OR (`skill_type` = 'Passive' AND type_rn IN (1, 2))
            )
            -- Bước cuối: JOIN ma trận siêu nhỏ giữa 500 Cards và ~18 Skills đã được cố định
            SELECT 
                tc.`user_id`, 
                tc.`card_colonel_id`, 
                fs.skill_id, 
                0 AS `level`,
                fs.`position`
            FROM TargetCards tc
            CROSS JOIN FilteredSkills fs
            ON DUPLICATE KEY UPDATE `updated_at` = CURRENT_TIMESTAMP;";

            int totalRowsAffected = 0;
            int batchSize = 500; // Mỗi đợt xử lý 500 cards để tránh quá tải MySQL

            // Bước 3: Chia nhỏ 10,000 cards thành các đợt nhỏ và thực thi
            for (int i = 0; i < cardColonelIds.Count; i += batchSize)
            {
                var batchIds = cardColonelIds.GetRange(i, Math.Min(batchSize, cardColonelIds.Count - i));

                using (var batchCommand = new MySqlCommand())
                {
                    batchCommand.Connection = connection;

                    // Tạo param động chống SQL Injection cho danh sách IN (...)
                    var paramNames = new List<string>();
                    for (int j = 0; j < batchIds.Count; j++)
                    {
                        string paramName = $"@cardId_{j}";
                        batchCommand.Parameters.AddWithValue(paramName, batchIds[j]);
                        paramNames.Add(paramName);
                    }

                    // Nhúng danh sách param vào câu SQL: IN (@cardId_0, @cardId_1, ...)
                    batchCommand.CommandText = string.Format(batchSqlQuery, string.Join(",", paramNames));
                    batchCommand.Parameters.AddWithValue("@userId", userId);

                    // Tăng timeout cho command này lên 60s để an toàn tuyệt đối
                    batchCommand.CommandTimeout = 60;

                    totalRowsAffected += await batchCommand.ExecuteNonQueryAsync();
                }
            }

            return totalRowsAffected;
        }
    }
    public async Task<int> AssignRandomSkillsToUserCardGeneralsAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID không được để trống.", nameof(userId));
        }

        string connectionString = DatabaseConfig.ConnectionString;

        // Bước 1: Lấy toàn bộ danh sách card_hero_id của User lên bộ nhớ (C#) trước
        var cardGeneralIds = new List<string>();
        string getCardsQuery = "SELECT `card_general_id` FROM `user_card_generals` WHERE `user_id` = @userId;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(getCardsQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cardGeneralIds.Add(reader.GetString(0));
                    }
                }
            }

            if (cardGeneralIds.Count == 0) return 0;

            // Bước 2: Chuẩn bị câu lệnh SQL xử lý Random cho một nhóm (Batch) các Card ID
            // Việc giới hạn danh sách ID giúp ma trận CROSS JOIN thu nhỏ lại, MySQL chạy cực nhẹ
            string batchSqlQuery = @"
            INSERT INTO `card_generals_skills` (`user_id`, `card_general_id`, `skill_id`, `level`, `position`)
            WITH TargetCards AS (
                -- Chỉ lọc ra các Card nằm trong Batch hiện tại từ C#
                SELECT `card_general_id`, `user_id`
                FROM `user_card_generals`
                WHERE `user_id` = @userId AND `card_general_id` IN ({0})
            ),
            RankedSkills AS (
                -- SỬA TẠI ĐÂY: Xáo trộn và đánh số thứ tự kỹ năng TRƯỚC khi JOIN
                -- Việc này ép MySQL tính RAND() độc lập cho bảng skills, không bị ảnh hưởng bởi số lượng Card
                SELECT 
                    `id` AS skill_id, 
                    `type`, 
                    `skill_type`,
                    ROW_NUMBER() OVER (
                        PARTITION BY `type`, `skill_type` 
                        ORDER BY RAND()
                    ) AS type_rn
                FROM `skills`
            ),
            FilteredSkills AS (
                -- SỬA TẠI ĐÂY: Lọc đúng 1 Active và 2 Passive của từng hệ ngay lập tức
                -- Tập dữ liệu này bây giờ chỉ còn đúng 3 skills * số lượng hệ (Ví dụ: 18 dòng cố định)
                SELECT 
                    skill_id, 
                    `type`, 
                    `skill_type`,
                    CASE 
                        WHEN `skill_type` = 'Active' THEN 1
                        WHEN `skill_type` = 'Passive' THEN type_rn + 1
                    END AS `position`
                FROM RankedSkills
                WHERE (`skill_type` = 'Active' AND type_rn = 1)
                OR (`skill_type` = 'Passive' AND type_rn IN (1, 2))
            )
            -- Bước cuối: JOIN ma trận siêu nhỏ giữa 500 Cards và ~18 Skills đã được cố định
            SELECT 
                tc.`user_id`, 
                tc.`card_general_id`, 
                fs.skill_id, 
                0 AS `level`,
                fs.`position`
            FROM TargetCards tc
            CROSS JOIN FilteredSkills fs
            ON DUPLICATE KEY UPDATE `updated_at` = CURRENT_TIMESTAMP;";

            int totalRowsAffected = 0;
            int batchSize = 500; // Mỗi đợt xử lý 500 cards để tránh quá tải MySQL

            // Bước 3: Chia nhỏ 10,000 cards thành các đợt nhỏ và thực thi
            for (int i = 0; i < cardGeneralIds.Count; i += batchSize)
            {
                var batchIds = cardGeneralIds.GetRange(i, Math.Min(batchSize, cardGeneralIds.Count - i));

                using (var batchCommand = new MySqlCommand())
                {
                    batchCommand.Connection = connection;

                    // Tạo param động chống SQL Injection cho danh sách IN (...)
                    var paramNames = new List<string>();
                    for (int j = 0; j < batchIds.Count; j++)
                    {
                        string paramName = $"@cardId_{j}";
                        batchCommand.Parameters.AddWithValue(paramName, batchIds[j]);
                        paramNames.Add(paramName);
                    }

                    // Nhúng danh sách param vào câu SQL: IN (@cardId_0, @cardId_1, ...)
                    batchCommand.CommandText = string.Format(batchSqlQuery, string.Join(",", paramNames));
                    batchCommand.Parameters.AddWithValue("@userId", userId);

                    // Tăng timeout cho command này lên 60s để an toàn tuyệt đối
                    batchCommand.CommandTimeout = 60;

                    totalRowsAffected += await batchCommand.ExecuteNonQueryAsync();
                }
            }

            return totalRowsAffected;
        }
    }
    public async Task<int> AssignRandomSkillsToUserCardAdmiralsAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID không được để trống.", nameof(userId));
        }

        string connectionString = DatabaseConfig.ConnectionString;

        // Bước 1: Lấy toàn bộ danh sách card_hero_id của User lên bộ nhớ (C#) trước
        var cardAdmiralIds = new List<string>();
        string getCardsQuery = "SELECT `card_admiral_id` FROM `user_card_admirals` WHERE `user_id` = @userId;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(getCardsQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cardAdmiralIds.Add(reader.GetString(0));
                    }
                }
            }

            if (cardAdmiralIds.Count == 0) return 0;

            // Bước 2: Chuẩn bị câu lệnh SQL xử lý Random cho một nhóm (Batch) các Card ID
            // Việc giới hạn danh sách ID giúp ma trận CROSS JOIN thu nhỏ lại, MySQL chạy cực nhẹ
            string batchSqlQuery = @"
            INSERT INTO `card_admirals_skills` (`user_id`, `card_admiral_id`, `skill_id`, `level`, `position`)
            WITH TargetCards AS (
                -- Chỉ lọc ra các Card nằm trong Batch hiện tại từ C#
                SELECT `card_admiral_id`, `user_id`
                FROM `user_card_admirals`
                WHERE `user_id` = @userId AND `card_admiral_id` IN ({0})
            ),
            RankedSkills AS (
                -- SỬA TẠI ĐÂY: Xáo trộn và đánh số thứ tự kỹ năng TRƯỚC khi JOIN
                -- Việc này ép MySQL tính RAND() độc lập cho bảng skills, không bị ảnh hưởng bởi số lượng Card
                SELECT 
                    `id` AS skill_id, 
                    `type`, 
                    `skill_type`,
                    ROW_NUMBER() OVER (
                        PARTITION BY `type`, `skill_type` 
                        ORDER BY RAND()
                    ) AS type_rn
                FROM `skills`
            ),
            FilteredSkills AS (
                -- SỬA TẠI ĐÂY: Lọc đúng 1 Active và 2 Passive của từng hệ ngay lập tức
                -- Tập dữ liệu này bây giờ chỉ còn đúng 3 skills * số lượng hệ (Ví dụ: 18 dòng cố định)
                SELECT 
                    skill_id, 
                    `type`, 
                    `skill_type`,
                    CASE 
                        WHEN `skill_type` = 'Active' THEN 1
                        WHEN `skill_type` = 'Passive' THEN type_rn + 1
                    END AS `position`
                FROM RankedSkills
                WHERE (`skill_type` = 'Active' AND type_rn = 1)
                OR (`skill_type` = 'Passive' AND type_rn IN (1, 2))
            )
            -- Bước cuối: JOIN ma trận siêu nhỏ giữa 500 Cards và ~18 Skills đã được cố định
            SELECT 
                tc.`user_id`, 
                tc.`card_admiral_id`, 
                fs.skill_id, 
                0 AS `level`,
                fs.`position`
            FROM TargetCards tc
            CROSS JOIN FilteredSkills fs
            ON DUPLICATE KEY UPDATE `updated_at` = CURRENT_TIMESTAMP;";

            int totalRowsAffected = 0;
            int batchSize = 500; // Mỗi đợt xử lý 500 cards để tránh quá tải MySQL

            // Bước 3: Chia nhỏ 10,000 cards thành các đợt nhỏ và thực thi
            for (int i = 0; i < cardAdmiralIds.Count; i += batchSize)
            {
                var batchIds = cardAdmiralIds.GetRange(i, Math.Min(batchSize, cardAdmiralIds.Count - i));

                using (var batchCommand = new MySqlCommand())
                {
                    batchCommand.Connection = connection;

                    // Tạo param động chống SQL Injection cho danh sách IN (...)
                    var paramNames = new List<string>();
                    for (int j = 0; j < batchIds.Count; j++)
                    {
                        string paramName = $"@cardId_{j}";
                        batchCommand.Parameters.AddWithValue(paramName, batchIds[j]);
                        paramNames.Add(paramName);
                    }

                    // Nhúng danh sách param vào câu SQL: IN (@cardId_0, @cardId_1, ...)
                    batchCommand.CommandText = string.Format(batchSqlQuery, string.Join(",", paramNames));
                    batchCommand.Parameters.AddWithValue("@userId", userId);

                    // Tăng timeout cho command này lên 60s để an toàn tuyệt đối
                    batchCommand.CommandTimeout = 60;

                    totalRowsAffected += await batchCommand.ExecuteNonQueryAsync();
                }
            }

            return totalRowsAffected;
        }
    }
    public async Task<int> AssignRandomSkillsToUserCardMonstersAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID không được để trống.", nameof(userId));
        }

        string connectionString = DatabaseConfig.ConnectionString;

        // Bước 1: Lấy toàn bộ danh sách card_hero_id của User lên bộ nhớ (C#) trước
        var cardMonsterIds = new List<string>();
        string getCardsQuery = "SELECT `card_monster_id` FROM `user_card_monsters` WHERE `user_id` = @userId;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(getCardsQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cardMonsterIds.Add(reader.GetString(0));
                    }
                }
            }

            if (cardMonsterIds.Count == 0) return 0;

            // Bước 2: Chuẩn bị câu lệnh SQL xử lý Random cho một nhóm (Batch) các Card ID
            // Việc giới hạn danh sách ID giúp ma trận CROSS JOIN thu nhỏ lại, MySQL chạy cực nhẹ
            string batchSqlQuery = @"
            INSERT INTO `card_monsters_skills` (`user_id`, `card_monster_id`, `skill_id`, `level`, `position`)
            WITH TargetCards AS (
                -- Chỉ lọc ra các Card nằm trong Batch hiện tại từ C#
                SELECT `card_monster_id`, `user_id`
                FROM `user_card_monsters`
                WHERE `user_id` = @userId AND `card_monster_id` IN ({0})
            ),
            RankedSkills AS (
                -- SỬA TẠI ĐÂY: Xáo trộn và đánh số thứ tự kỹ năng TRƯỚC khi JOIN
                -- Việc này ép MySQL tính RAND() độc lập cho bảng skills, không bị ảnh hưởng bởi số lượng Card
                SELECT 
                    `id` AS skill_id, 
                    `type`, 
                    `skill_type`,
                    ROW_NUMBER() OVER (
                        PARTITION BY `type`, `skill_type` 
                        ORDER BY RAND()
                    ) AS type_rn
                FROM `skills`
            ),
            FilteredSkills AS (
                -- SỬA TẠI ĐÂY: Lọc đúng 1 Active và 2 Passive của từng hệ ngay lập tức
                -- Tập dữ liệu này bây giờ chỉ còn đúng 3 skills * số lượng hệ (Ví dụ: 18 dòng cố định)
                SELECT 
                    skill_id, 
                    `type`, 
                    `skill_type`,
                    CASE 
                        WHEN `skill_type` = 'Active' THEN 1
                        WHEN `skill_type` = 'Passive' THEN type_rn + 1
                    END AS `position`
                FROM RankedSkills
                WHERE (`skill_type` = 'Active' AND type_rn = 1)
                OR (`skill_type` = 'Passive' AND type_rn IN (1, 2))
            )
            -- Bước cuối: JOIN ma trận siêu nhỏ giữa 500 Cards và ~18 Skills đã được cố định
            SELECT 
                tc.`user_id`, 
                tc.`card_monster_id`, 
                fs.skill_id, 
                0 AS `level`,
                fs.`position`
            FROM TargetCards tc
            CROSS JOIN FilteredSkills fs
            ON DUPLICATE KEY UPDATE `updated_at` = CURRENT_TIMESTAMP;";

            int totalRowsAffected = 0;
            int batchSize = 500; // Mỗi đợt xử lý 500 cards để tránh quá tải MySQL

            // Bước 3: Chia nhỏ 10,000 cards thành các đợt nhỏ và thực thi
            for (int i = 0; i < cardMonsterIds.Count; i += batchSize)
            {
                var batchIds = cardMonsterIds.GetRange(i, Math.Min(batchSize, cardMonsterIds.Count - i));

                using (var batchCommand = new MySqlCommand())
                {
                    batchCommand.Connection = connection;

                    // Tạo param động chống SQL Injection cho danh sách IN (...)
                    var paramNames = new List<string>();
                    for (int j = 0; j < batchIds.Count; j++)
                    {
                        string paramName = $"@cardId_{j}";
                        batchCommand.Parameters.AddWithValue(paramName, batchIds[j]);
                        paramNames.Add(paramName);
                    }

                    // Nhúng danh sách param vào câu SQL: IN (@cardId_0, @cardId_1, ...)
                    batchCommand.CommandText = string.Format(batchSqlQuery, string.Join(",", paramNames));
                    batchCommand.Parameters.AddWithValue("@userId", userId);

                    // Tăng timeout cho command này lên 60s để an toàn tuyệt đối
                    batchCommand.CommandTimeout = 60;

                    totalRowsAffected += await batchCommand.ExecuteNonQueryAsync();
                }
            }

            return totalRowsAffected;
        }
    }
    public async Task<int> AssignRandomSkillsToUserCardMilitariesAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID không được để trống.", nameof(userId));
        }

        string connectionString = DatabaseConfig.ConnectionString;

        // Bước 1: Lấy toàn bộ danh sách card_hero_id của User lên bộ nhớ (C#) trước
        var cardMilitaryIds = new List<string>();
        string getCardsQuery = "SELECT `card_military_id` FROM `user_card_militaries` WHERE `user_id` = @userId;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(getCardsQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cardMilitaryIds.Add(reader.GetString(0));
                    }
                }
            }

            if (cardMilitaryIds.Count == 0) return 0;

            // Bước 2: Chuẩn bị câu lệnh SQL xử lý Random cho một nhóm (Batch) các Card ID
            // Việc giới hạn danh sách ID giúp ma trận CROSS JOIN thu nhỏ lại, MySQL chạy cực nhẹ
            string batchSqlQuery = @"
            INSERT INTO `card_militaries_skills` (`user_id`, `card_military_id`, `skill_id`, `level`, `position`)
            WITH TargetCards AS (
                -- Chỉ lọc ra các Card nằm trong Batch hiện tại từ C#
                SELECT `card_military_id`, `user_id`
                FROM `user_card_militaries`
                WHERE `user_id` = @userId AND `card_military_id` IN ({0})
            ),
            RankedSkills AS (
                -- SỬA TẠI ĐÂY: Xáo trộn và đánh số thứ tự kỹ năng TRƯỚC khi JOIN
                -- Việc này ép MySQL tính RAND() độc lập cho bảng skills, không bị ảnh hưởng bởi số lượng Card
                SELECT 
                    `id` AS skill_id, 
                    `type`, 
                    `skill_type`,
                    ROW_NUMBER() OVER (
                        PARTITION BY `type`, `skill_type` 
                        ORDER BY RAND()
                    ) AS type_rn
                FROM `skills`
            ),
            FilteredSkills AS (
                -- SỬA TẠI ĐÂY: Lọc đúng 1 Active và 2 Passive của từng hệ ngay lập tức
                -- Tập dữ liệu này bây giờ chỉ còn đúng 3 skills * số lượng hệ (Ví dụ: 18 dòng cố định)
                SELECT 
                    skill_id, 
                    `type`, 
                    `skill_type`,
                    CASE 
                        WHEN `skill_type` = 'Active' THEN 1
                        WHEN `skill_type` = 'Passive' THEN type_rn + 1
                    END AS `position`
                FROM RankedSkills
                WHERE (`skill_type` = 'Active' AND type_rn = 1)
                OR (`skill_type` = 'Passive' AND type_rn IN (1, 2))
            )
            -- Bước cuối: JOIN ma trận siêu nhỏ giữa 500 Cards và ~18 Skills đã được cố định
            SELECT 
                tc.`user_id`, 
                tc.`card_military_id`, 
                fs.skill_id, 
                0 AS `level`,
                fs.`position`
            FROM TargetCards tc
            CROSS JOIN FilteredSkills fs
            ON DUPLICATE KEY UPDATE `updated_at` = CURRENT_TIMESTAMP;";

            int totalRowsAffected = 0;
            int batchSize = 500; // Mỗi đợt xử lý 500 cards để tránh quá tải MySQL

            // Bước 3: Chia nhỏ 10,000 cards thành các đợt nhỏ và thực thi
            for (int i = 0; i < cardMilitaryIds.Count; i += batchSize)
            {
                var batchIds = cardMilitaryIds.GetRange(i, Math.Min(batchSize, cardMilitaryIds.Count - i));

                using (var batchCommand = new MySqlCommand())
                {
                    batchCommand.Connection = connection;

                    // Tạo param động chống SQL Injection cho danh sách IN (...)
                    var paramNames = new List<string>();
                    for (int j = 0; j < batchIds.Count; j++)
                    {
                        string paramName = $"@cardId_{j}";
                        batchCommand.Parameters.AddWithValue(paramName, batchIds[j]);
                        paramNames.Add(paramName);
                    }

                    // Nhúng danh sách param vào câu SQL: IN (@cardId_0, @cardId_1, ...)
                    batchCommand.CommandText = string.Format(batchSqlQuery, string.Join(",", paramNames));
                    batchCommand.Parameters.AddWithValue("@userId", userId);

                    // Tăng timeout cho command này lên 60s để an toàn tuyệt đối
                    batchCommand.CommandTimeout = 60;

                    totalRowsAffected += await batchCommand.ExecuteNonQueryAsync();
                }
            }

            return totalRowsAffected;
        }
    }
    public async Task<int> AssignRandomSkillsToUserCardSoldiersAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID không được để trống.", nameof(userId));
        }

        string connectionString = DatabaseConfig.ConnectionString;

        // Bước 1: Lấy toàn bộ danh sách card_hero_id của User lên bộ nhớ (C#) trước
        var cardSoldierIds = new List<string>();
        string getCardsQuery = "SELECT `card_soldier_id` FROM `user_card_soldiers` WHERE `user_id` = @userId;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(getCardsQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cardSoldierIds.Add(reader.GetString(0));
                    }
                }
            }

            if (cardSoldierIds.Count == 0) return 0;

            // Bước 2: Chuẩn bị câu lệnh SQL xử lý Random cho một nhóm (Batch) các Card ID
            // Việc giới hạn danh sách ID giúp ma trận CROSS JOIN thu nhỏ lại, MySQL chạy cực nhẹ
            string batchSqlQuery = @"
            INSERT INTO `card_soldiers_skills` (`user_id`, `card_soldier_id`, `skill_id`, `level`, `position`)
            WITH TargetCards AS (
                -- Chỉ lọc ra các Card nằm trong Batch hiện tại từ C#
                SELECT `card_soldier_id`, `user_id`
                FROM `user_card_soldiers`
                WHERE `user_id` = @userId AND `card_soldier_id` IN ({0})
            ),
            RankedSkills AS (
                -- SỬA TẠI ĐÂY: Xáo trộn và đánh số thứ tự kỹ năng TRƯỚC khi JOIN
                -- Việc này ép MySQL tính RAND() độc lập cho bảng skills, không bị ảnh hưởng bởi số lượng Card
                SELECT 
                    `id` AS skill_id, 
                    `type`, 
                    `skill_type`,
                    ROW_NUMBER() OVER (
                        PARTITION BY `type`, `skill_type` 
                        ORDER BY RAND()
                    ) AS type_rn
                FROM `skills`
            ),
            FilteredSkills AS (
                -- SỬA TẠI ĐÂY: Lọc đúng 1 Active và 2 Passive của từng hệ ngay lập tức
                -- Tập dữ liệu này bây giờ chỉ còn đúng 3 skills * số lượng hệ (Ví dụ: 18 dòng cố định)
                SELECT 
                    skill_id, 
                    `type`, 
                    `skill_type`,
                    CASE 
                        WHEN `skill_type` = 'Active' THEN 1
                        WHEN `skill_type` = 'Passive' THEN type_rn + 1
                    END AS `position`
                FROM RankedSkills
                WHERE (`skill_type` = 'Active' AND type_rn = 1)
                OR (`skill_type` = 'Passive' AND type_rn IN (1, 2))
            )
            -- Bước cuối: JOIN ma trận siêu nhỏ giữa 500 Cards và ~18 Skills đã được cố định
            SELECT 
                tc.`user_id`, 
                tc.`card_soldier_id`, 
                fs.skill_id, 
                0 AS `level`,
                fs.`position`
            FROM TargetCards tc
            CROSS JOIN FilteredSkills fs
            ON DUPLICATE KEY UPDATE `updated_at` = CURRENT_TIMESTAMP;";

            int totalRowsAffected = 0;
            int batchSize = 500; // Mỗi đợt xử lý 500 cards để tránh quá tải MySQL

            // Bước 3: Chia nhỏ 10,000 cards thành các đợt nhỏ và thực thi
            for (int i = 0; i < cardSoldierIds.Count; i += batchSize)
            {
                var batchIds = cardSoldierIds.GetRange(i, Math.Min(batchSize, cardSoldierIds.Count - i));

                using (var batchCommand = new MySqlCommand())
                {
                    batchCommand.Connection = connection;

                    // Tạo param động chống SQL Injection cho danh sách IN (...)
                    var paramNames = new List<string>();
                    for (int j = 0; j < batchIds.Count; j++)
                    {
                        string paramName = $"@cardId_{j}";
                        batchCommand.Parameters.AddWithValue(paramName, batchIds[j]);
                        paramNames.Add(paramName);
                    }

                    // Nhúng danh sách param vào câu SQL: IN (@cardId_0, @cardId_1, ...)
                    batchCommand.CommandText = string.Format(batchSqlQuery, string.Join(",", paramNames));
                    batchCommand.Parameters.AddWithValue("@userId", userId);

                    // Tăng timeout cho command này lên 60s để an toàn tuyệt đối
                    batchCommand.CommandTimeout = 60;

                    totalRowsAffected += await batchCommand.ExecuteNonQueryAsync();
                }
            }

            return totalRowsAffected;
        }
    }
    public async Task<int> AssignRandomSkillsToUserCardSpellsAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID không được để trống.", nameof(userId));
        }

        string connectionString = DatabaseConfig.ConnectionString;

        // Bước 1: Lấy toàn bộ danh sách card_hero_id của User lên bộ nhớ (C#) trước
        var cardSpellIds = new List<string>();
        string getCardsQuery = "SELECT `card_spell_id` FROM `user_card_spells` WHERE `user_id` = @userId;";

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            using (var command = new MySqlCommand(getCardsQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cardSpellIds.Add(reader.GetString(0));
                    }
                }
            }

            if (cardSpellIds.Count == 0) return 0;

            // Bước 2: Chuẩn bị câu lệnh SQL xử lý Random cho một nhóm (Batch) các Card ID
            // Việc giới hạn danh sách ID giúp ma trận CROSS JOIN thu nhỏ lại, MySQL chạy cực nhẹ
            string batchSqlQuery = @"
            INSERT INTO `card_spells_skills` (`user_id`, `card_spell_id`, `skill_id`, `level`, `position`)
            WITH TargetCards AS (
                -- Chỉ lọc ra các Card nằm trong Batch hiện tại từ C#
                SELECT `card_spelll_id`, `user_id`
                FROM `user_card_spells`
                WHERE `user_id` = @userId AND `card_spell_id` IN ({0})
            ),
            RankedSkills AS (
                -- SỬA TẠI ĐÂY: Xáo trộn và đánh số thứ tự kỹ năng TRƯỚC khi JOIN
                -- Việc này ép MySQL tính RAND() độc lập cho bảng skills, không bị ảnh hưởng bởi số lượng Card
                SELECT 
                    `id` AS skill_id, 
                    `type`, 
                    `skill_type`,
                    ROW_NUMBER() OVER (
                        PARTITION BY `type`, `skill_type` 
                        ORDER BY RAND()
                    ) AS type_rn
                FROM `skills`
            ),
            FilteredSkills AS (
                -- SỬA TẠI ĐÂY: Lọc đúng 1 Active và 2 Passive của từng hệ ngay lập tức
                -- Tập dữ liệu này bây giờ chỉ còn đúng 3 skills * số lượng hệ (Ví dụ: 18 dòng cố định)
                SELECT 
                    skill_id, 
                    `type`, 
                    `skill_type`,
                    CASE 
                        WHEN `skill_type` = 'Active' THEN 1
                        WHEN `skill_type` = 'Passive' THEN type_rn + 1
                    END AS `position`
                FROM RankedSkills
                WHERE (`skill_type` = 'Active' AND type_rn = 1)
                OR (`skill_type` = 'Passive' AND type_rn IN (1, 2))
            )
            -- Bước cuối: JOIN ma trận siêu nhỏ giữa 500 Cards và ~18 Skills đã được cố định
            SELECT 
                tc.`user_id`, 
                tc.`card_spell_id`, 
                fs.skill_id, 
                0 AS `level`,
                fs.`position`
            FROM TargetCards tc
            CROSS JOIN FilteredSkills fs
            ON DUPLICATE KEY UPDATE `updated_at` = CURRENT_TIMESTAMP;";

            int totalRowsAffected = 0;
            int batchSize = 500; // Mỗi đợt xử lý 500 cards để tránh quá tải MySQL

            // Bước 3: Chia nhỏ 10,000 cards thành các đợt nhỏ và thực thi
            for (int i = 0; i < cardSpellIds.Count; i += batchSize)
            {
                var batchIds = cardSpellIds.GetRange(i, Math.Min(batchSize, cardSpellIds.Count - i));

                using (var batchCommand = new MySqlCommand())
                {
                    batchCommand.Connection = connection;

                    // Tạo param động chống SQL Injection cho danh sách IN (...)
                    var paramNames = new List<string>();
                    for (int j = 0; j < batchIds.Count; j++)
                    {
                        string paramName = $"@cardId_{j}";
                        batchCommand.Parameters.AddWithValue(paramName, batchIds[j]);
                        paramNames.Add(paramName);
                    }

                    // Nhúng danh sách param vào câu SQL: IN (@cardId_0, @cardId_1, ...)
                    batchCommand.CommandText = string.Format(batchSqlQuery, string.Join(",", paramNames));
                    batchCommand.Parameters.AddWithValue("@userId", userId);

                    // Tăng timeout cho command này lên 60s để an toàn tuyệt đối
                    batchCommand.CommandTimeout = 60;

                    totalRowsAffected += await batchCommand.ExecuteNonQueryAsync();
                }
            }

            return totalRowsAffected;
        }
    }
    #region Hàm Xử Lý Lõi (Generic Core Logic)

    /// <summary>
    /// Hàm xử lý ngẫu nhiên kỹ năng dùng chung cho tất cả các loại thẻ bài dựa trên RAM C# và Bulk Insert.
    /// </summary>
    public async Task<int> AssignRandomUserSkillsInternalAsync(string userId, string userCardTable, string targetSkillTable, string cardIdColumn)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID không được để trống.", nameof(userId));
        }

        string connectionString = DatabaseConfig.ConnectionString;

        var allSkills = new List<(string Id, string Type, string SkillType)>();
        var cardIds = new List<string>();

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            // 1. Tải toàn bộ Pool kỹ năng từ DB lên RAM C# (Chỉ lấy đúng 1 lần cho mỗi lượt chạy hàm)
            string getSkillsQuery = "SELECT `id`, `type`, `skill_type` FROM `skills`;";
            using (var cmd = new MySqlCommand(getSkillsQuery, connection))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    allSkills.Add((reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                }
            }

            if (allSkills.Count == 0) return 0;

            // 2. Lấy danh sách ID các thẻ bài hiện tại của User dựa trên bảng động truyền vào
            string getCardsQuery = $"SELECT `{cardIdColumn}` FROM `{userCardTable}` WHERE `user_id` = @userId;";
            using (var cmd = new MySqlCommand(getCardsQuery, connection))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cardIds.Add(reader.GetString(0));
                    }
                }
            }

            if (cardIds.Count == 0) return 0;

            // 3. Tiến hành chia Batch và thực hiện Bulk Insert thần tốc
            int totalRowsAffected = 0;
            int batchSize = 500; // Cân bằng tải hoàn hảo cho bộ nhớ RAM và gói tin MySQL Network Packet

            for (int i = 0; i < cardIds.Count; i += batchSize)
            {
                var batchCards = cardIds.GetRange(i, Math.Min(batchSize, cardIds.Count - i));

                var sqlBuilder = new StringBuilder();
                sqlBuilder.Append($"INSERT INTO `{targetSkillTable}` (`user_id`, `{cardIdColumn}`, `skill_id`, `level`, `position`) VALUES ");

                var parameters = new List<MySqlParameter>();
                int paramIndex = 0;

                // Xử lý logic Random độc lập hoàn toàn cho TỪNG THẺ BÀI
                foreach (var cardId in batchCards)
                {
                    // Phân nhóm kỹ năng theo Hệ (Type) và Loại chiêu (SkillType)
                    var groupedSkills = allSkills
                        .GroupBy(s => new { s.Type, s.SkillType })
                        .Select(g => new
                        {
                            g.Key.Type,
                            g.Key.SkillType,
                            // Xáo trộn ngẫu nhiên thứ tự danh sách skill trong nhóm bằng RAM của C#
                            Skills = g.OrderBy(_ => _rng.Next()).ToList()
                        });

                    foreach (var group in groupedSkills)
                    {
                        if (group.SkillType == "Active")
                        {
                            // Lấy 1 chiêu Active ngẫu nhiên duy nhất (vị trí số 1)
                            var activeSkill = group.Skills.FirstOrDefault();
                            if (activeSkill.Id != null)
                            {
                                BuildInsertRow(sqlBuilder, parameters, ref paramIndex, userId, cardId, activeSkill.Id, 1);
                            }
                        }
                        else if (group.SkillType == "Passive")
                        {
                            // Lấy tối đa 2 chiêu Passive ngẫu nhiên (vị trí số 2 và số 3)
                            var passiveSkills = group.Skills.Take(2).ToList();
                            for (int pIdx = 0; pIdx < passiveSkills.Count; pIdx++)
                            {
                                BuildInsertRow(sqlBuilder, parameters, ref paramIndex, userId, cardId, passiveSkills[pIdx].Id, pIdx + 2);
                            }
                        }
                    }
                }

                if (paramIndex == 0) continue;

                // Xử lý ký tự cuối chuỗi SQL và đính kèm cơ chế xử lý trùng ghi đè timestamp
                sqlBuilder.Length--;
                sqlBuilder.Append(" ON DUPLICATE KEY UPDATE `updated_at` = CURRENT_TIMESTAMP;");

                using (var batchCommand = new MySqlCommand(sqlBuilder.ToString(), connection))
                {
                    batchCommand.Parameters.AddRange(parameters.ToArray());
                    batchCommand.CommandTimeout = 60; // Đặt timeout lớn để bảo vệ tuyệt đối chu trình ghi dữ liệu
                    totalRowsAffected += await batchCommand.ExecuteNonQueryAsync();
                }
            }

            return totalRowsAffected;
        }
    }

    /// <summary>
    /// Hàm helper hỗ trợ gắn tham số chống SQL Injection cho chuỗi VALUES của Bulk Insert
    /// </summary>
    private void BuildInsertRow(StringBuilder sb, List<MySqlParameter> parameters, ref int index, string userId, string cardId, string skillId, int position)
    {
        string pUser = $"@u_{index}";
        string pCard = $"@c_{index}";
        string pSkill = $"@s_{index}";
        string pPos = $"@p_{index}";

        sb.Append($"({pUser}, {pCard}, {pSkill}, 0, {pPos}),");

        parameters.Add(new MySqlParameter(pUser, userId));
        parameters.Add(new MySqlParameter(pCard, cardId));
        parameters.Add(new MySqlParameter(pSkill, skillId));
        parameters.Add(new MySqlParameter(pPos, position));

        index++;
    }

    #endregion
}
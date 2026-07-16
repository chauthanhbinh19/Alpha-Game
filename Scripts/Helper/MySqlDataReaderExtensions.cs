using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public static class MySqlDataReaderExtensions
{
    // ==================== DOUBLE ====================
    public static double GetDoubleSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.GetDoubleSafe(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetDoubleSafe (string) FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static double GetDoubleSafe(this MySqlDataReader reader, int ordinal)
    {
        try
        {
            if (reader.IsDBNull(ordinal)) return 0d;
            return reader.GetDouble(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetDoubleSafe (index) FAILED | Ordinal: {ordinal} | Message: {ex.Message}");
            throw;
        }
    }


    // ==================== INT ====================
    public static int GetIntSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.GetIntSafe(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetIntSafe (string) FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static int GetIntSafe(this MySqlDataReader reader, int ordinal)
    {
        try
        {
            if (reader.IsDBNull(ordinal)) return 0;
            return reader.GetInt32(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetIntSafe (index) FAILED | Ordinal: {ordinal} | Message: {ex.Message}");
            throw;
        }
    }


    // ==================== LONG ====================
    public static long GetLongSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.GetLongSafe(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetLongSafe (string) FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static long GetLongSafe(this MySqlDataReader reader, int ordinal)
    {
        try
        {
            if (reader.IsDBNull(ordinal)) return 0L;
            return reader.GetInt64(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetLongSafe (index) FAILED | Ordinal: {ordinal} | Message: {ex.Message}");
            throw;
        }
    }


    // ==================== BOOL ====================
    public static bool GetBoolSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.GetBoolSafe(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetBoolSafe (string) FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static bool GetBoolSafe(this MySqlDataReader reader, int ordinal)
    {
        try
        {
            if (reader.IsDBNull(ordinal)) return false;
            return reader.GetBoolean(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetBoolSafe (index) FAILED | Ordinal: {ordinal} | Message: {ex.Message}");
            throw;
        }
    }


    // ==================== STRING ====================
    public static string GetStringSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.GetStringSafe(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetStringSafe (string) FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static string GetStringSafe(this MySqlDataReader reader, int ordinal)
    {
        try
        {
            if (reader.IsDBNull(ordinal)) return null;
            return reader.GetString(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetStringSafe (index) FAILED | Ordinal: {ordinal} | Message: {ex.Message}");
            throw;
        }
    }

    public static Dictionary<string, int> CacheColumns(MySqlDataReader reader)
    {
        var map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < reader.FieldCount; i++)
        {
            map[reader.GetName(i)] = i;
        }
        return map;
    }

    public static Dictionary<string, int> GetSkillOrdinals(MySqlDataReader reader, Func<string, int> getCol)
    {
        var fields = new string[] {
        "skill_id", "name", "image", "rare", "quality", "type", "star", "level", "skill_position", "skill_type",
        "experience", "quantity", "power", "health", "physical_attack", "physical_defense", "magical_attack",
        "magical_defense", "chemical_attack", "chemical_defense", "atomic_attack", "atomic_defense", "mental_attack",
        "mental_defense", "speed", "critical_damage_rate", "critical_rate", "critical_resistance_rate", "ignore_critical_rate",
        "penetration_rate", "penetration_resistance_rate", "evasion_rate", "damage_absorption_rate", "ignore_damage_absorption_rate",
        "absorbed_damage_rate", "vitality_regeneration_rate", "vitality_regeneration_resistance_rate", "accuracy_rate",
        "lifesteal_rate", "shield_strength", "tenacity", "resistance_rate", "combo_rate", "ignore_combo_rate",
        "combo_damage_rate", "combo_resistance_rate", "stun_rate", "ignore_stun_rate", "reflection_rate",
        "ignore_reflection_rate", "reflection_damage_rate", "reflection_resistance_rate", "mana", "mana_regeneration_rate",
        "damage_to_different_faction_rate", "resistance_to_different_faction_rate", "damage_to_same_faction_rate",
        "resistance_to_same_faction_rate", "normal_damage_rate", "normal_resistance_rate", "skill_damage_rate",
        "skill_resistance_rate", "description", "card_hero_id", "pattern_id", "skill_sub_type"
    };
        return fields.ToDictionary(f => f, f => getCol(f));
    }

    public static Skills MapToSkillFlat(MySqlDataReader reader, Dictionary<string, int> ords)
    {
        return new Skills
        {
            Id = reader.GetStringSafe(ords["skill_id"]),
            Name = reader.GetStringSafe(ords["name"]),
            Image = reader.GetStringSafe(ords["image"]),
            Rarity = reader.GetStringSafe(ords["rare"]),
            Quality = reader.GetDoubleSafe(ords["quality"]),
            Type = reader.GetStringSafe(ords["type"]),
            Star = reader.GetIntSafe(ords["star"]),
            Level = reader.GetIntSafe(ords["level"]),
            Position = reader.GetIntSafe(ords["skill_position"]),
            SkillType = reader.GetStringSafe(ords["skill_type"]),
            Experience = reader.GetDoubleSafe(ords["experience"]),
            Quantity = reader.GetIntSafe(ords["quantity"]),
            Power = reader.GetDoubleSafe(ords["power"]),
            Health = reader.GetDoubleSafe(ords["health"]),
            PhysicalAttack = reader.GetDoubleSafe(ords["physical_attack"]),
            PhysicalDefense = reader.GetDoubleSafe(ords["physical_defense"]),
            MagicalAttack = reader.GetDoubleSafe(ords["magical_attack"]),
            MagicalDefense = reader.GetDoubleSafe(ords["magical_defense"]),
            ChemicalAttack = reader.GetDoubleSafe(ords["chemical_attack"]),
            ChemicalDefense = reader.GetDoubleSafe(ords["chemical_defense"]),
            AtomicAttack = reader.GetDoubleSafe(ords["atomic_attack"]),
            AtomicDefense = reader.GetDoubleSafe(ords["atomic_defense"]),
            MentalAttack = reader.GetDoubleSafe(ords["mental_attack"]),
            MentalDefense = reader.GetDoubleSafe(ords["mental_defense"]),
            Speed = reader.GetDoubleSafe(ords["speed"]),
            CriticalDamageRate = reader.GetDoubleSafe(ords["critical_damage_rate"]),
            CriticalRate = reader.GetDoubleSafe(ords["critical_rate"]),
            CriticalResistanceRate = reader.GetDoubleSafe(ords["critical_resistance_rate"]),
            IgnoreCriticalRate = reader.GetDoubleSafe(ords["ignore_critical_rate"]),
            PenetrationRate = reader.GetDoubleSafe(ords["penetration_rate"]),
            PenetrationResistanceRate = reader.GetDoubleSafe(ords["penetration_resistance_rate"]),
            EvasionRate = reader.GetDoubleSafe(ords["evasion_rate"]),
            DamageAbsorptionRate = reader.GetDoubleSafe(ords["damage_absorption_rate"]),
            IgnoreDamageAbsorptionRate = reader.GetDoubleSafe(ords["ignore_damage_absorption_rate"]),
            AbsorbedDamageRate = reader.GetDoubleSafe(ords["absorbed_damage_rate"]),
            VitalityRegenerationRate = reader.GetDoubleSafe(ords["vitality_regeneration_rate"]),
            VitalityRegenerationResistanceRate = reader.GetDoubleSafe(ords["vitality_regeneration_resistance_rate"]),
            AccuracyRate = reader.GetDoubleSafe(ords["accuracy_rate"]),
            LifestealRate = reader.GetDoubleSafe(ords["lifesteal_rate"]),
            ShieldStrength = reader.GetDoubleSafe(ords["shield_strength"]),
            Tenacity = reader.GetDoubleSafe(ords["tenacity"]),
            ResistanceRate = reader.GetDoubleSafe(ords["resistance_rate"]),
            ComboRate = reader.GetDoubleSafe(ords["combo_rate"]),
            IgnoreComboRate = reader.GetDoubleSafe(ords["ignore_combo_rate"]),
            ComboDamageRate = reader.GetDoubleSafe(ords["combo_damage_rate"]),
            ComboResistanceRate = reader.GetDoubleSafe(ords["combo_resistance_rate"]),
            StunRate = reader.GetDoubleSafe(ords["stun_rate"]),
            IgnoreStunRate = reader.GetDoubleSafe(ords["ignore_stun_rate"]),
            ReflectionRate = reader.GetDoubleSafe(ords["reflection_rate"]),
            IgnoreReflectionRate = reader.GetDoubleSafe(ords["ignore_reflection_rate"]),
            ReflectionDamageRate = reader.GetDoubleSafe(ords["reflection_damage_rate"]),
            ReflectionResistanceRate = reader.GetDoubleSafe(ords["reflection_resistance_rate"]),
            Mana = reader.GetDoubleSafe(ords["mana"]),
            ManaRegenerationRate = reader.GetDoubleSafe(ords["mana_regeneration_rate"]),
            DamageToDifferentFactionRate = reader.GetDoubleSafe(ords["damage_to_different_faction_rate"]),
            ResistanceToDifferentFactionRate = reader.GetDoubleSafe(ords["resistance_to_different_faction_rate"]),
            DamageToSameFactionRate = reader.GetDoubleSafe(ords["damage_to_same_faction_rate"]),
            ResistanceToSameFactionRate = reader.GetDoubleSafe(ords["resistance_to_same_faction_rate"]),
            NormalDamageRate = reader.GetDoubleSafe(ords["normal_damage_rate"]),
            NormalResistanceRate = reader.GetDoubleSafe(ords["normal_resistance_rate"]),
            SkillDamageRate = reader.GetDoubleSafe(ords["skill_damage_rate"]),
            SkillResistanceRate = reader.GetDoubleSafe(ords["skill_resistance_rate"]),
            Description = reader.GetStringSafe(ords["description"]),

            CardId = reader.GetStringSafe(ords["card_hero_id"]),
            Pattern = new Patterns { Id = reader.GetStringSafe(ords["pattern_id"]) },
            SkillSubType = new SkillSubTypes { SubTypeCode = reader.GetStringSafe(ords["skill_sub_type"]) }
        };
    }
}

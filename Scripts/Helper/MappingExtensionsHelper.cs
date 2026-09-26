using MySqlConnector;

public static class MappingExtensionsHelper
{
    public static Achievements MapAchievementFromReader(this MySqlDataReader reader)
    {
        return new Achievements
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Alchemies MapAlchemyFromReader(this MySqlDataReader reader)
    {
        return new Alchemies
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Architectures MapArchitectureFromReader(this MySqlDataReader reader)
    {
        return new Architectures
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Artifacts MapArtifactFromReader(this MySqlDataReader reader)
    {
        return new Artifacts
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Artworks MapArtworkFromReader(this MySqlDataReader reader)
    {
        return new Artworks
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Avatars MapAvatarFromReader(this MySqlDataReader reader)
    {
        return new Avatars
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Badges MapBadgeFromReader(this MySqlDataReader reader)
    {
        return new Badges
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Beverages MapBeverageFromReader(this MySqlDataReader reader)
    {
        return new Beverages
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Books MapBookFromReader(this MySqlDataReader reader)
    {
        return new Books
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Borders MapBorderFromReader(this MySqlDataReader reader)
    {
        return new Borders
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Buildings MapBuildingFromReader(this MySqlDataReader reader)
    {
        return new Buildings
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static CardAdmirals MapCardAdmiralFromReader(this MySqlDataReader reader)
    {
        return new CardAdmirals
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static CardCaptains MapCardCaptainFromReader(this MySqlDataReader reader)
    {
        return new CardCaptains
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static CardColonels MapCardColonelFromReader(this MySqlDataReader reader)
    {
        return new CardColonels
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static CardGenerals MapCardGeneralFromReader(this MySqlDataReader reader)
    {
        return new CardGenerals
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static CardHeroes MapCardHeroFromReader(this MySqlDataReader reader)
    {
        return new CardHeroes
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static CardLives MapCardLifeFromReader(this MySqlDataReader reader)
    {
        return new CardLives
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static CardMilitaries MapCardMilitaryFromReader(this MySqlDataReader reader)
    {
        return new CardMilitaries
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static CardMonsters MapCardMonsterFromReader(this MySqlDataReader reader)
    {
        return new CardMonsters
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static CardSoldiers MapCardSoldierFromReader(this MySqlDataReader reader)
    {
        return new CardSoldiers
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static CardSpells MapCardSpellFromReader(this MySqlDataReader reader)
    {
        return new CardSpells
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static CollaborationEquipments MapCollaborationEquipmentFromReader(this MySqlDataReader reader)
    {
        return new CollaborationEquipments
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Collaborations MapCollaborationFromReader(this MySqlDataReader reader)
    {
        return new Collaborations
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Cores MapCoreFromReader(this MySqlDataReader reader)
    {
        return new Cores
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Emojis MapEmojiFromReader(this MySqlDataReader reader)
    {
        return new Emojis
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Equipments MapEquipmentFromReader(this MySqlDataReader reader)
    {
        return new Equipments
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Fashions MapFashionFromReader(this MySqlDataReader reader)
    {
        return new Fashions
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Foods MapFoodFromReader(this MySqlDataReader reader)
    {
        return new Foods
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Forges MapForgeFromReader(this MySqlDataReader reader)
    {
        return new Forges
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Furnitures MapFurnitureFromReader(this MySqlDataReader reader)
    {
        return new Furnitures
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static MagicFormationCircles MapMagicFormationCircleFromReader(this MySqlDataReader reader)
    {
        return new MagicFormationCircles
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static MechaBeasts MapMechaBeastFromReader(this MySqlDataReader reader)
    {
        return new MechaBeasts
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Medals MapMedalFromReader(this MySqlDataReader reader)
    {
        return new Medals
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Outfits MapOutfitFromReader(this MySqlDataReader reader)
    {
        return new Outfits
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Pets MapPetFromReader(this MySqlDataReader reader)
    {
        return new Pets
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Plants MapPlantFromReader(this MySqlDataReader reader)
    {
        return new Plants
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Puppets MapPuppetFromReader(this MySqlDataReader reader)
    {
        return new Puppets
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Relics MapRelicFromReader(this MySqlDataReader reader)
    {
        return new Relics
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Robots MapRobotFromReader(this MySqlDataReader reader)
    {
        return new Robots
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Runes MapRuneFromReader(this MySqlDataReader reader)
    {
        return new Runes
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Skills MapSkillFromReader(this MySqlDataReader reader)
    {
        return new Skills
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static SpiritBeasts MapSpiritBeastFromReader(this MySqlDataReader reader)
    {
        return new SpiritBeasts
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static SpiritCards MapSpiritCardFromReader(this MySqlDataReader reader)
    {
        return new SpiritCards
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Symbols MapSymbolFromReader(this MySqlDataReader reader)
    {
        return new Symbols
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Talismans MapTalismanFromReader(this MySqlDataReader reader)
    {
        return new Talismans
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Technologies MapTechnologyFromReader(this MySqlDataReader reader)
    {
        return new Technologies
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Titles MapTitleFromReader(this MySqlDataReader reader)
    {
        return new Titles
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Vehicles MapVehicleFromReader(this MySqlDataReader reader)
    {
        return new Vehicles
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    public static Weapons MapWeaponFromReader(this MySqlDataReader reader)
    {
        return new Weapons
        {
            Id = reader.GetString("id"),
            Rarity = reader.GetString("rare"),
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
    // Hàm bổ trợ map tham số vào Command
    public static void AddAchievementParameters(MySqlCommand command, string userId, Achievements achievement)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", achievement.Id);
        command.Parameters.AddWithValue("@rare", achievement.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(achievement.Rarity));
        command.Parameters.AddWithValue("@quantity", achievement.Quantity);
        command.Parameters.AddWithValue("@power", achievement.Power);
        command.Parameters.AddWithValue("@health", achievement.Health);
        command.Parameters.AddWithValue("@physical_attack", achievement.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", achievement.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", achievement.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", achievement.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", achievement.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", achievement.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", achievement.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", achievement.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", achievement.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", achievement.MentalDefense);
        command.Parameters.AddWithValue("@speed", achievement.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", achievement.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", achievement.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", achievement.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", achievement.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", achievement.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", achievement.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", achievement.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", achievement.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", achievement.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", achievement.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", achievement.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", achievement.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", achievement.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", achievement.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", achievement.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", achievement.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", achievement.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", achievement.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", achievement.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", achievement.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", achievement.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", achievement.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", achievement.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", achievement.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", achievement.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", achievement.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", achievement.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", achievement.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", achievement.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", achievement.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", achievement.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", achievement.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", achievement.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", achievement.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", achievement.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", achievement.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", achievement.SkillResistanceRate);
    }
    public static void AddAlchemyParameters(MySqlCommand command, string userId, Alchemies alchemy)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", alchemy.Id);
        command.Parameters.AddWithValue("@rare", alchemy.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(alchemy.Rarity));
        command.Parameters.AddWithValue("@quantity", alchemy.Quantity);
        command.Parameters.AddWithValue("@power", alchemy.Power);
        command.Parameters.AddWithValue("@health", alchemy.Health);
        command.Parameters.AddWithValue("@physical_attack", alchemy.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", alchemy.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", alchemy.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", alchemy.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", alchemy.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", alchemy.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", alchemy.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", alchemy.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", alchemy.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", alchemy.MentalDefense);
        command.Parameters.AddWithValue("@speed", alchemy.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", alchemy.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", alchemy.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", alchemy.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", alchemy.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", alchemy.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", alchemy.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", alchemy.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", alchemy.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", alchemy.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", alchemy.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", alchemy.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", alchemy.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", alchemy.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", alchemy.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", alchemy.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", alchemy.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", alchemy.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", alchemy.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", alchemy.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", alchemy.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", alchemy.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", alchemy.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", alchemy.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", alchemy.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", alchemy.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", alchemy.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", alchemy.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", alchemy.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", alchemy.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", alchemy.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", alchemy.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", alchemy.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", alchemy.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", alchemy.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", alchemy.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", alchemy.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", alchemy.SkillResistanceRate);
    }
    public static void AddArchitectureParameters(MySqlCommand command, string userId, Architectures architecture)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", architecture.Id);
        command.Parameters.AddWithValue("@rare", architecture.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(architecture.Rarity));
        command.Parameters.AddWithValue("@quantity", architecture.Quantity);
        command.Parameters.AddWithValue("@power", architecture.Power);
        command.Parameters.AddWithValue("@health", architecture.Health);
        command.Parameters.AddWithValue("@physical_attack", architecture.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", architecture.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", architecture.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", architecture.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", architecture.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", architecture.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", architecture.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", architecture.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", architecture.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", architecture.MentalDefense);
        command.Parameters.AddWithValue("@speed", architecture.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", architecture.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", architecture.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", architecture.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", architecture.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", architecture.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", architecture.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", architecture.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", architecture.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", architecture.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", architecture.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", architecture.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", architecture.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", architecture.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", architecture.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", architecture.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", architecture.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", architecture.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", architecture.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", architecture.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", architecture.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", architecture.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", architecture.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", architecture.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", architecture.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", architecture.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", architecture.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", architecture.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", architecture.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", architecture.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", architecture.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", architecture.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", architecture.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", architecture.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", architecture.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", architecture.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", architecture.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", architecture.SkillResistanceRate);
    }
    public static void AddArtifactParameters(MySqlCommand command, string userId, Artifacts artifact)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", artifact.Id);
        command.Parameters.AddWithValue("@rare", artifact.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(artifact.Rarity));
        command.Parameters.AddWithValue("@quantity", artifact.Quantity);
        command.Parameters.AddWithValue("@power", artifact.Power);
        command.Parameters.AddWithValue("@health", artifact.Health);
        command.Parameters.AddWithValue("@physical_attack", artifact.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", artifact.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", artifact.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", artifact.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", artifact.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", artifact.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", artifact.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", artifact.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", artifact.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", artifact.MentalDefense);
        command.Parameters.AddWithValue("@speed", artifact.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", artifact.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", artifact.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", artifact.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", artifact.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", artifact.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", artifact.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", artifact.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", artifact.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", artifact.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", artifact.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", artifact.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", artifact.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", artifact.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", artifact.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", artifact.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", artifact.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", artifact.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", artifact.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", artifact.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", artifact.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", artifact.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", artifact.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", artifact.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", artifact.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", artifact.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", artifact.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", artifact.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", artifact.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", artifact.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", artifact.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", artifact.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", artifact.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", artifact.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", artifact.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", artifact.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", artifact.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", artifact.SkillResistanceRate);
    }
    public static void AddArtworkParameters(MySqlCommand command, string userId, Artworks artwork)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", artwork.Id);
        command.Parameters.AddWithValue("@rare", artwork.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(artwork.Rarity));
        command.Parameters.AddWithValue("@quantity", artwork.Quantity);
        command.Parameters.AddWithValue("@power", artwork.Power);
        command.Parameters.AddWithValue("@health", artwork.Health);
        command.Parameters.AddWithValue("@physical_attack", artwork.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", artwork.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", artwork.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", artwork.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", artwork.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", artwork.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", artwork.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", artwork.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", artwork.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", artwork.MentalDefense);
        command.Parameters.AddWithValue("@speed", artwork.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", artwork.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", artwork.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", artwork.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", artwork.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", artwork.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", artwork.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", artwork.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", artwork.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", artwork.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", artwork.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", artwork.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", artwork.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", artwork.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", artwork.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", artwork.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", artwork.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", artwork.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", artwork.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", artwork.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", artwork.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", artwork.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", artwork.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", artwork.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", artwork.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", artwork.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", artwork.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", artwork.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", artwork.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", artwork.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", artwork.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", artwork.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", artwork.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", artwork.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", artwork.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", artwork.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", artwork.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", artwork.SkillResistanceRate);
    }
    public static void AddAvatarParameters(MySqlCommand command, string userId, Avatars avatar)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", avatar.Id);
        command.Parameters.AddWithValue("@rare", avatar.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(avatar.Rarity));
        command.Parameters.AddWithValue("@quantity", avatar.Quantity);
        command.Parameters.AddWithValue("@power", avatar.Power);
        command.Parameters.AddWithValue("@health", avatar.Health);
        command.Parameters.AddWithValue("@physical_attack", avatar.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", avatar.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", avatar.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", avatar.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", avatar.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", avatar.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", avatar.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", avatar.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", avatar.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", avatar.MentalDefense);
        command.Parameters.AddWithValue("@speed", avatar.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", avatar.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", avatar.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", avatar.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", avatar.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", avatar.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", avatar.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", avatar.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", avatar.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", avatar.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", avatar.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", avatar.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", avatar.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", avatar.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", avatar.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", avatar.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", avatar.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", avatar.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", avatar.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", avatar.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", avatar.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", avatar.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", avatar.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", avatar.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", avatar.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", avatar.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", avatar.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", avatar.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", avatar.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", avatar.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", avatar.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", avatar.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", avatar.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", avatar.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", avatar.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", avatar.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", avatar.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", avatar.SkillResistanceRate);
    }
    public static void AddBadgeParameters(MySqlCommand command, string userId, Badges badge)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", badge.Id);
        command.Parameters.AddWithValue("@rare", badge.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(badge.Rarity));
        command.Parameters.AddWithValue("@quantity", badge.Quantity);
        command.Parameters.AddWithValue("@power", badge.Power);
        command.Parameters.AddWithValue("@health", badge.Health);
        command.Parameters.AddWithValue("@physical_attack", badge.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", badge.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", badge.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", badge.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", badge.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", badge.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", badge.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", badge.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", badge.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", badge.MentalDefense);
        command.Parameters.AddWithValue("@speed", badge.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", badge.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", badge.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", badge.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", badge.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", badge.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", badge.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", badge.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", badge.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", badge.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", badge.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", badge.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", badge.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", badge.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", badge.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", badge.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", badge.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", badge.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", badge.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", badge.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", badge.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", badge.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", badge.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", badge.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", badge.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", badge.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", badge.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", badge.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", badge.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", badge.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", badge.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", badge.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", badge.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", badge.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", badge.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", badge.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", badge.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", badge.SkillResistanceRate);
    }
    public static void AddBeverageParameters(MySqlCommand command, string userId, Beverages beverage)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", beverage.Id);
        command.Parameters.AddWithValue("@rare", beverage.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(beverage.Rarity));
        command.Parameters.AddWithValue("@quantity", beverage.Quantity);
        command.Parameters.AddWithValue("@power", beverage.Power);
        command.Parameters.AddWithValue("@health", beverage.Health);
        command.Parameters.AddWithValue("@physical_attack", beverage.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", beverage.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", beverage.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", beverage.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", beverage.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", beverage.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", beverage.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", beverage.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", beverage.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", beverage.MentalDefense);
        command.Parameters.AddWithValue("@speed", beverage.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", beverage.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", beverage.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", beverage.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", beverage.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", beverage.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", beverage.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", beverage.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", beverage.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", beverage.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", beverage.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", beverage.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", beverage.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", beverage.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", beverage.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", beverage.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", beverage.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", beverage.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", beverage.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", beverage.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", beverage.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", beverage.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", beverage.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", beverage.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", beverage.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", beverage.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", beverage.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", beverage.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", beverage.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", beverage.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", beverage.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", beverage.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", beverage.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", beverage.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", beverage.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", beverage.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", beverage.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", beverage.SkillResistanceRate);
    }
    public static void AddBookParameters(MySqlCommand command, string userId, Books book)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", book.Id);
        command.Parameters.AddWithValue("@rare", book.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(book.Rarity));
        command.Parameters.AddWithValue("@quantity", book.Quantity);
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
    }
    public static void AddBorderParameters(MySqlCommand command, string userId, Borders border)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", border.Id);
        command.Parameters.AddWithValue("@rare", border.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(border.Rarity));
        command.Parameters.AddWithValue("@quantity", border.Quantity);
        command.Parameters.AddWithValue("@power", border.Power);
        command.Parameters.AddWithValue("@health", border.Health);
        command.Parameters.AddWithValue("@physical_attack", border.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", border.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", border.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", border.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", border.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", border.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", border.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", border.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", border.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", border.MentalDefense);
        command.Parameters.AddWithValue("@speed", border.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", border.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", border.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", border.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", border.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", border.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", border.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", border.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", border.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", border.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", border.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", border.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", border.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", border.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", border.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", border.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", border.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", border.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", border.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", border.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", border.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", border.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", border.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", border.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", border.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", border.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", border.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", border.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", border.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", border.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", border.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", border.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", border.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", border.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", border.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", border.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", border.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", border.SkillResistanceRate);
    }
    public static void AddBuildingParameters(MySqlCommand command, string userId, Buildings building)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", building.Id);
        command.Parameters.AddWithValue("@rare", building.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(building.Rarity));
        command.Parameters.AddWithValue("@quantity", building.Quantity);
        command.Parameters.AddWithValue("@power", building.Power);
        command.Parameters.AddWithValue("@health", building.Health);
        command.Parameters.AddWithValue("@physical_attack", building.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", building.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", building.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", building.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", building.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", building.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", building.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", building.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", building.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", building.MentalDefense);
        command.Parameters.AddWithValue("@speed", building.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", building.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", building.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", building.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", building.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", building.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", building.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", building.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", building.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", building.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", building.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", building.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", building.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", building.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", building.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", building.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", building.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", building.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", building.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", building.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", building.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", building.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", building.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", building.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", building.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", building.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", building.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", building.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", building.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", building.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", building.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", building.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", building.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", building.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", building.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", building.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", building.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", building.SkillResistanceRate);
    }
    public static void AddCardAdmiralParameters(MySqlCommand command, string userId, CardAdmirals cardAdmiral)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", cardAdmiral.Id);
        command.Parameters.AddWithValue("@rare", cardAdmiral.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardAdmiral.Rarity));
        command.Parameters.AddWithValue("@quantity", cardAdmiral.Quantity);
        command.Parameters.AddWithValue("@power", cardAdmiral.Power);
        command.Parameters.AddWithValue("@health", cardAdmiral.Health);
        command.Parameters.AddWithValue("@physical_attack", cardAdmiral.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", cardAdmiral.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", cardAdmiral.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", cardAdmiral.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", cardAdmiral.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", cardAdmiral.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", cardAdmiral.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", cardAdmiral.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", cardAdmiral.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", cardAdmiral.MentalDefense);
        command.Parameters.AddWithValue("@speed", cardAdmiral.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", cardAdmiral.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", cardAdmiral.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", cardAdmiral.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", cardAdmiral.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", cardAdmiral.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", cardAdmiral.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", cardAdmiral.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", cardAdmiral.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardAdmiral.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", cardAdmiral.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", cardAdmiral.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardAdmiral.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", cardAdmiral.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", cardAdmiral.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", cardAdmiral.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", cardAdmiral.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", cardAdmiral.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", cardAdmiral.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", cardAdmiral.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", cardAdmiral.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", cardAdmiral.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", cardAdmiral.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", cardAdmiral.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", cardAdmiral.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", cardAdmiral.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", cardAdmiral.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", cardAdmiral.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", cardAdmiral.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", cardAdmiral.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardAdmiral.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardAdmiral.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardAdmiral.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardAdmiral.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", cardAdmiral.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", cardAdmiral.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", cardAdmiral.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", cardAdmiral.SkillResistanceRate);
    }
    public static void AddCardCaptainParameters(MySqlCommand command, string userId, CardCaptains cardCaptain)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", cardCaptain.Id);
        command.Parameters.AddWithValue("@rare", cardCaptain.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardCaptain.Rarity));
        command.Parameters.AddWithValue("@quantity", cardCaptain.Quantity);
        command.Parameters.AddWithValue("@power", cardCaptain.Power);
        command.Parameters.AddWithValue("@health", cardCaptain.Health);
        command.Parameters.AddWithValue("@physical_attack", cardCaptain.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", cardCaptain.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", cardCaptain.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", cardCaptain.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", cardCaptain.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", cardCaptain.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", cardCaptain.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", cardCaptain.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", cardCaptain.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", cardCaptain.MentalDefense);
        command.Parameters.AddWithValue("@speed", cardCaptain.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", cardCaptain.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", cardCaptain.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", cardCaptain.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", cardCaptain.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", cardCaptain.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", cardCaptain.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", cardCaptain.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", cardCaptain.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardCaptain.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", cardCaptain.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", cardCaptain.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardCaptain.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", cardCaptain.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", cardCaptain.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", cardCaptain.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", cardCaptain.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", cardCaptain.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", cardCaptain.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", cardCaptain.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", cardCaptain.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", cardCaptain.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", cardCaptain.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", cardCaptain.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", cardCaptain.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", cardCaptain.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", cardCaptain.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", cardCaptain.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", cardCaptain.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", cardCaptain.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardCaptain.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardCaptain.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardCaptain.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardCaptain.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", cardCaptain.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", cardCaptain.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", cardCaptain.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", cardCaptain.SkillResistanceRate);
    }
    public static void AddCardColonelParameters(MySqlCommand command, string userId, CardColonels cardColonel)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", cardColonel.Id);
        command.Parameters.AddWithValue("@rare", cardColonel.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardColonel.Rarity));
        command.Parameters.AddWithValue("@quantity", cardColonel.Quantity);
        command.Parameters.AddWithValue("@power", cardColonel.Power);
        command.Parameters.AddWithValue("@health", cardColonel.Health);
        command.Parameters.AddWithValue("@physical_attack", cardColonel.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", cardColonel.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", cardColonel.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", cardColonel.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", cardColonel.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", cardColonel.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", cardColonel.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", cardColonel.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", cardColonel.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", cardColonel.MentalDefense);
        command.Parameters.AddWithValue("@speed", cardColonel.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", cardColonel.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", cardColonel.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", cardColonel.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", cardColonel.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", cardColonel.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", cardColonel.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", cardColonel.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", cardColonel.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardColonel.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", cardColonel.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", cardColonel.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardColonel.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", cardColonel.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", cardColonel.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", cardColonel.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", cardColonel.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", cardColonel.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", cardColonel.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", cardColonel.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", cardColonel.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", cardColonel.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", cardColonel.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", cardColonel.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", cardColonel.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", cardColonel.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", cardColonel.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", cardColonel.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", cardColonel.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", cardColonel.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardColonel.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardColonel.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardColonel.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardColonel.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", cardColonel.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", cardColonel.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", cardColonel.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", cardColonel.SkillResistanceRate);
    }
    public static void AddCardGeneralParameters(MySqlCommand command, string userId, CardGenerals cardGeneral)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", cardGeneral.Id);
        command.Parameters.AddWithValue("@rare", cardGeneral.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardGeneral.Rarity));
        command.Parameters.AddWithValue("@quantity", cardGeneral.Quantity);
        command.Parameters.AddWithValue("@power", cardGeneral.Power);
        command.Parameters.AddWithValue("@health", cardGeneral.Health);
        command.Parameters.AddWithValue("@physical_attack", cardGeneral.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", cardGeneral.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", cardGeneral.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", cardGeneral.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", cardGeneral.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", cardGeneral.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", cardGeneral.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", cardGeneral.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", cardGeneral.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", cardGeneral.MentalDefense);
        command.Parameters.AddWithValue("@speed", cardGeneral.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", cardGeneral.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", cardGeneral.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", cardGeneral.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", cardGeneral.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", cardGeneral.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", cardGeneral.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", cardGeneral.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", cardGeneral.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardGeneral.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", cardGeneral.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", cardGeneral.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardGeneral.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", cardGeneral.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", cardGeneral.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", cardGeneral.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", cardGeneral.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", cardGeneral.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", cardGeneral.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", cardGeneral.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", cardGeneral.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", cardGeneral.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", cardGeneral.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", cardGeneral.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", cardGeneral.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", cardGeneral.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", cardGeneral.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", cardGeneral.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", cardGeneral.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", cardGeneral.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardGeneral.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardGeneral.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardGeneral.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardGeneral.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", cardGeneral.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", cardGeneral.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", cardGeneral.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", cardGeneral.SkillResistanceRate);
    }
    public static void AddCardHeroParameters(MySqlCommand command, string userId, CardHeroes cardHero)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", cardHero.Id);
        command.Parameters.AddWithValue("@rare", cardHero.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardHero.Rarity));
        command.Parameters.AddWithValue("@quantity", cardHero.Quantity);
        command.Parameters.AddWithValue("@power", cardHero.Power);
        command.Parameters.AddWithValue("@health", cardHero.Health);
        command.Parameters.AddWithValue("@physical_attack", cardHero.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", cardHero.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", cardHero.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", cardHero.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", cardHero.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", cardHero.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", cardHero.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", cardHero.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", cardHero.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", cardHero.MentalDefense);
        command.Parameters.AddWithValue("@speed", cardHero.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", cardHero.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", cardHero.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", cardHero.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", cardHero.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", cardHero.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", cardHero.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", cardHero.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", cardHero.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardHero.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", cardHero.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", cardHero.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardHero.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", cardHero.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", cardHero.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", cardHero.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", cardHero.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", cardHero.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", cardHero.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", cardHero.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", cardHero.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", cardHero.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", cardHero.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", cardHero.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", cardHero.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", cardHero.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", cardHero.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", cardHero.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", cardHero.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", cardHero.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardHero.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardHero.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardHero.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardHero.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", cardHero.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", cardHero.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", cardHero.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", cardHero.SkillResistanceRate);
    }
    public static void AddCardLifeParameters(MySqlCommand command, string userId, CardLives cardLife)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", cardLife.Id);
        command.Parameters.AddWithValue("@rare", cardLife.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardLife.Rarity));
        command.Parameters.AddWithValue("@quantity", cardLife.Quantity);
        command.Parameters.AddWithValue("@power", cardLife.Power);
        command.Parameters.AddWithValue("@health", cardLife.Health);
        command.Parameters.AddWithValue("@physical_attack", cardLife.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", cardLife.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", cardLife.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", cardLife.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", cardLife.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", cardLife.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", cardLife.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", cardLife.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", cardLife.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", cardLife.MentalDefense);
        command.Parameters.AddWithValue("@speed", cardLife.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", cardLife.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", cardLife.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", cardLife.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", cardLife.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", cardLife.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", cardLife.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", cardLife.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", cardLife.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardLife.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", cardLife.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", cardLife.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardLife.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", cardLife.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", cardLife.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", cardLife.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", cardLife.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", cardLife.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", cardLife.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", cardLife.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", cardLife.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", cardLife.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", cardLife.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", cardLife.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", cardLife.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", cardLife.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", cardLife.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", cardLife.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", cardLife.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", cardLife.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardLife.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardLife.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardLife.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardLife.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", cardLife.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", cardLife.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", cardLife.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", cardLife.SkillResistanceRate);
    }
    public static void AddCardMilitaryParameters(MySqlCommand command, string userId, CardMilitaries cardMilitary)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", cardMilitary.Id);
        command.Parameters.AddWithValue("@rare", cardMilitary.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardMilitary.Rarity));
        command.Parameters.AddWithValue("@quantity", cardMilitary.Quantity);
        command.Parameters.AddWithValue("@power", cardMilitary.Power);
        command.Parameters.AddWithValue("@health", cardMilitary.Health);
        command.Parameters.AddWithValue("@physical_attack", cardMilitary.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", cardMilitary.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", cardMilitary.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", cardMilitary.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", cardMilitary.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", cardMilitary.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", cardMilitary.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", cardMilitary.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", cardMilitary.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", cardMilitary.MentalDefense);
        command.Parameters.AddWithValue("@speed", cardMilitary.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", cardMilitary.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", cardMilitary.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", cardMilitary.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", cardMilitary.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", cardMilitary.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", cardMilitary.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", cardMilitary.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", cardMilitary.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardMilitary.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", cardMilitary.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMilitary.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardMilitary.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", cardMilitary.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", cardMilitary.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", cardMilitary.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", cardMilitary.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", cardMilitary.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", cardMilitary.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", cardMilitary.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", cardMilitary.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", cardMilitary.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", cardMilitary.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", cardMilitary.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", cardMilitary.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", cardMilitary.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", cardMilitary.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", cardMilitary.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", cardMilitary.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", cardMilitary.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMilitary.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMilitary.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMilitary.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMilitary.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", cardMilitary.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", cardMilitary.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", cardMilitary.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", cardMilitary.SkillResistanceRate);
    }
    public static void AddCardMonsterParameters(MySqlCommand command, string userId, CardMonsters cardMonster)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", cardMonster.Id);
        command.Parameters.AddWithValue("@rare", cardMonster.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardMonster.Rarity));
        command.Parameters.AddWithValue("@quantity", cardMonster.Quantity);
        command.Parameters.AddWithValue("@power", cardMonster.Power);
        command.Parameters.AddWithValue("@health", cardMonster.Health);
        command.Parameters.AddWithValue("@physical_attack", cardMonster.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", cardMonster.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", cardMonster.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", cardMonster.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", cardMonster.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", cardMonster.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", cardMonster.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", cardMonster.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", cardMonster.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", cardMonster.MentalDefense);
        command.Parameters.AddWithValue("@speed", cardMonster.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", cardMonster.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", cardMonster.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", cardMonster.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", cardMonster.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", cardMonster.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", cardMonster.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", cardMonster.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", cardMonster.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardMonster.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", cardMonster.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", cardMonster.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardMonster.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", cardMonster.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", cardMonster.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", cardMonster.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", cardMonster.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", cardMonster.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", cardMonster.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", cardMonster.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", cardMonster.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", cardMonster.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", cardMonster.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", cardMonster.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", cardMonster.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", cardMonster.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", cardMonster.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", cardMonster.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", cardMonster.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", cardMonster.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardMonster.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardMonster.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardMonster.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardMonster.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", cardMonster.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", cardMonster.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", cardMonster.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", cardMonster.SkillResistanceRate);
    }
    public static void AddCardSoldierParameters(MySqlCommand command, string userId, CardSoldiers cardSoldier)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", cardSoldier.Id);
        command.Parameters.AddWithValue("@rare", cardSoldier.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardSoldier.Rarity));
        command.Parameters.AddWithValue("@quantity", cardSoldier.Quantity);
        command.Parameters.AddWithValue("@power", cardSoldier.Power);
        command.Parameters.AddWithValue("@health", cardSoldier.Health);
        command.Parameters.AddWithValue("@physical_attack", cardSoldier.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", cardSoldier.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", cardSoldier.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", cardSoldier.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", cardSoldier.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", cardSoldier.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", cardSoldier.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", cardSoldier.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", cardSoldier.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", cardSoldier.MentalDefense);
        command.Parameters.AddWithValue("@speed", cardSoldier.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", cardSoldier.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", cardSoldier.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", cardSoldier.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", cardSoldier.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", cardSoldier.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", cardSoldier.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", cardSoldier.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", cardSoldier.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardSoldier.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", cardSoldier.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", cardSoldier.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardSoldier.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", cardSoldier.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", cardSoldier.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", cardSoldier.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", cardSoldier.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", cardSoldier.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", cardSoldier.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", cardSoldier.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", cardSoldier.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", cardSoldier.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", cardSoldier.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", cardSoldier.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", cardSoldier.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", cardSoldier.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", cardSoldier.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", cardSoldier.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", cardSoldier.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", cardSoldier.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardSoldier.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardSoldier.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardSoldier.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardSoldier.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", cardSoldier.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", cardSoldier.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", cardSoldier.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", cardSoldier.SkillResistanceRate);
    }
    public static void AddCardSpellParameters(MySqlCommand command, string userId, CardSpells cardSpell)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", cardSpell.Id);
        command.Parameters.AddWithValue("@rare", cardSpell.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(cardSpell.Rarity));
        command.Parameters.AddWithValue("@quantity", cardSpell.Quantity);
        command.Parameters.AddWithValue("@power", cardSpell.Power);
        command.Parameters.AddWithValue("@health", cardSpell.Health);
        command.Parameters.AddWithValue("@physical_attack", cardSpell.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", cardSpell.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", cardSpell.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", cardSpell.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", cardSpell.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", cardSpell.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", cardSpell.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", cardSpell.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", cardSpell.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", cardSpell.MentalDefense);
        command.Parameters.AddWithValue("@speed", cardSpell.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", cardSpell.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", cardSpell.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", cardSpell.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", cardSpell.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", cardSpell.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", cardSpell.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", cardSpell.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", cardSpell.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", cardSpell.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", cardSpell.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", cardSpell.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", cardSpell.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", cardSpell.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", cardSpell.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", cardSpell.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", cardSpell.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", cardSpell.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", cardSpell.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", cardSpell.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", cardSpell.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", cardSpell.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", cardSpell.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", cardSpell.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", cardSpell.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", cardSpell.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", cardSpell.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", cardSpell.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", cardSpell.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", cardSpell.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", cardSpell.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", cardSpell.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", cardSpell.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", cardSpell.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", cardSpell.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", cardSpell.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", cardSpell.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", cardSpell.SkillResistanceRate);
    }
    public static void AddCollaborationEquipmentParameters(MySqlCommand command, string userId, CollaborationEquipments collaborationEquipment)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", collaborationEquipment.Id);
        command.Parameters.AddWithValue("@rare", collaborationEquipment.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(collaborationEquipment.Rarity));
        command.Parameters.AddWithValue("@quantity", collaborationEquipment.Quantity);
        command.Parameters.AddWithValue("@power", collaborationEquipment.Power);
        command.Parameters.AddWithValue("@health", collaborationEquipment.Health);
        command.Parameters.AddWithValue("@physical_attack", collaborationEquipment.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", collaborationEquipment.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", collaborationEquipment.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", collaborationEquipment.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", collaborationEquipment.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", collaborationEquipment.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", collaborationEquipment.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", collaborationEquipment.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", collaborationEquipment.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", collaborationEquipment.MentalDefense);
        command.Parameters.AddWithValue("@speed", collaborationEquipment.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", collaborationEquipment.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", collaborationEquipment.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", collaborationEquipment.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", collaborationEquipment.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", collaborationEquipment.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", collaborationEquipment.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", collaborationEquipment.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", collaborationEquipment.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", collaborationEquipment.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", collaborationEquipment.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", collaborationEquipment.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", collaborationEquipment.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", collaborationEquipment.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", collaborationEquipment.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", collaborationEquipment.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", collaborationEquipment.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", collaborationEquipment.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", collaborationEquipment.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", collaborationEquipment.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", collaborationEquipment.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", collaborationEquipment.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", collaborationEquipment.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", collaborationEquipment.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", collaborationEquipment.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", collaborationEquipment.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", collaborationEquipment.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", collaborationEquipment.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", collaborationEquipment.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", collaborationEquipment.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", collaborationEquipment.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", collaborationEquipment.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", collaborationEquipment.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", collaborationEquipment.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", collaborationEquipment.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", collaborationEquipment.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", collaborationEquipment.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", collaborationEquipment.SkillResistanceRate);
    }
    public static void AddCollaborationParameters(MySqlCommand command, string userId, Collaborations collaboration)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", collaboration.Id);
        command.Parameters.AddWithValue("@rare", collaboration.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(collaboration.Rarity));
        command.Parameters.AddWithValue("@quantity", collaboration.Quantity);
        command.Parameters.AddWithValue("@power", collaboration.Power);
        command.Parameters.AddWithValue("@health", collaboration.Health);
        command.Parameters.AddWithValue("@physical_attack", collaboration.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", collaboration.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", collaboration.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", collaboration.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", collaboration.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", collaboration.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", collaboration.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", collaboration.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", collaboration.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", collaboration.MentalDefense);
        command.Parameters.AddWithValue("@speed", collaboration.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", collaboration.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", collaboration.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", collaboration.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", collaboration.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", collaboration.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", collaboration.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", collaboration.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", collaboration.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", collaboration.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", collaboration.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", collaboration.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", collaboration.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", collaboration.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", collaboration.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", collaboration.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", collaboration.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", collaboration.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", collaboration.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", collaboration.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", collaboration.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", collaboration.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", collaboration.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", collaboration.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", collaboration.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", collaboration.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", collaboration.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", collaboration.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", collaboration.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", collaboration.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", collaboration.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", collaboration.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", collaboration.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", collaboration.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", collaboration.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", collaboration.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", collaboration.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", collaboration.SkillResistanceRate);
    }
    public static void AddCoreParameters(MySqlCommand command, string userId, Cores core)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", core.Id);
        command.Parameters.AddWithValue("@rare", core.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(core.Rarity));
        command.Parameters.AddWithValue("@quantity", core.Quantity);
        command.Parameters.AddWithValue("@power", core.Power);
        command.Parameters.AddWithValue("@health", core.Health);
        command.Parameters.AddWithValue("@physical_attack", core.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", core.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", core.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", core.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", core.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", core.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", core.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", core.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", core.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", core.MentalDefense);
        command.Parameters.AddWithValue("@speed", core.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", core.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", core.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", core.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", core.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", core.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", core.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", core.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", core.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", core.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", core.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", core.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", core.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", core.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", core.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", core.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", core.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", core.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", core.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", core.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", core.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", core.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", core.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", core.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", core.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", core.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", core.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", core.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", core.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", core.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", core.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", core.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", core.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", core.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", core.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", core.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", core.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", core.SkillResistanceRate);
    }
    public static void AddEmojiParameters(MySqlCommand command, string userId, Emojis emoji)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", emoji.Id);
        command.Parameters.AddWithValue("@rare", emoji.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(emoji.Rarity));
        command.Parameters.AddWithValue("@quantity", emoji.Quantity);
        command.Parameters.AddWithValue("@power", emoji.Power);
        command.Parameters.AddWithValue("@health", emoji.Health);
        command.Parameters.AddWithValue("@physical_attack", emoji.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", emoji.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", emoji.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", emoji.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", emoji.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", emoji.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", emoji.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", emoji.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", emoji.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", emoji.MentalDefense);
        command.Parameters.AddWithValue("@speed", emoji.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", emoji.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", emoji.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", emoji.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", emoji.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", emoji.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", emoji.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", emoji.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", emoji.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", emoji.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", emoji.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", emoji.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", emoji.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", emoji.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", emoji.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", emoji.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", emoji.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", emoji.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", emoji.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", emoji.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", emoji.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", emoji.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", emoji.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", emoji.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", emoji.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", emoji.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", emoji.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", emoji.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", emoji.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", emoji.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", emoji.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", emoji.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", emoji.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", emoji.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", emoji.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", emoji.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", emoji.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", emoji.SkillResistanceRate);
    }
    public static void AddEquipmentParameters(MySqlCommand command, string userId, Equipments equipment)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", equipment.Id);
        command.Parameters.AddWithValue("@rare", equipment.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(equipment.Rarity));
        command.Parameters.AddWithValue("@quantity", equipment.Quantity);
        command.Parameters.AddWithValue("@power", equipment.Power);
        command.Parameters.AddWithValue("@health", equipment.Health);
        command.Parameters.AddWithValue("@physical_attack", equipment.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", equipment.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", equipment.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", equipment.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", equipment.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", equipment.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", equipment.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", equipment.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", equipment.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", equipment.MentalDefense);
        command.Parameters.AddWithValue("@speed", equipment.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", equipment.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", equipment.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", equipment.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", equipment.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", equipment.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", equipment.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", equipment.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", equipment.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", equipment.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", equipment.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", equipment.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", equipment.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", equipment.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", equipment.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", equipment.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", equipment.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", equipment.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", equipment.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", equipment.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", equipment.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", equipment.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", equipment.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", equipment.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", equipment.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", equipment.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", equipment.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", equipment.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", equipment.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", equipment.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", equipment.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", equipment.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", equipment.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", equipment.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", equipment.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", equipment.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", equipment.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", equipment.SkillResistanceRate);
    }
    public static void AddFashionParameters(MySqlCommand command, string userId, Fashions fashion)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", fashion.Id);
        command.Parameters.AddWithValue("@rare", fashion.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(fashion.Rarity));
        command.Parameters.AddWithValue("@quantity", fashion.Quantity);
        command.Parameters.AddWithValue("@power", fashion.Power);
        command.Parameters.AddWithValue("@health", fashion.Health);
        command.Parameters.AddWithValue("@physical_attack", fashion.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", fashion.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", fashion.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", fashion.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", fashion.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", fashion.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", fashion.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", fashion.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", fashion.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", fashion.MentalDefense);
        command.Parameters.AddWithValue("@speed", fashion.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", fashion.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", fashion.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", fashion.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", fashion.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", fashion.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", fashion.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", fashion.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", fashion.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", fashion.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", fashion.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", fashion.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", fashion.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", fashion.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", fashion.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", fashion.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", fashion.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", fashion.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", fashion.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", fashion.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", fashion.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", fashion.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", fashion.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", fashion.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", fashion.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", fashion.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", fashion.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", fashion.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", fashion.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", fashion.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", fashion.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", fashion.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", fashion.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", fashion.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", fashion.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", fashion.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", fashion.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", fashion.SkillResistanceRate);
    }
    public static void AddFoodParameters(MySqlCommand command, string userId, Foods food)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", food.Id);
        command.Parameters.AddWithValue("@rare", food.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(food.Rarity));
        command.Parameters.AddWithValue("@quantity", food.Quantity);
        command.Parameters.AddWithValue("@power", food.Power);
        command.Parameters.AddWithValue("@health", food.Health);
        command.Parameters.AddWithValue("@physical_attack", food.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", food.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", food.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", food.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", food.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", food.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", food.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", food.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", food.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", food.MentalDefense);
        command.Parameters.AddWithValue("@speed", food.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", food.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", food.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", food.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", food.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", food.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", food.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", food.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", food.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", food.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", food.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", food.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", food.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", food.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", food.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", food.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", food.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", food.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", food.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", food.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", food.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", food.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", food.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", food.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", food.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", food.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", food.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", food.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", food.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", food.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", food.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", food.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", food.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", food.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", food.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", food.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", food.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", food.SkillResistanceRate);
    }
    public static void AddForgeParameters(MySqlCommand command, string userId, Forges forge)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", forge.Id);
        command.Parameters.AddWithValue("@rare", forge.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(forge.Rarity));
        command.Parameters.AddWithValue("@quantity", forge.Quantity);
        command.Parameters.AddWithValue("@power", forge.Power);
        command.Parameters.AddWithValue("@health", forge.Health);
        command.Parameters.AddWithValue("@physical_attack", forge.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", forge.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", forge.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", forge.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", forge.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", forge.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", forge.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", forge.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", forge.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", forge.MentalDefense);
        command.Parameters.AddWithValue("@speed", forge.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", forge.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", forge.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", forge.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", forge.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", forge.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", forge.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", forge.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", forge.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", forge.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", forge.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", forge.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", forge.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", forge.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", forge.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", forge.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", forge.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", forge.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", forge.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", forge.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", forge.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", forge.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", forge.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", forge.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", forge.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", forge.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", forge.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", forge.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", forge.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", forge.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", forge.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", forge.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", forge.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", forge.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", forge.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", forge.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", forge.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", forge.SkillResistanceRate);
    }
    public static void AddFurnitureParameters(MySqlCommand command, string userId, Furnitures furniture)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", furniture.Id);
        command.Parameters.AddWithValue("@rare", furniture.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(furniture.Rarity));
        command.Parameters.AddWithValue("@quantity", furniture.Quantity);
        command.Parameters.AddWithValue("@power", furniture.Power);
        command.Parameters.AddWithValue("@health", furniture.Health);
        command.Parameters.AddWithValue("@physical_attack", furniture.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", furniture.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", furniture.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", furniture.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", furniture.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", furniture.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", furniture.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", furniture.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", furniture.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", furniture.MentalDefense);
        command.Parameters.AddWithValue("@speed", furniture.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", furniture.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", furniture.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", furniture.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", furniture.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", furniture.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", furniture.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", furniture.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", furniture.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", furniture.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", furniture.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", furniture.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", furniture.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", furniture.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", furniture.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", furniture.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", furniture.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", furniture.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", furniture.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", furniture.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", furniture.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", furniture.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", furniture.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", furniture.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", furniture.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", furniture.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", furniture.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", furniture.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", furniture.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", furniture.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", furniture.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", furniture.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", furniture.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", furniture.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", furniture.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", furniture.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", furniture.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", furniture.SkillResistanceRate);
    }
    public static void AddMagicFormationCircleParameters(MySqlCommand command, string userId, MagicFormationCircles magicFormationCircle)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", magicFormationCircle.Id);
        command.Parameters.AddWithValue("@rare", magicFormationCircle.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(magicFormationCircle.Rarity));
        command.Parameters.AddWithValue("@quantity", magicFormationCircle.Quantity);
        command.Parameters.AddWithValue("@power", magicFormationCircle.Power);
        command.Parameters.AddWithValue("@health", magicFormationCircle.Health);
        command.Parameters.AddWithValue("@physical_attack", magicFormationCircle.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", magicFormationCircle.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", magicFormationCircle.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", magicFormationCircle.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", magicFormationCircle.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", magicFormationCircle.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", magicFormationCircle.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", magicFormationCircle.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", magicFormationCircle.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", magicFormationCircle.MentalDefense);
        command.Parameters.AddWithValue("@speed", magicFormationCircle.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", magicFormationCircle.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", magicFormationCircle.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", magicFormationCircle.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", magicFormationCircle.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", magicFormationCircle.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", magicFormationCircle.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", magicFormationCircle.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", magicFormationCircle.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", magicFormationCircle.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", magicFormationCircle.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", magicFormationCircle.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", magicFormationCircle.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", magicFormationCircle.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", magicFormationCircle.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", magicFormationCircle.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", magicFormationCircle.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", magicFormationCircle.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", magicFormationCircle.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", magicFormationCircle.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", magicFormationCircle.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", magicFormationCircle.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", magicFormationCircle.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", magicFormationCircle.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", magicFormationCircle.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", magicFormationCircle.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", magicFormationCircle.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", magicFormationCircle.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", magicFormationCircle.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", magicFormationCircle.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", magicFormationCircle.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", magicFormationCircle.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", magicFormationCircle.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", magicFormationCircle.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", magicFormationCircle.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", magicFormationCircle.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", magicFormationCircle.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", magicFormationCircle.SkillResistanceRate);
    }
    public static void AddMechaBeastParameters(MySqlCommand command, string userId, MechaBeasts mechaBeast)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", mechaBeast.Id);
        command.Parameters.AddWithValue("@rare", mechaBeast.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(mechaBeast.Rarity));
        command.Parameters.AddWithValue("@quantity", mechaBeast.Quantity);
        command.Parameters.AddWithValue("@power", mechaBeast.Power);
        command.Parameters.AddWithValue("@health", mechaBeast.Health);
        command.Parameters.AddWithValue("@physical_attack", mechaBeast.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", mechaBeast.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", mechaBeast.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", mechaBeast.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", mechaBeast.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", mechaBeast.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", mechaBeast.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", mechaBeast.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", mechaBeast.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", mechaBeast.MentalDefense);
        command.Parameters.AddWithValue("@speed", mechaBeast.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", mechaBeast.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", mechaBeast.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", mechaBeast.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", mechaBeast.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", mechaBeast.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", mechaBeast.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", mechaBeast.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", mechaBeast.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", mechaBeast.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", mechaBeast.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", mechaBeast.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", mechaBeast.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", mechaBeast.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", mechaBeast.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", mechaBeast.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", mechaBeast.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", mechaBeast.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", mechaBeast.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", mechaBeast.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", mechaBeast.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", mechaBeast.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", mechaBeast.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", mechaBeast.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", mechaBeast.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", mechaBeast.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", mechaBeast.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", mechaBeast.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", mechaBeast.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", mechaBeast.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", mechaBeast.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", mechaBeast.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", mechaBeast.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", mechaBeast.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", mechaBeast.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", mechaBeast.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", mechaBeast.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", mechaBeast.SkillResistanceRate);
    }
    public static void AddMedalParameters(MySqlCommand command, string userId, Medals medal)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", medal.Id);
        command.Parameters.AddWithValue("@rare", medal.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(medal.Rarity));
        command.Parameters.AddWithValue("@quantity", medal.Quantity);
        command.Parameters.AddWithValue("@power", medal.Power);
        command.Parameters.AddWithValue("@health", medal.Health);
        command.Parameters.AddWithValue("@physical_attack", medal.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", medal.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", medal.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", medal.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", medal.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", medal.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", medal.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", medal.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", medal.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", medal.MentalDefense);
        command.Parameters.AddWithValue("@speed", medal.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", medal.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", medal.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", medal.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", medal.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", medal.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", medal.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", medal.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", medal.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", medal.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", medal.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", medal.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", medal.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", medal.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", medal.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", medal.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", medal.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", medal.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", medal.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", medal.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", medal.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", medal.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", medal.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", medal.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", medal.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", medal.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", medal.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", medal.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", medal.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", medal.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", medal.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", medal.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", medal.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", medal.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", medal.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", medal.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", medal.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", medal.SkillResistanceRate);
    }
    public static void AddOutfitParameters(MySqlCommand command, string userId, Outfits outfit)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", outfit.Id);
        command.Parameters.AddWithValue("@rare", outfit.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(outfit.Rarity));
        command.Parameters.AddWithValue("@quantity", outfit.Quantity);
        command.Parameters.AddWithValue("@power", outfit.Power);
        command.Parameters.AddWithValue("@health", outfit.Health);
        command.Parameters.AddWithValue("@physical_attack", outfit.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", outfit.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", outfit.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", outfit.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", outfit.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", outfit.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", outfit.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", outfit.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", outfit.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", outfit.MentalDefense);
        command.Parameters.AddWithValue("@speed", outfit.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", outfit.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", outfit.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", outfit.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", outfit.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", outfit.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", outfit.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", outfit.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", outfit.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", outfit.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", outfit.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", outfit.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", outfit.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", outfit.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", outfit.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", outfit.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", outfit.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", outfit.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", outfit.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", outfit.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", outfit.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", outfit.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", outfit.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", outfit.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", outfit.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", outfit.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", outfit.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", outfit.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", outfit.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", outfit.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", outfit.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", outfit.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", outfit.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", outfit.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", outfit.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", outfit.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", outfit.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", outfit.SkillResistanceRate);
    }
    public static void AddPetParameters(MySqlCommand command, string userId, Pets pet)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", pet.Id);
        command.Parameters.AddWithValue("@rare", pet.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(pet.Rarity));
        command.Parameters.AddWithValue("@quantity", pet.Quantity);
        command.Parameters.AddWithValue("@power", pet.Power);
        command.Parameters.AddWithValue("@health", pet.Health);
        command.Parameters.AddWithValue("@physical_attack", pet.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", pet.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", pet.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", pet.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", pet.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", pet.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", pet.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", pet.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", pet.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", pet.MentalDefense);
        command.Parameters.AddWithValue("@speed", pet.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", pet.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", pet.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", pet.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", pet.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", pet.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", pet.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", pet.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", pet.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", pet.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", pet.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", pet.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", pet.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", pet.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", pet.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", pet.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", pet.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", pet.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", pet.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", pet.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", pet.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", pet.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", pet.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", pet.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", pet.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", pet.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", pet.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", pet.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", pet.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", pet.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", pet.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", pet.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", pet.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", pet.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", pet.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", pet.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", pet.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", pet.SkillResistanceRate);
    }
    public static void AddPlantParameters(MySqlCommand command, string userId, Plants plant)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", plant.Id);
        command.Parameters.AddWithValue("@rare", plant.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(plant.Rarity));
        command.Parameters.AddWithValue("@quantity", plant.Quantity);
        command.Parameters.AddWithValue("@power", plant.Power);
        command.Parameters.AddWithValue("@health", plant.Health);
        command.Parameters.AddWithValue("@physical_attack", plant.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", plant.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", plant.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", plant.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", plant.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", plant.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", plant.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", plant.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", plant.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", plant.MentalDefense);
        command.Parameters.AddWithValue("@speed", plant.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", plant.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", plant.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", plant.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", plant.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", plant.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", plant.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", plant.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", plant.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", plant.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", plant.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", plant.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", plant.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", plant.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", plant.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", plant.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", plant.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", plant.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", plant.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", plant.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", plant.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", plant.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", plant.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", plant.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", plant.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", plant.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", plant.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", plant.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", plant.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", plant.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", plant.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", plant.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", plant.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", plant.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", plant.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", plant.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", plant.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", plant.SkillResistanceRate);
    }
    public static void AddPuppetParameters(MySqlCommand command, string userId, Puppets puppet)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", puppet.Id);
        command.Parameters.AddWithValue("@rare", puppet.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(puppet.Rarity));
        command.Parameters.AddWithValue("@quantity", puppet.Quantity);
        command.Parameters.AddWithValue("@power", puppet.Power);
        command.Parameters.AddWithValue("@health", puppet.Health);
        command.Parameters.AddWithValue("@physical_attack", puppet.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", puppet.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", puppet.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", puppet.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", puppet.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", puppet.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", puppet.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", puppet.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", puppet.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", puppet.MentalDefense);
        command.Parameters.AddWithValue("@speed", puppet.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", puppet.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", puppet.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", puppet.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", puppet.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", puppet.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", puppet.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", puppet.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", puppet.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", puppet.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", puppet.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", puppet.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", puppet.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", puppet.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", puppet.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", puppet.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", puppet.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", puppet.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", puppet.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", puppet.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", puppet.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", puppet.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", puppet.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", puppet.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", puppet.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", puppet.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", puppet.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", puppet.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", puppet.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", puppet.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", puppet.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", puppet.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", puppet.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", puppet.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", puppet.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", puppet.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", puppet.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", puppet.SkillResistanceRate);
    }
    public static void AddRelicParameters(MySqlCommand command, string userId, Relics relic)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", relic.Id);
        command.Parameters.AddWithValue("@rare", relic.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(relic.Rarity));
        command.Parameters.AddWithValue("@quantity", relic.Quantity);
        command.Parameters.AddWithValue("@power", relic.Power);
        command.Parameters.AddWithValue("@health", relic.Health);
        command.Parameters.AddWithValue("@physical_attack", relic.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", relic.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", relic.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", relic.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", relic.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", relic.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", relic.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", relic.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", relic.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", relic.MentalDefense);
        command.Parameters.AddWithValue("@speed", relic.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", relic.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", relic.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", relic.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", relic.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", relic.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", relic.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", relic.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", relic.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", relic.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", relic.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", relic.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", relic.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", relic.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", relic.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", relic.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", relic.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", relic.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", relic.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", relic.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", relic.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", relic.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", relic.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", relic.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", relic.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", relic.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", relic.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", relic.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", relic.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", relic.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", relic.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", relic.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", relic.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", relic.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", relic.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", relic.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", relic.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", relic.SkillResistanceRate);
    }
    public static void AddRobotParameters(MySqlCommand command, string userId, Robots robot)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", robot.Id);
        command.Parameters.AddWithValue("@rare", robot.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(robot.Rarity));
        command.Parameters.AddWithValue("@quantity", robot.Quantity);
        command.Parameters.AddWithValue("@power", robot.Power);
        command.Parameters.AddWithValue("@health", robot.Health);
        command.Parameters.AddWithValue("@physical_attack", robot.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", robot.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", robot.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", robot.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", robot.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", robot.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", robot.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", robot.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", robot.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", robot.MentalDefense);
        command.Parameters.AddWithValue("@speed", robot.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", robot.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", robot.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", robot.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", robot.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", robot.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", robot.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", robot.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", robot.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", robot.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", robot.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", robot.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", robot.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", robot.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", robot.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", robot.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", robot.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", robot.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", robot.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", robot.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", robot.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", robot.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", robot.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", robot.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", robot.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", robot.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", robot.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", robot.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", robot.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", robot.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", robot.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", robot.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", robot.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", robot.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", robot.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", robot.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", robot.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", robot.SkillResistanceRate);
    }
    public static void AddRuneParameters(MySqlCommand command, string userId, Runes rune)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", rune.Id);
        command.Parameters.AddWithValue("@rare", rune.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(rune.Rarity));
        command.Parameters.AddWithValue("@quantity", rune.Quantity);
        command.Parameters.AddWithValue("@power", rune.Power);
        command.Parameters.AddWithValue("@health", rune.Health);
        command.Parameters.AddWithValue("@physical_attack", rune.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", rune.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", rune.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", rune.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", rune.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", rune.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", rune.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", rune.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", rune.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", rune.MentalDefense);
        command.Parameters.AddWithValue("@speed", rune.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", rune.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", rune.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", rune.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", rune.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", rune.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", rune.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", rune.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", rune.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", rune.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", rune.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", rune.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", rune.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", rune.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", rune.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", rune.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", rune.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", rune.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", rune.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", rune.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", rune.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", rune.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", rune.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", rune.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", rune.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", rune.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", rune.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", rune.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", rune.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", rune.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", rune.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", rune.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", rune.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", rune.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", rune.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", rune.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", rune.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", rune.SkillResistanceRate);
    }
    public static void AddSkillParameters(MySqlCommand command, string userId, Skills skill)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", skill.Id);
        command.Parameters.AddWithValue("@rare", skill.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(skill.Rarity));
        command.Parameters.AddWithValue("@quantity", skill.Quantity);
        command.Parameters.AddWithValue("@power", skill.Power);
        command.Parameters.AddWithValue("@health", skill.Health);
        command.Parameters.AddWithValue("@physical_attack", skill.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", skill.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", skill.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", skill.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", skill.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", skill.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", skill.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", skill.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", skill.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", skill.MentalDefense);
        command.Parameters.AddWithValue("@speed", skill.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", skill.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", skill.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", skill.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", skill.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", skill.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", skill.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", skill.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", skill.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", skill.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", skill.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", skill.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", skill.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", skill.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", skill.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", skill.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", skill.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", skill.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", skill.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", skill.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", skill.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", skill.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", skill.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", skill.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", skill.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", skill.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", skill.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", skill.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", skill.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", skill.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", skill.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", skill.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", skill.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", skill.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", skill.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", skill.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", skill.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", skill.SkillResistanceRate);
    }
    public static void AddSpiritBeastParameters(MySqlCommand command, string userId, SpiritBeasts spiritBeast)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", spiritBeast.Id);
        command.Parameters.AddWithValue("@rare", spiritBeast.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(spiritBeast.Rarity));
        command.Parameters.AddWithValue("@quantity", spiritBeast.Quantity);
        command.Parameters.AddWithValue("@power", spiritBeast.Power);
        command.Parameters.AddWithValue("@health", spiritBeast.Health);
        command.Parameters.AddWithValue("@physical_attack", spiritBeast.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", spiritBeast.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", spiritBeast.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", spiritBeast.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", spiritBeast.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", spiritBeast.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", spiritBeast.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", spiritBeast.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", spiritBeast.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", spiritBeast.MentalDefense);
        command.Parameters.AddWithValue("@speed", spiritBeast.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", spiritBeast.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", spiritBeast.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", spiritBeast.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", spiritBeast.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", spiritBeast.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", spiritBeast.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", spiritBeast.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", spiritBeast.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", spiritBeast.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", spiritBeast.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", spiritBeast.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", spiritBeast.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", spiritBeast.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", spiritBeast.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", spiritBeast.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", spiritBeast.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", spiritBeast.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", spiritBeast.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", spiritBeast.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", spiritBeast.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", spiritBeast.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", spiritBeast.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", spiritBeast.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", spiritBeast.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", spiritBeast.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", spiritBeast.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", spiritBeast.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", spiritBeast.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", spiritBeast.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", spiritBeast.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", spiritBeast.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", spiritBeast.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", spiritBeast.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", spiritBeast.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", spiritBeast.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", spiritBeast.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", spiritBeast.SkillResistanceRate);
    }
    public static void AddSpiritCardParameters(MySqlCommand command, string userId, SpiritCards spiritCard)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", spiritCard.Id);
        command.Parameters.AddWithValue("@rare", spiritCard.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(spiritCard.Rarity));
        command.Parameters.AddWithValue("@quantity", spiritCard.Quantity);
        command.Parameters.AddWithValue("@power", spiritCard.Power);
        command.Parameters.AddWithValue("@health", spiritCard.Health);
        command.Parameters.AddWithValue("@physical_attack", spiritCard.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", spiritCard.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", spiritCard.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", spiritCard.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", spiritCard.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", spiritCard.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", spiritCard.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", spiritCard.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", spiritCard.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", spiritCard.MentalDefense);
        command.Parameters.AddWithValue("@speed", spiritCard.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", spiritCard.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", spiritCard.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", spiritCard.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", spiritCard.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", spiritCard.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", spiritCard.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", spiritCard.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", spiritCard.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", spiritCard.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", spiritCard.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", spiritCard.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", spiritCard.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", spiritCard.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", spiritCard.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", spiritCard.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", spiritCard.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", spiritCard.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", spiritCard.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", spiritCard.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", spiritCard.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", spiritCard.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", spiritCard.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", spiritCard.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", spiritCard.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", spiritCard.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", spiritCard.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", spiritCard.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", spiritCard.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", spiritCard.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", spiritCard.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", spiritCard.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", spiritCard.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", spiritCard.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", spiritCard.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", spiritCard.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", spiritCard.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", spiritCard.SkillResistanceRate);
    }
    public static void AddSymbolParameters(MySqlCommand command, string userId, Symbols symbol)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", symbol.Id);
        command.Parameters.AddWithValue("@rare", symbol.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(symbol.Rarity));
        command.Parameters.AddWithValue("@quantity", symbol.Quantity);
        command.Parameters.AddWithValue("@power", symbol.Power);
        command.Parameters.AddWithValue("@health", symbol.Health);
        command.Parameters.AddWithValue("@physical_attack", symbol.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", symbol.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", symbol.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", symbol.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", symbol.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", symbol.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", symbol.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", symbol.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", symbol.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", symbol.MentalDefense);
        command.Parameters.AddWithValue("@speed", symbol.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", symbol.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", symbol.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", symbol.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", symbol.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", symbol.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", symbol.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", symbol.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", symbol.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", symbol.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", symbol.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", symbol.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", symbol.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", symbol.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", symbol.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", symbol.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", symbol.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", symbol.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", symbol.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", symbol.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", symbol.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", symbol.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", symbol.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", symbol.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", symbol.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", symbol.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", symbol.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", symbol.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", symbol.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", symbol.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", symbol.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", symbol.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", symbol.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", symbol.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", symbol.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", symbol.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", symbol.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", symbol.SkillResistanceRate);
    }
    public static void AddTalismanParameters(MySqlCommand command, string userId, Talismans talisman)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", talisman.Id);
        command.Parameters.AddWithValue("@rare", talisman.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(talisman.Rarity));
        command.Parameters.AddWithValue("@quantity", talisman.Quantity);
        command.Parameters.AddWithValue("@power", talisman.Power);
        command.Parameters.AddWithValue("@health", talisman.Health);
        command.Parameters.AddWithValue("@physical_attack", talisman.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", talisman.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", talisman.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", talisman.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", talisman.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", talisman.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", talisman.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", talisman.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", talisman.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", talisman.MentalDefense);
        command.Parameters.AddWithValue("@speed", talisman.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", talisman.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", talisman.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", talisman.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", talisman.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", talisman.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", talisman.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", talisman.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", talisman.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", talisman.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", talisman.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", talisman.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", talisman.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", talisman.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", talisman.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", talisman.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", talisman.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", talisman.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", talisman.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", talisman.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", talisman.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", talisman.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", talisman.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", talisman.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", talisman.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", talisman.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", talisman.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", talisman.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", talisman.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", talisman.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", talisman.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", talisman.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", talisman.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", talisman.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", talisman.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", talisman.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", talisman.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", talisman.SkillResistanceRate);
    }
    public static void AddTechnologyParameters(MySqlCommand command, string userId, Technologies technology)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", technology.Id);
        command.Parameters.AddWithValue("@rare", technology.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(technology.Rarity));
        command.Parameters.AddWithValue("@quantity", technology.Quantity);
        command.Parameters.AddWithValue("@power", technology.Power);
        command.Parameters.AddWithValue("@health", technology.Health);
        command.Parameters.AddWithValue("@physical_attack", technology.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", technology.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", technology.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", technology.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", technology.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", technology.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", technology.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", technology.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", technology.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", technology.MentalDefense);
        command.Parameters.AddWithValue("@speed", technology.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", technology.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", technology.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", technology.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", technology.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", technology.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", technology.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", technology.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", technology.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", technology.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", technology.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", technology.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", technology.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", technology.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", technology.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", technology.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", technology.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", technology.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", technology.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", technology.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", technology.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", technology.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", technology.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", technology.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", technology.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", technology.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", technology.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", technology.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", technology.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", technology.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", technology.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", technology.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", technology.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", technology.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", technology.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", technology.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", technology.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", technology.SkillResistanceRate);
    }
    public static void AddTitleParameters(MySqlCommand command, string userId, Titles title)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", title.Id);
        command.Parameters.AddWithValue("@rare", title.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(title.Rarity));
        command.Parameters.AddWithValue("@quantity", title.Quantity);
        command.Parameters.AddWithValue("@power", title.Power);
        command.Parameters.AddWithValue("@health", title.Health);
        command.Parameters.AddWithValue("@physical_attack", title.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", title.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", title.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", title.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", title.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", title.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", title.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", title.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", title.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", title.MentalDefense);
        command.Parameters.AddWithValue("@speed", title.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", title.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", title.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", title.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", title.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", title.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", title.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", title.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", title.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", title.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", title.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", title.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", title.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", title.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", title.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", title.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", title.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", title.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", title.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", title.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", title.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", title.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", title.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", title.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", title.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", title.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", title.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", title.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", title.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", title.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", title.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", title.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", title.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", title.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", title.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", title.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", title.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", title.SkillResistanceRate);
    }
    public static void AddVehicleParameters(MySqlCommand command, string userId, Vehicles vehicle)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", vehicle.Id);
        command.Parameters.AddWithValue("@rare", vehicle.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(vehicle.Rarity));
        command.Parameters.AddWithValue("@quantity", vehicle.Quantity);
        command.Parameters.AddWithValue("@power", vehicle.Power);
        command.Parameters.AddWithValue("@health", vehicle.Health);
        command.Parameters.AddWithValue("@physical_attack", vehicle.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", vehicle.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", vehicle.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", vehicle.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", vehicle.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", vehicle.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", vehicle.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", vehicle.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", vehicle.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", vehicle.MentalDefense);
        command.Parameters.AddWithValue("@speed", vehicle.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", vehicle.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", vehicle.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", vehicle.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", vehicle.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", vehicle.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", vehicle.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", vehicle.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", vehicle.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", vehicle.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", vehicle.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", vehicle.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", vehicle.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", vehicle.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", vehicle.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", vehicle.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", vehicle.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", vehicle.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", vehicle.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", vehicle.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", vehicle.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", vehicle.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", vehicle.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", vehicle.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", vehicle.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", vehicle.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", vehicle.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", vehicle.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", vehicle.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", vehicle.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", vehicle.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", vehicle.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", vehicle.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", vehicle.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", vehicle.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", vehicle.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", vehicle.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", vehicle.SkillResistanceRate);
    }
    public static void AddWeaponParameters(MySqlCommand command, string userId, Weapons weapon)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@object_id", weapon.Id);
        command.Parameters.AddWithValue("@rare", weapon.Rarity);
        command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(weapon.Rarity));
        command.Parameters.AddWithValue("@quantity", weapon.Quantity);
        command.Parameters.AddWithValue("@power", weapon.Power);
        command.Parameters.AddWithValue("@health", weapon.Health);
        command.Parameters.AddWithValue("@physical_attack", weapon.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", weapon.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", weapon.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", weapon.MagicalDefense);
        command.Parameters.AddWithValue("@chemical_attack", weapon.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", weapon.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", weapon.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", weapon.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", weapon.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", weapon.MentalDefense);
        command.Parameters.AddWithValue("@speed", weapon.Speed);
        command.Parameters.AddWithValue("@critical_damage_rate", weapon.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", weapon.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", weapon.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", weapon.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_rate", weapon.PenetrationRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", weapon.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@evasion_rate", weapon.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", weapon.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", weapon.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", weapon.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", weapon.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", weapon.VitalityRegenerationResistanceRate);
        command.Parameters.AddWithValue("@accuracy_rate", weapon.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", weapon.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", weapon.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", weapon.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", weapon.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", weapon.ComboRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", weapon.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", weapon.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", weapon.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", weapon.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", weapon.IgnoreStunRate);
        command.Parameters.AddWithValue("@reflection_rate", weapon.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", weapon.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", weapon.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", weapon.ReflectionResistanceRate);
        command.Parameters.AddWithValue("@mana", weapon.Mana);
        command.Parameters.AddWithValue("@mana_regeneration_rate", weapon.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", weapon.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", weapon.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", weapon.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", weapon.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", weapon.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", weapon.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", weapon.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", weapon.SkillResistanceRate);
    }
}
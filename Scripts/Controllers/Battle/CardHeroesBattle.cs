using UnityEngine;
public class CardHeroesBattle : CardBase
{
    public void SetProperty(CardHeroes cardHeroes)
    {
        // copy thuộc tính từ CardBase
        this.CardName = cardHeroes.name;
        this.Type = cardHeroes.type;
        this.Rare = cardHeroes.rare;
        this.Power = cardHeroes.all_power;
        this.Health = cardHeroes.all_health;
        this.PhysicalAttack = cardHeroes.all_physical_attack;
        this.PhysicalDefense = cardHeroes.all_physical_defense;
        this.MagicalAttack = cardHeroes.all_magical_attack;
        this.MagicalDefense = cardHeroes.all_magical_defense;
        this.ChemicalAttack = cardHeroes.all_chemical_attack;
        this.ChemicalDefense = cardHeroes.all_chemical_defense;
        this.AtomicAttack = cardHeroes.all_atomic_attack;
        this.AtomicDefense = cardHeroes.all_atomic_defense;
        this.MentalAttack = cardHeroes.all_mental_attack;
        this.MentalDefense = cardHeroes.all_mental_defense;
        this.Speed = cardHeroes.all_speed;
        this.CriticalDamageRate = cardHeroes.all_critical_damage_rate;
        this.CriticalRate = cardHeroes.all_critical_rate;
        this.CriticalResistanceRate = cardHeroes.all_critical_resistance_rate;
        this.IgnoreCriticalRate = cardHeroes.all_ignore_critical_rate;
        this.PenetrationRate = cardHeroes.all_penetration_rate;
        this.PenetrationResistanceRate = cardHeroes.all_penetration_resistance_rate;
        this.EvasionRate = cardHeroes.all_evasion_rate;
        this.DamageAbsorptionRate = cardHeroes.all_damage_absorption_rate;
        this.AbsorbedDamageRate = cardHeroes.all_absorbed_damage_rate;
        this.IgnoreDamageAbsorptionRate = cardHeroes.all_ignore_damage_absorption_rate;
        this.VitalityRegenerationRate = cardHeroes.all_vitality_regeneration_rate;
        this.VitalityRegenerationResistanceRate = cardHeroes.all_vitality_regeneration_resistance_rate;
        this.AccuracyRate = cardHeroes.all_accuracy_rate;
        this.LifestealRate = cardHeroes.all_lifesteal_rate;
        this.Mana = cardHeroes.all_mana;
        this.ManaRegenerationRate = cardHeroes.all_mana_regeneration_rate;
        this.ShieldStrength = cardHeroes.all_shield_strength;
        this.Tenacity = cardHeroes.all_tenacity;
        this.ResistanceRate = cardHeroes.all_resistance_rate;
        this.ComboRate = cardHeroes.all_combo_rate;
        this.IgnoreComboRate = cardHeroes.all_ignore_combo_rate;
        this.ComboDamageRate = cardHeroes.all_combo_damage_rate;
        this.ComboResistanceRate = cardHeroes.all_combo_resistance_rate;
        this.StunRate = cardHeroes.all_stun_rate;
        this.IgnoreStunRate = cardHeroes.all_ignore_stun_rate;
        this.ReflectionRate = cardHeroes.all_reflection_rate;
        this.IgnoreReflectionRate = cardHeroes.all_ignore_reflection_rate;
        this.ReflectionDamageRate = cardHeroes.all_reflection_damage_rate;
        this.ReflectionResistanceRate = cardHeroes.all_reflection_resistance_rate;
        this.DamageToDifferentFactionRate = cardHeroes.all_damage_to_different_faction_rate;
        this.ResistanceToDifferentFactionRate = cardHeroes.all_resistance_to_different_faction_rate;
        this.DamageToSameFactionRate = cardHeroes.all_damage_to_same_faction_rate;
        this.ResistanceToSameFactionRate = cardHeroes.all_resistance_to_same_faction_rate;
        this.NormalDamageRate = cardHeroes.all_normal_damage_rate;
        this.NormalResistanceRate = cardHeroes.all_normal_resistance_rate;
        this.SkillDamageRate = cardHeroes.all_skill_damage_rate;
        this.SkillResistanceRate = cardHeroes.all_skill_resistance_rate;
        this.Position = cardHeroes.position;

        this.CurrentHealth = cardHeroes.all_health;
        this.CurrentPhysicalAttack = cardHeroes.all_physical_attack;
        this.CurrentPhysicalDefense = cardHeroes.all_physical_defense;
        this.CurrentMagicalAttack = cardHeroes.all_magical_attack;
        this.CurrentMagicalDefense = cardHeroes.all_magical_defense;
        this.CurrentChemicalAttack = cardHeroes.all_chemical_attack;
        this.CurrentChemicalDefense = cardHeroes.all_chemical_defense;
        this.CurrentAtomicAttack = cardHeroes.all_atomic_attack;
        this.CurrentAtomicDefense = cardHeroes.all_atomic_defense;
        this.CurrentMentalAttack = cardHeroes.all_mental_attack;
        this.CurrentMentalDefense = cardHeroes.all_mental_defense;
        this.CurrentSpeed = cardHeroes.all_speed;
        this.CurrentCriticalDamageRate = cardHeroes.all_critical_damage_rate;
        this.CurrentCriticalRate = cardHeroes.all_critical_rate;
        this.CurrentCriticalResistanceRate = cardHeroes.all_critical_resistance_rate;
        this.CurrentIgnoreCriticalRate = cardHeroes.all_ignore_critical_rate;
        this.CurrentPenetrationRate = cardHeroes.all_penetration_rate;
        this.CurrentPenetrationResistanceRate = cardHeroes.all_penetration_resistance_rate;
        this.CurrentEvasionRate = cardHeroes.all_evasion_rate;
        this.CurrentDamageAbsorptionRate = cardHeroes.all_damage_absorption_rate;
        this.CurrentAbsorbedDamageRate = cardHeroes.all_absorbed_damage_rate;
        this.CurrentIgnoreDamageAbsorptionRate = cardHeroes.all_ignore_damage_absorption_rate;
        this.CurrentVitalityRegenerationRate = cardHeroes.all_vitality_regeneration_rate;
        this.CurrentVitalityRegenerationResistanceRate = cardHeroes.all_vitality_regeneration_resistance_rate;
        this.CurrentAccuracyRate = cardHeroes.all_accuracy_rate;
        this.CurrentLifestealRate = cardHeroes.all_lifesteal_rate;
        this.CurrentMana = cardHeroes.all_mana;
        this.CurrentManaRegenerationRate = cardHeroes.all_mana_regeneration_rate;
        this.CurrentShieldStrength = cardHeroes.all_shield_strength;
        this.CurrentTenacity = cardHeroes.all_tenacity;
        this.CurrentResistanceRate = cardHeroes.all_resistance_rate;
        this.CurrentComboRate = cardHeroes.all_combo_rate;
        this.CurrentIgnoreComboRate = cardHeroes.all_ignore_combo_rate;
        this.CurrentComboDamageRate = cardHeroes.all_combo_damage_rate;
        this.CurrentComboResistanceRate = cardHeroes.all_combo_resistance_rate;
        this.CurrentStunRate = cardHeroes.all_stun_rate;
        this.CurrentIgnoreStunRate = cardHeroes.all_ignore_stun_rate;
        this.CurrentReflectionRate = cardHeroes.all_reflection_rate;
        this.CurrentIgnoreReflectionRate = cardHeroes.all_ignore_reflection_rate;
        this.CurrentReflectionDamageRate = cardHeroes.all_reflection_damage_rate;
        this.CurrentReflectionResistanceRate = cardHeroes.all_reflection_resistance_rate;
        this.CurrentDamageToDifferentFactionRate = cardHeroes.all_damage_to_different_faction_rate;
        this.CurrentResistanceToDifferentFactionRate = cardHeroes.all_resistance_to_different_faction_rate;
        this.CurrentDamageToSameFactionRate = cardHeroes.all_damage_to_same_faction_rate;
        this.CurrentResistanceToSameFactionRate = cardHeroes.all_resistance_to_same_faction_rate;
        this.CurrentNormalDamageRate = cardHeroes.all_normal_damage_rate;
        this.CurrentNormalResistanceRate = cardHeroes.all_normal_resistance_rate;
        this.CurrentSkillDamageRate = cardHeroes.all_skill_damage_rate;
        this.CurrentSkillResistanceRate = cardHeroes.all_skill_resistance_rate;
    }

    // public void Attack(CardBase target)
    // {
    //     // Implement attack logic specific to Admiral cards
    //     // target.TakeDamage(attackPower);
    // }
}
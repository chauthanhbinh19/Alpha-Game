using UnityEngine;
public class CardHeroesBattle : CardBase
{
    public void SetProperty(CardHeroes cardHeroes)
    {
        // copy thuộc tính từ CardBase
        this.CardName = cardHeroes.name;
        this.Type = cardHeroes.type;
        this.Rare = cardHeroes.rare;
        this.Power = cardHeroes.power;
        this.Health = cardHeroes.health;
        this.PhysicalAttack = cardHeroes.physical_attack;
        this.PhysicalDefense = cardHeroes.physical_defense;
        this.MagicalAttack = cardHeroes.magical_attack;
        this.MagicalDefense = cardHeroes.magical_defense;
        this.ChemicalAttack = cardHeroes.chemical_attack;
        this.ChemicalDefense = cardHeroes.chemical_defense;
        this.AtomicAttack = cardHeroes.atomic_attack;
        this.AtomicDefense = cardHeroes.atomic_defense;
        this.MentalAttack = cardHeroes.mental_attack;
        this.MentalDefense = cardHeroes.mental_defense;
        this.Speed = cardHeroes.speed;
        this.CriticalDamageRate = cardHeroes.critical_damage_rate;
        this.CriticalRate = cardHeroes.critical_rate;
        this.CriticalResistanceRate = cardHeroes.critical_resistance_rate;
        this.IgnoreCriticalRate = cardHeroes.ignore_critical_rate;
        this.PenetrationRate = cardHeroes.penetration_rate;
        this.PenetrationResistanceRate = cardHeroes.penetration_resistance_rate;
        this.EvasionRate = cardHeroes.evasion_rate;
        this.DamageAbsorptionRate = cardHeroes.damage_absorption_rate;
        this.AbsorbedDamageRate = cardHeroes.absorbed_damage_rate;
        this.IgnoreDamageAbsorptionRate = cardHeroes.ignore_damage_absorption_rate;
        this.VitalityRegenerationRate = cardHeroes.vitality_regeneration_rate;
        this.VitalityRegenerationResistanceRate = cardHeroes.vitality_regeneration_resistance_rate;
        this.AccuracyRate = cardHeroes.accuracy_rate;
        this.LifestealRate = cardHeroes.lifesteal_rate;
        this.Mana = cardHeroes.mana;
        this.ManaRegenerationRate = cardHeroes.mana_regeneration_rate;
        this.ShieldStrength = cardHeroes.shield_strength;
        this.Tenacity = cardHeroes.tenacity;
        this.ResistanceRate = cardHeroes.resistance_rate;
        this.ComboRate = cardHeroes.combo_rate;
        this.IgnoreComboRate = cardHeroes.ignore_combo_rate;
        this.ComboDamageRate = cardHeroes.combo_damage_rate;
        this.ComboResistanceRate = cardHeroes.combo_resistance_rate;
        this.StunRate = cardHeroes.stun_rate;
        this.IgnoreStunRate = cardHeroes.ignore_stun_rate;
        this.ReflectionRate = cardHeroes.reflection_rate;
        this.IgnoreReflectionRate = cardHeroes.ignore_reflection_rate;
        this.ReflectionDamageRate = cardHeroes.reflection_damage_rate;
        this.ReflectionResistanceRate = cardHeroes.reflection_resistance_rate;
        this.DamageToDifferentFactionRate = cardHeroes.damage_to_different_faction_rate;
        this.ResistanceToDifferentFactionRate = cardHeroes.resistance_to_different_faction_rate;
        this.DamageToSameFactionRate = cardHeroes.damage_to_same_faction_rate;
        this.ResistanceToSameFactionRate = cardHeroes.resistance_to_same_faction_rate;
        this.NormalDamageRate = cardHeroes.normal_damage_rate;
        this.NormalResistanceRate = cardHeroes.normal_resistance_rate;
        this.SkillDamageRate = cardHeroes.skill_damage_rate;
        this.SkillResistanceRate = cardHeroes.skill_resistance_rate;
        this.Position = cardHeroes.position;

        this.CurrentHealth = cardHeroes.health;
        this.CurrentPhysicalAttack = cardHeroes.physical_attack;
        this.CurrentPhysicalDefense = cardHeroes.physical_defense;
        this.CurrentMagicalAttack = cardHeroes.magical_attack;
        this.CurrentMagicalDefense = cardHeroes.magical_defense;
        this.CurrentChemicalAttack = cardHeroes.chemical_attack;
        this.CurrentChemicalDefense = cardHeroes.chemical_defense;
        this.CurrentAtomicAttack = cardHeroes.atomic_attack;
        this.CurrentAtomicDefense = cardHeroes.atomic_defense;
        this.CurrentMentalAttack = cardHeroes.mental_attack;
        this.CurrentMentalDefense = cardHeroes.mental_defense;
        this.CurrentSpeed = cardHeroes.speed;
        this.CurrentCriticalDamageRate = cardHeroes.critical_damage_rate;
        this.CurrentCriticalRate = cardHeroes.critical_rate;
        this.CurrentCriticalResistanceRate = cardHeroes.critical_resistance_rate;
        this.CurrentIgnoreCriticalRate = cardHeroes.ignore_critical_rate;
        this.CurrentPenetrationRate = cardHeroes.penetration_rate;
        this.CurrentPenetrationResistanceRate = cardHeroes.penetration_resistance_rate;
        this.CurrentEvasionRate = cardHeroes.evasion_rate;
        this.CurrentDamageAbsorptionRate = cardHeroes.damage_absorption_rate;
        this.CurrentAbsorbedDamageRate = cardHeroes.absorbed_damage_rate;
        this.CurrentIgnoreDamageAbsorptionRate = cardHeroes.ignore_damage_absorption_rate;
        this.CurrentVitalityRegenerationRate = cardHeroes.vitality_regeneration_rate;
        this.CurrentVitalityRegenerationResistanceRate = cardHeroes.vitality_regeneration_resistance_rate;
        this.CurrentAccuracyRate = cardHeroes.accuracy_rate;
        this.CurrentLifestealRate = cardHeroes.lifesteal_rate;
        this.CurrentMana = cardHeroes.mana;
        this.CurrentManaRegenerationRate = cardHeroes.mana_regeneration_rate;
        this.CurrentShieldStrength = cardHeroes.shield_strength;
        this.CurrentTenacity = cardHeroes.tenacity;
        this.CurrentResistanceRate = cardHeroes.resistance_rate;
        this.CurrentComboRate = cardHeroes.combo_rate;
        this.CurrentIgnoreComboRate = cardHeroes.ignore_combo_rate;
        this.CurrentComboDamageRate = cardHeroes.combo_damage_rate;
        this.CurrentComboResistanceRate = cardHeroes.combo_resistance_rate;
        this.CurrentStunRate = cardHeroes.stun_rate;
        this.CurrentIgnoreStunRate = cardHeroes.ignore_stun_rate;
        this.CurrentReflectionRate = cardHeroes.reflection_rate;
        this.CurrentIgnoreReflectionRate = cardHeroes.ignore_reflection_rate;
        this.CurrentReflectionDamageRate = cardHeroes.reflection_damage_rate;
        this.CurrentReflectionResistanceRate = cardHeroes.reflection_resistance_rate;
        this.CurrentDamageToDifferentFactionRate = cardHeroes.damage_to_different_faction_rate;
        this.CurrentResistanceToDifferentFactionRate = cardHeroes.resistance_to_different_faction_rate;
        this.CurrentDamageToSameFactionRate = cardHeroes.damage_to_same_faction_rate;
        this.CurrentResistanceToSameFactionRate = cardHeroes.resistance_to_same_faction_rate;
        this.CurrentNormalDamageRate = cardHeroes.normal_damage_rate;
        this.CurrentNormalResistanceRate = cardHeroes.normal_resistance_rate;
        this.CurrentSkillDamageRate = cardHeroes.skill_damage_rate;
        this.CurrentSkillResistanceRate = cardHeroes.skill_resistance_rate;
    }

    // public void Attack(CardBase target)
    // {
    //     // Implement attack logic specific to Admiral cards
    //     // target.TakeDamage(attackPower);
    // }
}
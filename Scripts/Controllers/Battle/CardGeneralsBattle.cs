using UnityEngine;
public class CardGeneralsBattle : CardBase
{
    public void SetProperty(CardHeroes cardHeroes)
    {
        // copy thuộc tính từ CardBase
        this.CardName = cardHeroes.name;
        this.Type = cardHeroes.type;
        this.Power = cardHeroes.all_power;
        this.Health = cardHeroes.all_health;
        this.Physical_attack = cardHeroes.all_physical_attack;
        this.Physical_defense = cardHeroes.all_physical_defense;
        this.Magical_attack = cardHeroes.all_magical_attack;
        this.Magical_defense = cardHeroes.all_magical_defense;
        this.Chemical_attack = cardHeroes.all_chemical_attack;
        this.Chemical_defense = cardHeroes.all_chemical_defense;
        this.Atomic_attack = cardHeroes.all_atomic_attack;
        this.Atomic_defense = cardHeroes.all_atomic_defense;
        this.Mental_attack = cardHeroes.all_mental_attack;
        this.Mental_defense = cardHeroes.all_mental_defense;
        this.Speed = cardHeroes.all_speed;
        this.Critical_damage_rate = cardHeroes.all_critical_damage_rate;
        this.Critical_rate = cardHeroes.all_critical_rate;
        this.Penetration_rate = cardHeroes.all_penetration_rate;
        this.Evasion_rate = cardHeroes.all_evasion_rate;
        this.Damage_absorption_rate = cardHeroes.all_damage_absorption_rate;
        this.Vitality_regeneration_rate = cardHeroes.all_vitality_regeneration_rate;
        this.Accuracy_rate = cardHeroes.all_accuracy_rate;
        this.Lifesteal_rate = cardHeroes.all_lifesteal_rate;
        this.Mana = cardHeroes.all_mana;
        this.Mana_regeneration_rate = cardHeroes.all_mana_regeneration_rate;
        this.Shield_strength = cardHeroes.all_shield_strength;
        this.Tenacity = cardHeroes.all_tenacity;
        this.Resistance_rate = cardHeroes.all_resistance_rate;
        this.Combo_rate = cardHeroes.all_combo_rate;
        this.Reflection_rate = cardHeroes.all_reflection_rate;
        this.Damage_to_different_faction_rate = cardHeroes.all_damage_to_different_faction_rate;
        this.Resistance_to_different_faction_rate = cardHeroes.all_resistance_to_different_faction_rate;
        this.Damage_to_same_faction_rate = cardHeroes.all_damage_to_same_faction_rate;
        this.Resistance_to_same_faction_rate = cardHeroes.all_resistance_to_same_faction_rate;
        this.Position = cardHeroes.position;

        this.CurrentHealth = cardHeroes.all_health;
    }
}
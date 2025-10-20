using UnityEngine;
public class CardHero : CardBase
{
    public void Initialize(CardHeroes cardHeroes)
    {
        // copy thuộc tính từ CardBase
        this.CardName = cardHeroes.Name;
        this.Type = cardHeroes.Type;
        this.Rare = cardHeroes.Rare;
        this.Power = cardHeroes.Power;
        this.Health = cardHeroes.Health;
        this.PhysicalAttack = cardHeroes.PhysicalAttack;
        this.PhysicalDefense = cardHeroes.PhysicalDefense;
        this.MagicalAttack = cardHeroes.MagicalAttack;
        this.MagicalDefense = cardHeroes.MagicalDefense;
        this.ChemicalAttack = cardHeroes.ChemicalAttack;
        this.ChemicalDefense = cardHeroes.ChemicalDefense;
        this.AtomicAttack = cardHeroes.AtomicAttack;
        this.AtomicDefense = cardHeroes.AtomicDefense;
        this.MentalAttack = cardHeroes.MentalAttack;
        this.MentalDefense = cardHeroes.MentalDefense;
        this.Speed = cardHeroes.Speed;
        this.CriticalDamageRate = cardHeroes.CriticalDamageRate;
        this.CriticalRate = cardHeroes.CriticalRate;
        this.CriticalResistanceRate = cardHeroes.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardHeroes.IgnoreCriticalRate;
        this.PenetrationRate = cardHeroes.PenetrationRate;
        this.PenetrationResistanceRate = cardHeroes.PenetrationResistanceRate;
        this.EvasionRate = cardHeroes.EvasionRate;
        this.DamageAbsorptionRate = cardHeroes.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardHeroes.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardHeroes.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardHeroes.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardHeroes.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardHeroes.AccuracyRate;
        this.LifestealRate = cardHeroes.LifestealRate;
        this.Mana = cardHeroes.Mana;
        this.ManaRegenerationRate = cardHeroes.ManaRegenerationRate;
        this.ShieldStrength = cardHeroes.ShieldStrength;
        this.Tenacity = cardHeroes.Tenacity;
        this.ResistanceRate = cardHeroes.ResistanceRate;
        this.ComboRate = cardHeroes.ComboRate;
        this.IgnoreComboRate = cardHeroes.IgnoreComboRate;
        this.ComboDamageRate = cardHeroes.ComboDamageRate;
        this.ComboResistanceRate = cardHeroes.ComboResistanceRate;
        this.StunRate = cardHeroes.StunRate;
        this.IgnoreStunRate = cardHeroes.IgnoreStunRate;
        this.ReflectionRate = cardHeroes.ReflectionRate;
        this.IgnoreReflectionRate = cardHeroes.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardHeroes.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardHeroes.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardHeroes.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardHeroes.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardHeroes.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardHeroes.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardHeroes.NormalDamageRate;
        this.NormalResistanceRate = cardHeroes.NormalResistanceRate;
        this.SkillDamageRate = cardHeroes.SkillDamageRate;
        this.SkillResistanceRate = cardHeroes.SkillResistanceRate;
        this.Position = cardHeroes.Position;

        this.CurrentHealth = cardHeroes.Health;
        this.CurrentPhysicalAttack = cardHeroes.PhysicalAttack;
        this.CurrentPhysicalDefense = cardHeroes.PhysicalDefense;
        this.CurrentMagicalAttack = cardHeroes.MagicalAttack;
        this.CurrentMagicalDefense = cardHeroes.MagicalDefense;
        this.CurrentChemicalAttack = cardHeroes.ChemicalAttack;
        this.CurrentChemicalDefense = cardHeroes.ChemicalDefense;
        this.CurrentAtomicAttack = cardHeroes.AtomicAttack;
        this.CurrentAtomicDefense = cardHeroes.AtomicDefense;
        this.CurrentMentalAttack = cardHeroes.MentalAttack;
        this.CurrentMentalDefense = cardHeroes.MentalDefense;
        this.CurrentSpeed = cardHeroes.Speed;
        this.CurrentCriticalDamageRate = cardHeroes.CriticalDamageRate;
        this.CurrentCriticalRate = cardHeroes.CriticalRate;
        this.CurrentCriticalResistanceRate = cardHeroes.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardHeroes.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardHeroes.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardHeroes.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardHeroes.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardHeroes.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardHeroes.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardHeroes.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardHeroes.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardHeroes.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardHeroes.AccuracyRate;
        this.CurrentLifestealRate = cardHeroes.LifestealRate;
        this.CurrentMana = cardHeroes.Mana;
        this.CurrentManaRegenerationRate = cardHeroes.ManaRegenerationRate;
        this.CurrentShieldStrength = cardHeroes.ShieldStrength;
        this.CurrentTenacity = cardHeroes.Tenacity;
        this.CurrentResistanceRate = cardHeroes.ResistanceRate;
        this.CurrentComboRate = cardHeroes.ComboRate;
        this.CurrentIgnoreComboRate = cardHeroes.IgnoreComboRate;
        this.CurrentComboDamageRate = cardHeroes.ComboDamageRate;
        this.CurrentComboResistanceRate = cardHeroes.ComboResistanceRate;
        this.CurrentStunRate = cardHeroes.StunRate;
        this.CurrentIgnoreStunRate = cardHeroes.IgnoreStunRate;
        this.CurrentReflectionRate = cardHeroes.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardHeroes.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardHeroes.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardHeroes.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardHeroes.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardHeroes.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardHeroes.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardHeroes.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardHeroes.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardHeroes.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardHeroes.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardHeroes.SkillResistanceRate;
    }

    // public void Attack(CardBase target)
    // {
    //     // Implement attack logic specific to Admiral cards
    //     // target.TakeDamage(attackPower);
    // }
}
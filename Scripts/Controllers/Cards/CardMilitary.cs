using UnityEngine;
public class CardMilitary : CardBase
{
    public void Initialize(CardMilitaries cardMilitary)
    {
        // copy thuộc tính từ CardBase
        this.CardName = cardMilitary.Name;
        this.Image = cardMilitary.Image;
        this.Type = cardMilitary.Type;
        this.Rare = cardMilitary.Rare;
        this.Power = cardMilitary.Power;
        this.Health = cardMilitary.Health;
        this.PhysicalAttack = cardMilitary.PhysicalAttack;
        this.PhysicalDefense = cardMilitary.PhysicalDefense;
        this.MagicalAttack = cardMilitary.MagicalAttack;
        this.MagicalDefense = cardMilitary.MagicalDefense;
        this.ChemicalAttack = cardMilitary.ChemicalAttack;
        this.ChemicalDefense = cardMilitary.ChemicalDefense;
        this.AtomicAttack = cardMilitary.AtomicAttack;
        this.AtomicDefense = cardMilitary.AtomicDefense;
        this.MentalAttack = cardMilitary.MentalAttack;
        this.MentalDefense = cardMilitary.MentalDefense;
        this.Speed = cardMilitary.Speed;
        this.CriticalDamageRate = cardMilitary.CriticalDamageRate;
        this.CriticalRate = cardMilitary.CriticalRate;
        this.CriticalResistanceRate = cardMilitary.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardMilitary.IgnoreCriticalRate;
        this.PenetrationRate = cardMilitary.PenetrationRate;
        this.PenetrationResistanceRate = cardMilitary.PenetrationResistanceRate;
        this.EvasionRate = cardMilitary.EvasionRate;
        this.DamageAbsorptionRate = cardMilitary.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardMilitary.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardMilitary.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardMilitary.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardMilitary.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardMilitary.AccuracyRate;
        this.LifestealRate = cardMilitary.LifestealRate;
        this.Mana = cardMilitary.Mana;
        this.ManaRegenerationRate = cardMilitary.ManaRegenerationRate;
        this.ShieldStrength = cardMilitary.ShieldStrength;
        this.Tenacity = cardMilitary.Tenacity;
        this.ResistanceRate = cardMilitary.ResistanceRate;
        this.ComboRate = cardMilitary.ComboRate;
        this.IgnoreComboRate = cardMilitary.IgnoreComboRate;
        this.ComboDamageRate = cardMilitary.ComboDamageRate;
        this.ComboResistanceRate = cardMilitary.ComboResistanceRate;
        this.StunRate = cardMilitary.StunRate;
        this.IgnoreStunRate = cardMilitary.IgnoreStunRate;
        this.ReflectionRate = cardMilitary.ReflectionRate;
        this.IgnoreReflectionRate = cardMilitary.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardMilitary.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardMilitary.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardMilitary.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardMilitary.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardMilitary.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardMilitary.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardMilitary.NormalDamageRate;
        this.NormalResistanceRate = cardMilitary.NormalResistanceRate;
        this.SkillDamageRate = cardMilitary.SkillDamageRate;
        this.SkillResistanceRate = cardMilitary.SkillResistanceRate;
        this.Position = cardMilitary.Position;

        this.CurrentHealth = cardMilitary.Health;
        this.CurrentPhysicalAttack = cardMilitary.PhysicalAttack;
        this.CurrentPhysicalDefense = cardMilitary.PhysicalDefense;
        this.CurrentMagicalAttack = cardMilitary.MagicalAttack;
        this.CurrentMagicalDefense = cardMilitary.MagicalDefense;
        this.CurrentChemicalAttack = cardMilitary.ChemicalAttack;
        this.CurrentChemicalDefense = cardMilitary.ChemicalDefense;
        this.CurrentAtomicAttack = cardMilitary.AtomicAttack;
        this.CurrentAtomicDefense = cardMilitary.AtomicDefense;
        this.CurrentMentalAttack = cardMilitary.MentalAttack;
        this.CurrentMentalDefense = cardMilitary.MentalDefense;
        this.CurrentSpeed = cardMilitary.Speed;
        this.CurrentCriticalDamageRate = cardMilitary.CriticalDamageRate;
        this.CurrentCriticalRate = cardMilitary.CriticalRate;
        this.CurrentCriticalResistanceRate = cardMilitary.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardMilitary.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardMilitary.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardMilitary.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardMilitary.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardMilitary.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardMilitary.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardMilitary.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardMilitary.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardMilitary.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardMilitary.AccuracyRate;
        this.CurrentLifestealRate = cardMilitary.LifestealRate;
        this.CurrentMana = cardMilitary.Mana;
        this.CurrentManaRegenerationRate = cardMilitary.ManaRegenerationRate;
        this.CurrentShieldStrength = cardMilitary.ShieldStrength;
        this.CurrentTenacity = cardMilitary.Tenacity;
        this.CurrentResistanceRate = cardMilitary.ResistanceRate;
        this.CurrentComboRate = cardMilitary.ComboRate;
        this.CurrentIgnoreComboRate = cardMilitary.IgnoreComboRate;
        this.CurrentComboDamageRate = cardMilitary.ComboDamageRate;
        this.CurrentComboResistanceRate = cardMilitary.ComboResistanceRate;
        this.CurrentStunRate = cardMilitary.StunRate;
        this.CurrentIgnoreStunRate = cardMilitary.IgnoreStunRate;
        this.CurrentReflectionRate = cardMilitary.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardMilitary.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardMilitary.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardMilitary.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardMilitary.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardMilitary.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardMilitary.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardMilitary.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardMilitary.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardMilitary.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardMilitary.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardMilitary.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardMilitary.Position.Split('-');
        if (parts.Length == 2)
        {
            this.MainPosition = int.Parse(parts[0]);
            this.SubIndex = int.Parse(parts[1]);
        }
    }
}
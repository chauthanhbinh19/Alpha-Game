using UnityEngine;
public class CardGeneral : CardBase
{
    public void Initialize(CardGenerals cardGenerals)
    {
        // copy thuộc tính từ CardBase
        this.CardName = cardGenerals.Name;
        this.Image = cardGenerals.Image;
        this.Type = cardGenerals.Type;
        this.Rare = cardGenerals.Rare;
        this.Power = cardGenerals.Power;
        this.Health = cardGenerals.Health;
        this.PhysicalAttack = cardGenerals.PhysicalAttack;
        this.PhysicalDefense = cardGenerals.PhysicalDefense;
        this.MagicalAttack = cardGenerals.MagicalAttack;
        this.MagicalDefense = cardGenerals.MagicalDefense;
        this.ChemicalAttack = cardGenerals.ChemicalAttack;
        this.ChemicalDefense = cardGenerals.ChemicalDefense;
        this.AtomicAttack = cardGenerals.AtomicAttack;
        this.AtomicDefense = cardGenerals.AtomicDefense;
        this.MentalAttack = cardGenerals.MentalAttack;
        this.MentalDefense = cardGenerals.MentalDefense;
        this.Speed = cardGenerals.Speed;
        this.CriticalDamageRate = cardGenerals.CriticalDamageRate;
        this.CriticalRate = cardGenerals.CriticalRate;
        this.CriticalResistanceRate = cardGenerals.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardGenerals.IgnoreCriticalRate;
        this.PenetrationRate = cardGenerals.PenetrationRate;
        this.PenetrationResistanceRate = cardGenerals.PenetrationResistanceRate;
        this.EvasionRate = cardGenerals.EvasionRate;
        this.DamageAbsorptionRate = cardGenerals.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardGenerals.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardGenerals.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardGenerals.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardGenerals.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardGenerals.AccuracyRate;
        this.LifestealRate = cardGenerals.LifestealRate;
        this.Mana = cardGenerals.Mana;
        this.ManaRegenerationRate = cardGenerals.ManaRegenerationRate;
        this.ShieldStrength = cardGenerals.ShieldStrength;
        this.Tenacity = cardGenerals.Tenacity;
        this.ResistanceRate = cardGenerals.ResistanceRate;
        this.ComboRate = cardGenerals.ComboRate;
        this.IgnoreComboRate = cardGenerals.IgnoreComboRate;
        this.ComboDamageRate = cardGenerals.ComboDamageRate;
        this.ComboResistanceRate = cardGenerals.ComboResistanceRate;
        this.StunRate = cardGenerals.StunRate;
        this.IgnoreStunRate = cardGenerals.IgnoreStunRate;
        this.ReflectionRate = cardGenerals.ReflectionRate;
        this.IgnoreReflectionRate = cardGenerals.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardGenerals.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardGenerals.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardGenerals.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardGenerals.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardGenerals.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardGenerals.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardGenerals.NormalDamageRate;
        this.NormalResistanceRate = cardGenerals.NormalResistanceRate;
        this.SkillDamageRate = cardGenerals.SkillDamageRate;
        this.SkillResistanceRate = cardGenerals.SkillResistanceRate;
        this.Position = cardGenerals.Position;

        this.CurrentHealth = cardGenerals.Health;
        this.CurrentPhysicalAttack = cardGenerals.PhysicalAttack;
        this.CurrentPhysicalDefense = cardGenerals.PhysicalDefense;
        this.CurrentMagicalAttack = cardGenerals.MagicalAttack;
        this.CurrentMagicalDefense = cardGenerals.MagicalDefense;
        this.CurrentChemicalAttack = cardGenerals.ChemicalAttack;
        this.CurrentChemicalDefense = cardGenerals.ChemicalDefense;
        this.CurrentAtomicAttack = cardGenerals.AtomicAttack;
        this.CurrentAtomicDefense = cardGenerals.AtomicDefense;
        this.CurrentMentalAttack = cardGenerals.MentalAttack;
        this.CurrentMentalDefense = cardGenerals.MentalDefense;
        this.CurrentSpeed = cardGenerals.Speed;
        this.CurrentCriticalDamageRate = cardGenerals.CriticalDamageRate;
        this.CurrentCriticalRate = cardGenerals.CriticalRate;
        this.CurrentCriticalResistanceRate = cardGenerals.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardGenerals.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardGenerals.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardGenerals.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardGenerals.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardGenerals.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardGenerals.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardGenerals.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardGenerals.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardGenerals.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardGenerals.AccuracyRate;
        this.CurrentLifestealRate = cardGenerals.LifestealRate;
        this.CurrentMana = cardGenerals.Mana;
        this.CurrentManaRegenerationRate = cardGenerals.ManaRegenerationRate;
        this.CurrentShieldStrength = cardGenerals.ShieldStrength;
        this.CurrentTenacity = cardGenerals.Tenacity;
        this.CurrentResistanceRate = cardGenerals.ResistanceRate;
        this.CurrentComboRate = cardGenerals.ComboRate;
        this.CurrentIgnoreComboRate = cardGenerals.IgnoreComboRate;
        this.CurrentComboDamageRate = cardGenerals.ComboDamageRate;
        this.CurrentComboResistanceRate = cardGenerals.ComboResistanceRate;
        this.CurrentStunRate = cardGenerals.StunRate;
        this.CurrentIgnoreStunRate = cardGenerals.IgnoreStunRate;
        this.CurrentReflectionRate = cardGenerals.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardGenerals.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardGenerals.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardGenerals.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardGenerals.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardGenerals.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardGenerals.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardGenerals.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardGenerals.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardGenerals.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardGenerals.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardGenerals.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardGenerals.Position.Split('-');
        if (parts.Length == 2)
        {
            this.MainPosition = int.Parse(parts[0]);
            this.SubIndex = int.Parse(parts[1]);
        }
    }
}
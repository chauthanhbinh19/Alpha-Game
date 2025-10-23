using UnityEngine;
public class CardAdmiral : CardBase
{
    public void Initialize(CardAdmirals cardAdmirals)
    {
        // copy thuộc tính từ CardBase
        this.CardName = cardAdmirals.Name;
        this.Image = cardAdmirals.Image;
        this.Type = cardAdmirals.Type;
        this.Rare = cardAdmirals.Rare;
        this.Power = cardAdmirals.Power;
        this.Health = cardAdmirals.Health;
        this.PhysicalAttack = cardAdmirals.PhysicalAttack;
        this.PhysicalDefense = cardAdmirals.PhysicalDefense;
        this.MagicalAttack = cardAdmirals.MagicalAttack;
        this.MagicalDefense = cardAdmirals.MagicalDefense;
        this.ChemicalAttack = cardAdmirals.ChemicalAttack;
        this.ChemicalDefense = cardAdmirals.ChemicalDefense;
        this.AtomicAttack = cardAdmirals.AtomicAttack;
        this.AtomicDefense = cardAdmirals.AtomicDefense;
        this.MentalAttack = cardAdmirals.MentalAttack;
        this.MentalDefense = cardAdmirals.MentalDefense;
        this.Speed = cardAdmirals.Speed;
        this.CriticalDamageRate = cardAdmirals.CriticalDamageRate;
        this.CriticalRate = cardAdmirals.CriticalRate;
        this.CriticalResistanceRate = cardAdmirals.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardAdmirals.IgnoreCriticalRate;
        this.PenetrationRate = cardAdmirals.PenetrationRate;
        this.PenetrationResistanceRate = cardAdmirals.PenetrationResistanceRate;
        this.EvasionRate = cardAdmirals.EvasionRate;
        this.DamageAbsorptionRate = cardAdmirals.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardAdmirals.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardAdmirals.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardAdmirals.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardAdmirals.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardAdmirals.AccuracyRate;
        this.LifestealRate = cardAdmirals.LifestealRate;
        this.Mana = cardAdmirals.Mana;
        this.ManaRegenerationRate = cardAdmirals.ManaRegenerationRate;
        this.ShieldStrength = cardAdmirals.ShieldStrength;
        this.Tenacity = cardAdmirals.Tenacity;
        this.ResistanceRate = cardAdmirals.ResistanceRate;
        this.ComboRate = cardAdmirals.ComboRate;
        this.IgnoreComboRate = cardAdmirals.IgnoreComboRate;
        this.ComboDamageRate = cardAdmirals.ComboDamageRate;
        this.ComboResistanceRate = cardAdmirals.ComboResistanceRate;
        this.StunRate = cardAdmirals.StunRate;
        this.IgnoreStunRate = cardAdmirals.IgnoreStunRate;
        this.ReflectionRate = cardAdmirals.ReflectionRate;
        this.IgnoreReflectionRate = cardAdmirals.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardAdmirals.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardAdmirals.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardAdmirals.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardAdmirals.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardAdmirals.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardAdmirals.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardAdmirals.NormalDamageRate;
        this.NormalResistanceRate = cardAdmirals.NormalResistanceRate;
        this.SkillDamageRate = cardAdmirals.SkillDamageRate;
        this.SkillResistanceRate = cardAdmirals.SkillResistanceRate;
        this.Position = cardAdmirals.Position;

        this.CurrentHealth = cardAdmirals.Health;
        this.CurrentPhysicalAttack = cardAdmirals.PhysicalAttack;
        this.CurrentPhysicalDefense = cardAdmirals.PhysicalDefense;
        this.CurrentMagicalAttack = cardAdmirals.MagicalAttack;
        this.CurrentMagicalDefense = cardAdmirals.MagicalDefense;
        this.CurrentChemicalAttack = cardAdmirals.ChemicalAttack;
        this.CurrentChemicalDefense = cardAdmirals.ChemicalDefense;
        this.CurrentAtomicAttack = cardAdmirals.AtomicAttack;
        this.CurrentAtomicDefense = cardAdmirals.AtomicDefense;
        this.CurrentMentalAttack = cardAdmirals.MentalAttack;
        this.CurrentMentalDefense = cardAdmirals.MentalDefense;
        this.CurrentSpeed = cardAdmirals.Speed;
        this.CurrentCriticalDamageRate = cardAdmirals.CriticalDamageRate;
        this.CurrentCriticalRate = cardAdmirals.CriticalRate;
        this.CurrentCriticalResistanceRate = cardAdmirals.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardAdmirals.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardAdmirals.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardAdmirals.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardAdmirals.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardAdmirals.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardAdmirals.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardAdmirals.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardAdmirals.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardAdmirals.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardAdmirals.AccuracyRate;
        this.CurrentLifestealRate = cardAdmirals.LifestealRate;
        this.CurrentMana = cardAdmirals.Mana;
        this.CurrentManaRegenerationRate = cardAdmirals.ManaRegenerationRate;
        this.CurrentShieldStrength = cardAdmirals.ShieldStrength;
        this.CurrentTenacity = cardAdmirals.Tenacity;
        this.CurrentResistanceRate = cardAdmirals.ResistanceRate;
        this.CurrentComboRate = cardAdmirals.ComboRate;
        this.CurrentIgnoreComboRate = cardAdmirals.IgnoreComboRate;
        this.CurrentComboDamageRate = cardAdmirals.ComboDamageRate;
        this.CurrentComboResistanceRate = cardAdmirals.ComboResistanceRate;
        this.CurrentStunRate = cardAdmirals.StunRate;
        this.CurrentIgnoreStunRate = cardAdmirals.IgnoreStunRate;
        this.CurrentReflectionRate = cardAdmirals.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardAdmirals.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardAdmirals.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardAdmirals.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardAdmirals.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardAdmirals.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardAdmirals.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardAdmirals.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardAdmirals.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardAdmirals.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardAdmirals.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardAdmirals.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardAdmirals.Position.Split('-');
        if (parts.Length == 2)
        {
            this.MainPosition = int.Parse(parts[0]);
            this.SubIndex = int.Parse(parts[1]);
        }
    }
}
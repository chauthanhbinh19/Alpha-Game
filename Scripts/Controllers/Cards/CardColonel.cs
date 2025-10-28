using System;
using UnityEngine;
public class CardColonel : CardBase
{
    public void Initialize(CardColonels cardColonels)
    {
        // copy thuộc tính từ CardBase
        this.Id = cardColonels.Id;
        this.CardName = cardColonels.Name;
        this.Image = cardColonels.Image;
        this.Type = cardColonels.Type;
        this.Rare = cardColonels.Rare;
        this.Power = cardColonels.Power;
        this.Health = cardColonels.Health;
        this.PhysicalAttack = cardColonels.PhysicalAttack;
        this.PhysicalDefense = cardColonels.PhysicalDefense;
        this.MagicalAttack = cardColonels.MagicalAttack;
        this.MagicalDefense = cardColonels.MagicalDefense;
        this.ChemicalAttack = cardColonels.ChemicalAttack;
        this.ChemicalDefense = cardColonels.ChemicalDefense;
        this.AtomicAttack = cardColonels.AtomicAttack;
        this.AtomicDefense = cardColonels.AtomicDefense;
        this.MentalAttack = cardColonels.MentalAttack;
        this.MentalDefense = cardColonels.MentalDefense;
        this.Speed = cardColonels.Speed;
        this.CriticalDamageRate = cardColonels.CriticalDamageRate;
        this.CriticalRate = cardColonels.CriticalRate;
        this.CriticalResistanceRate = cardColonels.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardColonels.IgnoreCriticalRate;
        this.PenetrationRate = cardColonels.PenetrationRate;
        this.PenetrationResistanceRate = cardColonels.PenetrationResistanceRate;
        this.EvasionRate = cardColonels.EvasionRate;
        this.DamageAbsorptionRate = cardColonels.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardColonels.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardColonels.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardColonels.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardColonels.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardColonels.AccuracyRate;
        this.LifestealRate = cardColonels.LifestealRate;
        this.Mana = cardColonels.Mana;
        this.ManaRegenerationRate = cardColonels.ManaRegenerationRate;
        this.ShieldStrength = cardColonels.ShieldStrength;
        this.Tenacity = cardColonels.Tenacity;
        this.ResistanceRate = cardColonels.ResistanceRate;
        this.ComboRate = cardColonels.ComboRate;
        this.IgnoreComboRate = cardColonels.IgnoreComboRate;
        this.ComboDamageRate = cardColonels.ComboDamageRate;
        this.ComboResistanceRate = cardColonels.ComboResistanceRate;
        this.StunRate = cardColonels.StunRate;
        this.IgnoreStunRate = cardColonels.IgnoreStunRate;
        this.ReflectionRate = cardColonels.ReflectionRate;
        this.IgnoreReflectionRate = cardColonels.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardColonels.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardColonels.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardColonels.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardColonels.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardColonels.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardColonels.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardColonels.NormalDamageRate;
        this.NormalResistanceRate = cardColonels.NormalResistanceRate;
        this.SkillDamageRate = cardColonels.SkillDamageRate;
        this.SkillResistanceRate = cardColonels.SkillResistanceRate;
        this.Position = cardColonels.Position;
        this.IsAlive = true;
        this.Skills = cardColonels.Skills;

        this.CurrentHealth = cardColonels.Health;
        this.CurrentPhysicalAttack = cardColonels.PhysicalAttack;
        this.CurrentPhysicalDefense = cardColonels.PhysicalDefense;
        this.CurrentMagicalAttack = cardColonels.MagicalAttack;
        this.CurrentMagicalDefense = cardColonels.MagicalDefense;
        this.CurrentChemicalAttack = cardColonels.ChemicalAttack;
        this.CurrentChemicalDefense = cardColonels.ChemicalDefense;
        this.CurrentAtomicAttack = cardColonels.AtomicAttack;
        this.CurrentAtomicDefense = cardColonels.AtomicDefense;
        this.CurrentMentalAttack = cardColonels.MentalAttack;
        this.CurrentMentalDefense = cardColonels.MentalDefense;
        this.CurrentSpeed = cardColonels.Speed;
        this.CurrentCriticalDamageRate = cardColonels.CriticalDamageRate;
        this.CurrentCriticalRate = cardColonels.CriticalRate;
        this.CurrentCriticalResistanceRate = cardColonels.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardColonels.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardColonels.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardColonels.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardColonels.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardColonels.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardColonels.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardColonels.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardColonels.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardColonels.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardColonels.AccuracyRate;
        this.CurrentLifestealRate = cardColonels.LifestealRate;
        this.CurrentMana = cardColonels.Mana;
        this.CurrentManaRegenerationRate = cardColonels.ManaRegenerationRate;
        this.CurrentShieldStrength = cardColonels.ShieldStrength;
        this.CurrentTenacity = cardColonels.Tenacity;
        this.CurrentResistanceRate = cardColonels.ResistanceRate;
        this.CurrentComboRate = cardColonels.ComboRate;
        this.CurrentIgnoreComboRate = cardColonels.IgnoreComboRate;
        this.CurrentComboDamageRate = cardColonels.ComboDamageRate;
        this.CurrentComboResistanceRate = cardColonels.ComboResistanceRate;
        this.CurrentStunRate = cardColonels.StunRate;
        this.CurrentIgnoreStunRate = cardColonels.IgnoreStunRate;
        this.CurrentReflectionRate = cardColonels.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardColonels.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardColonels.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardColonels.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardColonels.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardColonels.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardColonels.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardColonels.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardColonels.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardColonels.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardColonels.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardColonels.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardColonels.Position.Split('-');
        if (parts.Length == 2)
        {
            this.MainPosition = int.Parse(parts[0]);
            this.SubIndex = int.Parse(parts[1]);
        }
    }
    public void ApplyPenalty(float penaltyPercent)
    {
        // Hệ số nhân giảm giá trị. Ví dụ: 10% penalty -> multiplier = 1 - (10 / 100) = 0.9
        float multiplier = 1f - (penaltyPercent / 100f);

        // Đảm bảo multiplier không âm (trừ khi bạn cho phép chỉ số tăng khi bị phạt)
        if (multiplier < 0) multiplier = 0; 

        this.CurrentHealth = Math.Max(0, this.CurrentHealth * multiplier);
        this.CurrentMana = Mathf.Max(0, this.CurrentMana * multiplier);
        
        // Chỉ số Tấn công và Phòng thủ
        this.CurrentPhysicalAttack *= multiplier;
        this.CurrentPhysicalDefense *= multiplier;
        this.CurrentMagicalAttack *= multiplier;
        this.CurrentMagicalDefense *= multiplier;
        this.CurrentChemicalAttack *= multiplier;
        this.CurrentChemicalDefense *= multiplier;
        this.CurrentAtomicAttack *= multiplier;
        this.CurrentAtomicDefense *= multiplier;
        this.CurrentMentalAttack *= multiplier;
        this.CurrentMentalDefense *= multiplier;
        
        // Chỉ số Tỷ lệ (Rates) và các chỉ số khác
        this.CurrentSpeed *= multiplier;
        this.CurrentCriticalDamageRate *= multiplier;
        this.CurrentCriticalRate *= multiplier;
        this.CurrentCriticalResistanceRate *= multiplier;
        this.CurrentIgnoreCriticalRate *= multiplier;
        this.CurrentPenetrationRate *= multiplier;
        this.CurrentPenetrationResistanceRate *= multiplier;
        this.CurrentEvasionRate *= multiplier;
        this.CurrentDamageAbsorptionRate *= multiplier;
        this.CurrentAbsorbedDamageRate *= multiplier;
        this.CurrentIgnoreDamageAbsorptionRate *= multiplier;
        this.CurrentVitalityRegenerationRate *= multiplier;
        this.CurrentVitalityRegenerationResistanceRate *= multiplier;
        this.CurrentAccuracyRate *= multiplier;
        this.CurrentLifestealRate *= multiplier;
        this.CurrentManaRegenerationRate *= multiplier;
        this.CurrentShieldStrength *= multiplier;
        this.CurrentTenacity *= multiplier;
        this.CurrentResistanceRate *= multiplier;
        this.CurrentComboRate *= multiplier;
        this.CurrentIgnoreComboRate *= multiplier;
        this.CurrentComboDamageRate *= multiplier;
        this.CurrentComboResistanceRate *= multiplier;
        this.CurrentStunRate *= multiplier;
        this.CurrentIgnoreStunRate *= multiplier;
        this.CurrentReflectionRate *= multiplier;
        this.CurrentIgnoreReflectionRate *= multiplier;
        this.CurrentReflectionDamageRate *= multiplier;
        this.CurrentReflectionResistanceRate *= multiplier;
        this.CurrentDamageToDifferentFactionRate *= multiplier;
        this.CurrentResistanceToDifferentFactionRate *= multiplier;
        this.CurrentDamageToSameFactionRate *= multiplier;
        this.CurrentResistanceToSameFactionRate *= multiplier;
        this.CurrentNormalDamageRate *= multiplier;
        this.CurrentNormalResistanceRate *= multiplier;
        this.CurrentSkillDamageRate *= multiplier;
        this.CurrentSkillResistanceRate *= multiplier;
        
        // Tùy chọn: Gọi hàm tính lại chỉ số Power nếu cần
        // RecalculatePower();
    }
}
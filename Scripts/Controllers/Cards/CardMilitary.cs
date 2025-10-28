using System;
using UnityEngine;
public class CardMilitary : CardBase
{
    public void Initialize(CardMilitaries cardMilitary)
    {
        // copy thuộc tính từ CardBase
        this.Id = cardMilitary.Id;
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
        this.IsAlive = true;
        this.Skills = cardMilitary.Skills;

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
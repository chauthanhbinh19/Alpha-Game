using System;
using UnityEngine;
public class CardCaptain : CardBase
{
    public void Initialize(CardCaptains cardCaptains)
    {
        // copy thuộc tính từ CardBase
        this.Id = cardCaptains.Id;
        this.CardName = cardCaptains.Name;
        this.Image = cardCaptains.Image;
        this.Type = cardCaptains.Type;
        this.Rare = cardCaptains.Rare;
        this.Power = cardCaptains.Power;
        this.Health = cardCaptains.Health;
        this.PhysicalAttack = cardCaptains.PhysicalAttack;
        this.PhysicalDefense = cardCaptains.PhysicalDefense;
        this.MagicalAttack = cardCaptains.MagicalAttack;
        this.MagicalDefense = cardCaptains.MagicalDefense;
        this.ChemicalAttack = cardCaptains.ChemicalAttack;
        this.ChemicalDefense = cardCaptains.ChemicalDefense;
        this.AtomicAttack = cardCaptains.AtomicAttack;
        this.AtomicDefense = cardCaptains.AtomicDefense;
        this.MentalAttack = cardCaptains.MentalAttack;
        this.MentalDefense = cardCaptains.MentalDefense;
        this.Speed = cardCaptains.Speed;
        this.CriticalDamageRate = cardCaptains.CriticalDamageRate;
        this.CriticalRate = cardCaptains.CriticalRate;
        this.CriticalResistanceRate = cardCaptains.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardCaptains.IgnoreCriticalRate;
        this.PenetrationRate = cardCaptains.PenetrationRate;
        this.PenetrationResistanceRate = cardCaptains.PenetrationResistanceRate;
        this.EvasionRate = cardCaptains.EvasionRate;
        this.DamageAbsorptionRate = cardCaptains.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardCaptains.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardCaptains.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardCaptains.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardCaptains.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardCaptains.AccuracyRate;
        this.LifestealRate = cardCaptains.LifestealRate;
        this.Mana = cardCaptains.Mana;
        this.ManaRegenerationRate = cardCaptains.ManaRegenerationRate;
        this.ShieldStrength = cardCaptains.ShieldStrength;
        this.Tenacity = cardCaptains.Tenacity;
        this.ResistanceRate = cardCaptains.ResistanceRate;
        this.ComboRate = cardCaptains.ComboRate;
        this.IgnoreComboRate = cardCaptains.IgnoreComboRate;
        this.ComboDamageRate = cardCaptains.ComboDamageRate;
        this.ComboResistanceRate = cardCaptains.ComboResistanceRate;
        this.StunRate = cardCaptains.StunRate;
        this.IgnoreStunRate = cardCaptains.IgnoreStunRate;
        this.ReflectionRate = cardCaptains.ReflectionRate;
        this.IgnoreReflectionRate = cardCaptains.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardCaptains.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardCaptains.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardCaptains.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardCaptains.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardCaptains.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardCaptains.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardCaptains.NormalDamageRate;
        this.NormalResistanceRate = cardCaptains.NormalResistanceRate;
        this.SkillDamageRate = cardCaptains.SkillDamageRate;
        this.SkillResistanceRate = cardCaptains.SkillResistanceRate;
        this.Position = cardCaptains.Position;
        this.IsAlive = true;
        this.Skills = cardCaptains.Skills;

        this.CurrentHealth = cardCaptains.Health;
        this.CurrentPhysicalAttack = cardCaptains.PhysicalAttack;
        this.CurrentPhysicalDefense = cardCaptains.PhysicalDefense;
        this.CurrentMagicalAttack = cardCaptains.MagicalAttack;
        this.CurrentMagicalDefense = cardCaptains.MagicalDefense;
        this.CurrentChemicalAttack = cardCaptains.ChemicalAttack;
        this.CurrentChemicalDefense = cardCaptains.ChemicalDefense;
        this.CurrentAtomicAttack = cardCaptains.AtomicAttack;
        this.CurrentAtomicDefense = cardCaptains.AtomicDefense;
        this.CurrentMentalAttack = cardCaptains.MentalAttack;
        this.CurrentMentalDefense = cardCaptains.MentalDefense;
        this.CurrentSpeed = cardCaptains.Speed;
        this.CurrentCriticalDamageRate = cardCaptains.CriticalDamageRate;
        this.CurrentCriticalRate = cardCaptains.CriticalRate;
        this.CurrentCriticalResistanceRate = cardCaptains.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardCaptains.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardCaptains.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardCaptains.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardCaptains.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardCaptains.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardCaptains.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardCaptains.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardCaptains.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardCaptains.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardCaptains.AccuracyRate;
        this.CurrentLifestealRate = cardCaptains.LifestealRate;
        this.CurrentMana = cardCaptains.Mana;
        this.CurrentManaRegenerationRate = cardCaptains.ManaRegenerationRate;
        this.CurrentShieldStrength = cardCaptains.ShieldStrength;
        this.CurrentTenacity = cardCaptains.Tenacity;
        this.CurrentResistanceRate = cardCaptains.ResistanceRate;
        this.CurrentComboRate = cardCaptains.ComboRate;
        this.CurrentIgnoreComboRate = cardCaptains.IgnoreComboRate;
        this.CurrentComboDamageRate = cardCaptains.ComboDamageRate;
        this.CurrentComboResistanceRate = cardCaptains.ComboResistanceRate;
        this.CurrentStunRate = cardCaptains.StunRate;
        this.CurrentIgnoreStunRate = cardCaptains.IgnoreStunRate;
        this.CurrentReflectionRate = cardCaptains.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardCaptains.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardCaptains.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardCaptains.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardCaptains.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardCaptains.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardCaptains.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardCaptains.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardCaptains.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardCaptains.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardCaptains.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardCaptains.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardCaptains.Position.Split('-');
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
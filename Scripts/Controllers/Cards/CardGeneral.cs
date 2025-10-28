using System;
using UnityEngine;
public class CardGeneral : CardBase
{
    public void Initialize(CardGenerals cardGenerals)
    {
        // copy thuộc tính từ CardBase
        this.Id = cardGenerals.Id;
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
        this.IsAlive = true;
        this.Skills = cardGenerals.Skills;

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
using System;
using UnityEngine;
public class CardMonster : CardBase
{
    public void Initialize(CardMonsters cardMonsters)
    {
        // copy thuộc tính từ CardBase
        this.Id = cardMonsters.Id;
        this.Name = cardMonsters.Name;
        this.Image = cardMonsters.Image;
        this.Type = cardMonsters.Type;
        this.Rare = cardMonsters.Rare;
        this.Power = cardMonsters.Power;
        this.Health = cardMonsters.Health;
        this.PhysicalAttack = cardMonsters.PhysicalAttack;
        this.PhysicalDefense = cardMonsters.PhysicalDefense;
        this.MagicalAttack = cardMonsters.MagicalAttack;
        this.MagicalDefense = cardMonsters.MagicalDefense;
        this.ChemicalAttack = cardMonsters.ChemicalAttack;
        this.ChemicalDefense = cardMonsters.ChemicalDefense;
        this.AtomicAttack = cardMonsters.AtomicAttack;
        this.AtomicDefense = cardMonsters.AtomicDefense;
        this.MentalAttack = cardMonsters.MentalAttack;
        this.MentalDefense = cardMonsters.MentalDefense;
        this.Speed = cardMonsters.Speed;
        this.CriticalDamageRate = cardMonsters.CriticalDamageRate;
        this.CriticalRate = cardMonsters.CriticalRate;
        this.CriticalResistanceRate = cardMonsters.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardMonsters.IgnoreCriticalRate;
        this.PenetrationRate = cardMonsters.PenetrationRate;
        this.PenetrationResistanceRate = cardMonsters.PenetrationResistanceRate;
        this.EvasionRate = cardMonsters.EvasionRate;
        this.DamageAbsorptionRate = cardMonsters.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardMonsters.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardMonsters.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardMonsters.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardMonsters.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardMonsters.AccuracyRate;
        this.LifestealRate = cardMonsters.LifestealRate;
        this.Mana = cardMonsters.Mana;
        this.ManaRegenerationRate = cardMonsters.ManaRegenerationRate;
        this.ShieldStrength = cardMonsters.ShieldStrength;
        this.Tenacity = cardMonsters.Tenacity;
        this.ResistanceRate = cardMonsters.ResistanceRate;
        this.ComboRate = cardMonsters.ComboRate;
        this.IgnoreComboRate = cardMonsters.IgnoreComboRate;
        this.ComboDamageRate = cardMonsters.ComboDamageRate;
        this.ComboResistanceRate = cardMonsters.ComboResistanceRate;
        this.StunRate = cardMonsters.StunRate;
        this.IgnoreStunRate = cardMonsters.IgnoreStunRate;
        this.ReflectionRate = cardMonsters.ReflectionRate;
        this.IgnoreReflectionRate = cardMonsters.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardMonsters.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardMonsters.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardMonsters.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardMonsters.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardMonsters.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardMonsters.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardMonsters.NormalDamageRate;
        this.NormalResistanceRate = cardMonsters.NormalResistanceRate;
        this.SkillDamageRate = cardMonsters.SkillDamageRate;
        this.SkillResistanceRate = cardMonsters.SkillResistanceRate;
        this.Position = cardMonsters.Position;
        this.IsAlive = true;
        this.Skills = cardMonsters.Skills;

        this.CurrentHealth = cardMonsters.Health;
        this.CurrentPhysicalAttack = cardMonsters.PhysicalAttack;
        this.CurrentPhysicalDefense = cardMonsters.PhysicalDefense;
        this.CurrentMagicalAttack = cardMonsters.MagicalAttack;
        this.CurrentMagicalDefense = cardMonsters.MagicalDefense;
        this.CurrentChemicalAttack = cardMonsters.ChemicalAttack;
        this.CurrentChemicalDefense = cardMonsters.ChemicalDefense;
        this.CurrentAtomicAttack = cardMonsters.AtomicAttack;
        this.CurrentAtomicDefense = cardMonsters.AtomicDefense;
        this.CurrentMentalAttack = cardMonsters.MentalAttack;
        this.CurrentMentalDefense = cardMonsters.MentalDefense;
        this.CurrentSpeed = cardMonsters.Speed;
        this.CurrentCriticalDamageRate = cardMonsters.CriticalDamageRate;
        this.CurrentCriticalRate = cardMonsters.CriticalRate;
        this.CurrentCriticalResistanceRate = cardMonsters.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardMonsters.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardMonsters.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardMonsters.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardMonsters.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardMonsters.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardMonsters.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardMonsters.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardMonsters.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardMonsters.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardMonsters.AccuracyRate;
        this.CurrentLifestealRate = cardMonsters.LifestealRate;
        this.CurrentMana = cardMonsters.Mana;
        this.CurrentManaRegenerationRate = cardMonsters.ManaRegenerationRate;
        this.CurrentShieldStrength = cardMonsters.ShieldStrength;
        this.CurrentTenacity = cardMonsters.Tenacity;
        this.CurrentResistanceRate = cardMonsters.ResistanceRate;
        this.CurrentComboRate = cardMonsters.ComboRate;
        this.CurrentIgnoreComboRate = cardMonsters.IgnoreComboRate;
        this.CurrentComboDamageRate = cardMonsters.ComboDamageRate;
        this.CurrentComboResistanceRate = cardMonsters.ComboResistanceRate;
        this.CurrentStunRate = cardMonsters.StunRate;
        this.CurrentIgnoreStunRate = cardMonsters.IgnoreStunRate;
        this.CurrentReflectionRate = cardMonsters.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardMonsters.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardMonsters.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardMonsters.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardMonsters.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardMonsters.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardMonsters.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardMonsters.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardMonsters.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardMonsters.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardMonsters.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardMonsters.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardMonsters.Position.Split('-');
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
        this.CurrentMana *= multiplier;
        
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
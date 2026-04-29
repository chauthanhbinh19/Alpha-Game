using System;
using UnityEngine;
public class CardCaptain : CardBase
{
    public override void Initialize(object data)
    {
        var cardCaptain = data as CardCaptains;

        if (cardCaptain == null)
        {
            Debug.LogError("Sai type CardCaptains");
            return;
        }
        // copy thuộc tính từ CardBase
        this.Id = cardCaptain.Id;
        this.Name = cardCaptain.Name;
        this.Image = cardCaptain.Image;
        this.Type = cardCaptain.Type;
        this.Rare = cardCaptain.Rare;
        this.Power = cardCaptain.Power;
        this.Health = cardCaptain.Health;
        this.PhysicalAttack = cardCaptain.PhysicalAttack;
        this.PhysicalDefense = cardCaptain.PhysicalDefense;
        this.MagicalAttack = cardCaptain.MagicalAttack;
        this.MagicalDefense = cardCaptain.MagicalDefense;
        this.ChemicalAttack = cardCaptain.ChemicalAttack;
        this.ChemicalDefense = cardCaptain.ChemicalDefense;
        this.AtomicAttack = cardCaptain.AtomicAttack;
        this.AtomicDefense = cardCaptain.AtomicDefense;
        this.MentalAttack = cardCaptain.MentalAttack;
        this.MentalDefense = cardCaptain.MentalDefense;
        this.Speed = cardCaptain.Speed;
        this.CriticalDamageRate = cardCaptain.CriticalDamageRate;
        this.CriticalRate = cardCaptain.CriticalRate;
        this.CriticalResistanceRate = cardCaptain.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardCaptain.IgnoreCriticalRate;
        this.PenetrationRate = cardCaptain.PenetrationRate;
        this.PenetrationResistanceRate = cardCaptain.PenetrationResistanceRate;
        this.EvasionRate = cardCaptain.EvasionRate;
        this.DamageAbsorptionRate = cardCaptain.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardCaptain.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardCaptain.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardCaptain.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardCaptain.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardCaptain.AccuracyRate;
        this.LifestealRate = cardCaptain.LifestealRate;
        this.Mana = cardCaptain.Mana;
        this.ManaRegenerationRate = cardCaptain.ManaRegenerationRate;
        this.ShieldStrength = cardCaptain.ShieldStrength;
        this.Tenacity = cardCaptain.Tenacity;
        this.ResistanceRate = cardCaptain.ResistanceRate;
        this.ComboRate = cardCaptain.ComboRate;
        this.IgnoreComboRate = cardCaptain.IgnoreComboRate;
        this.ComboDamageRate = cardCaptain.ComboDamageRate;
        this.ComboResistanceRate = cardCaptain.ComboResistanceRate;
        this.StunRate = cardCaptain.StunRate;
        this.IgnoreStunRate = cardCaptain.IgnoreStunRate;
        this.ReflectionRate = cardCaptain.ReflectionRate;
        this.IgnoreReflectionRate = cardCaptain.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardCaptain.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardCaptain.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardCaptain.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardCaptain.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardCaptain.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardCaptain.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardCaptain.NormalDamageRate;
        this.NormalResistanceRate = cardCaptain.NormalResistanceRate;
        this.SkillDamageRate = cardCaptain.SkillDamageRate;
        this.SkillResistanceRate = cardCaptain.SkillResistanceRate;
        this.Position = cardCaptain.Position;
        this.IsAlive = true;
        this.Skills = cardCaptain.Skills;
        this.CardType = CardType.CardCaptain;

        this.CurrentHealth = cardCaptain.Health;
        this.CurrentPhysicalAttack = cardCaptain.PhysicalAttack;
        this.CurrentPhysicalDefense = cardCaptain.PhysicalDefense;
        this.CurrentMagicalAttack = cardCaptain.MagicalAttack;
        this.CurrentMagicalDefense = cardCaptain.MagicalDefense;
        this.CurrentChemicalAttack = cardCaptain.ChemicalAttack;
        this.CurrentChemicalDefense = cardCaptain.ChemicalDefense;
        this.CurrentAtomicAttack = cardCaptain.AtomicAttack;
        this.CurrentAtomicDefense = cardCaptain.AtomicDefense;
        this.CurrentMentalAttack = cardCaptain.MentalAttack;
        this.CurrentMentalDefense = cardCaptain.MentalDefense;
        this.CurrentSpeed = cardCaptain.Speed;
        this.CurrentCriticalDamageRate = cardCaptain.CriticalDamageRate;
        this.CurrentCriticalRate = cardCaptain.CriticalRate;
        this.CurrentCriticalResistanceRate = cardCaptain.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardCaptain.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardCaptain.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardCaptain.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardCaptain.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardCaptain.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardCaptain.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardCaptain.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardCaptain.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardCaptain.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardCaptain.AccuracyRate;
        this.CurrentLifestealRate = cardCaptain.LifestealRate;
        this.CurrentMana = cardCaptain.Mana;
        this.CurrentManaRegenerationRate = cardCaptain.ManaRegenerationRate;
        this.CurrentShieldStrength = cardCaptain.ShieldStrength;
        this.CurrentTenacity = cardCaptain.Tenacity;
        this.CurrentResistanceRate = cardCaptain.ResistanceRate;
        this.CurrentComboRate = cardCaptain.ComboRate;
        this.CurrentIgnoreComboRate = cardCaptain.IgnoreComboRate;
        this.CurrentComboDamageRate = cardCaptain.ComboDamageRate;
        this.CurrentComboResistanceRate = cardCaptain.ComboResistanceRate;
        this.CurrentStunRate = cardCaptain.StunRate;
        this.CurrentIgnoreStunRate = cardCaptain.IgnoreStunRate;
        this.CurrentReflectionRate = cardCaptain.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardCaptain.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardCaptain.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardCaptain.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardCaptain.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardCaptain.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardCaptain.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardCaptain.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardCaptain.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardCaptain.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardCaptain.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardCaptain.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardCaptain.Position.Split('-');
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
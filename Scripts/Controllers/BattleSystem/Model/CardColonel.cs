using System;
using UnityEngine;
public class CardColonel : CardBase
{
    public override void Initialize(object data)
    {
        var cardColonel = data as CardColonels;

        if (cardColonel == null)
        {
            Debug.LogError("Sai type CardColonels");
            return;
        }
        // copy thuộc tính từ CardBase
        this.Id = cardColonel.Id;
        this.Name = cardColonel.Name;
        this.Image = cardColonel.Image;
        this.Type = cardColonel.Type;
        this.Rare = cardColonel.Rare;
        this.Power = cardColonel.Power;
        this.Health = cardColonel.Health;
        this.PhysicalAttack = cardColonel.PhysicalAttack;
        this.PhysicalDefense = cardColonel.PhysicalDefense;
        this.MagicalAttack = cardColonel.MagicalAttack;
        this.MagicalDefense = cardColonel.MagicalDefense;
        this.ChemicalAttack = cardColonel.ChemicalAttack;
        this.ChemicalDefense = cardColonel.ChemicalDefense;
        this.AtomicAttack = cardColonel.AtomicAttack;
        this.AtomicDefense = cardColonel.AtomicDefense;
        this.MentalAttack = cardColonel.MentalAttack;
        this.MentalDefense = cardColonel.MentalDefense;
        this.Speed = cardColonel.Speed;
        this.CriticalDamageRate = cardColonel.CriticalDamageRate;
        this.CriticalRate = cardColonel.CriticalRate;
        this.CriticalResistanceRate = cardColonel.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardColonel.IgnoreCriticalRate;
        this.PenetrationRate = cardColonel.PenetrationRate;
        this.PenetrationResistanceRate = cardColonel.PenetrationResistanceRate;
        this.EvasionRate = cardColonel.EvasionRate;
        this.DamageAbsorptionRate = cardColonel.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardColonel.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardColonel.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardColonel.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardColonel.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardColonel.AccuracyRate;
        this.LifestealRate = cardColonel.LifestealRate;
        this.Mana = cardColonel.Mana;
        this.ManaRegenerationRate = cardColonel.ManaRegenerationRate;
        this.ShieldStrength = cardColonel.ShieldStrength;
        this.Tenacity = cardColonel.Tenacity;
        this.ResistanceRate = cardColonel.ResistanceRate;
        this.ComboRate = cardColonel.ComboRate;
        this.IgnoreComboRate = cardColonel.IgnoreComboRate;
        this.ComboDamageRate = cardColonel.ComboDamageRate;
        this.ComboResistanceRate = cardColonel.ComboResistanceRate;
        this.StunRate = cardColonel.StunRate;
        this.IgnoreStunRate = cardColonel.IgnoreStunRate;
        this.ReflectionRate = cardColonel.ReflectionRate;
        this.IgnoreReflectionRate = cardColonel.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardColonel.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardColonel.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardColonel.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardColonel.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardColonel.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardColonel.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardColonel.NormalDamageRate;
        this.NormalResistanceRate = cardColonel.NormalResistanceRate;
        this.SkillDamageRate = cardColonel.SkillDamageRate;
        this.SkillResistanceRate = cardColonel.SkillResistanceRate;
        this.Position = cardColonel.Position;
        this.IsAlive = true;
        this.Skills = cardColonel.Skills;
        this.CardType = CardType.CardColonel;

        this.CurrentHealth = cardColonel.Health;
        this.CurrentPhysicalAttack = cardColonel.PhysicalAttack;
        this.CurrentPhysicalDefense = cardColonel.PhysicalDefense;
        this.CurrentMagicalAttack = cardColonel.MagicalAttack;
        this.CurrentMagicalDefense = cardColonel.MagicalDefense;
        this.CurrentChemicalAttack = cardColonel.ChemicalAttack;
        this.CurrentChemicalDefense = cardColonel.ChemicalDefense;
        this.CurrentAtomicAttack = cardColonel.AtomicAttack;
        this.CurrentAtomicDefense = cardColonel.AtomicDefense;
        this.CurrentMentalAttack = cardColonel.MentalAttack;
        this.CurrentMentalDefense = cardColonel.MentalDefense;
        this.CurrentSpeed = cardColonel.Speed;
        this.CurrentCriticalDamageRate = cardColonel.CriticalDamageRate;
        this.CurrentCriticalRate = cardColonel.CriticalRate;
        this.CurrentCriticalResistanceRate = cardColonel.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardColonel.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardColonel.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardColonel.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardColonel.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardColonel.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardColonel.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardColonel.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardColonel.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardColonel.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardColonel.AccuracyRate;
        this.CurrentLifestealRate = cardColonel.LifestealRate;
        this.CurrentMana = cardColonel.Mana;
        this.CurrentManaRegenerationRate = cardColonel.ManaRegenerationRate;
        this.CurrentShieldStrength = cardColonel.ShieldStrength;
        this.CurrentTenacity = cardColonel.Tenacity;
        this.CurrentResistanceRate = cardColonel.ResistanceRate;
        this.CurrentComboRate = cardColonel.ComboRate;
        this.CurrentIgnoreComboRate = cardColonel.IgnoreComboRate;
        this.CurrentComboDamageRate = cardColonel.ComboDamageRate;
        this.CurrentComboResistanceRate = cardColonel.ComboResistanceRate;
        this.CurrentStunRate = cardColonel.StunRate;
        this.CurrentIgnoreStunRate = cardColonel.IgnoreStunRate;
        this.CurrentReflectionRate = cardColonel.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardColonel.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardColonel.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardColonel.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardColonel.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardColonel.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardColonel.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardColonel.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardColonel.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardColonel.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardColonel.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardColonel.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardColonel.Position.Split('-');
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
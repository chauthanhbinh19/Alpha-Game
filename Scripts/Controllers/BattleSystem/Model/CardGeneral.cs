using System;
using UnityEngine;
public class CardGeneral : CardBase
{
    public override void Initialize(object data)
    {
        var cardGeneral = data as CardGenerals;

        if (cardGeneral == null)
        {
            Debug.LogError("Sai type CardGenerals");
            return;
        }
        // copy thuộc tính từ CardBase
        this.Id = cardGeneral.Id;
        this.Name = cardGeneral.Name;
        this.Image = cardGeneral.Image;
        this.Type = cardGeneral.Type;
        this.Rare = cardGeneral.Rare;
        this.Power = cardGeneral.Power;
        this.Health = cardGeneral.Health;
        this.PhysicalAttack = cardGeneral.PhysicalAttack;
        this.PhysicalDefense = cardGeneral.PhysicalDefense;
        this.MagicalAttack = cardGeneral.MagicalAttack;
        this.MagicalDefense = cardGeneral.MagicalDefense;
        this.ChemicalAttack = cardGeneral.ChemicalAttack;
        this.ChemicalDefense = cardGeneral.ChemicalDefense;
        this.AtomicAttack = cardGeneral.AtomicAttack;
        this.AtomicDefense = cardGeneral.AtomicDefense;
        this.MentalAttack = cardGeneral.MentalAttack;
        this.MentalDefense = cardGeneral.MentalDefense;
        this.Speed = cardGeneral.Speed;
        this.CriticalDamageRate = cardGeneral.CriticalDamageRate;
        this.CriticalRate = cardGeneral.CriticalRate;
        this.CriticalResistanceRate = cardGeneral.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardGeneral.IgnoreCriticalRate;
        this.PenetrationRate = cardGeneral.PenetrationRate;
        this.PenetrationResistanceRate = cardGeneral.PenetrationResistanceRate;
        this.EvasionRate = cardGeneral.EvasionRate;
        this.DamageAbsorptionRate = cardGeneral.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardGeneral.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardGeneral.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardGeneral.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardGeneral.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardGeneral.AccuracyRate;
        this.LifestealRate = cardGeneral.LifestealRate;
        this.Mana = cardGeneral.Mana;
        this.ManaRegenerationRate = cardGeneral.ManaRegenerationRate;
        this.ShieldStrength = cardGeneral.ShieldStrength;
        this.Tenacity = cardGeneral.Tenacity;
        this.ResistanceRate = cardGeneral.ResistanceRate;
        this.ComboRate = cardGeneral.ComboRate;
        this.IgnoreComboRate = cardGeneral.IgnoreComboRate;
        this.ComboDamageRate = cardGeneral.ComboDamageRate;
        this.ComboResistanceRate = cardGeneral.ComboResistanceRate;
        this.StunRate = cardGeneral.StunRate;
        this.IgnoreStunRate = cardGeneral.IgnoreStunRate;
        this.ReflectionRate = cardGeneral.ReflectionRate;
        this.IgnoreReflectionRate = cardGeneral.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardGeneral.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardGeneral.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardGeneral.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardGeneral.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardGeneral.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardGeneral.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardGeneral.NormalDamageRate;
        this.NormalResistanceRate = cardGeneral.NormalResistanceRate;
        this.SkillDamageRate = cardGeneral.SkillDamageRate;
        this.SkillResistanceRate = cardGeneral.SkillResistanceRate;
        this.Position = cardGeneral.Position;
        this.IsAlive = true;
        this.Skills = cardGeneral.Skills;
        this.CardType = CardType.CardGeneral;

        this.CurrentHealth = cardGeneral.Health;
        this.CurrentPhysicalAttack = cardGeneral.PhysicalAttack;
        this.CurrentPhysicalDefense = cardGeneral.PhysicalDefense;
        this.CurrentMagicalAttack = cardGeneral.MagicalAttack;
        this.CurrentMagicalDefense = cardGeneral.MagicalDefense;
        this.CurrentChemicalAttack = cardGeneral.ChemicalAttack;
        this.CurrentChemicalDefense = cardGeneral.ChemicalDefense;
        this.CurrentAtomicAttack = cardGeneral.AtomicAttack;
        this.CurrentAtomicDefense = cardGeneral.AtomicDefense;
        this.CurrentMentalAttack = cardGeneral.MentalAttack;
        this.CurrentMentalDefense = cardGeneral.MentalDefense;
        this.CurrentSpeed = cardGeneral.Speed;
        this.CurrentCriticalDamageRate = cardGeneral.CriticalDamageRate;
        this.CurrentCriticalRate = cardGeneral.CriticalRate;
        this.CurrentCriticalResistanceRate = cardGeneral.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardGeneral.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardGeneral.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardGeneral.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardGeneral.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardGeneral.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardGeneral.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardGeneral.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardGeneral.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardGeneral.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardGeneral.AccuracyRate;
        this.CurrentLifestealRate = cardGeneral.LifestealRate;
        this.CurrentMana = cardGeneral.Mana;
        this.CurrentManaRegenerationRate = cardGeneral.ManaRegenerationRate;
        this.CurrentShieldStrength = cardGeneral.ShieldStrength;
        this.CurrentTenacity = cardGeneral.Tenacity;
        this.CurrentResistanceRate = cardGeneral.ResistanceRate;
        this.CurrentComboRate = cardGeneral.ComboRate;
        this.CurrentIgnoreComboRate = cardGeneral.IgnoreComboRate;
        this.CurrentComboDamageRate = cardGeneral.ComboDamageRate;
        this.CurrentComboResistanceRate = cardGeneral.ComboResistanceRate;
        this.CurrentStunRate = cardGeneral.StunRate;
        this.CurrentIgnoreStunRate = cardGeneral.IgnoreStunRate;
        this.CurrentReflectionRate = cardGeneral.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardGeneral.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardGeneral.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardGeneral.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardGeneral.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardGeneral.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardGeneral.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardGeneral.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardGeneral.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardGeneral.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardGeneral.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardGeneral.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardGeneral.Position.Split('-');
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
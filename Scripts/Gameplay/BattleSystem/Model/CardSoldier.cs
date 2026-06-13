using System;
using UnityEngine;
public class CardSoldier : CardBase
{
    public override void Initialize(object data)
    {
        var cardSoldier = data as CardSoldiers;

        if (cardSoldier == null)
        {
            Debug.LogError("Sai type CardSoldiers");
            return;
        }
        // copy thuộc tính từ CardBase
        this.Id = cardSoldier.Id;
        this.Name = cardSoldier.Name;
        this.Image = cardSoldier.Image;
        this.Type = cardSoldier.Type;
        this.Rare = cardSoldier.Rarity;
        this.Power = cardSoldier.Power;
        this.Health = cardSoldier.Health;
        this.PhysicalAttack = cardSoldier.PhysicalAttack;
        this.PhysicalDefense = cardSoldier.PhysicalDefense;
        this.MagicalAttack = cardSoldier.MagicalAttack;
        this.MagicalDefense = cardSoldier.MagicalDefense;
        this.ChemicalAttack = cardSoldier.ChemicalAttack;
        this.ChemicalDefense = cardSoldier.ChemicalDefense;
        this.AtomicAttack = cardSoldier.AtomicAttack;
        this.AtomicDefense = cardSoldier.AtomicDefense;
        this.MentalAttack = cardSoldier.MentalAttack;
        this.MentalDefense = cardSoldier.MentalDefense;
        this.Speed = cardSoldier.Speed;
        this.CriticalDamageRate = cardSoldier.CriticalDamageRate;
        this.CriticalRate = cardSoldier.CriticalRate;
        this.CriticalResistanceRate = cardSoldier.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardSoldier.IgnoreCriticalRate;
        this.PenetrationRate = cardSoldier.PenetrationRate;
        this.PenetrationResistanceRate = cardSoldier.PenetrationResistanceRate;
        this.EvasionRate = cardSoldier.EvasionRate;
        this.DamageAbsorptionRate = cardSoldier.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardSoldier.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardSoldier.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardSoldier.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardSoldier.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardSoldier.AccuracyRate;
        this.LifestealRate = cardSoldier.LifestealRate;
        this.Mana = cardSoldier.Mana;
        this.ManaRegenerationRate = cardSoldier.ManaRegenerationRate;
        this.ShieldStrength = cardSoldier.ShieldStrength;
        this.Tenacity = cardSoldier.Tenacity;
        this.ResistanceRate = cardSoldier.ResistanceRate;
        this.ComboRate = cardSoldier.ComboRate;
        this.IgnoreComboRate = cardSoldier.IgnoreComboRate;
        this.ComboDamageRate = cardSoldier.ComboDamageRate;
        this.ComboResistanceRate = cardSoldier.ComboResistanceRate;
        this.StunRate = cardSoldier.StunRate;
        this.IgnoreStunRate = cardSoldier.IgnoreStunRate;
        this.ReflectionRate = cardSoldier.ReflectionRate;
        this.IgnoreReflectionRate = cardSoldier.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardSoldier.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardSoldier.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardSoldier.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardSoldier.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardSoldier.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardSoldier.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardSoldier.NormalDamageRate;
        this.NormalResistanceRate = cardSoldier.NormalResistanceRate;
        this.SkillDamageRate = cardSoldier.SkillDamageRate;
        this.SkillResistanceRate = cardSoldier.SkillResistanceRate;
        this.Position = cardSoldier.Position;
        this.IsAlive = true;
        this.Skills = cardSoldier.Skills;
        this.CardType = CardType.CardSoldier;
        this.Emblems = cardSoldier.Emblems;
        this.Class = cardSoldier.Class;

        this.CurrentHealth = cardSoldier.Health;
        this.CurrentPhysicalAttack = cardSoldier.PhysicalAttack;
        this.CurrentPhysicalDefense = cardSoldier.PhysicalDefense;
        this.CurrentMagicalAttack = cardSoldier.MagicalAttack;
        this.CurrentMagicalDefense = cardSoldier.MagicalDefense;
        this.CurrentChemicalAttack = cardSoldier.ChemicalAttack;
        this.CurrentChemicalDefense = cardSoldier.ChemicalDefense;
        this.CurrentAtomicAttack = cardSoldier.AtomicAttack;
        this.CurrentAtomicDefense = cardSoldier.AtomicDefense;
        this.CurrentMentalAttack = cardSoldier.MentalAttack;
        this.CurrentMentalDefense = cardSoldier.MentalDefense;
        this.CurrentSpeed = cardSoldier.Speed;
        this.CurrentCriticalDamageRate = cardSoldier.CriticalDamageRate;
        this.CurrentCriticalRate = cardSoldier.CriticalRate;
        this.CurrentCriticalResistanceRate = cardSoldier.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardSoldier.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardSoldier.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardSoldier.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardSoldier.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardSoldier.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardSoldier.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardSoldier.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardSoldier.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardSoldier.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardSoldier.AccuracyRate;
        this.CurrentLifestealRate = cardSoldier.LifestealRate;
        this.CurrentMana = cardSoldier.Mana;
        this.CurrentManaRegenerationRate = cardSoldier.ManaRegenerationRate;
        this.CurrentShieldStrength = cardSoldier.ShieldStrength;
        this.CurrentTenacity = cardSoldier.Tenacity;
        this.CurrentResistanceRate = cardSoldier.ResistanceRate;
        this.CurrentComboRate = cardSoldier.ComboRate;
        this.CurrentIgnoreComboRate = cardSoldier.IgnoreComboRate;
        this.CurrentComboDamageRate = cardSoldier.ComboDamageRate;
        this.CurrentComboResistanceRate = cardSoldier.ComboResistanceRate;
        this.CurrentStunRate = cardSoldier.StunRate;
        this.CurrentIgnoreStunRate = cardSoldier.IgnoreStunRate;
        this.CurrentReflectionRate = cardSoldier.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardSoldier.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardSoldier.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardSoldier.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardSoldier.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardSoldier.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardSoldier.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardSoldier.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardSoldier.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardSoldier.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardSoldier.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardSoldier.SkillResistanceRate;

        this.CurrentMovementPoint = cardSoldier.Class.MovementPoint;

        // Parse position code "x-y"
        string[] parts = cardSoldier.Position.Split('-');
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
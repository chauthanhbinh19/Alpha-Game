using System;
using UnityEngine;
public class CardAdmiral : CardBase
{
    public override void Initialize(object data)
    {
        var cardAdmiral = data as CardAdmirals;

        if (cardAdmiral == null)
        {
            Debug.LogError("Sai type CardAdmirals");
            return;
        }
        // copy thuộc tính từ CardBase
        this.Id = cardAdmiral.Id;
        this.Name = cardAdmiral.Name;
        this.Image = cardAdmiral.Image;
        this.Type = cardAdmiral.Type;
        this.Rare = cardAdmiral.Rare;
        this.Power = cardAdmiral.Power;
        this.Health = cardAdmiral.Health;
        this.PhysicalAttack = cardAdmiral.PhysicalAttack;
        this.PhysicalDefense = cardAdmiral.PhysicalDefense;
        this.MagicalAttack = cardAdmiral.MagicalAttack;
        this.MagicalDefense = cardAdmiral.MagicalDefense;
        this.ChemicalAttack = cardAdmiral.ChemicalAttack;
        this.ChemicalDefense = cardAdmiral.ChemicalDefense;
        this.AtomicAttack = cardAdmiral.AtomicAttack;
        this.AtomicDefense = cardAdmiral.AtomicDefense;
        this.MentalAttack = cardAdmiral.MentalAttack;
        this.MentalDefense = cardAdmiral.MentalDefense;
        this.Speed = cardAdmiral.Speed;
        this.CriticalDamageRate = cardAdmiral.CriticalDamageRate;
        this.CriticalRate = cardAdmiral.CriticalRate;
        this.CriticalResistanceRate = cardAdmiral.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardAdmiral.IgnoreCriticalRate;
        this.PenetrationRate = cardAdmiral.PenetrationRate;
        this.PenetrationResistanceRate = cardAdmiral.PenetrationResistanceRate;
        this.EvasionRate = cardAdmiral.EvasionRate;
        this.DamageAbsorptionRate = cardAdmiral.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardAdmiral.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardAdmiral.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardAdmiral.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardAdmiral.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardAdmiral.AccuracyRate;
        this.LifestealRate = cardAdmiral.LifestealRate;
        this.Mana = cardAdmiral.Mana;
        this.ManaRegenerationRate = cardAdmiral.ManaRegenerationRate;
        this.ShieldStrength = cardAdmiral.ShieldStrength;
        this.Tenacity = cardAdmiral.Tenacity;
        this.ResistanceRate = cardAdmiral.ResistanceRate;
        this.ComboRate = cardAdmiral.ComboRate;
        this.IgnoreComboRate = cardAdmiral.IgnoreComboRate;
        this.ComboDamageRate = cardAdmiral.ComboDamageRate;
        this.ComboResistanceRate = cardAdmiral.ComboResistanceRate;
        this.StunRate = cardAdmiral.StunRate;
        this.IgnoreStunRate = cardAdmiral.IgnoreStunRate;
        this.ReflectionRate = cardAdmiral.ReflectionRate;
        this.IgnoreReflectionRate = cardAdmiral.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardAdmiral.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardAdmiral.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardAdmiral.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardAdmiral.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardAdmiral.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardAdmiral.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardAdmiral.NormalDamageRate;
        this.NormalResistanceRate = cardAdmiral.NormalResistanceRate;
        this.SkillDamageRate = cardAdmiral.SkillDamageRate;
        this.SkillResistanceRate = cardAdmiral.SkillResistanceRate;
        this.Position = cardAdmiral.Position;
        this.IsAlive = true;
        this.Skills = cardAdmiral.Skills;
        this.CardType = CardType.CardAdmiral;

        this.CurrentHealth = cardAdmiral.Health;
        this.CurrentPhysicalAttack = cardAdmiral.PhysicalAttack;
        this.CurrentPhysicalDefense = cardAdmiral.PhysicalDefense;
        this.CurrentMagicalAttack = cardAdmiral.MagicalAttack;
        this.CurrentMagicalDefense = cardAdmiral.MagicalDefense;
        this.CurrentChemicalAttack = cardAdmiral.ChemicalAttack;
        this.CurrentChemicalDefense = cardAdmiral.ChemicalDefense;
        this.CurrentAtomicAttack = cardAdmiral.AtomicAttack;
        this.CurrentAtomicDefense = cardAdmiral.AtomicDefense;
        this.CurrentMentalAttack = cardAdmiral.MentalAttack;
        this.CurrentMentalDefense = cardAdmiral.MentalDefense;
        this.CurrentSpeed = cardAdmiral.Speed;
        this.CurrentCriticalDamageRate = cardAdmiral.CriticalDamageRate;
        this.CurrentCriticalRate = cardAdmiral.CriticalRate;
        this.CurrentCriticalResistanceRate = cardAdmiral.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardAdmiral.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardAdmiral.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardAdmiral.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardAdmiral.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardAdmiral.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardAdmiral.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardAdmiral.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardAdmiral.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardAdmiral.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardAdmiral.AccuracyRate;
        this.CurrentLifestealRate = cardAdmiral.LifestealRate;
        this.CurrentMana = cardAdmiral.Mana;
        this.CurrentManaRegenerationRate = cardAdmiral.ManaRegenerationRate;
        this.CurrentShieldStrength = cardAdmiral.ShieldStrength;
        this.CurrentTenacity = cardAdmiral.Tenacity;
        this.CurrentResistanceRate = cardAdmiral.ResistanceRate;
        this.CurrentComboRate = cardAdmiral.ComboRate;
        this.CurrentIgnoreComboRate = cardAdmiral.IgnoreComboRate;
        this.CurrentComboDamageRate = cardAdmiral.ComboDamageRate;
        this.CurrentComboResistanceRate = cardAdmiral.ComboResistanceRate;
        this.CurrentStunRate = cardAdmiral.StunRate;
        this.CurrentIgnoreStunRate = cardAdmiral.IgnoreStunRate;
        this.CurrentReflectionRate = cardAdmiral.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardAdmiral.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardAdmiral.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardAdmiral.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardAdmiral.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardAdmiral.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardAdmiral.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardAdmiral.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardAdmiral.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardAdmiral.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardAdmiral.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardAdmiral.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardAdmiral.Position.Split('-');
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
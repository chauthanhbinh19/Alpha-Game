using System;
using UnityEngine;
public class CardHero : CardBase
{
    public void Initialize(CardHeroes cardHeroes)
    {
        // copy thuộc tính từ CardBase
        this.Id = cardHeroes.Id;
        this.CardName = cardHeroes.Name;
        this.Image = cardHeroes.Image;
        this.Type = cardHeroes.Type;
        this.Rare = cardHeroes.Rare;
        this.Power = cardHeroes.Power;
        this.Health = cardHeroes.Health;
        this.PhysicalAttack = cardHeroes.PhysicalAttack;
        this.PhysicalDefense = cardHeroes.PhysicalDefense;
        this.MagicalAttack = cardHeroes.MagicalAttack;
        this.MagicalDefense = cardHeroes.MagicalDefense;
        this.ChemicalAttack = cardHeroes.ChemicalAttack;
        this.ChemicalDefense = cardHeroes.ChemicalDefense;
        this.AtomicAttack = cardHeroes.AtomicAttack;
        this.AtomicDefense = cardHeroes.AtomicDefense;
        this.MentalAttack = cardHeroes.MentalAttack;
        this.MentalDefense = cardHeroes.MentalDefense;
        this.Speed = cardHeroes.Speed;
        this.CriticalDamageRate = cardHeroes.CriticalDamageRate;
        this.CriticalRate = cardHeroes.CriticalRate;
        this.CriticalResistanceRate = cardHeroes.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardHeroes.IgnoreCriticalRate;
        this.PenetrationRate = cardHeroes.PenetrationRate;
        this.PenetrationResistanceRate = cardHeroes.PenetrationResistanceRate;
        this.EvasionRate = cardHeroes.EvasionRate;
        this.DamageAbsorptionRate = cardHeroes.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardHeroes.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardHeroes.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardHeroes.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardHeroes.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardHeroes.AccuracyRate;
        this.LifestealRate = cardHeroes.LifestealRate;
        this.Mana = cardHeroes.Mana;
        this.ManaRegenerationRate = cardHeroes.ManaRegenerationRate;
        this.ShieldStrength = cardHeroes.ShieldStrength;
        this.Tenacity = cardHeroes.Tenacity;
        this.ResistanceRate = cardHeroes.ResistanceRate;
        this.ComboRate = cardHeroes.ComboRate;
        this.IgnoreComboRate = cardHeroes.IgnoreComboRate;
        this.ComboDamageRate = cardHeroes.ComboDamageRate;
        this.ComboResistanceRate = cardHeroes.ComboResistanceRate;
        this.StunRate = cardHeroes.StunRate;
        this.IgnoreStunRate = cardHeroes.IgnoreStunRate;
        this.ReflectionRate = cardHeroes.ReflectionRate;
        this.IgnoreReflectionRate = cardHeroes.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardHeroes.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardHeroes.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardHeroes.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardHeroes.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardHeroes.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardHeroes.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardHeroes.NormalDamageRate;
        this.NormalResistanceRate = cardHeroes.NormalResistanceRate;
        this.SkillDamageRate = cardHeroes.SkillDamageRate;
        this.SkillResistanceRate = cardHeroes.SkillResistanceRate;
        this.Position = cardHeroes.Position;
        this.IsAlive = true;
        this.Skills = cardHeroes.Skills;

        this.CurrentHealth = cardHeroes.Health;
        this.CurrentPhysicalAttack = cardHeroes.PhysicalAttack;
        this.CurrentPhysicalDefense = cardHeroes.PhysicalDefense;
        this.CurrentMagicalAttack = cardHeroes.MagicalAttack;
        this.CurrentMagicalDefense = cardHeroes.MagicalDefense;
        this.CurrentChemicalAttack = cardHeroes.ChemicalAttack;
        this.CurrentChemicalDefense = cardHeroes.ChemicalDefense;
        this.CurrentAtomicAttack = cardHeroes.AtomicAttack;
        this.CurrentAtomicDefense = cardHeroes.AtomicDefense;
        this.CurrentMentalAttack = cardHeroes.MentalAttack;
        this.CurrentMentalDefense = cardHeroes.MentalDefense;
        this.CurrentSpeed = cardHeroes.Speed;
        this.CurrentCriticalDamageRate = cardHeroes.CriticalDamageRate;
        this.CurrentCriticalRate = cardHeroes.CriticalRate;
        this.CurrentCriticalResistanceRate = cardHeroes.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardHeroes.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardHeroes.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardHeroes.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardHeroes.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardHeroes.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardHeroes.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardHeroes.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardHeroes.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardHeroes.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardHeroes.AccuracyRate;
        this.CurrentLifestealRate = cardHeroes.LifestealRate;
        this.CurrentMana = cardHeroes.Mana;
        this.CurrentManaRegenerationRate = cardHeroes.ManaRegenerationRate;
        this.CurrentShieldStrength = cardHeroes.ShieldStrength;
        this.CurrentTenacity = cardHeroes.Tenacity;
        this.CurrentResistanceRate = cardHeroes.ResistanceRate;
        this.CurrentComboRate = cardHeroes.ComboRate;
        this.CurrentIgnoreComboRate = cardHeroes.IgnoreComboRate;
        this.CurrentComboDamageRate = cardHeroes.ComboDamageRate;
        this.CurrentComboResistanceRate = cardHeroes.ComboResistanceRate;
        this.CurrentStunRate = cardHeroes.StunRate;
        this.CurrentIgnoreStunRate = cardHeroes.IgnoreStunRate;
        this.CurrentReflectionRate = cardHeroes.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardHeroes.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardHeroes.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardHeroes.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardHeroes.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardHeroes.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardHeroes.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardHeroes.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardHeroes.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardHeroes.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardHeroes.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardHeroes.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardHeroes.Position.Split('-');
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
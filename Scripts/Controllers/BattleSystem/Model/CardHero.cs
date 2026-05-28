using System;
using UnityEngine;
public class CardHero : CardBase
{
    public override void Initialize(object data)
    {
        var cardHero = data as CardHeroes;

        if (cardHero == null)
        {
            Debug.LogError("Sai type CardHeroes");
            return;
        }
        // copy thuộc tính từ CardBase
        this.Id = cardHero.Id;
        this.Name = cardHero.Name;
        this.Image = cardHero.Image;
        this.Type = cardHero.Type;
        this.Rare = cardHero.Rare;
        this.Power = cardHero.Power;
        this.Health = cardHero.Health;
        this.PhysicalAttack = cardHero.PhysicalAttack;
        this.PhysicalDefense = cardHero.PhysicalDefense;
        this.MagicalAttack = cardHero.MagicalAttack;
        this.MagicalDefense = cardHero.MagicalDefense;
        this.ChemicalAttack = cardHero.ChemicalAttack;
        this.ChemicalDefense = cardHero.ChemicalDefense;
        this.AtomicAttack = cardHero.AtomicAttack;
        this.AtomicDefense = cardHero.AtomicDefense;
        this.MentalAttack = cardHero.MentalAttack;
        this.MentalDefense = cardHero.MentalDefense;
        this.Speed = cardHero.Speed;
        this.CriticalDamageRate = cardHero.CriticalDamageRate;
        this.CriticalRate = cardHero.CriticalRate;
        this.CriticalResistanceRate = cardHero.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardHero.IgnoreCriticalRate;
        this.PenetrationRate = cardHero.PenetrationRate;
        this.PenetrationResistanceRate = cardHero.PenetrationResistanceRate;
        this.EvasionRate = cardHero.EvasionRate;
        this.DamageAbsorptionRate = cardHero.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardHero.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardHero.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardHero.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardHero.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardHero.AccuracyRate;
        this.LifestealRate = cardHero.LifestealRate;
        this.Mana = cardHero.Mana;
        this.ManaRegenerationRate = cardHero.ManaRegenerationRate;
        this.ShieldStrength = cardHero.ShieldStrength;
        this.Tenacity = cardHero.Tenacity;
        this.ResistanceRate = cardHero.ResistanceRate;
        this.ComboRate = cardHero.ComboRate;
        this.IgnoreComboRate = cardHero.IgnoreComboRate;
        this.ComboDamageRate = cardHero.ComboDamageRate;
        this.ComboResistanceRate = cardHero.ComboResistanceRate;
        this.StunRate = cardHero.StunRate;
        this.IgnoreStunRate = cardHero.IgnoreStunRate;
        this.ReflectionRate = cardHero.ReflectionRate;
        this.IgnoreReflectionRate = cardHero.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardHero.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardHero.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardHero.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardHero.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardHero.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardHero.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardHero.NormalDamageRate;
        this.NormalResistanceRate = cardHero.NormalResistanceRate;
        this.SkillDamageRate = cardHero.SkillDamageRate;
        this.SkillResistanceRate = cardHero.SkillResistanceRate;
        this.Position = cardHero.Position;
        this.IsAlive = true;
        this.Skills = cardHero.Skills;
        this.CardType = CardType.CardHero;

        this.CurrentHealth = cardHero.Health;
        this.CurrentPhysicalAttack = cardHero.PhysicalAttack;
        this.CurrentPhysicalDefense = cardHero.PhysicalDefense;
        this.CurrentMagicalAttack = cardHero.MagicalAttack;
        this.CurrentMagicalDefense = cardHero.MagicalDefense;
        this.CurrentChemicalAttack = cardHero.ChemicalAttack;
        this.CurrentChemicalDefense = cardHero.ChemicalDefense;
        this.CurrentAtomicAttack = cardHero.AtomicAttack;
        this.CurrentAtomicDefense = cardHero.AtomicDefense;
        this.CurrentMentalAttack = cardHero.MentalAttack;
        this.CurrentMentalDefense = cardHero.MentalDefense;
        this.CurrentSpeed = cardHero.Speed;
        this.CurrentCriticalDamageRate = cardHero.CriticalDamageRate;
        this.CurrentCriticalRate = cardHero.CriticalRate;
        this.CurrentCriticalResistanceRate = cardHero.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardHero.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardHero.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardHero.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardHero.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardHero.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardHero.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardHero.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardHero.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardHero.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardHero.AccuracyRate;
        this.CurrentLifestealRate = cardHero.LifestealRate;
        this.CurrentMana = cardHero.Mana;
        this.CurrentManaRegenerationRate = cardHero.ManaRegenerationRate;
        this.CurrentShieldStrength = cardHero.ShieldStrength;
        this.CurrentTenacity = cardHero.Tenacity;
        this.CurrentResistanceRate = cardHero.ResistanceRate;
        this.CurrentComboRate = cardHero.ComboRate;
        this.CurrentIgnoreComboRate = cardHero.IgnoreComboRate;
        this.CurrentComboDamageRate = cardHero.ComboDamageRate;
        this.CurrentComboResistanceRate = cardHero.ComboResistanceRate;
        this.CurrentStunRate = cardHero.StunRate;
        this.CurrentIgnoreStunRate = cardHero.IgnoreStunRate;
        this.CurrentReflectionRate = cardHero.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardHero.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardHero.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardHero.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardHero.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardHero.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardHero.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardHero.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardHero.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardHero.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardHero.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardHero.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardHero.Position.Split('-');
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
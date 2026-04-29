using System;
using UnityEngine;
public class CardMonster : CardBase
{
    public override void Initialize(object data)
    {
        var cardMonster = data as CardMonsters;

        if (cardMonster == null)
        {
            Debug.LogError("Sai type CardMonsters");
            return;
        }
        // copy thuộc tính từ CardBase
        this.Id = cardMonster.Id;
        this.Name = cardMonster.Name;
        this.Image = cardMonster.Image;
        this.Type = cardMonster.Type;
        this.Rare = cardMonster.Rare;
        this.Power = cardMonster.Power;
        this.Health = cardMonster.Health;
        this.PhysicalAttack = cardMonster.PhysicalAttack;
        this.PhysicalDefense = cardMonster.PhysicalDefense;
        this.MagicalAttack = cardMonster.MagicalAttack;
        this.MagicalDefense = cardMonster.MagicalDefense;
        this.ChemicalAttack = cardMonster.ChemicalAttack;
        this.ChemicalDefense = cardMonster.ChemicalDefense;
        this.AtomicAttack = cardMonster.AtomicAttack;
        this.AtomicDefense = cardMonster.AtomicDefense;
        this.MentalAttack = cardMonster.MentalAttack;
        this.MentalDefense = cardMonster.MentalDefense;
        this.Speed = cardMonster.Speed;
        this.CriticalDamageRate = cardMonster.CriticalDamageRate;
        this.CriticalRate = cardMonster.CriticalRate;
        this.CriticalResistanceRate = cardMonster.CriticalResistanceRate;
        this.IgnoreCriticalRate = cardMonster.IgnoreCriticalRate;
        this.PenetrationRate = cardMonster.PenetrationRate;
        this.PenetrationResistanceRate = cardMonster.PenetrationResistanceRate;
        this.EvasionRate = cardMonster.EvasionRate;
        this.DamageAbsorptionRate = cardMonster.DamageAbsorptionRate;
        this.AbsorbedDamageRate = cardMonster.AbsorbedDamageRate;
        this.IgnoreDamageAbsorptionRate = cardMonster.IgnoreDamageAbsorptionRate;
        this.VitalityRegenerationRate = cardMonster.VitalityRegenerationRate;
        this.VitalityRegenerationResistanceRate = cardMonster.VitalityRegenerationResistanceRate;
        this.AccuracyRate = cardMonster.AccuracyRate;
        this.LifestealRate = cardMonster.LifestealRate;
        this.Mana = cardMonster.Mana;
        this.ManaRegenerationRate = cardMonster.ManaRegenerationRate;
        this.ShieldStrength = cardMonster.ShieldStrength;
        this.Tenacity = cardMonster.Tenacity;
        this.ResistanceRate = cardMonster.ResistanceRate;
        this.ComboRate = cardMonster.ComboRate;
        this.IgnoreComboRate = cardMonster.IgnoreComboRate;
        this.ComboDamageRate = cardMonster.ComboDamageRate;
        this.ComboResistanceRate = cardMonster.ComboResistanceRate;
        this.StunRate = cardMonster.StunRate;
        this.IgnoreStunRate = cardMonster.IgnoreStunRate;
        this.ReflectionRate = cardMonster.ReflectionRate;
        this.IgnoreReflectionRate = cardMonster.IgnoreReflectionRate;
        this.ReflectionDamageRate = cardMonster.ReflectionDamageRate;
        this.ReflectionResistanceRate = cardMonster.ReflectionResistanceRate;
        this.DamageToDifferentFactionRate = cardMonster.DamageToDifferentFactionRate;
        this.ResistanceToDifferentFactionRate = cardMonster.ResistanceToDifferentFactionRate;
        this.DamageToSameFactionRate = cardMonster.DamageToSameFactionRate;
        this.ResistanceToSameFactionRate = cardMonster.ResistanceToSameFactionRate;
        this.NormalDamageRate = cardMonster.NormalDamageRate;
        this.NormalResistanceRate = cardMonster.NormalResistanceRate;
        this.SkillDamageRate = cardMonster.SkillDamageRate;
        this.SkillResistanceRate = cardMonster.SkillResistanceRate;
        this.Position = cardMonster.Position;
        this.IsAlive = true;
        this.Skills = cardMonster.Skills;
        this.CardType = CardType.CardMonster;

        this.CurrentHealth = cardMonster.Health;
        this.CurrentPhysicalAttack = cardMonster.PhysicalAttack;
        this.CurrentPhysicalDefense = cardMonster.PhysicalDefense;
        this.CurrentMagicalAttack = cardMonster.MagicalAttack;
        this.CurrentMagicalDefense = cardMonster.MagicalDefense;
        this.CurrentChemicalAttack = cardMonster.ChemicalAttack;
        this.CurrentChemicalDefense = cardMonster.ChemicalDefense;
        this.CurrentAtomicAttack = cardMonster.AtomicAttack;
        this.CurrentAtomicDefense = cardMonster.AtomicDefense;
        this.CurrentMentalAttack = cardMonster.MentalAttack;
        this.CurrentMentalDefense = cardMonster.MentalDefense;
        this.CurrentSpeed = cardMonster.Speed;
        this.CurrentCriticalDamageRate = cardMonster.CriticalDamageRate;
        this.CurrentCriticalRate = cardMonster.CriticalRate;
        this.CurrentCriticalResistanceRate = cardMonster.CriticalResistanceRate;
        this.CurrentIgnoreCriticalRate = cardMonster.IgnoreCriticalRate;
        this.CurrentPenetrationRate = cardMonster.PenetrationRate;
        this.CurrentPenetrationResistanceRate = cardMonster.PenetrationResistanceRate;
        this.CurrentEvasionRate = cardMonster.EvasionRate;
        this.CurrentDamageAbsorptionRate = cardMonster.DamageAbsorptionRate;
        this.CurrentAbsorbedDamageRate = cardMonster.AbsorbedDamageRate;
        this.CurrentIgnoreDamageAbsorptionRate = cardMonster.IgnoreDamageAbsorptionRate;
        this.CurrentVitalityRegenerationRate = cardMonster.VitalityRegenerationRate;
        this.CurrentVitalityRegenerationResistanceRate = cardMonster.VitalityRegenerationResistanceRate;
        this.CurrentAccuracyRate = cardMonster.AccuracyRate;
        this.CurrentLifestealRate = cardMonster.LifestealRate;
        this.CurrentMana = cardMonster.Mana;
        this.CurrentManaRegenerationRate = cardMonster.ManaRegenerationRate;
        this.CurrentShieldStrength = cardMonster.ShieldStrength;
        this.CurrentTenacity = cardMonster.Tenacity;
        this.CurrentResistanceRate = cardMonster.ResistanceRate;
        this.CurrentComboRate = cardMonster.ComboRate;
        this.CurrentIgnoreComboRate = cardMonster.IgnoreComboRate;
        this.CurrentComboDamageRate = cardMonster.ComboDamageRate;
        this.CurrentComboResistanceRate = cardMonster.ComboResistanceRate;
        this.CurrentStunRate = cardMonster.StunRate;
        this.CurrentIgnoreStunRate = cardMonster.IgnoreStunRate;
        this.CurrentReflectionRate = cardMonster.ReflectionRate;
        this.CurrentIgnoreReflectionRate = cardMonster.IgnoreReflectionRate;
        this.CurrentReflectionDamageRate = cardMonster.ReflectionDamageRate;
        this.CurrentReflectionResistanceRate = cardMonster.ReflectionResistanceRate;
        this.CurrentDamageToDifferentFactionRate = cardMonster.DamageToDifferentFactionRate;
        this.CurrentResistanceToDifferentFactionRate = cardMonster.ResistanceToDifferentFactionRate;
        this.CurrentDamageToSameFactionRate = cardMonster.DamageToSameFactionRate;
        this.CurrentResistanceToSameFactionRate = cardMonster.ResistanceToSameFactionRate;
        this.CurrentNormalDamageRate = cardMonster.NormalDamageRate;
        this.CurrentNormalResistanceRate = cardMonster.NormalResistanceRate;
        this.CurrentSkillDamageRate = cardMonster.SkillDamageRate;
        this.CurrentSkillResistanceRate = cardMonster.SkillResistanceRate;

        // Parse position code "x-y"
        string[] parts = cardMonster.Position.Split('-');
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
using UnityEngine;
using System.Collections;
using TMPro;
using System;
using System.Collections.Generic;

public abstract class CardBase
{
    public string Id { get; set; }
    public string CardName { get; set; }
    public string Image { get; set; }
    public string Type { get; set; }
    public string Rare { get; set; }
    public double Power { get; set; } = 0;
    //Base
    public double Health { get; set; } = 0;
    public double PhysicalAttack { get; set; } = 0;
    public double PhysicalDefense { get; set; } = 0;
    public double MagicalAttack { get; set; } = 0;
    public double MagicalDefense { get; set; } = 0;
    public double ChemicalAttack { get; set; } = 0;
    public double ChemicalDefense { get; set; } = 0;
    public double AtomicAttack { get; set; } = 0;
    public double AtomicDefense { get; set; } = 0;
    public double MentalAttack { get; set; } = 0;
    public double MentalDefense { get; set; } = 0;
    public double Speed { get; set; } = 0;
    public double CriticalDamageRate { get; set; } = 0;
    public double CriticalRate { get; set; } = 0;
    public double CriticalResistanceRate { get; set; } = 0;
    public double IgnoreCriticalRate { get; set; } = 0;
    public double PenetrationRate { get; set; } = 0;
    public double PenetrationResistanceRate { get; set; } = 0;
    public double EvasionRate { get; set; } = 0;
    public double DamageAbsorptionRate { get; set; } = 0;
    public double IgnoreDamageAbsorptionRate { get; set; } = 0;
    public double AbsorbedDamageRate { get; set; } = 0;
    public double VitalityRegenerationRate { get; set; } = 0;
    public double VitalityRegenerationResistanceRate { get; set; } = 0;
    public double AccuracyRate { get; set; } = 0;
    public double LifestealRate { get; set; } = 0;
    public double Mana { get; set; } = 0;
    public double ManaRegenerationRate { get; set; } = 0;
    public double ShieldStrength { get; set; } = 0;
    public double Tenacity { get; set; } = 0;
    public double ResistanceRate { get; set; } = 0;
    public double ComboRate { get; set; } = 0;
    public double IgnoreComboRate { get; set; } = 0;
    public double ComboDamageRate { get; set; } = 0;
    public double ComboResistanceRate { get; set; } = 0;
    public double StunRate { get; set; } = 0;
    public double IgnoreStunRate { get; set; } = 0;
    public double ReflectionRate { get; set; } = 0;
    public double IgnoreReflectionRate { get; set; } = 0;
    public double ReflectionDamageRate { get; set; } = 0;
    public double ReflectionResistanceRate { get; set; } = 0;
    public double DamageToDifferentFactionRate { get; set; } = 0;
    public double ResistanceToDifferentFactionRate { get; set; } = 0;
    public double DamageToSameFactionRate { get; set; } = 0;
    public double ResistanceToSameFactionRate { get; set; } = 0;
    public double NormalDamageRate { get; set; } = 0;
    public double NormalResistanceRate { get; set; } = 0;
    public double SkillDamageRate { get; set; } = 0;
    public double SkillResistanceRate { get; set; } = 0;
    public List<Skills> Skills { get; set; } = new List<Skills>{ };
    public List<string> Emblems { get; set; } = new();
    //Current
    public double CurrentHealth { get; set; } = 0;
    public double CurrentPhysicalAttack { get; set; } = 0;
    public double CurrentPhysicalDefense { get; set; } = 0;
    public double CurrentMagicalAttack { get; set; } = 0;
    public double CurrentMagicalDefense { get; set; } = 0;
    public double CurrentChemicalAttack { get; set; } = 0;
    public double CurrentChemicalDefense { get; set; } = 0;
    public double CurrentAtomicAttack { get; set; } = 0;
    public double CurrentAtomicDefense { get; set; } = 0;
    public double CurrentMentalAttack { get; set; } = 0;
    public double CurrentMentalDefense { get; set; } = 0;
    public double CurrentSpeed { get; set; } = 0;
    public double CurrentCriticalDamageRate { get; set; } = 0;
    public double CurrentCriticalRate { get; set; } = 0;
    public double CurrentCriticalResistanceRate { get; set; } = 0;
    public double CurrentIgnoreCriticalRate { get; set; } = 0;
    public double CurrentPenetrationRate { get; set; } = 0;
    public double CurrentPenetrationResistanceRate { get; set; } = 0;
    public double CurrentEvasionRate { get; set; } = 0;
    public double CurrentDamageAbsorptionRate { get; set; } = 0;
    public double CurrentIgnoreDamageAbsorptionRate { get; set; } = 0;
    public double CurrentAbsorbedDamageRate { get; set; } = 0;
    public double CurrentVitalityRegenerationRate { get; set; } = 0;
    public double CurrentVitalityRegenerationResistanceRate { get; set; } = 0;
    public double CurrentAccuracyRate { get; set; } = 0;
    public double CurrentLifestealRate { get; set; } = 0;
    public double CurrentMana { get; set; } = 0;
    public double CurrentManaRegenerationRate { get; set; } = 0;
    public double CurrentShieldStrength { get; set; } = 0;
    public double CurrentTenacity { get; set; } = 0;
    public double CurrentResistanceRate { get; set; } = 0;
    public double CurrentComboRate { get; set; } = 0;
    public double CurrentIgnoreComboRate { get; set; } = 0;
    public double CurrentComboDamageRate { get; set; } = 0;
    public double CurrentComboResistanceRate { get; set; } = 0;
    public double CurrentStunRate { get; set; } = 0;
    public double CurrentIgnoreStunRate { get; set; } = 0;
    public double CurrentReflectionRate { get; set; } = 0;
    public double CurrentIgnoreReflectionRate { get; set; } = 0;
    public double CurrentReflectionDamageRate { get; set; } = 0;
    public double CurrentReflectionResistanceRate { get; set; } = 0;
    public double CurrentDamageToDifferentFactionRate { get; set; } = 0;
    public double CurrentResistanceToDifferentFactionRate { get; set; } = 0;
    public double CurrentDamageToSameFactionRate { get; set; } = 0;
    public double CurrentResistanceToSameFactionRate { get; set; } = 0;
    public double CurrentNormalDamageRate { get; set; } = 0;
    public double CurrentNormalResistanceRate { get; set; } = 0;
    public double CurrentSkillDamageRate { get; set; } = 0;
    public double CurrentSkillResistanceRate { get; set; } = 0;
    public string Position { get; set; }
    public int MainPosition { get; set; }
    public int SubIndex { get; set; }
    public bool IsAlive { get; set; }
    public GameObject damagePopupPrefab;
    public HealthBar healthBar;
    public virtual void PerformAction(PlayerController opponent)
    {
        var target = ChooseTarget(opponent);
        if (target == null)
        {
            Debug.Log($"{CardName} has no valid target.");
            return;
        }

        // Attack(target);
    }

    protected virtual CardBase ChooseTarget(PlayerController opponent)
    {
        if (opponent == null)
        {
            return null;
        }

        foreach (var enemyCard in opponent.GetCards())
        {
            if (enemyCard != null && enemyCard.IsAlive)
            {
                return enemyCard;
            }
        }

        return null;
    }

    public virtual void TakeDamage(double amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth < 0) CurrentHealth = 0;

        IsAlive = CurrentHealth > 0;

        if (healthBar != null)
        {
            healthBar.SetHealth(CurrentHealth);
        }
        // Có thể thêm hiệu ứng hoặc sự kiện khi bị tấn công ở đây
        // ShowDamagePopup(amount); // Hiển thị popup sát thương
    }

    public enum AttackType
    {
        Normal,
        Skill
    }

    protected double GetCombatValue(double currentValue, double baseValue)
    {
        return Math.Max(0, currentValue > 0 ? currentValue : baseValue);
    }

    protected double GetAttackTypeDamageRate(AttackType attackType)
    {
        if (attackType == AttackType.Skill)
        {
            return Math.Max(0, SkillDamageRate + CurrentSkillDamageRate);
        }

        return Math.Max(0, NormalDamageRate + CurrentNormalDamageRate);
    }



    // public void RegenerateMana()
    // {
    //     float regenAmount = (float)(CurrentMana * (CurrentManaRegenerationRate / 100.0));

    //     CurrentMana += regenAmount;

    //     // Giới hạn mana không vượt quá tối đa
    //     // Giả sử bạn có biến manaMax để lưu mana tối đa, nếu chưa có thì có thể thêm hoặc lấy từ đâu đó
    //     float manaMax = 100f; // ví dụ tạm, bạn thay bằng giá trị thực tế

    //     if (CurrentMana > manaMax)
    //     {
    //         CurrentMana = manaMax;
    //     }
    // }

    // public bool IsEffectResisted()
    // {
    //     double roll = UnityEngine.Random.Range(0f, 100f);
    //     return roll < CurrentTenacity;
    // }
  

}

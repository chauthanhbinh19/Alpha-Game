using UnityEngine;
using System.Collections;
using TMPro;
using System;
using System.Collections.Generic;

public abstract class CardBase : MonoBehaviour
{
    [SerializeField] public string CardName { get; set; }
    [SerializeField] public string Image { get; set; }
    [SerializeField] public string Type { get; set; }
    [SerializeField] public string Rare { get; set; }
    [SerializeField] public double Power { get; set; } = 0;
    //Base
    [SerializeField] public double Health { get; set; } = 0;
    [SerializeField] public double PhysicalAttack { get; set; } = 0;
    [SerializeField] public double PhysicalDefense { get; set; } = 0;
    [SerializeField] public double MagicalAttack { get; set; } = 0;
    [SerializeField] public double MagicalDefense { get; set; } = 0;
    [SerializeField] public double ChemicalAttack { get; set; } = 0;
    [SerializeField] public double ChemicalDefense { get; set; } = 0;
    [SerializeField] public double AtomicAttack { get; set; } = 0;
    [SerializeField] public double AtomicDefense { get; set; } = 0;
    [SerializeField] public double MentalAttack { get; set; } = 0;
    [SerializeField] public double MentalDefense { get; set; } = 0;
    [SerializeField] public double Speed { get; set; } = 0;
    [SerializeField] public double CriticalDamageRate { get; set; } = 0;
    [SerializeField] public double CriticalRate { get; set; } = 0;
    [SerializeField] public double CriticalResistanceRate { get; set; } = 0;
    [SerializeField] public double IgnoreCriticalRate { get; set; } = 0;
    [SerializeField] public double PenetrationRate { get; set; } = 0;
    [SerializeField] public double PenetrationResistanceRate { get; set; } = 0;
    [SerializeField] public double EvasionRate { get; set; } = 0;
    [SerializeField] public double DamageAbsorptionRate { get; set; } = 0;
    [SerializeField] public double IgnoreDamageAbsorptionRate { get; set; } = 0;
    [SerializeField] public double AbsorbedDamageRate { get; set; } = 0;
    [SerializeField] public double VitalityRegenerationRate { get; set; } = 0;
    [SerializeField] public double VitalityRegenerationResistanceRate { get; set; } = 0;
    [SerializeField] public double AccuracyRate { get; set; } = 0;
    [SerializeField] public double LifestealRate { get; set; } = 0;
    [SerializeField] public float Mana { get; set; } = 0;
    [SerializeField] public double ManaRegenerationRate { get; set; } = 0;
    [SerializeField] public double ShieldStrength { get; set; } = 0;
    [SerializeField] public double Tenacity { get; set; } = 0;
    [SerializeField] public double ResistanceRate { get; set; } = 0;
    [SerializeField] public double ComboRate { get; set; } = 0;
    [SerializeField] public double IgnoreComboRate { get; set; } = 0;
    [SerializeField] public double ComboDamageRate { get; set; } = 0;
    [SerializeField] public double ComboResistanceRate { get; set; } = 0;
    [SerializeField] public double StunRate { get; set; } = 0;
    [SerializeField] public double IgnoreStunRate { get; set; } = 0;
    [SerializeField] public double ReflectionRate { get; set; } = 0;
    [SerializeField] public double IgnoreReflectionRate { get; set; } = 0;
    [SerializeField] public double ReflectionDamageRate { get; set; } = 0;
    [SerializeField] public double ReflectionResistanceRate { get; set; } = 0;
    [SerializeField] public double DamageToDifferentFactionRate { get; set; } = 0;
    [SerializeField] public double ResistanceToDifferentFactionRate { get; set; } = 0;
    [SerializeField] public double DamageToSameFactionRate { get; set; } = 0;
    [SerializeField] public double ResistanceToSameFactionRate { get; set; } = 0;
    [SerializeField] public double NormalDamageRate { get; set; } = 0;
    [SerializeField] public double NormalResistanceRate { get; set; } = 0;
    [SerializeField] public double SkillDamageRate { get; set; } = 0;
    [SerializeField] public double SkillResistanceRate { get; set; } = 0;
    //Current
    [SerializeField] public double CurrentHealth { get; set; } = 0;
    [SerializeField] public double CurrentPhysicalAttack { get; set; } = 0;
    [SerializeField] public double CurrentPhysicalDefense { get; set; } = 0;
    [SerializeField] public double CurrentMagicalAttack { get; set; } = 0;
    [SerializeField] public double CurrentMagicalDefense { get; set; } = 0;
    [SerializeField] public double CurrentChemicalAttack { get; set; } = 0;
    [SerializeField] public double CurrentChemicalDefense { get; set; } = 0;
    [SerializeField] public double CurrentAtomicAttack { get; set; } = 0;
    [SerializeField] public double CurrentAtomicDefense { get; set; } = 0;
    [SerializeField] public double CurrentMentalAttack { get; set; } = 0;
    [SerializeField] public double CurrentMentalDefense { get; set; } = 0;
    [SerializeField] public double CurrentSpeed { get; set; } = 0;
    [SerializeField] public double CurrentCriticalDamageRate { get; set; } = 0;
    [SerializeField] public double CurrentCriticalRate { get; set; } = 0;
    [SerializeField] public double CurrentCriticalResistanceRate { get; set; } = 0;
    [SerializeField] public double CurrentIgnoreCriticalRate { get; set; } = 0;
    [SerializeField] public double CurrentPenetrationRate { get; set; } = 0;
    [SerializeField] public double CurrentPenetrationResistanceRate { get; set; } = 0;
    [SerializeField] public double CurrentEvasionRate { get; set; } = 0;
    [SerializeField] public double CurrentDamageAbsorptionRate { get; set; } = 0;
    [SerializeField] public double CurrentIgnoreDamageAbsorptionRate { get; set; } = 0;
    [SerializeField] public double CurrentAbsorbedDamageRate { get; set; } = 0;
    [SerializeField] public double CurrentVitalityRegenerationRate { get; set; } = 0;
    [SerializeField] public double CurrentVitalityRegenerationResistanceRate { get; set; } = 0;
    [SerializeField] public double CurrentAccuracyRate { get; set; } = 0;
    [SerializeField] public double CurrentLifestealRate { get; set; } = 0;
    [SerializeField] public float CurrentMana { get; set; } = 0;
    [SerializeField] public double CurrentManaRegenerationRate { get; set; } = 0;
    [SerializeField] public double CurrentShieldStrength { get; set; } = 0;
    [SerializeField] public double CurrentTenacity { get; set; } = 0;
    [SerializeField] public double CurrentResistanceRate { get; set; } = 0;
    [SerializeField] public double CurrentComboRate { get; set; } = 0;
    [SerializeField] public double CurrentIgnoreComboRate { get; set; } = 0;
    [SerializeField] public double CurrentComboDamageRate { get; set; } = 0;
    [SerializeField] public double CurrentComboResistanceRate { get; set; } = 0;
    [SerializeField] public double CurrentStunRate { get; set; } = 0;
    [SerializeField] public double CurrentIgnoreStunRate { get; set; } = 0;
    [SerializeField] public double CurrentReflectionRate { get; set; } = 0;
    [SerializeField] public double CurrentIgnoreReflectionRate { get; set; } = 0;
    [SerializeField] public double CurrentReflectionDamageRate { get; set; } = 0;
    [SerializeField] public double CurrentReflectionResistanceRate { get; set; } = 0;
    [SerializeField] public double CurrentDamageToDifferentFactionRate { get; set; } = 0;
    [SerializeField] public double CurrentResistanceToDifferentFactionRate { get; set; } = 0;
    [SerializeField] public double CurrentDamageToSameFactionRate { get; set; } = 0;
    [SerializeField] public double CurrentResistanceToSameFactionRate { get; set; } = 0;
    [SerializeField] public double CurrentNormalDamageRate { get; set; } = 0;
    [SerializeField] public double CurrentNormalResistanceRate { get; set; } = 0;
    [SerializeField] public double CurrentSkillDamageRate { get; set; } = 0;
    [SerializeField] public double CurrentSkillResistanceRate { get; set; } = 0;
    [SerializeField] public string Position { get; set; }
    [SerializeField] public int MainPosition { get; set; }
    [SerializeField] public int SubIndex { get; set; }
    public GameObject damagePopupPrefab;
    public HealthBar healthBar;
    public virtual void PerformAction(PlayerController opponent)
    {
        Debug.Log($"{CardName} performs a generic action.");
    }

    public virtual void TakeDamage(double amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (CurrentHealth > 0)
        {
            healthBar.SetHealth(CurrentHealth);
        }
        // Có thể thêm hiệu ứng hoặc sự kiện khi bị tấn công ở đây
        // ShowDamagePopup(amount); // Hiển thị popup sát thương
    }
    protected void ShowDamagePopup(double damage, VertexGradient colorGradient, float popupFactor = 1f)
    {
        if (damagePopupPrefab == null)
        {
            Debug.Log(MessageHelper.PrefabConstants.PrefabIsNull);
            return;
        }

        GameObject newPopup = Instantiate(damagePopupPrefab, transform);

        // Hướng bay lên theo local Y của chính popup (có thể bị xoay bởi billboard)
        Vector3 worldUp = Camera.main.transform.up;
        Vector3 localUp = newPopup.transform.InverseTransformDirection(worldUp);

        // Tính offset cao ban đầu + tăng theo chỉ số popupIndex
        float baseHeight = 0f;
        float offset = baseHeight + popupFactor * 2.5f;

        // Đặt vị trí local khởi tạo theo hướng "up"
        newPopup.transform.localPosition = localUp * offset + new Vector3(
            UnityEngine.Random.Range(-1.0f, 1.0f),
            0f,
            UnityEngine.Random.Range(-1.0f, 1.0f)
        );

        var text = newPopup.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.text = "-" + damage.ToString();
            text.colorGradient = colorGradient;
        }


        StartCoroutine(DamagePopupEffect(newPopup));

    }


    public virtual bool IsAlive()
    {
        return CurrentHealth > 0;
    }
    public IEnumerator DamagePopupEffect(GameObject popup)
    {
        float duration = 1f;
        float elapsed = 0f;

        //Lấy vị trí đã được đẩy lên sau vòng foreach
        Vector3 startPos = popup.transform.localPosition;
        Vector3 upDirection = popup.transform.up;
        Vector3 endPos = startPos + upDirection * 2.5f;

        Vector3 startScale = Vector3.one * 0.07f;
        Vector3 midScale = Vector3.one * 0.07f;
        Vector3 endScale = Vector3.one * 0.07f;

        popup.transform.localScale = startScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            popup.transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            if (t < 0.3f)
                popup.transform.localScale = Vector3.Lerp(startScale, midScale, t / 0.3f);
            else
                popup.transform.localScale = Vector3.Lerp(midScale, endScale, (t - 0.3f) / 0.7f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(popup);
    }

    public void Attack(CardBase enemyCard)
    {
        CausePhysicalAttack(enemyCard);
        CauseMagicalAttack(enemyCard);
        CauseChemicalAttack(enemyCard);
        CauseAtomicAttack(enemyCard);
        CauseMentalAttack(enemyCard);
        Debug.Log(enemyCard.CurrentHealth);
    }

    public void CausePhysicalAttack(CardBase enemyCard)
    {
        double damage = CalculateDamage(PhysicalAttack, enemyCard.PhysicalAttack, enemyCard);
        enemyCard.ShowDamagePopup(damage, ColorConstants.PhysicalGradient, 1f);
        Debug.Log("Physical damage " + damage);
        enemyCard.TakeDamage(damage);
    }

    public void CauseMagicalAttack(CardBase enemyCard)
    {
        double damage = CalculateDamage(MagicalAttack, enemyCard.MagicalAttack, enemyCard);
        enemyCard.ShowDamagePopup(damage, ColorConstants.MagicGradient, 2f);
        Debug.Log("Magical damage " + damage);
        enemyCard.TakeDamage(damage);
    }

    public void CauseChemicalAttack(CardBase enemyCard)
    {
        double damage = CalculateDamage(ChemicalAttack, enemyCard.ChemicalAttack, enemyCard);
        enemyCard.ShowDamagePopup(damage, ColorConstants.ChemicalGradient, 3f);
        Debug.Log("Chemical damage " + damage);
        enemyCard.TakeDamage(damage);
    }

    public void CauseAtomicAttack(CardBase enemyCard)
    {
        double damage = CalculateDamage(AtomicAttack, enemyCard.AtomicAttack, enemyCard);
        enemyCard.ShowDamagePopup(damage, ColorConstants.AtomicGradient, 4f);
        Debug.Log("Atomic damage " + damage);
        enemyCard.TakeDamage(damage);
    }

    public void CauseMentalAttack(CardBase enemyCard)
    {
        double damage = CalculateDamage(MentalAttack, enemyCard.MentalAttack, enemyCard);
        enemyCard.ShowDamagePopup(damage, ColorConstants.MentalGradient, 5f);
        Debug.Log("Mental damage " + damage);
        enemyCard.TakeDamage(damage);
    }

    public double CalculateDamage(double attack, double enemyDefense, CardBase enemyCard)
    {
        // Kiểm tra có đánh trúng không
        // if (!IsAttackHit(enemyCard))
        // {
        //     return 0; // bị né => không gây sát thương
        // }
        // Áp dụng xuyên giáp nếu có
        double effectiveDefense = enemyDefense;
        effectiveDefense = ApplyDefenseWithPenetration(effectiveDefense, enemyCard); // bỏ qua % giáp

        // Công thức tính damage theo tỉ lệ attack / (attack + defense)
        double ratio = attack / (attack + effectiveDefense);
        double baseDamage = attack * ratio * (1 + (QualityEvaluator.CheckQuality(Rare) / 100));
        // Debug.Log("Attack " + attack);
        // Debug.Log("ratio " + ratio);
        // Debug.Log(baseDamage);

        // Áp dụng hiệu ứng khác phe
        baseDamage = ApplyDamageToDifferentFaction(enemyCard, baseDamage);

        // Áp dụng hiệu ứng cùng phe
        baseDamage = ApplyDamageToSameFaction(enemyCard, baseDamage);

        // Áp dụng hiệu ứng khác phe
        baseDamage = ApplyResistanceToDifferentFaction(enemyCard, baseDamage);

        // Áp dụng hiệu ứng cùng phe
        baseDamage = ApplyResistanceToSameFaction(enemyCard, baseDamage);

        // Nếu chí mạng
        if (IsCriticalHit(enemyCard))
        {
            baseDamage = ApplyCriticalDamage(baseDamage, enemyCard);
        }

        // Áp dụng kháng sát thương
        baseDamage = ApplyDamageResistance(baseDamage, enemyCard);
        // Áp dụng hấp thụ sát thương
        baseDamage = ApplyDamageAbsorption(baseDamage, enemyCard);

        return Math.Max(1, Math.Floor(baseDamage)); // không để damage = 0
    }



    public bool IsAttackHit(CardBase enemyCard)
    {
        double chance = CurrentAccuracyRate - enemyCard.CurrentEvasionRate;

        if (chance <= 0)
            return false;

        if (chance >= 100)
            return true;

        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < chance;
    }

    public bool IsCriticalHit(CardBase enemyCard)
    {
        double chance = CurrentCriticalRate - enemyCard.CurrentIgnoreCriticalRate;

        if (chance <= 0)
            return false;

        if (chance >= 100)
            return true;

        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < chance;
    }

    public double ApplyCriticalDamage(double baseDamage, CardBase enemyCard)
    {
        // Tính multiplier (hệ số nhân sát thương chí mạng)
        double multiplier = 1.0 + (CriticalDamageRate / 100.0) - (enemyCard.CriticalResistanceRate / 100.0);

        // Đảm bảo không giảm dưới sát thương cơ bản
        if (multiplier < 1.0)
            multiplier = 1.0;

        return baseDamage * multiplier;
    }

    public double ApplyDefenseWithPenetration(double enemyDefense, CardBase enemyCard)
    {
        // Phần trăm xuyên giáp sau khi bị kháng
        double actualPenetration = CurrentPenetrationRate - enemyCard.CurrentPenetrationResistanceRate;
        if (actualPenetration < 0)
            actualPenetration = 0;

        // Tính defense sau khi xuyên
        double effectiveDefense = enemyDefense * (1 - actualPenetration / 100.0);

        // Không để defense âm
        return Math.Max(0, effectiveDefense);
    }

    public double ApplyDamageAbsorption(double incomingDamage, CardBase enemyCard)
    {
        // Giảm khả năng hấp thụ của enemy do bị xuyên
        double effectiveAbsorptionRate = CurrentDamageAbsorptionRate * (1 - enemyCard.CurrentIgnoreDamageAbsorptionRate / 100.0);

        // Đảm bảo không âm
        if (effectiveAbsorptionRate < 0)
            effectiveAbsorptionRate = 0;

        // Áp dụng công thức bão hòa
        double absorptionFactor = 1.0 / (1.0 + effectiveAbsorptionRate / 100.0);

        // Damage bị hấp thụ
        double absorbedDamage = incomingDamage * (1 - absorptionFactor);

        // Không để âm
        return Math.Max(0, incomingDamage - absorbedDamage);
    }

    public void RegenerateVitality()
    {
        double regenAmount = Health * (CurrentVitalityRegenerationRate / 100.0);

        CurrentHealth += regenAmount;

        // Giới hạn không vượt quá máu tối đa
        if (CurrentHealth > Health)
        {
            CurrentHealth = Health;
        }
    }

    public double ApplyLifesteal(double damageDealt)
    {
        double effectiveLifestealRate = Math.Min(CurrentLifestealRate, 100.0); // Giới hạn 100%
        double healAmount = damageDealt * effectiveLifestealRate;
        return healAmount;
    }

    public void RegenerateMana()
    {
        float regenAmount = (float)(CurrentMana * (CurrentManaRegenerationRate / 100.0));

        CurrentMana += regenAmount;

        // Giới hạn mana không vượt quá tối đa
        // Giả sử bạn có biến manaMax để lưu mana tối đa, nếu chưa có thì có thể thêm hoặc lấy từ đâu đó
        float manaMax = 100f; // ví dụ tạm, bạn thay bằng giá trị thực tế

        if (CurrentMana > manaMax)
        {
            CurrentMana = manaMax;
        }
    }

    public bool IsEffectResisted()
    {
        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < CurrentTenacity;
    }

    public double ApplyDamageResistance(double incomingDamage, CardBase enemyCard)
    {
        // Áp dụng công thức bão hòa cho kháng cao
        double resistanceFactor = 1.0 / (1.0 + enemyCard.ResistanceRate / 100.0);
        double reducedDamage = incomingDamage * resistanceFactor;
        return Math.Max(0, reducedDamage);
    }

    public bool IsComboHit(CardBase enemyCard)
    {
        double chance = CurrentComboRate - enemyCard.CurrentIgnoreComboRate;

        if (chance <= 0)
            return false;

        if (chance >= 100)
            return true;

        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < chance;
    }

    public double ApplyComboDamage(double baseDamage, CardBase enemyCard)
    {
        // Tính multiplier (hệ số nhân sát thương chí mạng)
        double multiplier = 1.0 + (CurrentComboDamageRate / 100.0) - (enemyCard.CurrentComboResistanceRate / 100.0);

        // Đảm bảo không giảm dưới sát thương cơ bản
        if (multiplier < 1.0)
            multiplier = 1.0;

        return baseDamage * multiplier;
    }

    public bool IsStunHit(CardBase enemyCard)
    {
        double chance = CurrentStunRate - enemyCard.CurrentIgnoreStunRate;

        if (chance <= 0)
            return false;

        if (chance >= 100)
            return true;

        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < chance;
    }
    
    public bool IsReflectionHit(CardBase enemyCard)
    {
        double chance = CurrentReflectionRate - enemyCard.CurrentIgnoreReflectionRate;

        if (chance <= 0)
            return false;

        if (chance >= 100)
            return true;

        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < chance;
    }

    public double ApplyReflectionDamage(double baseDamage, CardBase enemyCard)
    {
        // Tính multiplier (hệ số nhân sát thương chí mạng)
        double multiplier = 1.0 + (CurrentReflectionDamageRate / 100.0) - (enemyCard.CurrentReflectionResistanceRate / 100.0);

        // Đảm bảo không giảm dưới sát thương cơ bản
        if (multiplier < 1.0)
            multiplier = 1.0;

        return baseDamage * multiplier;
    }

    public double ApplyDamageToDifferentFaction(CardBase enemyCard, double baseDamage)
    {
        double damage = baseDamage;

        if (!this.Type.Equals(enemyCard.Type))
        {
            // Tăng sát thương theo tỉ lệ phần trăm
            damage += baseDamage * (CurrentDamageToDifferentFactionRate / 100.0);
        }

        return damage;
    }

    public double ApplyResistanceToDifferentFaction(CardBase enemyCard, double incomingDamage)
    {
        if (!this.Type.Equals(enemyCard.Type))
        {
            // Kháng theo tỉ lệ phần trăm
            if (CurrentResistanceToDifferentFactionRate < 100)
            {
                double reducedDamage = incomingDamage * (1 - CurrentResistanceToDifferentFactionRate / 100.0);
                return reducedDamage;
            }
            else
            {
                double reducedDamage = incomingDamage / (CurrentResistanceToDifferentFactionRate / 100.0);
                return reducedDamage;
            }

        }

        // Nếu cùng phe, không kháng thêm
        return incomingDamage;
    }

    public double ApplyDamageToSameFaction(CardBase enemyCard, double baseDamage)
    {
        double damage = baseDamage;

        if (this.Type.Equals(enemyCard.Type))
        {
            // Tăng sát thương theo tỉ lệ phần trăm
            damage += baseDamage * (CurrentDamageToSameFactionRate / 100.0);
        }

        return damage;
    }

    public double ApplyResistanceToSameFaction(CardBase enemy, double incomingDamage)
    {
        if (this.Type.Equals(enemy.Type))
        {
            // Kháng theo tỉ lệ phần trăm
            if (CurrentResistanceToSameFactionRate < 100)
            {
                double reducedDamage = incomingDamage * (1 - CurrentResistanceToSameFactionRate / 100.0);
                return reducedDamage;
            }
            else
            {
                double reducedDamage = incomingDamage / (CurrentResistanceToSameFactionRate / 100.0);
                return reducedDamage;
            }

        }

        // Nếu cùng phe, không kháng thêm
        return incomingDamage;
    }

}

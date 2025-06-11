using UnityEngine;
using System.Collections;
using TMPro;
using System;
using System.Collections.Generic;

public class CardBase : MonoBehaviour, IAttack
{
    private string cardName;
    private string type;
    private double power;
    private double health;
    private double physical_attack;
    private double physical_defense;
    private double magical_attack;
    private double magical_defense;
    private double chemical_attack;
    private double chemical_defense;
    private double atomic_attack;
    private double atomic_defense;
    private double mental_attack;
    private double mental_defense;
    private double speed;
    private double critical_damage_rate;
    private double critical_rate;
    private double penetration_rate;
    private double evasion_rate;
    private double damage_absorption_rate;
    private double vitality_regeneration_rate;
    private double accuracy_rate;
    private double lifesteal_rate;
    private float mana;
    private double mana_regeneration_rate;
    private double shield_strength;
    private double tenacity;
    private double resistance_rate;
    private double combo_rate;
    private double reflection_rate;
    private double damage_to_different_faction_rate;
    private double resistance_to_different_faction_rate;
    private double damage_to_same_faction_rate;
    private double resistance_to_same_faction_rate;
    private double currentHealth;
    private string position;
    public GameObject damagePopupPrefab; // Gán prefab này trong Inspector
    public HealthBar healthBar;

    public string CardName { get => cardName; set => cardName = value; }
    public string Type { get => type; set => type = value; }
    public double Power { get => power; set => power = value; }
    public double Health { get => health; set => health = value; }
    public double Physical_attack { get => physical_attack; set => physical_attack = value; }
    public double Physical_defense { get => physical_defense; set => physical_defense = value; }
    public double Magical_attack { get => magical_attack; set => magical_attack = value; }
    public double Magical_defense { get => magical_defense; set => magical_defense = value; }
    public double Chemical_attack { get => chemical_attack; set => chemical_attack = value; }
    public double Chemical_defense { get => chemical_defense; set => chemical_defense = value; }
    public double Atomic_attack { get => atomic_attack; set => atomic_attack = value; }
    public double Atomic_defense { get => atomic_defense; set => atomic_defense = value; }
    public double Mental_attack { get => mental_attack; set => mental_attack = value; }
    public double Mental_defense { get => mental_defense; set => mental_defense = value; }
    public double Speed { get => speed; set => speed = value; }
    public double Critical_damage_rate { get => critical_damage_rate; set => critical_damage_rate = value; }
    public double Critical_rate { get => critical_rate; set => critical_rate = value; }
    public double Penetration_rate { get => penetration_rate; set => penetration_rate = value; }
    public double Evasion_rate { get => evasion_rate; set => evasion_rate = value; }
    public double Damage_absorption_rate { get => damage_absorption_rate; set => damage_absorption_rate = value; }
    public double Vitality_regeneration_rate { get => vitality_regeneration_rate; set => vitality_regeneration_rate = value; }
    public double Accuracy_rate { get => accuracy_rate; set => accuracy_rate = value; }
    public double Lifesteal_rate { get => lifesteal_rate; set => lifesteal_rate = value; }
    public float Mana { get => mana; set => mana = value; }
    public double Mana_regeneration_rate { get => mana_regeneration_rate; set => mana_regeneration_rate = value; }
    public double Shield_strength { get => shield_strength; set => shield_strength = value; }
    public double Tenacity { get => tenacity; set => tenacity = value; }
    public double Resistance_rate { get => resistance_rate; set => resistance_rate = value; }
    public double Combo_rate { get => combo_rate; set => combo_rate = value; }
    public double Reflection_rate { get => reflection_rate; set => reflection_rate = value; }
    public double Damage_to_different_faction_rate { get => damage_to_different_faction_rate; set => damage_to_different_faction_rate = value; }
    public double Resistance_to_different_faction_rate { get => resistance_to_different_faction_rate; set => resistance_to_different_faction_rate = value; }
    public double Damage_to_same_faction_rate { get => damage_to_same_faction_rate; set => damage_to_same_faction_rate = value; }
    public double Resistance_to_same_faction_rate { get => resistance_to_same_faction_rate; set => resistance_to_same_faction_rate = value; }
    public double CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public string Position { get => position; set => position = value; }

    public virtual void TakeDamage(double amount)
    {
        // Debug.Log("TakeDamage called: " + amount);
        CurrentHealth -= amount;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (CurrentHealth > 0)
        {
            healthBar.SetHealth(CurrentHealth); // Cập nhật thanh máu
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
        return Health > 0;
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
        PhysicalAttack(enemyCard);
        MagicalAttack(enemyCard);
        ChemicalAttack(enemyCard);
        AtomicAttack(enemyCard);
        MentalAttack(enemyCard);
        Debug.Log(enemyCard.CurrentHealth);
    }

    public double CalculateDamage(double attack, double enemyDefense, CardBase enemyCard)
    {
        // Kiểm tra có đánh trúng không
        if (!IsAttackHit(enemyCard))
        {
            return 0; // bị né => không gây sát thương
        }
        // Áp dụng xuyên giáp nếu có
        double effectiveDefense = enemyDefense;
        effectiveDefense = CalculateDefenseWithPenetration(effectiveDefense); // bỏ qua % giáp

        // Công thức tính damage theo tỉ lệ attack / (attack + defense)
        double ratio = attack / (attack + effectiveDefense);
        double baseDamage = attack * ratio;
        // Debug.Log("Attack " + attack);
        // Debug.Log("ratio " + ratio);
        // Debug.Log(baseDamage);

        // Áp dụng hiệu ứng khác phe
        baseDamage = CalculateDamageToDifferentFaction(enemyCard, baseDamage);

        // Áp dụng hiệu ứng cùng phe
        baseDamage = CalculateDamageToSameFaction(enemyCard, baseDamage);

        // Áp dụng hiệu ứng khác phe
        baseDamage = ApplyResistanceToDifferentFaction(enemyCard, baseDamage);

        // Áp dụng hiệu ứng cùng phe
        baseDamage = ApplyResistanceToSameFaction(enemyCard, baseDamage);

        // Nếu chí mạng
        if (IsCriticalHit())
        {
            baseDamage = CalculateCriticalDamage(baseDamage);
        }

        // Áp dụng kháng sát thương
        baseDamage = enemyCard.ApplyDamageResistance(baseDamage);
        // Áp dụng hấp thụ sát thương
        baseDamage = enemyCard.ApplyDamageAbsorption(baseDamage);

        return Math.Max(1, Math.Floor(baseDamage)); // không để damage = 0
    }

    public void PhysicalAttack(CardBase enemyCard)
    {
        double damage = CalculateDamage(Physical_attack, enemyCard.Physical_defense, enemyCard);
        enemyCard.ShowDamagePopup(damage, ColorConstanst.PhysicalGradient, 1f);
        Debug.Log("Physical damage " + damage);
        enemyCard.TakeDamage(damage);
    }

    public void MagicalAttack(CardBase enemyCard)
    {
        double damage = CalculateDamage(Magical_attack, enemyCard.Magical_attack, enemyCard);
        enemyCard.ShowDamagePopup(damage, ColorConstanst.MagicGradient, 2f);
        Debug.Log("Magical damage " + damage);
        enemyCard.TakeDamage(damage);
    }

    public void ChemicalAttack(CardBase enemyCard)
    {
        double damage = CalculateDamage(Chemical_attack, enemyCard.Chemical_attack, enemyCard);
        enemyCard.ShowDamagePopup(damage, ColorConstanst.ChemicalGradient, 3f);
        Debug.Log("Chemical damage " + damage);
        enemyCard.TakeDamage(damage);
    }

    public void AtomicAttack(CardBase enemyCard)
    {
        double damage = CalculateDamage(Atomic_attack, enemyCard.Atomic_attack, enemyCard);
        enemyCard.ShowDamagePopup(damage, ColorConstanst.AtomicGradient, 4f);
        Debug.Log("Atomic damage " + damage);
        enemyCard.TakeDamage(damage);
    }

    public void MentalAttack(CardBase enemyCard)
    {
        double damage = CalculateDamage(Mental_attack, enemyCard.Mental_attack, enemyCard);
        enemyCard.ShowDamagePopup(damage, ColorConstanst.MentalGradient, 5f);
        Debug.Log("Mental damage " + damage);
        enemyCard.TakeDamage(damage);
    }

    public bool IsAttackHit(CardBase enemy)
    {
        double hitChance = Accuracy_rate / (Accuracy_rate + enemy.Evasion_rate) * 100;
        double roll = UnityEngine.Random.Range(0f, 100f);
        // return roll < hitChance;
        return true;
    }

    public bool IsCriticalHit()
    {
        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll <= Critical_rate;
    }

    public double CalculateCriticalDamage(double baseDamage)
    {
        double multiplier = 1.0 + (Critical_damage_rate / 100.0);
        return baseDamage * multiplier;
    }

    public double CalculateDefenseWithPenetration(double enemyDefense)
    {
        double effectiveDefense = enemyDefense / (Penetration_rate * 100);
        return Math.Max(0, effectiveDefense); // Không âm damage
    }

    public double ApplyDamageAbsorption(double incomingDamage)
    {
        // Áp dụng công thức bão hòa cho kháng cao
        double damageAbsorptionFactor = 1.0 / (1.0 + Damage_absorption_rate / 100.0);
        double reducedDamage = incomingDamage - incomingDamage * damageAbsorptionFactor;
        return Math.Max(0, reducedDamage);
    }

    public void RegenerateVitality()
    {
        double regenAmount = health * (Vitality_regeneration_rate / 100.0);

        currentHealth += regenAmount;

        // Giới hạn không vượt quá máu tối đa
        if (currentHealth > health)
        {
            currentHealth = health;
        }
    }

    public void ApplyLifesteal(double damageDealt)
    {
        double healAmount = damageDealt * Lifesteal_rate / 100.0;
        currentHealth += healAmount;
    }

    public void RegenerateMana()
    {
        float regenAmount = (float)(mana * (Mana_regeneration_rate / 100.0));

        mana += regenAmount;

        // Giới hạn mana không vượt quá tối đa
        // Giả sử bạn có biến manaMax để lưu mana tối đa, nếu chưa có thì có thể thêm hoặc lấy từ đâu đó
        float manaMax = 100f; // ví dụ tạm, bạn thay bằng giá trị thực tế

        if (mana > manaMax)
        {
            mana = manaMax;
        }
    }

    public bool IsEffectResisted()
    {
        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < Tenacity;
    }

    public double ApplyDamageResistance(double incomingDamage)
    {
        // Áp dụng công thức bão hòa cho kháng cao
        double resistanceFactor = 1.0 / (1.0 + Resistance_rate / 100.0);
        double reducedDamage = incomingDamage * resistanceFactor;
        return Math.Max(0, reducedDamage);
    }

    public bool ApplyPerformCombo()
    {
        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < Combo_rate;
    }

    public bool ApplyReflectAttack()
    {
        double roll = UnityEngine.Random.Range(0f, 100f);
        return roll < Reflection_rate;
    }

    public double CalculateDamageToDifferentFaction(CardBase enemy, double baseDamage)
    {
        double damage = baseDamage;

        if (!this.type.Equals(enemy.type))
        {
            // Tăng sát thương theo tỉ lệ phần trăm
            damage += baseDamage * (Damage_to_different_faction_rate / 100.0);
        }

        return damage;
    }

    public double ApplyResistanceToDifferentFaction(CardBase enemy, double incomingDamage)
    {
        if (!this.type.Equals(enemy.type))
        {
            // Kháng theo tỉ lệ phần trăm
            if (Resistance_to_different_faction_rate < 100)
            {
                double reducedDamage = incomingDamage * (1 - Resistance_to_different_faction_rate / 100.0);
                return reducedDamage;
            }
            else
            {
                double reducedDamage = incomingDamage / (Resistance_to_different_faction_rate / 100.0);
                return reducedDamage;
            }

        }

        // Nếu cùng phe, không kháng thêm
        return incomingDamage;
    }

    public double CalculateDamageToSameFaction(CardBase enemy, double baseDamage)
    {
        double damage = baseDamage;

        if (this.type.Equals(enemy.type))
        {
            // Tăng sát thương theo tỉ lệ phần trăm
            damage += baseDamage * (Damage_to_different_faction_rate / 100.0);
        }

        return damage;
    }

    public double ApplyResistanceToSameFaction(CardBase enemy, double incomingDamage)
    {
        if (this.type.Equals(enemy.type))
        {
            // Kháng theo tỉ lệ phần trăm
            if (Resistance_to_different_faction_rate < 100)
            {
                double reducedDamage = incomingDamage * (1 - Resistance_to_different_faction_rate / 100.0);
                return reducedDamage;
            }
            else
            {
                double reducedDamage = incomingDamage / (Resistance_to_different_faction_rate / 100.0);
                return reducedDamage;
            }

        }

        // Nếu cùng phe, không kháng thêm
        return incomingDamage;
    }

}

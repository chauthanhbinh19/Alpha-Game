using UnityEngine;
using System.Collections;
using TMPro;

public class CardBase : MonoBehaviour
{
    public string name;
    public string type;
    public double power;
    public double health;
    public double physical_attack;
    public double physical_defense;
    public double magical_attack;
    public double magical_defense;
    public double chemical_attack;
    public double chemical_defense;
    public double atomic_attack;
    public double atomic_defense;
    public double mental_attack;
    public double mental_defense;
    public double speed;
    public double critical_damage_rate;
    public double critical_rate;
    public double penetration_rate;
    public double evasion_rate;
    public double damage_absorption_rate;
    public double vitality_regeneration_rate;
    public double accuracy_rate;
    public double lifesteal_rate;
    public float mana;
    public double mana_regeneration_rate;
    public double shield_strength;
    public double tenacity;
    public double resistance_rate;
    public double combo_rate;
    public double reflection_rate;
    public double damage_to_different_faction_rate;
    public double resistance_to_different_faction_rate;
    public double damage_to_same_faction_rate;
    public double resistance_to_same_faction_rate;
    public double currentHealth;
    public string position;
    public GameObject damagePopupPrefab; // Gán prefab này trong Inspector
    public HealthBar healthBar;
    public static VertexGradient PhysicalGradient = new VertexGradient(
        new Color32(255, 99, 71, 255),   // Top Left - Tomato
        new Color32(255, 69, 0, 255),    // Top Right - OrangeRed
        new Color32(139, 0, 0, 255),     // Bottom Left - DarkRed
        new Color32(178, 34, 34, 255)    // Bottom Right - FireBrick
    );

    public static VertexGradient MagicGradient = new VertexGradient(
        new Color32(135, 206, 250, 255), // Top Left - LightSkyBlue
        new Color32(30, 144, 255, 255),  // Top Right - DodgerBlue
        new Color32(65, 105, 225, 255),  // Bottom Left - RoyalBlue
        new Color32(138, 43, 226, 255)   // Bottom Right - BlueViolet
    );

    public static VertexGradient ChemicalGradient = new VertexGradient(
        new Color32(255, 215, 0, 255),   // Top Left - Gold
        new Color32(255, 165, 0, 255),   // Top Right - Orange
        new Color32(255, 140, 0, 255),   // Bottom Left - DarkOrange
        new Color32(255, 127, 80, 255)   // Bottom Right - Coral
    );

    public static VertexGradient AtomicGradient = new VertexGradient(
        new Color32(218, 112, 214, 255), // Top Left - Orchid
        new Color32(186, 85, 211, 255),  // Top Right - MediumOrchid
        new Color32(139, 0, 139, 255),   // Bottom Left - DarkMagenta
        new Color32(148, 0, 211, 255)    // Bottom Right - DarkViolet
    );

    public static VertexGradient MentalGradient = new VertexGradient(
        new Color32(211, 211, 211, 255), // Top Left - LightGray
        new Color32(169, 169, 169, 255), // Top Right - DarkGray
        new Color32(128, 128, 128, 255), // Bottom Left - Gray
        new Color32(80, 80, 80, 255)     // Bottom Right - Charcoal
    );

    public virtual void TakeDamage(double amount)
    {
        // Debug.Log("TakeDamage called: " + amount);
        health -= amount;
        if (health < 0) health = 0;
        if (healthBar != null)
        {
            Debug.Log("Health bar found, updating health.");
            healthBar.SetHealth(health); // Cập nhật thanh máu
        }
        // Có thể thêm hiệu ứng hoặc sự kiện khi bị tấn công ở đây
        ShowDamagePopup(amount); // Hiển thị popup sát thương
    }

    protected void ShowDamagePopup(double amount)
    {
        if (damagePopupPrefab != null)
        {
            // Tạo popup làm con của cardInstance (chính là this.transform)
            GameObject popup = Instantiate(damagePopupPrefab, transform);
            popup.transform.localPosition = new Vector3(0, 1.5f, 0); // Điều chỉnh vị trí nổi phía trên card

            TextMeshProUGUI text = popup.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = "-" + amount.ToString();
                text.colorGradient = MagicGradient; // Gán gradient màu cho text
            }
            StartCoroutine(DamagePopupEffect(popup));
        }
        else
        {
            Debug.Log("Damage popup prefab is not assigned in the inspector!");
        }
    }

    public virtual bool IsAlive()
    {
        return health > 0;
    }
    public IEnumerator DamagePopupEffect(GameObject popup)
    {
        float duration = 1f;
        float elapsed = 0f;
        Vector3 startPos = popup.transform.localPosition;
        Vector3 endPos = startPos + new Vector3(0, 2.5f, 0);
        Vector3 startScale = Vector3.one * 0.07f;
        Vector3 midScale = Vector3.one * 0.07f;
        Vector3 endScale = Vector3.one * 0.07f;

        popup.transform.localScale = startScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // Bay lên
            popup.transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            // Scale to ra rồi nhỏ lại (dùng parabol)
            if (t < 0.3f)
                popup.transform.localScale = Vector3.Lerp(startScale, midScale, t / 0.3f);
            else
                popup.transform.localScale = Vector3.Lerp(midScale, endScale, (t - 0.3f) / 0.7f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(popup);
    }
}

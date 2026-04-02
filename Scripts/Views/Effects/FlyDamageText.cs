using System;
using System.Collections;
using TMPro;
using UnityEngine;

public enum DamageTextType
{
    Physical,
    Magical,
    Chemical,
    Atomic,
    Mental,
    Default
}

public class FlyDamageText : MonoBehaviour
{
    [Header("Text Settings")]
    public TextMeshProUGUI damageText;
    public DamageTextType damageTextType = DamageTextType.Physical;
    public bool isHealing;

    [Header("Animation Settings")]
    public float duration = 1f;
    public float floatDistance = 2.5f;
    public float popupFactor = 1f;
    public Vector2 randomRange = new Vector2(0.5f, 0.5f);

    private void Awake()
    {
        if (damageText == null)
        {
            damageText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void Init(double damage, DamageTextType type, bool healing = false, float factor = 1f)
    {
        if (damageText == null)
        {
            damageText = GetComponentInChildren<TextMeshProUGUI>();
            if (damageText == null)
            {
                Debug.LogWarning("FlyDamageText: no TextMeshProUGUI found in children.");
                return;
            }
        }

        damageTextType = type;
        isHealing = healing;
        popupFactor = factor;

        damageText.text = FormatDamage(damage, healing);
        damageText.colorGradient = GetGradientForType(type);
        damageText.alpha = 1f;

        SetupStartPosition();
        StartCoroutine(AnimatePopup());
    }

    public static VertexGradient GetGradientForType(DamageTextType type)
    {
        return type switch
        {
            DamageTextType.Physical => ColorConstants.PHYSICAL_GRADIENT_COLOR,
            DamageTextType.Magical => ColorConstants.MAGICAL_GRADIENT_COLOR,
            DamageTextType.Chemical => ColorConstants.CHEMICAL_GRADIENT_COLOR,
            DamageTextType.Atomic => ColorConstants.ATOMIC_GRADIENT_COLOR,
            DamageTextType.Mental => ColorConstants.MENTAL_GRADIENT_COLOR,
            _ => new VertexGradient(Color.white, Color.white, Color.white, Color.white),
        };
    }

    public static string FormatDamage(double damage, bool healing = false)
    {
        string sign = healing ? "+" : "-";
        return damage switch
        {
            0 => "0",
            _ => $"{sign}{Math.Abs(damage)}"
        };
    }

    private void SetupStartPosition()
    {
        Vector3 worldUp = Camera.main != null ? Camera.main.transform.up : Vector3.up;
        Vector3 parentSpaceUp = transform.parent != null ? transform.parent.InverseTransformDirection(worldUp) : worldUp;
        Vector3 randomOffset = new Vector3(
            UnityEngine.Random.Range(-randomRange.x, randomRange.x),
            0f,
            UnityEngine.Random.Range(-randomRange.y, randomRange.y)
        );

        float height = 0.5f + popupFactor * floatDistance * 0.25f;
        transform.localPosition = parentSpaceUp * height + randomOffset;
    }

    private IEnumerator AnimatePopup()
    {
        float elapsed = 0f;
        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = startPosition + Vector3.up * floatDistance;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);

            if (damageText != null)
            {
                damageText.alpha = Mathf.Lerp(1f, 0f, t);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}

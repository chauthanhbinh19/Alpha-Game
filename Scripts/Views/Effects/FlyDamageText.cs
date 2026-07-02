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
    public TextMeshProUGUI DamageText;
    public DamageTextType DamageTextType = DamageTextType.Physical;
    public bool IsHealing;

    [Header("Animation Settings")]
    public float Duration = 1f;
    public float FloatDistance = 2.5f;
    public float PopupFactor = 1f;
    public Vector2 RandomRange = new Vector2(0.5f, 0.5f);

    private void Awake()
    {
        if (DamageText == null)
        {
            DamageText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void Init(double damage, DamageTextType type, bool healing = false, float factor = 1f)
    {
        if (DamageText == null)
        {
            DamageText = GetComponentInChildren<TextMeshProUGUI>();
            if (DamageText == null)
            {
                Debug.LogWarning("FlyDamageText: no TextMeshProUGUI found in children.");
                return;
            }
        }

        DamageTextType = type;
        IsHealing = healing;
        PopupFactor = factor;

        DamageText.text = FormatDamage(damage, healing);
        DamageText.colorGradient = GetGradientForType(type);
        DamageText.alpha = 1f;

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
            UnityEngine.Random.Range(-RandomRange.x, RandomRange.x),
            0f,
            UnityEngine.Random.Range(-RandomRange.y, RandomRange.y)
        );

        float height = 0.5f + PopupFactor * FloatDistance * 0.25f;
        transform.localPosition = parentSpaceUp * height + randomOffset;
    }

    private IEnumerator AnimatePopup()
    {
        float elapsed = 0f;
        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = startPosition + Vector3.up * FloatDistance;

        while (elapsed < Duration)
        {
            float t = elapsed / Duration;
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);

            if (DamageText != null)
            {
                DamageText.alpha = Mathf.Lerp(1f, 0f, t);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}

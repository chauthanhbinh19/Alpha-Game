using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }
    private Transform MainPanel;
    public GameObject PopupStatsPanelPrefab;
    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        PopupStatsPanelPrefab = UIManager.Instance.Get("PopupStatsPanelPrefab");
    }
    public void CreateStatsManager(IStats stat)
    {
        GameObject gameObject = Instantiate(PopupStatsPanelPrefab, MainPanel);
        Transform transform = gameObject.transform;
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(gameObject);
        });
        TextMeshProUGUI titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.STATS);

        SetupStat(transform, "Power", AppConstants.StatFields.POWER, AppDisplayConstants.StatFieldsShort.POWER, stat.Power);
        SetupStat(transform, "Health", AppConstants.StatFields.HEALTH, AppDisplayConstants.StatFieldsShort.HEALTH, stat.Health);
        SetupStat(transform, "PhysicalAttack", AppConstants.StatFields.PHYSICAL_ATTACK, AppDisplayConstants.StatFieldsShort.PHYSICAL_ATTACK, stat.PhysicalAttack);
        SetupStat(transform, "PhysicalDefense", AppConstants.StatFields.PHYSICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.PHYSICAL_DEFENSE, stat.PhysicalDefense);
        SetupStat(transform, "MagicalAttack", AppConstants.StatFields.MAGICAL_ATTACK, AppDisplayConstants.StatFieldsShort.MAGICAL_ATTACK, stat.MagicalAttack);
        SetupStat(transform, "MagicalDefense", AppConstants.StatFields.MAGICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MAGICAL_DEFENSE, stat.MagicalDefense);
        SetupStat(transform, "ChemicalAttack", AppConstants.StatFields.CHEMICAL_ATTACK, AppDisplayConstants.StatFieldsShort.CHEMICAL_ATTACK, stat.ChemicalAttack);
        SetupStat(transform, "ChemicalDefense", AppConstants.StatFields.CHEMICAL_DEFENSE, AppDisplayConstants.StatFieldsShort.CHEMICAL_DEFENSE, stat.ChemicalDefense);
        SetupStat(transform, "AtomicAttack", AppConstants.StatFields.ATOMIC_ATTACK, AppDisplayConstants.StatFieldsShort.ATOMIC_ATTACK, stat.AtomicAttack);
        SetupStat(transform, "AtomicDefense", AppConstants.StatFields.ATOMIC_DEFENSE, AppDisplayConstants.StatFieldsShort.ATOMIC_DEFENSE, stat.AtomicDefense);
        SetupStat(transform, "MentalAttack", AppConstants.StatFields.MENTAL_ATTACK, AppDisplayConstants.StatFieldsShort.MENTAL_ATTACK, stat.MentalAttack);
        SetupStat(transform, "MentalDefense", AppConstants.StatFields.MENTAL_DEFENSE, AppDisplayConstants.StatFieldsShort.MENTAL_DEFENSE, stat.MentalDefense);
        SetupStat(transform, "Speed", AppConstants.StatFields.SPEED, AppDisplayConstants.StatFieldsShort.SPEED, stat.Speed);
        SetupStat(transform, "CriticalRate", AppConstants.StatFields.CRITICAL_RATE, AppDisplayConstants.StatFieldsShort.CRITICAL_RATE, stat.CriticalRate, true);
        SetupStat(transform, "CriticalDamageRate", AppConstants.StatFields.CRITICAL_DAMAGE_RATE, AppDisplayConstants.StatFieldsShort.CRITICAL_DAMAGE_RATE, stat.CriticalDamageRate, true);
        SetupStat(transform, "CriticalResistanceRate", AppConstants.StatFields.CRITICAL_RESISTANCE_RATE, AppDisplayConstants.StatFieldsShort.CRITICAL_RESISTANCE_RATE, stat.CriticalResistanceRate, true);
        SetupStat(transform, "IgnoreCriticalRate", AppConstants.StatFields.IGNORE_CRITICAL_RATE, AppDisplayConstants.StatFieldsShort.IGNORE_CRITICAL_RATE, stat.IgnoreCriticalRate, true);
        SetupStat(transform, "PenetrationRate", AppConstants.StatFields.PENETRATION_RATE, AppDisplayConstants.StatFieldsShort.PENETRATION_RATE, stat.PenetrationRate, true);
        SetupStat(transform, "PenetrationResistanceRate", AppConstants.StatFields.PENETRATION_RESISTANCE_RATE, AppDisplayConstants.StatFieldsShort.PENETRATION_RESISTANCE_RATE, stat.PenetrationResistanceRate, true);
        SetupStat(transform, "EvasionRate", AppConstants.StatFields.EVASION_RATE, AppDisplayConstants.StatFieldsShort.EVASION_RATE, stat.EvasionRate, true);
        SetupStat(transform, "DamageAbsorptionRate", AppConstants.StatFields.DAMAGE_ABSORPTION_RATE, AppDisplayConstants.StatFieldsShort.DAMAGE_ABSORPTION_RATE, stat.DamageAbsorptionRate, true);
        SetupStat(transform, "IgnoreDamageAbsorptionRate", AppConstants.StatFields.IGNORE_DAMAGE_ABSORPTION_RATE, AppDisplayConstants.StatFieldsShort.IGNORE_DAMAGE_ABSORPTION_RATE, stat.IgnoreDamageAbsorptionRate, true);
        SetupStat(transform, "AbsorbedDamageRate", AppConstants.StatFields.ABSORBED_DAMAGE_RATE, AppDisplayConstants.StatFieldsShort.ABSORBED_DAMAGE_RATE, stat.AbsorbedDamageRate, true);
        SetupStat(transform, "VitalityRegenerationRate", AppConstants.StatFields.VITALITY_REGENERATION_RATE, AppDisplayConstants.StatFieldsShort.VITALITY_REGENERATION_RATE, stat.VitalityRegenerationRate, true);
        SetupStat(transform, "VitalityRegenerationResistanceRate", AppConstants.StatFields.VITALITY_REGENERATION_RESISTANCE_RATE, AppDisplayConstants.StatFieldsShort.VITALITY_REGENERATION_RESISTANCE_RATE, stat.VitalityRegenerationResistanceRate, true);
        SetupStat(transform, "AccuracyRate", AppConstants.StatFields.ACCURACY_RATE, AppDisplayConstants.StatFieldsShort.ACCURACY_RATE, stat.AccuracyRate, true);
        SetupStat(transform, "LifestealRate", AppConstants.StatFields.LIFE_STEAL_RATE, AppDisplayConstants.StatFieldsShort.LIFE_STEAL_RATE, stat.LifestealRate, true);
        SetupStat(transform, "Mana", AppConstants.StatFields.MANA, AppDisplayConstants.StatFieldsShort.MANA, stat.Mana);
        SetupStat(transform, "ManaRegenerationRate", AppConstants.StatFields.MANA_REGENERATION_RATE, AppDisplayConstants.StatFieldsShort.MANA_REGENERATION_RATE, stat.ManaRegenerationRate, true);
        SetupStat(transform, "ShieldStrength", AppConstants.StatFields.SHIELD_STRENGTH, AppDisplayConstants.StatFieldsShort.SHIELD_STRENGTH, stat.ShieldStrength);
        SetupStat(transform, "Tenacity", AppConstants.StatFields.TENACITY, AppDisplayConstants.StatFieldsShort.TENACITY, stat.Tenacity);
        SetupStat(transform, "ResistanceRate", AppConstants.StatFields.RESISTANCE_RATE, AppDisplayConstants.StatFieldsShort.RESISTANCE_RATE, stat.ResistanceRate, true);
        SetupStat(transform, "ComboRate", AppConstants.StatFields.COMBO_RATE, AppDisplayConstants.StatFieldsShort.COMBO_RATE, stat.ComboRate, true);
        SetupStat(transform, "IgnoreComboRate", AppConstants.StatFields.IGNORE_COMBO_RATE, AppDisplayConstants.StatFieldsShort.IGNORE_COMBO_RATE, stat.IgnoreComboRate, true);
        SetupStat(transform, "ComboDamageRate", AppConstants.StatFields.COMBO_DAMAGE_RATE, AppDisplayConstants.StatFieldsShort.COMBO_DAMAGE_RATE, stat.ComboDamageRate, true);
        SetupStat(transform, "ComboResistanceRate", AppConstants.StatFields.COMBO_RESISTANCE_RATE, AppDisplayConstants.StatFieldsShort.COMBO_RESISTANCE_RATE, stat.ComboResistanceRate, true);
        SetupStat(transform, "StunRate", AppConstants.StatFields.STUN_RATE, AppDisplayConstants.StatFieldsShort.STUN_RATE, stat.StunRate, true);
        SetupStat(transform, "IgnoreStunRate", AppConstants.StatFields.IGNORE_STUN_RATE, AppDisplayConstants.StatFieldsShort.IGNORE_STUN_RATE, stat.IgnoreStunRate, true);
        SetupStat(transform, "ReflectionRate", AppConstants.StatFields.REFLECTION_RATE, AppDisplayConstants.StatFieldsShort.REFLECTION_RATE, stat.ReflectionRate, true);
        SetupStat(transform, "IgnoreReflectionRate", AppConstants.StatFields.IGNORE_REFLECTION_RATE, AppDisplayConstants.StatFieldsShort.IGNORE_REFLECTION_RATE, stat.IgnoreReflectionRate, true);
        SetupStat(transform, "ReflectionDamageRate", AppConstants.StatFields.REFLECTION_DAMAGE_RATE, AppDisplayConstants.StatFieldsShort.REFLECTION_DAMAGE_RATE, stat.ReflectionDamageRate, true);
        SetupStat(transform, "ReflectionResistanceRate", AppConstants.StatFields.REFLECTION_RESISTANCE_RATE, AppDisplayConstants.StatFieldsShort.REFLECTION_RESISTANCE_RATE, stat.ReflectionResistanceRate, true);
        SetupStat(transform, "DamageToDifferentFactionRate", AppConstants.StatFields.DAMAGE_TO_DIFFERENT_FACTION_RATE, AppDisplayConstants.StatFieldsShort.DAMAGE_TO_DIFFERENT_FACTION_RATE, stat.DamageToDifferentFactionRate, true);
        SetupStat(transform, "ResistanceToDifferentFactionRate", AppConstants.StatFields.RESISTANCE_TO_DIFFERENT_FACTION_RATE, AppDisplayConstants.StatFieldsShort.RESISTANCE_TO_DIFFERENT_FACTION_RATE, stat.ResistanceToDifferentFactionRate, true);
        SetupStat(transform, "DamageToSameFactionRate", AppConstants.StatFields.DAMAGE_TO_SAME_FACTION_RATE, AppDisplayConstants.StatFieldsShort.DAMAGE_TO_SAME_FACTION_RATE, stat.DamageToSameFactionRate, true);
        SetupStat(transform, "ResistanceToSameFactionRate", AppConstants.StatFields.RESISTANCE_TO_SAME_FACTION_RATE, AppDisplayConstants.StatFieldsShort.RESISTANCE_TO_SAME_FACTION_RATE, stat.ResistanceToSameFactionRate, true);
        SetupStat(transform, "NormalDamageRate", AppConstants.StatFields.NORMAL_DAMAGE_RATE, AppDisplayConstants.StatFieldsShort.NORMAL_DAMAGE_RATE, stat.NormalDamageRate, true);
        SetupStat(transform, "NormalResistanceRate", AppConstants.StatFields.NORMAL_RESISTANCE_RATE, AppDisplayConstants.StatFieldsShort.NORMAL_RESISTANCE_RATE, stat.NormalResistanceRate, true);
        SetupStat(transform, "SkillDamageRate", AppConstants.StatFields.SKILL_DAMAGE_RATE, AppDisplayConstants.StatFieldsShort.SKILL_DAMAGE_RATE, stat.SkillDamageRate, true);
        SetupStat(transform, "SkillResistanceRate", AppConstants.StatFields.SKILL_RESISTANCE_RATE, AppDisplayConstants.StatFieldsShort.SKILL_RESISTANCE_RATE, stat.SkillResistanceRate, true);
    }
    private void SetupStat(Transform root, string statObjectName, string statField, string statDisplayName, double value, bool isPercent = false)
    {
        Transform statTransform = root.Find($"Scroll View/Viewport/Content/{statObjectName}");

        RawImage iconImage = statTransform.Find("IconImage").GetComponent<RawImage>();
        TextMeshProUGUI titleText = statTransform.Find("StatTitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI valueText = statTransform.Find("StatText").GetComponent<TextMeshProUGUI>();

        TextureHelper.CreatePropertyRuneUI(statField, iconImage);

        titleText.text = LocalizationManager.Get(statDisplayName);
        titleText.enableWordWrapping = false;

        if (isPercent)
        {
            valueText.text = NumberFormatterHelper.FormatNumberExtended(value, true) + " %";
        }
        else
        {
            valueText.text = NumberFormatterHelper.FormatNumberExtended(value, true);
        }
    }
}
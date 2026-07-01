using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class CombatEffectProcessor
{
    /// <summary>
    /// Điểm tiếp nhận hiệu ứng duy nhất: Phân loại trạng thái và thực thi Action chuyên biệt
    /// </summary>
    public static void ApplyEffects(List<Effects> effectsList, CardBase caster, CardBase target)
    {
        // Kiểm tra an toàn đầu vào
        if (effectsList == null || effectsList.Count == 0 || target == null) return;

        // SẮP XẾP THỨ TỰ ƯU TIÊN: Trích xuất và sắp xếp danh sách dựa trên điểm số ưu tiên của ActionCode
        var sortedEffects = effectsList
            .Where(e => e != null)
            .OrderBy(e => GetActionPriority(e.EffectAction?.ActionCode))
            .ToList();

        // Duyệt qua từng hiệu ứng trong danh sách để xử lý độc lập
        foreach (var effect in sortedEffects)
        {
            if (effect == null) continue;

            if (effect.Duration > 0)
            {
                var existingEffect = target.FriendlyEffects.Concat(target.HostileEffects)
                    .FirstOrDefault(e => e.SourceEffect.Id == effect.Id && e.IsActive);

                if (existingEffect != null)
                {
                    existingEffect.RemainingDuration = effect.Duration;
                    Debug.Log($"<color=yellow>[Effect Refreshed]</color> Làm mới thời gian hiệu ứng {effect.Name} lên {effect.Duration} lượt.");

                }

                RuntimeEffectInstance instance = new RuntimeEffectInstance(effect, effect.Duration, caster);

                if (caster.TeamId == target.TeamId)
                {
                    target.FriendlyEffects.Add(instance);
                }
                else
                {
                    target.HostileEffects.Add(instance);
                }
            }
        }

        foreach (var effect in effectsList)
        {
            ExecuteActionLogic(effect, caster, target);
        }
    }

    private static int GetActionPriority(string actionCode)
    {
        if (string.IsNullOrEmpty(actionCode)) return 99; // Không có mã -> chạy cuối cùng

        switch (actionCode)
        {
            case AppConstants.EffectAction.IMMUNITY:     // Miễn nhiễm
                return 1;
            case AppConstants.EffectAction.BREAK:        // Hóa giải hiệu ứng (Xóa theo dòng chỉ số)
                return 2;
            case AppConstants.EffectAction.REMOVE:       // Xóa hiệu ứng có lợi/hại (Dispel)
                return 3;
            case AppConstants.EffectAction.EXTEND:       // Gia tăng thời gian
                return 4;
            case AppConstants.EffectAction.SHORTEN:      // Rút ngắn thời gian
                return 5;
            case AppConstants.EffectAction.LOCK:         // KHÓA chỉ số (Không cho thay đổi)
                return 6;
            case AppConstants.EffectAction.PREVENT:      // Ngăn chặn (Sát thương/Hiệu ứng)
                return 7;
            case AppConstants.EffectAction.COPY:         // Sao chép chỉ số mục tiêu sang bản thân
                return 8;
            case AppConstants.EffectAction.STEAL:        // Hút chỉ số mục tiêu sang bản thân
                return 9;
            case AppConstants.EffectAction.TRANSFER:     // Dịch chuyển hiệu ứng từ quân này sang quân khác
                return 10;
            case AppConstants.EffectAction.CONVERT:      // Chuyển hóa chỉ số (Ví dụ: Giáp -> Công)
                return 11;
            case AppConstants.EffectAction.REFLECT:      // Phản đòn sát thương
                return 12;
            case AppConstants.EffectAction.REVERSE:      // Đảo ngược thuộc tính/trạng thái
                return 13;
            case AppConstants.EffectAction.SET:          // Thiết lập cứng chỉ số về một giá trị
                return 14;
            case AppConstants.EffectAction.INCREASE:     // Tăng chỉ số (Flat/% gốc)
                return 15;
            case AppConstants.EffectAction.DECREASE:     // Giảm chỉ số (Flat/% gốc)
                return 16;
            case AppConstants.EffectAction.RESTORE:      // Phục hồi (Mana/Giáp...) về trạng thái ban đầu
                return 17;
            case AppConstants.EffectAction.DAMAGE:       // Gây sát thương trực tiếp/DoT
                return 18;
            case AppConstants.EffectAction.HEAL:         // Hồi phục máu
                return 19;
            case AppConstants.EffectAction.APPLY:        // Kích hoạt/Áp dụng một hiệu ứng đặc biệt khác lên sàn
                return 20;
            case AppConstants.EffectAction.LIMIT_ACTION: // Khống chế (Choáng/Câm lặng...)
                return 21;

            default:
                return 50; // Dự phòng cho các ActionCode mở rộng sau này
        }
    }

    /// <summary>
    /// Bộ chuyển mạch Switch-Case điều hướng sang các hàm xử lý riêng biệt
    /// </summary>
    public static void ExecuteActionLogic(Effects effect, CardBase caster, CardBase target)
    {
        string actionCode = effect.EffectAction?.ActionCode?.ToUpper();
        if (string.IsNullOrEmpty(actionCode)) return;

        switch (actionCode)
        {
            case AppConstants.EffectAction.INCREASE:
                ExecuteIncrease(effect, target);
                break;
            case AppConstants.EffectAction.DECREASE:
                ExecuteDecrease(effect, target);
                break;
            case AppConstants.EffectAction.RESTORE:
                ExecuteRestore(effect, target);
                break;
            case AppConstants.EffectAction.LOCK:
                ExecuteLock(effect, target);
                break;
            case AppConstants.EffectAction.BREAK:
                ExecuteBreak(effect, target);
                break;
            case AppConstants.EffectAction.CONVERT:
                ExecuteConvert(effect, target);
                break;
            case AppConstants.EffectAction.PREVENT:
                ExecutePrevent(effect, target);
                break;
            case AppConstants.EffectAction.SET:
                ExecuteSet(effect, target);
                break;
            case AppConstants.EffectAction.IMMUNITY:
                ExecuteImmunity(effect, target);
                break;
            case AppConstants.EffectAction.STEAL:
                ExecuteSteal(effect, caster, target);
                break;
            case AppConstants.EffectAction.COPY:
                ExecuteCopy(effect, caster, target);
                break;
            case AppConstants.EffectAction.TRANSFER:
                ExecuteImmunity(effect, target);
                break;
            case AppConstants.EffectAction.REVERSE:
                ExecuteReverse(effect, target);
                break;
            case AppConstants.EffectAction.REFLECT:
                ExecuteReflect(effect, caster, target);
                break;
            case AppConstants.EffectAction.APPLY:
                ExecuteImmunity(effect, target);
                break;
            case AppConstants.EffectAction.REMOVE:
                ExecuteRemove(effect, target);
                break;
            case AppConstants.EffectAction.EXTEND:
                ExecuteExtend(effect, target);
                break;
            case AppConstants.EffectAction.SHORTEN:
                ExecuteShorten(effect, target);
                break;
            case AppConstants.EffectAction.LIMIT_ACTION:
                ExecuteLimitAction(effect, target);
                break;
            case AppConstants.EffectAction.DAMAGE:
                ExecuteDamage(effect, caster, target);
                break;
            case AppConstants.EffectAction.HEAL:
                ExecuteHeal(effect, caster, target);
                break;

            // Các case còn lại (STEAL, COPY, TRANSFER, REVERSE...) xử lý tương tự
            default:
                Debug.LogWarning($"[Effect Engine] ActionCode [{actionCode}] đang được phát triển hoặc chưa kích hoạt logic.");
                break;
        }
    }

    // =====================================================================
    // CÁC HÀM EXECUTE RIÊNG BIỆT CHO TỪNG ACTION CODE
    // =====================================================================

    private static void ExecuteIncrease(Effects effect, CardBase target)
    {
        if (effect == null || target == null) return;

        if (target.LockedProperties.Contains(effect.EffectProperty.PropertyCode))
        {
            return;
        }

        switch (effect.EffectProperty.PropertyCode)
        {
            case AppConstants.EffectProperty.HEALTH:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentHealth = target.CurrentHealth + (target.Health * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentHealth = target.CurrentHealth + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PHYSICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPhysicalAttack = target.CurrentPhysicalAttack + (target.PhysicalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPhysicalAttack = target.CurrentPhysicalAttack + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PHYSICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPhysicalDefense = target.CurrentPhysicalDefense + (target.PhysicalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPhysicalDefense = target.CurrentPhysicalDefense + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MAGICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMagicalAttack = target.CurrentMagicalAttack + (target.MagicalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMagicalAttack = target.CurrentMagicalAttack + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MAGICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMagicalDefense = target.CurrentMagicalDefense + (target.MagicalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMagicalDefense = target.CurrentMagicalDefense + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CHEMICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentChemicalAttack = target.CurrentChemicalAttack + (target.ChemicalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentChemicalAttack = target.CurrentChemicalAttack + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CHEMICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentChemicalDefense = target.CurrentChemicalDefense + (target.ChemicalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentChemicalDefense = target.CurrentChemicalDefense + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ATOMIC_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAtomicAttack = target.CurrentAtomicAttack + (target.AtomicAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAtomicAttack = target.CurrentAtomicAttack + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ATOMIC_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAtomicDefense = target.CurrentAtomicDefense + (target.AtomicDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAtomicDefense = target.CurrentAtomicDefense + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MENTAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMentalAttack = target.CurrentMentalAttack + (target.MentalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMentalAttack = target.CurrentMentalAttack + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MENTAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMentalDefense = target.CurrentMentalDefense + (target.MentalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMentalDefense = target.CurrentMentalDefense + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SPEED:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentSpeed = target.CurrentSpeed + (target.Speed * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentSpeed = target.CurrentSpeed + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CRITICAL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentCriticalDamageRate = target.CurrentCriticalDamageRate + (target.CriticalDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentCriticalDamageRate = target.CurrentCriticalDamageRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CRITICAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentCriticalRate = target.CurrentCriticalRate + (target.CriticalRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentCriticalRate = target.CurrentCriticalRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CRITICAL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentCriticalResistanceRate = target.CurrentCriticalResistanceRate + (target.CriticalResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentCriticalResistanceRate = target.CurrentCriticalResistanceRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_CRITICAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreCriticalRate = target.CurrentIgnoreCriticalRate + (target.IgnoreCriticalRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreCriticalRate = target.CurrentIgnoreCriticalRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PENETRATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPenetrationRate = target.CurrentPenetrationRate + (target.PenetrationRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPenetrationRate = target.CurrentPenetrationRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PENETRATION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPenetrationResistanceRate = target.CurrentPenetrationResistanceRate + (target.PenetrationResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPenetrationResistanceRate = target.CurrentPenetrationResistanceRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.EVASION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentEvasionRate = target.CurrentEvasionRate + (target.EvasionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentEvasionRate = target.CurrentEvasionRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.DAMAGE_ABSORPTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentDamageAbsorptionRate = target.CurrentDamageAbsorptionRate + (target.DamageAbsorptionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentDamageAbsorptionRate = target.CurrentDamageAbsorptionRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_DAMAGE_ABSORPTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreDamageAbsorptionRate = target.CurrentIgnoreDamageAbsorptionRate + (target.IgnoreDamageAbsorptionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreDamageAbsorptionRate = target.CurrentIgnoreDamageAbsorptionRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ABSORBED_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAbsorbedDamageRate = target.CurrentAbsorbedDamageRate + (target.AbsorbedDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAbsorbedDamageRate = target.CurrentAbsorbedDamageRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.VITALITY_REGENERATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentVitalityRegenerationRate = target.CurrentVitalityRegenerationRate + (target.VitalityRegenerationRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentVitalityRegenerationRate = target.CurrentVitalityRegenerationRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.VITALITY_REGENERATION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentVitalityRegenerationResistanceRate = target.CurrentVitalityRegenerationResistanceRate + (target.VitalityRegenerationResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentVitalityRegenerationResistanceRate = target.CurrentVitalityRegenerationResistanceRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ACCURACY_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAccuracyRate = target.CurrentAccuracyRate + (target.AccuracyRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAccuracyRate = target.CurrentAccuracyRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.LIFESTEAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentLifestealRate = target.CurrentLifestealRate + (target.LifestealRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentLifestealRate = target.CurrentLifestealRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MANA:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMana = target.CurrentMana + (target.Mana * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMana = target.CurrentMana + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MANA_REGENERATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentManaRegenerationRate = target.CurrentManaRegenerationRate + (target.ManaRegenerationRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentManaRegenerationRate = target.CurrentManaRegenerationRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SHIELD_STRENGTH:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentShieldStrength = target.CurrentShieldStrength + (target.ShieldStrength * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentShieldStrength = target.CurrentShieldStrength + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.TENACITY:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentTenacity = target.CurrentTenacity + (target.Tenacity * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentTenacity = target.CurrentTenacity + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentResistanceRate = target.CurrentResistanceRate + (target.ResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentResistanceRate = target.CurrentResistanceRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.COMBO_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentComboRate = target.CurrentComboRate + (target.ComboRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentComboRate = target.CurrentComboRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_COMBO_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreComboRate = target.CurrentIgnoreComboRate + (target.IgnoreComboRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreComboRate = target.CurrentIgnoreComboRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.COMBO_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentComboDamageRate = target.CurrentComboDamageRate + (target.ComboDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentComboDamageRate = target.CurrentComboDamageRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.COMBO_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentComboResistanceRate = target.CurrentComboResistanceRate + (target.ComboResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentComboResistanceRate = target.CurrentComboResistanceRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.STUN_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentStunRate = target.CurrentStunRate + (target.StunRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentStunRate = target.CurrentStunRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_STUN_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreStunRate = target.CurrentIgnoreStunRate + (target.IgnoreStunRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreStunRate = target.CurrentIgnoreStunRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.REFLECTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentReflectionRate = target.CurrentReflectionRate + (target.ReflectionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentReflectionRate = target.CurrentReflectionRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_REFLECTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreReflectionRate = target.CurrentIgnoreReflectionRate + (target.IgnoreReflectionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreReflectionRate = target.CurrentIgnoreReflectionRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.REFLECTION_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentReflectionDamageRate = target.CurrentReflectionDamageRate + (target.ReflectionDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentReflectionDamageRate = target.CurrentReflectionDamageRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.REFLECTION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentReflectionResistanceRate = target.CurrentReflectionResistanceRate + (target.ReflectionResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentReflectionResistanceRate = target.CurrentReflectionResistanceRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.DAMAGE_TO_DIFFERENT_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentDamageToDifferentFactionRate = target.CurrentDamageToDifferentFactionRate + (target.DamageToDifferentFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentDamageToDifferentFactionRate = target.CurrentDamageToDifferentFactionRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.RESISTANCE_TO_DIFFERENT_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentResistanceToDifferentFactionRate = target.CurrentResistanceToDifferentFactionRate + (target.ResistanceToDifferentFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentResistanceToDifferentFactionRate = target.CurrentResistanceToDifferentFactionRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.DAMAGE_TO_SAME_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentDamageToSameFactionRate = target.CurrentDamageToSameFactionRate + (target.DamageToSameFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentDamageToSameFactionRate = target.CurrentDamageToSameFactionRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.RESISTANCE_TO_SAME_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentResistanceToSameFactionRate = target.CurrentResistanceToSameFactionRate + (target.ResistanceToSameFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentResistanceToSameFactionRate = target.CurrentResistanceToSameFactionRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.NORMAL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentNormalDamageRate = target.CurrentNormalDamageRate + (target.NormalDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentNormalDamageRate = target.CurrentNormalDamageRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.NORMAL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentNormalResistanceRate = target.CurrentNormalResistanceRate + (target.NormalResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentNormalResistanceRate = target.CurrentNormalResistanceRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SKILL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentSkillDamageRate = target.CurrentSkillDamageRate + (target.SkillDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentSkillDamageRate = target.CurrentSkillDamageRate + effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SKILL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentSkillResistanceRate = target.CurrentSkillResistanceRate + (target.SkillResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentSkillResistanceRate = target.CurrentSkillResistanceRate + effect.Value;
                }
                break;

            default:
                // UnityEngine.Debug.LogWarning($"[Stats Mapping] Chưa định nghĩa thuộc tính: {propertyCode}");
                break;
        }
    }

    private static void ExecuteDecrease(Effects effect, CardBase target)
    {
        if (effect == null || target == null) return;

        if (target.LockedProperties.Contains(effect.EffectProperty.PropertyCode))
        {
            return;
        }

        switch (effect.EffectProperty.PropertyCode)
        {
            case AppConstants.EffectProperty.HEALTH:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentHealth = target.CurrentHealth - (target.Health * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentHealth = target.CurrentHealth - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PHYSICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPhysicalAttack = target.CurrentPhysicalAttack - (target.PhysicalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPhysicalAttack = target.CurrentPhysicalAttack - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PHYSICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPhysicalDefense = target.CurrentPhysicalDefense - (target.PhysicalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPhysicalDefense = target.CurrentPhysicalDefense - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MAGICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMagicalAttack = target.CurrentMagicalAttack - (target.MagicalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMagicalAttack = target.CurrentMagicalAttack - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MAGICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMagicalDefense = target.CurrentMagicalDefense - (target.MagicalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMagicalDefense = target.CurrentMagicalDefense - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CHEMICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentChemicalAttack = target.CurrentChemicalAttack - (target.ChemicalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentChemicalAttack = target.CurrentChemicalAttack - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CHEMICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentChemicalDefense = target.CurrentChemicalDefense - (target.ChemicalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentChemicalDefense = target.CurrentChemicalDefense - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ATOMIC_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAtomicAttack = target.CurrentAtomicAttack - (target.AtomicAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAtomicAttack = target.CurrentAtomicAttack - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ATOMIC_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAtomicDefense = target.CurrentAtomicDefense - (target.AtomicDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAtomicDefense = target.CurrentAtomicDefense - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MENTAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMentalAttack = target.CurrentMentalAttack - (target.MentalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMentalAttack = target.CurrentMentalAttack - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MENTAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMentalDefense = target.CurrentMentalDefense - (target.MentalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMentalDefense = target.CurrentMentalDefense - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SPEED:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentSpeed = target.CurrentSpeed - (target.Speed * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentSpeed = target.CurrentSpeed - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CRITICAL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentCriticalDamageRate = target.CurrentCriticalDamageRate - (target.CriticalDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentCriticalDamageRate = target.CurrentCriticalDamageRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CRITICAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentCriticalRate = target.CurrentCriticalRate - (target.CriticalRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentCriticalRate = target.CurrentCriticalRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CRITICAL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentCriticalResistanceRate = target.CurrentCriticalResistanceRate - (target.CriticalResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentCriticalResistanceRate = target.CurrentCriticalResistanceRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_CRITICAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreCriticalRate = target.CurrentIgnoreCriticalRate - (target.IgnoreCriticalRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreCriticalRate = target.CurrentIgnoreCriticalRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PENETRATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPenetrationRate = target.CurrentPenetrationRate - (target.PenetrationRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPenetrationRate = target.CurrentPenetrationRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PENETRATION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPenetrationResistanceRate = target.CurrentPenetrationResistanceRate - (target.PenetrationResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPenetrationResistanceRate = target.CurrentPenetrationResistanceRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.EVASION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentEvasionRate = target.CurrentEvasionRate - (target.EvasionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentEvasionRate = target.CurrentEvasionRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.DAMAGE_ABSORPTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentDamageAbsorptionRate = target.CurrentDamageAbsorptionRate - (target.DamageAbsorptionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentDamageAbsorptionRate = target.CurrentDamageAbsorptionRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_DAMAGE_ABSORPTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreDamageAbsorptionRate = target.CurrentIgnoreDamageAbsorptionRate - (target.IgnoreDamageAbsorptionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreDamageAbsorptionRate = target.CurrentIgnoreDamageAbsorptionRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ABSORBED_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAbsorbedDamageRate = target.CurrentAbsorbedDamageRate - (target.AbsorbedDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAbsorbedDamageRate = target.CurrentAbsorbedDamageRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.VITALITY_REGENERATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentVitalityRegenerationRate = target.CurrentVitalityRegenerationRate - (target.VitalityRegenerationRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentVitalityRegenerationRate = target.CurrentVitalityRegenerationRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.VITALITY_REGENERATION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentVitalityRegenerationResistanceRate = target.CurrentVitalityRegenerationResistanceRate - (target.VitalityRegenerationResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentVitalityRegenerationResistanceRate = target.CurrentVitalityRegenerationResistanceRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ACCURACY_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAccuracyRate = target.CurrentAccuracyRate - (target.AccuracyRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAccuracyRate = target.CurrentAccuracyRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.LIFESTEAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentLifestealRate = target.CurrentLifestealRate - (target.LifestealRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentLifestealRate = target.CurrentLifestealRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MANA:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMana = target.CurrentMana - (target.Mana * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMana = target.CurrentMana - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MANA_REGENERATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentManaRegenerationRate = target.CurrentManaRegenerationRate - (target.ManaRegenerationRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentManaRegenerationRate = target.CurrentManaRegenerationRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SHIELD_STRENGTH:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentShieldStrength = target.CurrentShieldStrength - (target.ShieldStrength * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentShieldStrength = target.CurrentShieldStrength - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.TENACITY:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentTenacity = target.CurrentTenacity - (target.Tenacity * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentTenacity = target.CurrentTenacity - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentResistanceRate = target.CurrentResistanceRate - (target.ResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentResistanceRate = target.CurrentResistanceRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.COMBO_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentComboRate = target.CurrentComboRate - (target.ComboRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentComboRate = target.CurrentComboRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_COMBO_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreComboRate = target.CurrentIgnoreComboRate - (target.IgnoreComboRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreComboRate = target.CurrentIgnoreComboRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.COMBO_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentComboDamageRate = target.CurrentComboDamageRate - (target.ComboDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentComboDamageRate = target.CurrentComboDamageRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.COMBO_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentComboResistanceRate = target.CurrentComboResistanceRate - (target.ComboResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentComboResistanceRate = target.CurrentComboResistanceRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.STUN_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentStunRate = target.CurrentStunRate - (target.StunRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentStunRate = target.CurrentStunRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_STUN_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreStunRate = target.CurrentIgnoreStunRate - (target.IgnoreStunRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreStunRate = target.CurrentIgnoreStunRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.REFLECTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentReflectionRate = target.CurrentReflectionRate - (target.ReflectionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentReflectionRate = target.CurrentReflectionRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_REFLECTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreReflectionRate = target.CurrentIgnoreReflectionRate - (target.IgnoreReflectionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreReflectionRate = target.CurrentIgnoreReflectionRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.REFLECTION_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentReflectionDamageRate = target.CurrentReflectionDamageRate - (target.ReflectionDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentReflectionDamageRate = target.CurrentReflectionDamageRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.REFLECTION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentReflectionResistanceRate = target.CurrentReflectionResistanceRate - (target.ReflectionResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentReflectionResistanceRate = target.CurrentReflectionResistanceRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.DAMAGE_TO_DIFFERENT_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentDamageToDifferentFactionRate = target.CurrentDamageToDifferentFactionRate - (target.DamageToDifferentFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentDamageToDifferentFactionRate = target.CurrentDamageToDifferentFactionRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.RESISTANCE_TO_DIFFERENT_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentResistanceToDifferentFactionRate = target.CurrentResistanceToDifferentFactionRate - (target.ResistanceToDifferentFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentResistanceToDifferentFactionRate = target.CurrentResistanceToDifferentFactionRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.DAMAGE_TO_SAME_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentDamageToSameFactionRate = target.CurrentDamageToSameFactionRate - (target.DamageToSameFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentDamageToSameFactionRate = target.CurrentDamageToSameFactionRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.RESISTANCE_TO_SAME_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentResistanceToSameFactionRate = target.CurrentResistanceToSameFactionRate - (target.ResistanceToSameFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentResistanceToSameFactionRate = target.CurrentResistanceToSameFactionRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.NORMAL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentNormalDamageRate = target.CurrentNormalDamageRate - (target.NormalDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentNormalDamageRate = target.CurrentNormalDamageRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.NORMAL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentNormalResistanceRate = target.CurrentNormalResistanceRate - (target.NormalResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentNormalResistanceRate = target.CurrentNormalResistanceRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SKILL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentSkillDamageRate = target.CurrentSkillDamageRate - (target.SkillDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentSkillDamageRate = target.CurrentSkillDamageRate - effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SKILL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentSkillResistanceRate = target.CurrentSkillResistanceRate - (target.SkillResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentSkillResistanceRate = target.CurrentSkillResistanceRate - effect.Value;
                }
                break;

            default:
                // UnityEngine.Debug.LogWarning($"[Stats Mapping] Chưa định nghĩa thuộc tính: {propertyCode}");
                break;
        }
    }

    private static void ExecuteRestore(Effects effect, CardBase target)
    {
        if (effect == null || target == null) return;

        if (target.LockedProperties.Contains(effect.EffectProperty.PropertyCode))
        {
            return;
        }

        switch (effect.EffectProperty.PropertyCode)
        {
            case AppConstants.EffectProperty.HEALTH:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentHealth = (target.Health * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentHealth = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PHYSICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPhysicalAttack = (target.PhysicalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPhysicalAttack = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PHYSICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPhysicalDefense = (target.PhysicalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPhysicalDefense = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MAGICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMagicalAttack = (target.MagicalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMagicalAttack = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MAGICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMagicalDefense = (target.MagicalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMagicalDefense = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CHEMICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentChemicalAttack = (target.ChemicalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentChemicalAttack = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CHEMICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentChemicalDefense = (target.ChemicalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentChemicalDefense = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ATOMIC_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAtomicAttack = (target.AtomicAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAtomicAttack = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ATOMIC_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAtomicDefense = (target.AtomicDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAtomicDefense = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MENTAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMentalAttack = (target.MentalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMentalAttack = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MENTAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMentalDefense = (target.MentalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMentalDefense = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SPEED:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentSpeed = (target.Speed * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentSpeed = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CRITICAL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentCriticalDamageRate = (target.CriticalDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentCriticalDamageRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CRITICAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentCriticalRate = (target.CriticalRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentCriticalRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CRITICAL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentCriticalResistanceRate = (target.CriticalResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentCriticalResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_CRITICAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreCriticalRate = (target.IgnoreCriticalRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreCriticalRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PENETRATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPenetrationRate = (target.PenetrationRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPenetrationRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PENETRATION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPenetrationResistanceRate = (target.PenetrationResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPenetrationResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.EVASION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentEvasionRate = (target.EvasionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentEvasionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.DAMAGE_ABSORPTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentDamageAbsorptionRate = (target.DamageAbsorptionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentDamageAbsorptionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_DAMAGE_ABSORPTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreDamageAbsorptionRate = (target.IgnoreDamageAbsorptionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreDamageAbsorptionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ABSORBED_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAbsorbedDamageRate = (target.AbsorbedDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAbsorbedDamageRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.VITALITY_REGENERATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentVitalityRegenerationRate = (target.VitalityRegenerationRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentVitalityRegenerationRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.VITALITY_REGENERATION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentVitalityRegenerationResistanceRate = (target.VitalityRegenerationResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentVitalityRegenerationResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ACCURACY_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAccuracyRate = (target.AccuracyRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAccuracyRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.LIFESTEAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentLifestealRate = (target.LifestealRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentLifestealRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MANA:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMana = (target.Mana * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMana = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MANA_REGENERATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentManaRegenerationRate = (target.ManaRegenerationRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentManaRegenerationRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SHIELD_STRENGTH:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentShieldStrength = (target.ShieldStrength * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentShieldStrength = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.TENACITY:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentTenacity = (target.Tenacity * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentTenacity = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentResistanceRate = (target.ResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.COMBO_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentComboRate = (target.ComboRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentComboRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_COMBO_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreComboRate = (target.IgnoreComboRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreComboRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.COMBO_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentComboDamageRate = (target.ComboDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentComboDamageRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.COMBO_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentComboResistanceRate = (target.ComboResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentComboResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.STUN_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentStunRate = (target.StunRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentStunRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_STUN_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreStunRate = (target.IgnoreStunRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreStunRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.REFLECTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentReflectionRate = (target.ReflectionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentReflectionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_REFLECTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreReflectionRate = (target.IgnoreReflectionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreReflectionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.REFLECTION_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentReflectionDamageRate = (target.ReflectionDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentReflectionDamageRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.REFLECTION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentReflectionResistanceRate = (target.ReflectionResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentReflectionResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.DAMAGE_TO_DIFFERENT_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentDamageToDifferentFactionRate = (target.DamageToDifferentFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentDamageToDifferentFactionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.RESISTANCE_TO_DIFFERENT_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentResistanceToDifferentFactionRate = (target.ResistanceToDifferentFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentResistanceToDifferentFactionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.DAMAGE_TO_SAME_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentDamageToSameFactionRate = (target.DamageToSameFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentDamageToSameFactionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.RESISTANCE_TO_SAME_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentResistanceToSameFactionRate = (target.ResistanceToSameFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentResistanceToSameFactionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.NORMAL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentNormalDamageRate = (target.NormalDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentNormalDamageRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.NORMAL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentNormalResistanceRate = (target.NormalResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentNormalResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SKILL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentSkillDamageRate = (target.SkillDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentSkillDamageRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SKILL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentSkillResistanceRate = (target.SkillResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentSkillResistanceRate = effect.Value;
                }
                break;

            default:
                // UnityEngine.Debug.LogWarning($"[Stats Mapping] Chưa định nghĩa thuộc tính: {propertyCode}");
                break;
        }
        ;
    }

    private static void ExecuteLock(Effects effect, CardBase target)
    {
        if (effect == null || target == null) return;

        target.LockedProperties.Add(effect.EffectProperty.PropertyCode);
    }

    private static void ExecuteBreak(Effects effect, CardBase target)
    {
        if (effect == null || target == null) return;

        if (target.FriendlyEffects != null)
        {
            target.FriendlyEffects.RemoveAll(activeEffect =>
            activeEffect.SourceEffect != null &&
            activeEffect.SourceEffect.EffectProperty.PropertyCode == effect.EffectProperty.PropertyCode);
        }
    }

    private static void ExecuteConvert(Effects effect, CardBase target)
    {
        if (effect == null || target == null || effect.Value <= 0) return;

        List<string> allProperties = AppConstants.EffectProperty.AllProperties;

        if (allProperties.Count <= 1) return; // Nếu danh sách rỗng hoặc chỉ có 1 thuộc tính thì không convert được

        var activeHostileEffects = target.HostileEffects.Where(e => e.IsActive && e.SourceEffect?.EffectProperty != null).ToList();
        if (activeHostileEffects.Count == 0) return;

        System.Random rand = new System.Random();

        int convertCount = Math.Min(effect.Value, activeHostileEffects.Count);

        var effectsToConvert = activeHostileEffects.OrderBy(x => rand.Next()).Take(convertCount).ToList();

        foreach (var runtimeEffect in effectsToConvert)
        {
            string currentProperty = runtimeEffect.SourceEffect.EffectProperty.PropertyCode;

            var validNewProperties = allProperties.Where(p => p != currentProperty).ToList();

            if (validNewProperties.Count > 0)
            {
                // Bốc ngẫu nhiên 1 thuộc tính mới
                string newProperty = validNewProperties[rand.Next(validNewProperties.Count)];

                // Gán thuộc tính mới vào hiệu ứng
                runtimeEffect.SourceEffect.EffectProperty.PropertyCode = newProperty;

                // Debug.Log($"<color=cyan>[Convert Success]</color> Hiệu ứng {runtimeEffect.SourceEffect.Name} đã bị biến đổi thuộc tính bất lợi từ [{currentProperty}] thành [{newProperty}]!");
            }
        }
    }

    private static void ExecutePrevent(Effects effect, CardBase target)
    {
        if (effect == null || target == null) return;

        for (int i = 0; i < effect.Value; i++)
        {
            // Tìm hiệu ứng bất lợi ĐẦU TIÊN trong list có trùng PropertyCode
            var effectToRemove = target.HostileEffects.FirstOrDefault(e =>
                e.IsActive && e.SourceEffect?.EffectProperty?.PropertyCode == effect.EffectProperty?.PropertyCode);

            // Nếu tìm thấy, thực hiện xóa bỏ hoặc vô hiệu hóa nó
            if (effectToRemove != null)
            {
                // Cách 1: Xóa thẳng ra khỏi List (Nếu bạn quản lý list động)
                target.HostileEffects.Remove(effectToRemove);

                // Cách 2: Nếu bạn muốn giữ lại để log hoặc xử lý kết thúc duration, hãy clear Active trước khi xóa
                effectToRemove.IsActive = false;
                effectToRemove.RemainingDuration = 0;

                // Debug.Log($"<color=green>[Prevent Execution]</color> Đã xóa 1 tầng hiệu ứng xấu: {effectToRemove.SourceEffect.Name} (Còn lại {effect.Value - (i + 1)} lần xóa trong lượt này)");
            }
            else
            {
                // Nếu trong List không còn hiệu ứng nào trùng nữa thì dừng vòng lặp sớm để tối ưu
                // Debug.Log($"<color=white>[Prevent Execution]</color> Không còn hiệu ứng xấu nào trùng thuộc tính {effect.EffectProperty?.PropertyCode} để xóa.");
                break;
            }
        }
    }

    private static void ExecuteSet(Effects effect, CardBase target)
    {
        if (effect == null || target == null) return;

        if (target.LockedProperties.Contains(effect.EffectProperty.PropertyCode))
        {
            return;
        }

        switch (effect.EffectProperty.PropertyCode)
        {
            case AppConstants.EffectProperty.HEALTH:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentHealth = (target.Health * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentHealth = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PHYSICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPhysicalAttack = (target.PhysicalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPhysicalAttack = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PHYSICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPhysicalDefense = (target.PhysicalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPhysicalDefense = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MAGICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMagicalAttack = (target.MagicalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMagicalAttack = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MAGICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMagicalDefense = (target.MagicalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMagicalDefense = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CHEMICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentChemicalAttack = (target.ChemicalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentChemicalAttack = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CHEMICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentChemicalDefense = (target.ChemicalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentChemicalDefense = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ATOMIC_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAtomicAttack = (target.AtomicAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAtomicAttack = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ATOMIC_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAtomicDefense = (target.AtomicDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAtomicDefense = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MENTAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMentalAttack = (target.MentalAttack * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMentalAttack = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MENTAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMentalDefense = (target.MentalDefense * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMentalDefense = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SPEED:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentSpeed = (target.Speed * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentSpeed = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CRITICAL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentCriticalDamageRate = (target.CriticalDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentCriticalDamageRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CRITICAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentCriticalRate = (target.CriticalRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentCriticalRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.CRITICAL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentCriticalResistanceRate = (target.CriticalResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentCriticalResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_CRITICAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreCriticalRate = (target.IgnoreCriticalRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreCriticalRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PENETRATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPenetrationRate = (target.PenetrationRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPenetrationRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.PENETRATION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentPenetrationResistanceRate = (target.PenetrationResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentPenetrationResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.EVASION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentEvasionRate = (target.EvasionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentEvasionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.DAMAGE_ABSORPTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentDamageAbsorptionRate = (target.DamageAbsorptionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentDamageAbsorptionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_DAMAGE_ABSORPTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreDamageAbsorptionRate = (target.IgnoreDamageAbsorptionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreDamageAbsorptionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ABSORBED_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAbsorbedDamageRate = (target.AbsorbedDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAbsorbedDamageRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.VITALITY_REGENERATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentVitalityRegenerationRate = (target.VitalityRegenerationRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentVitalityRegenerationRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.VITALITY_REGENERATION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentVitalityRegenerationResistanceRate = (target.VitalityRegenerationResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentVitalityRegenerationResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.ACCURACY_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentAccuracyRate = (target.AccuracyRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentAccuracyRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.LIFESTEAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentLifestealRate = (target.LifestealRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentLifestealRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MANA:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentMana = (target.Mana * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentMana = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.MANA_REGENERATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentManaRegenerationRate = (target.ManaRegenerationRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentManaRegenerationRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SHIELD_STRENGTH:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentShieldStrength = (target.ShieldStrength * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentShieldStrength = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.TENACITY:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentTenacity = (target.Tenacity * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentTenacity = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentResistanceRate = (target.ResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.COMBO_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentComboRate = (target.ComboRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentComboRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_COMBO_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreComboRate = (target.IgnoreComboRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreComboRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.COMBO_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentComboDamageRate = (target.ComboDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentComboDamageRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.COMBO_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentComboResistanceRate = (target.ComboResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentComboResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.STUN_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentStunRate = (target.StunRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentStunRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_STUN_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreStunRate = (target.IgnoreStunRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreStunRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.REFLECTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentReflectionRate = (target.ReflectionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentReflectionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.IGNORE_REFLECTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentIgnoreReflectionRate = (target.IgnoreReflectionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentIgnoreReflectionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.REFLECTION_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentReflectionDamageRate = (target.ReflectionDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentReflectionDamageRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.REFLECTION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentReflectionResistanceRate = (target.ReflectionResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentReflectionResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.DAMAGE_TO_DIFFERENT_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentDamageToDifferentFactionRate = (target.DamageToDifferentFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentDamageToDifferentFactionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.RESISTANCE_TO_DIFFERENT_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentResistanceToDifferentFactionRate = (target.ResistanceToDifferentFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentResistanceToDifferentFactionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.DAMAGE_TO_SAME_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentDamageToSameFactionRate = (target.DamageToSameFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentDamageToSameFactionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.RESISTANCE_TO_SAME_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentResistanceToSameFactionRate = (target.ResistanceToSameFactionRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentResistanceToSameFactionRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.NORMAL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentNormalDamageRate = (target.NormalDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentNormalDamageRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.NORMAL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentNormalResistanceRate = (target.NormalResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentNormalResistanceRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SKILL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentSkillDamageRate = (target.SkillDamageRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentSkillDamageRate = effect.Value;
                }
                break;

            case AppConstants.EffectProperty.SKILL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    target.CurrentSkillResistanceRate = (target.SkillResistanceRate * effect.Value / 100);
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    target.CurrentSkillResistanceRate = effect.Value;
                }
                break;

            default:
                // UnityEngine.Debug.LogWarning($"[Stats Mapping] Chưa định nghĩa thuộc tính: {propertyCode}");
                break;
        }
        ;
    }

    private static void ExecuteImmunity(Effects effect, CardBase target)
    {
        if (effect == null || target == null) return;

        target.HostileEffects.Clear();
    }

    private static void ExecuteSteal(Effects effect, CardBase caster, CardBase target)
    {
        if (effect == null || target == null || caster == null) return;

        // Biến tạm để tính lượng chỉ số sẽ bị hút (đổi kiểu dữ liệu tùy theo hệ thống của bạn, ví dụ: int hoặc float)
        double stealValue = 0f;

        switch (effect.EffectProperty.PropertyCode)
        {
            // --- HEALTH ---
            case AppConstants.EffectProperty.HEALTH:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.Health * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentHealth = target.CurrentHealth - stealValue;
                caster.CurrentHealth = caster.CurrentHealth + stealValue;
                break;

            // --- ATTACK STATS ---
            case AppConstants.EffectProperty.PHYSICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.PhysicalAttack * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentPhysicalAttack = target.CurrentPhysicalAttack - stealValue;
                caster.CurrentPhysicalAttack = caster.CurrentPhysicalAttack + stealValue;
                break;

            // --- DEFENSE STATS ---
            case AppConstants.EffectProperty.PHYSICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.PhysicalDefense * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentPhysicalDefense = target.CurrentPhysicalDefense - stealValue;
                caster.CurrentPhysicalDefense = caster.CurrentPhysicalDefense + stealValue;
                break;

            case AppConstants.EffectProperty.MAGICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.MagicalAttack * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentMagicalAttack = target.CurrentMagicalAttack - stealValue;
                caster.CurrentMagicalAttack = caster.CurrentMagicalAttack + stealValue;
                break;

            case AppConstants.EffectProperty.MAGICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.MagicalDefense * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentMagicalDefense = target.CurrentMagicalDefense - stealValue;
                caster.CurrentMagicalDefense = caster.CurrentMagicalDefense + stealValue;
                break;

            case AppConstants.EffectProperty.CHEMICAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ChemicalAttack * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentChemicalAttack = target.CurrentChemicalAttack - stealValue;
                caster.CurrentChemicalAttack = caster.CurrentChemicalAttack + stealValue;
                break;

            case AppConstants.EffectProperty.CHEMICAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ChemicalDefense * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentChemicalDefense = target.CurrentChemicalDefense - stealValue;
                caster.CurrentChemicalDefense = caster.CurrentChemicalDefense + stealValue;
                break;

            case AppConstants.EffectProperty.ATOMIC_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.AtomicAttack * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentAtomicAttack = target.CurrentAtomicAttack - stealValue;
                caster.CurrentAtomicAttack = caster.CurrentAtomicAttack + stealValue;
                break;

            case AppConstants.EffectProperty.ATOMIC_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.AtomicDefense * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentAtomicDefense = target.CurrentAtomicDefense - stealValue;
                caster.CurrentAtomicDefense = caster.CurrentAtomicDefense + stealValue;
                break;

            case AppConstants.EffectProperty.MENTAL_ATTACK:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.MentalAttack * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentMentalAttack = target.CurrentMentalAttack - stealValue;
                caster.CurrentMentalAttack = caster.CurrentMentalAttack + stealValue;
                break;

            case AppConstants.EffectProperty.MENTAL_DEFENSE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.MentalDefense * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentMentalDefense = target.CurrentMentalDefense - stealValue;
                caster.CurrentMentalDefense = caster.CurrentMentalDefense + stealValue;
                break;

            // --- SPEED ---
            case AppConstants.EffectProperty.SPEED:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.Speed * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentSpeed = target.CurrentSpeed - stealValue;
                caster.CurrentSpeed = caster.CurrentSpeed + stealValue;
                break;

            // --- CRITICAL STATS ---
            case AppConstants.EffectProperty.CRITICAL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.CriticalDamageRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentCriticalDamageRate = target.CurrentCriticalDamageRate - stealValue;
                caster.CurrentCriticalDamageRate = caster.CurrentCriticalDamageRate + stealValue;
                break;

            case AppConstants.EffectProperty.CRITICAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.CriticalRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentCriticalRate = target.CurrentCriticalRate - stealValue;
                caster.CurrentCriticalRate = caster.CurrentCriticalRate + stealValue;
                break;

            case AppConstants.EffectProperty.CRITICAL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.CriticalResistanceRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentCriticalResistanceRate = target.CurrentCriticalResistanceRate - stealValue;
                caster.CurrentCriticalResistanceRate = caster.CurrentCriticalResistanceRate + stealValue;
                break;

            case AppConstants.EffectProperty.IGNORE_CRITICAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.IgnoreCriticalRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentIgnoreCriticalRate = target.CurrentIgnoreCriticalRate - stealValue;
                caster.CurrentIgnoreCriticalRate = caster.CurrentIgnoreCriticalRate + stealValue;
                break;

            // --- PENETRATION & EVASION ---
            case AppConstants.EffectProperty.PENETRATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.PenetrationRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentPenetrationRate = target.CurrentPenetrationRate - stealValue;
                caster.CurrentPenetrationRate = caster.CurrentPenetrationRate + stealValue;
                break;

            case AppConstants.EffectProperty.PENETRATION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.PenetrationResistanceRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentPenetrationResistanceRate = target.CurrentPenetrationResistanceRate - stealValue;
                caster.CurrentPenetrationResistanceRate = caster.CurrentPenetrationResistanceRate + stealValue;
                break;

            case AppConstants.EffectProperty.EVASION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.EvasionRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentEvasionRate = target.CurrentEvasionRate - stealValue;
                caster.CurrentEvasionRate = caster.CurrentEvasionRate + stealValue;
                break;

            // --- DAMAGE ABSORPTION ---
            case AppConstants.EffectProperty.DAMAGE_ABSORPTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.DamageAbsorptionRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentDamageAbsorptionRate = target.CurrentDamageAbsorptionRate - stealValue;
                caster.CurrentDamageAbsorptionRate = caster.CurrentDamageAbsorptionRate + stealValue;
                break;

            case AppConstants.EffectProperty.IGNORE_DAMAGE_ABSORPTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.IgnoreDamageAbsorptionRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentIgnoreDamageAbsorptionRate = target.CurrentIgnoreDamageAbsorptionRate - stealValue;
                caster.CurrentIgnoreDamageAbsorptionRate = caster.CurrentIgnoreDamageAbsorptionRate + stealValue;
                break;

            case AppConstants.EffectProperty.ABSORBED_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.AbsorbedDamageRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentAbsorbedDamageRate = target.CurrentAbsorbedDamageRate - stealValue;
                caster.CurrentAbsorbedDamageRate = caster.CurrentAbsorbedDamageRate + stealValue;
                break;

            // --- VITALITY REGENERATION ---
            case AppConstants.EffectProperty.VITALITY_REGENERATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.VitalityRegenerationRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentVitalityRegenerationRate = target.CurrentVitalityRegenerationRate - stealValue;
                caster.CurrentVitalityRegenerationRate = caster.CurrentVitalityRegenerationRate + stealValue;
                break;

            case AppConstants.EffectProperty.VITALITY_REGENERATION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.VitalityRegenerationResistanceRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentVitalityRegenerationResistanceRate = target.CurrentVitalityRegenerationResistanceRate - stealValue;
                caster.CurrentVitalityRegenerationResistanceRate = caster.CurrentVitalityRegenerationResistanceRate + stealValue;
                break;

            // --- ACCURACY & LIFESTEAL ---
            case AppConstants.EffectProperty.ACCURACY_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.AccuracyRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentAccuracyRate = target.CurrentAccuracyRate - stealValue;
                caster.CurrentAccuracyRate = caster.CurrentAccuracyRate + stealValue;
                break;

            case AppConstants.EffectProperty.LIFESTEAL_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.LifestealRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentLifestealRate = target.CurrentLifestealRate - stealValue;
                caster.CurrentLifestealRate = caster.CurrentLifestealRate + stealValue;
                break;

            // --- MANA STATS ---
            case AppConstants.EffectProperty.MANA:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.Mana * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentMana = target.CurrentMana - stealValue;
                caster.CurrentMana = caster.CurrentMana + stealValue;
                break;

            case AppConstants.EffectProperty.MANA_REGENERATION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ManaRegenerationRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentManaRegenerationRate = target.CurrentManaRegenerationRate - stealValue;
                caster.CurrentManaRegenerationRate = caster.CurrentManaRegenerationRate + stealValue;
                break;

            // --- SHIELD & CONTROL ---
            case AppConstants.EffectProperty.SHIELD_STRENGTH:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ShieldStrength * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentShieldStrength = target.CurrentShieldStrength - stealValue;
                caster.CurrentShieldStrength = caster.CurrentShieldStrength + stealValue;
                break;

            case AppConstants.EffectProperty.TENACITY:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.Tenacity * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentTenacity = target.CurrentTenacity - stealValue;
                caster.CurrentTenacity = caster.CurrentTenacity + stealValue;
                break;

            case AppConstants.EffectProperty.RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ResistanceRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentResistanceRate = target.CurrentResistanceRate - stealValue;
                caster.CurrentResistanceRate = caster.CurrentResistanceRate + stealValue;
                break;

            // --- COMBO STATS ---
            case AppConstants.EffectProperty.COMBO_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ComboRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentComboRate = target.CurrentComboRate - stealValue;
                caster.CurrentComboRate = caster.CurrentComboRate + stealValue;
                break;

            case AppConstants.EffectProperty.IGNORE_COMBO_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.IgnoreComboRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentIgnoreComboRate = target.CurrentIgnoreComboRate - stealValue;
                caster.CurrentIgnoreComboRate = caster.CurrentIgnoreComboRate + stealValue;
                break;

            case AppConstants.EffectProperty.COMBO_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ComboDamageRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentComboDamageRate = target.CurrentComboDamageRate - stealValue;
                caster.CurrentComboDamageRate = caster.CurrentComboDamageRate + stealValue;
                break;

            case AppConstants.EffectProperty.COMBO_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ComboResistanceRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentComboResistanceRate = target.CurrentComboResistanceRate - stealValue;
                caster.CurrentComboResistanceRate = caster.CurrentComboResistanceRate + stealValue;
                break;

            // --- STUN STATS ---
            case AppConstants.EffectProperty.STUN_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.StunRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentStunRate = target.CurrentStunRate - stealValue;
                caster.CurrentStunRate = caster.CurrentStunRate + stealValue;
                break;

            case AppConstants.EffectProperty.IGNORE_STUN_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.IgnoreStunRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentIgnoreStunRate = target.CurrentIgnoreStunRate - stealValue;
                caster.CurrentIgnoreStunRate = caster.CurrentIgnoreStunRate + stealValue;
                break;

            // --- REFLECTION STATS ---
            case AppConstants.EffectProperty.REFLECTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ReflectionRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentReflectionRate = target.CurrentReflectionRate - stealValue;
                caster.CurrentReflectionRate = caster.CurrentReflectionRate + stealValue;
                break;

            case AppConstants.EffectProperty.IGNORE_REFLECTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.IgnoreReflectionRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentIgnoreReflectionRate = target.CurrentIgnoreReflectionRate - stealValue;
                caster.CurrentIgnoreReflectionRate = caster.CurrentIgnoreReflectionRate + stealValue;
                break;

            case AppConstants.EffectProperty.REFLECTION_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ReflectionDamageRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentReflectionDamageRate = target.CurrentReflectionDamageRate - stealValue;
                caster.CurrentReflectionDamageRate = caster.CurrentReflectionDamageRate + stealValue;
                break;

            case AppConstants.EffectProperty.REFLECTION_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ReflectionResistanceRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentReflectionResistanceRate = target.CurrentReflectionResistanceRate - stealValue;
                caster.CurrentReflectionResistanceRate = caster.CurrentReflectionResistanceRate + stealValue;
                break;

            // --- FACTION INTERACTIONS ---
            case AppConstants.EffectProperty.DAMAGE_TO_DIFFERENT_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.DamageToDifferentFactionRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentDamageToDifferentFactionRate = target.CurrentDamageToDifferentFactionRate - stealValue;
                caster.CurrentDamageToDifferentFactionRate = caster.CurrentDamageToDifferentFactionRate + stealValue;
                break;

            case AppConstants.EffectProperty.RESISTANCE_TO_DIFFERENT_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ResistanceToDifferentFactionRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentResistanceToDifferentFactionRate = target.CurrentResistanceToDifferentFactionRate - stealValue;
                caster.CurrentResistanceToDifferentFactionRate = caster.CurrentResistanceToDifferentFactionRate + stealValue;
                break;

            case AppConstants.EffectProperty.DAMAGE_TO_SAME_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.DamageToSameFactionRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentDamageToSameFactionRate = target.CurrentDamageToSameFactionRate - stealValue;
                caster.CurrentDamageToSameFactionRate = caster.CurrentDamageToSameFactionRate + stealValue;
                break;

            case AppConstants.EffectProperty.RESISTANCE_TO_SAME_FACTION_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.ResistanceToSameFactionRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentResistanceToSameFactionRate = target.CurrentResistanceToSameFactionRate - stealValue;
                caster.CurrentResistanceToSameFactionRate = caster.CurrentResistanceToSameFactionRate + stealValue;
                break;

            // --- NORMAL VS SKILL DAMAGE ---
            case AppConstants.EffectProperty.NORMAL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.NormalDamageRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentNormalDamageRate = target.CurrentNormalDamageRate - stealValue;
                caster.CurrentNormalDamageRate = caster.CurrentNormalDamageRate + stealValue;
                break;

            case AppConstants.EffectProperty.NORMAL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.NormalResistanceRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentNormalResistanceRate = target.CurrentNormalResistanceRate - stealValue;
                caster.CurrentNormalResistanceRate = caster.CurrentNormalResistanceRate + stealValue;
                break;

            case AppConstants.EffectProperty.SKILL_DAMAGE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.SkillDamageRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentSkillDamageRate = target.CurrentSkillDamageRate - stealValue;
                caster.CurrentSkillDamageRate = caster.CurrentSkillDamageRate + stealValue;
                break;

            case AppConstants.EffectProperty.SKILL_RESISTANCE_RATE:
                if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    stealValue = target.SkillResistanceRate * effect.Value / 100f;
                }
                else if (effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    stealValue = effect.Value;
                }
                target.CurrentSkillResistanceRate = target.CurrentSkillResistanceRate - stealValue;
                caster.CurrentSkillResistanceRate = caster.CurrentSkillResistanceRate + stealValue;
                break;
        }
    }

    private static void ExecuteCopy(Effects effect, CardBase caster, CardBase target)
    {
        if (effect == null || target == null || caster == null) return;

        switch (effect.EffectProperty.PropertyCode)
        {
            // --- HEALTH ---
            case AppConstants.EffectProperty.HEALTH:
                caster.CurrentHealth = target.CurrentHealth;
                break;

            // --- ATTACK STATS ---
            case AppConstants.EffectProperty.PHYSICAL_ATTACK:
                caster.CurrentPhysicalAttack = target.CurrentPhysicalAttack;
                break;

            // --- DEFENSE STATS ---
            case AppConstants.EffectProperty.PHYSICAL_DEFENSE:
                caster.CurrentPhysicalDefense = target.CurrentPhysicalDefense;
                break;

            case AppConstants.EffectProperty.MAGICAL_ATTACK:
                caster.CurrentMagicalAttack = target.CurrentMagicalAttack;
                break;

            case AppConstants.EffectProperty.MAGICAL_DEFENSE:
                caster.CurrentMagicalDefense = target.CurrentMagicalDefense;
                break;

            case AppConstants.EffectProperty.CHEMICAL_ATTACK:
                caster.CurrentChemicalAttack = target.CurrentChemicalAttack;
                break;

            case AppConstants.EffectProperty.CHEMICAL_DEFENSE:
                caster.CurrentChemicalDefense = target.CurrentChemicalDefense;
                break;

            case AppConstants.EffectProperty.ATOMIC_ATTACK:
                caster.CurrentAtomicAttack = target.CurrentAtomicAttack;
                break;

            case AppConstants.EffectProperty.ATOMIC_DEFENSE:
                caster.CurrentAtomicDefense = target.CurrentAtomicDefense;
                break;

            case AppConstants.EffectProperty.MENTAL_ATTACK:
                caster.CurrentMentalAttack = target.CurrentMentalAttack;
                break;

            case AppConstants.EffectProperty.MENTAL_DEFENSE:
                caster.CurrentMentalDefense = target.CurrentMentalDefense;
                break;

            // --- SPEED ---
            case AppConstants.EffectProperty.SPEED:
                caster.CurrentSpeed = target.CurrentSpeed;
                break;

            // --- CRITICAL STATS ---
            case AppConstants.EffectProperty.CRITICAL_DAMAGE_RATE:
                caster.CurrentCriticalDamageRate = target.CurrentCriticalDamageRate;
                break;

            case AppConstants.EffectProperty.CRITICAL_RATE:
                caster.CurrentCriticalRate = target.CurrentCriticalRate;
                break;

            case AppConstants.EffectProperty.CRITICAL_RESISTANCE_RATE:
                caster.CurrentCriticalResistanceRate = target.CurrentCriticalResistanceRate;
                break;

            case AppConstants.EffectProperty.IGNORE_CRITICAL_RATE:
                caster.CurrentIgnoreCriticalRate = target.CurrentIgnoreCriticalRate;
                break;

            // --- PENETRATION & EVASION ---
            case AppConstants.EffectProperty.PENETRATION_RATE:
                caster.CurrentPenetrationRate = target.CurrentPenetrationRate;
                break;

            case AppConstants.EffectProperty.PENETRATION_RESISTANCE_RATE:
                caster.CurrentPenetrationResistanceRate = target.CurrentPenetrationResistanceRate;
                break;

            case AppConstants.EffectProperty.EVASION_RATE:
                caster.CurrentEvasionRate = target.CurrentEvasionRate;
                break;

            // --- DAMAGE ABSORPTION ---
            case AppConstants.EffectProperty.DAMAGE_ABSORPTION_RATE:
                caster.CurrentDamageAbsorptionRate = target.CurrentDamageAbsorptionRate;
                break;

            case AppConstants.EffectProperty.IGNORE_DAMAGE_ABSORPTION_RATE:
                caster.CurrentIgnoreDamageAbsorptionRate = target.CurrentIgnoreDamageAbsorptionRate;
                break;

            case AppConstants.EffectProperty.ABSORBED_DAMAGE_RATE:
                caster.CurrentAbsorbedDamageRate = target.CurrentAbsorbedDamageRate;
                break;

            // --- VITALITY REGENERATION ---
            case AppConstants.EffectProperty.VITALITY_REGENERATION_RATE:
                caster.CurrentVitalityRegenerationRate = target.CurrentVitalityRegenerationRate;
                break;

            case AppConstants.EffectProperty.VITALITY_REGENERATION_RESISTANCE_RATE:
                caster.CurrentVitalityRegenerationResistanceRate = target.CurrentVitalityRegenerationResistanceRate;
                break;

            // --- ACCURACY & LIFESTEAL ---
            case AppConstants.EffectProperty.ACCURACY_RATE:
                caster.CurrentAccuracyRate = target.CurrentAccuracyRate;
                break;

            case AppConstants.EffectProperty.LIFESTEAL_RATE:
                caster.CurrentLifestealRate = target.CurrentLifestealRate;
                break;

            // --- MANA STATS ---
            case AppConstants.EffectProperty.MANA:
                caster.CurrentMana = target.CurrentMana;
                break;

            case AppConstants.EffectProperty.MANA_REGENERATION_RATE:
                caster.CurrentManaRegenerationRate = target.CurrentManaRegenerationRate;
                break;

            // --- SHIELD & CONTROL ---
            case AppConstants.EffectProperty.SHIELD_STRENGTH:
                caster.CurrentShieldStrength = target.CurrentShieldStrength;
                break;

            case AppConstants.EffectProperty.TENACITY:
                caster.CurrentTenacity = target.CurrentTenacity;
                break;

            case AppConstants.EffectProperty.RESISTANCE_RATE:
                caster.CurrentResistanceRate = target.CurrentResistanceRate;
                break;

            // --- COMBO STATS ---
            case AppConstants.EffectProperty.COMBO_RATE:
                caster.CurrentComboRate = target.CurrentComboRate;
                break;

            case AppConstants.EffectProperty.IGNORE_COMBO_RATE:
                caster.CurrentIgnoreComboRate = target.CurrentIgnoreComboRate;
                break;

            case AppConstants.EffectProperty.COMBO_DAMAGE_RATE:
                caster.CurrentComboDamageRate = target.CurrentComboDamageRate;
                break;

            case AppConstants.EffectProperty.COMBO_RESISTANCE_RATE:
                caster.CurrentComboResistanceRate = target.CurrentComboResistanceRate;
                break;

            // --- STUN STATS ---
            case AppConstants.EffectProperty.STUN_RATE:
                caster.CurrentStunRate = target.CurrentStunRate;
                break;

            case AppConstants.EffectProperty.IGNORE_STUN_RATE:
                caster.CurrentIgnoreStunRate = target.CurrentIgnoreStunRate;
                break;

            // --- REFLECTION STATS ---
            case AppConstants.EffectProperty.REFLECTION_RATE:
                caster.CurrentReflectionRate = target.CurrentReflectionRate;
                break;

            case AppConstants.EffectProperty.IGNORE_REFLECTION_RATE:
                caster.CurrentIgnoreReflectionRate = target.CurrentIgnoreReflectionRate;
                break;

            case AppConstants.EffectProperty.REFLECTION_DAMAGE_RATE:
                caster.CurrentReflectionDamageRate = target.CurrentReflectionDamageRate;
                break;

            case AppConstants.EffectProperty.REFLECTION_RESISTANCE_RATE:
                caster.CurrentReflectionResistanceRate = target.CurrentReflectionResistanceRate;
                break;

            // --- FACTION INTERACTIONS ---
            case AppConstants.EffectProperty.DAMAGE_TO_DIFFERENT_FACTION_RATE:
                caster.CurrentDamageToDifferentFactionRate = target.CurrentDamageToDifferentFactionRate;
                break;

            case AppConstants.EffectProperty.RESISTANCE_TO_DIFFERENT_FACTION_RATE:
                caster.CurrentResistanceToDifferentFactionRate = target.CurrentResistanceToDifferentFactionRate;
                break;

            case AppConstants.EffectProperty.DAMAGE_TO_SAME_FACTION_RATE:
                caster.CurrentDamageToSameFactionRate = target.CurrentDamageToSameFactionRate;
                break;

            case AppConstants.EffectProperty.RESISTANCE_TO_SAME_FACTION_RATE:
                caster.CurrentResistanceToSameFactionRate = target.CurrentResistanceToSameFactionRate;
                break;

            // --- NORMAL VS SKILL DAMAGE ---
            case AppConstants.EffectProperty.NORMAL_DAMAGE_RATE:
                caster.CurrentNormalDamageRate = target.CurrentNormalDamageRate;
                break;

            case AppConstants.EffectProperty.NORMAL_RESISTANCE_RATE:
                caster.CurrentNormalResistanceRate = target.CurrentNormalResistanceRate;
                break;

            case AppConstants.EffectProperty.SKILL_DAMAGE_RATE:
                caster.CurrentSkillDamageRate = target.CurrentSkillDamageRate;
                break;

            case AppConstants.EffectProperty.SKILL_RESISTANCE_RATE:
                caster.CurrentSkillResistanceRate = target.CurrentSkillResistanceRate;
                break;
        }
    }

    private static void ExecuteReverse(Effects effect, CardBase target)
    {
        // 1. Kiểm tra an toàn đầu vào
        if (effect == null || target == null || effect.Value <= 0) return;

        System.Random rand = new System.Random();

        // Danh sách tạm để ghi nhớ những ông VỪA BỊ ĐẢO NGƯỢC trong lượt gọi hàm này
        HashSet<RuntimeEffectInstance> reversedInThisTurn = new HashSet<RuntimeEffectInstance>();

        // Chạy vòng lặp số lần bằng với effect.Value
        for (int i = 0; i < effect.Value; i++)
        {
            // 2. Lấy danh sách ứng viên và loại trừ những ông đã bị đảo ngược trước đó (.Except)
            var reversibleFriendly = target.FriendlyEffects
                .Where(e => e.IsActive && e.SourceEffect?.EffectAction?.ActionCode == AppConstants.EffectAction.INCREASE)
                .Except(reversedInThisTurn)
                .ToList();

            var reversibleHostile = target.HostileEffects
                .Where(e => e.IsActive && e.SourceEffect?.EffectAction?.ActionCode == AppConstants.EffectAction.DECREASE)
                .Except(reversedInThisTurn)
                .ToList();

            // Gom tất cả ứng viên hợp lệ còn lại
            var allCandidates = reversibleFriendly.Concat(reversibleHostile).ToList();

            // Nếu không tìm thấy hiệu ứng INCREASE hoặc DECREASE nào CHƯA BỊ ĐỤNG ĐẾN -> Thoát vòng lặp luôn
            if (allCandidates.Count == 0)
            {
                Debug.Log("<color=white>[Reverse]</color> Không còn hiệu ứng hợp lệ nào chưa xử lý để đảo ngược. Dừng sớm!");
                break;
            }

            // 3. Bốc ngẫu nhiên 1 hiệu ứng từ danh sách ứng viên sạch
            var chosenEffect = allCandidates[rand.Next(allCandidates.Count)];

            // Đưa ngay vào danh sách "đã xử lý" để vòng lặp sau không đụng lại nó nữa
            reversedInThisTurn.Add(chosenEffect);

            // 4. Tiến hành hoán đổi ActionCode và dịch chuyển List
            if (target.FriendlyEffects.Contains(chosenEffect))
            {
                chosenEffect.SourceEffect.EffectAction.ActionCode = AppConstants.EffectAction.DECREASE;

                target.FriendlyEffects.Remove(chosenEffect);
                target.HostileEffects.Add(chosenEffect);

                // Debug.Log($"<color=red>[Reversed]</color> Đã đảo [{chosenEffect.SourceEffect.Name}] từ INCREASE -> DECREASE và chuyển sang HostileEffects!");
            }
            else if (target.HostileEffects.Contains(chosenEffect))
            {
                chosenEffect.SourceEffect.EffectAction.ActionCode = AppConstants.EffectAction.INCREASE;

                target.HostileEffects.Remove(chosenEffect);
                target.FriendlyEffects.Add(chosenEffect);

                // Debug.Log($"<color=green>[Reversed]</color> Đã đảo [{chosenEffect.SourceEffect.Name}] từ DECREASE -> INCREASE và chuyển sang FriendlyEffects!");
            }
        }
    }

    private static void ExecuteReflect(Effects effect, CardBase caster, CardBase target)
    {
        // 1. Kiểm tra an toàn đầu vào và số lần phản (Value)
        if (effect == null || caster == null || target == null || effect.Value <= 0) return;
        if (caster.HostileEffects == null || caster.HostileEffects.Count == 0) return;

        // Lấy PropertyCode cần phản lại từ cấu hình của chiêu thức REFLECT
        var targetPropertyCode = effect.EffectProperty?.PropertyCode;
        if (string.IsNullOrEmpty(targetPropertyCode)) return;

        // 2. Chạy vòng lặp số lần bằng với effect.Value
        for (int i = 0; i < effect.Value; i++)
        {
            // Tìm hiệu ứng xấu ĐẦU TIÊN thỏa điều kiện tại thời điểm của vòng lặp này
            // (Thêm điều kiện !e.IsReflected nếu ní có làm flag chống phản vô hạn như tui gợi ý trước đó)
            var effectToReflect = caster.HostileEffects.FirstOrDefault(e =>
                e.IsActive && e.SourceEffect?.EffectProperty?.PropertyCode == targetPropertyCode);

            // Nếu tìm thấy hiệu ứng hợp lệ, tiến hành "ném" nó sang cho đối thủ
            if (effectToReflect != null)
            {
                // Xóa khỏi danh sách bất lợi của bản thân
                caster.HostileEffects.Remove(effectToReflect);

                // Đẩy sang danh sách bất lợi của kẻ địch (target)
                if (target.HostileEffects == null) target.HostileEffects = new List<RuntimeEffectInstance>();
                target.HostileEffects.Add(effectToReflect);

                // Debug.Log($"<color=magenta>[Reflect Loop]</color> Lượt {i + 1}: [{caster.Name}] đã phản thành công hiệu ứng [{effectToReflect.SourceEffect.Name}] về [{target.Name}]!");
            }
            else
            {
                // Nếu quét trong list không còn ông nào trùng PropertyCode nữa thì dừng sớm cho nhẹ máy
                // Debug.Log($"<color=white>[Reflect Loop]</color> Không còn hiệu ứng xấu nào trùng thuộc tính {targetPropertyCode} để phản. Dừng vòng lặp!");
                break;
            }
        }
    }

    private static void ExecuteRemove(Effects effect, CardBase target)
    {
        if (effect == null || target == null) return;

        if (target.HostileEffects != null)
        {
            target.HostileEffects.RemoveAll(activeEffect =>
            activeEffect.SourceEffect != null &&
            activeEffect.SourceEffect.EffectProperty.PropertyCode == effect.EffectProperty.PropertyCode);
        }
    }

    private static void ExecuteExtend(Effects effect, CardBase target)
    {
        if (effect == null || target == null) return;

        if (target.FriendlyEffects != null)
        {
            foreach (var instance in target.FriendlyEffects)
            {
                if (instance.SourceEffect != null
                    && instance.SourceEffect.EffectProperty.PropertyCode == effect.EffectProperty.PropertyCode
                    && effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    instance.RemainingDuration += effect.Value;
                }

                if (instance.SourceEffect != null
                    && instance.SourceEffect.EffectProperty.PropertyCode == effect.EffectProperty.PropertyCode
                    && effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    instance.RemainingDuration += Mathf.RoundToInt(instance.RemainingDuration + effect.Value / 100);
                }
            }
        }

        if (target.HostileEffects != null)
        {
            foreach (var instance in target.HostileEffects)
            {
                if (instance.SourceEffect != null
                    && instance.SourceEffect.EffectProperty.PropertyCode == effect.EffectProperty.PropertyCode
                    && effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    instance.RemainingDuration += effect.Value;
                }

                if (instance.SourceEffect != null
                    && instance.SourceEffect.EffectProperty.PropertyCode == effect.EffectProperty.PropertyCode
                    && effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    instance.RemainingDuration += Mathf.RoundToInt(instance.RemainingDuration + effect.Value / 100);
                }
            }
        }
    }

    private static void ExecuteShorten(Effects effect, CardBase target)
    {
        if (effect == null || target == null) return;

        if (target.FriendlyEffects != null)
        {
            foreach (var instance in target.FriendlyEffects)
            {
                if (instance.SourceEffect != null
                    && instance.SourceEffect.EffectProperty.PropertyCode == effect.EffectProperty.PropertyCode
                    && effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    instance.RemainingDuration -= effect.Value;
                }

                if (instance.SourceEffect != null
                    && instance.SourceEffect.EffectProperty.PropertyCode == effect.EffectProperty.PropertyCode
                    && effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    instance.RemainingDuration -= Mathf.RoundToInt(instance.RemainingDuration + effect.Value / 100);
                }
            }

            target.FriendlyEffects.RemoveAll(instance => instance.RemainingDuration <= 0);
        }

        if (target.HostileEffects != null)
        {
            foreach (var instance in target.HostileEffects)
            {
                if (instance.SourceEffect != null
                    && instance.SourceEffect.EffectProperty.PropertyCode == effect.EffectProperty.PropertyCode
                    && effect.ValueType.Equals(AppConstants.EffectPropertyAction.FLAT))
                {
                    instance.RemainingDuration -= effect.Value;
                }

                if (instance.SourceEffect != null
                    && instance.SourceEffect.EffectProperty.PropertyCode == effect.EffectProperty.PropertyCode
                    && effect.ValueType.Equals(AppConstants.EffectPropertyAction.PERCENTAGE))
                {
                    instance.RemainingDuration -= Mathf.RoundToInt(instance.RemainingDuration + effect.Value / 100);
                }
            }

            target.HostileEffects.RemoveAll(instance => instance.RemainingDuration <= 0);
        }
    }

    private static void ExecuteDamage(Effects effect, CardBase caster, CardBase target)
    {
        if (effect == null || target == null || caster == null) return;

        if (caster.isSkillAttack)
        {
            DamageCalculator.CausePhysicalSkillAttack(caster, target);
            DamageCalculator.CauseMagicalSkillAttack(caster, target);
            DamageCalculator.CauseChemicalSkillAttack(caster, target);
            DamageCalculator.CauseAtomicSkillAttack(caster, target);
            DamageCalculator.CauseMentalSkillAttack(caster, target);
        }
        else
        {
            DamageCalculator.CausePhysicalNormalAttack(caster, target);
            DamageCalculator.CauseMagicalNormalAttack(caster, target);
            DamageCalculator.CauseChemicalNormalAttack(caster, target);
            DamageCalculator.CauseAtomicNormalAttack(caster, target);
            DamageCalculator.CauseMentalNormalAttack(caster, target);
        }
    }

    private static void ExecuteHeal(Effects effect, CardBase caster, CardBase target)
    {
        if (effect == null || target == null || caster == null) return;
        double totalDamage = caster.CurrentPhysicalAttack
                           + caster.CurrentMagicalAttack
                           + caster.CurrentChemicalAttack
                           + caster.CurrentAtomicAttack
                           + caster.CurrentMentalAttack;
        totalDamage = totalDamage / 5;

        System.Random rand = new System.Random();
        double randomValue = rand.Next((int)effect.MinValue, (int)effect.MaxValue + 1);
        double healAmount = randomValue * (totalDamage / 100.0);
        target.CurrentHealth = target.CurrentHealth + healAmount;
    }

    private static void ExecuteLimitAction(Effects effect, CardBase target)
    {
        // Hạn chế hành động (Cấm đánh thường, hoặc cấm dùng Active Skill)
        Debug.Log($"🚫 {target.Name} dính trạng thái LIMIT_ACTION.");
    }

    /// <summary>
    /// Hàm hoàn trả lại chỉ số khi hiệu ứng hết thời gian duy trì (gọi tại Turn Lifecycle)
    /// </summary>
    public static void RevertExpiredEffect(CardBase target, Effects effect)
    {
        if (effect.EffectAction?.ActionCode?.ToUpper() == AppConstants.EffectAction.INCREASE)
        {
            // Nếu trước đó tăng chỉ số, khi hết hạn phải trừ đi lượng đã tăng
            // EffectProperty.ModifyCardAttribute(target, effect.PropertyCode, -effect.Value);
        }
        else if (effect.EffectAction?.ActionCode?.ToUpper() == AppConstants.EffectAction.DECREASE)
        {
            // Nếu trước đó giảm chỉ số, khi hết hạn phải cộng trả lại lượng chỉ số gốc
            // EffectProperty.ModifyCardAttribute(target, effect.PropertyCode, effect.Value);
        }
    }

    private static void HandleDeath(CardBase target)
    {
        Debug.Log($"<color=black><b>[Tử trận]</b></color> {target.Name} đã bị tiêu diệt!");
        // Gọi Event xử lý clear thẻ bài khỏi GridCell tại đây thông qua GridManager
    }
}
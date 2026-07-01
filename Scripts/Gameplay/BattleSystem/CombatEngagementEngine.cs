using System.Collections.Generic;
using UnityEngine;

public class CombatEngagementEngine : MonoBehaviour
{
    public static CombatEngagementEngine Instance { get; private set; }

    private void Awake() => Instance = this;

    /// <summary>
    /// KÍCH HOẠT CHỦ ĐỘNG: Đã đổi tham số từ string patternId sang đối tượng dữ liệu động Patterns
    /// </summary>
    public void ExecuteActiveSkill(CardBase caster, GridCell casterCell, GridCell castTargetCell, List<Effects> skillEffects, Patterns skillPattern)
    {
        Debug.Log($"<color=yellow><b>[Active Action]</b></color> {caster.Name} bắt đầu triển khai kỹ năng chủ động!");

        // 1. Duyệt qua toàn bộ Effect trong kỹ năng chủ động (Thường nằm ở Phase: MAIN)
        foreach (var effect in skillEffects)
        {
            if (effect.TriggerPhase.ToUpper() != "MAIN") continue;

            // 2. SỬA TẠI ĐÂY: Truyền cấu trúc dữ liệu động skillPattern vào thay vì patternId dạng string
            List<CardBase> targets = CombatTargetSelector.GetAffectedTargets(effect.Target.Id, casterCell, castTargetCell, skillPattern);

            // 3. Áp dụng hiệu ứng chủ động lên từng mục tiêu
            foreach (var target in targets)
            {
                // CombatEffectProcessor.ApplyEffect(effect, caster, target);

                // --- MÓC NỐI KÍCH HOẠT BỊ ĐỘNG (PASSIVE TRIGGERS) ---
                // Sau khi dính hiệu ứng chủ động, lập tức kiểm tra nội tại 'ON_HIT' (Bị đánh) của nạn nhân
                TriggerPassiveEffects(target, caster, "MAIN", "ON_HIT");
            }
        }

        // Sau khi kết thúc loạt chiêu chủ động, kích hoạt nội tại 'ON_ATTACK' (Khi tấn công xong) của bản thân
        TriggerPassiveEffects(caster, null, "MAIN", "ON_ATTACK");
    }

    /// <summary>
    /// KÍCH HOẠT BỊ ĐỘNG (PASSIVE): Tự động quét và thực thi hiệu ứng dựa trên nội tại của Card
    /// </summary>
    public void TriggerPassiveEffects(CardBase owner, CardBase attacker, string currentPhase, string condition)
    {
        // 1. Kiểm tra xem thẻ bài hiện tại có sở hữu hiệu ứng nội tại nào trùng khớp điều kiện kích hoạt hay không
        if (owner.PassiveEffects == null || owner.PassiveEffects.Count == 0) return;

        GridCell ownerCell = GridManager.Instance.GetCellOfCard(owner);
        GridCell attackerCell = attacker != null ? GridManager.Instance.GetCellOfCard(attacker) : null;

        foreach (var passEffect in owner.PassiveEffects)
        {
            // Kiểm tra tính hợp lệ của thời điểm kích hoạt nội tại (Ví dụ: Phase = START/MAIN, Condition = ON_HIT)
            if (passEffect.TriggerPhase.ToUpper() == currentPhase.ToUpper() && 
                passEffect.TriggerCondition.ToUpper() == condition.ToUpper())
            {
                Debug.Log($"<color=cyan>[Passive Triggered]</color> Nội tại {passEffect.Name} của {owner.Name} được kích hoạt theo điều kiện {condition}!");

                // LÝ DO KHÔNG CẦN TRUYỀN PATTERN Ở ĐÂY:
                // Các chiêu nội tại (Passive) phản đòn (ON_HIT) hoặc tăng công khi đánh (ON_ATTACK) thường có target_id là "CASTER" (bản thân) hoặc "CAST_TARGET" (kẻ đánh mình).
                // Do đó, ta truyền null vào tham số Pattern của hàm GetAffectedTargets, hàm selector sẽ tự hiểu để chỉ lấy đúng 1 mục tiêu đơn lẻ.
                List<CardBase> passiveTargets = CombatTargetSelector.GetAffectedTargets(passEffect.Target.Id, ownerCell, attackerCell, null);

                foreach (var pTarget in passiveTargets)
                {
                    // CombatEffectProcessor.ApplyEffect(passEffect, owner, pTarget);
                }
            }
        }
    }
}
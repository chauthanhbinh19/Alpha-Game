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

            // 2. Lấy mục tiêu chịu ảnh hưởng dựa trên target_id / Skill Pattern
            List<CardBase> targets = CombatTargetSelector.GetAffectedTargets(effect.Target.Id, casterCell, castTargetCell, skillPattern);

            // 3. Áp dụng hiệu ứng chủ động lên từng mục tiêu
            foreach (var target in targets)
            {
                CombatEffectProcessor.ApplyEffects(new List<Effects> { effect }, caster, target);
            }
        }

        // 4. Kích hoạt nội tại ON_ATTACK cho caster sau khi kỹ năng chủ động kết thúc
        TriggerPassiveEffects(caster, null, "MAIN", AppConstants.TriggerCondition.ON_ATTACK);
    }

    public void HandleAttackOutcome(CardBase attacker, CardBase target, DamageCalculator.AttackOutcome attackOutcome, string phase = "MAIN")
    {
        if (attacker == null || target == null)
        {
            return;
        }

        // Người tấn công kích hoạt ON_ATTACK khi bắt đầu hành động tấn công
        TriggerPassiveEffects(attacker, target, phase, AppConstants.TriggerCondition.ON_ATTACK);
        // TriggerAllyEvent(attacker, phase, AppConstants.TriggerCondition.ON_ALLY_ATTACK);

        // Mục tiêu bị nhắm đến luôn kích hoạt ON_BE_ATTACKED
        TriggerPassiveEffects(target, attacker, phase, AppConstants.TriggerCondition.ON_BE_ATTACKED);

        if (attackOutcome.IsMiss)
        {
            TriggerPassiveEffects(attacker, target, phase, AppConstants.TriggerCondition.ON_MISS);
            TriggerPassiveEffects(target, attacker, phase, AppConstants.TriggerCondition.ON_DODGE);
        }

        if (attackOutcome.IsHit)
        {
            TriggerPassiveEffects(attacker, target, phase, AppConstants.TriggerCondition.ON_HIT);
            TriggerPassiveEffects(target, attacker, phase, AppConstants.TriggerCondition.ON_BE_HIT);
            // TriggerAllyEvent(target, phase, AppConstants.TriggerCondition.ON_ALLY_BE_HIT);

            if (attackOutcome.IsCrit)
            {
                TriggerPassiveEffects(attacker, target, phase, AppConstants.TriggerCondition.ON_CRIT);
                TriggerPassiveEffects(target, attacker, phase, AppConstants.TriggerCondition.ON_BE_CRIT);
            }
        }

        if (attackOutcome.Damage > 0 && !target.IsAlive)
        {
            TriggerPassiveEffects(attacker, target, phase, AppConstants.TriggerCondition.ON_KILL);
            TriggerPassiveEffects(target, attacker, phase, AppConstants.TriggerCondition.ON_DEATH);
            // TriggerAllyEvent(target, phase, AppConstants.TriggerCondition.ON_ALLY_DEATH);
        }
    }

    public void HandleHealOutcome(CardBase caster, CardBase target, string phase = "MAIN")
    {
        if (caster == null || target == null) return;

        TriggerPassiveEffects(caster, target, phase, AppConstants.TriggerCondition.ON_HEAL);
        TriggerPassiveEffects(target, caster, phase, AppConstants.TriggerCondition.ON_BE_HEALED);
        // TriggerPassiveEffects(caster, target, phase, AppConstants.TriggerCondition.ON_ALLY);

        if (!string.IsNullOrWhiteSpace(caster.TeamId) && caster.TeamId == target.TeamId && caster.Id != target.Id)
        {
            // TriggerPassiveEffects(caster, target, phase, AppConstants.TriggerCondition.ON_ALLY_HEAL);
        }
    }

    private void TriggerAllyEvent(CardBase source, string phase, string triggerCondition)
    {
        if (source == null || string.IsNullOrWhiteSpace(triggerCondition)) return;
        if (TurnManager.Instance == null) return;

        foreach (CardBase card in TurnManager.Instance.AllCards)
        {
            if (card == null || card == source || !card.IsAlive) continue;
            if (!string.IsNullOrWhiteSpace(card.TeamId) && card.TeamId == source.TeamId)
            {
                TriggerPassiveEffects(card, source, phase, triggerCondition);
            }
        }
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
                    CombatEffectProcessor.ApplyEffects(new List<Effects> { passEffect }, owner, pTarget);
                }
            }
        }
    }
}
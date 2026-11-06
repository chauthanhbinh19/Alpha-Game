using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatEngagementEngine : MonoBehaviour
{
    private const int MaxDeferredCombatEvents = 128;

    public static CombatEngagementEngine Instance { get; private set; }

    private bool isResolvingPassiveEffects;
    private bool isProcessingCombatEvents;
    private readonly Queue<Action> deferredCombatEvents = new Queue<Action>();
    private readonly HashSet<CardBase> battleStartPassivesApplied = new HashSet<CardBase>();

    private void Awake() => Instance = this;

    public void InitializeBattlePassives(IEnumerable<CardBase> cards)
    {
        if (cards == null) return;

        foreach (CardBase card in cards)
        {
            if (card == null) continue;

            if (card.ActiveSkillEffects == null) card.ActiveSkillEffects = new List<Effects>();
            if (card.PassiveEffects == null) card.PassiveEffects = new List<Effects>();

            if (card.Skills == null) continue;
            foreach (Skills skill in card.Skills)
            {
                if (skill == null || skill.Effects == null) continue;

                if (string.Equals(skill.SkillType, "ACTIVE", StringComparison.OrdinalIgnoreCase))
                {
                    AddUniqueEffects(card.ActiveSkillEffects, skill.Effects);
                }
                else if (string.Equals(skill.SkillType, "PASSIVE", StringComparison.OrdinalIgnoreCase))
                {
                    AddUniqueEffects(card.PassiveEffects, skill.Effects);
                }
            }
        }
    }

    private static void AddUniqueEffects(List<Effects> destination, IEnumerable<Effects> effects)
    {
        foreach (Effects effect in effects)
        {
            if (effect == null || destination.Contains(effect)) continue;
            if (!string.IsNullOrEmpty(effect.Id)
                && destination.Exists(existing => existing != null && existing.Id == effect.Id))
            {
                continue;
            }

            destination.Add(effect);
        }
    }

    public void ApplyBattleStartPassives(IEnumerable<CardBase> cards)
    {
        if (cards == null) return;

        foreach (CardBase owner in cards)
        {
            if (owner == null || !owner.IsAlive || owner.PassiveEffects == null
                || !battleStartPassivesApplied.Add(owner))
            {
                continue;
            }

            GridCell ownerCell = GridManager.Instance != null ? GridManager.Instance.GetCellOfCard(owner) : null;
            foreach (Effects effect in owner.PassiveEffects)
            {
                if (effect == null) continue;

                bool isBattleStartEffect = string.IsNullOrWhiteSpace(effect.TriggerCondition)
                    || string.Equals(effect.TriggerCondition, AppConstants.TriggerCondition.ON_BATTLE_START, StringComparison.OrdinalIgnoreCase);
                if (!isBattleStartEffect) continue;

                List<CardBase> targets = CombatTargetSelector.GetAffectedTargets(
                    effect.Target != null ? effect.Target.Id : null,
                    ownerCell,
                    null,
                    null);

                foreach (CardBase target in targets)
                {
                    CombatEffectProcessor.ApplyEffects(new List<Effects> { effect }, owner, target);
                }
            }
        }
    }

    public bool ExecuteActiveSkill(CardBase caster, GridCell casterCell, GridCell castTargetCell, Skills skill)
    {
        if (caster == null || casterCell == null || skill == null || skill.Effects == null || skill.Effects.Count == 0)
        {
            Debug.LogWarning("[CombatEngagementEngine] Cannot execute an active skill without a caster, cell, and effects.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(skill.SkillType)
            && !string.Equals(skill.SkillType, "ACTIVE", StringComparison.OrdinalIgnoreCase))
        {
            Debug.LogWarning($"[CombatEngagementEngine] Skill {skill.Name} is not active and cannot be manually executed.");
            return false;
        }

        bool previousSkillAttackState = caster.isSkillAttack;
        bool hasTriggeredDamageOutcome = false;
        caster.isSkillAttack = true;
        try
        {
            foreach (Effects effect in skill.Effects)
            {
                if (effect == null || !MatchesActiveSkillPhase(effect.TriggerPhase)) continue;

                List<CardBase> targets = CombatTargetSelector.GetAffectedTargets(
                    effect.Target != null ? effect.Target.Id : null,
                    casterCell,
                    castTargetCell,
                    skill.Pattern);

                if (targets.Count > 0 && string.Equals(
                    effect.EffectAction != null ? effect.EffectAction.ActionCode : null,
                    AppConstants.EffectAction.DAMAGE,
                    StringComparison.OrdinalIgnoreCase))
                {
                    hasTriggeredDamageOutcome = true;
                }

                foreach (CardBase target in targets)
                {
                    if (!IsValidSkillTarget(skill, caster, target)) continue;
                    CombatEffectProcessor.ApplyEffects(new List<Effects> { effect }, caster, target);
                }
            }

            if (!hasTriggeredDamageOutcome)
            {
                TriggerPassiveEffects(caster, castTargetCell != null ? castTargetCell.OccupiedCard : null,
                    "MAIN", AppConstants.TriggerCondition.ON_ATTACK);
            }
        }
        finally
        {
            caster.isSkillAttack = previousSkillAttackState;
        }

        Debug.Log($"[Active Skill] {caster.Name} used {skill.Name}.");
        return true;
    }

    private static bool MatchesActiveSkillPhase(string phase)
    {
        return string.IsNullOrWhiteSpace(phase)
            || string.Equals(phase, "MAIN", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsValidSkillTarget(Skills skill, CardBase caster, CardBase target)
    {
        string subtype = skill.SkillSubType != null ? skill.SkillSubType.SubTypeCode : null;
        if (string.Equals(subtype, AppConstants.SkillType.DAMAGE, StringComparison.OrdinalIgnoreCase)
            || string.Equals(subtype, AppConstants.SkillType.DEBUFF, StringComparison.OrdinalIgnoreCase)
            || string.Equals(subtype, AppConstants.SkillType.CONTROL, StringComparison.OrdinalIgnoreCase))
        {
            return caster.Team != target.Team;
        }

        if (string.Equals(subtype, AppConstants.SkillType.HEAL, StringComparison.OrdinalIgnoreCase)
            || string.Equals(subtype, AppConstants.SkillType.BUFF, StringComparison.OrdinalIgnoreCase)
            || string.Equals(subtype, AppConstants.SkillType.CLEANSE, StringComparison.OrdinalIgnoreCase))
        {
            return caster.Team == target.Team;
        }

        return true;
    }

    public void HandleAttackOutcome(CardBase attacker, CardBase target, DamageCalculator.AttackOutcome attackOutcome, string phase = "MAIN")
    {
        if (attacker == null || target == null) return;

        EnqueueCombatEvent(() => DispatchAttackOutcome(attacker, target, attackOutcome, phase));
    }

    private void DispatchAttackOutcome(CardBase attacker, CardBase target, DamageCalculator.AttackOutcome attackOutcome, string phase)
    {
        TriggerPassiveEffects(attacker, target, phase, AppConstants.TriggerCondition.ON_ATTACK);
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
        }
    }

    public void HandleHealOutcome(CardBase caster, CardBase target, string phase = "MAIN")
    {
        if (caster == null || target == null) return;

        EnqueueCombatEvent(() => DispatchHealOutcome(caster, target, phase));
    }

    private void DispatchHealOutcome(CardBase caster, CardBase target, string phase)
    {
        TriggerPassiveEffects(caster, target, phase, AppConstants.TriggerCondition.ON_HEAL);
        TriggerPassiveEffects(target, caster, phase, AppConstants.TriggerCondition.ON_BE_HEALED);
    }

    public void TriggerPassiveEffects(CardBase owner, CardBase attacker, string currentPhase, string condition)
    {
        if (owner == null || owner.PassiveEffects == null || owner.PassiveEffects.Count == 0
            || string.IsNullOrWhiteSpace(currentPhase) || string.IsNullOrWhiteSpace(condition))
        {
            return;
        }

        GridCell ownerCell = GridManager.Instance != null ? GridManager.Instance.GetCellOfCard(owner) : null;
        GridCell attackerCell = attacker != null && GridManager.Instance != null
            ? GridManager.Instance.GetCellOfCard(attacker)
            : null;

        isResolvingPassiveEffects = true;
        try
        {
            foreach (Effects effect in owner.PassiveEffects)
            {
                if (effect == null
                    || (!string.IsNullOrWhiteSpace(effect.TriggerPhase)
                        && !string.Equals(effect.TriggerPhase, currentPhase, StringComparison.OrdinalIgnoreCase))
                    || !MatchesTriggerCondition(effect.TriggerCondition, condition))
                {
                    continue;
                }

                List<CardBase> targets = CombatTargetSelector.GetAffectedTargets(
                    effect.Target != null ? effect.Target.Id : null,
                    ownerCell,
                    attackerCell,
                    null);

                foreach (CardBase target in targets)
                {
                    CombatEffectProcessor.ApplyEffects(new List<Effects> { effect }, owner, target);
                }
            }
        }
        finally
        {
            isResolvingPassiveEffects = false;
            DrainDeferredCombatEvents();
        }
    }

    private void EnqueueCombatEvent(Action combatEvent)
    {
        if (isResolvingPassiveEffects || isProcessingCombatEvents)
        {
            deferredCombatEvents.Enqueue(combatEvent);
            return;
        }

        isProcessingCombatEvents = true;
        try
        {
            combatEvent();
            DrainCombatEventQueue();
        }
        finally
        {
            isProcessingCombatEvents = false;
        }
    }

    private void DrainDeferredCombatEvents()
    {
        if (isResolvingPassiveEffects || isProcessingCombatEvents || deferredCombatEvents.Count == 0) return;

        isProcessingCombatEvents = true;
        try
        {
            DrainCombatEventQueue();
        }
        finally
        {
            isProcessingCombatEvents = false;
        }
    }

    private void DrainCombatEventQueue()
    {
        int processedEvents = 0;
        while (deferredCombatEvents.Count > 0 && processedEvents < MaxDeferredCombatEvents)
        {
            Action combatEvent = deferredCombatEvents.Dequeue();
            combatEvent();
            processedEvents++;
        }

        if (deferredCombatEvents.Count > 0)
        {
            deferredCombatEvents.Clear();
            Debug.LogWarning($"[CombatEngagementEngine] Stopped a passive event chain after {MaxDeferredCombatEvents} deferred events.");
        }
    }

    private static bool MatchesTriggerCondition(string effectCondition, string requestedCondition)
    {
        if (string.Equals(effectCondition, requestedCondition, StringComparison.OrdinalIgnoreCase)) return true;

        return string.Equals(requestedCondition, AppConstants.TriggerCondition.ON_TURN_START, StringComparison.OrdinalIgnoreCase)
            && string.Equals(effectCondition, "TURN_START", StringComparison.OrdinalIgnoreCase);
    }
}

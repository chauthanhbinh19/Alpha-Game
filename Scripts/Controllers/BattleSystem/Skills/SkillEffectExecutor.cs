using System;
using System.Collections.Generic;
using UnityEngine;
public class SkillEffectExecutor
{
    private BattleManager battleManager;

    public SkillEffectExecutor(BattleManager battleManager)
    {
        this.battleManager = battleManager;
    }
    private List<CardBase> GetTargets(string targetType, CardBase source)
    {
        List<CardBase> result = new List<CardBase>();

        switch (targetType)
        {
            case AppConstants.Target.SELF:
                result.Add(source);
                break;

            case AppConstants.Target.ALLY:
                // Lấy danh sách đồng đội (trừ bản thân)
                // result.AddRange(BattleManager.Instance.GetAlliesOf(source, includeSelf: false));
                break;

            case AppConstants.Target.ENEMY:
                // Lấy danh sách đối thủ
                // result.AddRange(BattleManager.Instance.GetEnemiesOf(source));
                break;

            default:
                Debug.LogWarning($"[EffectExecutor] Unknown target type: {targetType}");
                break;
        }

        return result;
    }

    public void ExecuteEffects(List<Effects> effects, CardBase source, CardBase target)
    {
        foreach (var effect in effects)
        {
            switch (effect.EffectAction.ActionCode)
            {
                case AppConstants.EffectAction.INCREASE:
                    ApplyBuff(effect, source);
                    break;

                case AppConstants.EffectAction.DECREASE:
                    ApplyDebuff(effect, target);
                    break;

                case AppConstants.EffectAction.RESTORE:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.LOCK:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.BREAK:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.CONVERT:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.PREVENT:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.SET:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.IMMUNITY:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.STEAL:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.COPY:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.TRANSFER:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.REVERSE:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.REFLECT:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.APPLY:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.REMOVE:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.EXTEND:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.SHORTEN:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.LIMIT_ACTION:
                    ApplyControlState(effect, target);
                    break;

                case AppConstants.EffectAction.DAMAGE:
                    ApplyDamage(effect, source, target);
                    break;

                case AppConstants.EffectAction.HEAL:
                    ApplyHeal(effect, source);
                    break;

                default:
                    Debug.Log($"[EffectExecutor] Unknown action: {effect.EffectAction.ActionCode}");
                    break;
            }
        }
    }

    private void ApplyDamage(Effects effect, CardBase source, CardBase target)
    {
        float attackValue = GetAttackValue(effect.EffectProperty.PropertyCode, source);
        float defenseValue = GetDefenseValue(effect.EffectProperty.PropertyCode, target);
        float finalDamage = CalculateDamage(attackValue, defenseValue, effect.Value);

        target.Health -= finalDamage;
        Debug.Log($"{source.CardName} dealt {finalDamage} {effect.EffectProperty.PropertyCode.ToLower()} damage to {target.CardName}.");
    }

    private void ApplyHeal(Effects effect, CardBase source)
    {
        float healAmount = effect.Value;
        source.Health += healAmount;
        Debug.Log($"{source.CardName} healed {healAmount} HP.");
    }

    private void ApplyBuff(Effects effect, CardBase source)
    {
        Debug.Log($"{source.CardName} gained buff: {effect.EffectProperty.PropertyCode} +{effect.Value}");
        // Có thể set flag hoặc cộng stat tạm thời
    }

    private void ApplyDebuff(Effects effect, CardBase target)
    {
        Debug.Log($"{target.CardName} suffered debuff: {effect.EffectProperty.PropertyCode} -{effect.Value}");
        // Giảm chỉ số hoặc gắn debuff state
    }

    private void ApplyControlState(Effects effect, CardBase target)
    {
        Debug.Log($"{target.CardName} is affected by {effect.EffectProperty.PropertyCode}");
        // Ví dụ: Freeze, Silence, Stun...
    }

    private float GetAttackValue(string propertyCode, CardBase source)
    {
        return propertyCode switch
        {
            // "PHYSICAL_ATTACK" => source.PhysicalAttack,
            // "MAGICAL_ATTACK" => source.MagicalAttack,
            // "CHEMICAL_ATTACK" => source.ChemicalAttack,
            // "ATOMIC_ATTACK" => source.AtomicAttack,
            // "MENTAL_ATTACK" => source.MentalAttack,
            _ => 0
        };
    }

    private float GetDefenseValue(string propertyCode, CardBase target)
    {
        return propertyCode switch
        {
            // "PHYSICAL_ATTACK" => target.PhysicalDefense,
            // "MAGICAL_ATTACK" => target.MagicalDefense,
            // "CHEMICAL_ATTACK" => target.ChemicalDefense,
            // "ATOMIC_ATTACK" => target.AtomicDefense,
            // "MENTAL_ATTACK" => target.MentalDefense,
            _ => 0
        };
    }

    private float CalculateDamage(float attack, float defense, float multiplier)
    {
        return Math.Max(1, (attack * multiplier) - (defense / 2));
    }
}

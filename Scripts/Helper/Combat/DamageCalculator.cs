using System;

public static class DamageCalculator
{
    public struct AttackOutcome
    {
        public double Damage;
        public bool IsHit;
        public bool IsCrit;
        public bool IsMiss;
    }

    public static AttackOutcome CauseNormalAttack(CardBase playerCard, CardBase enemyCard)
    {
        AttackOutcome result = new AttackOutcome { Damage = 0, IsHit = false, IsCrit = false, IsMiss = true };

        var physical = CausePhysicalNormalAttack(playerCard, enemyCard);
        var magical = CauseMagicalNormalAttack(playerCard, enemyCard);
        var chemical = CauseChemicalNormalAttack(playerCard, enemyCard);
        var atomic = CauseAtomicNormalAttack(playerCard, enemyCard);
        var mental = CauseMentalNormalAttack(playerCard, enemyCard);

        CombineAttackOutcomes(result, physical, ref result);
        CombineAttackOutcomes(result, magical, ref result);
        CombineAttackOutcomes(result, chemical, ref result);
        CombineAttackOutcomes(result, atomic, ref result);
        CombineAttackOutcomes(result, mental, ref result);

        return result;
    }

    public static AttackOutcome CauseSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        AttackOutcome result = new AttackOutcome { Damage = 0, IsHit = false, IsCrit = false, IsMiss = true };

        var physical = CausePhysicalSkillAttack(playerCard, enemyCard);
        var magical = CauseMagicalSkillAttack(playerCard, enemyCard);
        var chemical = CauseChemicalSkillAttack(playerCard, enemyCard);
        var atomic = CauseAtomicSkillAttack(playerCard, enemyCard);
        var mental = CauseMentalSkillAttack(playerCard, enemyCard);

        CombineAttackOutcomes(result, physical, ref result);
        CombineAttackOutcomes(result, magical, ref result);
        CombineAttackOutcomes(result, chemical, ref result);
        CombineAttackOutcomes(result, atomic, ref result);
        CombineAttackOutcomes(result, mental, ref result);

        return result;
    }

    private static void CombineAttackOutcomes(AttackOutcome previous, AttackOutcome next, ref AttackOutcome result)
    {
        result.Damage = previous.Damage + next.Damage;
        result.IsHit = previous.IsHit || next.IsHit;
        result.IsCrit = previous.IsCrit || next.IsCrit;
        result.IsMiss = previous.IsMiss && next.IsMiss;
    }

    public static AttackOutcome CausePhysicalNormalAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateDamage(playerCard, enemyCard, AttackType.Normal, AttackElement.Physical);
        if (outcome.Damage > 0)
        {
            enemyCard.TakeDamage(outcome.Damage);
        }

        return outcome;
    }

    public static AttackOutcome CauseMagicalNormalAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateDamage(playerCard, enemyCard, AttackType.Normal, AttackElement.Magical);
        if (outcome.Damage > 0)
        {
            enemyCard.TakeDamage(outcome.Damage);
        }

        return outcome;
    }

    public static AttackOutcome CauseChemicalNormalAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateDamage(playerCard, enemyCard, AttackType.Normal, AttackElement.Chemical);
        if (outcome.Damage > 0)
        {
            enemyCard.TakeDamage(outcome.Damage);
        }

        return outcome;
    }

    public static AttackOutcome CauseAtomicNormalAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateDamage(playerCard, enemyCard, AttackType.Normal, AttackElement.Atomic);
        if (outcome.Damage > 0)
        {
            enemyCard.TakeDamage(outcome.Damage);
        }

        return outcome;
    }

    public static AttackOutcome CauseMentalNormalAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateDamage(playerCard, enemyCard, AttackType.Normal, AttackElement.Mental);
        if (outcome.Damage > 0)
        {
            enemyCard.TakeDamage(outcome.Damage);
        }

        return outcome;
    }

    public static AttackOutcome CausePhysicalSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateDamage(playerCard, enemyCard, AttackType.Skill, AttackElement.Physical);
        if (outcome.Damage > 0)
        {
            enemyCard.TakeDamage(outcome.Damage);
        }

        return outcome;
    }

    public static AttackOutcome CauseMagicalSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateDamage(playerCard, enemyCard, AttackType.Skill, AttackElement.Magical);
        if (outcome.Damage > 0)
        {
            enemyCard.TakeDamage(outcome.Damage);
        }

        return outcome;
    }

    public static AttackOutcome CauseChemicalSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateDamage(playerCard, enemyCard, AttackType.Skill, AttackElement.Chemical);
        if (outcome.Damage > 0)
        {
            enemyCard.TakeDamage(outcome.Damage);
        }

        return outcome;
    }

    public static AttackOutcome CauseAtomicSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateDamage(playerCard, enemyCard, AttackType.Skill, AttackElement.Atomic);
        if (outcome.Damage > 0)
        {
            enemyCard.TakeDamage(outcome.Damage);
        }

        return outcome;
    }

    public static AttackOutcome CauseMentalSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateDamage(playerCard, enemyCard, AttackType.Skill, AttackElement.Mental);
        if (outcome.Damage > 0)
        {
            enemyCard.TakeDamage(outcome.Damage);
        }

        return outcome;
    }

    public static AttackOutcome CalculateDamage(CardBase playerCard, CardBase enemyCard, AttackType attackType, AttackElement attackElement)
    {
        AttackOutcome result = new AttackOutcome { Damage = 0, IsHit = false, IsCrit = false, IsMiss = true };

        // Kiểm tra có đánh trúng không
        if (!AccuracyCalculator.IsAttackHit(playerCard, enemyCard))
        {
            return result;
        }

        result.IsHit = true;
        result.IsMiss = false;

        double enemyDefense = 0;
        double playerAttack = 0;

        switch (attackElement)
        {
            case AttackElement.Physical:
                enemyDefense = enemyCard.PhysicalDefense;
                playerAttack = playerCard.PhysicalAttack;
                break;
            case AttackElement.Magical:
                enemyDefense = enemyCard.MagicalDefense;
                playerAttack = playerCard.MagicalAttack;
                break;
            case AttackElement.Chemical:
                enemyDefense = enemyCard.ChemicalDefense;
                playerAttack = playerCard.ChemicalAttack;
                break;
            case AttackElement.Atomic:
                enemyDefense = enemyCard.AtomicDefense;
                playerAttack = playerCard.AtomicAttack;
                break;
            case AttackElement.Mental:
                enemyDefense = enemyCard.MentalDefense;
                playerAttack = playerCard.MentalAttack;
                break;
        }

        // Áp dụng xuyên giáp nếu có
        double effectiveDefense = PenetrationCalculator.ApplyDefenseWithPenetration(playerCard, enemyCard, enemyDefense);

        // Công thức tính damage theo tỉ lệ attack / (attack + defense)
        double ratio = (playerAttack + effectiveDefense) > 0 ? playerAttack / (playerAttack + effectiveDefense) : 0;
        double baseDamage = playerAttack * ratio * (1 + (QualityEvaluatorHelper.CheckQuality(playerCard.Rare) / 100));

        // Áp dụng hiệu ứng đánh thường
        baseDamage = NormalCalculator.ApplyDamageToNormal(playerCard, attackType, baseDamage);
        baseDamage = NormalCalculator.ApplyResistanceToNormal(enemyCard, attackType, baseDamage);
        // Áp dụng hiệu ứng kỹ năng
        baseDamage = SkillCalculator.ApplyDamageToSkill(playerCard, attackType, baseDamage);
        baseDamage = SkillCalculator.ApplyResistanceToSkill(enemyCard, attackType, baseDamage);

        // Áp dụng hiệu ứng khác phe
        baseDamage = DifferentFactionCalculator.ApplyDamageToDifferentFaction(playerCard, enemyCard, baseDamage);

        // Áp dụng hiệu ứng cùng phe
        baseDamage = SameFactionCalculator.ApplyDamageToSameFaction(playerCard, enemyCard, baseDamage);

        // Áp dụng hiệu ứng khác phe, đổi đầu vào enemy và player cho nhau
        baseDamage = DifferentFactionCalculator.ApplyResistanceToDifferentFaction(enemyCard, playerCard, baseDamage);

        // Áp dụng hiệu ứng cùng phe, đổi đầu vào enemy và player cho nhau
        baseDamage = SameFactionCalculator.ApplyResistanceToSameFaction(enemyCard, playerCard, baseDamage);

        // Nếu chí mạng
        if (CriticalCalculator.IsCriticalHit(playerCard, enemyCard))
        {
            result.IsCrit = true;
            baseDamage = CriticalCalculator.ApplyCriticalDamage(playerCard, enemyCard, baseDamage);
        }

        // Áp dụng kháng sát thương
        baseDamage = ResistanceCalculator.ApplyResistance(playerCard, baseDamage);
        // Áp dụng hấp thụ sát thương
        if (AbsorptionCalculator.IsAbsorptionHit(playerCard, enemyCard))
        {
            baseDamage = AbsorptionCalculator.ApplyDamageAbsorption(playerCard, baseDamage);
        }

        double flooredDamage = Math.Floor(baseDamage);
        if (playerAttack > 0 && flooredDamage < 1)
        {
            flooredDamage = 1;
        }

        result.Damage = Math.Max(0, flooredDamage);
        return result;
    }
}

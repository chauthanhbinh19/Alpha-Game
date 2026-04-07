using System;

public static class DamageCalculator
{
    public static void Attack(CardBase playerCard, CardBase enemyCard)
    {
        CauseNormalAttack(playerCard, enemyCard);
        CauseSkillAttack(playerCard, enemyCard);
    }

    public static void CauseNormalAttack(CardBase playerCard, CardBase enemyCard)
    {
        CausePhysicalNormalAttack(playerCard, enemyCard);
        CauseMagicalNormalAttack(playerCard, enemyCard);
        CauseChemicalNormalAttack(playerCard, enemyCard);
        CauseAtomicNormalAttack(playerCard, enemyCard);
        CauseMentalNormalAttack(playerCard, enemyCard);
    }

    public static void CauseSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        CausePhysicalSkillAttack(playerCard, enemyCard);
        CauseMagicalSkillAttack(playerCard, enemyCard);
        CauseChemicalSkillAttack(playerCard, enemyCard);
        CauseAtomicSkillAttack(playerCard, enemyCard);
        CauseMentalSkillAttack(playerCard, enemyCard);
    }

    public static void CausePhysicalNormalAttack(CardBase playerCard, CardBase enemyCard)
    {
        double damage = CalculateDamage(playerCard, enemyCard, AttackType.Normal, AttackElement.Physical);
        enemyCard.TakeDamage(damage);
    }

    public static void CauseMagicalNormalAttack(CardBase playerCard, CardBase enemyCard)
    {
        double damage = CalculateDamage(playerCard, enemyCard, AttackType.Normal, AttackElement.Magical);
        enemyCard.TakeDamage(damage);
    }

    public static void CauseChemicalNormalAttack(CardBase playerCard, CardBase enemyCard)
    {
        double damage = CalculateDamage(playerCard, enemyCard, AttackType.Normal, AttackElement.Chemical);
        enemyCard.TakeDamage(damage);
    }

    public static void CauseAtomicNormalAttack(CardBase playerCard, CardBase enemyCard)
    {
        double damage = CalculateDamage(playerCard, enemyCard, AttackType.Normal, AttackElement.Atomic);
        enemyCard.TakeDamage(damage);
    }

    public static void CauseMentalNormalAttack(CardBase playerCard, CardBase enemyCard)
    {
        double damage = CalculateDamage(playerCard, enemyCard, AttackType.Normal, AttackElement.Mental);
        enemyCard.TakeDamage(damage);
    }

    public static void CausePhysicalSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        double damage = CalculateDamage(playerCard, enemyCard, AttackType.Skill, AttackElement.Physical);
        enemyCard.TakeDamage(damage);
    }

    public static void CauseMagicalSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        double damage = CalculateDamage(playerCard, enemyCard, AttackType.Skill, AttackElement.Magical);
        enemyCard.TakeDamage(damage);
    }

    public static void CauseChemicalSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        double damage = CalculateDamage(playerCard, enemyCard, AttackType.Skill, AttackElement.Chemical);
        enemyCard.TakeDamage(damage);
    }

    public static void CauseAtomicSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        double damage = CalculateDamage(playerCard, enemyCard, AttackType.Skill, AttackElement.Atomic);
        enemyCard.TakeDamage(damage);
    }

    public static void CauseMentalSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        double damage = CalculateDamage(playerCard, enemyCard, AttackType.Skill, AttackElement.Mental);
        enemyCard.TakeDamage(damage);
    }

    public static double CalculateDamage(CardBase playerCard, CardBase enemyCard, AttackType attackType, AttackElement attackElement)
    {
        // Kiểm tra có đánh trúng không
        if (!AccuracyCalculator.IsAttackHit(playerCard, enemyCard))
        {
            return 0;
        }

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
        double effectiveDefense = PenetrationCalculator.ApplyDefenseWithPenetration(playerCard, enemyCard, enemyDefense); // bỏ qua % giáp

        // Công thức tính damage theo tỉ lệ attack / (attack + defense)
        double ratio = (playerAttack + effectiveDefense) > 0 ? playerAttack / (playerAttack + effectiveDefense) : 0;
        double baseDamage = playerAttack * ratio * (1 + (QualityEvaluator.CheckQuality(playerCard.Rare) / 100));

        // Áp dụng hiệu ứng đánh thường
        baseDamage = NormalCalculator.ApplyDamageToNormal(playerCard, attackType, baseDamage);
        baseDamage = NormalCalculator.ApplyResistanceToNormal(enemyCard, attackType, baseDamage);
        // Áp dụng hiệu ứng đánh thường
        baseDamage = SkillCalculator.ApplyDamageToSkill(playerCard, attackType, baseDamage);
        baseDamage = SkillCalculator.ApplyResistanceToSkill(enemyCard, attackType, baseDamage);

        // Debug.Log("Attack " + attack);
        // Debug.Log("ratio " + ratio);
        // Debug.Log(baseDamage);

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
            baseDamage = CriticalCalculator.ApplyCriticalDamage(playerCard, enemyCard, baseDamage);
        }

        // Áp dụng kháng sát thương
        baseDamage = ResistanceCalculator.ApplyResistance(playerCard, baseDamage);
        // Áp dụng hấp thụ sát thương
        if(AbsorptionCalculator.IsAbsorptionHit(playerCard, enemyCard))
        {
            baseDamage = AbsorptionCalculator.ApplyDamageAbsorption(playerCard, baseDamage);
        }
        
        double flooredDamage = Math.Floor(baseDamage);
        if (playerAttack > 0 && flooredDamage < 1)
        {
            return 1;
        }

        return Math.Max(0, flooredDamage);
    }

}
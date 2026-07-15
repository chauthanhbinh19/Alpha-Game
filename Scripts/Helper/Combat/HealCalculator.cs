public static class HealCalculator
{
    public static double CausePhysicalSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateHeal(playerCard, AttackElement.Physical);

        return outcome;
    }

    public static double CauseMagicalSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateHeal(playerCard, AttackElement.Magical);

        return outcome;
    }

    public static double CauseChemicalSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateHeal(playerCard, AttackElement.Chemical);

        return outcome;
    }

    public static double CauseAtomicSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateHeal(playerCard, AttackElement.Atomic);

        return outcome;
    }

    public static double CauseMentalSkillAttack(CardBase playerCard, CardBase enemyCard)
    {
        var outcome = CalculateHeal(playerCard, AttackElement.Mental);

        return outcome;
    }
    public static double CalculateHeal(CardBase playerCard, AttackElement attackElement)
    {
        double playerAttack = 0;
        switch (attackElement)
        {
            case AttackElement.Physical:
                playerAttack = playerCard.PhysicalAttack;
                break;
            case AttackElement.Magical:
                playerAttack = playerCard.MagicalAttack;
                break;
            case AttackElement.Chemical:
                playerAttack = playerCard.ChemicalAttack;
                break;
            case AttackElement.Atomic:
                playerAttack = playerCard.AtomicAttack;
                break;
            case AttackElement.Mental:
                playerAttack = playerCard.MentalAttack;
                break;
        }

        return playerAttack * 50/100;
    }
}
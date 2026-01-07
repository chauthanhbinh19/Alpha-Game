using System.Collections.Generic;
public static class EvaluateExperiment
{
    public static double GetItemExp(string item)
    {
        double expPerBottle = 0;
        switch (item)
        {
            case ItemConstants.Experiment.EXP_BOTTOLE_LV1:
                expPerBottle = 100;
                break;

            case ItemConstants.Experiment.EXP_BOTTOLE_LV2:
                expPerBottle = 500;
                break;

            case ItemConstants.Experiment.EXP_BOTTOLE_LV3:
                expPerBottle = 1000;
                break;

            case ItemConstants.Experiment.EXP_BOTTOLE_LV4:
                expPerBottle = 10000;
                break;

            case ItemConstants.Experiment.EXP_BOTTOLE_LV5:
                expPerBottle = 50000;
                break;

            case ItemConstants.Experiment.EXP_BOTTOLE_LV6:
                expPerBottle = 100000;
                break;

            case ItemConstants.Experiment.EXP_ACHIEVEMENT:
            case ItemConstants.Experiment.EXP_BOOK:
            case ItemConstants.Experiment.EXP_PET:
            case ItemConstants.Experiment.EXP_COLLABORATION_EQUIPMENT:
            case ItemConstants.Experiment.EXP_COLLABORATION:
            case ItemConstants.Experiment.EXP_EQUIPMENT:
            case ItemConstants.Experiment.EXP_MEDAL:
            case ItemConstants.Experiment.EXP_SKILL:
            case ItemConstants.Experiment.EXP_SYMBOL:
            case ItemConstants.Experiment.EXP_TITLE:
            case ItemConstants.Experiment.EXP_MAGIC_FORMATION_CIRCLE:
            case ItemConstants.Experiment.EXP_RELIC:
            case ItemConstants.Experiment.EXP_ALCHEMY:
            case ItemConstants.Experiment.EXP_PUPPET:
            case ItemConstants.Experiment.EXP_TALISMAN:
            case ItemConstants.Experiment.EXP_FORGE:
            case ItemConstants.Experiment.EXP_ARCHITECTURE:
            case ItemConstants.Experiment.EXP_ARTWORK:
            case ItemConstants.Experiment.EXP_AVATAR:
            case ItemConstants.Experiment.EXP_BADGE:
            case ItemConstants.Experiment.EXP_FOOD:
            case ItemConstants.Experiment.EXP_BEVERAGE:
            case ItemConstants.Experiment.EXP_BORDER:
            case ItemConstants.Experiment.EXP_BUILDING:
            case ItemConstants.Experiment.EXP_CARD:
            case ItemConstants.Experiment.EXP_CORE:
            case ItemConstants.Experiment.EXP_FURNITURE:
            case ItemConstants.Experiment.EXP_MECHA_BEAST:
            case ItemConstants.Experiment.EXP_PLANT:
            case ItemConstants.Experiment.EXP_ROBOT:
            case ItemConstants.Experiment.EXP_RUNE:
            case ItemConstants.Experiment.EXP_SPIRIT_BEAST:
            case ItemConstants.Experiment.EXP_SPIRIT_CARD:
            case ItemConstants.Experiment.EXP_TECHNOLOGY:
            case ItemConstants.Experiment.EXP_VEHICLE:
            case ItemConstants.Experiment.EXP_WEAPON:
                expPerBottle = 1000;
                break;

            case ItemConstants.Affinity.AFFINITY_NUMBER_1:
            case ItemConstants.Affinity.AFFINITY_NUMBER_2:
            case ItemConstants.Affinity.AFFINITY_NUMBER_3:
            case ItemConstants.Affinity.AFFINITY_NUMBER_4:
            case ItemConstants.Affinity.AFFINITY_NUMBER_5:
            case ItemConstants.Affinity.AFFINITY_NUMBER_6:
            case ItemConstants.Affinity.AFFINITY_NUMBER_7:
                expPerBottle = 100000;
                break;

            case ItemConstants.Affinity.AFFINITY_NUMBER_8:
            case ItemConstants.Affinity.AFFINITY_NUMBER_9:
                expPerBottle = 50000;
                break;

            case ItemConstants.Affinity.AFFINITY_NUMBER_10:
            case ItemConstants.Affinity.AFFINITY_NUMBER_11:
            case ItemConstants.Affinity.AFFINITY_NUMBER_12:
                expPerBottle = 10000;
                break;

            case ItemConstants.Affinity.AFFINITY_NUMBER_14:
            case ItemConstants.Affinity.AFFINITY_NUMBER_15:
                expPerBottle = 1000;
                break;

            default:
                expPerBottle = 0;
                break;
        }
        return expPerBottle;
    }
}
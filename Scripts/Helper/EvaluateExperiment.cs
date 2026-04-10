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

            case ItemConstants.Experiment.EXP_ACHIEVEMENTS:
            case ItemConstants.Experiment.EXP_BOOKS:
            case ItemConstants.Experiment.EXP_PETS:
            case ItemConstants.Experiment.EXP_COLLABORATION_EQUIPMENTS:
            case ItemConstants.Experiment.EXP_COLLABORATIONS:
            case ItemConstants.Experiment.EXP_EQUIPMENTS:
            case ItemConstants.Experiment.EXP_MEDALS:
            case ItemConstants.Experiment.EXP_SKILLS:
            case ItemConstants.Experiment.EXP_SYMBOLS:
            case ItemConstants.Experiment.EXP_TITLES:
            case ItemConstants.Experiment.EXP_MAGIC_FORMATION_CIRCLES:
            case ItemConstants.Experiment.EXP_RELICS:
            case ItemConstants.Experiment.EXP_ALCHEMIES:
            case ItemConstants.Experiment.EXP_PUPPETS:
            case ItemConstants.Experiment.EXP_TALISMANS:
            case ItemConstants.Experiment.EXP_FORGES:
            case ItemConstants.Experiment.EXP_ARCHITECTURES:
            case ItemConstants.Experiment.EXP_ARTWORKS:
            case ItemConstants.Experiment.EXP_AVATARS:
            case ItemConstants.Experiment.EXP_BADGES:
            case ItemConstants.Experiment.EXP_FOODS:
            case ItemConstants.Experiment.EXP_BEVERAGES:
            case ItemConstants.Experiment.EXP_BORDERS:
            case ItemConstants.Experiment.EXP_BUILDINGS:
            case ItemConstants.Experiment.EXP_ARTIFACTS:
            case ItemConstants.Experiment.EXP_CORES:
            case ItemConstants.Experiment.EXP_FURNITURES:
            case ItemConstants.Experiment.EXP_MECHA_BEASTS:
            case ItemConstants.Experiment.EXP_PLANTS:
            case ItemConstants.Experiment.EXP_ROBOTS:
            case ItemConstants.Experiment.EXP_RUNES:
            case ItemConstants.Experiment.EXP_SPIRIT_BEASTS:
            case ItemConstants.Experiment.EXP_SPIRIT_CARDS:
            case ItemConstants.Experiment.EXP_TECHNOLOGIES:
            case ItemConstants.Experiment.EXP_VEHICLES:
            case ItemConstants.Experiment.EXP_WEAPONS:
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
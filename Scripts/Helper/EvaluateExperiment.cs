using System.Collections.Generic;
public static class EvaluateExperiment
{
    public static int GetItemExp(string item)
    {
        int expPerBottle = 0;
        if (item.Equals(ItemConstants.Experiment.EXP_BOTTOLE_LV1))
        {
            expPerBottle = 100;
        }
        else if (item.Equals(ItemConstants.Experiment.EXP_BOTTOLE_LV2))
        {
            expPerBottle = 500;
        }
        else if (item.Equals(ItemConstants.Experiment.EXP_BOTTOLE_LV3))
        {
            expPerBottle = 1000;
        }
        else if (item.Equals(ItemConstants.Experiment.EXP_BOTTOLE_LV4))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals(ItemConstants.Experiment.EXP_BOTTOLE_LV5))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals(ItemConstants.Experiment.EXP_BOTTOLE_LV6))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_1))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_2))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_3))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_4))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_5))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_6))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_7))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_8))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_9))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_10))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_11))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_12))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_14))
        {
            expPerBottle = 1000;
        }
        else if (item.Equals(ItemConstants.Affinity.AFFINITY_NUMBER_15))
        {
            expPerBottle = 1000;
        }
        return expPerBottle;
    }
}
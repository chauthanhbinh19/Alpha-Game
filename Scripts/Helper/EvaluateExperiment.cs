using System.Collections.Generic;
public static class EvaluateExperiment
{
    public static int GetItemExp(string item)
    {
        int expPerBottle = 0;
        if (item.Equals(ItemConstants.Experiment.ExpBottleLv1))
        {
            expPerBottle = 100;
        }
        else if (item.Equals(ItemConstants.Experiment.ExpBottleLv2))
        {
            expPerBottle = 500;
        }
        else if (item.Equals(ItemConstants.Experiment.ExpBottleLv3))
        {
            expPerBottle = 1000;
        }
        else if (item.Equals(ItemConstants.Experiment.ExpBottleLv4))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals(ItemConstants.Experiment.ExpBottleLv5))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals(ItemConstants.Experiment.ExpBottleLv6))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber1))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber2))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber3))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber4))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber5))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber6))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber7))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber8))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber9))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber10))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber11))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber12))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber13))
        {
            expPerBottle = 1000;
        }
        else if (item.Equals(ItemConstants.Affinity.AffinityNumber14))
        {
            expPerBottle = 1000;
        }
        return expPerBottle;
    }
}
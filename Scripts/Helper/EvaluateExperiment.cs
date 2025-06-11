using System.Collections.Generic;
public static class EvaluateExperiment
{
    public static int GetItemExp(string item)
    {
        int expPerBottle = 0;
        if (item.Equals(ItemConstants.ExpBottleLv1))
        {
            expPerBottle = 100;
        }
        else if (item.Equals(ItemConstants.ExpBottleLv2))
        {
            expPerBottle = 500;
        }
        else if (item.Equals(ItemConstants.ExpBottleLv3))
        {
            expPerBottle = 1000;
        }
        else if (item.Equals(ItemConstants.ExpBottleLv4))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals(ItemConstants.ExpBottleLv5))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals(ItemConstants.ExpBottleLv6))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber1))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber2))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber3))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber4))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber5))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber6))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber7))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber8))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber9))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber10))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber11))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber12))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber13))
        {
            expPerBottle = 1000;
        }
        else if (item.Equals(ItemConstants.AffinityNumber14))
        {
            expPerBottle = 1000;
        }
        return expPerBottle;
    }
}
using System.Collections.Generic;
public static class EvaluateExperiment
{
    public static int GetItemExp(string item)
    {
        int expPerBottle = 0;
        if (item.Equals("Exp Bottle lv1"))
        {
            expPerBottle = 100;
        }
        else if (item.Equals("Exp Bottle lv2"))
        {
            expPerBottle = 500;
        }
        else if (item.Equals("Exp Bottle lv3"))
        {
            expPerBottle = 1000;
        }
        else if (item.Equals("Exp Bottle lv4"))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals("Exp Bottle lv5"))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals("Exp Bottle lv6"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 1"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 2"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 3"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 4"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 5"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 6"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 7"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 8"))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals("Affinity 9"))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals("Affinity 10"))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals("Affinity 11"))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals("Affinity 12"))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals("Affinity 13"))
        {
            expPerBottle = 1000;
        }
        else if (item.Equals("Affinity 14"))
        {
            expPerBottle = 1000;
        }
        return expPerBottle;
    }
}
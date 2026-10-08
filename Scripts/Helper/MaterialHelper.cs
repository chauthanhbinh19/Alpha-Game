using UnityEngine;

public static class MaterialHelper
{
    public static Material GetMaterial(string rare)
    {
        switch (rare)
        {
            case AppConstants.Rare.SR:
                return MaterialManager.Instance.Get("Rare_SR_Mat");
            case AppConstants.Rare.SSR:
                return MaterialManager.Instance.Get("Rare_SSR_Mat");
            case AppConstants.Rare.UR:
                return MaterialManager.Instance.Get("Rare_UR_Mat");
            case AppConstants.Rare.LG:
                return MaterialManager.Instance.Get("Rare_LG_Mat");
            case AppConstants.Rare.LG_Plus:
                return MaterialManager.Instance.Get("Rare_LGPlus_Mat");
            case AppConstants.Rare.MR:
                return MaterialManager.Instance.Get("Rare_MR_Mat");
            case AppConstants.Rare.MR_Plus:
                return MaterialManager.Instance.Get("Rare_MRPlus_Mat");
            case AppConstants.Rare.SLG:
                return MaterialManager.Instance.Get("Rare_SLG_Mat");
            case AppConstants.Rare.SLG_Plus:
                return MaterialManager.Instance.Get("Rare_SLGPlus_Mat");
            case AppConstants.Rare.SP:
                return MaterialManager.Instance.Get("Rare_SP_Mat");
            case AppConstants.Rare.SP_Plus:
                return MaterialManager.Instance.Get("Rare_SPPlus_Mat");
            default:
                return MaterialManager.Instance.Get("Rare_SR_Mat");
        }
    }
}
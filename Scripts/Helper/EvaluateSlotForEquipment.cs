using System.Collections.Generic;
public static class EvaluateSlotForEquipment
{
    public static int CheckSlotForEquipments(string type)
    {
        int slot = 0;
        switch (type)
        {
            case AppConstants.Amnitus:
                slot = 4;
                break;
            case AppConstants.Angelis:
                slot = 1;
                break;
            case AppConstants.Bellion:
                slot = 16;
                break;
            case AppConstants.Benzamin:
                slot = 4;
                break;
            case AppConstants.Celestial:
                slot = 4;
                break;
            case AppConstants.Ceverus:
                slot = 10;
                break;
            case AppConstants.Delius:
                slot = 10;
                break;
            case AppConstants.Domitius:
                slot = 8;
                break;
            case AppConstants.Everlyn:
                slot = 6;
                break;
            case AppConstants.Extra:
                slot = 4;
                break;
            case AppConstants.Faltus:
                slot = 16;
                break;
            case AppConstants.Fealan:
                slot = 16;
                break;
            case AppConstants.Gamma:
                slot = 8;
                break;
            case AppConstants.Gem:
                slot = 8;
                break;
            case AppConstants.Hagoro:
                slot = 6;
                break;
            case AppConstants.Hakalite:
                slot = 4;
                break;
            case AppConstants.Ignis:
                slot = 16;
                break;
            case AppConstants.Ivitus:
                slot = 14;
                break;
            case AppConstants.Jorvan:
                slot = 10;
                break;
            case AppConstants.Jullian:
                slot = 10;
                break;
            case AppConstants.Karis:
                slot = 8;
                break;
            case AppConstants.Karmus:
                slot = 8;
                break;
            case AppConstants.Lotus:
                slot = 16;
                break;
            case AppConstants.Luminius:
                slot = 1;
                break;
            case AppConstants.Macus:
                slot = 14;
                break;
            case AppConstants.Morganis:
                slot = 12;
                break;
            case AppConstants.Nimigazin:
                slot = 14;
                break;
            case AppConstants.Nova:
                slot = 4;
                break;
            case AppConstants.Omonitus:
                slot = 4;
                break;
            case AppConstants.Omega:
                slot = 8;
                break;
            case AppConstants.Pet:
                slot = 6;
                break;
            case AppConstants.Pyros:
                slot = 16;
                break;
            case AppConstants.Qiyantus:
                slot = 1;
                break;
            case AppConstants.Quasar:
                slot = 1;
                break;
            case AppConstants.Rainbow:
                slot = 4;
                break;
            case AppConstants.Redvenger:
                slot = 6;
                break;
            case AppConstants.Souls:
                slot = 10;
                break;
            case AppConstants.Syncroharon:
                slot = 16;
                break;
            case AppConstants.Tarian:
                slot = 1;
                break;
            case AppConstants.Uni:
                slot = 16;
                break;
            case AppConstants.Ultrion:
                slot = 4;
                break;
            case AppConstants.Varethion:
                slot = 16;
                break;
            case AppConstants.Velmira:
                slot = 12;
                break;
            case AppConstants.Wenlithar:
                slot = 8;
                break;
            case AppConstants.Wyrmora:
                slot = 16;
                break;
            case AppConstants.Xaltheon:
                slot = 16;
                break;
            case AppConstants.Xyralis:
                slot = 10;
                break;
            case AppConstants.Yloran:
                slot = 16;
                break;
            case AppConstants.Yvarion:
                slot = 10;
                break;
            case AppConstants.Zodiac:
                slot = 12;
                break;
            case AppConstants.Zerox:
                slot = 16;
                break;
            default:
                slot = 1;
                break;
        }
        return slot;
    }
    public static bool CanUseBorderEffect(string type)
    {
        switch (type)
        {
            case AppConstants.Amnitus:
                return true;
            case AppConstants.Angelis:
                return false;
            case AppConstants.Bellion:
                return true;
            case AppConstants.Benzamin:
                return false;
            case AppConstants.Celestial:
                return true;
            case AppConstants.Ceverus:
                return true;
            case AppConstants.Delius:
                return true;
            case AppConstants.Domitius:
                return true;
            case AppConstants.Everlyn:
                return true;
            case AppConstants.Extra:
                return true;
            case AppConstants.Faltus:
                return true;
            case AppConstants.Fealan:
                return true;
            case AppConstants.Gamma:
                return true;
            case AppConstants.Gem:
                return true;
            case AppConstants.Hagoro:
                return true;
            case AppConstants.Hakalite:
                return true;
            case AppConstants.Ignis:
                return false;
            case AppConstants.Ivitus:
                return true;
            case AppConstants.Jorvan:
                return false;
            case AppConstants.Jullian:
                return true;
            case AppConstants.Karis:
                return true;
            case AppConstants.Karmus:
                return false;
            case AppConstants.Lotus:
                return false;
            case AppConstants.Luminius:
                return false;
            case AppConstants.Macus:
                return false;
            case AppConstants.Morganis:
                return true;
            case AppConstants.Nimigazin:
                return true;
            case AppConstants.Nova:
                return true;
            case AppConstants.Omonitus:
                return true;
            case AppConstants.Omega:
                return false;
            case AppConstants.Pet:
                return true;
            case AppConstants.Pyros:
                return true;
            case AppConstants.Qiyantus:
                return false;
            case AppConstants.Quasar:
                return false;
            case AppConstants.Rainbow:
                return true;
            case AppConstants.Redvenger:
                return true;
            case AppConstants.Souls:
                return false;
            case AppConstants.Syncroharon:
                return true;
            case AppConstants.Tarian:
                return false;
            case AppConstants.Uni:
                return true;
            case AppConstants.Ultrion:
                return false;
            case AppConstants.Varethion:
                return true;
            case AppConstants.Velmira:
                return true;
            case AppConstants.Wenlithar:
                return true;
            case AppConstants.Wyrmora:
                return true;
            case AppConstants.Xaltheon:
                return true;
            case AppConstants.Xyralis:
                return false;
            case AppConstants.Yloran:
                return true;
            case AppConstants.Yvarion:
                return false;
            case AppConstants.Zodiac:
                return false;
            case AppConstants.Zerox:
                return false;
            default:
                return false;
        }

    }
}
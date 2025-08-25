using System.Collections.Generic;
public static class EvaluateSlotForEquipment
{
    public static int CheckSlotForEquipments(string type)
    {
        int slot = 0;
        switch (type)
        {
            case AppConstants.Equipment.Amnitus:
                slot = 4;
                break;
            case AppConstants.Equipment.Angelis:
                slot = 1;
                break;
            case AppConstants.Equipment.Bellion:
                slot = 16;
                break;
            case AppConstants.Equipment.Benzamin:
                slot = 4;
                break;
            case AppConstants.Equipment.Celestial:
                slot = 4;
                break;
            case AppConstants.Equipment.Ceverus:
                slot = 10;
                break;
            case AppConstants.Equipment.Delius:
                slot = 10;
                break;
            case AppConstants.Equipment.Domitius:
                slot = 8;
                break;
            case AppConstants.Equipment.Everlyn:
                slot = 6;
                break;
            case AppConstants.Equipment.Extra:
                slot = 4;
                break;
            case AppConstants.Equipment.Faltus:
                slot = 16;
                break;
            case AppConstants.Equipment.Fealan:
                slot = 16;
                break;
            case AppConstants.Equipment.Gamma:
                slot = 8;
                break;
            case AppConstants.Equipment.Gem:
                slot = 8;
                break;
            case AppConstants.Equipment.Hagoro:
                slot = 6;
                break;
            case AppConstants.Equipment.Hakalite:
                slot = 4;
                break;
            case AppConstants.Equipment.Ignis:
                slot = 16;
                break;
            case AppConstants.Equipment.Ivitus:
                slot = 14;
                break;
            case AppConstants.Equipment.Jorvan:
                slot = 10;
                break;
            case AppConstants.Equipment.Jullian:
                slot = 10;
                break;
            case AppConstants.Equipment.Karis:
                slot = 8;
                break;
            case AppConstants.Equipment.Karmus:
                slot = 8;
                break;
            case AppConstants.Equipment.Lotus:
                slot = 16;
                break;
            case AppConstants.Equipment.Luminius:
                slot = 1;
                break;
            case AppConstants.Equipment.Macus:
                slot = 14;
                break;
            case AppConstants.Equipment.Morganis:
                slot = 12;
                break;
            case AppConstants.Equipment.Nimigazin:
                slot = 14;
                break;
            case AppConstants.Equipment.Nova:
                slot = 4;
                break;
            case AppConstants.Equipment.Omonitus:
                slot = 4;
                break;
            case AppConstants.Equipment.Omega:
                slot = 8;
                break;
            case AppConstants.Equipment.Pet:
                slot = 6;
                break;
            case AppConstants.Equipment.Pyros:
                slot = 16;
                break;
            case AppConstants.Equipment.Qiyantus:
                slot = 1;
                break;
            case AppConstants.Equipment.Quasar:
                slot = 1;
                break;
            case AppConstants.Equipment.Rainbow:
                slot = 4;
                break;
            case AppConstants.Equipment.Redvenger:
                slot = 6;
                break;
            case AppConstants.Equipment.Souls:
                slot = 10;
                break;
            case AppConstants.Equipment.Syncroharon:
                slot = 16;
                break;
            case AppConstants.Equipment.Tarian:
                slot = 1;
                break;
            case AppConstants.Equipment.Uni:
                slot = 16;
                break;
            case AppConstants.Equipment.Ultrion:
                slot = 4;
                break;
            case AppConstants.Equipment.Varethion:
                slot = 16;
                break;
            case AppConstants.Equipment.Velmira:
                slot = 12;
                break;
            case AppConstants.Equipment.Wenlithar:
                slot = 8;
                break;
            case AppConstants.Equipment.Wyrmora:
                slot = 16;
                break;
            case AppConstants.Equipment.Xaltheon:
                slot = 16;
                break;
            case AppConstants.Equipment.Xyralis:
                slot = 10;
                break;
            case AppConstants.Equipment.Yloran:
                slot = 16;
                break;
            case AppConstants.Equipment.Yvarion:
                slot = 10;
                break;
            case AppConstants.Equipment.Zodiac:
                slot = 12;
                break;
            case AppConstants.Equipment.Zerox:
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
            case AppConstants.Equipment.Amnitus:
                return true;
            case AppConstants.Equipment.Angelis:
                return false;
            case AppConstants.Equipment.Bellion:
                return true;
            case AppConstants.Equipment.Benzamin:
                return false;
            case AppConstants.Equipment.Celestial:
                return true;
            case AppConstants.Equipment.Ceverus:
                return true;
            case AppConstants.Equipment.Delius:
                return true;
            case AppConstants.Equipment.Domitius:
                return true;
            case AppConstants.Equipment.Everlyn:
                return true;
            case AppConstants.Equipment.Extra:
                return true;
            case AppConstants.Equipment.Faltus:
                return true;
            case AppConstants.Equipment.Fealan:
                return true;
            case AppConstants.Equipment.Gamma:
                return true;
            case AppConstants.Equipment.Gem:
                return true;
            case AppConstants.Equipment.Hagoro:
                return true;
            case AppConstants.Equipment.Hakalite:
                return true;
            case AppConstants.Equipment.Ignis:
                return false;
            case AppConstants.Equipment.Ivitus:
                return true;
            case AppConstants.Equipment.Jorvan:
                return false;
            case AppConstants.Equipment.Jullian:
                return true;
            case AppConstants.Equipment.Karis:
                return true;
            case AppConstants.Equipment.Karmus:
                return false;
            case AppConstants.Equipment.Lotus:
                return false;
            case AppConstants.Equipment.Luminius:
                return false;
            case AppConstants.Equipment.Macus:
                return false;
            case AppConstants.Equipment.Morganis:
                return true;
            case AppConstants.Equipment.Nimigazin:
                return true;
            case AppConstants.Equipment.Nova:
                return true;
            case AppConstants.Equipment.Omonitus:
                return true;
            case AppConstants.Equipment.Omega:
                return false;
            case AppConstants.Equipment.Pet:
                return true;
            case AppConstants.Equipment.Pyros:
                return true;
            case AppConstants.Equipment.Qiyantus:
                return false;
            case AppConstants.Equipment.Quasar:
                return false;
            case AppConstants.Equipment.Rainbow:
                return true;
            case AppConstants.Equipment.Redvenger:
                return true;
            case AppConstants.Equipment.Souls:
                return false;
            case AppConstants.Equipment.Syncroharon:
                return true;
            case AppConstants.Equipment.Tarian:
                return false;
            case AppConstants.Equipment.Uni:
                return true;
            case AppConstants.Equipment.Ultrion:
                return false;
            case AppConstants.Equipment.Varethion:
                return true;
            case AppConstants.Equipment.Velmira:
                return true;
            case AppConstants.Equipment.Wenlithar:
                return true;
            case AppConstants.Equipment.Wyrmora:
                return true;
            case AppConstants.Equipment.Xaltheon:
                return true;
            case AppConstants.Equipment.Xyralis:
                return false;
            case AppConstants.Equipment.Yloran:
                return true;
            case AppConstants.Equipment.Yvarion:
                return false;
            case AppConstants.Equipment.Zodiac:
                return false;
            case AppConstants.Equipment.Zerox:
                return false;
            default:
                return false;
        }

    }
}
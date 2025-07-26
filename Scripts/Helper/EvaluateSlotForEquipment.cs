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
            case "Amnitus":
                return true;
            case "Angelis":
                return false;
            case "Bellion":
                return true;
            case "Benzamin":
                return false;
            case "Celestial":
                return true;
            case "Ceverus":
                return true;
            case "Delius":
                return true;
            case "Domitius":
                return true;
            case "Everlyn":
                return true;
            case "Extra":
                return true;
            case "Faltus":
                return true;
            case "Fealan":
                return true;
            case "Gamma":
                return true;
            case "Gem":
                return true;
            case "Hagoro":
                return true;
            case "Hakalite":
                return true;
            case "Ignis":
                return false;
            case "Ivitus":
                return true;
            case "Jorvan":
                return false;
            case "Jullian":
                return true;
            case "Karis":
                return true;
            case "Karmus":
                return false;
            case "Lotus":
                return false;
            case "Luminius":
                return false;
            case "Macus":
                return false;
            case "Morganis":
                return true;
            case "Nimigazin":
                return true;
            case "Nova":
                return true;
            case "Omonitus":
                return true;
            case "Omega":
                return false;
            case "Pet":
                return true;
            case "Pyros":
                return true;
            case "Qiyantus":
                return false;
            case "Quasar":
                return false;
            case "Rainbow":
                return true;
            case "Redvenger":
                return true;
            case "Souls":
                return false;
            case "Syncroharon":
                return true;
            case "Tarian":
                return false;
            case "Uni":
                return true;
            case "Ultrion":
                return false;
            case "Zodiac":
                return false;
            case "Zerox":
                return false;
            default:
                return false;
        }

    }
}
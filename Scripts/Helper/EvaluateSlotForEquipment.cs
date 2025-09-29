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
            case AppConstants.Equipment.Teyric:
                slot = 16;
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
            case AppConstants.Equipment.Teyric:
                return true;
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
    public static string BackgroundImageForEquipment(string type)
    {
        switch (type)
        {
            case AppConstants.Equipment.Amnitus:
                return ImageConstants.Background.CardBackground1;
            case AppConstants.Equipment.Angelis:
                return ImageConstants.Background.CardBackground2;
            case AppConstants.Equipment.Bellion:
                return ImageConstants.Background.CardBackground3;
            case AppConstants.Equipment.Benzamin:
                return ImageConstants.Background.CardBackground4;
            case AppConstants.Equipment.Celestial:
                return ImageConstants.Background.CardBackground5;
            case AppConstants.Equipment.Ceverus:
                return ImageConstants.Background.CardBackground6;
            case AppConstants.Equipment.Delius:
                return ImageConstants.Background.CardBackground7;
            case AppConstants.Equipment.Domitius:
                return ImageConstants.Background.CardBackground8;
            case AppConstants.Equipment.Everlyn:
                return ImageConstants.Background.CardBackground9;
            case AppConstants.Equipment.Extra:
                return ImageConstants.Background.CardBackground10;
            case AppConstants.Equipment.Faltus:
                return ImageConstants.Background.CardBackground11;
            case AppConstants.Equipment.Fealan:
                return ImageConstants.Background.CardBackground12;
            case AppConstants.Equipment.Gamma:
                return ImageConstants.Background.CardBackground13;
            case AppConstants.Equipment.Gem:
                return ImageConstants.Background.CardBackground14;
            case AppConstants.Equipment.Hagoro:
                return ImageConstants.Background.CardBackground15;
            case AppConstants.Equipment.Hakalite:
                return ImageConstants.Background.CardBackground16;
            case AppConstants.Equipment.Ignis:
                return ImageConstants.Background.CardBackground17;
            case AppConstants.Equipment.Ivitus:
                return ImageConstants.Background.CardBackground18;
            case AppConstants.Equipment.Jorvan:
                return ImageConstants.Background.CardBackground19;
            case AppConstants.Equipment.Jullian:
                return ImageConstants.Background.CardBackground20;
            case AppConstants.Equipment.Karis:
                return ImageConstants.Background.CardBackground21;
            case AppConstants.Equipment.Karmus:
                return ImageConstants.Background.CardBackground22;
            case AppConstants.Equipment.Lotus:
                return ImageConstants.Background.CardBackground23;
            case AppConstants.Equipment.Luminius:
                return ImageConstants.Background.CardBackground24;
            case AppConstants.Equipment.Macus:
                return ImageConstants.Background.CardBackground25;
            case AppConstants.Equipment.Morganis:
                return ImageConstants.Background.CardBackground26;
            case AppConstants.Equipment.Nimigazin:
                return ImageConstants.Background.CardBackground27;
            case AppConstants.Equipment.Nova:
                return ImageConstants.Background.CardBackground28;
            case AppConstants.Equipment.Omonitus:
                return ImageConstants.Background.CardBackground29;
            case AppConstants.Equipment.Omega:
                return ImageConstants.Background.CardBackground30;
            case AppConstants.Equipment.Pet:
                return ImageConstants.Background.CardBackground31;
            case AppConstants.Equipment.Pyros:
                return ImageConstants.Background.CardBackground32;
            case AppConstants.Equipment.Qiyantus:
                return ImageConstants.Background.CardBackground33;
            case AppConstants.Equipment.Quasar:
                return ImageConstants.Background.CardBackground34;
            case AppConstants.Equipment.Rainbow:
                return ImageConstants.Background.CardBackground35;
            case AppConstants.Equipment.Redvenger:
                return ImageConstants.Background.CardBackground36;
            case AppConstants.Equipment.Souls:
                return ImageConstants.Background.CardBackground37;
            case AppConstants.Equipment.Syncroharon:
                return ImageConstants.Background.CardBackground38;
            case AppConstants.Equipment.Tarian:
                return ImageConstants.Background.CardBackground39;
            case AppConstants.Equipment.Teyric:
                return ImageConstants.Background.CardBackground40;
            case AppConstants.Equipment.Uni:
                return ImageConstants.Background.CardBackground41;
            case AppConstants.Equipment.Ultrion:
                return ImageConstants.Background.CardBackground42;
            case AppConstants.Equipment.Varethion:
                return ImageConstants.Background.CardBackground43;
            case AppConstants.Equipment.Velmira:
                return ImageConstants.Background.CardBackground44;
            case AppConstants.Equipment.Wenlithar:
                return ImageConstants.Background.CardBackground45;
            case AppConstants.Equipment.Wyrmora:
                return ImageConstants.Background.CardBackground46;
            case AppConstants.Equipment.Xaltheon:
                return ImageConstants.Background.CardBackground47;
            case AppConstants.Equipment.Xyralis:
                return ImageConstants.Background.CardBackground48;
            case AppConstants.Equipment.Yloran:
                return ImageConstants.Background.CardBackground49;
            case AppConstants.Equipment.Yvarion:
                return ImageConstants.Background.CardBackground50;
            case AppConstants.Equipment.Zodiac:
                return ImageConstants.Background.CardBackground51;
            case AppConstants.Equipment.Zerox:
                return ImageConstants.Background.CardBackground52;
            default:
                return ImageConstants.Background.CardBackground1;
        }
    }
    public static string FrameImageForEquipment(string type)
    {
        switch (type)
        {
            case AppConstants.Equipment.Amnitus:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Angelis:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Bellion:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.Benzamin:
                return ImageConstants.Frame.CardFrame1;
            case AppConstants.Equipment.Celestial:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.Ceverus:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Delius:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Domitius:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Everlyn:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Extra:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Faltus:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Fealan:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.Gamma:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.Gem:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Hagoro:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Hakalite:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Ignis:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Ivitus:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Jorvan:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Jullian:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.Karis:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Karmus:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Lotus:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.Luminius:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Macus:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Morganis:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Nimigazin:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Nova:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.Omonitus:
                return ImageConstants.Frame.CardFrame1;
            case AppConstants.Equipment.Omega:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Pet:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.Pyros:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Qiyantus:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Quasar:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.Rainbow:
                return ImageConstants.Frame.CardFrame1;
            case AppConstants.Equipment.Redvenger:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.Souls:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Syncroharon:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.Tarian:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Teyric:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Uni:
                return ImageConstants.Frame.CardFrame1;
            case AppConstants.Equipment.Ultrion:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.Varethion:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Velmira:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Wenlithar:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Wyrmora:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.Xaltheon:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Xyralis:
                return ImageConstants.Frame.CardFrame1;
            case AppConstants.Equipment.Yloran:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.Yvarion:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.Zodiac:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.Zerox:
                return ImageConstants.Frame.CardFrame2;
            default:
                return ImageConstants.Frame.CardFrame1;
        }

    }
}
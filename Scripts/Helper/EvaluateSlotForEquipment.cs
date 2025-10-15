using System.Collections.Generic;
public static class EvaluateSlotForEquipment
{
    public static int CheckSlotForEquipments(string type)
    {
        int slot = 0;
        switch (type)
        {
            case AppConstants.Equipment.AMNITUS:
                slot = 4;
                break;
            case AppConstants.Equipment.ANGELIS:
                slot = 1;
                break;
            case AppConstants.Equipment.BELLION:
                slot = 16;
                break;
            case AppConstants.Equipment.BENZAMIN:
                slot = 4;
                break;
            case AppConstants.Equipment.CELESTIAL:
                slot = 4;
                break;
            case AppConstants.Equipment.CEVERUS:
                slot = 10;
                break;
            case AppConstants.Equipment.DELIUS:
                slot = 10;
                break;
            case AppConstants.Equipment.DOMITIUS:
                slot = 8;
                break;
            case AppConstants.Equipment.EVERLYN:
                slot = 6;
                break;
            case AppConstants.Equipment.EXTRA:
                slot = 4;
                break;
            case AppConstants.Equipment.FAILTUS:
                slot = 16;
                break;
            case AppConstants.Equipment.FEALAN:
                slot = 16;
                break;
            case AppConstants.Equipment.GAMMA:
                slot = 8;
                break;
            case AppConstants.Equipment.GEM:
                slot = 8;
                break;
            case AppConstants.Equipment.HAGORO:
                slot = 6;
                break;
            case AppConstants.Equipment.HAKALITE:
                slot = 4;
                break;
            case AppConstants.Equipment.IGNIS:
                slot = 16;
                break;
            case AppConstants.Equipment.IVITUS:
                slot = 14;
                break;
            case AppConstants.Equipment.JORVAN:
                slot = 10;
                break;
            case AppConstants.Equipment.JULLIAN:
                slot = 10;
                break;
            case AppConstants.Equipment.KARIS:
                slot = 8;
                break;
            case AppConstants.Equipment.KARMUS:
                slot = 8;
                break;
            case AppConstants.Equipment.LOTUS:
                slot = 16;
                break;
            case AppConstants.Equipment.LUMINIUS:
                slot = 1;
                break;
            case AppConstants.Equipment.MACUS:
                slot = 14;
                break;
            case AppConstants.Equipment.MORGANIS:
                slot = 12;
                break;
            case AppConstants.Equipment.NIMIGAZIN:
                slot = 14;
                break;
            case AppConstants.Equipment.NOVA:
                slot = 4;
                break;
            case AppConstants.Equipment.OMONITUS:
                slot = 4;
                break;
            case AppConstants.Equipment.OMEGA:
                slot = 8;
                break;
            case AppConstants.Equipment.PET:
                slot = 6;
                break;
            case AppConstants.Equipment.PYROS:
                slot = 16;
                break;
            case AppConstants.Equipment.QIYANTUS:
                slot = 1;
                break;
            case AppConstants.Equipment.QUASAR:
                slot = 1;
                break;
            case AppConstants.Equipment.RAINBOW:
                slot = 4;
                break;
            case AppConstants.Equipment.REDVENGER:
                slot = 6;
                break;
            case AppConstants.Equipment.SOULS:
                slot = 10;
                break;
            case AppConstants.Equipment.SYNCROHARON:
                slot = 16;
                break;
            case AppConstants.Equipment.TARIAN:
                slot = 1;
                break;
            case AppConstants.Equipment.TEYRIC:
                slot = 16;
                break;
            case AppConstants.Equipment.UNI:
                slot = 16;
                break;
            case AppConstants.Equipment.ULTRION:
                slot = 4;
                break;
            case AppConstants.Equipment.VARETHION:
                slot = 16;
                break;
            case AppConstants.Equipment.VELMIRA:
                slot = 12;
                break;
            case AppConstants.Equipment.WENLITHAR:
                slot = 8;
                break;
            case AppConstants.Equipment.WYRMORA:
                slot = 16;
                break;
            case AppConstants.Equipment.XALTHEON:
                slot = 16;
                break;
            case AppConstants.Equipment.XYRALIS:
                slot = 10;
                break;
            case AppConstants.Equipment.YLORAN:
                slot = 16;
                break;
            case AppConstants.Equipment.YVARION:
                slot = 10;
                break;
            case AppConstants.Equipment.ZODIAC:
                slot = 12;
                break;
            case AppConstants.Equipment.ZEROX:
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
            case AppConstants.Equipment.AMNITUS:
                return true;
            case AppConstants.Equipment.ANGELIS:
                return false;
            case AppConstants.Equipment.BELLION:
                return true;
            case AppConstants.Equipment.BENZAMIN:
                return false;
            case AppConstants.Equipment.CELESTIAL:
                return true;
            case AppConstants.Equipment.CEVERUS:
                return true;
            case AppConstants.Equipment.DELIUS:
                return true;
            case AppConstants.Equipment.DOMITIUS:
                return true;
            case AppConstants.Equipment.EVERLYN:
                return true;
            case AppConstants.Equipment.EXTRA:
                return true;
            case AppConstants.Equipment.FAILTUS:
                return true;
            case AppConstants.Equipment.FEALAN:
                return true;
            case AppConstants.Equipment.GAMMA:
                return true;
            case AppConstants.Equipment.GEM:
                return true;
            case AppConstants.Equipment.HAGORO:
                return true;
            case AppConstants.Equipment.HAKALITE:
                return true;
            case AppConstants.Equipment.IGNIS:
                return false;
            case AppConstants.Equipment.IVITUS:
                return true;
            case AppConstants.Equipment.JORVAN:
                return false;
            case AppConstants.Equipment.JULLIAN:
                return true;
            case AppConstants.Equipment.KARIS:
                return true;
            case AppConstants.Equipment.KARMUS:
                return false;
            case AppConstants.Equipment.LOTUS:
                return false;
            case AppConstants.Equipment.LUMINIUS:
                return false;
            case AppConstants.Equipment.MACUS:
                return false;
            case AppConstants.Equipment.MORGANIS:
                return true;
            case AppConstants.Equipment.NIMIGAZIN:
                return true;
            case AppConstants.Equipment.NOVA:
                return true;
            case AppConstants.Equipment.OMONITUS:
                return true;
            case AppConstants.Equipment.OMEGA:
                return false;
            case AppConstants.Equipment.PET:
                return true;
            case AppConstants.Equipment.PYROS:
                return true;
            case AppConstants.Equipment.QIYANTUS:
                return false;
            case AppConstants.Equipment.QUASAR:
                return false;
            case AppConstants.Equipment.RAINBOW:
                return true;
            case AppConstants.Equipment.REDVENGER:
                return true;
            case AppConstants.Equipment.SOULS:
                return false;
            case AppConstants.Equipment.SYNCROHARON:
                return true;
            case AppConstants.Equipment.TARIAN:
                return false;
            case AppConstants.Equipment.TEYRIC:
                return true;
            case AppConstants.Equipment.UNI:
                return true;
            case AppConstants.Equipment.ULTRION:
                return false;
            case AppConstants.Equipment.VARETHION:
                return true;
            case AppConstants.Equipment.VELMIRA:
                return true;
            case AppConstants.Equipment.WENLITHAR:
                return true;
            case AppConstants.Equipment.WYRMORA:
                return true;
            case AppConstants.Equipment.XALTHEON:
                return true;
            case AppConstants.Equipment.XYRALIS:
                return false;
            case AppConstants.Equipment.YLORAN:
                return true;
            case AppConstants.Equipment.YVARION:
                return false;
            case AppConstants.Equipment.ZODIAC:
                return false;
            case AppConstants.Equipment.ZEROX:
                return false;
            default:
                return false;
        }

    }
    public static string BackgroundImageForEquipment(string type)
    {
        switch (type)
        {
            case AppConstants.Equipment.AMNITUS:
                return ImageConstants.Background.CardBackground1;
            case AppConstants.Equipment.ANGELIS:
                return ImageConstants.Background.CardBackground2;
            case AppConstants.Equipment.BELLION:
                return ImageConstants.Background.CardBackground3;
            case AppConstants.Equipment.BENZAMIN:
                return ImageConstants.Background.CardBackground4;
            case AppConstants.Equipment.CELESTIAL:
                return ImageConstants.Background.CardBackground5;
            case AppConstants.Equipment.CEVERUS:
                return ImageConstants.Background.CardBackground6;
            case AppConstants.Equipment.DELIUS:
                return ImageConstants.Background.CardBackground7;
            case AppConstants.Equipment.DOMITIUS:
                return ImageConstants.Background.CardBackground8;
            case AppConstants.Equipment.EVERLYN:
                return ImageConstants.Background.CardBackground9;
            case AppConstants.Equipment.EXTRA:
                return ImageConstants.Background.CardBackground10;
            case AppConstants.Equipment.FAILTUS:
                return ImageConstants.Background.CardBackground11;
            case AppConstants.Equipment.FEALAN:
                return ImageConstants.Background.CardBackground12;
            case AppConstants.Equipment.GAMMA:
                return ImageConstants.Background.CardBackground13;
            case AppConstants.Equipment.GEM:
                return ImageConstants.Background.CardBackground14;
            case AppConstants.Equipment.HAGORO:
                return ImageConstants.Background.CardBackground15;
            case AppConstants.Equipment.HAKALITE:
                return ImageConstants.Background.CardBackground16;
            case AppConstants.Equipment.IGNIS:
                return ImageConstants.Background.CardBackground17;
            case AppConstants.Equipment.IVITUS:
                return ImageConstants.Background.CardBackground18;
            case AppConstants.Equipment.JORVAN:
                return ImageConstants.Background.CardBackground19;
            case AppConstants.Equipment.JULLIAN:
                return ImageConstants.Background.CardBackground20;
            case AppConstants.Equipment.KARIS:
                return ImageConstants.Background.CardBackground21;
            case AppConstants.Equipment.KARMUS:
                return ImageConstants.Background.CardBackground22;
            case AppConstants.Equipment.LOTUS:
                return ImageConstants.Background.CardBackground23;
            case AppConstants.Equipment.LUMINIUS:
                return ImageConstants.Background.CardBackground24;
            case AppConstants.Equipment.MACUS:
                return ImageConstants.Background.CardBackground25;
            case AppConstants.Equipment.MORGANIS:
                return ImageConstants.Background.CardBackground26;
            case AppConstants.Equipment.NIMIGAZIN:
                return ImageConstants.Background.CardBackground27;
            case AppConstants.Equipment.NOVA:
                return ImageConstants.Background.CardBackground28;
            case AppConstants.Equipment.OMONITUS:
                return ImageConstants.Background.CardBackground29;
            case AppConstants.Equipment.OMEGA:
                return ImageConstants.Background.CardBackground30;
            case AppConstants.Equipment.PET:
                return ImageConstants.Background.CardBackground31;
            case AppConstants.Equipment.PYROS:
                return ImageConstants.Background.CardBackground32;
            case AppConstants.Equipment.QIYANTUS:
                return ImageConstants.Background.CardBackground33;
            case AppConstants.Equipment.QUASAR:
                return ImageConstants.Background.CardBackground34;
            case AppConstants.Equipment.RAINBOW:
                return ImageConstants.Background.CardBackground35;
            case AppConstants.Equipment.REDVENGER:
                return ImageConstants.Background.CardBackground36;
            case AppConstants.Equipment.SOULS:
                return ImageConstants.Background.CardBackground37;
            case AppConstants.Equipment.SYNCROHARON:
                return ImageConstants.Background.CardBackground38;
            case AppConstants.Equipment.TARIAN:
                return ImageConstants.Background.CardBackground39;
            case AppConstants.Equipment.TEYRIC:
                return ImageConstants.Background.CardBackground40;
            case AppConstants.Equipment.UNI:
                return ImageConstants.Background.CardBackground41;
            case AppConstants.Equipment.ULTRION:
                return ImageConstants.Background.CardBackground42;
            case AppConstants.Equipment.VARETHION:
                return ImageConstants.Background.CardBackground43;
            case AppConstants.Equipment.VELMIRA:
                return ImageConstants.Background.CardBackground44;
            case AppConstants.Equipment.WENLITHAR:
                return ImageConstants.Background.CardBackground45;
            case AppConstants.Equipment.WYRMORA:
                return ImageConstants.Background.CardBackground46;
            case AppConstants.Equipment.XALTHEON:
                return ImageConstants.Background.CardBackground47;
            case AppConstants.Equipment.XYRALIS:
                return ImageConstants.Background.CardBackground48;
            case AppConstants.Equipment.YLORAN:
                return ImageConstants.Background.CardBackground49;
            case AppConstants.Equipment.YVARION:
                return ImageConstants.Background.CardBackground50;
            case AppConstants.Equipment.ZODIAC:
                return ImageConstants.Background.CardBackground51;
            case AppConstants.Equipment.ZEROX:
                return ImageConstants.Background.CardBackground52;
            default:
                return ImageConstants.Background.CardBackground1;
        }
    }
    public static string FrameImageForEquipment(string type)
    {
        switch (type)
        {
            case AppConstants.Equipment.AMNITUS:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.ANGELIS:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.BELLION:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.BENZAMIN:
                return ImageConstants.Frame.CardFrame1;
            case AppConstants.Equipment.CELESTIAL:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.CEVERUS:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.DELIUS:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.DOMITIUS:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.EVERLYN:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.EXTRA:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.FAILTUS:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.FEALAN:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.GAMMA:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.GEM:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.HAGORO:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.HAKALITE:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.IGNIS:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.IVITUS:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.JORVAN:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.JULLIAN:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.KARIS:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.KARMUS:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.LOTUS:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.LUMINIUS:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.MACUS:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.MORGANIS:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.NIMIGAZIN:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.NOVA:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.OMONITUS:
                return ImageConstants.Frame.CardFrame1;
            case AppConstants.Equipment.OMEGA:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.PET:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.PYROS:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.QIYANTUS:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.QUASAR:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.RAINBOW:
                return ImageConstants.Frame.CardFrame1;
            case AppConstants.Equipment.REDVENGER:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.SOULS:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.SYNCROHARON:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.TARIAN:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.TEYRIC:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.UNI:
                return ImageConstants.Frame.CardFrame1;
            case AppConstants.Equipment.ULTRION:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.VARETHION:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.VELMIRA:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.WENLITHAR:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.WYRMORA:
                return ImageConstants.Frame.CardFrame2;
            case AppConstants.Equipment.XALTHEON:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.XYRALIS:
                return ImageConstants.Frame.CardFrame1;
            case AppConstants.Equipment.YLORAN:
                return ImageConstants.Frame.CardFrame3;
            case AppConstants.Equipment.YVARION:
                return ImageConstants.Frame.CardFrame5;
            case AppConstants.Equipment.ZODIAC:
                return ImageConstants.Frame.CardFrame4;
            case AppConstants.Equipment.ZEROX:
                return ImageConstants.Frame.CardFrame2;
            default:
                return ImageConstants.Frame.CardFrame1;
        }

    }
}
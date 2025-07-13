using System.Collections.Generic;
public static class EvaluateSlotForEquipment
{
    public static int CheckSlotForEquipments(string type)
    {
        int slot = 0;
        switch (type)
        {
            case "Amnitus_Equipment":
                slot = 4;
                break;
            case "Angelis_Equipment":
                slot = 1;
                break;
            case "Bellion_Equipment":
                slot = 16;
                break;
            case "Benzamin_Equipment":
                slot = 4;
                break;
            case "Celestial_Equipment":
                slot = 4;
                break;
            case "Ceverus_Equipment":
                slot = 10;
                break;
            case "Delius_Equipment":
                slot = 10;
                break;
            case "Domitius_Equipment":
                slot = 8;
                break;
            case "Everlyn_Equipment":
                slot = 6;
                break;
            case "Extra_Equipment":
                slot = 4;
                break;
            case "Faltus_Equipment":
                slot = 16;
                break;
            case "Fealan_Equipment":
                slot = 16;
                break;
            case "Gamma_Equipment":
                slot = 8;
                break;
            case "Gem_Equipment":
                slot = 8;
                break;
            case "Hagoro_Equipment":
                slot = 6;
                break;
            case "Hakalite_Equipment":
                slot = 4;
                break;
            case "Ignis_Equipment":
                slot = 16;
                break;
            case "Ivitus_Equipment":
                slot = 14;
                break;
            case "Jorvan_Equipment":
                slot = 10;
                break;
            case "Jullian_Equipment":
                slot = 10;
                break;
            case "Karis_Equipment":
                slot = 8;
                break;
            case "Karmus_Equipment":
                slot = 8;
                break;
            case "Lotus_Equipment":
                slot = 16;
                break;
            case "Luminius_Equipment":
                slot = 1;
                break;
            case "Macus_Equipment":
                slot = 14;
                break;
            case "Morganis_Equipment":
                slot = 12;
                break;
            case "Nimigazin_Equipment":
                slot = 14;
                break;
            case "Nova_Equipment":
                slot = 4;
                break;
            case "Omonitus_Equipment":
                slot = 4;
                break;
            case "Omega_Equipment":
                slot = 8;
                break;
            case "Pet_Equipment":
                slot = 6;
                break;
            case "Pyros_Equipment":
                slot = 16;
                break;
            case "Qiyantus_Equipment":
                slot = 1;
                break;
            case "Quasar_Equipment":
                slot = 1;
                break;
            case "Rainbow_Equipment":
                slot = 4;
                break;
            case "Redvenger_Equipment":
                slot = 6;
                break;
            case "Souls_Equipment":
                slot = 10;
                break;
            case "Syncroharon_Equipment":
                slot = 16;
                break;
            case "Tarian_Equipment":
                slot = 1;
                break;
            case "Uni_Equipment":
                slot = 16;
                break;
            case "Ultrion_Equipment":
                slot = 4;
                break;
            case "Zodiac_Equipment":
                slot = 12;
                break;
            case "Zerox_Equipment":
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
            case "Amnitus_Equipment":
                return true;
            case "Angelis_Equipment":
                return false;
            case "Bellion_Equipment":
                return true;
            case "Benzamin_Equipment":
                return false;
            case "Celestial_Equipment":
                return true;
            case "Ceverus_Equipment":
                return true;
            case "Delius_Equipment":
                return true;
            case "Domitius_Equipment":
                return true;
            case "Everlyn_Equipment":
                return true;
            case "Extra_Equipment":
                return true;
            case "Faltus_Equipment":
                return true;
            case "Fealan_Equipment":
                return true;
            case "Gamma_Equipment":
                return true;
            case "Gem_Equipment":
                return true;
            case "Hagoro_Equipment":
                return true;
            case "Hakalite_Equipment":
                return true;
            case "Ignis_Equipment":
                return false;
            case "Ivitus_Equipment":
                return true;
            case "Jorvan_Equipment":
                return false;
            case "Jullian_Equipment":
                return true;
            case "Karis_Equipment":
                return true;
            case "Karmus_Equipment":
                return false;
            case "Lotus_Equipment":
                return false;
            case "Luminius_Equipment":
                return false;
            case "Macus_Equipment":
                return false;
            case "Morganis_Equipment":
                return true;
            case "Nimigazin_Equipment":
                return true;
            case "Nova_Equipment":
                return true;
            case "Omonitus_Equipment":
                return true;
            case "Omega_Equipment":
                return false;
            case "Pet_Equipment":
                return true;
            case "Pyros_Equipment":
                return true;
            case "Qiyantus_Equipment":
                return false;
            case "Quasar_Equipment":
                return false;
            case "Rainbow_Equipment":
                return true;
            case "Redvenger_Equipment":
                return true;
            case "Souls_Equipment":
                return false;
            case "Syncroharon_Equipment":
                return true;
            case "Tarian_Equipment":
                return false;
            case "Uni_Equipment":
                return true;
            case "Ultrion_Equipment":
                return false;
            case "Zodiac_Equipment":
                return false;
            case "Zerox_Equipment":
                return false;
            default:
                return false;
        }
    }
}
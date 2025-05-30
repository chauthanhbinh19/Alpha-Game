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
            case "Etherium_Equipment":
                slot = 10;
                break;
            case "Everlyn_Equipment":
                slot = 6;
                break;
            case "EvilFruit_Equipment":
                slot = 10;
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
            case "Support_Equipment":
                slot = 1;
                break;
            case "Syncroharon_Equipment":
                slot = 16;
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
}
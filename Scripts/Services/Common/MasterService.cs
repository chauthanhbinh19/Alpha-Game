using System.Collections.Generic;

public class MasterService : IMasterService
{
    public Master EnhanceMaster(Master master, int level, int multiplier = 1)
    {
        int startLevel = master.level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                master.health += 10000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                master.physical_attack += 1500000 * statMultiplier;
                master.physical_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                master.magical_attack += 1500000 * statMultiplier;
                master.magical_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                master.chemical_attack += 1500000 * statMultiplier;
                master.chemical_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                master.atomic_attack += 1500000 * statMultiplier;
                master.atomic_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                master.mental_attack += 1500000 * statMultiplier;
                master.mental_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                master.speed += 1500000 * statMultiplier;
                master.critical_damage_rate += 0.1 * statMultiplier;
                master.critical_rate += 0.1 * statMultiplier;
                master.critical_resistance_rate += 0.1 * statMultiplier;
                master.ignore_critical_rate += 0.1 * statMultiplier;
                master.penetration_rate += 0.1 * statMultiplier;
                master.penetration_resistance_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                master.evasion_rate += 0.1 * statMultiplier;
                master.damage_absorption_rate += 0.1 * statMultiplier;
                master.ignore_damage_absorption_rate += 0.1 * statMultiplier;
                master.absorbed_damage_rate += 0.1 * statMultiplier;
                master.vitality_regeneration_rate += 0.1 * statMultiplier;
                master.vitality_regeneration_resistance_rate += 0.1 * statMultiplier;
                master.accuracy_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                master.lifesteal_rate += 0.1 * statMultiplier;
                master.mana += 1500000 * statMultiplier;
                master.mana_regeneration_rate += 0.1 * statMultiplier;
                master.shield_strength += 1500000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                master.tenacity += 0.5 * statMultiplier;
                master.resistance_rate += 0.1 * statMultiplier;
                master.combo_rate += 0.1 * statMultiplier;
                master.ignore_combo_rate += 0.1 * statMultiplier;
                master.combo_damage_rate += 0.1 * statMultiplier;
                master.combo_resistance_rate += 0.1 * statMultiplier;
                master.stun_rate += 0.1 * statMultiplier;
                master.ignore_stun_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                master.reflection_rate += 0.1 * statMultiplier;
                master.ignore_reflection_rate += 0.1 * statMultiplier;
                master.reflection_damage_rate += 0.1 * statMultiplier;
                master.reflection_resistance_rate += 0.1 * statMultiplier;
                master.damage_to_different_faction_rate += 0.1 * statMultiplier;
                master.resistance_to_different_faction_rate += 0.1 * statMultiplier;
                master.damage_to_same_faction_rate += 0.1 * statMultiplier;
                master.resistance_to_same_faction_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                master.normal_damage_rate += 0.1 * statMultiplier;
                master.normal_resistance_rate += 0.1 * statMultiplier;
                master.skill_damage_rate += 0.1 * statMultiplier;
                master.skill_resistance_rate += 0.1 * statMultiplier;
                master.percent_all_health += 5 * statMultiplier;
                master.percent_all_physical_attack += 5 * statMultiplier;
                master.percent_all_physical_defense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                master.percent_all_magical_attack += 5 * statMultiplier;
                master.percent_all_magical_defense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                master.percent_all_chemical_attack += 5 * statMultiplier;
                master.percent_all_chemical_defense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                master.percent_all_atomic_attack += 5 * statMultiplier;
                master.percent_all_atomic_defense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                master.percent_all_mental_attack += 5 * statMultiplier;
                master.percent_all_mental_defense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                master.physical_attack += 1500000 * statMultiplier;
                master.magical_attack += 1500000 * statMultiplier;
                master.chemical_attack += 1500000 * statMultiplier;
                master.atomic_attack += 1500000 * statMultiplier;
                master.mental_attack += 1500000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                master.physical_defense += 1500000 * statMultiplier;
                master.magical_defense += 1500000 * statMultiplier;
                master.chemical_defense += 1500000 * statMultiplier;
                master.atomic_defense += 1500000 * statMultiplier;
                master.mental_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                master.speed += 1500000 * statMultiplier;
                master.critical_damage_rate += 0.1 * statMultiplier;
                master.critical_rate += 0.1 * statMultiplier;
                master.penetration_rate += 0.1 * statMultiplier;
                master.evasion_rate += 0.1 * statMultiplier;
                master.damage_absorption_rate += 0.1 * statMultiplier;
                master.vitality_regeneration_rate += 0.1 * statMultiplier;
                master.accuracy_rate += 0.1 * statMultiplier;
                master.lifesteal_rate += 0.1 * statMultiplier;
                master.mana += 1500000 * statMultiplier;
                master.mana_regeneration_rate += 0.1 * statMultiplier;
                master.shield_strength += 1500000 * statMultiplier;
                master.tenacity += 0.5 * statMultiplier;
                master.resistance_rate += 0.1 * statMultiplier;
                master.combo_rate += 0.1 * statMultiplier;
                master.reflection_rate += 0.1 * statMultiplier;
                master.damage_to_different_faction_rate += 0.1 * statMultiplier;
                master.resistance_to_different_faction_rate += 0.1 * statMultiplier;
                master.damage_to_same_faction_rate += 0.1 * statMultiplier;
                master.resistance_to_same_faction_rate += 0.1 * statMultiplier;
            }
        }

        master.level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return master;
    }
    public void UpLevel(object data, Master master, string type)
    {
        if (data is CardHeroes cardHeroes)
        {
            UserCardHeroesMasterService.Create().InsertOrUpdateCardHeroesMaster(master, type, cardHeroes.id);
        }
        else if (data is Books books)
        {
            UserBooksMasterService.Create().InsertOrUpdateBooksMaster(master, type, books.id);
        }
        else if (data is CardCaptains cardCaptains)
        {
            UserCardCaptainsMasterService.Create().InsertOrUpdateCardCaptainsMaster(master, type, cardCaptains.id);
        }
        else if (data is Pets pets)
        {
            UserPetsMasterService.Create().InsertOrUpdatePetsMaster(master, type, pets.id);
        }
        else if (data is CardMilitary cardMilitary)
        {
            UserCardMilitaryMasterService.Create().InsertOrUpdateCardMilitaryMaster(master, type, cardMilitary.id);
        }
        else if (data is CardSpell cardSpell)
        {
            UserCardSpellMasterService.Create().InsertOrUpdateCardSpellMaster(master, type, cardSpell.id);
        }
        else if (data is CardMonsters cardMonsters)
        {
            UserCardMonstersMasterService.Create().InsertOrUpdateCardMonstersMaster(master, type, cardMonsters.id);
        }
        else if (data is CardColonels cardColonels)
        {
            UserCardColonelsMasterService.Create().InsertOrUpdateCardColonelsMaster(master, type, cardColonels.id);
        }
        else if (data is CardGenerals cardGenerals)
        {
            UserCardGeneralsMasterService.Create().InsertOrUpdateCardGeneralsMaster(master, type, cardGenerals.id);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            UserCardAdmiralsMasterService.Create().InsertOrUpdateCardAdmiralsMaster(master, type, cardAdmirals.id);
        }
    }
}
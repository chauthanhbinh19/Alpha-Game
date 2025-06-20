using System.Collections.Generic;

public class RankService : IRankService
{
    public Rank EnhanceRank(Rank rank, int level)
    {
        int startLevel = rank.level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = 1;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                rank.health += 10000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                rank.physical_attack += 1500000 * statMultiplier;
                rank.physical_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                rank.magical_attack += 1500000 * statMultiplier;
                rank.magical_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                rank.chemical_attack += 1500000 * statMultiplier;
                rank.chemical_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                rank.atomic_attack += 1500000 * statMultiplier;
                rank.atomic_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                rank.mental_attack += 1500000 * statMultiplier;
                rank.mental_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                rank.speed += 1500000 * statMultiplier;
                rank.critical_damage_rate += 0.1 * statMultiplier;
                rank.critical_rate += 0.1 * statMultiplier;
                rank.critical_resistance_rate += 0.1 * statMultiplier;
                rank.ignore_critical_rate += 0.1 * statMultiplier;
                rank.penetration_rate += 0.1 * statMultiplier;
                rank.penetration_resistance_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                rank.evasion_rate += 0.1 * statMultiplier;
                rank.damage_absorption_rate += 0.1 * statMultiplier;
                rank.ignore_damage_absorption_rate += 0.1 * statMultiplier;
                rank.absorbed_damage_rate += 0.1 * statMultiplier;
                rank.vitality_regeneration_rate += 0.1 * statMultiplier;
                rank.vitality_regeneration_resistance_rate += 0.1 * statMultiplier;
                rank.accuracy_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                rank.lifesteal_rate += 0.1 * statMultiplier;
                rank.mana += 1500000 * statMultiplier;
                rank.mana_regeneration_rate += 0.1 * statMultiplier;
                rank.shield_strength += 1500000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                rank.tenacity += 0.5 * statMultiplier;
                rank.resistance_rate += 0.1 * statMultiplier;
                rank.combo_rate += 0.1 * statMultiplier;
                rank.ignore_combo_rate += 0.1 * statMultiplier;
                rank.combo_damage_rate += 0.1 * statMultiplier;
                rank.combo_resistance_rate += 0.1 * statMultiplier;
                rank.stun_rate += 0.1 * statMultiplier;
                rank.ignore_stun_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                rank.reflection_rate += 0.1 * statMultiplier;
                rank.ignore_reflection_rate += 0.1 * statMultiplier;
                rank.reflection_damage_rate += 0.1 * statMultiplier;
                rank.reflection_resistance_rate += 0.1 * statMultiplier;
                rank.damage_to_different_faction_rate += 0.1 * statMultiplier;
                rank.resistance_to_different_faction_rate += 0.1 * statMultiplier;
                rank.damage_to_same_faction_rate += 0.1 * statMultiplier;
                rank.resistance_to_same_faction_rate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                rank.normal_damage_rate += 0.1 * statMultiplier;
                rank.normal_resistance_rate += 0.1 * statMultiplier;
                rank.skill_damage_rate += 0.1 * statMultiplier;
                rank.skill_resistance_rate += 0.1 * statMultiplier;
                rank.percent_all_health += 5 * statMultiplier;
                rank.percent_all_physical_attack += 5 * statMultiplier;
                rank.percent_all_physical_defense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                rank.percent_all_magical_attack += 5 * statMultiplier;
                rank.percent_all_magical_defense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                rank.percent_all_chemical_attack += 5 * statMultiplier;
                rank.percent_all_chemical_defense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                rank.percent_all_atomic_attack += 5 * statMultiplier;
                rank.percent_all_atomic_defense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                rank.percent_all_mental_attack += 5 * statMultiplier;
                rank.percent_all_mental_defense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                rank.physical_attack += 1500000 * statMultiplier;
                rank.magical_attack += 1500000 * statMultiplier;
                rank.chemical_attack += 1500000 * statMultiplier;
                rank.atomic_attack += 1500000 * statMultiplier;
                rank.mental_attack += 1500000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                rank.physical_defense += 1500000 * statMultiplier;
                rank.magical_defense += 1500000 * statMultiplier;
                rank.chemical_defense += 1500000 * statMultiplier;
                rank.atomic_defense += 1500000 * statMultiplier;
                rank.mental_defense += 1500000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                rank.speed += 1500000 * statMultiplier;
                rank.critical_damage_rate += 0.1 * statMultiplier;
                rank.critical_rate += 0.1 * statMultiplier;
                rank.penetration_rate += 0.1 * statMultiplier;
                rank.evasion_rate += 0.1 * statMultiplier;
                rank.damage_absorption_rate += 0.1 * statMultiplier;
                rank.vitality_regeneration_rate += 0.1 * statMultiplier;
                rank.accuracy_rate += 0.1 * statMultiplier;
                rank.lifesteal_rate += 0.1 * statMultiplier;
                rank.mana += 1500000 * statMultiplier;
                rank.mana_regeneration_rate += 0.1 * statMultiplier;
                rank.shield_strength += 1500000 * statMultiplier;
                rank.tenacity += 0.5 * statMultiplier;
                rank.resistance_rate += 0.1 * statMultiplier;
                rank.combo_rate += 0.1 * statMultiplier;
                rank.reflection_rate += 0.1 * statMultiplier;
                rank.damage_to_different_faction_rate += 0.1 * statMultiplier;
                rank.resistance_to_different_faction_rate += 0.1 * statMultiplier;
                rank.damage_to_same_faction_rate += 0.1 * statMultiplier;
                rank.resistance_to_same_faction_rate += 0.1 * statMultiplier;
            }
        }

        rank.level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return rank;
    }
    public void UpLevel(object data, Rank rank, string type)
    {
        if (data is CardHeroes cardHeroes)
        {
            UserCardHeroesRankService.Create().InsertOrUpdateCardHeroesRank(rank, type, cardHeroes.id);
        }
        else if (data is Books books)
        {
            UserBooksRankService.Create().InsertOrUpdateBooksRank(rank, type, books.id);
        }
        else if (data is CardCaptains cardCaptains)
        {
            UserCardCaptainsRankService.Create().InsertOrUpdateCardCaptainsRank(rank, type, cardCaptains.id);
        }
        else if (data is Pets pets)
        {
            UserPetsRankService.Create().InsertOrUpdatePetsRank(rank, type, pets.id);
        }
        else if (data is CardMilitary cardMilitary)
        {
            UserCardMilitaryRankService.Create().InsertOrUpdateCardMilitaryRank(rank, type, cardMilitary.id);
        }
        else if (data is CardSpell cardSpell)
        {
            UserCardSpellRankService.Create().InsertOrUpdateCardSpellRank(rank, type, cardSpell.id);
        }
        else if (data is CardMonsters cardMonsters)
        {
            UserCardMonstersRankService.Create().InsertOrUpdateCardMonstersRank(rank, type, cardMonsters.id);
        }
        else if (data is CardColonels cardColonels)
        {
            UserCardColonelsRankService.Create().InsertOrUpdateCardColonelsRank(rank, type, cardColonels.id);
        }
        else if (data is CardGenerals cardGenerals)
        {
            UserCardGeneralsRankService.Create().InsertOrUpdateCardGeneralsRank(rank, type, cardGenerals.id);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            UserCardAdmiralsRankService.Create().InsertOrUpdateCardAdmiralsRank(rank, type, cardAdmirals.id);
        }
    }
}
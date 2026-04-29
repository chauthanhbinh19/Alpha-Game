using System.Collections.Generic;
using System.Threading.Tasks;

public class RankService : IRankService
{
    public Rank EnhanceRank(Rank rank, int level, int multiplier = 1)
    {
        int startLevel = rank.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                rank.Health += 10000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                rank.PhysicalAttack += 1500000 * statMultiplier;
                rank.PhysicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                rank.MagicalAttack += 1500000 * statMultiplier;
                rank.MagicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                rank.ChemicalAttack += 1500000 * statMultiplier;
                rank.ChemicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                rank.AtomicAttack += 1500000 * statMultiplier;
                rank.AtomicDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                rank.MentalAttack += 1500000 * statMultiplier;
                rank.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                rank.Speed += 1500000 * statMultiplier;
                rank.CriticalDamageRate += 0.1 * statMultiplier;
                rank.CriticalRate += 0.1 * statMultiplier;
                rank.CriticalResistanceRate += 0.1 * statMultiplier;
                rank.IgnoreCriticalRate += 0.1 * statMultiplier;
                rank.PenetrationRate += 0.1 * statMultiplier;
                rank.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                rank.EvasionRate += 0.1 * statMultiplier;
                rank.DamageAbsorptionRate += 0.1 * statMultiplier;
                rank.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                rank.AbsorbedDamageRate += 0.1 * statMultiplier;
                rank.VitalityRegenerationRate += 0.1 * statMultiplier;
                rank.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                rank.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                rank.LifestealRate += 0.1 * statMultiplier;
                rank.Mana += 1500000 * statMultiplier;
                rank.ManaRegenerationRate += 0.1 * statMultiplier;
                rank.ShieldStrength += 1500000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                rank.Tenacity += 0.5 * statMultiplier;
                rank.ResistanceRate += 0.1 * statMultiplier;
                rank.ComboRate += 0.1 * statMultiplier;
                rank.IgnoreComboRate += 0.1 * statMultiplier;
                rank.ComboDamageRate += 0.1 * statMultiplier;
                rank.ComboResistanceRate += 0.1 * statMultiplier;
                rank.StunRate += 0.1 * statMultiplier;
                rank.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                rank.ReflectionRate += 0.1 * statMultiplier;
                rank.IgnoreReflectionRate += 0.1 * statMultiplier;
                rank.ReflectionDamageRate += 0.1 * statMultiplier;
                rank.ReflectionResistanceRate += 0.1 * statMultiplier;
                rank.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                rank.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                rank.DamageToSameFactionRate += 0.1 * statMultiplier;
                rank.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                rank.NormalDamageRate += 0.1 * statMultiplier;
                rank.NormalResistanceRate += 0.1 * statMultiplier;
                rank.SkillDamageRate += 0.1 * statMultiplier;
                rank.SkillResistanceRate += 0.1 * statMultiplier;
                rank.PercentAllHealth += 5 * statMultiplier;
                rank.PercentAllPhysicalAttack += 5 * statMultiplier;
                rank.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                rank.PercentAllMagicalAttack += 5 * statMultiplier;
                rank.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                rank.PercentAllChemicalAttack += 5 * statMultiplier;
                rank.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                rank.PercentAllAtomicAttack += 5 * statMultiplier;
                rank.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                rank.PercentAllMentalAttack += 5 * statMultiplier;
                rank.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                rank.PhysicalAttack += 1500000 * statMultiplier;
                rank.MagicalAttack += 1500000 * statMultiplier;
                rank.ChemicalAttack += 1500000 * statMultiplier;
                rank.AtomicAttack += 1500000 * statMultiplier;
                rank.MentalAttack += 1500000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                rank.PhysicalDefense += 1500000 * statMultiplier;
                rank.MagicalDefense += 1500000 * statMultiplier;
                rank.ChemicalDefense += 1500000 * statMultiplier;
                rank.AtomicDefense += 1500000 * statMultiplier;
                rank.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                rank.Speed += 1500000 * statMultiplier;
                rank.CriticalDamageRate += 0.1 * statMultiplier;
                rank.CriticalRate += 0.1 * statMultiplier;
                rank.PenetrationRate += 0.1 * statMultiplier;
                rank.EvasionRate += 0.1 * statMultiplier;
                rank.DamageAbsorptionRate += 0.1 * statMultiplier;
                rank.VitalityRegenerationRate += 0.1 * statMultiplier;
                rank.AccuracyRate += 0.1 * statMultiplier;
                rank.LifestealRate += 0.1 * statMultiplier;
                rank.Mana += 1500000 * statMultiplier;
                rank.ManaRegenerationRate += 0.1 * statMultiplier;
                rank.ShieldStrength += 1500000 * statMultiplier;
                rank.Tenacity += 0.5 * statMultiplier;
                rank.ResistanceRate += 0.1 * statMultiplier;
                rank.ComboRate += 0.1 * statMultiplier;
                rank.ReflectionRate += 0.1 * statMultiplier;
                rank.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                rank.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                rank.DamageToSameFactionRate += 0.1 * statMultiplier;
                rank.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        rank.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return rank;
    }
    public ScienceFiction EnhanceScienceFiction(ScienceFiction scienceFiction, int level, int multiplier = 1)
    {
        int startLevel = scienceFiction.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                scienceFiction.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                scienceFiction.PhysicalAttack += 15000000 * statMultiplier;
                scienceFiction.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                scienceFiction.MagicalAttack += 15000000 * statMultiplier;
                scienceFiction.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                scienceFiction.ChemicalAttack += 15000000 * statMultiplier;
                scienceFiction.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                scienceFiction.AtomicAttack += 15000000 * statMultiplier;
                scienceFiction.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                scienceFiction.MentalAttack += 15000000 * statMultiplier;
                scienceFiction.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                scienceFiction.Speed += 15000000 * statMultiplier;
                scienceFiction.CriticalDamageRate += 0.1 * statMultiplier;
                scienceFiction.CriticalRate += 0.1 * statMultiplier;
                scienceFiction.CriticalResistanceRate += 0.1 * statMultiplier;
                scienceFiction.IgnoreCriticalRate += 0.1 * statMultiplier;
                scienceFiction.PenetrationRate += 0.1 * statMultiplier;
                scienceFiction.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                scienceFiction.EvasionRate += 0.1 * statMultiplier;
                scienceFiction.DamageAbsorptionRate += 0.1 * statMultiplier;
                scienceFiction.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                scienceFiction.AbsorbedDamageRate += 0.1 * statMultiplier;
                scienceFiction.VitalityRegenerationRate += 0.1 * statMultiplier;
                scienceFiction.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                scienceFiction.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                scienceFiction.LifestealRate += 0.1 * statMultiplier;
                scienceFiction.Mana += 15000000 * statMultiplier;
                scienceFiction.ManaRegenerationRate += 0.1 * statMultiplier;
                scienceFiction.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                scienceFiction.Tenacity += 0.5 * statMultiplier;
                scienceFiction.ResistanceRate += 0.1 * statMultiplier;
                scienceFiction.ComboRate += 0.1 * statMultiplier;
                scienceFiction.IgnoreComboRate += 0.1 * statMultiplier;
                scienceFiction.ComboDamageRate += 0.1 * statMultiplier;
                scienceFiction.ComboResistanceRate += 0.1 * statMultiplier;
                scienceFiction.StunRate += 0.1 * statMultiplier;
                scienceFiction.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                scienceFiction.ReflectionRate += 0.1 * statMultiplier;
                scienceFiction.IgnoreReflectionRate += 0.1 * statMultiplier;
                scienceFiction.ReflectionDamageRate += 0.1 * statMultiplier;
                scienceFiction.ReflectionResistanceRate += 0.1 * statMultiplier;
                scienceFiction.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                scienceFiction.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                scienceFiction.DamageToSameFactionRate += 0.1 * statMultiplier;
                scienceFiction.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                scienceFiction.NormalDamageRate += 0.1 * statMultiplier;
                scienceFiction.NormalResistanceRate += 0.1 * statMultiplier;
                scienceFiction.SkillDamageRate += 0.1 * statMultiplier;
                scienceFiction.SkillResistanceRate += 0.1 * statMultiplier;
                scienceFiction.PercentAllHealth += 5 * statMultiplier;
                scienceFiction.PercentAllPhysicalAttack += 5 * statMultiplier;
                scienceFiction.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                scienceFiction.PercentAllMagicalAttack += 5 * statMultiplier;
                scienceFiction.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                scienceFiction.PercentAllChemicalAttack += 5 * statMultiplier;
                scienceFiction.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                scienceFiction.PercentAllAtomicAttack += 5 * statMultiplier;
                scienceFiction.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                scienceFiction.PercentAllMentalAttack += 5 * statMultiplier;
                scienceFiction.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                scienceFiction.PhysicalAttack += 15000000 * statMultiplier;
                scienceFiction.MagicalAttack += 15000000 * statMultiplier;
                scienceFiction.ChemicalAttack += 15000000 * statMultiplier;
                scienceFiction.AtomicAttack += 15000000 * statMultiplier;
                scienceFiction.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                scienceFiction.PhysicalDefense += 15000000 * statMultiplier;
                scienceFiction.MagicalDefense += 15000000 * statMultiplier;
                scienceFiction.ChemicalDefense += 15000000 * statMultiplier;
                scienceFiction.AtomicDefense += 15000000 * statMultiplier;
                scienceFiction.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                scienceFiction.Speed += 15000000 * statMultiplier;
                scienceFiction.CriticalDamageRate += 0.1 * statMultiplier;
                scienceFiction.CriticalRate += 0.1 * statMultiplier;
                scienceFiction.PenetrationRate += 0.1 * statMultiplier;
                scienceFiction.EvasionRate += 0.1 * statMultiplier;
                scienceFiction.DamageAbsorptionRate += 0.1 * statMultiplier;
                scienceFiction.VitalityRegenerationRate += 0.1 * statMultiplier;
                scienceFiction.AccuracyRate += 0.1 * statMultiplier;
                scienceFiction.LifestealRate += 0.1 * statMultiplier;
                scienceFiction.Mana += 15000000 * statMultiplier;
                scienceFiction.ManaRegenerationRate += 0.1 * statMultiplier;
                scienceFiction.ShieldStrength += 15000000 * statMultiplier;
                scienceFiction.Tenacity += 0.5 * statMultiplier;
                scienceFiction.ResistanceRate += 0.1 * statMultiplier;
                scienceFiction.ComboRate += 0.1 * statMultiplier;
                scienceFiction.ReflectionRate += 0.1 * statMultiplier;
                scienceFiction.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                scienceFiction.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                scienceFiction.DamageToSameFactionRate += 0.1 * statMultiplier;
                scienceFiction.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        scienceFiction.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return scienceFiction;
    }
    public async Task UpLevelAsync(object data, Rank rank, string type)
    {
        if (data is Equipments cardHero)
        {
            await UserCardHeroesRankService.Create().InsertOrUpdateCardHeroRankAsync(rank, cardHero.Id);
        }
        else if (data is Books book)
        {
            await UserBooksRankService.Create().InsertOrUpdateBookRankAsync(rank, book.Id);
        }
        else if (data is CardCaptains cardCaptain)
        {
            await UserCardCaptainsRankService.Create().InsertOrUpdateCardCaptainRankAsync(rank, cardCaptain.Id);
        }
        else if (data is Pets pet)
        {
            await UserPetsRankService.Create().InsertOrUpdatePetRankAsync(rank, pet.Id);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            await UserCardMilitariesRankService.Create().InsertOrUpdateCardMilitaryRankAsync(rank, cardMilitary.Id);
        }
        else if (data is CardSpells cardSpell)
        {
            await UserCardSpellsRankService.Create().InsertOrUpdateCardSpellRankAsync(rank, cardSpell.Id);
        }
        else if (data is CardMonsters cardMonster)
        {
            await UserCardMonstersRankService.Create().InsertOrUpdateCardMonsterRankAsync(rank, cardMonster.Id);
        }
        else if (data is CardColonels cardColonel)
        {
            await UserCardColonelsRankService.Create().InsertOrUpdateCardColonelRankAsync(rank, cardColonel.Id);
        }
        else if (data is CardGenerals cardGeneral)
        {
            await UserCardGeneralsRankService.Create().InsertOrUpdateCardGeneralRankAsync(rank, cardGeneral.Id);
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            await UserCardAdmiralsRankService.Create().InsertOrUpdateCardAdmiralRankAsync(rank, cardAdmiral.Id);
        }
    }
}
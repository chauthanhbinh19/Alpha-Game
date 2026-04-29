using System.Collections.Generic;
using System.Threading.Tasks;

public class MasterService : IMasterService
{
    public Master EnhanceMaster(Master master, int level, int multiplier = 1)
    {
        int startLevel = master.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                master.Health += 10000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                master.PhysicalAttack += 1500000 * statMultiplier;
                master.PhysicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                master.MagicalAttack += 1500000 * statMultiplier;
                master.MagicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                master.ChemicalAttack += 1500000 * statMultiplier;
                master.ChemicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                master.AtomicAttack += 1500000 * statMultiplier;
                master.AtomicDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                master.MentalAttack += 1500000 * statMultiplier;
                master.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                master.Speed += 1500000 * statMultiplier;
                master.CriticalDamageRate += 0.1 * statMultiplier;
                master.CriticalRate += 0.1 * statMultiplier;
                master.CriticalResistanceRate += 0.1 * statMultiplier;
                master.IgnoreCriticalRate += 0.1 * statMultiplier;
                master.PenetrationRate += 0.1 * statMultiplier;
                master.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                master.EvasionRate += 0.1 * statMultiplier;
                master.DamageAbsorptionRate += 0.1 * statMultiplier;
                master.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                master.AbsorbedDamageRate += 0.1 * statMultiplier;
                master.VitalityRegenerationRate += 0.1 * statMultiplier;
                master.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                master.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                master.LifestealRate += 0.1 * statMultiplier;
                master.Mana += 1500000 * statMultiplier;
                master.ManaRegenerationRate += 0.1 * statMultiplier;
                master.ShieldStrength += 1500000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                master.Tenacity += 0.5 * statMultiplier;
                master.ResistanceRate += 0.1 * statMultiplier;
                master.ComboRate += 0.1 * statMultiplier;
                master.IgnoreComboRate += 0.1 * statMultiplier;
                master.ComboDamageRate += 0.1 * statMultiplier;
                master.ComboResistanceRate += 0.1 * statMultiplier;
                master.StunRate += 0.1 * statMultiplier;
                master.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                master.ReflectionRate += 0.1 * statMultiplier;
                master.IgnoreReflectionRate += 0.1 * statMultiplier;
                master.ReflectionDamageRate += 0.1 * statMultiplier;
                master.ReflectionResistanceRate += 0.1 * statMultiplier;
                master.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                master.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                master.DamageToSameFactionRate += 0.1 * statMultiplier;
                master.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                master.NormalDamageRate += 0.1 * statMultiplier;
                master.NormalResistanceRate += 0.1 * statMultiplier;
                master.SkillDamageRate += 0.1 * statMultiplier;
                master.SkillResistanceRate += 0.1 * statMultiplier;
                master.PercentAllHealth += 5 * statMultiplier;
                master.PercentAllPhysicalAttack += 5 * statMultiplier;
                master.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                master.PercentAllMagicalAttack += 5 * statMultiplier;
                master.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                master.PercentAllChemicalAttack += 5 * statMultiplier;
                master.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                master.PercentAllAtomicAttack += 5 * statMultiplier;
                master.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                master.PercentAllMentalAttack += 5 * statMultiplier;
                master.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                master.PhysicalAttack += 1500000 * statMultiplier;
                master.MagicalAttack += 1500000 * statMultiplier;
                master.ChemicalAttack += 1500000 * statMultiplier;
                master.AtomicAttack += 1500000 * statMultiplier;
                master.MentalAttack += 1500000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                master.PhysicalDefense += 1500000 * statMultiplier;
                master.MagicalDefense += 1500000 * statMultiplier;
                master.ChemicalDefense += 1500000 * statMultiplier;
                master.AtomicDefense += 1500000 * statMultiplier;
                master.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                master.Speed += 1500000 * statMultiplier;
                master.CriticalDamageRate += 0.1 * statMultiplier;
                master.CriticalRate += 0.1 * statMultiplier;
                master.PenetrationRate += 0.1 * statMultiplier;
                master.EvasionRate += 0.1 * statMultiplier;
                master.DamageAbsorptionRate += 0.1 * statMultiplier;
                master.VitalityRegenerationRate += 0.1 * statMultiplier;
                master.AccuracyRate += 0.1 * statMultiplier;
                master.LifestealRate += 0.1 * statMultiplier;
                master.Mana += 1500000 * statMultiplier;
                master.ManaRegenerationRate += 0.1 * statMultiplier;
                master.ShieldStrength += 1500000 * statMultiplier;
                master.Tenacity += 0.5 * statMultiplier;
                master.ResistanceRate += 0.1 * statMultiplier;
                master.ComboRate += 0.1 * statMultiplier;
                master.ReflectionRate += 0.1 * statMultiplier;
                master.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                master.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                master.DamageToSameFactionRate += 0.1 * statMultiplier;
                master.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        master.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return master;
    }
    public async Task UpLevelAsync(object data, Master master, string type)
    {
        if (data is Equipments cardHero)
        {
            await UserCardHeroesMasterService.Create().InsertOrUpdateCardHeroMasterAsync(master, cardHero.Id);
        }
        else if (data is Books book)
        {
            await UserBooksMasterService.Create().InsertOrUpdateBookMasterAsync(master, book.Id);
        }
        else if (data is CardCaptains cardCaptain)
        {
            await UserCardCaptainsMasterService.Create().InsertOrUpdateCardCaptainMasterAsync(master, cardCaptain.Id);
        }
        else if (data is Pets pet)
        {
            await UserPetsMasterService.Create().InsertOrUpdatePetMasterAsync(master, pet.Id);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            await UserCardMilitariesMasterService.Create().InsertOrUpdateCardMilitaryMasterAsync(master, cardMilitary.Id);
        }
        else if (data is CardSpells cardSpell)
        {
            await UserCardSpellsMasterService.Create().InsertOrUpdateCardSpellMasterAsync(master, cardSpell.Id);
        }
        else if (data is CardMonsters cardMonster)
        {
            await UserCardMonstersMasterService.Create().InsertOrUpdateCardMonsterMasterAsync(master, cardMonster.Id);
        }
        else if (data is CardColonels cardColonel)
        {
            await UserCardColonelsMasterService.Create().InsertOrUpdateCardColonelMasterAsync(master, cardColonel.Id);
        }
        else if (data is CardGenerals cardGeneral)
        {
            await UserCardGeneralsMasterService.Create().InsertOrUpdateCardGeneralMasterAsync(master, cardGeneral.Id);
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            await UserCardAdmiralsMasterService.Create().InsertOrUpdateCardAdmiralMasterAsync(master, cardAdmiral.Id);
        }
    }
}
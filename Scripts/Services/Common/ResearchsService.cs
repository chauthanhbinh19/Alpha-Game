using System.Collections.Generic;
using System.Threading.Tasks;
public class ResearchsService : IResearchsService
{
    private static ResearchsService _instance;
    private readonly IResearchsRepository _ResearchsRepository;

    public ResearchsService(IResearchsRepository ResearchsRepository)
    {
        _ResearchsRepository = ResearchsRepository;
    }

    public static ResearchsService Create()
    {
        if (_instance == null)
        {
            _instance = new ResearchsService(new ResearchsRepository());
        }
        return _instance;
    }

    public async Task<Researchs> GetResearchsAsync(string id)
    {
        return await _ResearchsRepository.GetResearchsAsync(id);
    }

    public async Task<Researchs> GetSumResearchsAsync(string user_id)
    {
        return await _ResearchsRepository.GetSumResearchsAsync(user_id);
    }

    public async Task InsertOrUpdateResearchsAsync(string userId, Researchs Researchs, string id)
    {
        await _ResearchsRepository.InsertOrUpdateResearchsAsync(userId, Researchs, id);
    }

    public Researchs EnhanceResearchs(Researchs research, int level, int multiplier = 1)
    {
        int startLevel = research.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = multiplier;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                research.Health += 100000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.PhysicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                research.MagicalAttack += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                research.AtomicAttack += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                research.MentalAttack += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.CriticalResistanceRate += 0.1 * statMultiplier;
                research.IgnoreCriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                research.AbsorbedDamageRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.IgnoreComboRate += 0.1 * statMultiplier;
                research.ComboDamageRate += 0.1 * statMultiplier;
                research.ComboResistanceRate += 0.1 * statMultiplier;
                research.StunRate += 0.1 * statMultiplier;
                research.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                research.ReflectionRate += 0.1 * statMultiplier;
                research.IgnoreReflectionRate += 0.1 * statMultiplier;
                research.ReflectionDamageRate += 0.1 * statMultiplier;
                research.ReflectionResistanceRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                research.NormalDamageRate += 0.1 * statMultiplier;
                research.NormalResistanceRate += 0.1 * statMultiplier;
                research.SkillDamageRate += 0.1 * statMultiplier;
                research.SkillResistanceRate += 0.1 * statMultiplier;
                research.PercentAllHealth += 5 * statMultiplier;
                research.PercentAllPhysicalAttack += 5 * statMultiplier;
                research.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                research.PercentAllMagicalAttack += 5 * statMultiplier;
                research.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                research.PercentAllChemicalAttack += 5 * statMultiplier;
                research.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                research.PercentAllAtomicAttack += 5 * statMultiplier;
                research.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                research.PercentAllMentalAttack += 5 * statMultiplier;
                research.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                research.PhysicalAttack += 15000000 * statMultiplier;
                research.MagicalAttack += 15000000 * statMultiplier;
                research.ChemicalAttack += 15000000 * statMultiplier;
                research.AtomicAttack += 15000000 * statMultiplier;
                research.MentalAttack += 15000000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                research.PhysicalDefense += 15000000 * statMultiplier;
                research.MagicalDefense += 15000000 * statMultiplier;
                research.ChemicalDefense += 15000000 * statMultiplier;
                research.AtomicDefense += 15000000 * statMultiplier;
                research.MentalDefense += 15000000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                research.Speed += 15000000 * statMultiplier;
                research.CriticalDamageRate += 0.1 * statMultiplier;
                research.CriticalRate += 0.1 * statMultiplier;
                research.PenetrationRate += 0.1 * statMultiplier;
                research.EvasionRate += 0.1 * statMultiplier;
                research.DamageAbsorptionRate += 0.1 * statMultiplier;
                research.VitalityRegenerationRate += 0.1 * statMultiplier;
                research.AccuracyRate += 0.1 * statMultiplier;
                research.LifestealRate += 0.1 * statMultiplier;
                research.Mana += 15000000 * statMultiplier;
                research.ManaRegenerationRate += 0.1 * statMultiplier;
                research.ShieldStrength += 15000000 * statMultiplier;
                research.Tenacity += 0.5 * statMultiplier;
                research.ResistanceRate += 0.1 * statMultiplier;
                research.ComboRate += 0.1 * statMultiplier;
                research.ReflectionRate += 0.1 * statMultiplier;
                research.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                research.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                research.DamageToSameFactionRate += 0.1 * statMultiplier;
                research.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        research.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return research;
    }
}
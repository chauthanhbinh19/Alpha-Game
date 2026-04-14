using System.Collections;
using System.Collections.Generic;

public class CardGenerals : BaseEntity, IPowerSortable, ICard
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Rare { get; set; }
    public double Quality { get; set; }
    public string Type { get; set; }
    public int CurrentStar { get; set; }
    public int TempStar { get; set; }
    public int Star { get; set; }
    public int Level { get; set; }
    public double Experiment { get; set; }
    public double Quantity { get; set; }
    public bool Block { get; set; }
    public string Position { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string TeamId { get; set; }
    public double PercentAllHealth { get; set; }
    public double PercentAllPhysicalAttack { get; set; }
    public double PercentAllPhysicalDefense { get; set; }
    public double PercentAllMagicalAttack { get; set; }
    public double PercentAllMagicalDefense { get; set; }
    public double PercentAllChemicalAttack { get; set; }
    public double PercentAllChemicalDefense { get; set; }
    public double PercentAllAtomicAttack { get; set; }
    public double PercentAllAtomicDefense { get; set; }
    public double PercentAllMentalAttack { get; set; }
    public double PercentAllMentalDefense { get; set; }
    public Currencies Currency { get; set; }
    public BaseStats BaseStats { get; set; } = new BaseStats();
    public List<Skills> Skills { get; set; } = new List<Skills>();
    public Teams Team { get; set; } = new Teams();
    double IPowerSortable.Power => Power;
    public List<Emblems> Emblems { get; set; } = new List<Emblems>();
    public CardGenerals()
    {
        Power = -1;
        Health = -1;
        PhysicalAttack = -1;
        PhysicalDefense = -1;
        MagicalAttack = -1;
        MagicalDefense = -1;
        ChemicalAttack = -1;
        ChemicalDefense = -1;
        AtomicAttack = -1;
        AtomicDefense = -1;
        MentalAttack = -1;
        MentalDefense = -1;
        Speed = -1;
        CriticalDamageRate = -1;
        CriticalRate = -1;
        PenetrationRate = -1;
        EvasionRate = -1;
        DamageAbsorptionRate = -1;
        VitalityRegenerationRate = -1;
        AccuracyRate = -1;
        LifestealRate = -1;
        Mana = -1;
        ManaRegenerationRate = -1;
        ShieldStrength = -1;
        Tenacity = -1;
        ResistanceRate = -1;
        ComboRate = -1;
        ReflectionRate = -1;
        DamageToDifferentFactionRate = -1;
        ResistanceToDifferentFactionRate = -1;
        DamageToSameFactionRate = -1;
        ResistanceToSameFactionRate = -1;

        TeamId= "-1";
        PercentAllHealth = -1;
        PercentAllPhysicalAttack = -1;
        PercentAllPhysicalDefense = -1;
        PercentAllMagicalAttack = -1;
        PercentAllMagicalDefense = -1;
        PercentAllChemicalAttack = -1;
        PercentAllChemicalDefense = -1;
        PercentAllAtomicAttack = -1;
        PercentAllAtomicDefense = -1;
        PercentAllMentalAttack = -1;
        PercentAllMentalDefense = -1;
    }
    
}

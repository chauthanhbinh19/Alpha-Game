using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;


public class PowerManager
{
    public double Power { get; set; } = 0;
    public double Health { get; set; } = 0;
    public double PhysicalAttack { get; set; } = 0;
    public double PhysicalDefense { get; set; } = 0;
    public double MagicalAttack { get; set; } = 0;
    public double MagicalDefense { get; set; } = 0;
    public double ChemicalAttack { get; set; } = 0;
    public double ChemicalDefense { get; set; } = 0;
    public double AtomicAttack { get; set; } = 0;
    public double AtomicDefense { get; set; } = 0;
    public double MentalAttack { get; set; } = 0;
    public double MentalDefense { get; set; } = 0;
    public double Speed { get; set; } = 0;
    public double CriticalDamageRate { get; set; } = 0;
    public double CriticalRate { get; set; } = 0;
    public double CriticalResistanceRate { get; set; } = 0;
    public double IgnoreCriticalRate { get; set; } = 0;
    public double PenetrationRate { get; set; } = 0;
    public double PenetrationResistanceRate { get; set; } = 0;
    public double EvasionRate { get; set; } = 0;
    public double DamageAbsorptionRate { get; set; } = 0;
    public double IgnoreDamageAbsorptionRate { get; set; } = 0;
    public double AbsorbedDamageRate { get; set; } = 0;
    public double VitalityRegenerationRate { get; set; } = 0;
    public double VitalityRegenerationResistanceRate { get; set; } = 0;
    public double AccuracyRate { get; set; } = 0;
    public double LifestealRate { get; set; } = 0;
    public float Mana { get; set; } = 0;
    public double ManaRegenerationRate { get; set; } = 0;
    public double ShieldStrength { get; set; } = 0;
    public double Tenacity { get; set; } = 0;
    public double ResistanceRate { get; set; } = 0;
    public double ComboRate { get; set; } = 0;
    public double IgnoreComboRate { get; set; } = 0;
    public double ComboDamageRate { get; set; } = 0;
    public double ComboResistanceRate { get; set; } = 0;
    public double StunRate { get; set; } = 0;
    public double IgnoreStunRate { get; set; } = 0;
    public double ReflectionRate { get; set; } = 0;
    public double IgnoreReflectionRate { get; set; } = 0;
    public double ReflectionDamageRate { get; set; } = 0;
    public double ReflectionResistanceRate { get; set; } = 0;
    public double DamageToDifferentFactionRate { get; set; } = 0;
    public double ResistanceToDifferentFactionRate { get; set; } = 0;
    public double DamageToSameFactionRate { get; set; } = 0;
    public double ResistanceToSameFactionRate { get; set; } = 0;
    public double NormalDamageRate { get; set; } = 0;
    public double NormalResistanceRate { get; set; } = 0;
    public double SkillDamageRate { get; set; } = 0;
    public double SkillResistanceRate { get; set; } = 0;
    public double PercentAllHealth { get; set; } = 0;
    public double PercentAllPhysicalAttack { get; set; } = 0;
    public double PercentAllPhysicalDefense { get; set; } = 0;
    public double PercentAllMagicalAttack { get; set; } = 0;
    public double PercentAllMagicalDefense { get; set; } = 0;
    public double PercentAllChemicalAttack { get; set; } = 0;
    public double PercentAllChemicalDefense { get; set; } = 0;
    public double PercentAllAtomicAttack { get; set; } = 0;
    public double PercentAllAtomicDefense { get; set; } = 0;
    public double PercentAllMentalAttack { get; set; } = 0;
    public double PercentAllMentalDefense { get; set; } = 0;
    public const double coefficient = 0.5;

    // Start is called before the first frame update
    public PowerManager()
    {

    }
    
    
}

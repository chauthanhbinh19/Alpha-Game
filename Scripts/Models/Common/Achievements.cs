using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class Achievements : BaseEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Rare { get; set; }
    public int Quality { get; set; }
    public int CurrentStar { get; set; }
    public int TempStar { get; set; }
    public string Type { get; set; }
    public int Star { get; set; }
    public int Level { get; set; }
    public double Experiment { get; set; }
    public double Quantity { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
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
    public Achievements()
    {

    }
    
}

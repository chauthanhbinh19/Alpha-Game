using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class Equipments : BaseEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Rare { get; set; }
    public int Quality { get; set; }
    public string Type { get; set; }
    public int CurrentStar { get; set; }
    public int TempStar { get; set; }
    public string Set { get; set; }
    public int Star { get; set; }
    public int Sequence { get; set; }
    public int Level { get; set; }
    public int Experiment { get; set; }
    public int Quantity { get; set; }
    public int Block { get; set; }
    public double SpecialHealth { get; set; }
    public double SpecialPhysicalAttack { get; set; }
    public double SpecialPhysicalDefense { get; set; }
    public double SpecialMagicalAttack { get; set; }
    public double SpecialMagicalDefense { get; set; }
    public double SpecialChemicalAttack { get; set; }
    public double SpecialChemicalDefense { get; set; }
    public double SpecialAtomicAttack { get; set; }
    public double SpecialAtomicDefense { get; set; }
    public double SpecialMentalAttack { get; set; }
    public double SpecialMentalDefense { get; set; }
    public double SpecialSpeed { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string CurrencyImage { get; set; }
    public double Price { get; set; }
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
    public int Position { get; set; }
    public Equipments()
    {
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
        Position = -1;
    }
}

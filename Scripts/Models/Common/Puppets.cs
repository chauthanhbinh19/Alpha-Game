using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class Puppets : BaseEntity
{
    private string id1;
    private string name1;
    private string image1;
    private string rare1;
    private string type1;
    private int star1;
    private int level1;
    private int experiment1;
    private int quantity1;
    private string description1;
    private string status1;
    private int quality1;

    public string Id { get => id1; set => id1 = value; }
    public string Name { get => name1; set => name1 = value; }
    public string Image { get => image1; set => image1 = value; }
    public string Rare { get => rare1; set => rare1 = value; }
    public int Quality { get => quality1; set => quality1 = value; }
    public string Type { get => type1; set => type1 = value; }
    public int CurrentStar { get; set; }
    public int TempStar { get; set; }
    public int Star { get => star1; set => star1 = value; }
    public int Level { get => level1; set => level1 = value; }
    public int Experiment { get => experiment1; set => experiment1 = value; }
    public int Quantity { get => quantity1; set => quantity1 = value; }
    public string Description { get => description1; set => description1 = value; }
    public string Status { get => status1; set => status1 = value; }
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
    public Puppets()
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
    }
    
}

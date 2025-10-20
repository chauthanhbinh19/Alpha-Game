using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class MasterBoard : BaseEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string RankLevel { get; set; }
    public string Type { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
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
    public MasterBoard()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class Equipments : BaseEntity
{
    private string id1;
    private string name1;
    private string image1;
    private string rare1;
    private string type1;
    private string set1;
    private int star1;
    private int sequence1;
    private int level1;
    private int experiment1;
    private int quantity1;
    private int block1;
    private double special_health1;
    private double special_physical_attack1;
    private double special_physical_defense1;
    private double special_magical_attack1;
    private double special_magical_defense1;
    private double special_chemical_attack1;
    private double special_chemical_defense1;
    private double special_atomic_attack1;
    private double special_atomic_defense1;
    private double special_mental_attack1;
    private double special_mental_defense1;
    private double special_speed1;
    private string description1;
    private string status1;
    private string currency_image1;
    private double price1;
    private int position1;
    private int quality1;

    public string Id { get => id1; set => id1 = value; }
    public string Name { get => name1; set => name1 = value; }
    public string Image { get => image1; set => image1 = value; }
    public string Rare { get => rare1; set => rare1 = value; }
    public int Quality { get => quality1; set => quality1 = value; }
    public string Type { get => type1; set => type1 = value; }
    public int CurrentStar { get; set; }
    public int TempStar { get; set; }
    public string Set { get => set1; set => set1 = value; }
    public int Star { get => star1; set => star1 = value; }
    public int Sequence { get => sequence1; set => sequence1 = value; }
    public int Level { get => level1; set => level1 = value; }
    public int Experiment { get => experiment1; set => experiment1 = value; }
    public int Quantity { get => quantity1; set => quantity1 = value; }
    public int Block { get => block1; set => block1 = value; }
    public double SpecialHealth { get => special_health1; set => special_health1 = value; }
    public double SpecialPhysicalAttack { get => special_physical_attack1; set => special_physical_attack1 = value; }
    public double SpecialPhysicalDefense { get => special_physical_defense1; set => special_physical_defense1 = value; }
    public double SpecialMagicalAttack { get => special_magical_attack1; set => special_magical_attack1 = value; }
    public double SpecialMagicalDefense { get => special_magical_defense1; set => special_magical_defense1 = value; }
    public double SpecialChemicalAttack { get => special_chemical_attack1; set => special_chemical_attack1 = value; }
    public double SpecialChemicalDefense { get => special_chemical_defense1; set => special_chemical_defense1 = value; }
    public double SpecialAtomicAttack { get => special_atomic_attack1; set => special_atomic_attack1 = value; }
    public double SpecialAtomicDefense { get => special_atomic_defense1; set => special_atomic_defense1 = value; }
    public double SpecialMentalAttack { get => special_mental_attack1; set => special_mental_attack1 = value; }
    public double SpecialMentalDefense { get => special_mental_defense1; set => special_mental_defense1 = value; }
    public double SpecialSpeed { get => special_speed1; set => special_speed1 = value; }
    public string Description { get => description1; set => description1 = value; }
    public string Status { get => status1; set => status1 = value; }
    public string CurrencyImage { get => currency_image1; set => currency_image1 = value; }
    public double Price { get => price1; set => price1 = value; }
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
    public int Position { get => position1; set => position1 = value; }
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

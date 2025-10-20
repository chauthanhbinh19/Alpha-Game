using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Currencies
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int Quantity { get; set; }
    public Currencies()
    {
        
    }
}
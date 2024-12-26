using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Currency
{
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public int quantity { get; set; }
}
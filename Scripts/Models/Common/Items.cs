using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Items
{
    public string id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public string type { get; set; }
    public int price { get; set; }
    public string description { get; set; }
    public int quantity { get; set; }
    public Items()
    {

    }
}

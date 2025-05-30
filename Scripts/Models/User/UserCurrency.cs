using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class UserCurrency
{
    private string id1;
    private string name1;
    private string image1;
    private int quantity1;

    public string id { get => id1; set => id1 = value; }
    public string name { get => name1; set => name1 = value; }
    public string image { get => image1; set => image1 = value; }
    public int quantity { get => quantity1; set => quantity1 = value; }
    public UserCurrency()
    {
        
    }
}
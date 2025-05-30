using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Features
{
    private string feature_name1;
    private int required_level1;
    private string type1;
    private string description1;

    public string feature_name { get => feature_name1; set => feature_name1 = value; }
    public int required_level { get => required_level1; set => required_level1 = value; }
    public string type { get => type1; set => type1 = value; }
    public string description { get => description1; set => description1 = value; }

}

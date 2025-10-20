using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Features
{
    public string FeatureName { get; set; }
    public int RequiredLevel { get; set; }
    public string Type { get; set ; }
    public string Description { get; set; }

}

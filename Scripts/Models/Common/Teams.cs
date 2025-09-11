using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Teams
{
    public string user_id { get; set; }
    public string team_id { get; set; }
    public int team_number { get; set; }
    public string team_avatar { get; set; }
    public string team_border { get; set; }
    public Teams()
    {

    }
}

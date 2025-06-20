using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Teams
{
    private string user_id1;
    private string team_id1;

    public string user_id { get => user_id1; set => user_id1 = value; }
    public string team_id { get => team_id1; set => team_id1 = value; }
    public Teams()
    {

    }
}

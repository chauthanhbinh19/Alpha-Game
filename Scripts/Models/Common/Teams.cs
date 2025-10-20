using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Teams
{
    public string UserId { get; set; }
    public string TeamId { get; set; }
    public int TeamNumber { get; set; }
    public string TeamAvatar { get; set; }
    public string TeamBorder { get; set; }
    public Teams()
    {

    }
}

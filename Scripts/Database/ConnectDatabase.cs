using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public static class DatabaseConfig
{
    public static string ConnectionString => "server=localhost;port=3306;database=alpha;user=root;password=binh123456";
}

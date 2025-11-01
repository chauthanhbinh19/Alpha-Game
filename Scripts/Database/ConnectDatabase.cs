using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public static class DatabaseConfig
{
    public static string ConnectionString => "Server=localhost; Port=3306; Database=alpha; User=root; Password=binh123456; Pooling=true; Max Pool Size=100;";
}

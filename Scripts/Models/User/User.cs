using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

public class User
{
    // public GameObject signInPanel;
    // public GameObject namePanel;
    public string id;
    public string name;
    public string image;
    public string border;
    public int level;
    public int experiment;
    public int vip;
    public double power;
    public string Username { get; set; }
    public string Password { get; set; }
    public static string CurrentUserId { get; set; }
    public static int CurrentUserLevel { get; set; }
    public static string CurrentUserAvatar { get; set; }
    public static string CurrentUserBorder { get; set; }
    public static string savedUsername;
    public static string savedPassword;
    public List<Currency> Currencies { get; set; }
    public User(){
        Currencies = new List<Currency>();
        power = 0;
    }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

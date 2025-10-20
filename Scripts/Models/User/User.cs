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
    public string Id;
    public string Name;
    public string Image;
    public string Border;
    public int Level;
    public int Experiment;
    public int Vip;
    public double Power;
    public string Username { get; set; }
    public string Password { get; set; }
    public static string CurrentUserId { get; set; }
    public static int CurrentUserLevel { get; set; }
    public static string CurrentUserAvatar { get; set; }
    public static string CurrentUserBorder { get; set; }
    public static string SavedUsername;
    public static string SavedPassword;
    public List<Currencies> Currencies { get; set; }
    public User(){
        Currencies = new List<Currencies>();
        Power = 0;
    }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
